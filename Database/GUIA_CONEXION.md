# Guía de Conexión de Base de Datos - ISSEG_DB

## 📌 Información de Conexión

### Servidor SQL Server
- **Nombre del Servidor:** `.` (Local)
- **Puerto:** 1433 (por defecto)
- **Base de Datos:** ISSEG_DB
- **Versión:** SQL Server 2025 (compatible con SQL Server 2022)
- **Autenticación:** Windows (Recomendado) o SQL Server Authentication

---

## 🔗 Cadenas de Conexión

### Opción 1: Autenticación de Windows (Recomendado)
```csharp
// appsettings.json
{
  "ConnectionStrings": {
	"DefaultConnection": "Server=.;Database=ISSEG_DB;Trusted_Connection=true;TrustServerCertificate=true;Encrypt=false;"
  }
}
```

### Opción 2: Autenticación SQL Server
```csharp
// appsettings.json (Requiere usuario y contraseña)
{
  "ConnectionStrings": {
	"DefaultConnection": "Server=.;Database=ISSEG_DB;User Id=usuario;Password=contraseña;TrustServerCertificate=true;Encrypt=false;"
  }
}
```

### Opción 3: Conexión a Servidor Remoto
```csharp
// appsettings.json (si SQL Server está en otro equipo)
{
  "ConnectionStrings": {
	"DefaultConnection": "Server=tu-servidor.ejemplo.com;Database=ISSEG_DB;User Id=usuario;Password=contraseña;TrustServerCertificate=true;Encrypt=false;"
  }
}
```

### Opción 4: Conexión Named Pipes (en red local)
```csharp
// appsettings.json
{
  "ConnectionStrings": {
	"DefaultConnection": "Server=np:\\\\.\\pipe\\MSSQL$SQLEXPRESS\\sql\\query;Database=ISSEG_DB;Trusted_Connection=true;TrustServerCertificate=true;"
  }
}
```

---

## 🔐 Creación de Usuario de Base de Datos (Opcional)

Si deseas crear un usuario específico para tu aplicación, ejecuta estos comandos en SQL Server:

```sql
-- Crear un nuevo usuario de SQL Server
CREATE LOGIN [isseg_user] WITH PASSWORD = 'TuContraseñaSegura123!';
GO

-- Crear el usuario en la base de datos
USE ISSEG_DB;
CREATE USER [isseg_user] FOR LOGIN [isseg_user];
GO

-- Asignar permisos
ALTER ROLE db_owner ADD MEMBER [isseg_user];
GO

-- Verificar el usuario
SELECT name FROM sys.database_principals WHERE type = 'S' AND name = 'isseg_user';
GO
```

Luego, usa esta cadena de conexión:
```csharp
"DefaultConnection": "Server=.;Database=ISSEG_DB;User Id=isseg_user;Password=TuContraseñaSegura123!;TrustServerCertificate=true;Encrypt=false;"
```

---

## 🔗 Configuración en Entity Framework Core

### 1. Instalar NuGet Packages

```bash
dotnet add package Microsoft.EntityFrameworkCore
dotnet add package Microsoft.EntityFrameworkCore.SqlServer
dotnet add package Microsoft.EntityFrameworkCore.Design
```

### 2. Configurar DbContext en Program.cs

```csharp
using Microsoft.EntityFrameworkCore;
using YourNamespace.Data;

var builder = WebApplicationBuilder.CreateBuilder(args);

// Obtener la cadena de conexión desde appsettings.json
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Registrar DbContext
builder.Services.AddDbContext<IssegDbContext>(options =>
	options.UseSqlServer(connectionString));

// Resto de la configuración...
var app = builder.Build();
```

### 3. Crear DbContext

```csharp
using Microsoft.EntityFrameworkCore;
using YourNamespace.Models;

namespace YourNamespace.Data
{
	public class IssegDbContext : DbContext
	{
		public IssegDbContext(DbContextOptions<IssegDbContext> options) 
			: base(options)
		{
		}

		// Definir DbSets para cada tabla
		public DbSet<Rol> Roles { get; set; }
		public DbSet<Usuario> Usuarios { get; set; }
		public DbSet<Area> Areas { get; set; }
		public DbSet<Sistema> Sistemas { get; set; }
		public DbSet<Solicitud> Solicitudes { get; set; }
		// ... más DbSets según sea necesario

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
			// Configuraciones adicionales del modelo
		}
	}
}
```

