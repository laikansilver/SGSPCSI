using AutoMapper;
using SGSPCSI.API.DTOs;
using SGSPCSI.API.Models;

namespace SGSPCSI.API.Profiles
{
    public class IssegProfile : Profile
    {
        public IssegProfile()
        {
            CreateMap<Usuario, UsuarioDto>().ReverseMap();
            CreateMap<CreateUsuarioDto, Usuario>();
            CreateMap<UpdateUsuarioDto, Usuario>();

            CreateMap<Area, AreaDto>().ReverseMap();
            CreateMap<CreateAreaDto, Area>();
            CreateMap<UpdateAreaDto, Area>();

            CreateMap<Solicitud, SolicitudDto>().ReverseMap();
            CreateMap<CreateSolicitudDto, Solicitud>();
            CreateMap<UpdateSolicitudDto, Solicitud>();

            CreateMap<TareaDesarrollo, TareaDesarrolloDto>().ReverseMap();
            CreateMap<CreateTareaDesarrolloDto, TareaDesarrollo>();
            CreateMap<UpdateTareaDesarrolloDto, TareaDesarrollo>();

            CreateMap<Proyecto, ProyectoDto>().ReverseMap();
            CreateMap<CreateProyectoDto, Proyecto>();
            CreateMap<UpdateProyectoDto, Proyecto>();

            CreateMap<Notificacion, NotificacionDto>().ReverseMap();
            CreateMap<CreateNotificacionDto, Notificacion>();
            CreateMap<UpdateNotificacionDto, Notificacion>();

            CreateMap<Rol, RolDto>().ReverseMap();
            CreateMap<CreateRolDto, Rol>();
            CreateMap<UpdateRolDto, Rol>();

            CreateMap<LoginRequestDto, UsuarioCredencial>();
            CreateMap<Usuario, AuthResponseDto>();
        }
    }
}