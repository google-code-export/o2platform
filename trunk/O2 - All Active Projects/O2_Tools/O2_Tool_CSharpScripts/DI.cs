using O2.External.WinFormsUI.O2Environment;
using O2.Kernel.Interfaces;
using O2.Kernel.Interfaces.Messages;
using O2.Kernel.Interfaces.O2Core;
using O2.Kernel.InterfacesBaseImpl;

namespace O2.Tool.CSharpScripts
{
    class DI
    {

        static DI()
        {
            log = new WinFormsUILog();
            config = new KO2Config();
                       
            o2MessageQueue = KO2MessageQueue.getO2KernelQueue();
        }

        public static IO2Log log { get; set; }
        public static IO2Config config { get; set; }
        //public static ISearchEngineAPI searchEngineAPI { get; set; }        

        public static IO2MessageQueue o2MessageQueue;
    }
}