namespace LaravelWinForms.UI
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;
        private TabControl tabControl;
        private TabPage tabNew;
        private TabPage tabExisting;
        private TextBox txtProjectName, txtProjectPath, txtExistingPath, txtControllerName, txtModelName;
        private ComboBox cmbVersion;
        private Button btnBrowseNew, btnCreateProject, btnBrowseExisting, btnServe, btnMigrate, btnMakeController, btnMakeModel;

        private void InitializeComponent()
        {
            this.tabControl = new TabControl();
            this.tabNew = new TabPage("Nuevo Proyecto");
            this.tabExisting = new TabPage("Proyecto Existente");
            this.txtProjectName = new TextBox();
            this.txtProjectPath = new TextBox();
            this.cmbVersion = new ComboBox();
            this.btnBrowseNew = new Button();
            this.btnCreateProject = new Button();
            this.txtExistingPath = new TextBox();
            this.btnBrowseExisting = new Button();
            this.btnServe = new Button();
            this.btnMigrate = new Button();
            this.txtControllerName = new TextBox();
            this.btnMakeController = new Button();
            this.txtModelName = new TextBox();
            this.btnMakeModel = new Button();

            // --- Nuevo Proyecto ---
            this.tabNew.Controls.AddRange(new Control[] {
                new Label { Text = "Nombre del proyecto:", Top = 20, Left = 20, Width = 130 },
                txtProjectName,
                new Label { Text = "Ubicación:", Top = 60, Left = 20, Width = 130 },
                txtProjectPath,
                btnBrowseNew,
                new Label { Text = "Versión de Laravel:", Top = 100, Left = 20, Width = 130 },
                cmbVersion,
                btnCreateProject
            });
            txtProjectName.SetBounds(160, 20, 200, 25);
            txtProjectPath.SetBounds(160, 60, 200, 25);
            btnBrowseNew.Text = "Examinar";
            btnBrowseNew.SetBounds(370, 60, 80, 25);
            btnBrowseNew.Click += btnBrowseNew_Click;
            cmbVersion.SetBounds(160, 100, 120, 25);
            btnCreateProject.Text = "Crear Proyecto Laravel";
            btnCreateProject.SetBounds(160, 140, 180, 30);
            btnCreateProject.Click += btnCreateProject_Click;

            // --- Proyecto Existente ---
            this.tabExisting.Controls.AddRange(new Control[] {
                new Label { Text = "Ruta del proyecto:", Top = 20, Left = 20, Width = 130 },
                txtExistingPath,
                btnBrowseExisting,
                btnServe,
                btnMigrate,
                new Label { Text = "Controlador:", Top = 120, Left = 20, Width = 130 },
                txtControllerName,
                btnMakeController,
                new Label { Text = "Modelo:", Top = 160, Left = 20, Width = 130 },
                txtModelName,
                btnMakeModel
            });
            txtExistingPath.SetBounds(160, 20, 200, 25);
            btnBrowseExisting.Text = "Examinar";
            btnBrowseExisting.SetBounds(370, 20, 80, 25);
            btnBrowseExisting.Click += btnBrowseExisting_Click;
            btnServe.Text = "Ejecutar servidor";
            btnServe.SetBounds(160, 60, 130, 30);
            btnServe.Click += btnServe_Click;
            btnMigrate.Text = "Ejecutar migraciones";
            btnMigrate.SetBounds(300, 60, 130, 30);
            btnMigrate.Click += btnMigrate_Click;
            txtControllerName.SetBounds(160, 120, 200, 25);
            btnMakeController.Text = "Crear Controlador";
            btnMakeController.SetBounds(370, 120, 120, 25);
            btnMakeController.Click += btnMakeController_Click;
            txtModelName.SetBounds(160, 160, 200, 25);
            btnMakeModel.Text = "Crear Modelo";
            btnMakeModel.SetBounds(370, 160, 120, 25);
            btnMakeModel.Click += btnMakeModel_Click;

            // --- TabControl ---
            this.tabControl.Controls.AddRange(new Control[] { this.tabNew, this.tabExisting });
            this.tabControl.Dock = DockStyle.Fill;
            this.Controls.Add(this.tabControl);
            this.Text = "Laravel Visual Installer";
            this.ClientSize = new System.Drawing.Size(600, 300);
        }
    }
}
