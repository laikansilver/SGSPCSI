namespace SGSPCSI.API.Models
{
    public class TareaDesarrollo
    {
        public long TareaDesarrolloId { get; set; }
        public long SolicitudId { get; set; }
        public string Titulo { get; set; } = null!;
        public string? Descripcion { get; set; }
        public int PrioridadSolicitudId { get; set; }
        public string EstadoTarea { get; set; } = null!;
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaCompromiso { get; set; }
        public DateTime? FechaCierre { get; set; }
        public int CreadoPorUsuarioId { get; set; }
        public bool Activo { get; set; }

        // Navigation properties
        public virtual Solicitud Solicitud { get; set; } = null!;
        public virtual PrioridadSolicitud PrioridadSolicitud { get; set; } = null!;
        public virtual Usuario CreadoPor { get; set; } = null!;
        public virtual ICollection<TareaDesarrolloAsignacion> Asignaciones { get; set; } = new List<TareaDesarrolloAsignacion>();
        public virtual ICollection<ActividadReciente> Actividades { get; set; } = new List<ActividadReciente>();
    }
}
