// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.Collections.Generic;
using NUnit.Framework;
using O2.Debugger.Mdbg.NewCode;
using O2.Kernel.Objects;

namespace O2.UnitTests.Test_O2Debugger.Misc
{
    [TestFixture]
    public class Test_MultipleAppDomains
    {
        private const string assemblyName = "O2_Kernel";
        //private const string typeToCreate = "O2.Kernel.Objects.O2Proxy";
        private const string typeToCreateSimpleName = "O2Proxy";

        [Test]
        public void test_CheckForCreationOfMultipleProxys()
        {
            IList<AppDomain> currentAppDomains = ViaMscoree.GetAppDomains();
            DI.log.debug("currentAppDomains : {0} ", currentAppDomains.Count);
            foreach (AppDomain appDomain in currentAppDomains)
                DI.log.info(appDomain.FriendlyName);
            //Assert.That(currentAppDomains.Count == 2, "There should only be two appdomain Loaded");
            O2AppDomainFactory.getProxy(assemblyName, typeToCreateSimpleName + " " + assemblyName);
            O2AppDomainFactory.getProxy(assemblyName, typeToCreateSimpleName + " " + assemblyName);
            O2AppDomainFactory.getProxy(assemblyName, typeToCreateSimpleName + " " + assemblyName);

            IList<AppDomain> newListOfAppDomains = ViaMscoree.GetAppDomains();
            DI.log.debug("newListOfAppDomains: {0} ", newListOfAppDomains.Count);
            foreach (AppDomain appDomain in newListOfAppDomains)
                DI.log.info(appDomain.FriendlyName);
            Assert.That(newListOfAppDomains.Count == currentAppDomains.Count + 3,
                        "The number of AppDomains should be the original count + 3");
            //var proxy = O2AppDomainFactory.getProxy(assemblyName, typeToCreateSimpleName);            
        }
    }
}
