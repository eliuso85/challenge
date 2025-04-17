using Dapper;
using Domain.DTO;
using Domain.Request;
using Infrastructure.Repository.Interfaces;
using StackExchange.Redis;
using System.Data;
using System.Text.Json;

namespace Infrastructure.Repository.Data;
public class RepositorySession(IDbConnection db) : IRepositorySession
{
    private readonly IDbConnection _db = db;

    public async Task<IEnumerable<Session>> ObtenerSesionesActivasAsync()
    {
        return await _db.QueryAsync<Session>("[dbo].[sp_ObtenerSesionesActivas]", commandType: CommandType.StoredProcedure);
    }

    public async Task<Recurso?> ObtenerCapacidadMaxAsync(int recursoId)
    {
        var Params = new DynamicParameters();
        Params.Add($"@id", recursoId, DbType.Int32);
        return await _db.QuerySingleOrDefaultAsync<Recurso>("[dbo].[sp_GetListaRecursos]", Params, commandType: CommandType.StoredProcedure);
    }

    public async Task<Session?> ObtenerCapacidadOcupadaAsync(int recursoId)
    {
        var Params = new DynamicParameters();
        Params.Add($"@RecursoId", recursoId, DbType.Int32);
        return await _db.QuerySingleOrDefaultAsync<Session>("[dbo].[sp_GetCapacidadOcupada]", Params, commandType: CommandType.StoredProcedure);
    }

    public async Task<Session?> PostSesionAsync(Session session)
    {
        var Params = new DynamicParameters();
        Params.Add($"@UserId", session.UserId, DbType.Int32);
        Params.Add($"@RecursoId", session.RecursoId, DbType.Int32);
        Params.Add($"@CantidadRequerida", session.CantidadRequerida, DbType.Int32);
        return await _db.QuerySingleOrDefaultAsync<Session>("[dbo].[sp_IniciarSesion]", Params, commandType: CommandType.StoredProcedure);
    }

    public async Task<Session?> PostFinalizaSesionAsync(Session session)
    {
        var Params = new DynamicParameters();
        Params.Add($"@SessionId", session.Id, DbType.Guid);
        Params.Add($"@RecursoId", session.RecursoId, DbType.Int32);
        return await _db.QuerySingleOrDefaultAsync<Session>("[dbo].[sp_FinalizarSesion]", Params, commandType: CommandType.StoredProcedure);
    }

    #region  Codigo para usar Cache en Redis
    //private readonly IDatabase _redis;

    //public RepositorySession(IDbConnection db, IConnectionMultiplexer redis)
    //{
    //    _db = db;
    //    _redis = redis.GetDatabase(); // Aquí extraemos la base Redis
    //}

    //public async Task<IEnumerable<Session>> ObtenerSesionesActivasAsync()
    //{
    //    var cacheKey = "sesiones_activas";
    //    string cached = await _redis.StringGetAsync(cacheKey);

    //    if (!string.IsNullOrEmpty(cached))
    //    {
    //        // Deserializar desde Redis
    //        return JsonSerializer.Deserialize<IEnumerable<Session>>(cached)!;
    //    }

    //    // Consultar desde la base de datos
    //    var sesiones = await _db.QueryAsync<Session>(
    //        "[dbo].[sp_ObtenerSesionesActivas]",
    //        commandType: CommandType.StoredProcedure
    //    );

    //    // Serializar y guardar en caché
    //    var serialized = JsonSerializer.Serialize(sesiones);
    //    await _redis.StringSetAsync(cacheKey, serialized, TimeSpan.FromMinutes(5));

    //    return sesiones;
    //}
    #endregion
}

