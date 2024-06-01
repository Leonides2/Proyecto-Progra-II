using Proyecto_Progra_II.Models;

namespace Services.Interfaces
{
    public interface IUsuariosService
    {

        Task<List<Usuario>> GetUsuarios();
        Task<Usuario> GetUsuarios(int id);

        Task<Usuario> ReadUsuarioToken(string Jtoken);

        Task<Usuario> PostUsuario(Usuario usuario);

        Task<Usuario> PutUsuario(int id, Usuario usuario);
        Task<Usuario> DeleteUsuario(int id);
    }
}
