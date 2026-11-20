<div align="center">

# Instalador visual de Laravel

**Crea y administra proyectos de Laravel en Windows sin escribir un solo comando.**

![C%23](https://img.shields.io/badge/C%23-512BD4?style=flat-square&logo=dotnet&logoColor=white) ![.NET 8](https://img.shields.io/badge/.NET%208-512BD4?style=flat-square) ![Windows](https://img.shields.io/badge/Windows-0078D6?style=flat-square&logo=windows&logoColor=white) [![CI](https://github.com/RobiiAragon/Free-Code-Laravel-installer/actions/workflows/ci.yml/badge.svg)](https://github.com/RobiiAragon/Free-Code-Laravel-installer/actions/workflows/ci.yml) ![License MIT](https://img.shields.io/badge/License%20MIT-2ea44f?style=flat-square)

[English](README.md) · **Español**

</div>

---

Una app pequeña en WinForms que convierte en botones los comandos de `composer` y `php artisan` que usas a diario. Útil cuando vas empezando con Laravel o cuando lo enseñas.

## Funciones

- 🆕 **Proyecto nuevo**: eliges nombre, carpeta y versión de Laravel (la más reciente, 12, 11, 10, 9 u 8).
- ▶️ **Servidor**: arranca `php artisan serve` en http://127.0.0.1:8000.
- 🗃️ **Migraciones**: corre `php artisan migrate`.
- 🧩 **Generadores**: `make:controller`, `make:model` y `make:migration` desde una caja de texto.
- 🔒 **Entrada segura**: los nombres se validan antes de llegar a `cmd.exe`, así que caracteres como `&`, `|` o comillas no pueden inyectar otros comandos.
- 🪟 La ventana no se congela mientras Composer trabaja, y si algo falla te muestra el error real.

## Requisitos

- Windows 10/11
- [PHP](https://windows.php.net/download/) y [Composer](https://getcomposer.org/download/) en tu `PATH`
- [.NET 8 SDK](https://dotnet.microsoft.com/download) para compilar

## Compilar

```powershell
git clone https://github.com/RobiiAragon/Free-Code-Laravel-installer.git
cd Free-Code-Laravel-installer
./build-windows.ps1          # .exe de un solo archivo en ./Program
```

O abre `Free-Code-Laravel-installer.sln` en Visual Studio y presiona <kbd>F5</kbd>.

## Pruebas

Multiplataforma, no necesitas Windows:

```bash
dotnet test LaravelWinForms.Tests
```

## Estructura

```
LaravelWinForms/
  LaravelCommands.cs   envuelve composer / artisan (usa cmd.exe y captura la salida)
  Program.cs           punto de entrada
  UI/MainForm*.cs      la ventana
  InputValidator.cs    valida los nombres antes de pasarlos a la consola
LaravelWinForms.Tests/ pruebas xUnit del validador
build-windows.ps1      build de release (autocontenido, un solo archivo)
```

## Licencia

[MIT](LICENSE)

---

Hecho por **[Jesús Aragón](https://github.com/RobiiAragon)** · [Portafolio](https://robiiaragon.github.io/portfolio/)
