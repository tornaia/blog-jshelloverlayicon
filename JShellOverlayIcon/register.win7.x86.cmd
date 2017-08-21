@echo off
echo Register for Win7 x86
%SystemRoot%\Microsoft.NET\Framework\v2.0.50727\regasm bin\x86\Debug\JShellOverlayIconHandler.net2.x86.dll /codebase
echo Kill all explorer.exe instances
taskkill /F /IM explorer.exe
echo Restart explorer.exe
%WINDIR%\explorer.exe
echo Done