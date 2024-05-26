using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Proyecto_Progra_II.Models;
using Proyecto_Progra_II.Services.Citas;

namespace Proyecto_Progra_II.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class CitasController : ControllerBase
    {
        private readonly ICitasService _citasService;
        

        public CitasController(ICitasService citasService)
        {
            _citasService = citasService;
        }


        [Authorize(Policy = "AdminPolicy")]
        [HttpGet]
        public async Task<IActionResult> GetCitas()
        {
            var citas_request = await _citasService.GetCitas();

            if (citas_request == null)
            {
                return NoContent();
            }

            return Ok(citas_request);
        }

        [Authorize(Policy = "AdminPolicy")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCita(int id)
        {
            var cita = await _citasService.GetCitas(id);

            if (cita == null)
            {
                return NotFound();
            }

            return Ok(cita);
        }

        [Authorize(Policy = "UserPolicy")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCita(int id, Cita cita)
        {
            if (id != cita.Id)
            {
                return BadRequest();
            }


            var newCita = await _citasService.PutCita(id, cita);

            return Ok(newCita);
        

        }

        [Authorize(Policy = "UserPolicy")]
        [HttpPost]
        public async Task<IActionResult> PostCita(Cita cita)
        {   
            var newCita = await _citasService.PostCita(cita);

            return Ok(newCita);
        }

        [Authorize(Policy = "AdminPolicy")]
        [HttpDelete("{id}")]
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
