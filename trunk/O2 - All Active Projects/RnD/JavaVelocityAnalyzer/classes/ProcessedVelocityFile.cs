using System;
using System.Collections.Generic;
using System.IO;
using O2.DotNetWrappers.Windows;
using O2.Rnd.JavaVelocityAnalyzer;


namespace O2.Rnd.JavaVelocityAnalyzer.classes
{
    public class ProcessedVelocityFile
    {
        //
        public bool bIgnoreComments;
        public bool bLoadedOk;
        public List<String> lsFileLines = new List<string>();
        public List<velocity.VelocityNode> lvnVelocityNodes = new List<velocity.VelocityNode>();
        public String sFileContents;
        public String sFileName;
        public String sFullPathToOriginalFile;
        public String sFullPathToProcessedFile;
        public String sRootDirectory;


        public ProcessedVelocityFile(string sProcessedVelocityFile, String sRootDirectory)
        {
            if (File.Exists(sProcessedVelocityFile) &&
                velocityloader.isFileAVelocityProcessedFile(sProcessedVelocityFile))
                bLoadedOk = loadDataAndPopulateObjects(sProcessedVelocityFile, sRootDirectory);
        }

        private bool loadDataAndPopulateObjects(String sFileToLoad, String sRootDirectory)
        {
            try
            {
                sFullPathToProcessedFile = sFileToLoad;
                sFileName = Path.GetFileName(sFullPathToProcessedFile);
                sFileContents = Files.getFileContents(sFullPathToProcessedFile);
                this.sRootDirectory = sRootDirectory;
                sFullPathToOriginalFile =
                    sFullPathToProcessedFile.Replace(velocityloader.sExtensionOfProcessedVelocityFiles,
                                                     velocityloader.sExtensionOfVelocityFiles);
                if (false == File.Exists(sFullPathToOriginalFile))
                    sFullPathToOriginalFile = ""; // make it "" if the original file is not there

                lsFileLines = Files.getFileLines(sFullPathToProcessedFile);
                foreach (String sLine in lsFileLines)
                {
                    var vnVelocityNode = new velocity.VelocityNode(sLine);
                    lvnVelocityNodes.Add(vnVelocityNode);
                }
                return true;
            }
            catch (Exception Ex)
            {
                DI.log.error("In loadDataAndPopulateObjects: {0}", Ex.Message);
                return false;
            }
        }

        public String getNormalizedFileName()
        {
            return ToString().Replace('/', '.').Replace('\\', '.');
        }

        public override string ToString()
        {
            if (sRootDirectory != "")
                return sFullPathToProcessedFile.Replace(sRootDirectory, "");
            else
                return sFullPathToProcessedFile;
        }

        public List<String> getVars()
        {
            var lsVars = new List<string>();
            for (int i = 0; i < lvnVelocityNodes.Count; i++)
                if (lvnVelocityNodes[i].ntNodeType == velocity.NodeType.ASTReference &&
                    lvnVelocityNodes[i + 1].ntNodeType != velocity.NodeType.ASTMethod)
                    lsVars.Add(lvnVelocityNodes[i].sAllTokens);
            return lsVars;
        }

        public List<String> getFunctions()
        {
            var lsFunctions = new List<string>();
            for (int i = 0; i < lvnVelocityNodes.Count; i++)
                if (lvnVelocityNodes[i].ntNodeType == velocity.NodeType.ASTReference && (i + 1) < lvnVelocityNodes.Count)
                {
                    //lsMethods.Add(lvnVelocityNodes[i].sAllTokens);
                    //  String sMethodName = lvnVelocityNodes[i].sRootToken ;
                    for (int j = i + 1; j < lvnVelocityNodes.Count; j++)
                    {
                        if ((lvnVelocityNodes[j].iDepth == lvnVelocityNodes[i].iDepth + 1) &&
                            (lvnVelocityNodes[j].ntNodeType == velocity.NodeType.ASTMethod))
                        {
                            //         sMethodName += "." + lvnVelocityNodes[j].sRootToken;

                            lsFunctions.Add(lvnVelocityNodes[i].sRootToken + "." + lvnVelocityNodes[j].sAllTokens);
                        }
                        if (lvnVelocityNodes[j].iDepth == lvnVelocityNodes[i].iDepth)
                            break;
                    }
                }
            return lsFunctions;
        }

        public List<String> getDirectives()
        {
            var lsDirectives = new List<string>();
            foreach (velocity.VelocityNode vnVelocityNode in lvnVelocityNodes)
                if (vnVelocityNode.ntNodeType == velocity.NodeType.ASTDirective)
                    lsDirectives.Add(vnVelocityNode.sRootToken);
            return lsDirectives;
        }

        public List<String> getReferencesToOtherVmFiles()
        {
            var lsReferencesToVmFiles = new List<string>();
            for (int i = 0; i < lvnVelocityNodes.Count; i++)
                if (lvnVelocityNodes[i].sRootToken.IndexOf(velocityloader.sExtensionOfVelocityFiles) > -1)
                {
                    String sReference = String.Format("{0}  :   {1}", lvnVelocityNodes[i - 1].sRootToken,
                                                      lvnVelocityNodes[i].sRootToken);
                    lsReferencesToVmFiles.Add(sReference);
                }
            return lsReferencesToVmFiles;
        }
    }
}