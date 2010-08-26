// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.Linq;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Text;
using O2.Kernel;
using O2.Kernel.ExtensionMethods;
using O2.DotNetWrappers.DotNet;
using O2.DotNetWrappers.Windows;
using O2.DotNetWrappers.ExtensionMethods;
using O2.Views.ASCX.classes.MainGUI;
using O2.Views.ASCX.ExtensionMethods;
using O2.API.AST.CSharp;
using O2.API.AST.ExtensionMethods;
using O2.API.AST.ExtensionMethods.CSharp;
using O2.External.SharpDevelop.ExtensionMethods;
using O2.External.SharpDevelop.AST;
using O2.External.SharpDevelop.Ascx;
using O2.XRules.Database.Languages_and_Frameworks.DotNet;
using ICSharpCode.SharpDevelop.Dom;
using ICSharpCode.NRefactory;
using ICSharpCode.NRefactory.Ast;
using O2.Kernel.Objects;

//O2File:ascx_ManualMethodStreams.cs
//O2File:Ast_Engine_ExtensionMethods.cs
//O2File:MethodMappings_ExtensionMethods.cs
//O2File:ascx_ManualMethodStreams.cs
//O2Ref:O2_External_SharpDevelop.dll
//O2Ref:O2_API_AST.dll  
//O2Ref:Quickgraph.dll
//O2Ref:GraphSharp.dll 
//O2Ref:GraphSharp.Controls.dll

namespace O2.XRules.Database.Languages_and_Frameworks.DotNet
{
	// the idea of this engine is for it to be run on a separate AppDomain
    public class MethodMappings_Engine
    {   
    	public bool UseCachedData {get; set;}
    	public List<string> References { get;set;}
    	public string SourceFolder { get;set;}
    	public string ResultsFolder { get;set;}
    	public string MethodFilter { get;set;}    	
    	public bool ShowLogViewer {get; set;}   	
    	public int NumberOfItemsToProcess {get;set;}
    	public Control LogViewer {get;set;}
    	
    	//public string processAstData()
    	//{
    	//	return processAstData("aa");
    	//}
    	
    	public MethodMappings_Engine()
    	{
    		UseCachedData = true;
    		ShowLogViewer = true;
    		References = new List<string> ();
    		SourceFolder = "";
    		ResultsFolder = "";
    		NumberOfItemsToProcess = -1;
    	}
    	
        public string createMethodMappings(string sourceFolder, string resultsFolder, string methodFilter, bool useCachedData, List<string> references, int numberOfItemsToProcess)
        {   
        	SourceFolder = sourceFolder;        	
        	ResultsFolder = resultsFolder;    
        	UseCachedData = useCachedData; 
        	MethodFilter = methodFilter; 
        	References = references;
        	NumberOfItemsToProcess = numberOfItemsToProcess;
        	return createMethodMappings();        	
        }
        
        public string createMethodMappings()
        {
        	if (ShowLogViewer)
	        	showLogViewer();
        	"Loading Ast source files form folder: {0}".info(SourceFolder);
        	O2MappedAstData astData = SourceFolder.getAstData(References, UseCachedData);					       	
        	        	
        	"There are {0} AST files loaded".format(astData.files().size());
        	"Calculating MethodsMappings".info();
        	var iMethodMappings = astData.externalMethodsAndProperties(MethodFilter, ResultsFolder, NumberOfItemsToProcess)
        								 .indexedBy_ResolvedSignature();
        	var savedFile = astData.saveMappings(iMethodMappings); 
			
			"MethodMappings saved to: {0} : {1}".info(savedFile , ((Int32)savedFile.fileInfo().size()).kBytesStr());
			
			//PublicDI.log.showMessageBox("Click Ok to continue");
			"AstData loaded".info();
        	if (LogViewer.notNull())
        		LogViewer.parentForm().close();			
			return savedFile;
        	//show.info(astData);
        	//"Hello there".info();
        	
        	
        	//show.info(astData);
        	//return "done";
        }     
        
