using Proyecto_Progra_II.Models;

namespace Services.Interfaces
{
    public interface IRolesService
    {
        Task<List<Rol>> GetRoles();
        Task<Rol> GetRoles(int id);

        Task<Rol> PostRol(Rol rol);

        Task<Rol> PutRol(int id, Rol rol);
        Task<Rol> DeleteRol(int id);

    }
}
