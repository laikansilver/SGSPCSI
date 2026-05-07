# API RESTful SGSPCSI

**Responsable:** Eduardo  
**Herramienta:** Visual Studio Community  
**Estado:** ✅ Implementado

## Descripción

API RESTful completa para el Sistema de Gestión de Solicitudes para la Coordinación de Sistemas Institucionales (SGSPCSI). Implementa patrón de arquitectura en capas (Repositorio, Servicios, Controladores) con Entity Framework Core para acceso a datos.

## Características

- ✅ 30 modelos de entidad correspondientes a las 33 tablas SQL Server
- ✅ Patrón Repositorio genérico + repositorios específicos
- ✅ Servicios de negocio para lógica centralizada
- ✅ 6 controladores API principales con operaciones CRUD
- ✅ Entity Framework Core DbContext con todas las configuraciones
- ✅ DTOs para transferencia de datos segura
- ✅ Inyección de dependencias completa
- ✅ Swagger/OpenAPI integrado
- ✅ CORS configurado para desarrollo local
- ✅ Migraciones automáticas en startup

## Estructura del proyecto

```
API/
├── Controllers/              # Controladores REST (6)
│   ├── SolicitudesController.cs
│   ├── UsuariosController.cs
│   ├── AreasController.cs
│   ├── TareasDesarrolloController.cs
│   ├── ProyectosController.cs
│   └── NotificacionesController.cs
├── Models/                   # Entidades (30 clases)
│   ├── Catalogs base/
│   ├── Security relations/
│   ├── Solicitudes/
│   ├── Authentication/
│   ├── Tasks & Projects/
│   └── Calendar events/
├── DTOs/                     # Data Transfer Objects (8 entidades)
│   ├── RolDto.cs
│   ├── UsuarioDto.cs
│   ├── AreaDto.cs
│   ├── SistemaDto.cs
│   ├── SolicitudDto.cs
│   ├── TareaDesarrolloDto.cs
│   ├── ProyectoDto.cs
│   └── NotificacionDto.cs
├── Services/                 # Lógica de negocio (6 servicios)
│   ├── ISolicitudService.cs / SolicitudService.cs
│   ├── IUsuarioService.cs / UsuarioService.cs
│   ├── IAreaService.cs / AreaService.cs
│   ├── ITareaDesarrolloService.cs / TareaDesarrolloService.cs
│   ├── IProyectoService.cs / ProyectoService.cs
│   └── INotificacionService.cs / NotificacionService.cs
├── Repositories/             # Acceso a datos
│   ├── IRepository.cs / Repository.cs (genérico)
│   ├── ISolicitudRepository.cs / SolicitudRepository.cs
│   ├── IUsuarioRepository.cs / UsuarioRepository.cs
│   ├── IAreaRepository.cs / AreaRepository.cs
│   ├── ITareaDesarrolloRepository.cs / TareaDesarrolloRepository.cs
│   ├── IProyectoRepository.cs / ProyectoRepository.cs
│   └── INotificacionRepository.cs / NotificacionRepository.cs
├── Data/                     # Entity Framework Core
│   └── IssegDbContext.cs (29 DbSets configurados)
├── SGSPCSI.API.csproj       # Proyecto con dependencias NuGet
├── Program.cs               # Configuración y startup
├── appsettings.json         # Configuración
└── README.md                # Esta documentación
```

## Tecnologías

- **Framework:** ASP.NET Core 8.0
- **ORM:** Entity Framework Core 8.0
- **Database:** SQL Server 2022
- **API:** REST con Swagger/OpenAPI
- **DI:** Inyección de dependencias nativa de ASP.NET Core
- **Migrations:** EF Core Migrations automáticas

## Endpoints principales

### Solicitudes
```
GET    /api/solicitudes              # Obtener todas
GET    /api/solicitudes/{id}         # Obtener por ID
GET    /api/solicitudes/area/{areaId}         # Por área
GET    /api/solicitudes/desarrollador/{usuarioId} # Por desarrollador
POST   /api/solicitudes              # Crear
PUT    /api/solicitudes/{id}         # Actualizar
DELETE /api/solicitudes/{id}         # Eliminar
```

### Usuarios
```
GET    /api/usuarios                 # Obtener todas
GET    /api/usuarios/{id}            # Obtener por ID
GET    /api/usuarios/correo/{correo} # Por correo
POST   /api/usuarios                 # Crear
PUT    /api/usuarios/{id}            # Actualizar
DELETE /api/usuarios/{id}            # Eliminar
```

