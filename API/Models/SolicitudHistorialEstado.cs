namespace SGSPCSI.API.Models
{
    public class SolicitudHistorialEstado
    {
        public long SolicitudHistorialEstadoId { get; set; }
        public long SolicitudId { get; set; }
        public int? EstadoAnteriorId { get; set; }
        public int EstadoNuevoId { get; set; }
        public string? Comentario { get; set; }
        public int CambiadoPorUsuarioId { get; set; }
        public DateTime FechaCambio { get; set; }

        // Navigation properties
        public virtual Solicitud Solicitud { get; set; } = null!;
        public virtual EstadoSolicitud? EstadoAnterior { get; set; }
        public virtual EstadoSolicitud EstadoNuevo { get; set; } = null!;
        public virtual Usuario CambiadoPor { get; set; } = null!;
    }
}
