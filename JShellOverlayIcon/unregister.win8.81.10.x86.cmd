@echo off
echo Unregister for Win8/8.1/10 x86
%SystemRoot%\Microsoft.NET\Framework\v4.0.30319\regasm bin\x86\Debug\JShellOverlayIconHandler.net4.x86.dll /unregister
echo Kill all explorer.exe instances
taskkill /F /IM explorer.exe
echo Restart explorer.exe
%WINDIR%\explorer.exe
echo Done