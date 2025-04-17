using Application.Interfaces;
using Application.Services;
using Domain.DTO;
using Domain.Request;
using Domain.Response;
using Microsoft.AspNetCore.Mvc;


namespace ApiGestionChallenge.Controllers
{
    /// <summary>
    ///  controlador de Autorizacion en su primera version
    /// </summary>
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AuthController(ILoginService loginService) : ControllerBase
    {
        #region GET

        #endregion

        #region POST

        /// <summary>
        ///  control de Autorizacion y login
        /// </summary>
        [HttpPost]
        [Route("login")]
        public async Task<ResponseApi> Login([FromBody] LoginRequest request)
        => await loginService.LoginUserSync(request);
        #endregion

        #region PUT 
        #endregion

        #region DELETE
        #endregion
    }
}
