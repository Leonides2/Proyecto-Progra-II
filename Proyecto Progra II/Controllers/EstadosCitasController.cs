using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Proyecto_Progra_II.Models;

namespace Proyecto_Progra_II.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstadosCitasController : ControllerBase
    {
        private readonly ApiContext _context;

        public EstadosCitasController(ApiContext context)
        {
            _context = context;
        }

        // GET: api/EstadosCitas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EstadosCita>>> GetEstadosCitas()
        {
            return await _context.EstadosCitas.ToListAsync();
        }

        // GET: api/EstadosCitas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EstadosCita>> GetEstadosCita(int id)
        {
            var estadosCita = await _context.EstadosCitas.FindAsync(id);

            if (estadosCita == null)
            {
                return NotFound();
            }

            return estadosCita;
        }

        // PUT: api/EstadosCitas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEstadosCita(int id, EstadosCita estadosCita)
        {
            if (id != estadosCita.Id)
            {
                return BadRequest();
            }

            _context.Entry(estadosCita).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EstadosCitaExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/EstadosCitas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<EstadosCita>> PostEstadosCita(EstadosCita estadosCita)
        {
            _context.EstadosCitas.Add(estadosCita);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEstadosCita", new { id = estadosCita.Id }, estadosCita);
        }

        // DELETE: api/EstadosCitas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEstadosCita(int id)
        {
            var estadosCita = await _context.EstadosCitas.FindAsync(id);
            if (estadosCita == null)
            {
                return NotFound();
            }

            _context.EstadosCitas.Remove(estadosCita);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EstadosCitaExists(int id)
        {
            return _context.EstadosCitas.Any(e => e.Id == id);
        }
    }
}
