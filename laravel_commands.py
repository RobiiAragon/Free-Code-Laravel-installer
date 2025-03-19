import os
import subprocess
import sys
import threading

class LaravelCommands:
    def __init__(self):
        # Determinar si estamos en Windows o en otro sistema
        self.is_windows = sys.platform.startswith('win')
        
    def run_command(self, command, cwd=None):
        """Ejecuta un comando del sistema y devuelve True si fue exitoso"""
        try:
            # En Windows, usamos shell=True para comandos como 'composer'
            result = subprocess.run(command, shell=self.is_windows, cwd=cwd, 
                                   check=True, capture_output=True, text=True)
            return True
        except subprocess.CalledProcessError as e:
            print(f"Error ejecutando '{' '.join(command)}': {e}")
            print(f"Output: {e.stdout}")
            print(f"Error: {e.stderr}")
            return False
        except Exception as e:
            print(f"Excepción: {str(e)}")
            return False
    
    def create_project(self, name, path, version=None):
        """Crea un nuevo proyecto Laravel con una versión específica si se proporciona"""
        full_path = os.path.join(path, name)
        
        if version:
            # Si se especifica una versión, usamos laravel/laravel seguido de la versión
            command = ["composer", "create-project", f"laravel/laravel:{version}", name]
        else:
            # Si no se especifica versión, usar la última versión disponible
            command = ["composer", "create-project", "laravel/laravel", name]
            
        return self.run_command(command, cwd=path)
    
    def serve(self, project_path):
        """Inicia el servidor de desarrollo de Laravel"""
        def run_server_thread():
            if self.is_windows:
                subprocess.Popen("php artisan serve", shell=True, cwd=project_path)
            else:
                subprocess.Popen(["php", "artisan", "serve"], cwd=project_path)
        
        # Ejecutar en un hilo separado para no bloquear la interfaz
        threading.Thread(target=run_server_thread, daemon=True).start()
        return True
    
    def migrate(self, project_path):
        """Ejecuta las migraciones de Laravel"""
        command = ["php", "artisan", "migrate"]
        return self.run_command(command, cwd=project_path)
    
    def make_controller(self, project_path, name):
        """Crea un nuevo controlador"""
        command = ["php", "artisan", "make:controller", name]
        return self.run_command(command, cwd=project_path)
    
    def make_model(self, project_path, name):
        """Crea un nuevo modelo"""
        command = ["php", "artisan", "make:model", name]
        return self.run_command(command, cwd=project_path)
    
    def make_migration(self, project_path, name):
        """Crea una nueva migración"""
        command = ["php", "artisan", "make:migration", name]
        return self.run_command(command, cwd=project_path)
