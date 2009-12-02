using System;
using System.IO;
using O2.Kernel.InterfacesBaseImpl;
using O2.Scanner.OunceLabsCLI.Utils;

namespace O2.Scanner.OunceLabsCLI.ScanTargets
{
    public class ScanTarget_Java : KScanTarget
    {
        public String fileName;

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
            return fileName;
        }


        private void setupScanEnvironment()
        {
            try
            {
                string project = ScanSupport.Java.createTempProjectFileForProject(Target, WorkDirectory, false);
                ApplicationFile = ScanSupport.createTempApplicationFileForProject(project, false, WorkDirectory);
                fileName = Path.GetFileName(Target);                
            }
            catch (Exception ex)
            {
                DI.log.ex(ex, "in ScanTarget_Java.setupScanEnvironment");
            }
        }        
    }
}