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
    public class NotificacionesController : ControllerBase
    {
        private readonly INotificacionService _notificacionService;
        private readonly ILogger<NotificacionesController> _logger;

        public NotificacionesController(INotificacionService notificacionService, ILogger<NotificacionesController> logger)
        {
            _notificacionService = notificacionService;
            _logger = logger;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<NotificacionDto>> GetNotificacionById(long id)
        {
            var notificacion = await _notificacionService.GetNotificacionByIdAsync(id);
            if (notificacion == null)
                return NotFound();

            return Ok(MapToDto(notificacion));
        }

        [HttpGet("usuario/{usuarioId}/no-leidas")]
        public async Task<ActionResult<IEnumerable<NotificacionDto>>> GetNotificacionesNoLeidas(int usuarioId)
        {
            var notificaciones = await _notificacionService.GetNotificacionesNoLeidasAsync(usuarioId);
            return Ok(notificaciones.Select(MapToDto));
        }

        [HttpGet("usuario/{usuarioId}")]
        public async Task<ActionResult<IEnumerable<NotificacionDto>>> GetNotificacionesUsuario(int usuarioId, [FromQuery] int? limit = null)
        {
            var notificaciones = await _notificacionService.GetNotificacionesUsuarioAsync(usuarioId, limit ?? 50);
            return Ok(notificaciones.Select(MapToDto));
        }

        [HttpPost]
        public async Task<ActionResult<NotificacionDto>> CreateNotificacion(CreateNotificacionDto createDto)
        {
            var notificacion = new Notificacion
            {
                UsuarioId = createDto.UsuarioId,
                SolicitudId = createDto.SolicitudId,
                Titulo = createDto.Titulo,
                Mensaje = createDto.Mensaje
            };

            var id = await _notificacionService.CreateNotificacionAsync(notificacion);
            var created = await _notificacionService.GetNotificacionByIdAsync(id);

            return CreatedAtAction(nameof(GetNotificacionById), new { id }, MapToDto(created!));
        }

        [HttpPatch("{id}/marcar-leida")]
        public async Task<IActionResult> MarkAsRead(long id)
        {
            await _notificacionService.MarkAsReadAsync(id);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNotificacion(long id)
        {
            await _notificacionService.DeleteNotificacionAsync(id);
            return NoContent();
        }

        private NotificacionDto MapToDto(Notificacion notificacion)
        {
            return new NotificacionDto
            {
                NotificacionId = notificacion.NotificacionId,
                UsuarioId = notificacion.UsuarioId,
                SolicitudId = notificacion.SolicitudId,
                Titulo = notificacion.Titulo,
                Mensaje = notificacion.Mensaje,
                Leida = notificacion.Leida,
                FechaCreacion = notificacion.FechaCreacion
            };
        }
    }
}
