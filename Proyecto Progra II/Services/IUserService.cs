using Proyecto_Progra_II.Entities;
namespace Proyecto_Progra_II.Services
{
    public interface IUserService
    {
        Task<User> ReturnToken(UserResponse response);
    }
}
