using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Proyecto_Progra_II.Models;
using Proyecto_Progra_II.Services.Especialidades;

namespace Proyecto_Progra_II.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EspecialidadesController : ControllerBase
    {
        private readonly IEspecialidadService _especialidadesService;

        public EspecialidadesController(IEspecialidadService especialidad)
        {
            _especialidadesService = especialidad;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetEspecialidades()
        {
            var especialidades_request = await _especialidadesService.GetEspecialidades();

            if (especialidades_request == null)
            {
                return NoContent();
            }

            return Ok(especialidades_request);
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetEspecialidades(int id)
        {
            var especialidad = await _especialidadesService.GetEspecialidades(id);

            if (especialidad == null)
            {
                return NotFound();
            }

            return Ok(especialidad);
        }

        [Authorize(Policy = "AdminPolicy")]
        [HttpPost]
        public async Task<IActionResult> PostEspecialidad(Especialidad especialidad)
        {
            var newEspecialidad = await _especialidadesService.PostEspecialidad(especialidad);

            return Ok(newEspecialidad);
        }

        [Authorize(Policy = "AdminPolicy")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEspecidalidad(int id, Especialidad especialidad)
        {
            if (id != especialidad.Id)
            {
                return BadRequest();
            }


            var newEspecialidad = await _especialidadesService.PutEspecidalidad(id, especialidad);

            return Ok(newEspecialidad);
        }

        [Authorize(Policy = "AdminPolicy")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEspecialidad(int id)
        {
            var especialidad = await _especialidadesService.DeleteEspecialidad(id);
            if (especialidad == null)
            {
                return NotFound();
            }

            return Ok("Especialidad deleted succesfully");
        }

    }
}
