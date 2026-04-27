namespace SGSPCSI.API.Models
{
    public class ProyectoMiembro
    {
        public long ProyectoMiembroId { get; set; }
        public long ProyectoId { get; set; }
        public int UsuarioId { get; set; }
        public string RolEnProyecto { get; set; } = null!;
        public decimal? CargaEstimadaPct { get; set; }
        public bool Activo { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }

        // Navigation properties
        public virtual Proyecto Proyecto { get; set; } = null!;
        public virtual Usuario Usuario { get; set; } = null!;
    }
}
