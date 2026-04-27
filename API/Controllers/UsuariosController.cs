using Microsoft.AspNetCore.Mvc;
using SGSPCSI.API.DTOs;
using SGSPCSI.API.Models;
using SGSPCSI.API.Services;

namespace SGSPCSI.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuariosController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;
        private readonly ILogger<UsuariosController> _logger;

        public UsuariosController(IUsuarioService usuarioService, ILogger<UsuariosController> logger)
        {
            _usuarioService = usuarioService;
            _logger = logger;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UsuarioDto>> GetUsuarioById(int id)
        {
            var usuario = await _usuarioService.GetUsuarioByIdAsync(id);
            if (usuario == null)
                return NotFound();

            return Ok(MapToDto(usuario));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UsuarioDto>>> GetAllUsuarios()
        {
            var usuarios = await _usuarioService.GetAllUsuariosActivosAsync();
            return Ok(usuarios.Select(MapToDto));
        }

        [HttpGet("correo/{correo}")]
        public async Task<ActionResult<UsuarioDto>> GetUsuarioByCorreo(string correo)
        {
            var usuario = await _usuarioService.GetUsuarioByCorreoAsync(correo);
            if (usuario == null)
                return NotFound();

            return Ok(MapToDto(usuario));
        }

        [HttpPost]
        public async Task<ActionResult<UsuarioDto>> CreateUsuario(CreateUsuarioDto createDto)
        {
            var usuario = new Usuario
            {
                NombrePila = createDto.NombrePila,
                ApellidoPaterno = createDto.ApellidoPaterno,
                ApellidoMaterno = createDto.ApellidoMaterno,
                CorreoInstitucional = createDto.CorreoInstitucional,
                Puesto = createDto.Puesto,
                Activo = true
            };

            var id = await _usuarioService.CreateUsuarioAsync(usuario);
            var created = await _usuarioService.GetUsuarioByIdAsync(id);

            return CreatedAtAction(nameof(GetUsuarioById), new { id }, MapToDto(created!));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUsuario(int id, UpdateUsuarioDto updateDto)
        {
            var usuario = await _usuarioService.GetUsuarioByIdAsync(id);
            if (usuario == null)
                return NotFound();

            usuario.NombrePila = updateDto.NombrePila;
            usuario.ApellidoPaterno = updateDto.ApellidoPaterno;
            usuario.ApellidoMaterno = updateDto.ApellidoMaterno;
            usuario.Puesto = updateDto.Puesto;
            usuario.Activo = updateDto.Activo;

            await _usuarioService.UpdateUsuarioAsync(usuario);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsuario(int id)
        {
            await _usuarioService.DeleteUsuarioAsync(id);
            return NoContent();
        }

        private UsuarioDto MapToDto(Usuario usuario)
        {
            return new UsuarioDto
            {
                UsuarioId = usuario.UsuarioId,
                NombrePila = usuario.NombrePila,
                ApellidoPaterno = usuario.ApellidoPaterno,
                ApellidoMaterno = usuario.ApellidoMaterno,
                CorreoInstitucional = usuario.CorreoInstitucional,
                Puesto = usuario.Puesto,
                Activo = usuario.Activo
            };
        }
    }
}
