using Microsoft.AspNetCore.Mvc;
using SGSPCSI.API.Models;
using SGSPCSI.API.Repositories;

namespace SGSPCSI.API.Controllers
{
    [Route("api/[controller]")]
    public class RolesController : CrudController<Rol, int>
    {
        public RolesController(IRepository<Rol> repository) : base(repository) { }
    }

    [Route("api/[controller]")]
    public class SistemasController : CrudController<Sistema, int>
    {
        public SistemasController(IRepository<Sistema> repository) : base(repository) { }
    }

    [Route("api/[controller]")]
    public class TiposSolicitudController : CrudController<TipoSolicitud, int>
    {
        public TiposSolicitudController(IRepository<TipoSolicitud> repository) : base(repository) { }
    }

    [Route("api/[controller]")]
    public class PrioridadesSolicitudController : CrudController<PrioridadSolicitud, int>
    {
        public PrioridadesSolicitudController(IRepository<PrioridadSolicitud> repository) : base(repository) { }
    }

    [Route("api/[controller]")]
    public class EstadosSolicitudController : CrudController<EstadoSolicitud, int>
    {
        public EstadosSolicitudController(IRepository<EstadoSolicitud> repository) : base(repository) { }
    }

    [Route("api/[controller]")]
    public class MenuOpcionesController : CrudController<MenuOpcion, int>
    {
        public MenuOpcionesController(IRepository<MenuOpcion> repository) : base(repository) { }
    }

    [Route("api/[controller]")]
    public class UsuarioRolesController : CrudController<UsuarioRol, int>
    {
        public UsuarioRolesController(IRepository<UsuarioRol> repository) : base(repository) { }
    }

    [Route("api/[controller]")]
    public class UsuarioAreasController : CrudController<UsuarioArea, int>
    {
        public UsuarioAreasController(IRepository<UsuarioArea> repository) : base(repository) { }
    }

    [Route("api/[controller]")]
    public class AreaSistemasController : CrudController<AreaSistema, int>
    {
        public AreaSistemasController(IRepository<AreaSistema> repository) : base(repository) { }
    }

    [Route("api/[controller]")]
    public class RolMenuOpcionesController : CrudController<RolMenuOpcion, int>
    {
        public RolMenuOpcionesController(IRepository<RolMenuOpcion> repository) : base(repository) { }
    }

    [Route("api/[controller]")]
    public class SistemaDesarrolladoresController : CrudController<SistemaDesarrollador, int>
    {
        public SistemaDesarrolladoresController(IRepository<SistemaDesarrollador> repository) : base(repository) { }
    }

    [Route("api/[controller]")]
    public class SolicitudDesarrolladoresController : CrudController<SolicitudDesarrollador, int>
    {
        public SolicitudDesarrolladoresController(IRepository<SolicitudDesarrollador> repository) : base(repository) { }
    }

    [Route("api/[controller]")]
    public class SolicitudHistorialEstadosController : CrudController<SolicitudHistorialEstado, int>
    {
        public SolicitudHistorialEstadosController(IRepository<SolicitudHistorialEstado> repository) : base(repository) { }
    }

    [Route("api/[controller]")]
    public class SolicitudAprobacionesController : CrudController<SolicitudAprobacion, int>
    {
        public SolicitudAprobacionesController(IRepository<SolicitudAprobacion> repository) : base(repository) { }
    }

    [Route("api/[controller]")]
    public class SolicitudComentariosController : CrudController<SolicitudComentario, int>
    {
        public SolicitudComentariosController(IRepository<SolicitudComentario> repository) : base(repository) { }
    }

    [Route("api/[controller]")]
    public class SolicitudAdjuntosController : CrudController<SolicitudAdjunto, int>
    {
        public SolicitudAdjuntosController(IRepository<SolicitudAdjunto> repository) : base(repository) { }
    }

    [Route("api/[controller]")]
    public class TareaDesarrolloAsignacionesController : CrudController<TareaDesarrolloAsignacion, int>
    {
        public TareaDesarrolloAsignacionesController(IRepository<TareaDesarrolloAsignacion> repository) : base(repository) { }
    }

    [Route("api/[controller]")]
    public class ProyectoMiembrosController : CrudController<ProyectoMiembro, int>
    {
        public ProyectoMiembrosController(IRepository<ProyectoMiembro> repository) : base(repository) { }
    }

    [Route("api/[controller]")]
    public class ProyectoSolicitudesController : CrudController<ProyectoSolicitud, int>
    {
        public ProyectoSolicitudesController(IRepository<ProyectoSolicitud> repository) : base(repository) { }
    }

    [Route("api/[controller]")]
    public class EventosCalendarioController : CrudController<EventoCalendario, int>
    {
        public EventosCalendarioController(IRepository<EventoCalendario> repository) : base(repository) { }
    }

    [Route("api/[controller]")]
    public class EventosParticipantesController : CrudController<EventoParticipante, int>
    {
        public EventosParticipantesController(IRepository<EventoParticipante> repository) : base(repository) { }
    }

    [Route("api/[controller]")]
    public class DocumentosProyectosController : CrudController<DocumentoProyecto, int>
    {
        public DocumentosProyectosController(IRepository<DocumentoProyecto> repository) : base(repository) { }
    }

    [Route("api/[controller]")]
    public class ActividadesRecientesController : CrudController<ActividadReciente, int>
    {
        public ActividadesRecientesController(IRepository<ActividadReciente> repository) : base(repository) { }
    }

    [Route("api/[controller]")]
    public class UsuariosCredencialesController : CrudController<UsuarioCredencial, int>
    {
        public UsuariosCredencialesController(IRepository<UsuarioCredencial> repository) : base(repository) { }
    }
}