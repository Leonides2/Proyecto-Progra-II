using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.Models.Custom;
using Proyecto_Progra_II.Models;
using Services.Interfaces;

namespace Proyecto_Progra_II.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuariosService _usuariosService;
        private readonly IEmailService _emailService;
        private readonly IConfiguration _config;

        public UsuarioController(IUsuariosService usuariosService, IEmailService emailService, IConfiguration config)
        {
            _usuariosService = usuariosService;
            _emailService = emailService;
            _config = config;
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


        [AllowAnonymous]
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
            SmtpSettings settings = new SmtpSettings();
            settings.Port = _config.GetValue<int>("SmtpSettings:Port");
            settings.Server = _config.GetValue<string>("SmtpSettings:Server");
            settings.Username = _config.GetValue<string>("SmtpSettings:Username");
            settings.Password = _config.GetValue<string>("SmtpSettings:Password");

            var newUsuario = await _usuariosService.PostUsuario(usuario);

            string subject = "Bienvenido a nuestra clinica";
            string message = $"Hola {usuario.Name}, bienvenido a nuestra aplicación.";

            await _emailService.SendEmailAsync(usuario.Email, subject, message, settings );


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
