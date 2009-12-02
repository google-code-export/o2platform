using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using O2.AppScan.AppScanDE.Xsd;
using O2.DotNetWrappers.DotNet;
using O2.DotNetWrappers.O2Findings;
using O2.DotNetWrappers.Windows;
using O2.DotNetWrappers.Zip;
using O2.Kernel.Interfaces.O2Findings;

namespace O2.ImportExport.Misc.AppScanDE
{
    public class O2AssesmentLoad_AppScanDE :  IO2AssessmentLoad
    {
        public string engineName { get; set; }
        public O2AssesmentLoad_AppScanDE()
        {
            engineName = "O2AssesmentLoad_AppScanDE";
        }

        public bool canLoadFile(string fileToTryToLoad)
        {
            return Path.GetExtension(fileToTryToLoad) == ".srpt" || Path.GetExtension(fileToTryToLoad) == ".zip" || Path.GetExtension(fileToTryToLoad) == ".xml";
        }

        public IO2Assessment loadFile(string fileOrFolderToLoad)
        {
            var scanFile ="";            
            var tempUnzipFolder = DI.config.getTempFolderInTempDirectory("_AppscanDE_Unzip");
            if (Path.GetExtension(fileOrFolderToLoad) == ".xml")
            {
                scanFile = fileOrFolderToLoad;
            }
            else
            {
                var filesToSearch = new List<string>();
                if (Directory.Exists(fileOrFolderToLoad))
                    filesToSearch = Files.getFilesFromDir_returnFullPath(fileOrFolderToLoad, "*.xml", true);
                else
                {
                    //fileToLoad = Files.MoveFile(fileToLoad, (fileToLoad + ".zip"));

                    // Path.GetFileNameWithoutExtension(fileToLoad));
                    filesToSearch = new zipUtils().unzipFileAndReturtListOfUnzipedFiles(fileOrFolderToLoad,
                                                                                        tempUnzipFolder);
                }

                if (filesToSearch.Count == 0)
                    DI.log.error("in O2AssesmentLoad_AppScanDE.loadFile, unzip operation failed for file: {0}", fileOrFolderToLoad);
                else
                    foreach (var file in filesToSearch)
                        if (
                            file.IndexOf(@"com.ibm.rational.appscan.ui.security.runnable.codeAnalysis.staticAnalysis\securityResultSet.xml") >-1)
                            scanFile = file;
                //scanFile = Path.Combine(tempFolder, @"com.ibm.rational.appscan.ui.security.runnable.codeAnalysis.staticAnalysis\securityResultSet.xml");                
            }
            if (false == File.Exists(scanFile))
            {
                DI.log.error("Cound not find AppScanDE static analysis file: {0}", scanFile);
                return new O2Assessment();
            }

            IO2Assessment o2Assessment = null;
            var appScanDEResults = Serialize.getDeSerializedObjectFromXmlFile(scanFile, typeof(taintResultSet));
            if (appScanDEResults != null && appScanDEResults is taintResultSet)
                o2Assessment = createO2AssessmentFromCodeCrawlerObject((taintResultSet)appScanDEResults, Path.GetFileNameWithoutExtension(scanFile));

            Files.deleteFolder(tempUnzipFolder);
            return o2Assessment;

        }

        private IO2Assessment createO2AssessmentFromCodeCrawlerObject(taintResultSet appScanDEResultsFile, String fileName)
        {
            var o2Assessment = new O2Assessment();
            o2Assessment.name = "AppScan Import of: " + fileName;            
            var o2Findings = new List<IO2Finding>();
            foreach (taintResultSetTaintResult resultSet in appScanDEResultsFile.TaintResult)
            {
                //log.info(" id: {0} {1} {2}", resultSet.id, resultSet.issueID, resultSet.userSeverity);
                var o2Finding = new O2Finding();
                o2Finding.vulnName = resultSet.issueID;
                o2Finding.vulnType = resultSet.issueID;
                //o2Finding.severity = resultSet.userSeverity;				
                var sourceNode = new O2Trace(resultSet.taintSource.className + "." + resultSet.taintSource.methodName + resultSet.taintSource.methodSignature);
                sourceNode.traceType = TraceType.Source;
                //sourceNode.file = resultSet.taintSource.fileName;
                var lastNode = sourceNode;
                foreach (var taintStep in resultSet.taintStep)
                {
                    var stepNode = new O2Trace(taintStep.className + "." + taintStep.methodName + taintStep.methodSignature);

                    // set filename and line number for step trace:
                    stepNode.file = taintStep.fileName;
                    stepNode.lineNumber = taintStep.highlight.lineNumber;
                    if (taintStep.snippetText != null)
                    {
                        var splittedText = taintStep.snippetText.Split(new[] { '\n' });
                        var lineIndex = taintStep.highlight.lineNumber - taintStep.snippetStartLine;
                        if (taintStep.snippetText != "")
                        {
                            stepNode.context = (lineIndex > -1) ? splittedText[lineIndex - 1] : taintStep.snippetText;
                            stepNode.context = "> " + stepNode.context.Replace("\t", " ").Trim() + "                                                      \n\n  --------  \n\n" + taintStep.snippetText;
                        }
                    }
                    // make the finding have the values of the last taitstep
                    o2Finding.file = taintStep.fileName;
                    o2Finding.lineNumber = taintStep.highlight.lineNumber;

                    // set childnodes
                    lastNode.childTraces.Add(stepNode);
                    lastNode = stepNode;
                }

                var sinkNode = new O2Trace(resultSet.taintSink.className + "." + resultSet.taintSink.methodName + resultSet.taintSink.methodSignature);
                sinkNode.traceType = TraceType.Known_Sink;
                //sinkNode.file = resultSet.taintSink.fileName;

                lastNode.childTraces.Add(sinkNode);



                o2Finding.o2Traces.Add(sourceNode);
                o2Findings.Add(o2Finding);


                o2Assessment.o2Findings.Add(o2Finding);
            }
            return o2Assessment;
        }

        public bool importFile(string fileToLoad, IO2Assessment o2Assessment)
        {
            var loadedO2Assessment = loadFile(fileToLoad);
            if (loadedO2Assessment!= null)
            {
                o2Assessment.o2Findings = loadedO2Assessment.o2Findings;
                return true;
            }
            return false;
        }

    }
}