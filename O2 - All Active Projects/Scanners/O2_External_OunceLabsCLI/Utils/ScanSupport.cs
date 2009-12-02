using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using O2.DotNetWrappers.Windows;


namespace O2.Scanner.OunceLabsCLI.Utils
{
    public class ScanSupport
    {
        // create temp Assessment files (*.ppf.paf) for projects to scan (this allows the scan of projects without needing an Application file
        public static void createTempApplicationFileForProjects(Dictionary<String, String> dProjects)
        {
            var lKeys = new String[dProjects.Keys.Count];
            dProjects.Keys.CopyTo(lKeys, 0);
            foreach (String sProject in lKeys)
            {
                String sNewApplication = sProject + "_tempApplication_.paf";
                if (createTempApplicationFileForProject(sProject, sNewApplication))
                    dProjects[sProject] = sNewApplication;
            }
        }

        public static String createTempApplicationFileForProject(String sProjectPath)
        {
            return createTempApplicationFileForProject(sProjectPath, true, Path.GetDirectoryName(sProjectPath));
        }

        public static String createTempApplicationFileForProject(String sProjectPath, bool bAddTempStringToFileName, string targetDirectory)
        {
            String sTempApplicationPath = String.Format("{0}\\{1}{2}.paf", targetDirectory,
                                                        (bAddTempStringToFileName) ? "_o2temp_" : "",
                                                        Path.GetFileNameWithoutExtension(sProjectPath));
            if (createTempApplicationFileForProject(sProjectPath, sTempApplicationPath))
                return sTempApplicationPath;
            else
                return "";
        }

        public static bool createTempApplicationFileForProject(String sProjectPath, String sApplicationPath)
        {
            String sLanguageType = fromPpfFile_get_LanguageType(sProjectPath);
            if (Path.GetDirectoryName(sProjectPath) == Path.GetDirectoryName(sApplicationPath))
                sProjectPath = Path.GetFileName(sProjectPath);

            if (sLanguageType != "")
            {
                String sApplicationName = Path.GetFileNameWithoutExtension(sProjectPath);
                String sApplicationFileContents = String.Format(
                    "<?xml version=\"1.0\" encoding=\"UTF-8\"?>" + Environment.NewLine +
                    "<Application name=\"{0}\">" + Environment.NewLine +
                    "\t<Project path=\"{1}\" language_type=\"{2}\"/>" + Environment.NewLine +
                    "</Application>",
                    sApplicationName,
                    sProjectPath,
                    sLanguageType);
                Files.WriteFileContent(sApplicationPath, sApplicationFileContents);
                return true;
            }
            DI.log.error("in createTempApplicationFileForProject: could not calculate the language type");
            return false;
        }

        public static void deleteTempAssessmentsForProjects(Dictionary<String, String> dProjects)
        {
            foreach (String sProject in dProjects.Keys)
                if (File.Exists(dProjects[sProject]))
                    File.Delete(dProjects[sProject]);
        }

        public static String fromPpfFile_get_LanguageType(String sPpfFile)
        {
            try
            {
                var xdAssessmentFile = new XmlDocument();

                xdAssessmentFile.Load(sPpfFile);
                XmlNodeList xnlAssessmentStats = xdAssessmentFile.GetElementsByTagName("Project");
                if (xnlAssessmentStats.Count != 1)
                    DI.log.error("fromPpfFile_get_LanguageType: xnlAssessmentStats.Count != 1 : {0}",
                                 xnlAssessmentStats.Count);
                else
                {
                    if (xnlAssessmentStats[0].Attributes["language_type"] != null)
                        return xnlAssessmentStats[0].Attributes["language_type"].Value;
                    else
                        DI.log.error(
                            "fromPpfFile_get_LanguageType: language_type attribute not found in Project Element");
                }
            }
            catch (Exception ex)
            {
                DI.log.error("fromPpfFile_get_LanguageType: {0}", ex.Message);
            }
            return "";
        }

        public static String createTempProjectFileForFile(String fileToScan, String targetPath,String languageType, bool addTempStringToFileName)
        {
            return createTempProjectFileForFiles(new List<string> {fileToScan}, targetPath, languageType,addTempStringToFileName);
        }

