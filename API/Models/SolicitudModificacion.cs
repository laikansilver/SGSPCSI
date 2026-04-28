namespace SGSPCSI.API.Models
{
    public class SolicitudModificacion
    {
        public long SolicitudModificacionId { get; set; }
        public long SolicitudId { get; set; }
        public int TipoModificacionId { get; set; }
        public string? SistemaVersionActual { get; set; }
        public string? ModuloAfectado { get; set; }
        public string? ImpactoTecnico { get; set; }
        public string? Justificacion { get; set; }
        public DateTime FechaRegistro { get; set; }

        // Navigation properties
        public virtual Solicitud Solicitud { get; set; } = null!;
        public virtual TipoModificacion TipoModificacion { get; set; } = null!;
    }
}
