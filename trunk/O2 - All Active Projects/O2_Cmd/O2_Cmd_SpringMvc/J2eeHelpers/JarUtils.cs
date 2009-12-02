// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using O2.Core.CIR.CirUtils;
using O2.DotNetWrappers.Windows;
using O2.DotNetWrappers.Zip;

namespace O2.Cmd.SpringMvc.J2eeHelpers
{
    public class JarUtils
    {
        public static string test()
        {
            return "Hello there";
        }

        public static void calculateJarsForJSP(string cirDataFile, string folderWithMappedJars, List<string> jarSearchPaths, List<string> logFilesToParse)
        {
            DI.log.info("in calculateJarsForJSP");
            var missingClasses = new List<string>();
            // Load the CirData file
            var cirData = CirLoad.loadSerializedO2CirDataObject(cirDataFile);
            if (cirData == null)
            {
                DI.log.error("Could not load CirData file");
                return;
            }
            //log.debug("There are {0} classes loaded from the cirData file", cirData.dClasses_bySignature.Count);
            missingClasses.AddRange(cirData.dClasses_bySignature.Keys);

            // get the list of mapped clases (to Jars) 	
            var mappedClasses = mapAvailableJars(jarSearchPaths);

            // get some extra dependencies form the log file:			
            foreach (var logFileToParse in logFilesToParse)
                missingClasses.AddRange(getMissingClassesFromLogFile(logFileToParse));

            //

            // calculate dependencies
            var dependencies = getDependencies(missingClasses, mappedClasses);

            copyDependenciesJarsIntoJarsFolder(dependencies, folderWithMappedJars);
            DI.log.info("Dependencies files mapped to folder: {0}", folderWithMappedJars);
        }

        public static List<string> getMissingClassesFromLogFile(string logFile)
        {
            return getMissingClassesFromLogFile(logFile, "Failed to find class: ");
        }

        public static List<string> getMissingClassesFromLogFile(string logFile, string startStringToMatch)
        {
            var missingClasses = new List<string>();
            var linesInLogFile = Files.getFileLines(logFile);
            DI.log.info("There are {0} lines in this file", linesInLogFile.Count);
            
            foreach (var line in linesInLogFile)
                if (line.StartsWith(startStringToMatch))
                {
                    var className = line.Replace(startStringToMatch, "");
                    if (false == missingClasses.Contains(className))
                    {
                        DI.log.info("  -   " + className);
                        missingClasses.Add(className);
                    }
                }
            return missingClasses;
        }

        public static void copyDependenciesJarsIntoJarsFolder(Dictionary<string, List<string>> dependencies, string dependentJarFolder)
        {
            Files.deleteAllFilesFromDir(dependentJarFolder);				// delete files
            Files.checkIfDirectoryExistsAndCreateIfNot(dependentJarFolder);	// make sure directory exists

            foreach (var classFile in dependencies.Keys)
                if (dependencies[classFile] != null)
                {
                    if (dependencies[classFile].Count == 1)
                    {
                        Files.Copy(dependencies[classFile][0], dependentJarFolder);
                    }
                    else
                    {
                        DI.log.debug("NOTE: there was more than one jar mapped to the class:{0}", classFile);
                        foreach (var jarFile in dependencies[classFile])
                        {
                            Files.Copy(jarFile, dependentJarFolder);
                            DI.log.info("    {0}", jarFile);
                        }
                    }
                }
                else
                {
                    // if there is no mapping for this class file create a temp text file
                    var tempTxtFile = Path.Combine(dependentJarFolder,
                                                    string.Format("_missingclass {0}        .txt", classFile.Replace("/", ".")));
                    Files.WriteFileContent(tempTxtFile, "");
                }
        }

        public static Dictionary<string, List<string>> getDependencies(List<string> missingClasses, Dictionary<string, List<string>> mappedClasses)
        {
            var dependencies = new Dictionary<string, List<string>>();
            foreach (var missingClass in missingClasses)
            {
                var fixedName = missingClass.Replace(".", "/") + ".class";		// map class name to the Jars' file path format
                if (mappedClasses.ContainsKey(fixedName))
                    dependencies.Add(fixedName, mappedClasses[fixedName]);
                //log.info("Found class: {0}", fixedName);
                else
                    dependencies.Add(fixedName, null);
                //log.error("Count not find class: {0}", fixedName);

                //log.debug(cirClassName);
            }
            return dependencies;
        }

        public static Dictionary<string, List<string>> mapAvailableJars(List<string> jarPaths)
        {
            DI.log.info("Search recursively for all *.jar files");
            var availableJars = new List<string>();
            foreach (var jarSearchPath in jarPaths)
                availableJars.AddRange(Files.getFilesFromDir_returnFullPath(jarSearchPath, "*.jar", true));

            DI.log.info("Found {0} jar files", availableJars.Count);

            var mappedClasses = new Dictionary<string, List<string>>();
            foreach (var jarFile in availableJars)
            {
                var filesInJar = new zipUtils().getListOfFilesInZip(jarFile);
                foreach (var fileInJar in filesInJar)
                    if (Path.GetExtension(fileInJar) == ".class")
                    {
                        if (false == mappedClasses.ContainsKey(fileInJar))
                            mappedClasses.Add(fileInJar, new List<string>());
                        mappedClasses[fileInJar].Add(jarFile);
                    }
            }
            DI.log.info("There are {0} classes mapped", mappedClasses.Keys.Count);
            return mappedClasses;
        }
    	
    	
    }
}
