using Proyecto_Progra_II.Models;

namespace Services.Interfaces
{
    public interface IEspecialidadService
    {
        Task<List<Especialidad>> GetEspecialidades();
        Task<Especialidad> GetEspecialidades(int id);

        Task<Especialidad> PostEspecialidad(Especialidad especialidad);

        Task<Especialidad> PutEspecidalidad(int id, Especialidad especialidad);
        Task<Especialidad> DeleteEspecialidad(int id);

    }
}
