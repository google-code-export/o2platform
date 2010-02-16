// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System.Collections.Generic;
using O2.Interfaces.Messages;
using O2.Interfaces.O2Core;
using O2.Kernel.CodeUtils;
using O2.Kernel.InterfacesBaseImpl;
using O2.Kernel.O2CmdShell;
using O2.Kernel.Objects;

namespace O2.Kernel
{
    internal static class DI
    {        
        static DI()
        {
            //O2KernelProcessName = "Generic O2 Kernel Process";
            //config = PublicDI.config;
            //log = PublicDI.log;
            //reflection = PublicDI.reflection;
            //o2MessageQueue = PublicDI.o2MessageQueue;
            //appDomainsControledByO2Kernel = PublicDI.appDomainsControledByO2Kernel;
            //AppDomainUtils.registerCurrentAppDomain();

            // all these variables need to be setup
            appDomainsControledByO2Kernel = new Dictionary<string, O2AppDomainFactory>();
            log = new KO2Log();
            reflection = new KReflection();
            o2MessageQueue = KO2MessageQueue.getO2KernelQueue();
            

            // before we load the O2Config data (which is loaded from the local disk)
            config = O2ConfigLoader.getKO2Config();

            O2KernelProcessName = "Generic O2 Kernel Process"; ;
            AppDomainUtils.registerCurrentAppDomain();
        }

        // DI targets
        public static IO2Config config { get; set; }
        public static IO2Log log { get; set; }      
        public static IReflection reflection { get; set; }
        public static IO2MessageQueue o2MessageQueue { get; set; }        
        public static O2Shell o2Shell { get; set;}

        // local global variables
        public static string O2KernelProcessName { get; set; }

        public static Dictionary<string, O2AppDomainFactory> appDomainsControledByO2Kernel;
    }
}
