# Tarea 1: API RESTful

**Responsable:** Eduardo  
**Herramienta:** Visual Studio Community

## Descripción

Este módulo contiene el desarrollo de la API RESTful para el Sistema de Gestión de Solicitudes para la Coordinación de Sistemas Institucionales (SGSPCSI).

## Estructura del proyecto

```
API/
├── Controllers/       # Controladores de la API
├── Models/            # Modelos de datos
├── Services/          # Lógica de negocio
├── Repositories/      # Acceso a datos (patrón repositorio)
├── DTOs/              # Data Transfer Objects
└── Migrations/        # Migraciones de base de datos
```

## Tecnologías

- ASP.NET Core Web API
- Patrón de Arquitectura de Repositorio
- Entity Framework Core

## Instrucciones de configuración

1. Abrir la solución en Visual Studio Community.
2. Restaurar los paquetes NuGet.
3. Configurar la cadena de conexión en `appsettings.json`.
4. Ejecutar las migraciones de base de datos.
5. Compilar y ejecutar el proyecto.
