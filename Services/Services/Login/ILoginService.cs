using Proyecto_Progra_II.Models.Custom;
namespace Proyecto_Progra_II.Services.Login
{
    public interface ILoginService
    {
        Task<UsuarioResponse> ReturnToken(UsuarioRequest request, string secret);
    }
}
