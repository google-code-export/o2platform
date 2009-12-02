// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using O2.DotNetWrappers.O2Findings;
using O2.DotNetWrappers.Windows;
using O2.External.WinFormsUI.Forms;
using O2.Kernel.Interfaces.O2Core;
using O2.Kernel.Interfaces.O2Findings;
using O2.Views.ASCX.O2Findings;

namespace O2.Cmd.FindingsFilter.SampleScripts
{
    public class parse_Jsf_xHtml_Files
    {
        public static string rootDirectoryOfFiles = @"";
        public static string fileExtension = "*.xHtml";

        public static IO2Log log = O2.Kernel.PublicDI.log;

        public static void processXHtmlfiles()
        {
            if (rootDirectoryOfFiles == "")
                log.error("you must provide the base directory (rootDirectoryOfFiles) to find {0} files", fileExtension);
            else
            {


                var o2Findings = new List<IO2Finding>();
                var filesToProcess = Files.getFilesFromDir_returnFullPath(rootDirectoryOfFiles, fileExtension, true
                    /*recursive search */);
                log.info("There are {0} {1} files to process", filesToProcess.Count, fileExtension);
                foreach (var file in filesToProcess)
                {
                    var o2FindingsFromFile = processXHtmlfile(file);
                    o2Findings.AddRange(o2FindingsFromFile);
                }
                log.info("There were {0} findings created", o2Findings.Count);

                // showing findings in Findings Viewer
                showFindingsInFindingsViewer(o2Findings);
                log.info("done...");
            }
        }

        public static List<IO2Finding> processXHtmlfile(string xHtmlFileToLoad)
        {
            var o2Findings = new List<IO2Finding>();
            if (!File.Exists(xHtmlFileToLoad))
                log.error("Could not find file to load");
            else
            {
                log.info("Loading file:{0}", xHtmlFileToLoad);

                // using LINQ's Xdocument
                XDocument doc = XDocument.Load(xHtmlFileToLoad, LoadOptions.SetLineInfo);

                processXElements(doc.Elements(), o2Findings, xHtmlFileToLoad);

            }
            return o2Findings;
        }

        public static void processXElements(IEnumerable<XElement> xElements, List<IO2Finding> o2Findings, string currentFile)
        {
            foreach (var xElement in xElements)
            {
                var newO2Finding = createO2FindingForXElement(xElement, currentFile);
                if (newO2Finding != null)
                    o2Findings.Add(newO2Finding);
                processXElements(xElement.Elements(), o2Findings, currentFile);
            }
        }

        public static IO2Finding createO2FindingForXElement(XElement xElement, string currentFile)
        {

            var createFinding = false;
            var xElementSignature = xElement.Name.LocalName;
            var sourceTrace1 = new O2Trace();
            var sourceTrace2 = new O2Trace();
            foreach (var attribute in xElement.Attributes())
            {
                if (attribute.Name == "value" && attribute.Value.IndexOf("#{") > -1)
                {
                    createFinding = true;
                    sourceTrace1.signature = attribute.Value;
                    sourceTrace2.signature = getFilteredValue(attribute.Value);
                    sourceTrace2.traceType = TraceType.Source;
                    // set source file and line number
                    var attributeLineInfo = (IXmlLineInfo)attribute;
                    sourceTrace2.file = currentFile;
                    sourceTrace2.lineNumber = (uint)attributeLineInfo.LineNumber;

                }
                //
                xElementSignature += String.Format(" {0}=\"{1}\" ", attribute.Name, attribute.Value);
            }
            if (createFinding == false)
                return null;

            var newFinding = new O2Finding();
            newFinding.vulnType = "JSF.AutoMapping";
            newFinding.vulnName = xElementSignature;

            // add traces

            // root trace
            var rootTrace = new O2Trace(currentFile);
            newFinding.o2Traces.Add(rootTrace);
            // traceWithFileName
            var traceWithFileName = new O2Trace(Path.GetFileName(currentFile));
            rootTrace.childTraces.Add(traceWithFileName);
            // sourceTrace
            traceWithFileName.childTraces.Add(sourceTrace1);
            sourceTrace1.childTraces.Add(sourceTrace2);
            // traceWithNamespaceAndElementName
            var traceWithNamespaceAndElementName = new O2Trace(xElement.Name.ToString());
            sourceTrace2.childTraces.Add(traceWithNamespaceAndElementName);
            // Sink trace (with xElementSignature contents)
            var sinkTrace = new O2Trace(xElementSignature);
            sinkTrace.traceType = TraceType.Known_Sink;
            traceWithNamespaceAndElementName.childTraces.Add(sinkTrace);

            // set file and line number for sink & finding
            var elementLineInfo = (IXmlLineInfo)xElement;
            newFinding.file = sourceTrace2.file = currentFile;
            newFinding.lineNumber = sourceTrace2.lineNumber = (uint)elementLineInfo.LineNumber;
            return newFinding;
        }

        public static string getFilteredValue(string valueToFilter)
        {
            var indexOfFirstBracket = valueToFilter.IndexOf("#{");
            var indexOfLastBracket = valueToFilter.LastIndexOf("}");
            if (indexOfFirstBracket == -1 || indexOfLastBracket == -1 || indexOfLastBracket < indexOfFirstBracket)
                return valueToFilter;

            indexOfFirstBracket += 2;
            return valueToFilter.Substring(indexOfFirstBracket, indexOfLastBracket - indexOfFirstBracket);
        }

        public static void showFindingsInFindingsViewer(List<IO2Finding> findingsToShow)
        {
            var findingsViewerControl = (ascx_FindingsViewer)O2AscxGUI.getAscx("Findings Viewer");
            findingsViewerControl.clearO2Findings();
            findingsViewerControl.loadO2Findings(findingsToShow);
        }
    }
}
