// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.Collections.Generic;
using System.IO;
//using System.Linq;
using System.Reflection;
using FluentSharp.O2.Interfaces.O2Core;
using FluentSharp.O2.Kernel.InterfacesBaseImpl;
using FluentSharp.O2.Kernel.Objects;

//O2File:../PublicDI.cs

namespace FluentSharp.O2.Kernel.CodeUtils
{
    public class AppDomainUtils
    {
        private static readonly IO2Log log = new KO2Log("AppDomainUtils");

        public static string findDllInCurrentAppDomain(string dllToFind)
        {
            foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
                if (assembly.GetName().Name == dllToFind)
                    return assembly.Location;
            log.error("in findDllInCurrentAppDomain, could not find {0} in the current AppDomain assemblies", dllToFind);
            return "";
        }

        public static IEnumerable<string> getDllsInCurrentAppDomain_FullPath()
        {
            var dllList = new List<String>();
            foreach(var assembly in AppDomain.CurrentDomain.GetAssemblies())
                dllList.Add(assembly.Location);
            return dllList;
            //return from assembly in AppDomain.CurrentDomain.GetAssemblies() select assembly.Location;                                            
        }

        public static void closeAppDomain(AppDomain appDomain, bool deleteFilesInBaseDirectory)
        {
            log.info("Unloading AppDomain:{0}", appDomain.FriendlyName);
            string baseDirectory = appDomain.BaseDirectory;
            AppDomain.Unload(appDomain);
            if (deleteFilesInBaseDirectory)
            {
                log.info("Deleting all files from AppDomain BaseDirectory : {0}", baseDirectory);
                Directory.Delete(baseDirectory, true);
            }
        }

        public static void renameCurrentO2KernelProcessName(string newO2KernelProcessName)
        {
            var o2AppDomainFactory = DI.appDomainsControledByO2Kernel[DI.O2KernelProcessName];
            //o2AppDomainFactory.appDomain.FriendlyName = newAppDomainName; // can't do this since there is no Setter for the FiendlyName property
            DI.appDomainsControledByO2Kernel.Remove(newO2KernelProcessName);
            DI.O2KernelProcessName = newO2KernelProcessName;
            DI.appDomainsControledByO2Kernel.Add(DI.O2KernelProcessName, o2AppDomainFactory);
        }

        public static void registerCurrentAppDomain()
        {
            try
            {
                DI.appDomainsControledByO2Kernel.Add(DI.O2KernelProcessName, new O2AppDomainFactory(AppDomain.CurrentDomain));
            }
            catch (Exception ex)
            {
                DI.log.error("in registerCurrentAppDomain: {0}", ex.Message);
            }
            
        }

        public static O2AppDomainFactory getO2AppDomainFactoryForCurrentO2Kernel()
        {
            return DI.appDomainsControledByO2Kernel[DI.O2KernelProcessName];
        }        
    }
}
