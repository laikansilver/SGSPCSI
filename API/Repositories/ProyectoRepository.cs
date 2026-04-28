using Microsoft.EntityFrameworkCore;
using SGSPCSI.API.Data;
using SGSPCSI.API.Models;

namespace SGSPCSI.API.Repositories
{
    public interface IProyectoRepository : IRepository<Proyecto>
    {
        Task<Proyecto?> GetProyectoWithDetailsAsync(long proyectoId);
        Task<IEnumerable<Proyecto>> GetProyectosPorPmAsync(int usuarioId);
        Task<Proyecto?> GetByClaveAsync(string clave);
    }

    public class ProyectoRepository : Repository<Proyecto>, IProyectoRepository
    {
        public ProyectoRepository(IssegDbContext context) : base(context)
        {
        }

        public async Task<Proyecto?> GetProyectoWithDetailsAsync(long proyectoId)
        {
            return await _dbSet
                .Include(p => p.Pm)
                .Include(p => p.Miembros)
                .ThenInclude(pm => pm.Usuario)
                .Include(p => p.ProyectosSolicitudes)
                .ThenInclude(ps => ps.Solicitud)
                .Include(p => p.Documentos)
                .FirstOrDefaultAsync(p => p.ProyectoId == proyectoId);
        }

        public async Task<IEnumerable<Proyecto>> GetProyectosPorPmAsync(int usuarioId)
        {
            return await _dbSet
                .Where(p => p.PmUsuarioId == usuarioId && p.Activo)
                .Include(p => p.Miembros)
                .OrderByDescending(p => p.FechaInicio)
                .ToListAsync();
        }

        public async Task<Proyecto?> GetByClaveAsync(string clave)
        {
            return await _dbSet.FirstOrDefaultAsync(p => p.Clave == clave);
        }
    }
}
