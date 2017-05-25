@echo off
echo Killing explorer
taskkill /F /IM explorer.exe
echo Restarting explorer
explorer.exe
echo Done