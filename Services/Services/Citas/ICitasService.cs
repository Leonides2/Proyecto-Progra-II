using Proyecto_Progra_II.Models;

namespace Proyecto_Progra_II.Services.Citas
{
    public interface ICitasService
    {

        Task<List<Cita>> GetCitas();
        Task<Cita> GetCitas(int id);
        Task<List<Cita>> GetCitasUsuarios(int idUsuario);

        Task<Cita> PostCita(Cita cita);

        Task<Cita> PutCita(int id, Cita cita);
        Task<Cita> DeleteCita(int id);
    }
}
