using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.Models.Custom;
using Proyecto_Progra_II.Models;
using Services.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Proyecto_Progra_II.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuariosService _usuariosService;
        private readonly IEmailService _emailService;
        private readonly IConfiguration _config;
        private readonly ApiContext _context;

        public UsuarioController(IUsuariosService usuariosService, IEmailService emailService, IConfiguration config, ApiContext context)
        {
            _usuariosService = usuariosService;
            _emailService = emailService;
            _config = config;
            _context = context;
        }


        //[Authorize(Policy = "AdminPolicy")]
        [Authorize]
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

        [HttpPost("{Jtoken}")]
        public async Task<IActionResult> GetUserFromToken(string Jtoken)
        {
            var user = await  _usuariosService.ReadUsuarioToken(Jtoken);

            return Ok(user);  
        }

        //[Authorize(Policy = "UserPolicy")]
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
            SmtpSettings settings = new()
            {
                Port = _config.GetValue<int>("SmtpSettings:Port"),
                Server = _config.GetValue<string>("SmtpSettings:Server"),
                Username = _config.GetValue<string>("SmtpSettings:Username"),
                Password = _config.GetValue<string>("SmtpSettings:Password")
            };

            usuario.IdRol = 2;

            var newUsuario = await _usuariosService.PostUsuario(usuario);

            if(newUsuario == null)
            {
                return BadRequest("Email already in use");
            }

            string subject = "Bienvenido a nuestra clinica";
            string message = $"Hola {usuario.Name}, bienvenido a nuestra aplicación.";
            string table = "";

            await _emailService.SendEmailAsync(usuario.Email, subject, message, settings, table);


            return Ok(newUsuario);
        }

        [Authorize(Policy = "AdminPolicy")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsuario(int id)
        {
            var user = await _usuariosService.DeleteUsuario(id);
            if (user == null)
            {
                return NotFound();
            }

            return Ok("Usuario deleted succesfully");
        }

    }
}
