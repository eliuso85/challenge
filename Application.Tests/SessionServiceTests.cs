using Application.Interfaces;
using Application.Services;
using Infrastructure.Repository.Interfaces;
using Domain.DTO;
using Domain.Request;
using Domain.Response;
using Moq;
using Xunit;
namespace Application.Tests;


public class SessionServiceTests
{
    private readonly Mock<IRepositorySession> _mockRepo;
    private readonly SessionService _service;

    public SessionServiceTests()
    {
        _mockRepo = new Mock<IRepositorySession>();
        _service = new SessionService(_mockRepo.Object);
    }

    //[Fact]
    //public async Task IniciarSesionAsync_DeberiaRetornarError_SiNoHayCapacidad()
    //{
    //    var request = new IniciarSesionRequest { RecursoId = 1, CantidadRequerida = 5 };

    //    _mockRepo.Setup(expression: static r => r.ObtenerCapacidadOcupadaAsync(1)).ReturnsAsync(2);

    //    var result = await _service.IniciarSesionAsync(request);

    //    Assert.False(result.Exito);
    //    Assert.Equal("Capacidad insuficiente para iniciar la sesión.", result.Mensaje);
    //}

    [Fact]
    public async Task ObtenerSesionesActivasAsync_DeberiaRetornarListaDeSesiones()
    {
        var sesiones = new List<Session>
    {
        new() { SessionId = Guid.NewGuid(), RecursoId = 1, Estado = "activa" }
    };

        _mockRepo.Setup(static r => r.ObtenerSesionesActivasAsync()).ReturnsAsync(sesiones);

        var result = await _service.ObtenerSesionesActivasAsync();

        Assert.NotNull(result);
        Assert.Single((IAsyncEnumerable<Session>)result);
        Assert.Equal("activa", result.Estado);
    }

    [Fact]
    public async Task FinalizarSesionAsync_DeberiaCambiarEstado_SiSesionExiste()
    {
        var session = new Session { SessionId = Guid.NewGuid(), RecursoId = 1 };

        _mockRepo.Setup(r => r.PostFinalizaSesionAsync(session)).ReturnsAsync(session);

        var result = _service.FinalizarSesion(session);

        Assert.NotNull(result);
        Assert.Equal(session.SessionId, result);
    }


}