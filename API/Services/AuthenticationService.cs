using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SGSPCSI.API.DTOs;
using SGSPCSI.API.Infrastructure;
using SGSPCSI.API.Models;
using SGSPCSI.API.Repositories;

namespace SGSPCSI.API.Services
{
    public interface IAuthenticationService
    {
        Task<AuthResponseDto?> AuthenticateAsync(LoginRequestDto request);
    }

    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly JwtOptions _jwtOptions;

        public AuthenticationService(IUsuarioRepository usuarioRepository, IOptions<JwtOptions> jwtOptions)
        {
            _usuarioRepository = usuarioRepository;
            _jwtOptions = jwtOptions.Value;
        }

        public async Task<AuthResponseDto?> AuthenticateAsync(LoginRequestDto request)
        {
            var usuario = await _usuarioRepository.GetByLoginAsync(request.LoginUsuario);
            if (usuario?.Credencial == null || !usuario.Activo)
                return null;

            if (!VerifyPassword(request.Password, usuario.Credencial))
                return null;

            usuario.Credencial.UltimoAcceso = DateTime.UtcNow;
            await UpdateCredentialAsync(usuario.Credencial);

            var roles = usuario.UsuariosRoles
                .Where(ur => ur.Activo)
                .Select(ur => ur.Rol.Nombre)
                .Distinct(StringComparer.OrdinalIgnoreCase)
                .ToArray();

            var expiresAt = DateTime.UtcNow.AddMinutes(_jwtOptions.ExpirationMinutes);
            var token = GenerateToken(usuario, roles, expiresAt);

            return new AuthResponseDto
            {
                UsuarioId = usuario.UsuarioId,
                NombreCompleto = $"{usuario.NombrePila} {usuario.ApellidoPaterno}".Trim(),
                CorreoInstitucional = usuario.CorreoInstitucional,
                Roles = roles,
                Token = token,
                ExpiresAtUtc = expiresAt
            };
        }

        private async Task UpdateCredentialAsync(UsuarioCredencial credential)
        {
            await _usuarioRepository.SaveChangesAsync();
        }

        private bool VerifyPassword(string password, UsuarioCredencial credential)
        {
            if (credential.PasswordSalt.Length == 0 || credential.PasswordHash.Length == 0)
                return false;

            var hash = Rfc2898DeriveBytes.Pbkdf2(
                password,
                credential.PasswordSalt,
                credential.Iteraciones,
                HashAlgorithmName.SHA256,
                credential.PasswordHash.Length);

            return CryptographicOperations.FixedTimeEquals(hash, credential.PasswordHash);
        }

        private string GenerateToken(Usuario usuario, IEnumerable<string> roles, DateTime expiresAt)
        {
            var claims = new List<Claim>
            {
                new(JwtRegisteredClaimNames.Sub, usuario.UsuarioId.ToString()),
                new(ClaimTypes.NameIdentifier, usuario.UsuarioId.ToString()),
                new(ClaimTypes.Name, $"{usuario.NombrePila} {usuario.ApellidoPaterno}".Trim()),
                new(ClaimTypes.Email, usuario.CorreoInstitucional)
            };

            claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.Key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer: _jwtOptions.Issuer,
                audience: _jwtOptions.Audience,
                claims: claims,
                expires: expiresAt,
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}