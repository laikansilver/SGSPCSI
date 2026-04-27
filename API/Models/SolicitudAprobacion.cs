namespace SGSPCSI.API.Models
{
    public class SolicitudAprobacion
    {
        public long SolicitudAprobacionId { get; set; }
        public long SolicitudId { get; set; }
        public int AprobadoPorUsuarioId { get; set; }
        public string EstatusAprobacion { get; set; } = null!;
        public string? Motivo { get; set; }
        public DateTime FechaAprobacion { get; set; }

        // Navigation properties
        public virtual Solicitud Solicitud { get; set; } = null!;
        public virtual Usuario AprobadoPor { get; set; } = null!;
    }
}
