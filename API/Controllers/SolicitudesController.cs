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
    public class SolicitudesController : ControllerBase
    {
        private readonly ISolicitudService _solicitudService;
        private readonly ILogger<SolicitudesController> _logger;

        public SolicitudesController(ISolicitudService solicitudService, ILogger<SolicitudesController> logger)
        {
            _solicitudService = solicitudService;
            _logger = logger;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SolicitudDto>> GetSolicitudById(long id)
        {
            var solicitud = await _solicitudService.GetSolicitudByIdAsync(id);
            if (solicitud == null)
                return NotFound();

            return Ok(MapToDto(solicitud));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SolicitudDto>>> GetAllSolicitudes()
        {
            var solicitudes = await _solicitudService.GetAllSolicitudesActivasAsync();
            return Ok(solicitudes.Select(MapToDto));
        }

        [HttpGet("area/{areaId}")]
        public async Task<ActionResult<IEnumerable<SolicitudDto>>> GetSolicitudesByArea(int areaId)
        {
            var solicitudes = await _solicitudService.GetSolicitudesByAreaAsync(areaId);
            return Ok(solicitudes.Select(MapToDto));
        }

        [HttpGet("desarrollador/{usuarioId}")]
        public async Task<ActionResult<IEnumerable<SolicitudDto>>> GetSolicitudesByDesarrollador(int usuarioId)
        {
            var solicitudes = await _solicitudService.GetSolicitudesByDesarrolladorAsync(usuarioId);
            return Ok(solicitudes.Select(MapToDto));
        }

        [HttpPost]
        public async Task<ActionResult<SolicitudDto>> CreateSolicitud(CreateSolicitudDto createDto)
        {
            var solicitud = new Solicitud
            {
                Folio = $"SOL-{DateTime.UtcNow:yyyyMMdd}-{Guid.NewGuid().ToString().Substring(0, 8).ToUpper()}",
                Titulo = createDto.Titulo,
                Descripcion = createDto.Descripcion,
                AreaSolicitanteId = createDto.AreaSolicitanteId,
                SistemaId = createDto.SistemaId,
                TipoSolicitudId = createDto.TipoSolicitudId,
                PrioridadSolicitudId = createDto.PrioridadSolicitudId,
                EstadoSolicitudId = 1, // Estado inicial: Nuevo
                CreadoPorUsuarioId = 1, // Se debe obtener del usuario autenticado
                Activo = true
            };

            var id = await _solicitudService.CreateSolicitudAsync(solicitud);
            var created = await _solicitudService.GetSolicitudByIdAsync(id);

            return CreatedAtAction(nameof(GetSolicitudById), new { id }, MapToDto(created!));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSolicitud(long id, UpdateSolicitudDto updateDto)
        {
            var solicitud = await _solicitudService.GetSolicitudByIdAsync(id);
            if (solicitud == null)
                return NotFound();

            solicitud.Titulo = updateDto.Titulo;
            solicitud.Descripcion = updateDto.Descripcion;
            solicitud.TipoSolicitudId = updateDto.TipoSolicitudId;
            solicitud.PrioridadSolicitudId = updateDto.PrioridadSolicitudId;
            solicitud.EstadoSolicitudId = updateDto.EstadoSolicitudId;
            solicitud.FechaCompromiso = updateDto.FechaCompromiso;
            solicitud.EsfuerzoHoras = updateDto.EsfuerzoHoras;

            await _solicitudService.UpdateSolicitudAsync(solicitud);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSolicitud(long id)
        {
            await _solicitudService.DeleteSolicitudAsync(id);
            return NoContent();
        }

        private SolicitudDto MapToDto(Solicitud solicitud)
        {
            return new SolicitudDto
            {
                SolicitudId = solicitud.SolicitudId,
                Folio = solicitud.Folio,
                Titulo = solicitud.Titulo,
                Descripcion = solicitud.Descripcion,
                AreaSolicitanteId = solicitud.AreaSolicitanteId,
                SistemaId = solicitud.SistemaId,
                TipoSolicitudId = solicitud.TipoSolicitudId,
                PrioridadSolicitudId = solicitud.PrioridadSolicitudId,
                EstadoSolicitudId = solicitud.EstadoSolicitudId,
                FechaSolicitud = solicitud.FechaSolicitud,
                FechaCompromiso = solicitud.FechaCompromiso,
                FechaResolucion = solicitud.FechaResolucion,
                EsfuerzoHoras = solicitud.EsfuerzoHoras
            };
        }
    }
}
