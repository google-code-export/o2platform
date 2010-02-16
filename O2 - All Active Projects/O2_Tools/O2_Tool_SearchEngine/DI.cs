// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using O2.External.WinFormsUI.O2Environment;
using O2.Interfaces.Messages;
using O2.Interfaces.O2Core;
using O2.Kernel;
using O2.Kernel.InterfacesBaseImpl;


namespace O2.Tool.SearchEngine
{
    class DI
    {
        static DI()
        {
            log = PublicDI.log;// new WinFormsUILog();            
            config = PublicDI.config; //new KO2Config();
            
            searchEngineAPI = new DotNetWrappers.SearchApi.SearchEngine();            
            o2MessageQueue = KO2MessageQueue.getO2KernelQueue();
            searchEngineAPI = new DotNetWrappers.SearchApi.SearchEngine();
        }
        
        public static IO2Log log { get; set;}
        public static IO2Config config { get; set;}
        public static DotNetWrappers.SearchApi.SearchEngine searchEngineAPI { get; set; }        

        public static IO2MessageQueue o2MessageQueue;

        //public static IReflection reflection = new KReflection();
    }
}
