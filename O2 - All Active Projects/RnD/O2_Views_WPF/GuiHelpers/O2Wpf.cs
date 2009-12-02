// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.Collections.Generic;
using System.Windows.Controls;
using O2.Debugger.Mdbg.NewCode;
using O2.Kernel.CodeUtils;
using O2.Kernel.Objects;

namespace O2.Rnd.Views.Wpf.GuiHelpers
{
    public class O2Wpf
    {
        internal static void populateWpfListBoxWithO2AppDomainFactoryObjectsWithCurrentAppDomains(
            ListBox lbCurrentAppDomains)
        {
            IList<AppDomain> currentAppDomains = ViaMscoree.GetAppDomains();
            lbCurrentAppDomains.Items.Clear();
            foreach (AppDomain appDomain in currentAppDomains)
            {
                if (appDomain != AppDomain.CurrentDomain)
                {
                    string localCopyOfo2ProxyAssembly = AppDomain.CurrentDomain.Load("O2_Kernel").Location;
                    O2Kernel_Files.Copy(localCopyOfo2ProxyAssembly,
                                        appDomain.BaseDirectory);
                }
                var o2Proxy =
                    (O2Proxy)
                    appDomain.CreateInstanceAndUnwrap("O2_Kernel", "o2.o2AppDomainProxy.Objects.O2Proxy");
                //var o2AppDomainFactory = o2Proxy.getO2AppDomainFactoryWithCurrentAppDomain();
                if (o2Proxy != null)
                    //var o2AppDomainFactory = O2AppDomainFactory 
                    lbCurrentAppDomains.Items.Add(o2Proxy);
                else
                {
                }
      
            }
      
        }
    }
}
