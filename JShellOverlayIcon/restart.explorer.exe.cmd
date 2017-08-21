@echo off
echo Killing explorer
taskkill /F /IM explorer.exe
echo Restarting explorer
%WINDIR%\explorer.exe
echo Done