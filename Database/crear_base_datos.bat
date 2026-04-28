@echo off
REM ============================================================
REM Script de Automatizacion: Crear Base de Datos ISSEG_DB
REM Motor: SQL Server 2022+
REM Descripcion: Ejecuta todos los scripts SQL en orden
REM ============================================================

setlocal enabledelayedexpansion

cls
echo.
echo ============================================================
echo  CREACION AUTOMATICA DE BASE DE DATOS ISSEG_DB
echo  SQL Server 2022+
echo ============================================================
echo.

REM Verificar que sqlcmd está disponible
where sqlcmd >nul 2>nul
if errorlevel 1 (
	echo ERROR: sqlcmd no se encuentra en el PATH
	echo Por favor instale SQL Server Command-Line Tools
	echo https://learn.microsoft.com/en-us/sql/tools/sqlcmd/go-sqlcmd-utility
	pause
	exit /b 1
)

REM Obtener el directorio actual
cd /d "%~dp0"

echo.
echo [1/3] Eliminando base de datos anterior si existe...
sqlcmd -S . -E -Q "IF DB_ID(N'ISSEG_DB') IS NOT NULL BEGIN ALTER DATABASE [ISSEG_DB] SET SINGLE_USER WITH ROLLBACK IMMEDIATE; DROP DATABASE [ISSEG_DB]; END;" -C -m -1
if errorlevel 1 (
	echo ERROR: No se pudo eliminar la base de datos anterior
	pause
	exit /b 1
)
echo [OK] Base de datos anterior eliminada (o no existía)

echo.
echo [2/3] Creando estructura de base de datos...
sqlcmd -S . -E -i "01_crear_base_datos.sql" -C -m -1
if errorlevel 1 (
	echo ERROR: Fallo en la creacion de la estructura
	pause
	exit /b 1
)
echo [OK] Estructura de base de datos creada

echo.
echo [3/3] Cargando datos iniciales...
sqlcmd -S . -E -i "02_datos_iniciales.sql" -C -m -1
if errorlevel 1 (
	echo ERROR: Fallo en la carga de datos iniciales
	pause
	exit /b 1
)
echo [OK] Datos iniciales cargados

echo.
echo ============================================================
echo  VERIFICANDO INTEGRIDAD DE LA BASE DE DATOS...
echo ============================================================
echo.

REM Verificación rápida
sqlcmd -S . -E -Q "USE ISSEG_DB; DECLARE @tablas INT; SELECT @tablas = COUNT(*) FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE = 'BASE TABLE'; DECLARE @roles INT; SELECT @roles = COUNT(*) FROM rol; SELECT 'RESULTADO' AS [Tipo], 'EXITOSO' AS [Estado] WHERE @tablas = 33 AND @roles = 7 UNION ALL SELECT 'Tablas Creadas', CAST(@tablas AS VARCHAR(10)) UNION ALL SELECT 'Roles Cargados', CAST(@roles AS VARCHAR(10));" -C -m -1

if errorlevel 0 (
	echo.
	echo ============================================================
	echo  ✓ BASE DE DATOS ISSEG_DB CREADA EXITOSAMENTE
	echo ============================================================
	echo.
	echo Informacion:
	echo  - Servidor: . (Local)
	echo  - Base de Datos: ISSEG_DB
	echo  - Tablas: 33
	echo  - Datos Iniciales: Cargados
	echo.
	echo Proximos pasos:
	echo  1. Verificar appsettings.json
	echo  2. Ejecutar: dotnet build
	echo  3. Probar conexion desde la aplicacion
	echo.
	echo Para ver detalles completos, ejecute:
	echo  sqlcmd -S . -E -i "03_verificar_base_datos.sql" -C -m -1
	echo.
	pause
) else (
	echo.
	echo ERROR: Ocurrio un error durante la verificacion
	pause
	exit /b 1
)

endlocal
