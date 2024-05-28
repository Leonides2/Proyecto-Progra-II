using Proyecto_Progra_II.Models;

namespace Services.Interfaces
{
    public interface IEstadoCitaService
    {
        Task<List<EstadosCita>> GetEstadosCitas();
        Task<EstadosCita> GetEstadosCitas(int id);

        Task<EstadosCita> PostEstadoCita(EstadosCita estadosCita);

        Task<EstadosCita> PutEstadoCita(int id, EstadosCita estadosCita);
        Task<EstadosCita> DeleteEstadoCita(int id);
    }
}
