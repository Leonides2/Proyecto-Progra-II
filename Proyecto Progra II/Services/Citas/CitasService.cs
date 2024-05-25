using Microsoft.EntityFrameworkCore;
using Proyecto_Progra_II.Models;

namespace Proyecto_Progra_II.Services.Citas
{
    public class CitasService : ICitasService
    {

        private readonly ApiContext _context;


        public CitasService(ApiContext context)
        {
            _context = context;
        }
    
        public Task<Cita> DeleteCita(int id)
        {
            throw new NotImplementedException();
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

    }
}