        public static String createTempProjectFileForFiles(List<String> filesToScan, String targetPath, String sLanguageType, bool bAddTempStringToFileName)
        {
            if (filesToScan != null && filesToScan.Count > 0)
            {

                var firstFileToScan = filesToScan[0];        // try to use the first one to calculate the location of the new project file
                String sTempProjectFile = Path.Combine(targetPath,
                                                       (bAddTempStringToFileName)
                                                           ? "_o2temp_ProjectFile_"
                                                           : "" + Path.GetFileName(firstFileToScan) + ".ppf");
                String projetFileContents = String.Format(
                    "<?xml version=\"1.0\" encoding=\"UTF-8\"?>" + Environment.NewLine +
                    "<Project cma_behavior=\"default\" cma_compute_limit=\"500\" cma_memory_limit=\"20\" default_configuration_name=\"Configuration 1\" filter_findings_to_project=\"false\" " + 
                    "language_type=\"{0}\" name=\"O2_Scan\" perform_cma=\"true\">" + Environment.NewLine +
                    "<Configuration exclude_from_build=\"false\" inherit_from_project=\"false\" name=\"Configuration 1\"/>" + Environment.NewLine, 
                    sLanguageType);
                foreach(var fileToScan in filesToScan)
                {
                    String pathToFileToScan = (Path.GetDirectoryName(fileToScan) == targetPath)
                                                      ? Path.GetFileName(fileToScan)
                                                      : fileToScan;
                    projetFileContents+= string.Format("<Source exclude=\"false\" path=\"{0}\" web=\"false\"/>" + Environment.NewLine , 
                                    pathToFileToScan);
                }
                projetFileContents += "</Project>";

                Files.WriteFileContent(sTempProjectFile, projetFileContents);
                return sTempProjectFile;
            }
            return "";
        }
        
        public class Java
        {
            public static String createTempProjectFileForProject(String sPathToDll, String sTargetPath)
            {
                return createTempProjectFileForProject(sPathToDll, sTargetPath, true);
            }

            public static String createTempProjectFileForProject(String sPathToDll, String sTargetPath,
                                                                 bool bAddTempStringToFileName)
            {
                const string sLanguageType = "2"; // 2 for Java
                return createTempProjectFileForFile(sPathToDll, sTargetPath, sLanguageType, bAddTempStringToFileName);
            }
        }

        #region Nested type: DotNet

        public class DotNet
        {
            public static String createTempProjectFileForProject(String sPathToDll, String sTargetPath)
            {
                return createTempProjectFileForProject(sPathToDll, sTargetPath, true);
            }

            public static String createTempProjectFileForProject(String sPathToDll, String sTargetPath,
                                                                 bool bAddTempStringToFileName)
            {
                const string sLanguageType = "12"; // needs to be 12 for current version of 6.0                
                return createTempProjectFileForFile(sPathToDll, sTargetPath, sLanguageType, bAddTempStringToFileName);


                /*String sTempProjectFile = Path.Combine(sTargetPath,
                                                       (bAddTempStringToFileName)
                                                           ? "_o2temp_ProjectFile_"
                                                           : "" + Path.GetFileName(sPathToDll) + ".ppf");
                String sDllToScan = (Path.GetDirectoryName(sPathToDll) == sTargetPath)
                                        ? Path.GetFileName(sPathToDll)
                                        : sPathToDll;
                String sProjetFileContents = String.Format(
                    "<?xml version=\"1.0\" encoding=\"UTF-8\"?>" + Environment.NewLine +
                    "<Project cma_behavior=\"default\" cma_compute_limit=\"500\" cma_memory_limit=\"20\" default_configuration_name=\"Configuration 1\" filter_findings_to_project=\"false\" language_type=\"{0}\" name=\"O2_Scan\" perform_cma=\"true\">" +
                    Environment.NewLine +
                    "<Configuration exclude_from_build=\"false\" inherit_from_project=\"false\" name=\"Configuration 1\"/>" +
                    Environment.NewLine +
                    "<Source exclude=\"false\" path=\"{1}\" web=\"false\"/>" + Environment.NewLine +
                    "</Project>", sLanguageType, sDllToScan);
                Files.WriteFileContent(sTempProjectFile, sProjetFileContents);
                return sTempProjectFile;*/
            }
        }

        #endregion
    }
}