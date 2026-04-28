namespace SGSPCSI.API.Models
{
    public class ProyectoSolicitud
    {
        public long ProyectoSolicitudId { get; set; }
        public long ProyectoId { get; set; }
        public long SolicitudId { get; set; }
        public DateTime FechaVinculacion { get; set; }

        // Navigation properties
        public virtual Proyecto Proyecto { get; set; } = null!;
        public virtual Solicitud Solicitud { get; set; } = null!;
    }
}
