using SGSPCSI.API.Models;
using SGSPCSI.API.Repositories;

namespace SGSPCSI.API.Services
{
    public interface IProyectoService
    {
        Task<Proyecto?> GetProyectoByIdAsync(long id);
        Task<Proyecto?> GetProyectoByClaveAsync(string clave);
        Task<IEnumerable<Proyecto>> GetProyectosPorPmAsync(int usuarioId);
        Task<IEnumerable<Proyecto>> GetAllProyectosActivosAsync();
        Task<long> CreateProyectoAsync(Proyecto proyecto);
        Task UpdateProyectoAsync(Proyecto proyecto);
        Task DeleteProyectoAsync(long id);
    }

    public class ProyectoService : IProyectoService
    {
        private readonly IProyectoRepository _proyectoRepository;

        public ProyectoService(IProyectoRepository proyectoRepository)
        {
            _proyectoRepository = proyectoRepository;
        }

        public async Task<Proyecto?> GetProyectoByIdAsync(long id)
        {
            return await _proyectoRepository.GetProyectoWithDetailsAsync(id);
        }

        public async Task<Proyecto?> GetProyectoByClaveAsync(string clave)
        {
            return await _proyectoRepository.GetByClaveAsync(clave);
        }

        public async Task<IEnumerable<Proyecto>> GetProyectosPorPmAsync(int usuarioId)
        {
            return await _proyectoRepository.GetProyectosPorPmAsync(usuarioId);
        }

        public async Task<IEnumerable<Proyecto>> GetAllProyectosActivosAsync()
        {
            return await _proyectoRepository.FindAsync(p => p.Activo);
        }

        public async Task<long> CreateProyectoAsync(Proyecto proyecto)
        {
            await _proyectoRepository.AddAsync(proyecto);
            await _proyectoRepository.SaveChangesAsync();
            return proyecto.ProyectoId;
        }

        public async Task UpdateProyectoAsync(Proyecto proyecto)
        {
            _proyectoRepository.Update(proyecto);
            await _proyectoRepository.SaveChangesAsync();
        }

        public async Task DeleteProyectoAsync(long id)
        {
            var proyecto = await _proyectoRepository.GetByIdAsync(id);
            if (proyecto != null)
            {
                proyecto.Activo = false;
                _proyectoRepository.Update(proyecto);
                await _proyectoRepository.SaveChangesAsync();
            }
        }
    }
}
