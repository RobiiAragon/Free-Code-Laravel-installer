<div align="center">

# Laravel Visual Installer

**Create and manage Laravel projects on Windows without typing a single command.**

![C%23](https://img.shields.io/badge/C%23-512BD4?style=flat-square&logo=dotnet&logoColor=white) ![.NET 8](https://img.shields.io/badge/.NET%208-512BD4?style=flat-square) ![Windows](https://img.shields.io/badge/Windows-0078D6?style=flat-square&logo=windows&logoColor=white) [![CI](https://github.com/RobiiAragon/Free-Code-Laravel-installer/actions/workflows/ci.yml/badge.svg)](https://github.com/RobiiAragon/Free-Code-Laravel-installer/actions/workflows/ci.yml) ![License MIT](https://img.shields.io/badge/License%20MIT-2ea44f?style=flat-square)

**English** · [Español](README.es.md)

</div>

---

A small WinForms app that wraps the everyday `composer` and `php artisan` commands in buttons — handy when you're starting with Laravel or teaching it.

## Features

- 🆕 **New project** — pick a name, a folder and a Laravel version (latest, 12, 11, 10, 9 or 8).
- ▶️ **Run server** — starts `php artisan serve` at http://127.0.0.1:8000.
- 🗃️ **Migrations** — runs `php artisan migrate`.
- 🧩 **Generators** — `make:controller`, `make:model` and `make:migration` from a text box.
- 🔒 **Safe input** — names are validated before they reach `cmd.exe`, so characters like `&`, `|` or quotes can't inject extra commands.
- 🪟 The window stays responsive while Composer works, and shows the real error output if something fails.

## Requirements

- Windows 10/11
- [PHP](https://windows.php.net/download/) and [Composer](https://getcomposer.org/download/) available in your `PATH`
- [.NET 8 SDK](https://dotnet.microsoft.com/download) to build

## Build

```powershell
git clone https://github.com/RobiiAragon/Free-Code-Laravel-installer.git
cd Free-Code-Laravel-installer
./build-windows.ps1          # single-file .exe in ./Program
```

Run the tests (cross-platform, no Windows needed):

```bash
dotnet test LaravelWinForms.Tests
```

Or open `Free-Code-Laravel-installer.sln` in Visual Studio and press <kbd>F5</kbd>.

## Project structure

```
LaravelWinForms/
  LaravelCommands.cs   wraps composer / artisan (runs through cmd.exe, captures output)
  InputValidator.cs    validates names before they reach the shell
  Program.cs           entry point
  UI/MainForm*.cs      the window
LaravelWinForms.Tests/ xUnit tests for the validator
build-windows.ps1      release build (self-contained, single file)
```

## License

[MIT](LICENSE)

---

Made by **[Jesús Aragón](https://github.com/RobiiAragon)** · [Portfolio](https://robiiaragon.github.io/portfolio/)
