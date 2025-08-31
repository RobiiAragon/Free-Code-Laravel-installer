using System;
using System.Diagnostics;
using System.Threading.Tasks;

public class LaravelCommands
{
    public bool RunCommand(string command, string args, string workingDir)
    {
        try
        {
            var psi = new ProcessStartInfo
            {
                FileName = command,
                Arguments = args,
                WorkingDirectory = workingDir,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };
            using (var process = Process.Start(psi))
            {
                process.WaitForExit();
                return process.ExitCode == 0;
            }
        }
        catch
        {
            return false;
        }
    }

    public bool CreateProject(string name, string path, string version = null)
    {
        string args = version != null && version != "Última versión"
            ? $"create-project laravel/laravel:{version} {name}"
            : $"create-project laravel/laravel {name}";
        return RunCommand("composer", args, path);
    }

    public void Serve(string projectPath)
    {
        Task.Run(() =>
        {
            var psi = new ProcessStartInfo
            {
                FileName = "php",
                Arguments = "artisan serve",
                WorkingDirectory = projectPath,
                UseShellExecute = true
            };
            Process.Start(psi);
        });
    }

    public bool Migrate(string projectPath)
    {
        return RunCommand("php", "artisan migrate", projectPath);
    }

    public bool MakeController(string projectPath, string name)
    {
        return RunCommand("php", $"artisan make:controller {name}", projectPath);
    }

    public bool MakeModel(string projectPath, string name)
    {
        return RunCommand("php", $"artisan make:model {name}", projectPath);
    }

    public bool MakeMigration(string projectPath, string name)
    {
        return RunCommand("php", $"artisan make:migration {name}", projectPath);
    }
}