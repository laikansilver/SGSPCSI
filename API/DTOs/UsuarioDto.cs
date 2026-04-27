namespace SGSPCSI.API.DTOs
{
    public class UsuarioDto
    {
        public int UsuarioId { get; set; }
        public string NombrePila { get; set; } = null!;
        public string ApellidoPaterno { get; set; } = null!;
        public string? ApellidoMaterno { get; set; }
        public string CorreoInstitucional { get; set; } = null!;
        public string? Puesto { get; set; }
        public bool Activo { get; set; }
    }

    public class CreateUsuarioDto
    {
        public string NombrePila { get; set; } = null!;
        public string ApellidoPaterno { get; set; } = null!;
        public string? ApellidoMaterno { get; set; }
        public string CorreoInstitucional { get; set; } = null!;
        public string? Puesto { get; set; }
    }

    public class UpdateUsuarioDto
    {
        public string NombrePila { get; set; } = null!;
        public string ApellidoPaterno { get; set; } = null!;
        public string? ApellidoMaterno { get; set; }
        public string? Puesto { get; set; }
        public bool Activo { get; set; }
    }
}
