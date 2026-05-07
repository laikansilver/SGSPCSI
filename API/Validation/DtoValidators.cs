using FluentValidation;
using SGSPCSI.API.DTOs;

namespace SGSPCSI.API.Validation;

public class CreateUsuarioDtoValidator : AbstractValidator<CreateUsuarioDto>
{
    public CreateUsuarioDtoValidator()
    {
        RuleFor(x => x.NombrePila).NotEmpty().MaximumLength(100);
        RuleFor(x => x.ApellidoPaterno).NotEmpty().MaximumLength(100);
        RuleFor(x => x.ApellidoMaterno).MaximumLength(100);
        RuleFor(x => x.CorreoInstitucional).NotEmpty().EmailAddress().MaximumLength(180);
        RuleFor(x => x.Puesto).MaximumLength(120);
    }
}

public class UpdateUsuarioDtoValidator : AbstractValidator<UpdateUsuarioDto>
{
    public UpdateUsuarioDtoValidator()
    {
        RuleFor(x => x.NombrePila).NotEmpty().MaximumLength(100);
        RuleFor(x => x.ApellidoPaterno).NotEmpty().MaximumLength(100);
        RuleFor(x => x.ApellidoMaterno).MaximumLength(100);
        RuleFor(x => x.Puesto).MaximumLength(120);
    }
}

public class CreateAreaDtoValidator : AbstractValidator<CreateAreaDto>
{
    public CreateAreaDtoValidator()
    {
        RuleFor(x => x.Nombre).NotEmpty().MaximumLength(220);
        RuleFor(x => x.TipoArea).NotEmpty().MaximumLength(30);
        RuleFor(x => x.Clave).MaximumLength(50);
    }
}

public class UpdateAreaDtoValidator : AbstractValidator<UpdateAreaDto>
{
    public UpdateAreaDtoValidator()
    {
        RuleFor(x => x.Nombre).NotEmpty().MaximumLength(220);
        RuleFor(x => x.TipoArea).NotEmpty().MaximumLength(30);
        RuleFor(x => x.Clave).MaximumLength(50);
    }
}

public class CreateSolicitudDtoValidator : AbstractValidator<CreateSolicitudDto>
{
    public CreateSolicitudDtoValidator()
    {
        RuleFor(x => x.Titulo).NotEmpty().MaximumLength(200);
        RuleFor(x => x.Descripcion).NotEmpty();
        RuleFor(x => x.AreaSolicitanteId).GreaterThan(0);
        RuleFor(x => x.TipoSolicitudId).GreaterThan(0);
        RuleFor(x => x.PrioridadSolicitudId).GreaterThan(0);
    }
}

public class UpdateSolicitudDtoValidator : AbstractValidator<UpdateSolicitudDto>
{
    public UpdateSolicitudDtoValidator()
    {
        RuleFor(x => x.Titulo).NotEmpty().MaximumLength(200);
        RuleFor(x => x.Descripcion).NotEmpty();
        RuleFor(x => x.TipoSolicitudId).GreaterThan(0);
        RuleFor(x => x.PrioridadSolicitudId).GreaterThan(0);
        RuleFor(x => x.EstadoSolicitudId).GreaterThan(0);
    }
}

public class CreateTareaDesarrolloDtoValidator : AbstractValidator<CreateTareaDesarrolloDto>
{
    public CreateTareaDesarrolloDtoValidator()
    {
        RuleFor(x => x.SolicitudId).GreaterThan(0);
        RuleFor(x => x.Titulo).NotEmpty().MaximumLength(200);
        RuleFor(x => x.Descripcion).MaximumLength(500);
        RuleFor(x => x.PrioridadSolicitudId).GreaterThan(0);
    }
}

public class UpdateTareaDesarrolloDtoValidator : AbstractValidator<UpdateTareaDesarrolloDto>
{
    public UpdateTareaDesarrolloDtoValidator()
    {
        RuleFor(x => x.Titulo).NotEmpty().MaximumLength(200);
        RuleFor(x => x.Descripcion).MaximumLength(500);
        RuleFor(x => x.PrioridadSolicitudId).GreaterThan(0);
        RuleFor(x => x.EstadoTarea).NotEmpty().MaximumLength(50);
    }
}

public class CreateProyectoDtoValidator : AbstractValidator<CreateProyectoDto>
{
    public CreateProyectoDtoValidator()
    {
        RuleFor(x => x.Clave).NotEmpty().MaximumLength(50);
        RuleFor(x => x.Nombre).NotEmpty().MaximumLength(200);
        RuleFor(x => x.Descripcion).MaximumLength(500);
        RuleFor(x => x.PmUsuarioId).GreaterThan(0);
    }
}

public class UpdateProyectoDtoValidator : AbstractValidator<UpdateProyectoDto>
{
    public UpdateProyectoDtoValidator()
    {
        RuleFor(x => x.Nombre).NotEmpty().MaximumLength(200);
        RuleFor(x => x.Descripcion).MaximumLength(500);
        RuleFor(x => x.EstadoProyecto).NotEmpty().MaximumLength(80);
    }
}

public class CreateNotificacionDtoValidator : AbstractValidator<CreateNotificacionDto>
{
    public CreateNotificacionDtoValidator()
    {
        RuleFor(x => x.UsuarioId).GreaterThan(0);
        RuleFor(x => x.Titulo).NotEmpty().MaximumLength(200);
        RuleFor(x => x.Mensaje).NotEmpty().MaximumLength(1000);
    }
}

public class UpdateNotificacionDtoValidator : AbstractValidator<UpdateNotificacionDto>
{
    public UpdateNotificacionDtoValidator()
    {
        RuleFor(x => x.Leida).NotNull();
    }
}

public class CreateRolDtoValidator : AbstractValidator<CreateRolDto>
{
    public CreateRolDtoValidator()
    {
        RuleFor(x => x.Clave).NotEmpty().MaximumLength(30);
        RuleFor(x => x.Nombre).NotEmpty().MaximumLength(100);
        RuleFor(x => x.Descripcion).MaximumLength(255);
    }
}

public class UpdateRolDtoValidator : AbstractValidator<UpdateRolDto>
{
    public UpdateRolDtoValidator()
    {
        RuleFor(x => x.Nombre).NotEmpty().MaximumLength(100);
        RuleFor(x => x.Descripcion).MaximumLength(255);
    }
}

public class LoginRequestDtoValidator : AbstractValidator<LoginRequestDto>
{
    public LoginRequestDtoValidator()
    {
        RuleFor(x => x.LoginUsuario).NotEmpty().MaximumLength(80);
        RuleFor(x => x.Password).NotEmpty().MinimumLength(8);
    }
}