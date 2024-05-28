using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Proyecto_Progra_II.Models;
using Services.Interfaces;



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

        [AllowAnonymous]
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


        [AllowAnonymous]
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


        [Authorize(Policy = "AdminPolicy")]
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

        [Authorize(Policy = "AdminPolicy")]
        [HttpPost]
        public async Task<IActionResult> PostRol(Rol rol)
        {
            var newRol = await _rolesService.PostRol(rol);

            return Ok(newRol);
        }

        [Authorize(Policy = "AdminPolicy")]
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
