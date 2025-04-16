using Dapper;
using Domain.DTO;
using Infrastructure.Repository.Interfaces;
using System.Data;
using Infrastructure.Tools;
using Domain.Response;
using Domain.Request;

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
}
