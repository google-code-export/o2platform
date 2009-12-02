using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using O2.Kernel.Objects;

namespace O2.UnitTests.Test_O2Kernel.Test_O2AppDomainFactory
{
    [TestFixture]
    public class Test_CreateAppDomains
    {
        [Test]
        public void createAppDomainWithPreloadedDlls()
        {
            var appDomainName = "AppDomin with preloaded Dlls";
            var appDomainTempDirectory = DI.config.TempFolderInTempDirectory;
            var assembliesToLoadInNewAppDomain = new List<string> {"O2_Kernel.exe", "O2_DotNetWrappers.dll" };
            var o2AppDomainFactory = new O2AppDomainFactory(appDomainName, appDomainTempDirectory, assembliesToLoadInNewAppDomain);
            Assert.That(o2AppDomainFactory != null, "o2AppDomainFactory was null");
            DI.log.info("AppDomain base directory:{0}", o2AppDomainFactory.BaseDirectory); 
            Assert.That(o2AppDomainFactory.Name == appDomainName, "o2AppDomainFactory.Name != appDomainName");
//            Assert.That(o2AppDomainFactory.AssembliesInBaseDirectory.Count ==2, "There should only be two Assemblies in AppDomain Directory"
            //.createAppDomainWithDlls(newAppDomain, dllToLoadInNewAppDomain);
        }
    }
}