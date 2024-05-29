using Microsoft.EntityFrameworkCore;
using Proyecto_Progra_II.Models;
using Services.Interfaces;
using System.Data;

namespace Proyecto_Progra_II.Services.Citas
{
    public class CitasService : ICitasService
    {

        private readonly ApiContext _context;


        public CitasService(ApiContext context)
        {
            _context = context;
        }
    
        async public Task<Cita> DeleteCita(int id)
        {
            var cita = await _context.Citas.FindAsync(id);
            if (cita == null)
            {
                return null;
            }

            _context.Citas.Remove(cita);
            await _context.SaveChangesAsync();

            return cita;
        }

        public async Task<List<Cita>> GetCitas()
        {
            var ListCitas = new List<Cita>();
            ListCitas = await _context.Citas.ToListAsync();


            return ListCitas;
        }

        async public Task<Cita> GetCitas(int id)
        {
            var cita = await _context.Citas.FindAsync(id);


            return cita;
        }

        async public Task<List<Cita>> GetCitasUsuarios(int idUsuario)
        {
            var listCitas = _context.Citas.Where<Cita>(item => item.IdPaciente == idUsuario).ToList();
            return listCitas;
        }



        async public Task<Cita> PostCita(Cita cita)
        {
            _context.Citas.Add(cita);
            await _context.SaveChangesAsync();

            return cita;
        }



        async public Task<Cita> PutCita(int id, Cita cita)
        {
            _context.Entry(cita).State = EntityState.Modified;
            await _context.SaveChangesAsync();
      

            return cita; 
        }

        public bool HasCitaTheSameDay(Cita cita) {


            var userDb = _context.Usuarios.FirstOrDefault(item => item.Id == cita.IdPaciente)!;
            var ListCita = _context.Citas.Where(item => item.IdPaciente == userDb.Id).ToList();
            var hasCita = ListCita.Find(item => item.Fecha.DayOfYear == cita.Fecha.DayOfYear);

            if (hasCita != null)
            {
                return true;
            }


            return false;
        }

    }
}
