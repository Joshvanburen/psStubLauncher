::Josh Van Buren
::Batch file to compile the C# code for the PowerShell stub launcher

::Uncomment to hide command line echoes

::Set some variables to make editing easier
set csc="%SystemRoot%\Microsoft.NET\Framework64\v4.0.30319\csc.exe"
set platform=/platform:anycpu
set target=/target:winexe
set manifest=/win32manifest:"%~dp0psStubLauncher.manifest" 
set icon=/win32icon:"%~dp0defaultIcon.ico"
set references=/reference:"%SystemRoot%\Microsoft.Net\assembly\GAC_MSIL\System.Management.Automation\v4.0_3.0.0.0__31bf3856ad364e35\System.Management.Automation.dll"
set optionalParameters=/optimize
set out=/out:
set executableName="%~dp0psStubLauncher.exe"
set sources="%~dp0psStubLauncher.cs" "%~dp0psStubLauncherAssembly.cs"

::Compile command 
%csc% %platform% %target% %manifest% %icon% %references% %optionalParameters% %out%%executableName% %sources%