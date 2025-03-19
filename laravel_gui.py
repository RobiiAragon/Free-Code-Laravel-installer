import tkinter as tk
from tkinter import ttk, filedialog, messagebox
import os
import subprocess
import sys
from laravel_commands import LaravelCommands

class LaravelGUI:
    def __init__(self, root):
        self.root = root
        self.root.title("Laravel Visual Installer")
        self.root.geometry("800x600")
        self.root.resizable(True, True)
        
        # Comandos Laravel
        self.laravel = LaravelCommands()
        
        # Variables
        self.project_name = tk.StringVar()
        self.project_path = tk.StringVar()
        self.project_path.set(os.path.expanduser("~/Documents"))
        self.command_running = False  # Variable para controlar si hay un comando en ejecución
        
        # Crear interfaz
        self.create_notebook()
        
    def create_notebook(self):
        notebook = ttk.Notebook(self.root)
        notebook.pack(expand=True, fill="both", padx=10, pady=10)
        
        # Pestañas
        self.create_new_project_tab(notebook)
        self.create_existing_project_tab(notebook)
        
    def create_new_project_tab(self, notebook):
        new_project_frame = ttk.Frame(notebook)
        notebook.add(new_project_frame, text="Nuevo Proyecto")
        
        # Contenido
        ttk.Label(new_project_frame, text="Crear nuevo proyecto Laravel", font=("Arial", 14)).pack(pady=10)
        
        # Nombre del proyecto
        project_frame = ttk.Frame(new_project_frame)
        project_frame.pack(fill="x", padx=20, pady=10)
        ttk.Label(project_frame, text="Nombre del proyecto:").pack(side="left", padx=5)
        ttk.Entry(project_frame, textvariable=self.project_name, width=30).pack(side="left", padx=5)
        
        # Ruta del proyecto
        path_frame = ttk.Frame(new_project_frame)
        path_frame.pack(fill="x", padx=20, pady=10)
        ttk.Label(path_frame, text="Ubicación:").pack(side="left", padx=5)
        ttk.Entry(path_frame, textvariable=self.project_path, width=50).pack(side="left", padx=5)
        ttk.Button(path_frame, text="Examinar", command=self.browse_directory).pack(side="left", padx=5)
        
        # Versión de Laravel
        version_frame = ttk.Frame(new_project_frame)
        version_frame.pack(fill="x", padx=20, pady=10)
        ttk.Label(version_frame, text="Versión de Laravel:").pack(side="left", padx=5)
        
        # Lista de versiones disponibles
        self.laravel_version = tk.StringVar()
        versions = ["Última versión", "10.*", "9.*", "8.*", "7.*", "6.*"]
        version_combo = ttk.Combobox(version_frame, textvariable=self.laravel_version, values=versions, width=20)
        version_combo.current(0)
        version_combo.pack(side="left", padx=5)
        
        # Opciones
        options_frame = ttk.LabelFrame(new_project_frame, text="Opciones")
        options_frame.pack(fill="x", padx=20, pady=10)
        
        self.git_init = tk.BooleanVar(value=True)
        ttk.Checkbutton(options_frame, text="Inicializar Git", variable=self.git_init).pack(anchor="w", padx=5, pady=5)
        
        # Botón crear proyecto
        ttk.Button(new_project_frame, text="Crear Proyecto Laravel", 
                   command=self.create_project).pack(pady=20)
        
    def create_existing_project_tab(self, notebook):
        existing_project_frame = ttk.Frame(notebook)
        notebook.add(existing_project_frame, text="Proyecto Existente")
        
        # Contenido
        ttk.Label(existing_project_frame, text="Gestionar proyecto Laravel existente", font=("Arial", 14)).pack(pady=10)
        
        # Seleccionar proyecto existente
        path_frame = ttk.Frame(existing_project_frame)
        path_frame.pack(fill="x", padx=20, pady=10)
        ttk.Label(path_frame, text="Proyecto:").pack(side="left", padx=5)
        self.existing_path = tk.StringVar()
        ttk.Entry(path_frame, textvariable=self.existing_path, width=50).pack(side="left", padx=5)
        ttk.Button(path_frame, text="Examinar", command=self.browse_project).pack(side="left", padx=5)
        
        # Acciones comunes
        actions_frame = ttk.LabelFrame(existing_project_frame, text="Acciones")
        actions_frame.pack(fill="both", expand=True, padx=20, pady=10)
        
        # Botones de acciones
        ttk.Button(actions_frame, text="Ejecutar servidor (php artisan serve)",
                  command=self.run_server).pack(fill="x", padx=10, pady=5)
        ttk.Button(actions_frame, text="Ejecutar migraciones", 
                  command=self.run_migrations).pack(fill="x", padx=10, pady=5)
        
        # Creación de componentes
        components_frame = ttk.LabelFrame(existing_project_frame, text="Crear componentes")
        components_frame.pack(fill="x", padx=20, pady=10)
        
        # Fila para crear controlador
        controller_frame = ttk.Frame(components_frame)
        controller_frame.pack(fill="x", padx=5, pady=5)
        ttk.Label(controller_frame, text="Nombre del controlador:").pack(side="left", padx=5)
        self.controller_name = tk.StringVar()
        ttk.Entry(controller_frame, textvariable=self.controller_name, width=30).pack(side="left", padx=5)
        ttk.Button(controller_frame, text="Crear Controlador", 
                  command=self.create_controller).pack(side="left", padx=10)
        
        # Fila para crear modelo
        model_frame = ttk.Frame(components_frame)
        model_frame.pack(fill="x", padx=5, pady=5)
        ttk.Label(model_frame, text="Nombre del modelo:").pack(side="left", padx=5)
        self.model_name = tk.StringVar()
        ttk.Entry(model_frame, textvariable=self.model_name, width=30).pack(side="left", padx=5)
        ttk.Button(model_frame, text="Crear Modelo", 
                  command=self.create_model).pack(side="left", padx=10)
    
    def check_command_running(self):
        if self.command_running:
            messagebox.showwarning("Comando en ejecución", "Hay un comando en ejecución. Por favor espere a que termine.")
            return True
        return False
    
    def browse_directory(self):
        directory = filedialog.askdirectory()
        if directory:
            self.project_path.set(directory)
    
    def browse_project(self):
        directory = filedialog.askdirectory()
        if directory:
            self.existing_path.set(directory)
    
    def create_project(self):
        if self.check_command_running():
            return
            
        name = self.project_name.get().strip()
        path = self.project_path.get().strip()
        version = self.laravel_version.get()
        
        if not name:
            messagebox.showerror("Error", "Por favor ingrese un nombre para el proyecto")
            return
            
        if not path:
            messagebox.showerror("Error", "Por favor seleccione una ubicación para el proyecto")
            return
            
        try:
            # Mostrar ventana de progreso
            progress_window = tk.Toplevel(self.root)
            progress_window.title("Creando proyecto...")
            progress_window.geometry("400x150")
            
            ttk.Label(progress_window, text=f"Creando proyecto {name}...").pack(pady=10)
            progress = ttk.Progressbar(progress_window, mode="indeterminate")
            progress.pack(fill="x", padx=20, pady=10)
            progress.start()
            
            # Actualizar la GUI
            progress_window.update()
            
            # Marcar que hay un comando en ejecución
            self.command_running = True
            
            # Crear el proyecto con la versión seleccionada
            version_param = None
            if version != "Última versión":
                version_param = version
                
            # Crear el proyecto
            result = self.laravel.create_project(name, path, version_param)
            
            # Marcar que el comando ha terminado
            self.command_running = False
            
            progress_window.destroy()
            
            if result:
                messagebox.showinfo("Éxito", f"Proyecto Laravel '{name}' creado correctamente en:\n{os.path.join(path, name)}")
            else:
                messagebox.showerror("Error", "No se pudo crear el proyecto. Verifique que tenga Composer instalado.")
                
        except Exception as e:
            self.command_running = False
            messagebox.showerror("Error", f"Ocurrió un error: {str(e)}")
    
    def run_server(self):
        if self.check_command_running():
            return
            
        path = self.existing_path.get().strip()
        if not path or not os.path.exists(path):
            messagebox.showerror("Error", "Por favor seleccione un proyecto Laravel válido")
            return
            
        try:
            self.command_running = True
            result = self.laravel.serve(path)
            self.command_running = False
            
            if result:
                messagebox.showinfo("Servidor iniciado", "Servidor Laravel iniciado en http://127.0.0.1:8000")
            else:
                messagebox.showerror("Error", "No se pudo iniciar el servidor")
        except Exception as e:
            self.command_running = False
            messagebox.showerror("Error", f"Error al iniciar servidor: {str(e)}")
    
    def run_migrations(self):
        if self.check_command_running():
            return
            
        path = self.existing_path.get().strip()
        if not path or not os.path.exists(path):
            messagebox.showerror("Error", "Por favor seleccione un proyecto Laravel válido")
            return
            
        try:
            self.command_running = True
            result = self.laravel.migrate(path)
            self.command_running = False
            
            if result:
                messagebox.showinfo("Éxito", "Migraciones ejecutadas correctamente")
            else:
                messagebox.showerror("Error", "Error al ejecutar migraciones")
        except Exception as e:
            self.command_running = False
            messagebox.showerror("Error", f"Error: {str(e)}")
    
    def create_controller(self):
        if self.check_command_running():
            return
            
        path = self.existing_path.get().strip()
        name = self.controller_name.get().strip()
        
        if not path or not os.path.exists(path):
            messagebox.showerror("Error", "Por favor seleccione un proyecto Laravel válido")
            return
        
        if not name:
            messagebox.showerror("Error", "Por favor ingrese un nombre para el controlador")
            return
            
        try:
            self.command_running = True
            result = self.laravel.make_controller(path, name)
            self.command_running = False
            
            if result:
                messagebox.showinfo("Éxito", f"Controlador {name} creado correctamente")
            else:
                messagebox.showerror("Error", "No se pudo crear el controlador")
        except Exception as e:
            self.command_running = False
            messagebox.showerror("Error", f"Error: {str(e)}")
    
    def create_model(self):
        if self.check_command_running():
            return
            
        path = self.existing_path.get().strip()
        name = self.model_name.get().strip()
        
        if not path or not os.path.exists(path):
            messagebox.showerror("Error", "Por favor seleccione un proyecto Laravel válido")
            return
        
        if not name:
            messagebox.showerror("Error", "Por favor ingrese un nombre para el modelo")
            return
            
        try:
            self.command_running = True
            result = self.laravel.make_model(path, name)
            self.command_running = False
            
            if result:
                messagebox.showinfo("Éxito", f"Modelo {name} creado correctamente")
            else:
                messagebox.showerror("Error", "No se pudo crear el modelo")
        except Exception as e:
            self.command_running = False
            messagebox.showerror("Error", f"Error: {str(e)}")

if __name__ == "__main__":
    root = tk.Tk()
    app = LaravelGUI(root)
    root.mainloop()
