using Dapper;
using Domain.DTO;
using Infrastructure.Repository.Interfaces;
using System.Data;
using Infrastructure.Tools;

namespace Infrastructure.Repository.Data;
public class RepositoryGestion(IDbConnection db) : IRepositoryGestion
{
    private readonly IDbConnection _db = db;
       
    public async Task<User> saveUserAsync(User user)
    {
        var pHashedPassword = PasswordHelper.HashPassword(user.Password);

        var Params = new DynamicParameters();

        Params.Add($"@Nombre", user.Nombre, DbType.String);
        Params.Add($"@Correo", user.Email, DbType.String);
        Params.Add($"@ContrasenaHash", pHashedPassword, DbType.String);
        Params.Add($"@Role", user.Role, DbType.String);

        return await _db.QuerySingleAsync<User>("[dbo].sp_SaveUser", Params, commandType: CommandType.StoredProcedure);
    }
}
