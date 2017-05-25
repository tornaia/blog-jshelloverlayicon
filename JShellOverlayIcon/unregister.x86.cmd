@echo off
echo Unregister for x86 apps
%SystemRoot%\Microsoft.NET\Framework\v2.0.50727\regasm bin\x86\Debug\JShellOverlayIcon.dll /unregister
echo Kill all explorer.exe instances
taskkill /F /IM explorer.exe
echo Restart explorer.exe
explorer.exe
echo Done