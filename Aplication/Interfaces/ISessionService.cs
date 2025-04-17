using Domain.Request;
using Domain.Response;

namespace Application.Interfaces;

public interface ISessionService
{
    Task<ResponseApi> IniciarSesionAsync(IniciarSesionRequest request);
    Task<ResponseApi> FinalizarSesionAsync(FinalizarSesionRequest request);
    Task<ResponseApi> ObtenerSesionesActivasAsync();
}
