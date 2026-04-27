using Microsoft.EntityFrameworkCore;
using SGSPCSI.API.Data;
using SGSPCSI.API.Models;

namespace SGSPCSI.API.Repositories
{
    public interface IAreaRepository : IRepository<Area>
    {
        Task<Area?> GetAreaWithHijasAsync(int areaId);
        Task<IEnumerable<Area>> GetAreasRaizAsync();
        Task<IEnumerable<Area>> GetAreasNivelAsync(byte nivel);
    }

    public class AreaRepository : Repository<Area>, IAreaRepository
    {
        public AreaRepository(IssegDbContext context) : base(context)
        {
        }

        public async Task<Area?> GetAreaWithHijasAsync(int areaId)
        {
            return await _dbSet
                .Include(a => a.AreasHijas)
                .Include(a => a.AreaPadre)
                .FirstOrDefaultAsync(a => a.AreaId == areaId);
        }

        public async Task<IEnumerable<Area>> GetAreasRaizAsync()
        {
            return await _dbSet
                .Where(a => a.AreaPadreId == null && a.Activa)
                .Include(a => a.AreasHijas)
                .ToListAsync();
        }

        public async Task<IEnumerable<Area>> GetAreasNivelAsync(byte nivel)
        {
            return await _dbSet
                .Where(a => a.Nivel == nivel && a.Activa)
                .ToListAsync();
        }
    }
}
