-- ============================================================
-- SCRIPT DE VERIFICACION DE BASE DE DATOS ISSEG_DB
-- Motor: SQL Server 2022+
-- Proposito: Validar que la base de datos está completamente creada
-- ============================================================

USE [ISSEG_DB]
GO

PRINT '=========================================='
PRINT 'VERIFICACION DE BASE DE DATOS ISSEG_DB'
PRINT '=========================================='
PRINT ''

-- 1. Verificar que la base de datos existe
PRINT '1. ESTADO DE LA BASE DE DATOS'
PRINT '-------------------------------------------'
SELECT 
	DB_NAME() AS [Base de Datos],
	CONVERT(VARCHAR(30), SYSDATETIME(), 121) AS [Fecha y Hora Actual]
GO

-- 2. Contar todas las tablas
PRINT ''
PRINT '2. CONTEO DE TABLAS'
PRINT '-------------------------------------------'
SELECT 
	COUNT(*) AS [Total de Tablas]
FROM INFORMATION_SCHEMA.TABLES
WHERE TABLE_TYPE = 'BASE TABLE' AND TABLE_SCHEMA = 'dbo'
GO

-- 3. Listar todas las tablas
PRINT ''
PRINT '3. LISTA DE TABLAS'
PRINT '-------------------------------------------'
SELECT 
	ROW_NUMBER() OVER (ORDER BY TABLE_NAME) AS [#],
	TABLE_NAME AS [Tabla]
FROM INFORMATION_SCHEMA.TABLES
WHERE TABLE_TYPE = 'BASE TABLE' AND TABLE_SCHEMA = 'dbo'
ORDER BY TABLE_NAME
GO

-- 4. Verificar datos en catálogos
PRINT ''
PRINT '4. DATOS EN CATALOGOS'
PRINT '-------------------------------------------'
DECLARE @rol_count INT
DECLARE @tipo_solicitud_count INT
DECLARE @estado_solicitud_count INT
DECLARE @prioridad_count INT
DECLARE @area_count INT
DECLARE @sistema_count INT
DECLARE @menu_count INT

SELECT @rol_count = COUNT(*) FROM rol
SELECT @tipo_solicitud_count = COUNT(*) FROM tipo_solicitud
SELECT @estado_solicitud_count = COUNT(*) FROM estado_solicitud
SELECT @prioridad_count = COUNT(*) FROM prioridad_solicitud
SELECT @area_count = COUNT(*) FROM area
SELECT @sistema_count = COUNT(*) FROM sistema
SELECT @menu_count = COUNT(*) FROM menu_opcion

SELECT 
	'Roles' AS [Catálogo], @rol_count AS [Cantidad] UNION ALL
SELECT 'Tipos de Solicitud', @tipo_solicitud_count UNION ALL
SELECT 'Estados de Solicitud', @estado_solicitud_count UNION ALL
SELECT 'Prioridades', @prioridad_count UNION ALL
SELECT 'Áreas Organizacionales', @area_count UNION ALL
SELECT 'Sistemas', @sistema_count UNION ALL
SELECT 'Opciones de Menú', @menu_count
ORDER BY [Catálogo]
GO

-- 5. Verificar integridad referencial
PRINT ''
PRINT '5. VERIFICAR INTEGRIDAD REFERENCIAL'
PRINT '-------------------------------------------'
SELECT 
	CONSTRAINT_NAME AS [Restricción],
	TABLE_NAME AS [Tabla],
	CONSTRAINT_TYPE AS [Tipo]
FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS
WHERE CONSTRAINT_TYPE IN ('PRIMARY KEY', 'FOREIGN KEY', 'UNIQUE', 'CHECK')
ORDER BY TABLE_NAME, CONSTRAINT_TYPE DESC
GO

-- 6. Verificar índices
PRINT ''
PRINT '6. VERIFICAR INDICES'
PRINT '-------------------------------------------'
SELECT 
	OBJECT_NAME(i.object_id) AS [Tabla],
	i.name AS [Índice],
	CASE WHEN i.is_primary_key = 1 THEN 'Clave Primaria'
		 WHEN i.is_unique = 1 THEN 'Único'
		 ELSE 'Índice Normal' END AS [Tipo]
FROM sys.indexes i
WHERE OBJECT_DB_ID(i.object_id) = DB_ID() AND i.name IS NOT NULL
ORDER BY [Tabla], i.is_primary_key DESC, i.name
GO

-- 7. Verificar procedimientos almacenados (si existen)
PRINT ''
PRINT '7. PROCEDIMIENTOS ALMACENADOS'
PRINT '-------------------------------------------'
SELECT 
	COUNT(*) AS [Total Procedimientos]
FROM INFORMATION_SCHEMA.ROUTINES
WHERE ROUTINE_TYPE = 'PROCEDURE'
GO

-- 8. Resumen de columnas por tabla
PRINT ''
PRINT '8. RESUMEN DE COLUMNAS'
PRINT '-------------------------------------------'
SELECT 
	TABLE_NAME AS [Tabla],
	COUNT(*) AS [Numero de Columnas]
FROM INFORMATION_SCHEMA.COLUMNS
WHERE TABLE_SCHEMA = 'dbo'
GROUP BY TABLE_NAME
ORDER BY TABLE_NAME
GO

-- 9. Información de espacio utilizado
PRINT ''
PRINT '9. ESPACIO UTILIZADO'
PRINT '-------------------------------------------'
SELECT 
	'Base de Datos' AS [Tipo],
	SUM(size) * 8 / 1024 AS [MB Asignados]
FROM sys.database_files
UNION ALL
SELECT 
	'Log de Transacciones',
	SUM(size) * 8 / 1024
FROM sys.database_files
WHERE type_desc = 'LOG'
GO

-- 10. Mensaje de finalización
PRINT ''
PRINT '=========================================='
PRINT 'VERIFICACION COMPLETADA EXITOSAMENTE'
PRINT '=========================================='
PRINT ''
PRINT 'La base de datos ISSEG_DB está lista para usar.'
PRINT 'Próximos pasos:'
PRINT '  1. Crear usuarios para la aplicación'
PRINT '  2. Configurar las credenciales de base de datos'
PRINT '  3. Actualizar la cadena de conexión en appsettings.json'
GO
