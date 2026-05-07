using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SGSPCSI.API.DTOs;
using SGSPCSI.API.Models;
using SGSPCSI.API.Services;

namespace SGSPCSI.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class AreasController : ControllerBase
    {
        private readonly IAreaService _areaService;
        private readonly ILogger<AreasController> _logger;

        public AreasController(IAreaService areaService, ILogger<AreasController> logger)
        {
            _areaService = areaService;
            _logger = logger;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AreaDto>> GetAreaById(int id)
        {
            var area = await _areaService.GetAreaByIdAsync(id);
            if (area == null)
                return NotFound();

            return Ok(MapToDto(area));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AreaDto>>> GetAllAreas()
        {
            var areas = await _areaService.GetAllAreasActivasAsync();
            return Ok(areas.Select(MapToDto));
        }

        [HttpGet("raiz")]
        public async Task<ActionResult<IEnumerable<AreaDto>>> GetAreasRaiz()
        {
            var areas = await _areaService.GetAreasRaizAsync();
            return Ok(areas.Select(MapToDto));
        }

        [HttpPost]
        public async Task<ActionResult<AreaDto>> CreateArea(CreateAreaDto createDto)
        {
            var area = new Area
            {
                AreaPadreId = createDto.AreaPadreId,
                Clave = createDto.Clave,
                Nombre = createDto.Nombre,
                TipoArea = createDto.TipoArea,
                Nivel = createDto.Nivel,
                Activa = true
            };

            var id = await _areaService.CreateAreaAsync(area);
            var created = await _areaService.GetAreaByIdAsync(id);

            return CreatedAtAction(nameof(GetAreaById), new { id }, MapToDto(created!));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateArea(int id, UpdateAreaDto updateDto)
        {
            var area = await _areaService.GetAreaByIdAsync(id);
            if (area == null)
                return NotFound();

            area.Clave = updateDto.Clave;
            area.Nombre = updateDto.Nombre;
            area.TipoArea = updateDto.TipoArea;
            area.Nivel = updateDto.Nivel;
            area.Activa = updateDto.Activa;

            await _areaService.UpdateAreaAsync(area);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteArea(int id)
        {
            await _areaService.DeleteAreaAsync(id);
            return NoContent();
        }

        private AreaDto MapToDto(Area area)
        {
            return new AreaDto
            {
                AreaId = area.AreaId,
                AreaPadreId = area.AreaPadreId,
                Clave = area.Clave,
                Nombre = area.Nombre,
                TipoArea = area.TipoArea,
                Nivel = area.Nivel,
                Activa = area.Activa
            };
        }
    }
}
