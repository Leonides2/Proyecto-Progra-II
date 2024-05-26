using Microsoft.AspNetCore.Mvc;
using Proyecto_Progra_II.Models;
using Proyecto_Progra_II.Services.Roles;



namespace Proyecto_Progra_II.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {

        private readonly IRolesService _rolesService;

        public RolesController(IRolesService rolesService)
        {
            _rolesService = rolesService;
        }

        [HttpGet]
        public async Task<IActionResult> GetRoles()
        {
            var roles_request = await _rolesService.GetRoles();

            if (roles_request == null)
            {
                return NoContent();
            }

            return Ok(roles_request);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetRoles(int id)
        {
            var rol = await _rolesService.GetRoles(id);

            if (rol == null)
            {
                return NotFound();
            }

            return Ok(rol);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> PutRol(int id, Rol rol)
        {
            if (id != rol.Id)
            {
                return BadRequest();
            }


            var newRol = await _rolesService.PutRol(id, rol);

            return Ok(newRol);
        }

        [HttpPost]
        public async Task<IActionResult> PostRol(Rol rol)
        {
            var newRol = await _rolesService.PostRol(rol);

            return Ok(newRol);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRol(int id)
        {
            var rol = await _rolesService.DeleteRol(id);
            if (rol == null)
            {
                return NotFound();
            }

            return Ok("Rol deleted succesfully");

        }
    }
}
