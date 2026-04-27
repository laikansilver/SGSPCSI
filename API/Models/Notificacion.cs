namespace SGSPCSI.API.Models
{
    public class Notificacion
    {
        public long NotificacionId { get; set; }
        public int UsuarioId { get; set; }
        public long? SolicitudId { get; set; }
        public string Titulo { get; set; } = null!;
        public string Mensaje { get; set; } = null!;
        public bool Leida { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime? FechaLectura { get; set; }

        // Navigation properties
        public virtual Usuario Usuario { get; set; } = null!;
        public virtual Solicitud? Solicitud { get; set; }
    }
}
