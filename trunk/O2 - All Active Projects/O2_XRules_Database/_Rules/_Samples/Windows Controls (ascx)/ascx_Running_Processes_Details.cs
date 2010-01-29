// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using System.Data;
//O2Ref:System.Management.dll
using System.Management;
using O2.Kernel;
using O2.Kernel.Interfaces.O2Core;
using O2.Kernel.Interfaces.Views;
using O2.DotNetWrappers.DotNet;
using O2.DotNetWrappers.Windows;
using O2.Views.ASCX;
using O2.Views.ASCX.DataViewers;
using O2.External.WinFormsUI.Forms;
using O2.Views.ASCX.classes.MainGUI;
using O2.Views.ASCX.classes;

namespace O2.Script
{	
    public class ascx_Running_Processes_Details : UserControl
    {    
    	private static IO2Log log = PublicDI.log;
		private ascx_TableList tableList;				
		
		public static void openAscx()
		{
			WinForms.showAscxInForm(
				typeof(ascx_Running_Processes_Details), 
				"Running Process Details", 
				700, 
				500);		
		}
		
        public ascx_Running_Processes_Details()
    	{
    		createGUI();
    	}    	
    	
    	public void createGUI()
        {
        	var columns = new List<string> { "ProcessName", "Id", "SessionId", "MainModule", "MainWindowTitle", "FileName", "WorkingDirectory", "Arguments"};
        	tableList = this.add_TableList("Processes");
        	tableList.setDataTable(GetRunningProcesses());        	

     	}
    	    	    	    	    
    
   		// this method was based on the code from http://www.dreamincode.net/forums/showtopic67090.htm
   		// (this version has some modifications on the original code, and was copied from reflector)
	    public static DataTable GetRunningProcesses()
		{
		    string wmiClass = "Win32_Process";
		    string condition = "";
		    string[] queryProperties = new string[] { "Name", "ProcessId", "Caption", "ExecutablePath", "CommandLine" };
		    SelectQuery wmiQuery = new SelectQuery(wmiClass, condition, queryProperties);
		    ManagementScope scope = new ManagementScope(@"\\.\root\CIMV2");
		    ManagementObjectCollection runningProcesses = new ManagementObjectSearcher(scope, wmiQuery).Get();
		    DataTable queryResults = new DataTable();
		    queryResults.Columns.Add("Name", Type.GetType("System.String"));
		    queryResults.Columns.Add("ProcessId", Type.GetType("System.Int32"));
		    queryResults.Columns.Add("Caption", Type.GetType("System.String"));
		    queryResults.Columns.Add("Path", Type.GetType("System.String"));
		    queryResults.Columns.Add("CommandLine", Type.GetType("System.String"));
		    foreach (ManagementObject obj in runningProcesses)
		    {
		        DataRow row = queryResults.NewRow();
		        row["Name"] = obj["Name"].ToString();
		        row["ProcessId"] = Convert.ToInt32(obj["ProcessId"]);
		        if (obj["Caption"] != null)
		        {
		            row["Caption"] = obj["Caption"].ToString();
		        }
		        if ((obj["ExecutablePath"] != null) && (obj["CommandLine"] != null))
		        {
		            string rawCommandLine = obj["CommandLine"].ToString();
		            string executablePath = obj["ExecutablePath"].ToString();
		            row["Path"] = executablePath;
		            if (rawCommandLine.StartsWith("\"" + executablePath + "\""))
		            {
		                rawCommandLine = rawCommandLine.Substring(executablePath.Length + 2);
		            }
		            else if (rawCommandLine.StartsWith(executablePath))
		            {
		                rawCommandLine = rawCommandLine.Substring(executablePath.Length);
		            }
		            row["CommandLine"] = rawCommandLine;
		        }
		        queryResults.Rows.Add(row);
		    }
		    return queryResults;
		}
	}
}
