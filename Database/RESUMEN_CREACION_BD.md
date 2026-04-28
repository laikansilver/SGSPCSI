# Resumen de Creación de Base de Datos ISSEG_DB

## Estado: ✅ COMPLETADO

**Fecha de Creación:** 27 de Abril de 2026  
**Motor de Base de Datos:** Microsoft SQL Server 2025 (RTM-GDR)  
**Nombre de la Base de Datos:** ISSEG_DB

---

## 📊 Estructura Creada

### Tablas Creadas: 33
La base de datos incluye las siguientes categorías de tablas:

#### 1. **Catálogos Base (6 tablas)**
- `rol` - Roles de seguridad del sistema
- `usuario` - Usuarios del sistema
- `area` - Estructura organizacional
- `sistema` - Sistemas disponibles
- `tipo_solicitud` - Tipos de solicitudes
- `prioridad_solicitud` - Niveles de prioridad
- `estado_solicitud` - Estados posibles de solicitudes

#### 2. **Relaciones de Seguridad y Organización (4 tablas)**
- `usuario_rol` - Asignación de roles a usuarios
- `usuario_area` - Asignación de usuarios a áreas
- `area_sistema` - Relación entre áreas y sistemas
- `sistema_desarrollador` - Participación de desarrolladores en sistemas

#### 3. **Gestión de Solicitudes (8 tablas)**
- `solicitud` - Solicitudes principales
- `solicitud_desarrollador` - Asignación de desarrolladores a solicitudes
- `solicitud_historial_estado` - Histórico de cambios de estado
- `solicitud_aprobacion` - Aprobaciones de solicitudes
- `solicitud_comentario` - Comentarios en solicitudes
- `solicitud_adjunto` - Archivos adjuntos
- `notificacion` - Notificaciones del sistema
- `tipo_modificacion` - Tipos de modificaciones

#### 4. **Autenticación y Menú (4 tablas)**
- `usuario_credencial` - Credenciales de acceso
- `menu_opcion` - Opciones del menú
- `rol_menu_opcion` - Permisos de roles en opciones de menú
- `solicitud_modificacion` - Detalles de solicitudes de modificación

#### 5. **Especificaciones Técnicas (1 tabla)**
- `solicitud_requerimiento_tecnico` - Requisitos técnicos de solicitudes

#### 6. **Tareas, Actividades y Proyectos (10 tablas)**
- `tarea_desarrollo` - Tareas de desarrollo
- `tarea_desarrollo_asignacion` - Asignación de tareas
- `actividad_reciente` - Registro de actividades
- `proyecto` - Proyectos
- `proyecto_solicitud` - Vinculación solicitud-proyecto
- `proyecto_miembro` - Miembros del proyecto
- `documento_proyecto` - Documentación del proyecto
- `evento_calendario` - Eventos del calendario
- `evento_participante` - Participantes en eventos

---

## 📋 Datos Iniciales Cargados

### Catálogos Maestros
| Concepto | Cantidad |
|----------|----------|
| Roles de Seguridad | 7 |
| Tipos de Solicitud | 7 |
| Estados de Solicitud | 7 |
| Niveles de Prioridad | 4 |
| Tipos de Modificación | 6 |
| Áreas Organizacionales | 7 |
| Sistemas Disponibles | 6 |
| Opciones de Menú | 8 |

### Roles Disponibles
1. **ADMIN** - Administrador (Acceso total)
2. **DIRECTOR** - Director/a (Dirección y aprobaciones)
3. **JEFE** - Jefe/a de Área (Gestión del área)
4. **COORDINADOR** - Coordinador/a (Coordinación)
5. **DESARROLLADOR** - Desarrollador/a (Ejecución técnica)
6. **USUARIO** - Usuario Final (Solicitudes)
7. **AUDITOR** - Auditor/a (Auditoría)

### Estructura Organizacional
- Dirección General (nivel 1)
  - Dirección de Tecnología e Innovación (nivel 2)
	- Coordinación de Desarrollo de Software (nivel 3)
	- Coordinación de Infraestructura TI (nivel 3)
	- Coordinación de Soporte Operativo (nivel 3)
  - Dirección Administrativa (nivel 2)
	- Coordinación de Recursos Humanos (nivel 3)

---

## 🔧 Características de Seguridad

- **Autenticación:** PBKDF2 con 100,000 iteraciones
- **Almacenamiento de Contraseñas:** Hash con salt
- **Bloqueo de Cuenta:** Tras intentos fallidos de acceso
- **Control de Acceso:** Basado en roles y permisos por menú

---

## 📈 Índices de Apoyo Operativo

Se crearon 9 índices para optimizar las consultas más frecuentes:
- Usuarios por área activa
- Sistemas por área activa
- Desarrolladores por usuario activo
- Solicitudes por estado y fecha
- Solicitudes por área y estado
- Historial de solicitudes por fecha
- Tareas por solicitud y estado
- Notificaciones no leídas
- Actividades recientes por fecha

---

## ✅ Verificación de Integridad

- ✓ Base de datos creada exitosamente
- ✓ 33 tablas del esquema
- ✓ Restricciones de integridad referencial aplicadas
- ✓ Índices creados y operativos
- ✓ Datos iniciales cargados correctamente
- ✓ Valores por defecto configurados

---

## 📝 Próximos Pasos

1. Crear usuarios de base de datos para la aplicación
2. Configurar permisos de base de datos
3. Crear usuarios iniciales en la tabla `usuario`
4. Configurar las credenciales en `usuario_credencial`
5. Asignar roles a usuarios mediante `usuario_rol`
6. Conectar la aplicación C# a la base de datos

---

## 📌 Notas Importantes

- La base de datos usa SQL Server 2025 (compatible con scripts SQL Server 2022)
- Todas las tablas tienen campos de auditoría (`fecha_creacion`, `fecha_actualizacion`, etc.)
- El sistema está preparado para multitenancy y auditoría completa
- Las restricciones CHECK garantizan datos consistentes (ej: `nivel >= 1` en áreas)

---

**Generado:** 27/04/2026  
**Por:** Asistente de Configuración de Base de Datos  
**Estado:** ✅ LISTO PARA USAR
