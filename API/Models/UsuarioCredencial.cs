namespace SGSPCSI.API.Models
{
    public class UsuarioCredencial
    {
        public int UsuarioId { get; set; }
        public string LoginUsuario { get; set; } = null!;
        public byte[] PasswordHash { get; set; } = null!;
        public byte[] PasswordSalt { get; set; } = null!;
        public string AlgoritmoHash { get; set; } = null!;
        public int Iteraciones { get; set; }
        public DateTime? UltimoAcceso { get; set; }
        public short IntentosFallidos { get; set; }
        public DateTime? BloqueadoHasta { get; set; }
        public bool RequiereCambioPassword { get; set; }
        public DateTime FechaActualizacion { get; set; }

        // Navigation properties
        public virtual Usuario Usuario { get; set; } = null!;
    }
}
