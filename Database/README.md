# Tarea 3: Base de Datos

## Descripción

Este módulo contiene los scripts y migraciones de la base de datos para el Sistema de Gestión de Solicitudes para la Coordinación de Sistemas Institucionales (SGSPCSI).

## Estructura del proyecto

```
Database/
├── scripts/       # Scripts SQL de creación y configuración
└── migrations/    # Migraciones de base de datos
```

## Contenido

- **scripts/**: Scripts SQL para la creación de tablas, procedimientos almacenados, vistas e índices.
- **migrations/**: Archivos de migración para el control de versiones del esquema de la base de datos.

## Instrucciones de configuración

1. Crear la base de datos en el servidor correspondiente.
2. Ejecutar los scripts en orden desde la carpeta `scripts/`.
3. Aplicar las migraciones desde la carpeta `migrations/`.
