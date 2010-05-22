using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using O2.Kernel;
using O2.Kernel.ExtensionMethods;
using O2.DotNetWrappers.Windows;
using O2.DotNetWrappers.ExtensionMethods;

namespace O2.XRules.Database._Rules.DotNet
{
    public class DotNet_SDK
    {
    	public static string WSDL_EXE_PATH = @"C:\Program Files\\Microsoft SDKs\Windows\v6.0A\bin\wsdl.exe";
		//modified
		
		public static bool wsdl_exe_exists()
		{
			
			return WSDL_EXE_PATH.fileExists();
		}
		
        public static string wsdl_CreateCSharp(string sourceFile, string targetFolder)
        {        	
        	if (wsdl_exe_exists())
        	{
        		//var expectedFile = targetFolder.pathCombine(Path.ChangeExtension(sourceFile.fileName(),"cs"));
        		//Files.deleteFile(expectedFile);
            	var parameters = "\"{0}\" /out:\"{1}\"".format(sourceFile, targetFolder);
            	var executionResult = Processes.startProcessAsConsoleApplicationAndReturnConsoleOutput(WSDL_EXE_PATH, parameters);            	
            	executionResult.info();
            	if (executionResult.valid())
            	{
            		var splitData = executionResult.split("'");            		
            		if(splitData.size()==3)
            		{
            			var fileCreated = splitData[1];
            			if (fileCreated.fileExists())
            				return fileCreated;
            		}
            	}            	
			}            	
            return "";
        }
       
    }
}
