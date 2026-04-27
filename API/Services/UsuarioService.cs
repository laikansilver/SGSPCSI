using SGSPCSI.API.Models;
using SGSPCSI.API.Repositories;

namespace SGSPCSI.API.Services
{
    public interface IUsuarioService
    {
        Task<Usuario?> GetUsuarioByIdAsync(int id);
        Task<Usuario?> GetUsuarioByCorreoAsync(string correo);
        Task<IEnumerable<Usuario>> GetAllUsuariosActivosAsync();
        Task<int> CreateUsuarioAsync(Usuario usuario);
        Task UpdateUsuarioAsync(Usuario usuario);
        Task DeleteUsuarioAsync(int id);
    }

    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioService(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public async Task<Usuario?> GetUsuarioByIdAsync(int id)
        {
            return await _usuarioRepository.GetWithRolesAndAreasAsync(id);
        }

        public async Task<Usuario?> GetUsuarioByCorreoAsync(string correo)
        {
            return await _usuarioRepository.GetByCorreoAsync(correo);
        }

        public async Task<IEnumerable<Usuario>> GetAllUsuariosActivosAsync()
        {
            return await _usuarioRepository.FindAsync(u => u.Activo);
        }

        public async Task<int> CreateUsuarioAsync(Usuario usuario)
        {
            usuario.FechaCreacion = DateTime.UtcNow;
            await _usuarioRepository.AddAsync(usuario);
            await _usuarioRepository.SaveChangesAsync();
            return usuario.UsuarioId;
        }

        public async Task UpdateUsuarioAsync(Usuario usuario)
        {
            _usuarioRepository.Update(usuario);
            await _usuarioRepository.SaveChangesAsync();
        }

        public async Task DeleteUsuarioAsync(int id)
        {
            var usuario = await _usuarioRepository.GetByIdAsync(id);
            if (usuario != null)
            {
                usuario.Activo = false;
                _usuarioRepository.Update(usuario);
                await _usuarioRepository.SaveChangesAsync();
            }
        }
    }
}
