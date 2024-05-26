
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Proyecto_Progra_II.Models;
using Proyecto_Progra_II.Services.EstadosCitas;

namespace Proyecto_Progra_II.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstadosCitasController : ControllerBase
    {
        private readonly IEstadoCitaService _estadoCitaService;

        public EstadosCitasController(IEstadoCitaService estadoCitaService)
        {
            _estadoCitaService = estadoCitaService;
        }


        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetEstadosCitas()
        {
            var estadosCitas_request = await _estadoCitaService.GetEstadosCitas();

            if (estadosCitas_request == null)
            {
                return NoContent();
            }

            return Ok(estadosCitas_request);
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetEstadosCitas(int id)
        {
            var estadoCita = await _estadoCitaService.GetEstadosCitas(id);

            if (estadoCita == null)
            {
                return NotFound();
            }

            return Ok(estadoCita);
        }

        [Authorize(Policy = "AdminPolicy")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEstadoCita(int id, EstadosCita estadosCita)
        {
            if (id != estadosCita.Id)
            {
                return BadRequest();
            }


            var newEstadoCita = await _estadoCitaService.PutEstadoCita(id, estadosCita);

            return Ok(newEstadoCita);
        }

        [Authorize(Policy = "AdminPolicy")]
        [HttpPost]
        public async Task<IActionResult> PostEstadoCita(EstadosCita estadosCita)
        {
            var newEstadoCita = await _estadoCitaService.PostEstadoCita(estadosCita);

            return Ok(newEstadoCita);
        }

        [Authorize(Policy = "AdminPolicy")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEstadoCita(int id)
        {
            var estadoCita = await _estadoCitaService.DeleteEstadoCita(id);
            if (estadoCita == null)
            {
                return NotFound();
            }

            return Ok("Estadocita deleted succesfully");

        }

    }
}
