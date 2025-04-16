using Application.Interfaces;
using Domain.DTO;
using Domain.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiGestionChallenge.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class GestionController(IGestionService gestionService): ControllerBase
{
    #region GET

    #endregion

    #region POST
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
