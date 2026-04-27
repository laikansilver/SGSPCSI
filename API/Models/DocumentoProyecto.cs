namespace SGSPCSI.API.Models
{
    public class DocumentoProyecto
    {
        public long DocumentoProyectoId { get; set; }
        public long ProyectoId { get; set; }
        public long? SolicitudId { get; set; }
        public string NombreDocumento { get; set; } = null!;
        public string TipoDocumento { get; set; } = null!;
        public string? VersionDocumento { get; set; }
        public string RutaArchivo { get; set; } = null!;
        public long? TamanioBytes { get; set; }
        public int SubidoPorUsuarioId { get; set; }
        public DateTime FechaSubida { get; set; }
        public bool VisibleParaParticipantes { get; set; }

        // Navigation properties
        public virtual Proyecto Proyecto { get; set; } = null!;
        public virtual Solicitud? Solicitud { get; set; }
        public virtual Usuario SubidoPor { get; set; } = null!;
    }
}
