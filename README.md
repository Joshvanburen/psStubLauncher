# psStubLauncher
Stub Executable to Launch a PowerShell Script

The executable and the PowerShell script must have the same file name, i.e. test.exe and test.ps1.

The executable does the following:

1. Looks for the script in the same folder.
2. Sets the execution policy to bypass for the current process (this executable).
3. Launches the script.

A batch file to compile the executable in Windows is included that uses the C# compiler included in .NET.