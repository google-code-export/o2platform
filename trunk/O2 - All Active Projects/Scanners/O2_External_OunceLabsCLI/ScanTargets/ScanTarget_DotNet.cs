// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.IO;
using O2.Kernel.InterfacesBaseImpl;
using O2.Scanner.OunceLabsCLI.Utils;

namespace O2.Scanner.OunceLabsCLI.ScanTargets
{
    public class ScanTarget_DotNet : KScanTarget
    {
//        public String project;
//        public String application;        
        //public String fileToTest;
        public String dllInTempDirectory;
        public String fileName;
        public String pdbFile;
        public String pdbInTempDirectory;


        public override string Target
        {
            get { return base.Target; }
            set
            {
                base.Target = value;
                setupScanEnvironment();
            }
        }

        public override string ToString()
        {
            return fileName ?? "";  // this solves the VERY NASTY '...Attempted to read or write protected memory..' issue that O2 
                                     // had on (what looked like) ramdom places. It looks like this was created when an object is placed on a 
                                     // listbox and its ToString method returns null
        }


        private void setupScanEnvironment()
        {
            try
            {
                string project = ScanSupport.DotNet.createTempProjectFileForProject(Target, WorkDirectory, false);
                ApplicationFile = ScanSupport.createTempApplicationFileForProject(project, false, WorkDirectory);
                fileName = Path.GetFileName(Target);
                // todo: move this code to a function to create a Scan Package for .net (so normally scan from the local place
                /*

                //      Files.deleteFolder(sTempDirectory);                
                dllInTempDirectory = Files.Copy(Target, Path.Combine(workDirectory, Path.GetFileName(Target)));                               
                // setup temp project file
                project = ScanSupport.DotNet.createTempProjectFileForProject(dllInTempDirectory, workDirectory, false);
                // setup temp Application file
                application = ScanSupport.createTempApplicationFileForProject(project, false);
                // check for pdb file
                pdbFile = Target.Replace(Path.GetExtension(Target), ".pdb");
                if (false == File.Exists(pdbFile))
                     DI.log.debug("Could not find pdb file, there might be problems scanning this dll");
                else
                    pdbInTempDirectory = Files.Copy(pdbFile, Path.Combine(workDirectory, Path.GetFileName(pdbFile)));
                 * */
            }
            catch (Exception ex)
            {
                DI.log.ex(ex, "in ScanTarget_DotNet.setupScanEnvironment");
            }
        }

        public bool havePdb()
        {
            return (File.Exists(pdbFile)) ? true : false;
        }
    }
}
