// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System.IO;
using O2.Core.CIR.CirCreator.DotNet;
using O2.Core.CIR.CirObjects;
using O2.DotNetWrappers.DotNet;
using O2.DotNetWrappers.Windows;
using O2.Interfaces.CIR;
using O2.Views.ASCX.CoreControls;

namespace O2.Core.CIR.Ascx
{
    partial class ascx_CirCreator
    {        
        private void setUpCirCreationDirectories()
        {
            directory_CirCreationQueue.openDirectory(DI.defaultDirectoryForCirCreationQueue);
            directory_CreatedCirFiles.openDirectory(DI.defaultDirectoryForCreatedCirFiles);
        }

        private void copyFileToCirCreationQueue(string fileToCopy)
        {
            Files.Copy(fileToCopy, directory_CirCreationQueue.getCurrentDirectory());
        }

        private void deleteFilesFromCirCreationQueueDirectory()
        {
            Files.deleteAllFilesFromDir(directory_CirCreationQueue.getCurrentDirectory());
        }

        private void deleteFilesFromCreatedCirFileDirectory()
        {
            Files.deleteAllFilesFromDir(directory_CreatedCirFiles.getCurrentDirectory());
        }
        

        public ascx_Directory getDirectoryControlFor_CirCreationQueue()
        {
            return directory_CirCreationQueue;
        }

        public ascx_Directory getDirectoryControlFor_CreatedCirFiles()
        {
            return directory_CreatedCirFiles;
        }

        public void processCirCreationQueue()
        {
            if (directory_CirCreationQueue.getFiles().Count > 0)
            {
                O2Thread.mtaThread(() =>
                                       {
                                           foreach (var fileInQueue in directory_CirCreationQueue.getFiles())
                                           {
                                               // first copy the assembly file to CreatedCirFiles directory
                                               var fileToProcess = Files.MoveFile(fileInQueue, directory_CreatedCirFiles.getCurrentDirectory());
                                               // start CirCreation thread
                                               createCirDataForFile(fileToProcess, true /*deleteFileOnCompletion*/, false /*decompileCodeIfNoPdb*/);                                               
                                           }
                                           processCirCreationQueue(); // call it again just in case there was another file in there
                                       });
            }
        }

        private void createCirDataForFile(string fileToProcess, bool deleteFileOnCompletion, bool decompileCodeIfNoPdb)
        {
            O2Thread.mtaThread(() =>
            {
                ICirData assemblyCirData = new CirData();
                new CirFactory().processAssemblyAndSaveAsCirDataFile(assemblyCirData, fileToProcess, directory_CreatedCirFiles.getCurrentDirectory(), decompileCodeIfNoPdb);
                if (deleteFileOnCompletion)
                {
                    File.Delete(fileToProcess);
                }
            })
            ;

            //new CirCreatorEngineForDotnet().createCirForAssembly(fileToProcess);
        }
    }
}
