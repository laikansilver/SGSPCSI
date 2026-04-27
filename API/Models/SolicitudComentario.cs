namespace SGSPCSI.API.Models
{
    public class SolicitudComentario
    {
        public long SolicitudComentarioId { get; set; }
        public long SolicitudId { get; set; }
        public int AutorUsuarioId { get; set; }
        public string Comentario { get; set; } = null!;
        public bool EsInterno { get; set; }
        public DateTime FechaComentario { get; set; }

        // Navigation properties
        public virtual Solicitud Solicitud { get; set; } = null!;
        public virtual Usuario Autor { get; set; } = null!;
    }
}
