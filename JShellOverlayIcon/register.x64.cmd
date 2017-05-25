@echo off
echo Register for x86 apps
%SystemRoot%\Microsoft.NET\Framework\v2.0.50727\regasm bin\x86\Debug\JShellOverlayIcon.dll /codebase
echo Register for x64 apps
%SystemRoot%\Microsoft.NET\Framework64\v2.0.50727\regasm bin\x64\Debug\JShellOverlayIcon.dll /codebase
echo Kill all explorer.exe instances
taskkill /F /IM explorer.exe
echo Restart explorer.exe
explorer.exe
echo Done