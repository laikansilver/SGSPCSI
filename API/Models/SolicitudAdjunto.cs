namespace SGSPCSI.API.Models
{
    public class SolicitudAdjunto
    {
        public long SolicitudAdjuntoId { get; set; }
        public long SolicitudId { get; set; }
        public string NombreArchivo { get; set; } = null!;
        public string RutaArchivo { get; set; } = null!;
        public string? TipoMime { get; set; }
        public long? TamanioBytes { get; set; }
        public int SubidoPorUsuarioId { get; set; }
        public DateTime FechaSubida { get; set; }

        // Navigation properties
        public virtual Solicitud Solicitud { get; set; } = null!;
        public virtual Usuario SubidoPor { get; set; } = null!;
    }
}
