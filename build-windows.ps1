# build-winforms.ps1
# Compila el proyecto en modo Release y publica un ejecutable como un solo archivo en la carpeta Program

Write-Host "Compilando el proyecto en modo Release..."
dotnet build --configuration Release

Write-Host "Publicando ejecutable self-contained, single-file para Windows x64 en la carpeta Program..."
# Elimina archivos intermedios y fuerza single-file, sin incluir archivos de depuración ni PDB
dotnet publish -c Release -r win-x64 --self-contained true /p:PublishSingleFile=true /p:IncludeNativeLibrariesForSelfExtract=true /p:DebugType=None /p:DebugSymbols=false -o "Program"

# Buscar el .exe generado y mostrarlo
$exe = Get-ChildItem -Path Program -Filter *.exe | Select-Object -First 1
if ($exe) {
	Write-Host "Build y publicación completados."
	Write-Host "El ejecutable único se encuentra en: $($exe.FullName)"
} else {
	Write-Host "No se encontró el ejecutable en la carpeta Program."
}