---

## ✅ Verificar la Conexión

### Desde C# (Unit Test)
```csharp
[Fact]
public async Task VerifyDatabaseConnection()
{
	using (var context = new IssegDbContext(options))
	{
		var canConnect = await context.Database.CanConnectAsync();
		Assert.True(canConnect, "No se pudo conectar a la base de datos");

		var rolesCount = await context.Roles.CountAsync();
		Assert.Equal(7, rolesCount);
	}
}
```

### Desde SQL Server Management Studio
```sql
-- Conectar directamente a: .
-- Base de datos: ISSEG_DB
-- Ejecutar:
SELECT COUNT(*) AS TablesCount FROM INFORMATION_SCHEMA.TABLES 
WHERE TABLE_TYPE = 'BASE TABLE'
-- Resultado esperado: 33 tablas
```

### Desde Línea de Comandos
```bash
sqlcmd -S . -E -d ISSEG_DB -Q "SELECT COUNT(*) AS TablesCount FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE = 'BASE TABLE'"
```

---

## 🔒 Seguridad - Recomendaciones

| Aspecto | Recomendación |
|---------|---------------|
| **Autenticación** | Usar Windows Authentication en la red corporativa |
| **Contraseñas** | Usar contraseñas fuertes (min 12 caracteres, mayús, símbolos) |
| **Cifrado** | Habilitar SSL/TLS para conexiones remotas |
| **Permisos** | Usar el principio de menor privilegio |
| **Backup** | Realizar backups automáticos regularmente |
| **Auditoría** | Habilitar auditoría de cambios en datos sensibles |

---

## 📊 Parámetros de Conexión Avanzados

```csharp
"DefaultConnection": "Server=.;Database=ISSEG_DB;Trusted_Connection=true;TrustServerCertificate=true;Encrypt=false;Connection Timeout=30;MultipleActiveResultSets=true;"
```

| Parámetro | Descripción |
|-----------|-------------|
| `Server` | Nombre o IP del servidor SQL |
| `Database` | Nombre de la base de datos |
| `Trusted_Connection` | true = autenticación Windows, false = SQL Auth |
| `TrustServerCertificate` | true = confiar en certificado autofirmado |
| `Encrypt` | true = cifrar conexión, false = sin cifrado |
| `Connection Timeout` | Segundos para intentar conectar (default: 15) |
| `MultipleActiveResultSets` | Permite múltiples resultados activos simultáneos |

---

## 🚀 Próximos Pasos

1. ✅ Base de datos creada: **ISSEG_DB**
2. ⏳ Actualizar `appsettings.json` con la cadena de conexión
3. ⏳ Configurar Entity Framework Core en el proyecto C#
4. ⏳ Crear migraciones (si se usa Code-First)
5. ⏳ Ejecutar migraciones (`dotnet ef database update`)
6. ⏳ Crear datos iniciales (usuarios, roles asignados)
7. ⏳ Probar conexión desde la aplicación

---

## 📞 Solución de Problemas

### Error: "Cannot open database 'ISSEG_DB'"
- **Causa:** Base de datos no existe o nombre incorrecto
- **Solución:** Verificar que `ISSEG_DB` fue creada ejecutando: `SELECT name FROM sys.databases WHERE name = 'ISSEG_DB'`

### Error: "Login failed for user"
- **Causa:** Credenciales incorrectas o usuario no existe
- **Solución:** Verificar usuario y contraseña, o cambiar a autenticación Windows

### Error: "Network or instance-specific error"
- **Causa:** SQL Server no está accesible o está apagado
- **Solución:** Verificar que SQL Server está corriendo: `sqlcmd -S . -E -Q "SELECT @@VERSION"`

### Error: "Connection timeout expired"
- **Causa:** Servidor lento o cortafuegos bloqueando
- **Solución:** Aumentar `Connection Timeout` en la cadena de conexión

---

**Documentación creada:** 27/04/2026  
**Versión:** 1.0  
**Estado:** Listo para implementar
