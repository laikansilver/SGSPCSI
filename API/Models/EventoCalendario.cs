namespace SGSPCSI.API.Models
{
    public class EventoCalendario
    {
        public long EventoCalendarioId { get; set; }
        public long? ProyectoId { get; set; }
        public long? SolicitudId { get; set; }
        public string Titulo { get; set; } = null!;
        public string? Descripcion { get; set; }
        public string TipoEvento { get; set; } = null!;
        public DateTime FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public int CreadoPorUsuarioId { get; set; }
        public bool EsTodoDia { get; set; }

        // Navigation properties
        public virtual Proyecto? Proyecto { get; set; }
        public virtual Solicitud? Solicitud { get; set; }
        public virtual Usuario CreadoPor { get; set; } = null!;
        public virtual ICollection<EventoParticipante> Participantes { get; set; } = new List<EventoParticipante>();
    }
}
