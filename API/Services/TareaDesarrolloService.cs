using SGSPCSI.API.Models;
using SGSPCSI.API.Repositories;

namespace SGSPCSI.API.Services
{
    public interface ITareaDesarrolloService
    {
        Task<TareaDesarrollo?> GetTareaByIdAsync(long id);
        Task<IEnumerable<TareaDesarrollo>> GetTareasPorSolicitudAsync(long solicitudId);
        Task<IEnumerable<TareaDesarrollo>> GetTareasAsignadasAAsync(int usuarioId);
        Task<long> CreateTareaAsync(TareaDesarrollo tarea);
        Task UpdateTareaAsync(TareaDesarrollo tarea);
        Task DeleteTareaAsync(long id);
    }

    public class TareaDesarrolloService : ITareaDesarrolloService
    {
        private readonly ITareaDesarrolloRepository _tareaRepository;

        public TareaDesarrolloService(ITareaDesarrolloRepository tareaRepository)
        {
            _tareaRepository = tareaRepository;
        }

        public async Task<TareaDesarrollo?> GetTareaByIdAsync(long id)
        {
            return await _tareaRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<TareaDesarrollo>> GetTareasPorSolicitudAsync(long solicitudId)
        {
            return await _tareaRepository.GetTareasPorSolicitudAsync(solicitudId);
        }

        public async Task<IEnumerable<TareaDesarrollo>> GetTareasAsignadasAAsync(int usuarioId)
        {
            return await _tareaRepository.GetTareasAsignadasAAsync(usuarioId);
        }

        public async Task<long> CreateTareaAsync(TareaDesarrollo tarea)
        {
            await _tareaRepository.AddAsync(tarea);
            await _tareaRepository.SaveChangesAsync();
            return tarea.TareaDesarrolloId;
        }

        public async Task UpdateTareaAsync(TareaDesarrollo tarea)
        {
            _tareaRepository.Update(tarea);
            await _tareaRepository.SaveChangesAsync();
        }

        public async Task DeleteTareaAsync(long id)
        {
            var tarea = await _tareaRepository.GetByIdAsync(id);
            if (tarea != null)
            {
                tarea.Activo = false;
                _tareaRepository.Update(tarea);
                await _tareaRepository.SaveChangesAsync();
            }
        }
    }
}
