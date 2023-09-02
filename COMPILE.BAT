:: Warning: Untested
@echo off
echo Warning: This batch script was not tested and thus may have bugs.
echo.
echo It is intended to compile the bash.cs single source file with *CSC compiler,
echo which is located in the following path:
echo %WINDIR%\Microsoft.NET\Framework64\v4.0.30319\csc.exe
echo.
echo This compiler compiles C# versions up to 5.0.
echo However, on some systems, such folder path may not exist (for example on Windows 7).
echo.
echo If you still want to compile this program, either use a newer compiler like Roslyn, C# make tool like MSBuild, compile with Visual Studio, test on an online compiler. We have already compiled bash.exe for you, which you can find in the ..\output folder.
echo.
set /P "cho=Proceed with caution! Would you like to continue? (Y/N): "
if %cho%==y goto :Compile
if %cho%==n exit
echo Warning: Invalid choice.
echo Exiting... No changes were made.
exit

:Compile
SET "OutputFile="

if "%~1" == "" (
    echo Warning: No command-line argument specifying output file was passed.
    echo Defaulting to bash.exe.

    SET "OutputFile=bash.exe"
) else (
    SET "OutputFile=%~1"
)

%WINDIR%\Microsoft.NET\Framework64\v4.0.30319\csc.exe /out:"%OutputFile%" %~dp0bash.cs

echo.
echo.
echo.
echo Compilation done. Press any key to exit...
pause >nul
