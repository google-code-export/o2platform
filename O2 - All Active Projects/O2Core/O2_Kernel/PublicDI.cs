// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System.Collections.Generic;
using O2.Kernel.CodeUtils;
using O2.Kernel.InterfacesBaseImpl;
using O2.Kernel.Objects;

namespace O2.Kernel
{
    /// <summary>
    /// These are public DI objects which can be used and manipulated by O2 modules 
    /// For example the log one is a good candidate for the GUI controls to take over
    /// </summary>
    public static class PublicDI
    {
        static PublicDI()
        {
            log = (KO2Log)DI.log;
            config = (KO2Config)DI.config;
            reflection = (KReflection) DI.reflection;
            o2MessageQueue = (KO2MessageQueue)DI.o2MessageQueue;
            appDomainsControledByO2Kernel = DI.appDomainsControledByO2Kernel;
            O2KernelProcessName = DI.O2KernelProcessName;
           /* // all these variables need to be setup
            appDomainsControledByO2Kernel = new Dictionary<string, O2AppDomainFactory>();            
            log = new KO2Log();
            reflection = new KReflection();
            o2MessageQueue = KO2MessageQueue.getO2KernelQueue();            
            O2KernelProcessName = DI.O2KernelProcessName;

            // before we load the O2Config data (which is loaded from the local disk)
            config = O2ConfigLoader.getKO2Config(); 
            */
            
            
        }

        public static KO2Log log { get; set; }
        public static KO2Config config { get; set; }
        public static KReflection reflection { get; set; }
        public static KO2MessageQueue o2MessageQueue { get; set; }
        public static Dictionary<string, O2AppDomainFactory> appDomainsControledByO2Kernel;
        public static string O2KernelProcessName { get; set; }
                        
    }
}
