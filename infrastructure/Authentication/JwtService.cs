using Domain.DTO;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Infrastructure.Authentication;

public class JwtTokenGenerator(IOptions<JwtSettings> jwtOptions)
{
    private readonly JwtSettings _jwtSettings = jwtOptions.Value;

    public SesionActiva GenerateToken(User user)
    {
        SesionActiva sesionActiva = new();

        var claims = new[]
          {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role.ToString())
            };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.Aes128CbcHmacSha256);
        var expiresInMinutes = DateTime.Now.AddMinutes(_jwtSettings.ExpirationInMinutes);

        var token = new JwtSecurityToken(
            issuer: _jwtSettings.Issuer,
            audience: _jwtSettings.Audience,
            claims: claims,
            expires: expiresInMinutes,
            signingCredentials: credentials
        );

        sesionActiva.Id = user.Id;
        sesionActiva.Token = new JwtSecurityTokenHandler().WriteToken(token);
        sesionActiva.FechaExpiracion = expiresInMinutes;

        return sesionActiva;
    }


}
