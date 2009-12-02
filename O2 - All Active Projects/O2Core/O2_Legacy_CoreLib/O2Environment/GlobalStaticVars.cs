// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows.Forms;

namespace O2.Legacy.CoreLib.O2Core.O2Environment
{
    public class GlobalStaticVars
    {
        // might need to add a clear to this data since it will not reflect source code changes


        // use to store catched version of resolved signatures           

        public static Dictionary<String, Type> dO2Controls = new Dictionary<string, Type>();

        public static Dictionary<String, Object> dO2ExposedMethodsForDynamicInvokation =
            new Dictionary<string, object>();

        public static Dictionary<String, List<Type>> dO2LoadedAddOns = new Dictionary<string, List<Type>>();
        public static Dictionary<String, Assembly> dO2LoadedDlls = new Dictionary<String, Assembly>();
        public static Dictionary<String, Form> dO2LoadedForms = new Dictionary<String, Form>();

        //public static Dictionary<String, List<Mdi.mdiChild>> dO2MdiViews = new Dictionary<string, List<Mdi.mdiChild>>();

        // to be used as a temp var place by o2 O2JavaScript


        //public static O2GUI fO2;

        public static List<String> lsModulesLoadedInCurrentProcess = new List<string>();
        //public static O2GuiWithDockPanel o2GuiWithDockPanel;
        public static StatusStrip ssO2StatusStrip_RightHandDropZone;
    }
}
