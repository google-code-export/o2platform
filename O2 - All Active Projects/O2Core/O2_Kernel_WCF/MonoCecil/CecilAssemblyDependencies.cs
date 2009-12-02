using System;
using System.Collections.Generic;
using O2.External.O2Mono.MonoCecil;
using O2.Kernel.WCF.classes;

namespace O2.Kernel.WCF.MonoCecil
{
    public class WCFCecilAssemblyDependencies
    {
        public static void createAppDomainViaWcfClient(O2WcfClient wcfClient, Type typeToLoadWithAllDependencies)
        {
            createAppDomainViaWcfClient(wcfClient, new List<Type> { typeToLoadWithAllDependencies });
        }

        public static void createAppDomainViaWcfClient(O2WcfClient wcfClient, List<Type> typesToLoadWithAllDependencies)
        {
            List<String> dependentAssemblies = CecilAssemblyDependencies.getListOfDependenciesForTypes(typesToLoadWithAllDependencies);

            wcfClient.createRemoteAppDomainWithAssemblies(wcfClient.RemoteAppDomainName, new List<string>(dependentAssemblies));
        }
    }
}
