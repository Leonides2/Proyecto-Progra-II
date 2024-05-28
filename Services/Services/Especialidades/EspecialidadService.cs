using Microsoft.EntityFrameworkCore;
using Proyecto_Progra_II.Models;
using Services.Interfaces;

namespace Proyecto_Progra_II.Services.Especialidades
{
    public class EspecialidadService : IEspecialidadService
    {

        private readonly ApiContext _context;

        public EspecialidadService(ApiContext context)
        {
            _context = context;
        }
        public async Task<Especialidad> DeleteEspecialidad(int id)
        {
            var especialidad = await _context.Especialidades.FindAsync(id);
            if (especialidad == null)
            {
                return null;
            }

            _context.Especialidades.Remove(especialidad);
            await _context.SaveChangesAsync();

            return especialidad;
        }

        public async Task<List<Especialidad>> GetEspecialidades()
        {
            var ListEspecialidades = new List<Especialidad>();
            ListEspecialidades = await _context.Especialidades.ToListAsync();


            return ListEspecialidades;
        }

        public async Task<Especialidad> GetEspecialidades(int id)
        {
            var especialidad = await _context.Especialidades.FindAsync(id);


            return especialidad;
        }

        public async Task<Especialidad> PostEspecialidad(Especialidad especialidad)
        {
            _context.Especialidades.Add(especialidad);
            await _context.SaveChangesAsync();

            return especialidad;
        }

        public async Task<Especialidad> PutEspecidalidad(int id, Especialidad especialidad)
        {
            _context.Entry(especialidad).State = EntityState.Modified;
            await _context.SaveChangesAsync();


            return especialidad;
        }
    }
}