### Áreas
```
GET    /api/areas                    # Obtener todas
GET    /api/areas/{id}               # Obtener por ID
GET    /api/areas/raiz               # Áreas raíz
POST   /api/areas                    # Crear
PUT    /api/areas/{id}               # Actualizar
DELETE /api/areas/{id}               # Eliminar
```

### Tareas de Desarrollo
```
GET    /api/tarasdesarrollo/{id}                    # Obtener por ID
GET    /api/tarasdesarrollo/solicitud/{solicitudId} # Por solicitud
GET    /api/tarasdesarrollo/asignadas/{usuarioId}   # Asignadas a usuario
POST   /api/tarasdesarrollo                         # Crear
PUT    /api/tarasdesarrollo/{id}                    # Actualizar
DELETE /api/tarasdesarrollo/{id}                    # Eliminar
```

### Proyectos
```
GET    /api/proyectos                # Obtener todas
GET    /api/proyectos/{id}           # Obtener por ID
GET    /api/proyectos/clave/{clave}  # Por clave
GET    /api/proyectos/pm/{usuarioId} # Por PM
POST   /api/proyectos                # Crear
PUT    /api/proyectos/{id}           # Actualizar
DELETE /api/proyectos/{id}           # Eliminar
```

### Notificaciones
```
GET    /api/notificaciones/{id}                     # Obtener por ID
GET    /api/notificaciones/usuario/{usuarioId}      # Del usuario
GET    /api/notificaciones/usuario/{usuarioId}/no-leidas # No leídas
POST   /api/notificaciones                          # Crear
PATCH  /api/notificaciones/{id}/marcar-leida        # Marcar leída
DELETE /api/notificaciones/{id}                     # Eliminar
```

## Instrucciones de configuración

### Prerequisitos
- Visual Studio Community 2022 o superior
- .NET 8.0 SDK
- SQL Server 2022 (local o remoto)

### Pasos de instalación

1. **Abrir la solución**
   ```bash
   cd SGSPCSI
   ```

2. **Restaurar paquetes NuGet**
   - Click derecho en la solución → "Restaurar paquetes NuGet"
   - O desde Package Manager Console:
   ```powershell
   Update-Package
   ```

3. **Configurar la cadena de conexión**
   - Editar `appsettings.json`
   - Actualizar `ConnectionStrings.DefaultConnection` con tu servidor SQL Server
   ```json
   "ConnectionStrings": {
   "DefaultConnection": "Server=TU_SERVIDOR;Database=SGSPCSI_DB;Trusted_Connection=true;TrustServerCertificate=true;"
   }
   ```

4. **Crear la base de datos (si no existe)**
   - Ejecutar el script SQL desde: `../base de datos/01_crear_base_datos.sql`
   - O dejar que EF Core cree las tablas automáticamente en el primer run

5. **Compilar el proyecto**
   ```bash
   dotnet build
   ```

6. **Ejecutar la API**
   - F5 o Ctrl+F5 en Visual Studio
   - O desde línea de comandos:
   ```bash
   dotnet run
   ```

7. **Acceder a Swagger**
   - Navegar a: `https://localhost:7000/swagger` (puerto puede variar)
   - Explorar y probar todos los endpoints

## Configuración de base de datos

### Migraciones EF Core

Si necesitas crear una migración personalizada:

```powershell
# En Package Manager Console
Add-Migration NombreMigracion
Update-Database
```

La migración automática ocurre en el startup del programa.

## Notas de desarrollo

### Autenticación
- Actualmente el `CreadoPorUsuarioId` se establece como 1 (hardcoded)
- Modificar `CreateSolicitudAsync` en SolicitudService para obtener del usuario autenticado
- Recomendar: Implementar JWT o claims basados en HttpContext

### CORS
- Configurado para `http://localhost:3000` y `http://localhost:5173` (desarrollo)
- Modificar `appsettings.json` para producción

### Logging
- EF Core logging habilitado en appsettings
- Revisar `Logging.LogLevel.Microsoft.EntityFrameworkCore` para debugging

### Próximas mejoras
- Agregar AutoMapper para mapping automático entre modelos y DTOs
- Implementar autenticación JWT
- Agregar validaciones fluent con FluentValidation
- Crear especificaciones (ISpecification) para queries complejas
- Implementar Unit of Work pattern
- Agregar tests unitarios

## Soporte

Para consultas sobre la API:
- Revisar documentación en Swagger UI
- Consultar comentarios XML en clases (generar con `/// <summary>`)
- Verificar logs en Output window durante development
