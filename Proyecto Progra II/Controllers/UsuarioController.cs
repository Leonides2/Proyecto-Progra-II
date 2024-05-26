using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Proyecto_Progra_II.Models;

using Proyecto_Progra_II.Services.Usuarios;

namespace Proyecto_Progra_II.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuariosService _usuariosService;

        public UsuarioController(IUsuariosService usuariosService)
        {
            _usuariosService = usuariosService;
        }

        [Authorize(Policy = "AdminPolicy")]
        [HttpGet]
        public async Task<IActionResult> GetUsuarios()
        {
            var usuarios_request = await _usuariosService.GetUsuarios();

            if (usuarios_request == null)
            {
                return NoContent();
            }

            return Ok(usuarios_request);
        }


        [Authorize(Policy = "UserPolicy")]
        [Authorize(Policy = "AdminPolicy")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUsuarios(int id)
        {
            var usuario = await _usuariosService.GetUsuarios(id);

            if (usuario == null)
            {
                return NotFound();
            }

            return Ok(usuario);
        }

        [Authorize(Policy = "UserPolicy")]
        [Authorize(Policy = "AdminPolicy")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUsuario(int id, Usuario usuario)
        {
            if (id != usuario.Id)
            {
                return BadRequest();
            }


            var newUsuario = await _usuariosService.PutUsuario(id, usuario);

            return Ok(newUsuario);


        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> PostUsuario(Usuario usuario)
        {
            var newUsuario = await _usuariosService.PostUsuario(usuario);

            return Ok(newUsuario);
        }

        [Authorize(Policy = "AdminPolicy")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsuario(int id)
        {
            var cita = await _usuariosService.DeleteUsuario(id);
            if (cita == null)
            {
                return NotFound();
            }

            return Ok("Usuario deleted succesfully");
        }

    }
}
