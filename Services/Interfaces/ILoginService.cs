using Proyecto_Progra_II.Models.Custom;
namespace Services.Interfaces
{
    public interface ILoginService
    {
        Task<UsuarioResponse> ReturnToken(UsuarioRequest request, string secret);
    }
}
