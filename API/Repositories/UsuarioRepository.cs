using Microsoft.EntityFrameworkCore;
using SGSPCSI.API.Data;
using SGSPCSI.API.Models;

namespace SGSPCSI.API.Repositories
{
    public interface IUsuarioRepository : IRepository<Usuario>
    {
        Task<Usuario?> GetByCorreoAsync(string correo);
        Task<Usuario?> GetWithRolesAndAreasAsync(int usuarioId);
        Task<IEnumerable<Usuario>> GetActualizarPorAreaAsync(int areaId);
    }

    public class UsuarioRepository : Repository<Usuario>, IUsuarioRepository
    {
        public UsuarioRepository(IssegDbContext context) : base(context)
        {
        }

        public async Task<Usuario?> GetByCorreoAsync(string correo)
        {
            return await _dbSet.FirstOrDefaultAsync(u => u.CorreoInstitucional == correo);
        }

        public async Task<Usuario?> GetWithRolesAndAreasAsync(int usuarioId)
        {
            return await _dbSet
                .Include(u => u.UsuariosRoles)
                .ThenInclude(ur => ur.Rol)
                .Include(u => u.UsuariosAreas)
                .ThenInclude(ua => ua.Area)
                .FirstOrDefaultAsync(u => u.UsuarioId == usuarioId);
        }

        public async Task<IEnumerable<Usuario>> GetActualizarPorAreaAsync(int areaId)
        {
            return await _dbSet
                .Where(u => u.UsuariosAreas.Any(ua => ua.AreaId == areaId && ua.Activo) && u.Activo)
                .ToListAsync();
        }
    }
}
