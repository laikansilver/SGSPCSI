namespace SGSPCSI.API.Models
{
    public class EventoParticipante
    {
        public long EventoParticipanteId { get; set; }
        public long EventoCalendarioId { get; set; }
        public int UsuarioId { get; set; }
        public string Asistencia { get; set; } = null!;

        // Navigation properties
        public virtual EventoCalendario EventoCalendario { get; set; } = null!;
        public virtual Usuario Usuario { get; set; } = null!;
    }
}
