using Microsoft.EntityFrameworkCore;
using Proyecto_Progra_II.Models;

namespace Proyecto_Progra_II.Services.Usuarios
{
    public class UsuariosService : IUsuariosService
    {

        private readonly ApiContext _context;


        public UsuariosService(ApiContext context)
        {
            _context = context;
        }
    

        public Task<Usuario> DeleteUsuario(int id)
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

 

        public Task<List<Usuario>> GetUsuarios()
        {
            var ListCitas = new List<Cita>();
            ListCitas = await _context.Citas.ToListAsync();


            return ListCitas;
        }

        public Task<Usuario> GetUsuarios(int id)
        {
            var cita = await _context.Citas.FindAsync(id);


            return cita;
        }

        public Task<Usuario> PostUsuario(Cita cita)
        {
            _context.Citas.Add(cita);
            await _context.SaveChangesAsync();

            return cita;
        }

        public Task<Usuario> PutUsuario(int id, Cita cita)
        {
            _context.Entry(cita).State = EntityState.Modified;
            await _context.SaveChangesAsync();


            return cita;
        }
    }
}
