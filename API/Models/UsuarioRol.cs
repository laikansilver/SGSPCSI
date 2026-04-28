namespace SGSPCSI.API.Models
{
    public class UsuarioRol
    {
        public int UsuarioRolId { get; set; }
        public int UsuarioId { get; set; }
        public int RolId { get; set; }
        public bool Activo { get; set; }
        public DateTime FechaAsignacion { get; set; }
        public DateTime? FechaFin { get; set; }

        // Navigation properties
        public virtual Usuario Usuario { get; set; } = null!;
        public virtual Rol Rol { get; set; } = null!;
    }
}
