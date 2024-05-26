using Microsoft.EntityFrameworkCore;
using Proyecto_Progra_II.Models;
using Proyecto_Progra_II.Services.Sucursales;


namespace Services.Services.Sucursales
{
    public class SucursalService : ISucursalService
    {
        private readonly ApiContext _context;

        public SucursalService(ApiContext context)
        {
            _context = context;
        }

        public async Task<Sucursal> DeleteSucursal(int id)
        {
            var sucursal = await _context.Sucursales.FindAsync(id);
            if (sucursal == null)
            {
                return null;
            }

            _context.Sucursales.Remove(sucursal);
            await _context.SaveChangesAsync();

            return sucursal;
        }

        public async Task<List<Sucursal>> GetSucursales()
        {
            var ListSucursales = new List<Sucursal>();
            ListSucursales = await _context.Sucursales.ToListAsync();


            return ListSucursales;
        }

        public async Task<Sucursal> GetSucursales(int id)
        {
            var sucursal = await _context.Sucursales.FindAsync(id);


            return sucursal;
        }

        public async Task<Sucursal> PostSucursal(Sucursal sucursal)
        {
            _context.Sucursales.Add(sucursal);
            await _context.SaveChangesAsync();

            return sucursal;
        }

        public async Task<Sucursal> PutSucursal(int id, Sucursal sucursal)
        {
            _context.Sucursales.Add(sucursal);
            await _context.SaveChangesAsync();

            return sucursal;
        }
    }
}
