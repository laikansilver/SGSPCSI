namespace SGSPCSI.API.Models
{
    public class EstadoSolicitud
    {
        public int EstadoSolicitudId { get; set; }
        public string Clave { get; set; } = null!;
        public string Nombre { get; set; } = null!;
        public bool EsTerminal { get; set; }
        public bool Activa { get; set; }

        // Navigation properties
        public virtual ICollection<Solicitud> Solicitudes { get; set; } = new List<Solicitud>();
        public virtual ICollection<SolicitudHistorialEstado> HistorialesEstadoAnterior { get; set; } = new List<SolicitudHistorialEstado>();
        public virtual ICollection<SolicitudHistorialEstado> HistorialesEstadoNuevo { get; set; } = new List<SolicitudHistorialEstado>();
    }
}
