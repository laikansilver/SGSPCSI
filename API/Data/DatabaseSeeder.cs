using System.Security.Cryptography;
using Microsoft.Extensions.Logging;
using SGSPCSI.API.Models;

namespace SGSPCSI.API.Data
{
    public static class DatabaseSeeder
    {
        public static async Task SeedAsync(IssegDbContext db, ILogger logger)
        {
            if (db.Roles.Any())
            {
                logger.LogInformation("Roles already exist, skipping seeding.");
                return;
            }

            logger.LogInformation("Seeding initial roles and admin user...");

            var roles = new List<Rol>
            {
                new Rol { Clave = "ADMIN", Nombre = "Administrador", Descripcion = "Rol administrador", Activo = true, FechaCreacion = DateTime.UtcNow },
                new Rol { Clave = "USER", Nombre = "Usuario", Descripcion = "Rol usuario estándar", Activo = true, FechaCreacion = DateTime.UtcNow }
            };

            db.Roles.AddRange(roles);
            await db.SaveChangesAsync();

            // Crear usuario admin
            var adminCorreo = "admin@isseg.local";
            if (!db.Usuarios.Any(u => u.CorreoInstitucional == adminCorreo))
            {
                var admin = new Usuario
                {
                    NombrePila = "Admin",
                    ApellidoPaterno = "ISSEG",
                    CorreoInstitucional = adminCorreo,
                    Activo = true,
                    FechaCreacion = DateTime.UtcNow
                };

                db.Usuarios.Add(admin);
                await db.SaveChangesAsync();

                // Generar credenciales PBKDF2-SHA256
                var password = "Admin123!";
                var salt = RandomNumberGenerator.GetBytes(32);
                var iterations = 100_000;
                var hash = Rfc2898DeriveBytes.Pbkdf2(password, salt, iterations, HashAlgorithmName.SHA256, 32);

                var cred = new UsuarioCredencial
                {
                    UsuarioId = admin.UsuarioId,
                    LoginUsuario = "admin",
                    PasswordHash = hash,
                    PasswordSalt = salt,
                    AlgoritmoHash = "PBKDF2-SHA256",
                    Iteraciones = iterations,
                    FechaActualizacion = DateTime.UtcNow,
                    RequiereCambioPassword = false
                };

                db.UsuariosCredenciales.Add(cred);

                // Asignar rol administrador
                var adminRole = db.Roles.FirstOrDefault(r => r.Clave == "ADMIN");
                if (adminRole != null)
                {
                    var ur = new UsuarioRol
                    {
                        UsuarioId = admin.UsuarioId,
                        RolId = adminRole.RolId,
                        Activo = true,
                        FechaAsignacion = DateTime.UtcNow
                    };
                    db.UsuariosRoles.Add(ur);
                }

                await db.SaveChangesAsync();
                logger.LogInformation("Admin user created: login=admin, password=Admin123!");
            }
            else
            {
                logger.LogInformation("Admin user already exists, skipping user creation.");
            }
        }
    }
}
