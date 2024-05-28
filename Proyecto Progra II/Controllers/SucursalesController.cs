using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Proyecto_Progra_II.Models;
using Services.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Proyecto_Progra_II.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SucursalesController : ControllerBase
    {
        private readonly ISucursalService _service;

        public SucursalesController(ISucursalService service)
        {
            _service = service;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetSucursales()
        {
            var sucursales_request = await _service.GetSucursales();

            if (sucursales_request == null)
            {
                return NoContent();
            }

            return Ok(sucursales_request);
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSucursales(int id)
        {
            var sucursal = await _service.GetSucursales(id);

            if (sucursal == null)
            {
                return NotFound();
            }

            return Ok(sucursal);
        }

        [Authorize(Policy = "AdminPolicy")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRol(int id, Sucursal sucursal)
        {
            if (id != sucursal.Id)
            {
                return BadRequest();
            }


            var newSucursal = await _service.PutSucursal(id, sucursal);

            return Ok(newSucursal);
        }

        [Authorize(Policy = "AdminPolicy")]
        [HttpPost]
        public async Task<IActionResult> PostSucursal(Sucursal sucursal)
        {
            var newSucursal = await _service.PostSucursal(sucursal);

            return Ok(newSucursal);
        }

        [Authorize(Policy = "AdminPolicy")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSucursal(int id)
        {
            var sucursal = await _service.DeleteSucursal(id);
            if (sucursal == null)
            {
                return NotFound();
            }

            return Ok("Sucursal deleted succesfully");

        }
    }
}
