using System;
using System.IO;
using O2.DotNetWrappers.Windows;
using O2.Scanner.OunceLabsCLI.Scan;

namespace O2.Scanner.OunceLabsCLI.ScanTargets
{
    public class dotNetScan
    {
        public static bool scanSolution(String sPathToSolutionFile, String sPathToSaveAssessmentFile)
        {
            if (Path.GetExtension(sPathToSolutionFile) != ".sln")
                DI.log.error("Only .sln files are supported: {0}", sPathToSolutionFile);
            else
            {
                return new CliScanning().scanApplication(sPathToSolutionFile, sPathToSaveAssessmentFile);
            }
            return false;
        }

        public static bool scanProject(String sPathToProject, string sPathToSaveAssessmentFile)
        {
            if (Path.GetExtension(sPathToProject) != ".csproj")
                DI.log.error("Only .csproj files are supported: {0}", sPathToProject);
            else
            {
                String sTempProjectFile = String.Format("{0}\\__tempProjectFile_{1}",
                                                        Path.GetDirectoryName(sPathToProject),
                                                        Path.GetFileName(sPathToProject));
                File.Copy(sPathToProject, sTempProjectFile);
                String sTempProjectScanFile = createTempProjectScanFileFor_csproj(sTempProjectFile);
                //
                bool bResult = new CliScanning().scanProject(sTempProjectFile, sPathToSaveAssessmentFile);
                File.Delete(sTempProjectFile);
                return bResult;
            }
            return false;
        }


        public static String createTempProjectScanFileFor_csproj(String sSourceFile)
        {
            // dot net projects are either of type 4 or 6
            return createTempProjectScanFileFor_csproj(sSourceFile, Path.GetFileNameWithoutExtension(sSourceFile), "4");
        }

        public static String createTempProjectScanFileFor_csproj(String sSourceFile, String sName, String sLanguageType)
        {
            String sFileContents = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>" + Environment.NewLine;
            sFileContents +=
                String.Format(
                    "<Project name=\"{0}\" language_type=\"{1}\" default_configuration_name=\"Debug\" cma_memory_limit=\"20\" cma_compute_limit=\"50\" perform_cma=\"true\" cma_behavior=\"default\"/>\n",
                    sName, sLanguageType);
            String sTempProjectScanFile = sSourceFile + ".gpf";
            Files.WriteFileContent(sTempProjectScanFile, sFileContents);
            return sTempProjectScanFile;
        }
    }
}