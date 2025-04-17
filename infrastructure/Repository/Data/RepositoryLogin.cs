using Dapper;
using Domain.DTO;
using Domain.Request;
using Infrastructure.Repository.Interfaces;
using System.Data;

namespace Infrastructure.Repository.Data;
public class RepositoryLogin(IDbConnection db) : IRepositoryLogin
{
    private readonly IDbConnection _db = db;

    public async Task<User?> LoginUserAsync(LoginRequest user)
    {
        var Params = new DynamicParameters();
        Params.Add($"@Correo", user.Email, DbType.String);
        return await _db.QuerySingleOrDefaultAsync<User>("[dbo].sp_GetUserByEmail",
                                Params, 
                                commandType: CommandType.StoredProcedure);
    }
    public async Task<SesionActiva?> LoginUserActiveAsync(User user)
    {
        var Params = new DynamicParameters();
        Params.Add($"@id", user.Id, DbType.String);
        return await _db.QuerySingleOrDefaultAsync<SesionActiva>("[dbo].sp_GetSesionesActivas",
                                Params,
                                commandType: CommandType.StoredProcedure);
    }
    public async Task<SesionActiva?> SaveUserActiveAsync(SesionActiva sesionActiva)
    {
        var Params = new DynamicParameters();
        Params.Add($"@id", sesionActiva.Id, DbType.String);
        Params.Add($"@Token", sesionActiva.Token, DbType.String);
        Params.Add($"@FechaExpiracion", sesionActiva.FechaExpiracion, DbType.DateTime);
        return await _db.QuerySingleOrDefaultAsync<SesionActiva>("[dbo].sp_PostSesionesActivas",
                                Params,
                                commandType: CommandType.StoredProcedure);
    }
}
