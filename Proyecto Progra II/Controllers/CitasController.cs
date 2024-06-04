using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.Models.Custom;
using Proyecto_Progra_II.Models;
using Services.Interfaces;
using System.Data;
using System.Security.Claims;

namespace Proyecto_Progra_II.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class CitasController : ControllerBase
    {
        private readonly ICitasService _citasService;
        private readonly IEmailService _emailService;
        private readonly IConfiguration _configuration;
        private readonly ApiContext _context;

        public CitasController(ICitasService citasService, IEmailService emailService, IConfiguration configuration, ApiContext context)
        {
            _citasService = citasService;
            _emailService = emailService;
            _configuration = configuration;
            _context = context;
        }



        [HttpGet]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> GetCitas()
        {
            var citas_request = await _citasService.GetCitas();

            if (citas_request == null)
            {
                return NoContent();
            }

            return Ok(citas_request);
        }




        [HttpGet("{id}")]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> GetCita(int id)
        {
            var cita = await _citasService.GetCitas(id);

            if (cita == null)
            {
                return NotFound();
            }

            return Ok(cita);
        }


        [HttpGet]
        [Authorize(Policy = "UserPolicy")]
        [Route("/UsuarioCitas/" + "{id}")]
        public async Task<IActionResult> GetCitas(int id)
        {
                
            var cita = await _citasService.GetCitasUsuarios(id);

            if (cita == null)
            {
                return Ok(new List<Cita>());
            }

            return Ok(cita);
        }

        [HttpGet]
        [Authorize(Policy = "AdminPolicy")]
        [Route("/Hoy")]
        public async Task<IActionResult> GetTodayUserCitas()
        {

            var cita = await _citasService.getTodayUserCitas();

            if (cita == null)
            {
                return Ok(new List<Cita>());
            }

            return Ok(cita);
        }


        [HttpPut("{id}")]
        [Authorize(Policy = "UserPolicy")]
        public async Task<IActionResult> PutCita(int id, Cita cita)
        {
            if (id != cita.Id)
            {
                return BadRequest();
            }

            if (cita.IdEstado == 2)
            {
                DateTime dateNow = DateTime.Now;
                TimeSpan timeSpan = cita.Fecha.Subtract(dateNow);

                if (timeSpan.TotalHours < 24 )
                {
                    return BadRequest("can't cancel cita");

                }else
                {

                    var user = await _context.Usuarios.FindAsync(cita.IdPaciente);
                    var sucursal = await _context.Sucursales.FindAsync(cita.IdSucursal);
                    var especialidad = await _context.Especialidades.FindAsync(cita.IdEspecialidad);
                    var estado = await _context.EstadosCitas.FindAsync(cita.IdEstado);
                    var date = cita.Fecha;

                    SmtpSettings settings = new SmtpSettings();
                    settings.Port = _configuration.GetValue<int>("SmtpSettings:Port");
                    settings.Server = _configuration.GetValue<string>("SmtpSettings:Server");
                    settings.Username = _configuration.GetValue<string>("SmtpSettings:Username");
                    settings.Password = _configuration.GetValue<string>("SmtpSettings:Password");

                    string subject = "Resumen de los nuevos datos de su cita";
                    string message = $"Hola {user.Name}, aqui tienes un resumen de los datos modificados de su cita el dia " + dateNow.ToShortDateString() + " .";
                    string table = "<table>\r\n        " +
                        "<tr>\r\n            " +
                            "<th> </th>\r\n            " +
                            "<th>Datos</th>\r\n        " +
                        "</tr>\r\n        " +
                        "<tr>\r\n           " +
                            "<td> Fecha </td>\r\n           " +
                            "<td> " + date.ToShortDateString() + " </td>\r\n        " +
                        "</tr>\r\n    " +
                        "<tr>\r\n           " +
                            "<td> Hora </td>\r\n           " +
                            "<td> " + date.ToShortTimeString() + " </td>\r\n        " +
                        "</tr>\r\n    " +
                        "<tr>\r\n           " +
                            "<td> Especialidad </td>\r\n           " +
                            "<td> " + especialidad.Nombre + " </td>\r\n        " +
                        "</tr>\r\n    " +
                        "<tr>\r\n           " +
                            "<td> Sucursal </td>\r\n           " +
                            "<td> " + sucursal.NombreSucursal + " </td>\r\n        " +
                        "</tr>\r\n    " +
                         "<tr>\r\n           " +
                            "<td> Lugar </td>\r\n           " +
                            "<td> " + cita.Lugar + " </td>\r\n        " +
                        "</tr>\r\n    " +
                        "<tr>\r\n           " +
                            "<td> Estado </td>\r\n           " +
                            "<td> " + estado.NombreEstado + " </td>\r\n        " +
                        "</tr>\r\n    " +
                        "</table>\r\n\r\n    ";


                    await _emailService.SendEmailAsync(user.Email, subject, message, settings, table);

                    var newCita = await _citasService.PutCita(id, cita);

                    return Ok(newCita);
                }
            }
            else
            {
                var newCita = await _citasService.PutCita(id, cita);

                return Ok(newCita);
            }


        }



        
        [HttpPost]
        [Authorize(Policy = "UserPolicy")]
        public async Task<IActionResult> PostCita(Cita cita)
        {

            switch (cita.IdEspecialidad)
            {
                case 1:
                    cita.Lugar = "Consultorio 1";
                    break;
                case 2:
                    cita.Lugar = "Consultorio 6";
                    break;
                case 3:
                    cita.Lugar = "Consultorio 3";
                    break;
                case 4:
                    cita.Lugar = "Consultorio 4";
                    break;
                default:
                    return BadRequest();

            }

            cita.IdEstado = 1;

            var user = await _context.Usuarios.FindAsync(cita.IdPaciente);
            var sucursal = await _context.Sucursales.FindAsync(cita.IdSucursal);
            var especialidad = await _context.Especialidades.FindAsync(cita.IdEspecialidad);
            var estado = await _context.EstadosCitas.FindAsync(cita.IdEstado);
            var date = cita.Fecha;

            if (_citasService.HasCitaTheSameDay(cita))
            {
                return BadRequest("The user has another appointment the same day");
            }
            else {

                DateTime dateNow = DateTime.Now;

                var newCita = await _citasService.PostCita(cita);

                if (newCita == null)
                {
                    return BadRequest();
                }
                else {
                    SmtpSettings settings = new SmtpSettings();
                    settings.Port = _configuration.GetValue<int>("SmtpSettings:Port");
                    settings.Server = _configuration.GetValue<string>("SmtpSettings:Server");
                    settings.Username = _configuration.GetValue<string>("SmtpSettings:Username");
                    settings.Password = _configuration.GetValue<string>("SmtpSettings:Password");

                    string subject = "Resumen de su nueva cita";
                    string message = $"Hola {user.Name}, aqui tienes un resumen de tu cita agendada el dia " + dateNow.ToShortDateString() + " .";
                    string table = "<table>\r\n        " +
                        "<tr>\r\n            " +
                            "<th> </th>\r\n            " +
                            "<th>Datos</th>\r\n        " +
                        "</tr>\r\n        " +
                        "<tr>\r\n           " +
                            "<td> Fecha </td>\r\n           " +
                            "<td> " + date.ToShortDateString() + " </td>\r\n        " +
                        "</tr>\r\n    " +
                        "<tr>\r\n           " +
                            "<td> Hora </td>\r\n           " +
                            "<td> " + date.ToShortTimeString() + " </td>\r\n        " +
                        "</tr>\r\n    " +
                        "<tr>\r\n           " +
                            "<td> Especialidad </td>\r\n           " +
                            "<td> " + especialidad.Nombre + " </td>\r\n        " +
                        "</tr>\r\n    " +
                        "<tr>\r\n           " +
                            "<td> Sucursal </td>\r\n           " +
                            "<td> " + sucursal.NombreSucursal + " </td>\r\n        " +
                        "</tr>\r\n    " +
                         "<tr>\r\n           " +
                            "<td> Lugar </td>\r\n           " +
                            "<td> " + cita.Lugar + " </td>\r\n        " +
                        "</tr>\r\n    " +
                        "<tr>\r\n           " +
                            "<td> Estado </td>\r\n           " +
                            "<td> " + estado.NombreEstado + " </td>\r\n        " +
                        "</tr>\r\n    " +
                        "</table>\r\n\r\n    ";


                    await _emailService.SendEmailAsync(user.Email, subject, message, settings, table);
                    return Ok(newCita);
                }

                
            }
        }

        
        [HttpDelete("{id}")]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> DeleteCita(int id)
        {
            var cita = await _citasService.DeleteCita(id);
            if (cita == null)
            {
                return NotFound();
            }

            return Ok("Cita deleted succesfully");
        }

    }
}
