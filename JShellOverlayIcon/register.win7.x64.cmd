@echo off
echo Register for Win7 x64
%SystemRoot%\Microsoft.NET\Framework64\v2.0.50727\regasm bin\x64\Debug\JShellOverlayIconHandler.net2.x64.dll /codebase
echo Kill all explorer.exe instances
taskkill /F /IM explorer.exe
echo Restart explorer.exe
%WINDIR%\explorer.exe
echo Done