// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
//O2Ref:System.dll
using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Collections.Generic;
//O2Ref:System.Windows.Forms.dll
using System.Windows.Forms;
//O2Ref:O2_DotNetWrappers.dll
using O2.DotNetWrappers.DotNet;
using O2.DotNetWrappers.O2Findings;
using O2.DotNetWrappers.Windows;
using O2.DotNetWrappers.ExtensionMethods;
//O2Ref:O2_ImportExport_OunceLabs.dll
using O2.ImportExport.OunceLabs.Ozasmt_OunceV6;
//O2Ref:O2_Kernel.dll
using O2.Interfaces.O2Core;
using O2.Interfaces.O2Findings;
using O2.Kernel;
using O2.Kernel.ExtensionMethods;
//O2Ref:O2_Views_ASCX.dll
//O2Ref:O2_Interfaces.dll

using O2.Views.ASCX.O2Findings;

namespace O2.XRules.Database.Findings
{
    public static class Findings_ExtensionMethods_OpenAndLoad
    {
        private static readonly IO2Log log = PublicDI.log;

        public static Thread openFindingsInNewWindow(this List<IO2Finding> o2Findings)
        {
            return ascx_FindingsViewer.openInFloatWindow(o2Findings);
        }

        public static Thread openFindingsInNewWindow(List<IO2Finding> o2Findings, string windowTitle)
        {
            return ascx_FindingsViewer.openInFloatWindow(o2Findings, windowTitle);
        }


        public static List<IO2Finding> loadFindingsFile(this string fileToLoad)
        {
            var o2Assessment = new O2Assessment(new O2AssessmentLoad_OunceV6(), fileToLoad);
            log.info("there are {0} findings loaded in this file", o2Assessment.o2Findings.Count);
            return o2Assessment.o2Findings;
        }
        
        public static string saveFindings(this List<IO2Finding> o2Findings)
        {
            var savedFile = new O2Assessment(o2Findings).save(new O2AssessmentSave_OunceV6()); 
            log.info("Assessemnt File saved with {0} findings: {1}", o2Findings.Count, savedFile);
            return savedFile;
        }
        public static void saveFindings(this List<IO2Finding> o2Findings, string pathToSaveFindings)
        {
        	new O2Assessment(o2Findings).save(new O2AssessmentSave_OunceV6(), pathToSaveFindings); 
        	log.info("Assessemnt File saved with {0} findings: {1}", o2Findings.Count, pathToSaveFindings);
        }

        public static List<IO2Finding> loadFindingsFiles(this List<string> filesToLoad)
        {
            var loadedFindings = new List<IO2Finding>();
            foreach (var fileToLoad in filesToLoad)
            {
                var o2Findings = loadFindingsFile(fileToLoad);
                loadedFindings.AddRange(o2Findings);
            }
            log.info("Total # of findings loaded: {0}", loadedFindings);
            return loadedFindings;
        }


        // Description: this function loads multiple Ozasmt Files (not recursively)
        public static List<IO2Finding> loadMultipleOzasmtFiles(this string pathToOzastmFilesToLoad)
        {
            return loadMultipleOzasmtFiles(pathToOzastmFilesToLoad,"*.ozasmt",  false);
        }

        // Description: this function loads multiple Ozasmt Files (recursively or not, based on filter)
        public static List<IO2Finding> loadMultipleOzasmtFiles(this string pathToOzastmFilesToLoad, string filter, bool searchRecursively)
        {
            var o2Findings = new List<IO2Finding>();
            if (Directory.Exists(pathToOzastmFilesToLoad))
                foreach (var fileToLoad in Files.getFilesFromDir_returnFullPath(pathToOzastmFilesToLoad, filter, searchRecursively))
                {
                    log.info("loading findings from file: {0}", fileToLoad);
                    o2Findings.AddRange(loadFindingsFile(fileToLoad));
                }
            return o2Findings;
        }
	}


	public static class Findings_ExtensionMethods_Filtering
	{
		//[XRule(Name="All findings")]
        public static  List<IO2Finding> allFindings(this List<IO2Finding> o2Findings)
        {        	
            return o2Findings;        	
        }

        //[XRule(Name="Only Findings With Traces")]
        public static List<IO2Finding> onlyTraces(this List<IO2Finding> o2Findings)
        {        	
            return 
                (from IO2Finding o2Finding in o2Findings 
                 where o2Finding.o2Traces.Count > 0  select o2Finding).ToList();
            //return o2Assesment.o2Findings;
        }

		
        //[XRule(Name="Only.findings.where.vulnName.CONTAINS")]
        public static  List<IO2Finding> whereVulnName_Contains(this List<IO2Finding> o2Findings, string text)
        {        	
            return 
                (from IO2Finding o2Finding in o2Findings
                 where o2Finding.vulnName.IndexOf(text) > -1
                 select o2Finding).ToList();            
        }

        //[XRule(Name = "Only.findings.where.vulnName.IS")]
        public static  List<IO2Finding> whereVulnName_Is(this List<IO2Finding> o2Findings, string text)
        {
            return
                (from IO2Finding o2Finding in o2Findings
                 where o2Finding.vulnName == text
                 select o2Finding).ToList();
        }

