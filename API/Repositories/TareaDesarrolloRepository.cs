using Microsoft.EntityFrameworkCore;
using SGSPCSI.API.Data;
using SGSPCSI.API.Models;

namespace SGSPCSI.API.Repositories
{
    public interface ITareaDesarrolloRepository : IRepository<TareaDesarrollo>
    {
        Task<IEnumerable<TareaDesarrollo>> GetTareasPorSolicitudAsync(long solicitudId);
        Task<IEnumerable<TareaDesarrollo>> GetTareasAsignadasAAsync(int usuarioId);
        Task<IEnumerable<TareaDesarrollo>> GetTareasPorEstadoAsync(string estado);
    }

    public class TareaDesarrolloRepository : Repository<TareaDesarrollo>, ITareaDesarrolloRepository
    {
        public TareaDesarrolloRepository(IssegDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<TareaDesarrollo>> GetTareasPorSolicitudAsync(long solicitudId)
        {
            return await _dbSet
                .Where(t => t.SolicitudId == solicitudId && t.Activo)
                .Include(t => t.Asignaciones)
                .ThenInclude(ta => ta.Usuario)
                .OrderByDescending(t => t.PrioridadSolicitud.Orden)
                .ToListAsync();
        }

        public async Task<IEnumerable<TareaDesarrollo>> GetTareasAsignadasAAsync(int usuarioId)
        {
            return await _dbSet
                .Where(t => t.Asignaciones.Any(ta => ta.UsuarioId == usuarioId && ta.Activo) && t.Activo)
                .Include(t => t.Solicitud)
                .Include(t => t.PrioridadSolicitud)
                .ToListAsync();
        }

        public async Task<IEnumerable<TareaDesarrollo>> GetTareasPorEstadoAsync(string estado)
        {
            return await _dbSet
                .Where(t => t.EstadoTarea == estado && t.Activo)
                .Include(t => t.Solicitud)
                .Include(t => t.Asignaciones)
                .ThenInclude(ta => ta.Usuario)
                .ToListAsync();
        }
    }
}
