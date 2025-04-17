using Application.Interfaces;
using Domain.DTO;
using Domain.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiGestionChallenge.Controllers;

/// <summary>
///  controlador de Gestion
/// </summary>
[Route("api/v1/[controller]")]
[ApiController]
public class GestionController(IGestionService gestionService): ControllerBase
{
    #region GET

    #endregion

    #region POST
    /// <summary>
    ///  controlador de Gestion guarda el nuevo usuario y manda correo electronico
    /// </summary>
    [HttpPost]
    [Route("SaveUser")]
    public async Task<ResponseApi> SaveUser([FromBody] User user)
    => await gestionService.SaveUserSync(user);
    #endregion

    #region PUT 
    #endregion

    #region DELETE
    #endregion
}
