using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using O2.DotNetWrappers.DotNet;
using O2.DotNetWrappers.O2Findings;
using O2.DotNetWrappers.Windows;
using O2.ImportExport.Misc.Xsd;
using O2.Kernel.Interfaces.O2Findings;

namespace O2.ImportExport.Misc.FindBugs
{
    public class O2AssesmentLoad_FindBugs :  IO2AssessmentLoad
    {
        public string engineName { get; set; }
        public O2AssesmentLoad_FindBugs()
        {
            engineName = "O2AssesmentLoad_FindBugs";
        }

        // rewrite in a more optimized way
        public bool canLoadFile(string fileToTryToLoad)
        {
            if (Path.GetExtension(fileToTryToLoad) == ".xml")
                return Serialize.getDeSerializedObjectFromXmlFile(fileToTryToLoad, typeof(Xsd.BugCollection)) != null;
            return false;
        }

        public bool importFile(string fileToLoad, IO2Assessment o2Assessment)
        {
            var loadedO2Assessment = loadFile(fileToLoad);
            if (loadedO2Assessment != null)
            {
                o2Assessment.o2Findings = loadedO2Assessment.o2Findings;
                return true;
            }
            return false;
        }

        public IO2Assessment loadFile(string fileToLoad)
        {
            var findBugsObject = Serialize.getDeSerializedObjectFromXmlFile(fileToLoad, typeof (Xsd.BugCollection));
            if (findBugsObject == null || false == findBugsObject is Xsd.BugCollection)
                return null;
            return createO2AssessmentFromFindBugsObject((Xsd.BugCollection)findBugsObject, Path.GetFileNameWithoutExtension(fileToLoad));            
        }

        private IO2Assessment createO2AssessmentFromFindBugsObject(Xsd.BugCollection findBugsObject, String fileName)
        {            
            var o2Assessment = new O2Assessment();
            o2Assessment.name = "FindBugs Import of: " + fileName;
            foreach (var bug in findBugsObject.BugInstance)            
            {
                var o2Finding = new O2Finding
                                    {
                                        vulnName = bug.type,
                                        vulnType = bug.category + "." + bug.abbrev,
                                        severity = bug.priority,
                                        confidence = 2                                                                                
                                    };
             //   o2Finding.text.Add(threat.Description);
                
                foreach (var item in bug.Items)
                {
                    var o2Trace = new O2Trace();                    
                    switch (item.GetType().Name)
                    {
                        case "BugCollectionBugInstanceClass":
                            var clazz = (BugCollectionBugInstanceClass) item;
                            
                            o2Trace.signature = "Class: " + clazz.classname;
                            o2Trace.context = "Class: " + clazz.role;

                            o2Trace.file = tryToResolveFullFilePath(clazz.SourceLine.sourcepath, findBugsObject);
                            o2Trace.lineNumber = 0;
                            break;

                        case "BugCollectionBugInstanceSourceLine":
                            var sourceLine = (BugCollectionBugInstanceSourceLine)item;
                            o2Trace.signature = "SourceLine: " + sourceLine.sourcefile + "  on line " + sourceLine.start;
                            o2Trace.file = tryToResolveFullFilePath(sourceLine.sourcepath , findBugsObject);
                            o2Trace.lineNumber = sourceLine.start;
                            break;

                        case "BugCollectionBugInstanceMethod":
                            var method = (BugCollectionBugInstanceMethod)item;
                            o2Trace.signature = "Method:  + " + method.signature;
                            o2Trace.file = tryToResolveFullFilePath(method.SourceLine.sourcepath, findBugsObject);
                            o2Trace.lineNumber = method.SourceLine.start;
                            break;

                        case "BugCollectionBugInstanceClassSourceLine":
                            o2Trace.signature = "ClassSourceLine";                                                        
                            break;
                        case "BugCollectionBugInstanceField":
                            o2Trace.signature = "Field";
                            break;
                        case "BugCollectionBugInstanceFieldSourceLine":
                            o2Trace.signature = "FieldSourceLine";
                            break;                        
                        case "BugCollectionBugInstanceMethodSourceLine":
                            o2Trace.signature = "MethodSourceLine";
                            break;                        
                        case "BugCollectionBugInstanceInt":
                            o2Trace.signature = "Int";
                            break;
                        case "BugCollectionBugInstanceLocalVariable":
                            o2Trace.signature = "LocalVariable";
                            break;
                        case "BugCollectionBugInstanceString":
                            o2Trace.signature = "String";
                            break;
                        case "BugCollectionBugInstanceProperty":
                            o2Trace.signature = "Property";
                            break;
                        case "BugCollectionBugInstanceType":
                            o2Trace.signature = "Type";
                            break;
                        case "BugCollectionBugInstanceTypeSourceLine":
                            o2Trace.signature = "TypeSourceLine";
                            break;
                        case "Object":          // ignore it
                            break;
                        default:                            
                            o2Trace.signature = item.GetType().Name;


                            break;
                    }
                    o2Finding.o2Traces.Add(o2Trace);
                }
                o2Assessment.o2Findings.Add(o2Finding);
            }
            return o2Assessment;
        }

        private string tryToResolveFullFilePath(string partialFilePath, BugCollection findBugsObject)
        {
            if (partialFilePath != null && findBugsObject.Project != null && findBugsObject.Project.SrcDir != null)
                foreach (var srcDir in findBugsObject.Project.SrcDir)
                {
                    var filePath = Path.Combine(srcDir, partialFilePath);
                    if (File.Exists(filePath))
                        return filePath;
                }
            return "";
        }

        
    }
}