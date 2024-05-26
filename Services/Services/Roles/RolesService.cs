using Microsoft.EntityFrameworkCore;
using Proyecto_Progra_II.Models;
using Proyecto_Progra_II.Services.Roles;


namespace Services.Services.Roles
{
    public class RolesService : IRolesService
    {

        private readonly ApiContext _context;

        public RolesService(ApiContext apiContext)
        {
            _context = apiContext;
        }

        public async Task<Rol> DeleteRol(int id)
        {
            var rol = await _context.Roles.FindAsync(id);
            if (rol == null)
            {
                return null;
            }

            _context.Roles.Remove(rol);
            await _context.SaveChangesAsync();

            return rol;
        }

        public async Task<List<Rol>> GetRoles()
        {
            var ListRoles = new List<Rol>();
            ListRoles = await _context.Roles.ToListAsync();


            return ListRoles;
        }

        public async Task<Rol> GetRoles(int id)
        {
            var rol = await _context.Roles.FindAsync(id);


            return rol;
        }

        public async Task<Rol> PostRol(Rol rol)
        {
            _context.Roles.Add(rol);
            await _context.SaveChangesAsync();

            return rol;
        }

        public async Task<Rol> PutRol(int id, Rol rol)
        {
            _context.Entry(rol).State = EntityState.Modified;
            await _context.SaveChangesAsync();


            return rol;
        }
    }
}
