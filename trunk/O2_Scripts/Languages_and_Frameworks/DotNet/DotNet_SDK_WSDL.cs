using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using O2.Kernel;
using O2.Kernel.ExtensionMethods;
using O2.DotNetWrappers.Windows;
using O2.DotNetWrappers.ExtensionMethods;
//O2Ref:MS_SDK_wsdl.exe

namespace O2.XRules.Database.Languages_and_Frameworks.DotNet
{
    public class DotNet_SDK_WSDL
    {
    	public string Wsdl_Exe { get; set;}
		//modified
		
		public DotNet_SDK_WSDL()
		{
			Wsdl_Exe = PublicDI.config.CurrentExecutableDirectory.pathCombine("MS_SDK_wsdl.exe");
		}
		
		public bool wsdl_exe_exists()
		{			
			return Wsdl_Exe.fileExists();
		}
		
        public string wsdl_CreateCSharp(string wsdlSourceFile, string targetFolder)
        {        	
        	if (wsdl_exe_exists())
        	{        		
            	var parameters = "\"{0}\" /out:\"{1}\"".format(wsdlSourceFile, targetFolder);
            	var executionResult = Processes.startProcessAsConsoleApplicationAndReturnConsoleOutput(Wsdl_Exe, parameters);            	
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
