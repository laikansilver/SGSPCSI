namespace SGSPCSI.API.DTOs
{
    public class LoginRequestDto
    {
        public string LoginUsuario { get; set; } = null!;
        public string Password { get; set; } = null!;
    }

    public class AuthResponseDto
    {
        public int UsuarioId { get; set; }
        public string NombreCompleto { get; set; } = null!;
        public string CorreoInstitucional { get; set; } = null!;
        public IReadOnlyCollection<string> Roles { get; set; } = Array.Empty<string>();
        public string Token { get; set; } = null!;
        public DateTime ExpiresAtUtc { get; set; }
    }
}