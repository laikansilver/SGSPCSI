using Microsoft.EntityFrameworkCore;
using SGSPCSI.API.Models;

namespace SGSPCSI.API.Data
{
    public class IssegDbContext : DbContext
    {
        public IssegDbContext(DbContextOptions<IssegDbContext> options) : base(options)
        {
        }

        // DbSets para catálogos base
        public DbSet<Rol> Roles { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Area> Areas { get; set; }
        public DbSet<Sistema> Sistemas { get; set; }
        public DbSet<TipoSolicitud> TiposSolicitud { get; set; }
        public DbSet<PrioridadSolicitud> PrioridadesSolicitud { get; set; }
        public DbSet<EstadoSolicitud> EstadosSolicitud { get; set; }

        // DbSets para relaciones de seguridad
        public DbSet<UsuarioRol> UsuariosRoles { get; set; }
        public DbSet<UsuarioArea> UsuariosAreas { get; set; }
        public DbSet<AreaSistema> AreaSistemas { get; set; }
        public DbSet<SistemaDesarrollador> SistemasDesarrollador { get; set; }
        public DbSet<UsuarioCredencial> UsuariosCredenciales { get; set; }

        // DbSets para solicitudes
        public DbSet<Solicitud> Solicitudes { get; set; }
        public DbSet<SolicitudDesarrollador> SolicitudesDesarrollador { get; set; }
        public DbSet<SolicitudHistorialEstado> SolicitudesHistorialEstado { get; set; }
        public DbSet<SolicitudAprobacion> SolicitudesAprobacion { get; set; }
        public DbSet<SolicitudComentario> SolicitudesComentarios { get; set; }
        public DbSet<SolicitudAdjunto> SolicitudesAdjuntos { get; set; }
        public DbSet<Notificacion> Notificaciones { get; set; }

        // DbSets para autenticación y menú
        public DbSet<MenuOpcion> MenuOpciones { get; set; }
        public DbSet<RolMenuOpcion> RolesMenuOpciones { get; set; }
        public DbSet<TipoModificacion> TiposModificacion { get; set; }
        public DbSet<SolicitudModificacion> SolicitudesModificacion { get; set; }
        public DbSet<SolicitudRequerimientoTecnico> SolicitudesRequerimientosTecnicos { get; set; }

        // DbSets para tareas y proyectos
        public DbSet<TareaDesarrollo> TareasDesarrollo { get; set; }
        public DbSet<TareaDesarrolloAsignacion> TareasDesarrolloAsignacion { get; set; }
        public DbSet<ActividadReciente> ActividadesRecientes { get; set; }
        public DbSet<Proyecto> Proyectos { get; set; }
        public DbSet<ProyectoSolicitud> ProyectosSolicitudes { get; set; }
        public DbSet<ProyectoMiembro> ProyectosMiembros { get; set; }
        public DbSet<DocumentoProyecto> DocumentosProyectos { get; set; }
        public DbSet<EventoCalendario> EventosCalendario { get; set; }
        public DbSet<EventoParticipante> EventosParticipantes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Apply snake_case naming convention to all tables and columns
                foreach (var entity in modelBuilder.Model.GetEntityTypes())
                {
                    // Convert table name to snake_case using the CLASS name (singular)
                    // This prevents EF Core from pluralizing the name
                    var tableName = entity.ClrType.Name;
                    entity.SetTableName(ToSnakeCase(tableName));

                    // Convert column names to snake_case
                    foreach (var property in entity.GetProperties())
                    {
                        var columnName = ToSnakeCase(property.Name);
                        property.SetColumnName(columnName);
                    }
                }

            // Configuración de Rol
            modelBuilder.Entity<Rol>(entity =>
            {
                entity.HasKey(e => e.RolId);
                entity.Property(e => e.Clave).IsRequired().HasMaxLength(30);
                entity.Property(e => e.Nombre).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Descripcion).HasMaxLength(255);
                entity.HasIndex(e => e.Clave).IsUnique();
            });

