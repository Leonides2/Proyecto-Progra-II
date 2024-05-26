using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Proyecto_Progra_II.Models;
using Proyecto_Progra_II.Models.Custom;
using Proyecto_Progra_II.Services.Login;

namespace Proyecto_Progra_II.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {

        private readonly ILoginService _loginService;
        private readonly IConfiguration _configuration;

        public LoginController(ILoginService loginService, IConfiguration configuration)
        {
            _loginService = loginService;
            _configuration = configuration;
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] UsuarioRequest request)
        {
            var user_request = await _loginService.ReturnToken(request, _configuration.GetValue<string>("JwtSettings:key"));

            if (user_request == null)
            {
                return Unauthorized();
            }

            return Ok(user_request);
        }

    }
}
