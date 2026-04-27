namespace SGSPCSI.API.Models
{
    public class TareaDesarrolloAsignacion
    {
        public long TareaDesarrolloAsignacionId { get; set; }
        public long TareaDesarrolloId { get; set; }
        public int UsuarioId { get; set; }
        public string? RolAsignacion { get; set; }
        public bool Activo { get; set; }
        public DateTime FechaAsignacion { get; set; }
        public DateTime? FechaFin { get; set; }

        // Navigation properties
        public virtual TareaDesarrollo TareaDesarrollo { get; set; } = null!;
        public virtual Usuario Usuario { get; set; } = null!;
    }
}
