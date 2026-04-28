using SGSPCSI.API.Models;
using SGSPCSI.API.Repositories;

namespace SGSPCSI.API.Services
{
    public interface INotificacionService
    {
        Task<Notificacion?> GetNotificacionByIdAsync(long id);
        Task<IEnumerable<Notificacion>> GetNotificacionesNoLeidasAsync(int usuarioId);
        Task<IEnumerable<Notificacion>> GetNotificacionesUsuarioAsync(int usuarioId, int? limit = null);
        Task<long> CreateNotificacionAsync(Notificacion notificacion);
        Task MarkAsReadAsync(long notificacionId);
        Task DeleteNotificacionAsync(long id);
    }

    public class NotificacionService : INotificacionService
    {
        private readonly INotificacionRepository _notificacionRepository;

        public NotificacionService(INotificacionRepository notificacionRepository)
        {
            _notificacionRepository = notificacionRepository;
        }

        public async Task<Notificacion?> GetNotificacionByIdAsync(long id)
        {
            return await _notificacionRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Notificacion>> GetNotificacionesNoLeidasAsync(int usuarioId)
        {
            return await _notificacionRepository.GetNotificacionesNoLeidasAsync(usuarioId);
        }

        public async Task<IEnumerable<Notificacion>> GetNotificacionesUsuarioAsync(int usuarioId, int? limit = null)
        {
            return await _notificacionRepository.GetNotificacionesUsuarioAsync(usuarioId, limit);
        }

        public async Task<long> CreateNotificacionAsync(Notificacion notificacion)
        {
            notificacion.FechaCreacion = DateTime.UtcNow;
            notificacion.Leida = false;
            await _notificacionRepository.AddAsync(notificacion);
            await _notificacionRepository.SaveChangesAsync();
            return notificacion.NotificacionId;
        }

        public async Task MarkAsReadAsync(long notificacionId)
        {
            var notificacion = await _notificacionRepository.GetByIdAsync(notificacionId);
            if (notificacion != null)
            {
                notificacion.Leida = true;
                notificacion.FechaLectura = DateTime.UtcNow;
                _notificacionRepository.Update(notificacion);
                await _notificacionRepository.SaveChangesAsync();
            }
        }

        public async Task DeleteNotificacionAsync(long id)
        {
            var notificacion = await _notificacionRepository.GetByIdAsync(id);
            if (notificacion != null)
            {
                _notificacionRepository.Remove(notificacion);
                await _notificacionRepository.SaveChangesAsync();
            }
        }
    }
}
