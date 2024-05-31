using Proyecto_Progra_II.Models;

namespace Services.Interfaces
{
    public interface ICitasService
    {

        Task<List<Cita>> GetCitas();
        Task<Cita> GetCitas(int id);
        Task<List<Cita>> GetCitasUsuarios(int idUsuario);

        //Task<List<Cita>> GetCitasUsuarioFromToken(string token);

        Task<Cita> PostCita(Cita cita);

        Task<Cita> PutCita(int id, Cita cita);
        Task<Cita> DeleteCita(int id);

        public bool HasCitaTheSameDay(Cita cita);
    }  
}
