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