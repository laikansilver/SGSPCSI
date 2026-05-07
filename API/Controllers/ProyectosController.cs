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
    public class ProyectosController : ControllerBase
    {
        private readonly IProyectoService _proyectoService;
        private readonly ILogger<ProyectosController> _logger;

        public ProyectosController(IProyectoService proyectoService, ILogger<ProyectosController> logger)
        {
            _proyectoService = proyectoService;
            _logger = logger;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProyectoDto>> GetProyectoById(long id)
        {
            var proyecto = await _proyectoService.GetProyectoByIdAsync(id);
            if (proyecto == null)
                return NotFound();

            return Ok(MapToDto(proyecto));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProyectoDto>>> GetAllProyectos()
        {
            var proyectos = await _proyectoService.GetAllProyectosActivosAsync();
            return Ok(proyectos.Select(MapToDto));
        }

        [HttpGet("clave/{clave}")]
        public async Task<ActionResult<ProyectoDto>> GetProyectoByClave(string clave)
        {
            var proyecto = await _proyectoService.GetProyectoByClaveAsync(clave);
            if (proyecto == null)
                return NotFound();

            return Ok(MapToDto(proyecto));
        }

        [HttpGet("pm/{usuarioId}")]
        public async Task<ActionResult<IEnumerable<ProyectoDto>>> GetProyectosPorPm(int usuarioId)
        {
            var proyectos = await _proyectoService.GetProyectosPorPmAsync(usuarioId);
            return Ok(proyectos.Select(MapToDto));
        }

        [HttpPost]
        public async Task<ActionResult<ProyectoDto>> CreateProyecto(CreateProyectoDto createDto)
        {
            var proyecto = new Proyecto
            {
                Clave = createDto.Clave,
                Nombre = createDto.Nombre,
                Descripcion = createDto.Descripcion,
                EstadoProyecto = "Planeación",
                FechaInicio = createDto.FechaInicio,
                FechaFinPlaneada = createDto.FechaFinPlaneada,
                PmUsuarioId = createDto.PmUsuarioId,
                Activo = true
            };

            var id = await _proyectoService.CreateProyectoAsync(proyecto);
            var created = await _proyectoService.GetProyectoByIdAsync(id);

            return CreatedAtAction(nameof(GetProyectoById), new { id }, MapToDto(created!));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProyecto(long id, UpdateProyectoDto updateDto)
        {
            var proyecto = await _proyectoService.GetProyectoByIdAsync(id);
            if (proyecto == null)
                return NotFound();

            proyecto.Nombre = updateDto.Nombre;
            proyecto.Descripcion = updateDto.Descripcion;
            proyecto.EstadoProyecto = updateDto.EstadoProyecto;
            proyecto.FechaFinPlaneada = updateDto.FechaFinPlaneada;
            proyecto.FechaFinReal = updateDto.FechaFinReal;

            await _proyectoService.UpdateProyectoAsync(proyecto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProyecto(long id)
        {
            await _proyectoService.DeleteProyectoAsync(id);
            return NoContent();
        }

        private ProyectoDto MapToDto(Proyecto proyecto)
        {
            return new ProyectoDto
            {
                ProyectoId = proyecto.ProyectoId,
                Clave = proyecto.Clave,
                Nombre = proyecto.Nombre,
                Descripcion = proyecto.Descripcion,
                EstadoProyecto = proyecto.EstadoProyecto,
                FechaInicio = proyecto.FechaInicio,
                FechaFinPlaneada = proyecto.FechaFinPlaneada,
                FechaFinReal = proyecto.FechaFinReal,
                PmUsuarioId = proyecto.PmUsuarioId
            };
        }
    }
}
