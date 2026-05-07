# 🔗 Guía de Conexión entre Base de Datos y API

## Flujo de Conexión

```
┌─────────────────────────────────────────────────────────────┐
│                      CLIENTE (Frontend)                     │
│                                                             │
│         Angular en http://localhost:3000             	      │
└──────────────────────┬──────────────────────────────────────┘
		       │
		       │ HTTP Requests
		       │
┌──────────────────────▼──────────────────────────────────────┐
│                    API (C# .NET)                            │
│           localhost:5000 / localhost:5173                   │
│                                                             │
│  📄 Program.cs  (Línea 12-13)                               │
│  ├─ Configura Entity Framework Core                         │
│  └─ Lee la cadena de conexión de appsettings.json           │
│                                                             │
│  builder.Services.AddDbContext<IssegDbContext>(options =>   │
│      options.UseSqlServer(                                  │
│          builder.Configuration                              │
│              .GetConnectionString("DefaultConnection")      │
│      )                                                      │
│  );                                                         │
└──────────────────────┬──────────────────────────────────────┘
					   │
					   │ String de Conexión
					   │
┌──────────────────────▼──────────────────────────────────────┐
│           📋 appsettings.json (API\appsettings.json)        │
│                                                               │
│  "ConnectionStrings": {                                     │
│    "DefaultConnection": "Server=.;                          │
│                          Database=SGSPCSI_DB;              │
│                          Trusted_Connection=true;           │
│                          TrustServerCertificate=true;       │
│                          MultipleActiveResultSets=true;     │
│                          Encrypt=false;"                    │
│  }                                                            │
└──────────────────────┬──────────────────────────────────────┘
					   │
					   │ DbContext
					   │
┌──────────────────────▼──────────────────────────────────────┐
│          🗄️  IssegDbContext (API\Data\IssegDbContext.cs)    │
│                                                               │
│  public class IssegDbContext : DbContext                    │
│  {                                                            │
│      // DbSets que mapean a las tablas de SQL Server        │
│      public DbSet<Usuario> Usuarios { get; set; }           │
│      public DbSet<Solicitud> Solicitudes { get; set; }      │
│      public DbSet<Area> Areas { get; set; }                 │
│      ...más 30 tablas...                                     │
│  }                                                            │
└──────────────────────┬──────────────────────────────────────┘
					   │
					   │ SQL Queries
					   │
┌──────────────────────▼──────────────────────────────────────┐
│             🗄️  SQL Server 2022 (SGSPCSI_DB)               │
│                                                               │
│  ├─ 33 Tablas Normalizadas                                  │
│  ├─ usuario, rol, usuario_rol, ...                          │
│  ├─ solicitud, estado_solicitud, ...                        │
│  └─ Datos Iniciales Cargados                                │
│                                                               │
│  Ubicación: Server=. (LocalHost)                            │
│  Base de Datos: SGSPCSI_DB                                  │
│  Autenticación: Windows Auth (Trusted Connection)           │
└─────────────────────────────────────────────────────────────┘
```

## 📁 Archivos Clave de Configuración

### 1️⃣ **Program.cs** (Punto de entrada)

- **Ubicación:** `API\Program.cs`
- **Líneas:** 12-13
- **Función:** Registra el DbContext de Entity Framework Core
- **Código:**

```csharp
builder.Services.AddDbContext<IssegDbContext>(options =>
	options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
```

### 2️⃣ **appsettings.json** (Configuración de Conexión)

- **Ubicación:** `API\appsettings.json`
- **Función:** Define la cadena de conexión a SQL Server
- **Contenido:**

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=.;Database=SGSPCSI_DB;Trusted_Connection=true;..."
}
```

**Parámetros de la cadena de conexión:**

| Parámetro                   | Valor          | Significado                                      |
| ---------------------------- | -------------- | ------------------------------------------------ |
| `Server`                   | `.`          | Servidor local                                   |
| `Database`                 | `SGSPCSI_DB` | Nombre de la base de datos                       |
| `Trusted_Connection`       | `true`       | Autenticación Windows (sin usuario/contraseña) |
| `TrustServerCertificate`   | `true`       | Confía en certificados SSL                      |
| `MultipleActiveResultSets` | `true`       | Permite múltiples consultas simultáneas        |
| `Encrypt`                  | `false`      | No encripta la conexión (solo local)            |

### 3️⃣ **IssegDbContext.cs** (Modelo de Datos)

- **Ubicación:** `API\Data\IssegDbContext.cs`
- **Función:** Define el mapeo entre objetos C# y tablas SQL Server
- **Contiene:** 33 DbSets que representan las 33 tablas

## 🔄 Cómo Funciona la Consulta

Cuando se hace una request a la API:

```
1. Cliente → GET /api/usuarios
				↓
2. Controller (UsuariosController.cs)
				↓
3. Service (UsuarioService.cs) 
				↓
4. Repository (UsuarioRepository.cs)
				↓
5. DbContext (IssegDbContext.cs) 
   Usa: await _context.Usuarios.ToListAsync()
				↓
6. Program.cs → Lee appsettings.json
				↓
7. Conexión a SQL Server
				↓
8. Ejecuta: SELECT * FROM usuario
				↓
9. SQL Server retorna datos
				↓
10. JSON Response → Cliente
```

## ✅ Verificación de Conexión

Para verificar que todo está conectado correctamente:

### En Visual Studio:

1. Abre `Program.cs`
2. Verifica que `AddDbContext<IssegDbContext>` esté configurado
3. Ejecuta la API (F5)
4. En swagger (http://localhost:5000/swagger), prueba los endpoints

### En SQL Server Management Studio:

1. Conéctate a `.` (local)
2. Verifica que exista la base de datos `SGSPCSI_DB`
3. Expande → Tablas
4. Deberías ver 33 tablas creadas

## 🐛 Troubleshooting

| Problema                     | Causa                                | Solución                                 |
| ---------------------------- | ------------------------------------ | ----------------------------------------- |
| "Cannot connect to database" | Cadena de conexión incorrecta       | Verifica `appsettings.json`             |
| "Database not found"         | SGSPCSI_DB no existe                 | Ejecuta `Database\crear_base_datos.bat` |
| "Login failed"               | Autenticación Windows deshabilitada | Verifica credenciales en SQL Server       |
| Connection timeout           | Firewall bloqueando puerto 1433      | Abre puerto en Windows Firewall           |

## 📞 Resumen

- **Configuración:** `appsettings.json`
- **Registro de DbContext:** `Program.cs` línea 12-13
- **Mapeo de Tablas:** `IssegDbContext.cs`
- **Base de Datos:** `SGSPCSI_DB` en SQL Server local
- **Autenticación:** Windows Authentication (usuario actual)
