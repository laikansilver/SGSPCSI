# Tarea 2: Sitio Web (Angular)

**Responsable:** Jacqueline  
**Herramienta:** Visual Studio Code

## Descripción

Este módulo contiene el desarrollo del sitio web para el Sistema de Gestión de Solicitudes para la Coordinación de Sistemas Institucionales (SGSPCSI), desarrollado con Angular.

## Estructura del proyecto

```
Frontend/
└── src/
    ├── app/
    │   ├── components/    # Componentes reutilizables
    │   ├── pages/         # Páginas de la aplicación
    │   ├── services/      # Servicios para consumir la API
    │   └── models/        # Interfaces y modelos TypeScript
    ├── assets/            # Recursos estáticos (imágenes, estilos)
    └── environments/      # Variables de entorno
```

## Tecnologías

- Angular
- TypeScript
- HTML / CSS

## Instrucciones de configuración

1. Abrir la carpeta `Frontend/` en Visual Studio Code.
2. Instalar las dependencias:
   ```bash
   npm install
   ```
3. Configurar la URL de la API en `src/environments/environment.ts`.
4. Ejecutar la aplicación en modo desarrollo:
   ```bash
   ng serve
   ```
5. Abrir el navegador en `http://localhost:4200`.
