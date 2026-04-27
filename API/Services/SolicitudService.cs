using SGSPCSI.API.Models;
using SGSPCSI.API.Repositories;

namespace SGSPCSI.API.Services
{
    public interface ISolicitudService
    {
        Task<Solicitud?> GetSolicitudByIdAsync(long id);
        Task<IEnumerable<Solicitud>> GetAllSolicitudesActivasAsync();
        Task<IEnumerable<Solicitud>> GetSolicitudesByAreaAsync(int areaId);
        Task<IEnumerable<Solicitud>> GetSolicitudesByDesarrolladorAsync(int usuarioId);
        Task<long> CreateSolicitudAsync(Solicitud solicitud);
        Task UpdateSolicitudAsync(Solicitud solicitud);
        Task DeleteSolicitudAsync(long id);
    }

    public class SolicitudService : ISolicitudService
    {
        private readonly ISolicitudRepository _solicitudRepository;

        public SolicitudService(ISolicitudRepository solicitudRepository)
        {
            _solicitudRepository = solicitudRepository;
        }

        public async Task<Solicitud?> GetSolicitudByIdAsync(long id)
        {
            return await _solicitudRepository.GetSolicitudWithDetailsAsync(id);
        }

        public async Task<IEnumerable<Solicitud>> GetAllSolicitudesActivasAsync()
        {
            return await _solicitudRepository.FindAsync(s => s.Activo);
        }

        public async Task<IEnumerable<Solicitud>> GetSolicitudesByAreaAsync(int areaId)
        {
            return await _solicitudRepository.GetSolicitudesByAreaAsync(areaId);
        }

        public async Task<IEnumerable<Solicitud>> GetSolicitudesByDesarrolladorAsync(int usuarioId)
        {
            return await _solicitudRepository.GetSolicitudesByDesarrolladorAsync(usuarioId);
        }

        public async Task<long> CreateSolicitudAsync(Solicitud solicitud)
        {
            solicitud.FechaSolicitud = DateTime.UtcNow;
            await _solicitudRepository.AddAsync(solicitud);
            await _solicitudRepository.SaveChangesAsync();
            return solicitud.SolicitudId;
        }

        public async Task UpdateSolicitudAsync(Solicitud solicitud)
        {
            _solicitudRepository.Update(solicitud);
            await _solicitudRepository.SaveChangesAsync();
        }

        public async Task DeleteSolicitudAsync(long id)
        {
            var solicitud = await _solicitudRepository.GetByIdAsync(id);
            if (solicitud != null)
            {
                solicitud.Activo = false;
                _solicitudRepository.Update(solicitud);
                await _solicitudRepository.SaveChangesAsync();
            }
        }
    }
}