            // Configuración de Usuario
            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(e => e.UsuarioId);
                entity.Property(e => e.NombrePila).IsRequired().HasMaxLength(100);
                entity.Property(e => e.ApellidoPaterno).IsRequired().HasMaxLength(100);
                entity.Property(e => e.ApellidoMaterno).HasMaxLength(100);
                entity.Property(e => e.CorreoInstitucional).IsRequired().HasMaxLength(180);
                entity.Property(e => e.Puesto).HasMaxLength(120);
                entity.HasIndex(e => e.CorreoInstitucional).IsUnique();
            });

            // Configuración de Area
            modelBuilder.Entity<Area>(entity =>
            {
                entity.HasKey(e => e.AreaId);
                entity.Property(e => e.Clave).HasMaxLength(50);
                entity.Property(e => e.Nombre).IsRequired().HasMaxLength(220);
                entity.Property(e => e.TipoArea).IsRequired().HasMaxLength(30);
                entity.HasIndex(e => e.Nombre).IsUnique();
                entity.HasOne(e => e.AreaPadre)
                    .WithMany(e => e.AreasHijas)
                    .HasForeignKey(e => e.AreaPadreId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // Configuración de Sistema
            modelBuilder.Entity<Sistema>(entity =>
            {
                entity.HasKey(e => e.SistemaId);
                entity.Property(e => e.Clave).IsRequired().HasMaxLength(50);
                entity.Property(e => e.Nombre).IsRequired().HasMaxLength(200);
                entity.Property(e => e.Descripcion).HasMaxLength(500);
                entity.HasIndex(e => e.Clave).IsUnique();
            });

            // Configuración de TipoSolicitud
            modelBuilder.Entity<TipoSolicitud>(entity =>
            {
                entity.HasKey(e => e.TipoSolicitudId);
                entity.Property(e => e.Clave).IsRequired().HasMaxLength(40);
                entity.Property(e => e.Nombre).IsRequired().HasMaxLength(120);
                entity.HasIndex(e => e.Clave).IsUnique();
            });

            // Configuración de PrioridadSolicitud
            modelBuilder.Entity<PrioridadSolicitud>(entity =>
            {
                entity.HasKey(e => e.PrioridadSolicitudId);
                entity.Property(e => e.Clave).IsRequired().HasMaxLength(30);
                entity.Property(e => e.Nombre).IsRequired().HasMaxLength(80);
                entity.HasIndex(e => e.Clave).IsUnique();
                entity.HasIndex(e => e.Orden).IsUnique();
            });

            // Configuración de EstadoSolicitud
            modelBuilder.Entity<EstadoSolicitud>(entity =>
            {
                entity.HasKey(e => e.EstadoSolicitudId);
                entity.Property(e => e.Clave).IsRequired().HasMaxLength(30);
                entity.Property(e => e.Nombre).IsRequired().HasMaxLength(100);
                entity.HasIndex(e => e.Clave).IsUnique();
            });

            // Configuración de UsuarioRol
            modelBuilder.Entity<UsuarioRol>(entity =>
            {
                entity.HasKey(e => e.UsuarioRolId);
                entity.HasOne(e => e.Usuario)
                    .WithMany(e => e.UsuariosRoles)
                    .HasForeignKey(e => e.UsuarioId)
                    .OnDelete(DeleteBehavior.Cascade);
                entity.HasOne(e => e.Rol)
                    .WithMany(e => e.UsuariosRoles)
                    .HasForeignKey(e => e.RolId)
                    .OnDelete(DeleteBehavior.Cascade);
                entity.HasIndex(e => new { e.UsuarioId, e.RolId }).IsUnique();
            });

            // Configuración de UsuarioArea
            modelBuilder.Entity<UsuarioArea>(entity =>
            {
                entity.HasKey(e => e.UsuarioAreaId);
                entity.HasOne(e => e.Usuario)
                    .WithMany(e => e.UsuariosAreas)
                    .HasForeignKey(e => e.UsuarioId)
                    .OnDelete(DeleteBehavior.Cascade);
                entity.HasOne(e => e.Area)
                    .WithMany(e => e.UsuariosAreas)
                    .HasForeignKey(e => e.AreaId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // Configuración de AreaSistema
            modelBuilder.Entity<AreaSistema>(entity =>
            {
                entity.HasKey(e => e.AreaSistemaId);
                entity.HasOne(e => e.Area)
                    .WithMany(e => e.AreaSistemas)
                    .HasForeignKey(e => e.AreaId)
                    .OnDelete(DeleteBehavior.Cascade);
                entity.HasOne(e => e.Sistema)
                    .WithMany(e => e.AreaSistemas)
                    .HasForeignKey(e => e.SistemaId)
                    .OnDelete(DeleteBehavior.Cascade);
                entity.HasIndex(e => new { e.AreaId, e.SistemaId }).IsUnique();
            });

            // Configuración de SistemaDesarrollador
            modelBuilder.Entity<SistemaDesarrollador>(entity =>
            {
                entity.HasKey(e => e.SistemaDesarrolladorId);
                entity.Property(e => e.TipoParticipacion).IsRequired().HasMaxLength(20);
                entity.HasOne(e => e.Sistema)
                    .WithMany(e => e.SistemasDesarrollador)
                    .HasForeignKey(e => e.SistemaId)
                    .OnDelete(DeleteBehavior.Cascade);
                entity.HasOne(e => e.Usuario)
                    .WithMany(e => e.SistemasDesarrollador)
                    .HasForeignKey(e => e.UsuarioId)
                    .OnDelete(DeleteBehavior.Cascade);
                entity.HasIndex(e => new { e.SistemaId, e.UsuarioId }).IsUnique();
            });

            // Configuración de UsuarioCredencial
            modelBuilder.Entity<UsuarioCredencial>(entity =>
            {
                entity.HasKey(e => e.UsuarioId);
                entity.Property(e => e.LoginUsuario).IsRequired().HasMaxLength(80);
                entity.Property(e => e.AlgoritmoHash).IsRequired().HasMaxLength(30);
                entity.HasIndex(e => e.LoginUsuario).IsUnique();
                entity.HasOne(e => e.Usuario)
                    .WithOne(e => e.Credencial)
                    .HasForeignKey<UsuarioCredencial>(e => e.UsuarioId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // Configuración de Solicitud
            modelBuilder.Entity<Solicitud>(entity =>
            {
                entity.HasKey(e => e.SolicitudId);
                entity.Property(e => e.Folio).IsRequired().HasMaxLength(40);
                entity.Property(e => e.Titulo).IsRequired().HasMaxLength(200);
                entity.Property(e => e.Descripcion).IsRequired();
                entity.Property(e => e.EsfuerzoHoras).HasPrecision(10, 2);
                entity.HasIndex(e => e.Folio).IsUnique();
                entity.HasOne(e => e.AreaSolicitante)
                    .WithMany(e => e.Solicitudes)
                    .HasForeignKey(e => e.AreaSolicitanteId)
                    .OnDelete(DeleteBehavior.Restrict);
                entity.HasOne(e => e.CreadoPor)
                    .WithMany(e => e.SolicitudesCreadas)
                    .HasForeignKey(e => e.CreadoPorUsuarioId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // Configuración de SolicitudDesarrollador
            modelBuilder.Entity<SolicitudDesarrollador>(entity =>
            {
                entity.HasKey(e => e.SolicitudDesarrolladorId);
                entity.Property(e => e.TipoParticipacion).IsRequired().HasMaxLength(20);
                entity.HasOne(e => e.Solicitud)
                    .WithMany(e => e.SolicitudesDesarrollador)
                    .HasForeignKey(e => e.SolicitudId)
                    .OnDelete(DeleteBehavior.Cascade);
                entity.HasOne(e => e.Usuario)
                    .WithMany(e => e.SolicitudesDesarrollador)
                    .HasForeignKey(e => e.UsuarioId)
                    .OnDelete(DeleteBehavior.Cascade);
                entity.HasIndex(e => new { e.SolicitudId, e.UsuarioId }).IsUnique();
            });

            // Configuración de SolicitudHistorialEstado
            modelBuilder.Entity<SolicitudHistorialEstado>(entity =>
            {
                entity.HasKey(e => e.SolicitudHistorialEstadoId);
                entity.Property(e => e.Comentario).HasMaxLength(800);
                entity.HasOne(e => e.Solicitud)
                    .WithMany(e => e.HistorialesEstado)
                    .HasForeignKey(e => e.SolicitudId)
                    .OnDelete(DeleteBehavior.Cascade);
                entity.HasOne(e => e.EstadoAnterior)
                    .WithMany(e => e.HistorialesEstadoAnterior)
                    .HasForeignKey(e => e.EstadoAnteriorId)
                    .OnDelete(DeleteBehavior.Restrict);
                entity.HasOne(e => e.EstadoNuevo)
                    .WithMany(e => e.HistorialesEstadoNuevo)
                    .HasForeignKey(e => e.EstadoNuevoId)
                    .OnDelete(DeleteBehavior.Restrict);
                entity.HasOne(e => e.CambiadoPor)
                    .WithMany(e => e.HistorialEstadosCambiados)
                    .HasForeignKey(e => e.CambiadoPorUsuarioId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // Configuración de SolicitudAprobacion
            modelBuilder.Entity<SolicitudAprobacion>(entity =>
            {
                entity.HasKey(e => e.SolicitudAprobacionId);
                entity.Property(e => e.EstatusAprobacion).IsRequired().HasMaxLength(20);
                entity.Property(e => e.Motivo).HasMaxLength(1000);
                entity.HasOne(e => e.Solicitud)
                    .WithMany(e => e.Aprobaciones)
                    .HasForeignKey(e => e.SolicitudId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // Configuración de SolicitudComentario
            modelBuilder.Entity<SolicitudComentario>(entity =>
            {
                entity.HasKey(e => e.SolicitudComentarioId);
                entity.Property(e => e.Comentario).IsRequired().HasMaxLength(2000);
                entity.HasOne(e => e.Solicitud)
                    .WithMany(e => e.Comentarios)
                    .HasForeignKey(e => e.SolicitudId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // Configuración de SolicitudAdjunto
            modelBuilder.Entity<SolicitudAdjunto>(entity =>
            {
                entity.HasKey(e => e.SolicitudAdjuntoId);
                entity.Property(e => e.NombreArchivo).IsRequired().HasMaxLength(255);
                entity.Property(e => e.RutaArchivo).IsRequired().HasMaxLength(500);
                entity.Property(e => e.TipoMime).HasMaxLength(120);
                entity.HasOne(e => e.Solicitud)
                    .WithMany(e => e.Adjuntos)
                    .HasForeignKey(e => e.SolicitudId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // Configuración de Notificacion
            modelBuilder.Entity<Notificacion>(entity =>
            {
                entity.HasKey(e => e.NotificacionId);
                entity.Property(e => e.Titulo).IsRequired().HasMaxLength(220);
                entity.Property(e => e.Mensaje).IsRequired().HasMaxLength(1500);
                entity.HasOne(e => e.Usuario)
                    .WithMany(e => e.Notificaciones)
                    .HasForeignKey(e => e.UsuarioId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // Configuración de MenuOpcion
            modelBuilder.Entity<MenuOpcion>(entity =>
            {
                entity.HasKey(e => e.MenuOpcionId);
                entity.Property(e => e.Clave).IsRequired().HasMaxLength(60);
                entity.Property(e => e.Nombre).IsRequired().HasMaxLength(120);
                entity.Property(e => e.Ruta).IsRequired().HasMaxLength(220);
                entity.Property(e => e.Icono).HasMaxLength(80);
                entity.HasIndex(e => e.Clave).IsUnique();
                entity.HasOne(e => e.MenuPadre)
                    .WithMany(e => e.MenusHijos)
                    .HasForeignKey(e => e.MenuPadreId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // Configuración de RolMenuOpcion
            modelBuilder.Entity<RolMenuOpcion>(entity =>
            {
                entity.HasKey(e => e.RolMenuOpcionId);
                entity.HasIndex(e => new { e.RolId, e.MenuOpcionId }).IsUnique();
                entity.HasOne(e => e.Rol)
                    .WithMany(e => e.RolMenuOpciones)
                    .HasForeignKey(e => e.RolId)
                    .OnDelete(DeleteBehavior.Cascade);
                entity.HasOne(e => e.MenuOpcion)
                    .WithMany(e => e.RolMenuOpciones)
                    .HasForeignKey(e => e.MenuOpcionId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // Configuración de TipoModificacion
            modelBuilder.Entity<TipoModificacion>(entity =>
            {
                entity.HasKey(e => e.TipoModificacionId);
                entity.Property(e => e.Clave).IsRequired().HasMaxLength(30);
                entity.Property(e => e.Nombre).IsRequired().HasMaxLength(80);
                entity.HasIndex(e => e.Clave).IsUnique();
            });

            // Configuración de SolicitudModificacion
            modelBuilder.Entity<SolicitudModificacion>(entity =>
            {
                entity.HasKey(e => e.SolicitudModificacionId);
                entity.Property(e => e.SistemaVersionActual).HasMaxLength(80);
                entity.Property(e => e.ModuloAfectado).HasMaxLength(200);
                entity.Property(e => e.ImpactoTecnico).HasMaxLength(1500);
                entity.Property(e => e.Justificacion).HasMaxLength(2000);
                entity.HasIndex(e => e.SolicitudId).IsUnique();
                entity.HasOne(e => e.Solicitud)
                    .WithOne(e => e.Modificacion)
                    .HasForeignKey<SolicitudModificacion>(e => e.SolicitudId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // Configuración de SolicitudRequerimientoTecnico
            modelBuilder.Entity<SolicitudRequerimientoTecnico>(entity =>
            {
                entity.HasKey(e => e.SolicitudRequerimientoTecnicoId);
                entity.Property(e => e.ArquitecturaPropuesta).HasMaxLength(2000);
                entity.Property(e => e.AlcanceTecnico).HasMaxLength(2000);
                entity.Property(e => e.Dependencias).HasMaxLength(2000);
                entity.Property(e => e.CriteriosAceptacion).HasMaxLength(2000);
                entity.Property(e => e.Riesgos).HasMaxLength(2000);
                entity.Property(e => e.PlanPruebas).HasMaxLength(2000);
                entity.Property(e => e.Observaciones).HasMaxLength(2000);
                entity.HasIndex(e => e.SolicitudId).IsUnique();
                entity.HasOne(e => e.Solicitud)
                    .WithOne(e => e.RequerimientoTecnico)
                    .HasForeignKey<SolicitudRequerimientoTecnico>(e => e.SolicitudId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // Configuración de TareaDesarrollo
            modelBuilder.Entity<TareaDesarrollo>(entity =>
            {
                entity.HasKey(e => e.TareaDesarrolloId);
                entity.Property(e => e.Titulo).IsRequired().HasMaxLength(220);
                entity.Property(e => e.Descripcion).HasMaxLength(2000);
                entity.Property(e => e.EstadoTarea).IsRequired().HasMaxLength(30);
                entity.HasOne(e => e.Solicitud)
                    .WithMany(e => e.Tareas)
                    .HasForeignKey(e => e.SolicitudId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // Configuración de TareaDesarrolloAsignacion
            modelBuilder.Entity<TareaDesarrolloAsignacion>(entity =>
            {
                entity.HasKey(e => e.TareaDesarrolloAsignacionId);
                entity.Property(e => e.RolAsignacion).HasMaxLength(40);
                entity.HasIndex(e => new { e.TareaDesarrolloId, e.UsuarioId }).IsUnique();
                entity.HasOne(e => e.TareaDesarrollo)
                    .WithMany(e => e.Asignaciones)
                    .HasForeignKey(e => e.TareaDesarrolloId)
                    .OnDelete(DeleteBehavior.Cascade);
                entity.HasOne(e => e.Usuario)
                    .WithMany(e => e.TareasAsignadas)
                    .HasForeignKey(e => e.UsuarioId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // Configuración de ActividadReciente
            modelBuilder.Entity<ActividadReciente>(entity =>
            {
                entity.HasKey(e => e.ActividadRecienteId);
                entity.Property(e => e.TipoActividad).IsRequired().HasMaxLength(40);
                entity.Property(e => e.Titulo).IsRequired().HasMaxLength(220);
                entity.Property(e => e.Detalle).HasMaxLength(2000);
                entity.HasOne(e => e.Usuario)
                    .WithMany(e => e.Actividades)
                    .HasForeignKey(e => e.UsuarioId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // Configuración de Proyecto
            modelBuilder.Entity<Proyecto>(entity =>
            {
                entity.HasKey(e => e.ProyectoId);
                entity.Property(e => e.Clave).IsRequired().HasMaxLength(40);
                entity.Property(e => e.Nombre).IsRequired().HasMaxLength(200);
                entity.Property(e => e.Descripcion).HasMaxLength(1500);
                entity.Property(e => e.EstadoProyecto).IsRequired().HasMaxLength(30);
                entity.HasIndex(e => e.Clave).IsUnique();
                entity.HasOne(e => e.Pm)
                    .WithMany(e => e.Proyectos)
                    .HasForeignKey(e => e.PmUsuarioId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // Configuración de ProyectoSolicitud
            modelBuilder.Entity<ProyectoSolicitud>(entity =>
            {
                entity.HasKey(e => e.ProyectoSolicitudId);
                entity.HasIndex(e => new { e.ProyectoId, e.SolicitudId }).IsUnique();
                entity.HasOne(e => e.Proyecto)
                    .WithMany(e => e.ProyectosSolicitudes)
                    .HasForeignKey(e => e.ProyectoId)
                    .OnDelete(DeleteBehavior.Cascade);
                entity.HasOne(e => e.Solicitud)
                    .WithMany(e => e.ProyectosSolicitudes)
                    .HasForeignKey(e => e.SolicitudId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // Configuración de ProyectoMiembro
            modelBuilder.Entity<ProyectoMiembro>(entity =>
            {
                entity.HasKey(e => e.ProyectoMiembroId);
                entity.Property(e => e.RolEnProyecto).IsRequired().HasMaxLength(40);
                entity.Property(e => e.CargaEstimadaPct).HasPrecision(5, 2);
                entity.HasIndex(e => new { e.ProyectoId, e.UsuarioId }).IsUnique();
                entity.HasOne(e => e.Proyecto)
                    .WithMany(e => e.Miembros)
                    .HasForeignKey(e => e.ProyectoId)
                    .OnDelete(DeleteBehavior.Cascade);
                entity.HasOne(e => e.Usuario)
                    .WithMany(e => e.ProyectosMiembro)
                    .HasForeignKey(e => e.UsuarioId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // Configuración de DocumentoProyecto
            modelBuilder.Entity<DocumentoProyecto>(entity =>
            {
                entity.HasKey(e => e.DocumentoProyectoId);
                entity.Property(e => e.NombreDocumento).IsRequired().HasMaxLength(255);
                entity.Property(e => e.TipoDocumento).IsRequired().HasMaxLength(50);
                entity.Property(e => e.VersionDocumento).HasMaxLength(40);
                entity.Property(e => e.RutaArchivo).IsRequired().HasMaxLength(600);
                entity.HasOne(e => e.Proyecto)
                    .WithMany(e => e.Documentos)
                    .HasForeignKey(e => e.ProyectoId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // Configuración de EventoCalendario
            modelBuilder.Entity<EventoCalendario>(entity =>
            {
                entity.HasKey(e => e.EventoCalendarioId);
                entity.Property(e => e.Titulo).IsRequired().HasMaxLength(220);
                entity.Property(e => e.Descripcion).HasMaxLength(2000);
                entity.Property(e => e.TipoEvento).IsRequired().HasMaxLength(40);
                entity.HasOne(e => e.Proyecto)
                    .WithMany(e => e.Eventos)
                    .HasForeignKey(e => e.ProyectoId)
                    .OnDelete(DeleteBehavior.SetNull);
                entity.HasOne(e => e.Solicitud)
                    .WithMany(e => e.Eventos)
                    .HasForeignKey(e => e.SolicitudId)
                    .OnDelete(DeleteBehavior.SetNull);
            });

            // Configuración de EventoParticipante
            modelBuilder.Entity<EventoParticipante>(entity =>
            {
                entity.HasKey(e => e.EventoParticipanteId);
                entity.Property(e => e.Asistencia).IsRequired().HasMaxLength(20);
                entity.HasIndex(e => new { e.EventoCalendarioId, e.UsuarioId }).IsUnique();
                entity.HasOne(e => e.EventoCalendario)
                    .WithMany(e => e.Participantes)
                    .HasForeignKey(e => e.EventoCalendarioId)
                    .OnDelete(DeleteBehavior.Cascade);
                entity.HasOne(e => e.Usuario)
                    .WithMany(e => e.EventosParticipando)
                    .HasForeignKey(e => e.UsuarioId)
                    .OnDelete(DeleteBehavior.Restrict);
            });
        }

        /// <summary>
        /// Converts a PascalCase or camelCase string to snake_case.
        /// </summary>
        private static string ToSnakeCase(string name)
        {
            if (string.IsNullOrEmpty(name))
                return name;

            var result = new System.Text.StringBuilder();
            for (int i = 0; i < name.Length; i++)
            {
                char c = name[i];
                if (i > 0 && char.IsUpper(c))
                {
                    result.Append('_');
                }
                result.Append(char.ToLower(c));
            }
            return result.ToString();
        }
    }
}