        //[XRule(Name = "Only.findings.where.Source.IS")]
        public static  List<IO2Finding> whereSource_Is(this List<IO2Finding> o2Findings, string text)
        {
            return
                (from O2Finding o2Finding in o2Findings
                 where o2Finding.Source == text 
                 select (IO2Finding)o2Finding).ToList();
        }

        //[XRule(Name = "Only.findings.where.Source.CONTAINS")]
        public static  List<IO2Finding> whereSource_Contains(this List<IO2Finding> o2Findings, string text)
        {
            return
                (from O2Finding o2Finding in o2Findings
                 where o2Finding.Source.IndexOf(text) > -1
                 select (IO2Finding)o2Finding).ToList();
        }

        //[XRule(Name = "Only.findings.where.Sink.IS")]
        public static  List<IO2Finding> whereSink_Is(this List<IO2Finding> o2Findings, string text)
        {
            return
                (from O2Finding o2Finding in o2Findings
                 where o2Finding.Sink == text
                 select (IO2Finding)o2Finding).ToList();
        }

        //[XRule(Name = "Only.findings.where.Sink.CONTAINS")]
        public static  List<IO2Finding> whereSink_Contains(this List<IO2Finding> o2Findings, string text)
        {
            return
                (from O2Finding o2Finding in o2Findings
                 where o2Finding.Sink.IndexOf(text) > -1
                 select (IO2Finding)o2Finding).ToList();
        }

        //[XRule(Name = "Only.findings.where.Context.CONTAINS")]
        public static  List<IO2Finding> whereContext_Contains(this List<IO2Finding> o2Findings, string text)
        {
            return
                (from O2Finding o2Finding in o2Findings
                 where o2Finding.context.IndexOf(text) > -1
                 select (IO2Finding)o2Finding).ToList();
        }

        //[XRule(Name = "Only.findings.where.SourceAndSink.CONTAINS.Regex")]
        /*(public static  List<IO2Finding> whereSourceAndSink_ContainsRegex(List<IO2Finding> o2Findings, string source, string sink )
        {
            return o2Findings.calculateFindings(source, sink);
        }*/
	}
	
	
	public static class Findings_ExtensionMethods_Joining
	{
		public static IO2Finding copy(this IO2Finding o2Finding)
		{
			return OzasmtCopy.createCopy(o2Finding);
		}
		
		public static Dictionary<string,List<IO2Finding>> indexBy(this List<IO2Finding> o2Findings, Func<O2Finding,string> calculateIndex)
		{
			var indexedData = new Dictionary<string,List<IO2Finding>>();
			foreach(var o2Finding in o2Findings)
				indexedData.add(calculateIndex((O2Finding)o2Finding),o2Finding);
			return indexedData;
		}
		
		public static Dictionary<string,List<IO2Finding>> indexBy(this List<IO2Finding> o2Findings, string propertyToIndexBy)
		{
			var indexedData = new Dictionary<string,List<IO2Finding>>();
			foreach(var o2Finding in o2Findings)
				indexedData.add(o2Finding.prop(propertyToIndexBy).str(),o2Finding);
			return indexedData;
		}
		
		public static Dictionary<string,List<IO2Finding>> indexBy_Sink(this List<IO2Finding> o2Findings)
		{
			return o2Findings.indexBy("Sink");
		}
		
		public static Dictionary<string,List<IO2Finding>> indexBy_Source(this List<IO2Finding> o2Findings)
		{
			return o2Findings.indexBy("Source");
		}
		
		public static List<IO2Trace> pathToSink(this IO2Finding o2Finding)
		{
			var pathToTraceType = new List<IO2Trace>();
    		OzasmtUtils.getPathToTraceType(o2Finding.o2Traces, TraceType.Known_Sink, pathToTraceType);
    		return pathToTraceType;
		}
		public static IO2Trace firstThatStartsWith(this List<IO2Trace> o2Traces, string text)
		{
			foreach(var o2Trace in o2Traces)
				if (o2Trace.str().starts(text))
					return o2Trace;
			return null;
		}
		
	}
	
	public static class Findings_ExtensionMethods_FindingsTrasformation
	{
		public static List<IO2Finding> removeFirstSource(this List<IO2Finding> o2Findings)
		{
			foreach(O2Finding o2Finding in o2Findings)
				o2Finding.getSource().traceType = TraceType.Type_4;
			return o2Findings;	
		}
	}
	
	public static class Findings_ExtensionMethods_DotNet_FindingsTransformations
	{
		public static List<IO2Finding> getFindingsWith_WebServicesInvoke(this List<IO2Finding> o2Findings)
		{
			var webServicesInvokeMethod = "method -> protected object[] System.Web.Services.Protocols.SoapHttpClientProtocol.Invoke(string methodName, object[] parameters)";			
			return o2Findings.whereSink_Is(webServicesInvokeMethod);
		}
			
		public static List<IO2Finding> makeSinks_WebServicesInvokeTarget(this List<IO2Finding> o2Findings)
		{			
			foreach(O2Finding o2Finding in o2Findings)
			{				
				var pathToSink = o2Finding.pathToSink(); 
				if (pathToSink.size() > 0)
				{
					pathToSink.RemoveAt(0);					
					var firstMatch = pathToSink.firstThatStartsWith("method");					
						if(firstMatch.notNull())
						{
							o2Finding.getSink().traceType = TraceType.Lost_Sink; 
							firstMatch.traceType = TraceType.Known_Sink;
						}
				}
			}
			return o2Findings;
		}
	}
}
