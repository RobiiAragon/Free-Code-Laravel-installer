<div align="center">

# Laravel Visual Installer

**Create and manage Laravel projects on Windows without typing a single command.**

![C%23](https://img.shields.io/badge/C%23-512BD4?style=flat-square&logo=dotnet&logoColor=white) ![.NET 8](https://img.shields.io/badge/.NET%208-512BD4?style=flat-square) ![Windows](https://img.shields.io/badge/Windows-0078D6?style=flat-square&logo=windows&logoColor=white) ![License MIT](https://img.shields.io/badge/License%20MIT-2ea44f?style=flat-square)

**English** · [Español](README.es.md)

</div>

---

A small WinForms app that wraps the everyday `composer` and `php artisan` commands in buttons — handy when you're starting with Laravel or teaching it.

## Features

- 🆕 **New project** — pick a name, a folder and a Laravel version (latest, 12, 11, 10, 9 or 8).
- ▶️ **Run server** — starts `php artisan serve` at http://127.0.0.1:8000.
- 🗃️ **Migrations** — runs `php artisan migrate`.
- 🧩 **Generators** — `make:controller` and `make:model` from a text box.
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

Or open `Free-Code-Laravel-installer.sln` in Visual Studio and press <kbd>F5</kbd>.

## Project structure

```
LaravelWinForms/
  LaravelCommands.cs   wraps composer / artisan (runs through cmd.exe, captures output)
  Program.cs           entry point
  UI/MainForm*.cs      the window
build-windows.ps1      release build (self-contained, single file)
```

## License

[MIT](LICENSE)

---

Made by **[Jesús Aragón](https://github.com/RobiiAragon)** · [Portfolio](https://robiiaragon.github.io/portfolio/)
