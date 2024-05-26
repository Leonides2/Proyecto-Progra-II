using Microsoft.EntityFrameworkCore;
using Proyecto_Progra_II.Models;
using Proyecto_Progra_II.Services.EstadosCitas;


namespace Services.Services.EstadosCitas
{
    public class EstadoCitaService : IEstadoCitaService
    {
        private readonly ApiContext _context;

        public EstadoCitaService(ApiContext context)
        {
            _context = context;
        }

        public async Task<EstadosCita> DeleteEstadoCita(int id)
        {
            var estadoCita = await _context.EstadosCitas.FindAsync(id);
            if (estadoCita == null)
            {
                return null;
            }

            _context.EstadosCitas.Remove(estadoCita);
            await _context.SaveChangesAsync();

            return estadoCita;
        }

        public async Task<List<EstadosCita>> GetEstadosCitas()
        {
            var ListEstadoCita = new List<EstadosCita>();
            ListEstadoCita = await _context.EstadosCitas.ToListAsync();


            return ListEstadoCita;
        }

        public async Task<EstadosCita> GetEstadosCitas(int id)
        {
            var estadoCita = await _context.EstadosCitas.FindAsync(id);


            return estadoCita;
        }

        public async Task<EstadosCita> PostEstadoCita(EstadosCita estadosCita)
        {
            _context.EstadosCitas.Add(estadosCita);
            await _context.SaveChangesAsync();

            return estadosCita;
        }

            public async Task<EstadosCita> PutEstadoCita(int id, EstadosCita estadosCita)
        {
            _context.Entry(estadosCita).State = EntityState.Modified;
            await _context.SaveChangesAsync();


            return estadosCita;
        }
    }
}
