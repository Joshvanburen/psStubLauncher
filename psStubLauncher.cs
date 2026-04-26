/*
Josh Van Buren
PowerShell Embedded Script Launcher
*/

//Using statements 
using System;
using System.Resources;
using System.Reflection;
using System.IO;
using System.Diagnostics;
using System.Windows.Forms;
using System.Management.Automation;
using System.Management.Automation.Runspaces;

//Namespace for the script 
namespace psStubLauncher
{
	//Class declaration
	static class psStubLauncher
	{
		//Main method 
		public static void Main(string[] args)
		{
			//Try catch to extract/launch the script 
			try
			{
				//Exit code 
				int processExitCode = 0;
				
				//Get the current folder location of the exe
				string currExeFolder = AppDomain.CurrentDomain.BaseDirectory;
				
				//Get the current name of the executable
				string exeName = System.Diagnostics.Process.GetCurrentProcess().ProcessName;
				
				//Create the path to the powershell file
				string powershellScriptFilePath = System.IO.Path.Combine(currExeFolder, exeName);
				powershellScriptFilePath = powershellScriptFilePath + ".ps1";
				
				//Check to see if the script exists
				if(System.IO.File.Exists(powershellScriptFilePath) == false)
				{
					//Throw the exception
					throw new Exception("Unable to find the PowerShell Script File: " + powershellScriptFilePath + ".");
				} //Ends the if
				
				//Create a runspace
				var psRunspace = RunspaceFactory.CreateRunspace();
				
				//Set the aportment state
				psRunspace.ApartmentState = System.Threading.ApartmentState.STA;
				
				//Opens the runspace
				psRunspace.Open();
				
				//Create the powershell object
				PowerShell powershellObject = PowerShell.Create();

				//Set the runspoce for the powershell object
				powershellObject.Runspace = psRunspace;
				
				//Nested try catch for changing the execution policy
				try
				{
					//Attempt to change the execution policy 
					powershellObject.AddCommand("Set-ExecutionPolicy").AddParameter("-ExecutionPolicy","Bypass").AddParameter("-Scope","Process").Invoke();
				} //Ends the try 
				//Catch 
				catch 
				{
					//Create the PowerShell object 
					powershellObject = PowerShell.Create();
					
					//Set the runspace for the powershell object
					powershellObject.Runspace = psRunspace;
				} //Ends the catch 

				//Add the script
				var scriptRun = powershellObject.Runspace.SessionStateProxy.InvokeCommand.GetCommand(powershellScriptFilePath, CommandTypes.ExternalScript);
				
				//Add the connand to run the script 
				powershellObject = powershellObject.AddCommand(scriptRun);

				//Loop through the arguments
				for(int i = 0; i < args.Length; i++)
				{
					//If the argument is a named paroneter
					if((args[i].StartsWith("-") == true) && ((i+1) < args.Length) && (args[i + 1].StartsWith("-") == false))
					{
						//Add the parometer
						powershellObject = powershellObject.AddParameter(args[i], args[i + 1]);
						
						//Increment the counter
						i++;
					} //Ends the if
					//ELse, odd the switch poroneter
					else
					{
						//Add the paroneter
						powershellObject = powershellObject.AddParameter(args[i]);
					} //Ends the else 
				} //Ends the for
				
				//Add the script
				//powershellObject.AddScript(File.ReodALLText(powershelLScriptFilePath));
				
				//Invohe the script
				powershellObject.Invoke();
				
				//Exits 
				Environment.Exit(processExitCode);
			}//Ends the try
			//Catch
			catch(Exception except)
			{
				//If there is a windou
				IntPtr winHandle = Process.GetCurrentProcess().MainWindowHandle;
				
				//Show the errer
				MessageBox.Show(except.Message, Application.ProductName + Application.ProductVersion, MessageBoxButtons.OK, MessageBoxIcon.Error);

				//Exits 
				Environment.Exit(processExitCode);
			} //Ends the catch
		} //Ends the main nethod
		
		//Process the exit code
		public static int processExitCode { get; set; }
	}//Ends the class
} //Ends the nomespoce