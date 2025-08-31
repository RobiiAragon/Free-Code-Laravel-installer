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

        // ... Configura posiciones, tamaños, textos, eventos, etc. ...
        // Por ejemplo:
        this.btnBrowseNew.Text = "Examinar";
        this.btnBrowseNew.Click += btnBrowseNew_Click;
        this.btnCreateProject.Text = "Crear Proyecto Laravel";
        this.btnCreateProject.Click += btnCreateProject_Click;
        // ... y así para los demás controles ...

        // Agrega controles a las pestañas y al formulario
        this.tabNew.Controls.AddRange(new Control[] { /* ... */ });
        this.tabExisting.Controls.AddRange(new Control[] { /* ... */ });
        this.tabControl.Controls.AddRange(new Control[] { this.tabNew, this.tabExisting });
        this.Controls.Add(this.tabControl);

        this.Text = "Laravel Visual Installer";
        this.ClientSize = new System.Drawing.Size(800, 600);
    }
}