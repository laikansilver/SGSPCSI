namespace SGSPCSI.API.Models
{
    public class SolicitudDesarrollador
    {
        public long SolicitudDesarrolladorId { get; set; }
        public long SolicitudId { get; set; }
        public int UsuarioId { get; set; }
        public string TipoParticipacion { get; set; } = null!;
        public bool Activo { get; set; }
        public DateTime FechaAsignacion { get; set; }
        public DateTime? FechaFin { get; set; }

        // Navigation properties
        public virtual Solicitud Solicitud { get; set; } = null!;
        public virtual Usuario Usuario { get; set; } = null!;
    }
}
