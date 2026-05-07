using Microsoft.EntityFrameworkCore;
using SGSPCSI.API.Data;
using SGSPCSI.API.Models;

namespace SGSPCSI.API.Repositories
{
    public interface INotificacionRepository : IRepository<Notificacion>
    {
        Task<IEnumerable<Notificacion>> GetNotificacionesNoLeidasAsync(int usuarioId);
        Task<IEnumerable<Notificacion>> GetNotificacionesUsuarioAsync(int usuarioId, int? limit = null);
    }

    public class NotificacionRepository : Repository<Notificacion>, INotificacionRepository
    {
        public NotificacionRepository(IssegDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Notificacion>> GetNotificacionesNoLeidasAsync(int usuarioId)
        {
            return await _dbSet
                .Where(n => n.UsuarioId == usuarioId && !n.Leida)
                .OrderByDescending(n => n.FechaCreacion)
                .ToListAsync();
        }

        public async Task<IEnumerable<Notificacion>> GetNotificacionesUsuarioAsync(int usuarioId, int? limit = null)
        {
            IQueryable<Notificacion> query = _dbSet
                .Where(n => n.UsuarioId == usuarioId)
                .OrderByDescending(n => n.FechaCreacion);

            if (limit.HasValue)
                query = query.Take(limit.Value);

            return await query.ToListAsync();
        }
    }
}
