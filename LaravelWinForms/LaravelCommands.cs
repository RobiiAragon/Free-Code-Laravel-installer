using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace LaravelWinForms
{
    public class LaravelCommands
    {
        public const string Latest = "Latest";

        /// <summary>Last output (stdout + stderr) of RunCommand, useful to show errors in the UI.</summary>
        public string LastOutput { get; private set; } = "";

        public bool RunCommand(string command, string args, string workingDir)
        {
            try
            {
                // composer / php are often .bat/.cmd shims on Windows, so go through cmd.exe.
                var psi = new ProcessStartInfo
                {
                    FileName = "cmd.exe",
                    Arguments = $"/c {command} {args}",
                    WorkingDirectory = workingDir,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                };
                using var process = Process.Start(psi);
                if (process == null) return false;
                // Read both streams while the process runs: otherwise a full buffer deadlocks WaitForExit.
                var stdout = process.StandardOutput.ReadToEndAsync();
                var stderr = process.StandardError.ReadToEndAsync();
                process.WaitForExit();
                LastOutput = stdout.Result + stderr.Result;
                return process.ExitCode == 0;
            }
            catch (Exception ex)
            {
                LastOutput = ex.Message;
                return false;
            }
        }

        public bool CreateProject(string name, string path, string? version = null)
        {
            string args = !string.IsNullOrEmpty(version) && version != Latest
                ? $"create-project laravel/laravel:{version} \"{name}\""
                : $"create-project laravel/laravel \"{name}\"";
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
}
