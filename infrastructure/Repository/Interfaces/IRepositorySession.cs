using Domain.DTO;
using Domain.Request;
namespace Infrastructure.Repository.Interfaces;
public interface IRepositorySession
{
    Task<IEnumerable<Session>> ObtenerSesionesActivasAsync();
    Task<Recurso?> ObtenerCapacidadMaxAsync(int recursoId);
    Task<Session?> ObtenerCapacidadOcupadaAsync(int recursoId);
    Task<Session?> PostSesionAsync(Session session);
    Task<Session?> PostFinalizaSesionAsync(Session session);
}
