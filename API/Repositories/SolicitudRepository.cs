using Microsoft.EntityFrameworkCore;
using SGSPCSI.API.Data;
using SGSPCSI.API.Models;

namespace SGSPCSI.API.Repositories
{
    public interface ISolicitudRepository : IRepository<Solicitud>
    {
        Task<Solicitud?> GetSolicitudWithDetailsAsync(long solicitudId);
        Task<IEnumerable<Solicitud>> GetSolicitudesByAreaAsync(int areaId);
        Task<IEnumerable<Solicitud>> GetSolicitudesByEstadoAsync(int estadoId);
        Task<IEnumerable<Solicitud>> GetSolicitudesByDesarrolladorAsync(int usuarioId);
        Task<Solicitud?> GetByFolioAsync(string folio);
    }

    public class SolicitudRepository : Repository<Solicitud>, ISolicitudRepository
    {
        public SolicitudRepository(IssegDbContext context) : base(context)
        {
        }

        public async Task<Solicitud?> GetSolicitudWithDetailsAsync(long solicitudId)
        {
            return await _dbSet
                .Include(s => s.AreaSolicitante)
                .Include(s => s.Sistema)
                .Include(s => s.TipoSolicitud)
                .Include(s => s.PrioridadSolicitud)
                .Include(s => s.EstadoSolicitud)
                .Include(s => s.CreadoPor)
                .Include(s => s.SolicitudesDesarrollador)
                .Include(s => s.Comentarios)
                .Include(s => s.Adjuntos)
                .FirstOrDefaultAsync(s => s.SolicitudId == solicitudId);
        }

        public async Task<IEnumerable<Solicitud>> GetSolicitudesByAreaAsync(int areaId)
        {
            return await _dbSet
                .Where(s => s.AreaSolicitanteId == areaId && s.Activo)
                .Include(s => s.EstadoSolicitud)
                .Include(s => s.PrioridadSolicitud)
                .OrderByDescending(s => s.FechaSolicitud)
                .ToListAsync();
        }

        public async Task<IEnumerable<Solicitud>> GetSolicitudesByEstadoAsync(int estadoId)
        {
            return await _dbSet
                .Where(s => s.EstadoSolicitudId == estadoId && s.Activo)
                .Include(s => s.AreaSolicitante)
                .Include(s => s.PrioridadSolicitud)
                .OrderByDescending(s => s.FechaSolicitud)
                .ToListAsync();
        }

        public async Task<IEnumerable<Solicitud>> GetSolicitudesByDesarrolladorAsync(int usuarioId)
        {
            return await _dbSet
                .Where(s => s.SolicitudesDesarrollador.Any(sd => sd.UsuarioId == usuarioId && sd.Activo) && s.Activo)
                .Include(s => s.EstadoSolicitud)
                .Include(s => s.PrioridadSolicitud)
                .OrderByDescending(s => s.FechaSolicitud)
                .ToListAsync();
        }

        public async Task<Solicitud?> GetByFolioAsync(string folio)
        {
            return await _dbSet.FirstOrDefaultAsync(s => s.Folio == folio);
        }
    }
}
