@echo off
:loop
call delay1.vbs
taskkill /f /im DevDispatch3.exe

call delay2.vbs
start DevDispatch3.exe

goto loop