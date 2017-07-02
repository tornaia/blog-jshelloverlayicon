@echo off
echo Unregister for Win7 x86
%SystemRoot%\Microsoft.NET\Framework\v2.0.50727\regasm bin\x86\Debug\JShellOverlayIconHandler.net4.x86.dll /unregister
echo Kill all explorer.exe instances
taskkill /F /IM explorer.exe
echo Restart explorer.exe
explorer.exe
echo Done