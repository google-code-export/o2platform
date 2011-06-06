// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System.Collections.Generic;
using FluentSharp.O2.Interfaces.Messages;
using FluentSharp.O2.Interfaces.O2Core;
using FluentSharp.O2.Kernel.CodeUtils;
using FluentSharp.O2.Kernel.InterfacesBaseImpl;
using FluentSharp.O2.Kernel.O2CmdShell;
using FluentSharp.O2.Kernel.Objects;

//O2File:O2KernelCmdShell/O2Shell.cs
//O2File:Objects/O2AppDomainFactory.cs
//O2File:CodeUtils/O2Kernel_Web.cs
//O2File:CodeUtils/O2ConfigLoader.cs
//O2File:CodeUtils/AppDomainUtils.cs
//O2File:InterfacesBaseImpl/KO2Log.cs

namespace FluentSharp.O2.Kernel
{
    internal static class DI
    {        
        static DI()
        {               
            //Apply .NET Network Connection hack
            O2Kernel_Web.ApplyNetworkConnectionHack();

            // all these variables need to be setup
            appDomainsControledByO2Kernel = new Dictionary<string, O2AppDomainFactory>();
            log = new KO2Log();
            reflection = new KReflection();
            o2MessageQueue = KO2MessageQueue.getO2KernelQueue();
            
            // before we load the O2Config data (which is loaded from the local disk)
            config = O2ConfigLoader.getKO2Config();
            
            //make sure theses values are set (could be a prob due to changed location of these values)
            if (config.LocalScriptsFolder == null)
            {
                config.LocalScriptsFolder = KO2Config.defaultLocalScriptFolder;
                config.SvnO2RootFolder = KO2Config.defaultSvnO2RootFolder;
                config.SvnO2DatabaseRulesFolder = KO2Config.defaultSvnO2DatabaseRulesFolder;
            }
            
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
