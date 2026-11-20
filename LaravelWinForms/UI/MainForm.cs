
using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using LaravelWinForms;

namespace LaravelWinForms.UI
{
    public partial class MainForm : Form
    {
        private LaravelCommands laravel = new LaravelCommands();

        public MainForm()
        {
            InitializeComponent();
            cmbVersion.Items.AddRange(new string[] { LaravelCommands.Latest, "12.*", "11.*", "10.*", "9.*", "8.*" });
            cmbVersion.SelectedIndex = 0;
        }

        private void btnBrowseNew_Click(object sender, EventArgs e)
        {
            using (var fbd = new FolderBrowserDialog())
            {
                if (fbd.ShowDialog() == DialogResult.OK)
                    txtProjectPath.Text = fbd.SelectedPath;
            }
        }

        private void btnBrowseExisting_Click(object sender, EventArgs e)
        {
            using (var fbd = new FolderBrowserDialog())
            {
                if (fbd.ShowDialog() == DialogResult.OK)
                    txtExistingPath.Text = fbd.SelectedPath;
            }
        }

        // Runs a slow command without freezing the window and shows the real error on failure.
        private async Task RunAsync(Button button, Func<bool> action, string ok, string fail)
        {
            button.Enabled = false;
            UseWaitCursor = true;
            try
            {
                bool result = await Task.Run(action);
                if (result) MessageBox.Show(ok, "Laravel Installer", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else MessageBox.Show($"{fail}\n\n{laravel.LastOutput}".Trim(), "Laravel Installer", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                UseWaitCursor = false;
                button.Enabled = true;
            }
        }

        private bool ValidProject(string path)
        {
            if (Directory.Exists(path) && File.Exists(Path.Combine(path, "artisan"))) return true;
            MessageBox.Show("Pick a Laravel project folder (it must contain the 'artisan' file).", "Laravel Installer");
            return false;
        }

        private async void btnCreateProject_Click(object sender, EventArgs e)
        {
            string name = txtProjectName.Text.Trim();
            string path = txtProjectPath.Text.Trim();
            string? version = cmbVersion.SelectedItem?.ToString();
            if (string.IsNullOrEmpty(name) || !Directory.Exists(path))
            {
                MessageBox.Show("Enter a project name and an existing folder.", "Laravel Installer");
                return;
            }
            await RunAsync(btnCreateProject, () => laravel.CreateProject(name, path, version),
                $"Project created in {Path.Combine(path, name)}", "Couldn't create the project.");
        }

        private void btnServe_Click(object sender, EventArgs e)
        {
            string path = txtExistingPath.Text.Trim();
            if (!ValidProject(path)) return;
            laravel.Serve(path);
            MessageBox.Show("Server started at http://127.0.0.1:8000", "Laravel Installer");
        }

        private async void btnMigrate_Click(object sender, EventArgs e)
        {
            string path = txtExistingPath.Text.Trim();
            if (!ValidProject(path)) return;
            await RunAsync(btnMigrate, () => laravel.Migrate(path), "Migrations ran.", "Migrations failed.");
        }

        private async void btnMakeController_Click(object sender, EventArgs e)
        {
            string path = txtExistingPath.Text.Trim();
            string name = txtControllerName.Text.Trim();
            if (!ValidProject(path) || name.Length == 0) return;
            await RunAsync(btnMakeController, () => laravel.MakeController(path, name), $"Controller {name} created.", "Couldn't create the controller.");
        }

        private async void btnMakeModel_Click(object sender, EventArgs e)
        {
            string path = txtExistingPath.Text.Trim();
            string name = txtModelName.Text.Trim();
            if (!ValidProject(path) || name.Length == 0) return;
            await RunAsync(btnMakeModel, () => laravel.MakeModel(path, name), $"Model {name} created.", "Couldn't create the model.");
        }

        private async void btnMakeMigration_Click(object sender, EventArgs e)
        {
            string path = txtExistingPath.Text.Trim();
            string name = txtMigrationName.Text.Trim();
            if (!ValidProject(path) || name.Length == 0) return;
            await RunAsync(btnMakeMigration, () => laravel.MakeMigration(path, name), $"Migration {name} created.", "Couldn't create the migration.");
        }
    }
}
