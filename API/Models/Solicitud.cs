namespace SGSPCSI.API.Models
{
    public class Solicitud
    {
        public long SolicitudId { get; set; }
        public string Folio { get; set; } = null!;
        public string Titulo { get; set; } = null!;
        public string Descripcion { get; set; } = null!;
        public int AreaSolicitanteId { get; set; }
        public int? SistemaId { get; set; }
        public int TipoSolicitudId { get; set; }
        public int PrioridadSolicitudId { get; set; }
        public int EstadoSolicitudId { get; set; }
        public int CreadoPorUsuarioId { get; set; }
        public DateTime FechaSolicitud { get; set; }
        public DateTime? FechaCompromiso { get; set; }
        public DateTime? FechaResolucion { get; set; }
        public decimal? EsfuerzoHoras { get; set; }
        public bool Activo { get; set; }

        // Navigation properties
        public virtual Area AreaSolicitante { get; set; } = null!;
        public virtual Sistema? Sistema { get; set; }
        public virtual TipoSolicitud TipoSolicitud { get; set; } = null!;
        public virtual PrioridadSolicitud PrioridadSolicitud { get; set; } = null!;
        public virtual EstadoSolicitud EstadoSolicitud { get; set; } = null!;
        public virtual Usuario CreadoPor { get; set; } = null!;
        public virtual ICollection<SolicitudDesarrollador> SolicitudesDesarrollador { get; set; } = new List<SolicitudDesarrollador>();
        public virtual ICollection<SolicitudHistorialEstado> HistorialesEstado { get; set; } = new List<SolicitudHistorialEstado>();
        public virtual ICollection<SolicitudAprobacion> Aprobaciones { get; set; } = new List<SolicitudAprobacion>();
        public virtual ICollection<SolicitudComentario> Comentarios { get; set; } = new List<SolicitudComentario>();
        public virtual ICollection<SolicitudAdjunto> Adjuntos { get; set; } = new List<SolicitudAdjunto>();
        public virtual ICollection<Notificacion> Notificaciones { get; set; } = new List<Notificacion>();
        public virtual ICollection<TareaDesarrollo> Tareas { get; set; } = new List<TareaDesarrollo>();
        public virtual ICollection<ActividadReciente> Actividades { get; set; } = new List<ActividadReciente>();
        public virtual ICollection<ProyectoSolicitud> ProyectosSolicitudes { get; set; } = new List<ProyectoSolicitud>();
        public virtual ICollection<DocumentoProyecto> Documentos { get; set; } = new List<DocumentoProyecto>();
        public virtual ICollection<EventoCalendario> Eventos { get; set; } = new List<EventoCalendario>();
        public virtual SolicitudModificacion? Modificacion { get; set; }
        public virtual SolicitudRequerimientoTecnico? RequerimientoTecnico { get; set; }
    }
}
