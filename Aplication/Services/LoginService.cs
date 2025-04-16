using Application.Interfaces;
using Domain.DTO;
using Domain.Request;
using Domain.Response;
using Infrastructure.Authentication;
using Infrastructure.Repository.Interfaces;
using Infrastructure.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services;

public class LoginService(IRepositoryLogin _repositoryLogin, JwtTokenGenerator _jwtTokenGenerator) : ILoginService
{
    #region Builder
    private readonly ResponseApi responseApi = new();
    #endregion

    public async Task<ResponseApi> LoginUserSync(LoginRequest userLogin)
    {
        try
        {
            var dataUser = await _repositoryLogin.LoginUserAsync(userLogin);

            if (dataUser == null)
            {
                responseApi.Success = false;
                responseApi.Mensajes.Add($"El Usuario con el correo: {userLogin.Email} no encontrado, revisa la información!!");
                return responseApi;
            }

            var isValid = PasswordHelper.VerifyPassword(userLogin.Password, dataUser.Password);

            if (!isValid)
            {
                responseApi.Success = false;
                responseApi.Mensajes.Add($"El Usuario con el correo: {userLogin.Email} no es valido, revisa información!!");
                return responseApi;
            }
            else
            {
                var gettoken = _jwtTokenGenerator.GenerateToken(dataUser);

                var authResponse = new AuthResponse
                {
                    Token = gettoken,
                    Email = dataUser.Email,
                    Role = dataUser.Role
                };

                responseApi.Data = authResponse;
                responseApi.Success = true;
                responseApi.Mensajes.Add($"El Usuario con el correo: {userLogin.Email} se valido correctamente");
            }
        }
        catch (Exception ex)
        {
            responseApi.Success = false;
            responseApi.Mensajes.Add(ex.Message);
        }
        return responseApi;
    }
}
