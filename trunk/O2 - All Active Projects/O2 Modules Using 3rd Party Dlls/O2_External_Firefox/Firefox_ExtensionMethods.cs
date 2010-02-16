using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using O2.DotNetWrappers.DotNet;
using O2.DotNetWrappers.ExtensionMethods;
using O2.External.Firefox.WebAutomation;
using O2.Kernel;
using Skybound.Gecko;

namespace O2.External.Firefox
{
    public static class Firefox_ExtensionMethods
    {
        public static GeckoWebBrowser add_Firefox2(this Control control)
        {
        	PublicDI.log.debug("in add_Firefox");
            return (GeckoWebBrowser) control.invokeOnThread(
                 () =>
                     {
                     	PublicDI.log.debug("in add_Firefox: Thread");
                         var firefoxWebBrowser = FirefoxXul.createGeckoWebBrowser();
                         if (firefoxWebBrowser != null)
                         {
                             PublicDI.log.info(firefoxWebBrowser.GetType().FullName);
                             control.Controls.Add(firefoxWebBrowser);
                             return firefoxWebBrowser;
                         }
                         PublicDI.log.error("Could not create Firefox WebBrowser control");
                         
                         return null;
                     });
        }
    }
}
