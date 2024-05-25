using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Proyecto_Progra_II.Models;
using Proyecto_Progra_II.Models.Custom;
using Proyecto_Progra_II.Services.Login;

namespace Proyecto_Progra_II.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly ILoginService _loginService;
        private readonly ApiContext _context;
        public UsuarioController(ILoginService loginService)
        {
            _loginService = loginService;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<Usuario>>> GetUsuario()
        {
            return await _context.Usuarios.ToListAsync();
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] UsuarioRequest request)
        {
            var user_request = await _loginService.ReturnToken(request);

            if (user_request == null)
            {
                return Unauthorized();
            }

            return Ok(user_request);
        }

        [HttpPost]
        public async Task<ActionResult<Usuario>> PostUsuario(Usuario usuario)
        {
            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUsuario", new { id = usuario.Id }, usuario);
        }
    }
}
