// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using O2.Kernel.Interfaces.Controllers;

namespace O2.Rnd.Tool.ScanQueue.Utils
{
    public class ScanThread
    {
        public bool active;
        public IScanTarget scanTarget;
        //public bool active;

        public ScanThread()
        {
        }

        public ScanThread(string xmlSerializedScanTargetObject)
        {
        }

        public ScanThread(IScanTarget target)
        {
            scanTarget = scanTarget;
        }

        public void startScan()
        {
            //CliScanning.
        }
    }
}
