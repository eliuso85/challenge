using Application.Interfaces;
using Application.Services;
using Domain.Request;
using Domain.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.Eventing.Reader;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace ApiGestionChallenge.Controllers;

/// <summary>
///  controlador de Sesiones
/// </summary>
[Route("api/v1/[controller]")]
[ApiController]
public class SesionesController(ISessionService _service) : ControllerBase
{
    #region GET
    /// <summary>
    /// Lista todas las sesiones activas
    /// </summary>
    [HttpGet("activas")]
    //[Authorize(Roles = "admin")] // Solo administradores pueden ver todas las sesiones
    public async Task<IActionResult> ObtenerSesionesActivas()
    {
        var sesiones = await _service.ObtenerSesionesActivasAsync();
        return Ok(sesiones);
    }
    #endregion

    #region POST
    /// <summary>
    ///IniciarSesion
    /// </summary>
    [HttpPost]
    [Route("iniciar")]
    //[Authorize(Roles = "User")]
    public async Task<ResponseApi> IniciarSesion([FromBody] IniciarSesionRequest request)
    => await _service.IniciarSesionAsync(request);

    /// <summary>
    ///FinalizarSesion
    /// </summary>
    [HttpPost]
    [Route("finalizar")]
    //[Authorize(Roles = "admin")] 
    public async Task<ResponseApi> FinalizarSesion([FromBody] FinalizarSesionRequest request)
    => await _service.FinalizarSesionAsync(request);
    #endregion

    #region PUT 
    #endregion

    #region DELETE
    #endregion
}
