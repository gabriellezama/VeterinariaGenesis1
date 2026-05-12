@echo off
title Veterinaria Genesis - Iniciando Sistema...
color 0A

echo ================================================
echo    VETERINARIA GENESIS - Sistema de Gestion
echo ================================================
echo.
echo Iniciando servidores... Por favor espera.
echo.

:: Iniciar API en una ventana separada
start "API - Veterinaria Genesis" cmd /k "cd /d C:\Users\YASSERGABRIELLEZAMAV\Downloads\MongoDb && dotnet run --project VeterinariaGenesis.Api --launch-profile https"

:: Esperar 8 segundos para que el API inicie primero
echo Iniciando API... (espera 8 segundos)
timeout /t 8 /nobreak > nul

:: Iniciar Cliente en otra ventana
start "CLIENTE - Veterinaria Genesis" cmd /k "cd /d C:\Users\YASSERGABRIELLEZAMAV\Downloads\MongoDb && dotnet run --project VeterinariaGenesis.Client --launch-profile https"

:: Esperar que el cliente compile
echo Iniciando Cliente web... (espera 15 segundos)
timeout /t 15 /nobreak > nul

:: Abrir el navegador
echo Abriendo navegador...
start https://localhost:7197

echo.
echo ================================================
echo  Sistema iniciado! Abre: https://localhost:7197
echo  Usuario: Administrador
echo  Contrasena: 123456
echo ================================================
echo.
echo Puedes cerrar esta ventana.
pause
