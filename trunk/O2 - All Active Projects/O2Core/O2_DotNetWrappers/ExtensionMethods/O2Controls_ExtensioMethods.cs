using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using O2.Kernel.ExtensionMethods;

namespace O2.DotNetWrappers.ExtensionMethods
{
    // these are controls created via reflection (only available if the target dll is locally available
    public static class O2Controls_ExtensioMethods
    {
        public static Control add_Browser(this Control hostControl)
        {
            var browserType = "O2_External_IE.dll".type("O2BrowserIE");
            return hostControl.add_Control(browserType);             
        }

        //public static Control add_WebBrowser(this Control hostControl)
        //{
        //
        //}

        /*public static object add_Graph(this Control hostControl)
        {
            return hostControl.createAndInvokeUsingExtensionMethod("O2_API_Visualization.dll", "WPF_WinFormIntegration_ExtensionMethods", "add_Graph", null, null, null);
            //return hostControl.add_XRule("ascx_GraphWithInspector");
        }*/
        
        public static Control add_ProcessDetails(this Control hostControl)
        {
            return hostControl.add_XRule("ascx_Running_Processes_Details");
        }

        public static Control add_ProcessStop(this Control hostControl)
        {
            return hostControl.add_XRule("ascx_Processes_Stop");
        }

        public static Control add_ServicesStop(this Control hostControl)
        {
            return hostControl.add_XRule("ascx_Services_Stop");
        }

        public static Control add_StartTools(this Control hostControl)
        {
            return hostControl.add_XRule("ascx_Start_Tools");
        }

        public static Control add_XRule(this Control hostControl, string xRuleName)
        {
            return hostControl.add_XRuleDatabase_Control(xRuleName);
        }

        public static Control add_XRuleDatabase_Control(this Control hostControl, string xRuleName)
        {
            var controlType = "O2_XRules_Database.exe".type(xRuleName);
            return hostControl.add_Control(controlType);
        }


        public static Control add_ControlUsingExtensionMethod(this Control hostControl, string assembly, string extensionMethodClass, string ctorMethod, object ctorData, string setMethod, object setData)
        {
            var type = assembly.type(extensionMethodClass);
            var newControl = (ctorData != null)
                           ? type.invokeStatic(ctorMethod, hostControl, ctorData)
                           : type.invokeStatic(ctorMethod, hostControl);
            if (newControl is Control)
            {
                if (setMethod != null)
                    type.invokeStatic(setMethod, newControl, setData);
                return (Control)newControl;
            }
            return null;
        }

        public static object createAndInvokeUsingExtensionMethod(this Control hostControl, string assembly, string extensionMethodClass, string ctorMethod, object ctorData, string setMethod, object setData)
        {
            var type = assembly.type(extensionMethodClass);
            var newControl = (ctorData != null)
                           ? type.invokeStatic(ctorMethod, hostControl, ctorData)
                           : type.invokeStatic(ctorMethod, hostControl);

            if (setMethod != null)
                type.invokeStatic(setMethod, newControl, setData);
            return newControl;

            return null;
        }
    }
}
