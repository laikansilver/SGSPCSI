using SGSPCSI.API.Models;
using SGSPCSI.API.Repositories;

namespace SGSPCSI.API.Services
{
    public interface IAreaService
    {
        Task<Area?> GetAreaByIdAsync(int id);
        Task<IEnumerable<Area>> GetAllAreasActivasAsync();
        Task<IEnumerable<Area>> GetAreasRaizAsync();
        Task<int> CreateAreaAsync(Area area);
        Task UpdateAreaAsync(Area area);
        Task DeleteAreaAsync(int id);
    }

    public class AreaService : IAreaService
    {
        private readonly IAreaRepository _areaRepository;

        public AreaService(IAreaRepository areaRepository)
        {
            _areaRepository = areaRepository;
        }

        public async Task<Area?> GetAreaByIdAsync(int id)
        {
            return await _areaRepository.GetAreaWithHijasAsync(id);
        }

        public async Task<IEnumerable<Area>> GetAllAreasActivasAsync()
        {
            return await _areaRepository.FindAsync(a => a.Activa);
        }

        public async Task<IEnumerable<Area>> GetAreasRaizAsync()
        {
            return await _areaRepository.GetAreasRaizAsync();
        }

        public async Task<int> CreateAreaAsync(Area area)
        {
            area.FechaCreacion = DateTime.UtcNow;
            await _areaRepository.AddAsync(area);
            await _areaRepository.SaveChangesAsync();
            return area.AreaId;
        }

        public async Task UpdateAreaAsync(Area area)
        {
            _areaRepository.Update(area);
            await _areaRepository.SaveChangesAsync();
        }

        public async Task DeleteAreaAsync(int id)
        {
            var area = await _areaRepository.GetByIdAsync(id);
            if (area != null)
            {
                area.Activa = false;
                _areaRepository.Update(area);
                await _areaRepository.SaveChangesAsync();
            }
        }
    }
}