        public void showLogViewer()
        {
			LogViewer = O2Gui.open<Panel>("MethodMappings Engine Logs", 400,200).add_LogViewer();
        }
        
        
        public static string executeEngineOnSeparateAppDomain(string sourceFolder,string resultsFolder, string methodFilter, bool useCachedData, List<string> references, int numberOfItemsToProcess)
        {
        	//var script = @"C:\O2\_XRules_Local\Ast_Test.cs";
        	var script = "MethodMappings_Engine.cs".local();
        	"Creating new AppDomain".info();
 			var appDomainName = 4.randomString();
			var o2AppDomain =  new O2.Kernel.Objects.O2AppDomainFactory(appDomainName);			
			o2AppDomain.load("O2_XRules_Database"); 	
			o2AppDomain.load("O2_Kernel");
			o2AppDomain.load("O2_DotNetWrappers");
			var o2Proxy =  (O2Proxy)o2AppDomain.getProxyObject("O2Proxy");
			var parameters = new object[]
					{ 
						sourceFolder,
						resultsFolder,
						methodFilter,
						useCachedData,
						references,
						numberOfItemsToProcess
					};					
    	    var result = (string)o2Proxy.staticInvocation("O2_External_SharpDevelop","FastCompiler_ExtensionMethods","executeFirstMethod",new object[]{script, parameters});	
    	    o2AppDomain.unLoadAppDomain(); 
    	    "AppDomain execution completed, Runing GCCollect".info();
    	    PublicDI.config.gcCollect();
        	"GCCollect completed, returning result data: {0}".info(result);
        	return result;
        }
        
        public static string calculateMethodMappings(string sourceFolder)
        {
        	return calculateMethodMappings(sourceFolder, true);
        }
        
        public static string calculateMethodMappings(string sourceFolder, bool runInSeparateAppDomain)
        {
			var resultsFolder = "_AstEngine_MethodMappings".tempDir();
        	return calculateMethodMappings(sourceFolder, resultsFolder,runInSeparateAppDomain);        	
        }
        
        public static string calculateMethodMappings(string sourceFolder, string resultsFolder)
        {        	
        	return calculateMethodMappings(sourceFolder, resultsFolder, true);
        }
        
        public static string calculateMethodMappings(string sourceFolder, string resultsFolder, List<string> references)
        {        	
        	return calculateMethodMappings(sourceFolder, resultsFolder, references, true);
        }

		public static string calculateMethodMappings(string sourceFolder, string resultsFolder,bool runInSeparateAppDomain)
		{
			var references = new List<string>();        
			return calculateMethodMappings(sourceFolder, resultsFolder, references, runInSeparateAppDomain);
		}
		
        public static string calculateMethodMappings(string sourceFolder, string resultsFolder, List<string> references ,bool runInSeparateAppDomain)
        {        	
        	var methodFilter = "";
        	var useCachedData = true;        	
        	var numberOfMethodsToProcess = 1000;
        	return calculateMethodMappings(sourceFolder, resultsFolder, methodFilter, useCachedData, references, numberOfMethodsToProcess,runInSeparateAppDomain);
        }
        
        public static string calculateMethodMappings(string sourceFolder,string resultsFolder, string methodFilter, bool useCachedData, List<string> references, int numberOfMethodsToProcess, bool runInSeparateAppDomain)
        {    
        	if (resultsFolder.valid().isFalse())
        		 resultsFolder = "_AstEngine_MethodMappings".tempDir();
        	resultsFolder.createDir();	 
        	var rawResultsFolder = 	 resultsFolder.pathCombine("_PartialMethodMappings");
        	rawResultsFolder.createDir();
	        var continueScan = true;			
			while(continueScan) 
			{
				"Executing MethodMappings_Engine for {0} methods".debug(numberOfMethodsToProcess);				
				var resultsFile = (runInSeparateAppDomain)
									? MethodMappings_Engine.executeEngineOnSeparateAppDomain(sourceFolder, rawResultsFolder, methodFilter ,useCachedData, references, numberOfMethodsToProcess)
									: new MethodMappings_Engine().createMethodMappings(sourceFolder, rawResultsFolder, methodFilter ,useCachedData, references, numberOfMethodsToProcess); 
				if (resultsFile.isNull())
					break;
				else								
					if (resultsFile.xRoot().elementsAll().size() < 6)
						break;	
				"NUMBER OF ELEMENTS:{0}".error(resultsFile.xRoot().elementsAll().size());										
			}
			var consolidatedFile = resultsFolder.pathCombine("ConsolidatedMethodMappings.xml");
			if (consolidatedFile.fileExists().isFalse())
			{
				var consolidatedMethodMappings = rawResultsFolder.files("*.xml").loadAndMergeMethodMappings();
				consolidatedMethodMappings.SourceCodeFolder = sourceFolder;
				consolidatedMethodMappings.ResultsFolder = resultsFolder;
				consolidatedMethodMappings.MethodFilter = methodFilter;
				consolidatedMethodMappings.References = references;
			
				consolidatedMethodMappings.saveMappings(consolidatedFile);
			}
			// create consolidated
			return resultsFolder;
        }
    }

}
