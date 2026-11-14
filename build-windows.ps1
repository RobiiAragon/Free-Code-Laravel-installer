# build-windows.ps1
# Builds in Release mode and publishes a single-file executable to ./Program

Write-Host "Building in Release mode..."
dotnet build --configuration Release

Write-Host "Publishing a self-contained single-file build for Windows x64 to ./Program..."
# Single file, no debug symbols or PDBs
dotnet publish -c Release -r win-x64 --self-contained true /p:PublishSingleFile=true /p:IncludeNativeLibrariesForSelfExtract=true /p:DebugType=None /p:DebugSymbols=false -o "Program"

# Find the generated .exe
$exe = Get-ChildItem -Path Program -Filter *.exe | Select-Object -First 1
if ($exe) {
	Write-Host "Build and publish complete."
	Write-Host "Executable: $($exe.FullName)"
} else {
	Write-Host "No executable found in ./Program."
}