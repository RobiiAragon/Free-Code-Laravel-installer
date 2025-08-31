
using System;
using System.IO;
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
            cmbVersion.Items.AddRange(new string[] { "Última versión", "10.*", "9.*", "8.*", "7.*", "6.*" });
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

        private void btnCreateProject_Click(object sender, EventArgs e)
        {
            string name = txtProjectName.Text.Trim();
            string path = txtProjectPath.Text.Trim();
            string version = cmbVersion.SelectedItem?.ToString();
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(path))
            {
                MessageBox.Show("Por favor ingrese nombre y ruta.");
                return;
            }
            bool result = laravel.CreateProject(name, path, version);
            MessageBox.Show(result ? "Proyecto creado correctamente." : "Error al crear el proyecto.");
        }

        private void btnServe_Click(object sender, EventArgs e)
        {
            string path = txtExistingPath.Text.Trim();
            if (Directory.Exists(path))
            {
                laravel.Serve(path);
                MessageBox.Show("Servidor iniciado en http://127.0.0.1:8000");
            }
            else
            {
                MessageBox.Show("Ruta inválida.");
            }
        }

        private void btnMigrate_Click(object sender, EventArgs e)
        {
            string path = txtExistingPath.Text.Trim();
            bool result = laravel.Migrate(path);
            MessageBox.Show(result ? "Migraciones ejecutadas." : "Error en migraciones.");
        }

        private void btnMakeController_Click(object sender, EventArgs e)
        {
            string path = txtExistingPath.Text.Trim();
            string name = txtControllerName.Text.Trim();
            bool result = laravel.MakeController(path, name);
            MessageBox.Show(result ? "Controlador creado." : "Error al crear controlador.");
        }

        private void btnMakeModel_Click(object sender, EventArgs e)
        {
            string path = txtExistingPath.Text.Trim();
            string name = txtModelName.Text.Trim();
            bool result = laravel.MakeModel(path, name);
            MessageBox.Show(result ? "Modelo creado." : "Error al crear modelo.");
        }
    }
}
