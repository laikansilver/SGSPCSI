namespace SGSPCSI.API.Models
{
    public class Proyecto
    {
        public long ProyectoId { get; set; }
        public string Clave { get; set; } = null!;
        public string Nombre { get; set; } = null!;
        public string? Descripcion { get; set; }
        public string EstadoProyecto { get; set; } = null!;
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFinPlaneada { get; set; }
        public DateTime? FechaFinReal { get; set; }
        public int PmUsuarioId { get; set; }
        public bool Activo { get; set; }

        // Navigation properties
        public virtual Usuario Pm { get; set; } = null!;
        public virtual ICollection<ProyectoSolicitud> ProyectosSolicitudes { get; set; } = new List<ProyectoSolicitud>();
        public virtual ICollection<ProyectoMiembro> Miembros { get; set; } = new List<ProyectoMiembro>();
        public virtual ICollection<DocumentoProyecto> Documentos { get; set; } = new List<DocumentoProyecto>();
        public virtual ICollection<EventoCalendario> Eventos { get; set; } = new List<EventoCalendario>();
    }
}
