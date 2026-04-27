namespace SGSPCSI.API.Models
{
    public class Usuario
    {
        public int UsuarioId { get; set; }
        public string NombrePila { get; set; } = null!;
        public string ApellidoPaterno { get; set; } = null!;
        public string? ApellidoMaterno { get; set; }
        public string CorreoInstitucional { get; set; } = null!;
        public string? Puesto { get; set; }
        public bool Activo { get; set; }
        public DateTime FechaCreacion { get; set; }

        // Navigation properties
        public virtual UsuarioCredencial? Credencial { get; set; }
        public virtual ICollection<UsuarioRol> UsuariosRoles { get; set; } = new List<UsuarioRol>();
        public virtual ICollection<UsuarioArea> UsuariosAreas { get; set; } = new List<UsuarioArea>();
        public virtual ICollection<SistemaDesarrollador> SistemasDesarrollador { get; set; } = new List<SistemaDesarrollador>();
        public virtual ICollection<Solicitud> SolicitudesCreadas { get; set; } = new List<Solicitud>();
        public virtual ICollection<SolicitudDesarrollador> SolicitudesDesarrollador { get; set; } = new List<SolicitudDesarrollador>();
        public virtual ICollection<SolicitudHistorialEstado> HistorialEstadosCambiados { get; set; } = new List<SolicitudHistorialEstado>();
        public virtual ICollection<SolicitudAprobacion> Aprobaciones { get; set; } = new List<SolicitudAprobacion>();
        public virtual ICollection<SolicitudComentario> Comentarios { get; set; } = new List<SolicitudComentario>();
        public virtual ICollection<SolicitudAdjunto> Adjuntos { get; set; } = new List<SolicitudAdjunto>();
        public virtual ICollection<Notificacion> Notificaciones { get; set; } = new List<Notificacion>();
        public virtual ICollection<TareaDesarrollo> TareasCreadas { get; set; } = new List<TareaDesarrollo>();
        public virtual ICollection<TareaDesarrolloAsignacion> TareasAsignadas { get; set; } = new List<TareaDesarrolloAsignacion>();
        public virtual ICollection<ActividadReciente> Actividades { get; set; } = new List<ActividadReciente>();
        public virtual ICollection<Proyecto> Proyectos { get; set; } = new List<Proyecto>();
        public virtual ICollection<ProyectoMiembro> ProyectosMiembro { get; set; } = new List<ProyectoMiembro>();
        public virtual ICollection<DocumentoProyecto> DocumentosProyectos { get; set; } = new List<DocumentoProyecto>();
        public virtual ICollection<EventoCalendario> EventosCreados { get; set; } = new List<EventoCalendario>();
        public virtual ICollection<EventoParticipante> EventosParticipando { get; set; } = new List<EventoParticipante>();
        public virtual ICollection<SolicitudRequerimientoTecnico> RequerimientosTecnicos { get; set; } = new List<SolicitudRequerimientoTecnico>();
    }
}
