namespace SGSPCSI.API.DTOs
{
    public class NotificacionDto
    {
        public long NotificacionId { get; set; }
        public int UsuarioId { get; set; }
        public long? SolicitudId { get; set; }
        public string Titulo { get; set; } = null!;
        public string Mensaje { get; set; } = null!;
        public bool Leida { get; set; }
        public DateTime FechaCreacion { get; set; }
    }

    public class CreateNotificacionDto
    {
        public int UsuarioId { get; set; }
        public long? SolicitudId { get; set; }
        public string Titulo { get; set; } = null!;
        public string Mensaje { get; set; } = null!;
    }

    public class UpdateNotificacionDto
    {
        public bool Leida { get; set; }
    }
}
