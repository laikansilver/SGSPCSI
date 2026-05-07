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
    public class TareasDesarrolloController : ControllerBase
    {
        private readonly ITareaDesarrolloService _tareaService;
        private readonly ILogger<TareasDesarrolloController> _logger;

        public TareasDesarrolloController(ITareaDesarrolloService tareaService, ILogger<TareasDesarrolloController> logger)
        {
            _tareaService = tareaService;
            _logger = logger;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TareaDesarrolloDto>> GetTareaById(long id)
        {
            var tarea = await _tareaService.GetTareaByIdAsync(id);
            if (tarea == null)
                return NotFound();

            return Ok(MapToDto(tarea));
        }

        [HttpGet("solicitud/{solicitudId}")]
        public async Task<ActionResult<IEnumerable<TareaDesarrolloDto>>> GetTareasPorSolicitud(long solicitudId)
        {
            var tareas = await _tareaService.GetTareasPorSolicitudAsync(solicitudId);
            return Ok(tareas.Select(MapToDto));
        }

        [HttpGet("asignadas/{usuarioId}")]
        public async Task<ActionResult<IEnumerable<TareaDesarrolloDto>>> GetTareasAsignadas(int usuarioId)
        {
            var tareas = await _tareaService.GetTareasAsignadasAAsync(usuarioId);
            return Ok(tareas.Select(MapToDto));
        }

        [HttpPost]
        public async Task<ActionResult<TareaDesarrolloDto>> CreateTarea(CreateTareaDesarrolloDto createDto)
        {
            var tarea = new TareaDesarrollo
            {
                SolicitudId = createDto.SolicitudId,
                Titulo = createDto.Titulo,
                Descripcion = createDto.Descripcion,
                PrioridadSolicitudId = createDto.PrioridadSolicitudId,
                EstadoTarea = "Pendiente",
                CreadoPorUsuarioId = 1, // Se debe obtener del usuario autenticado
                Activo = true
            };

            var id = await _tareaService.CreateTareaAsync(tarea);
            var created = await _tareaService.GetTareaByIdAsync(id);

            return CreatedAtAction(nameof(GetTareaById), new { id }, MapToDto(created!));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTarea(long id, UpdateTareaDesarrolloDto updateDto)
        {
            var tarea = await _tareaService.GetTareaByIdAsync(id);
            if (tarea == null)
                return NotFound();

            tarea.Titulo = updateDto.Titulo;
            tarea.Descripcion = updateDto.Descripcion;
            tarea.PrioridadSolicitudId = updateDto.PrioridadSolicitudId;
            tarea.EstadoTarea = updateDto.EstadoTarea;
            tarea.FechaInicio = updateDto.FechaInicio;
            tarea.FechaCompromiso = updateDto.FechaCompromiso;
            tarea.FechaCierre = updateDto.FechaCierre;

            await _tareaService.UpdateTareaAsync(tarea);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTarea(long id)
        {
            await _tareaService.DeleteTareaAsync(id);
            return NoContent();
        }

        private TareaDesarrolloDto MapToDto(TareaDesarrollo tarea)
        {
            return new TareaDesarrolloDto
            {
                TareaDesarrolloId = tarea.TareaDesarrolloId,
                SolicitudId = tarea.SolicitudId,
                Titulo = tarea.Titulo,
                Descripcion = tarea.Descripcion,
                PrioridadSolicitudId = tarea.PrioridadSolicitudId,
                EstadoTarea = tarea.EstadoTarea,
                FechaInicio = tarea.FechaInicio,
                FechaCompromiso = tarea.FechaCompromiso,
                FechaCierre = tarea.FechaCierre
            };
        }
    }
}
