@echo off
echo Unregister for Win8/8.1/10 x64
%SystemRoot%\Microsoft.NET\Framework64\v4.0.30319\regasm bin\x64\Debug\JShellOverlayIconHandler.net4.x64.dll /unregister
echo Kill all explorer.exe instances
taskkill /F /IM explorer.exe
echo Restart explorer.exe
explorer.exe
echo Done