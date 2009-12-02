using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace O2.External.WinFormsUI.Forms
{
    public static class O2AscxGUI_Ext
    {
        public static object invokeOnAscx(this string ascxName, string methodToExecute)
        {
            return invokeOnAscx(ascxName, methodToExecute, new object[0]);
        }

        public static object invokeOnAscx(this string ascxName, string methodToExecute, object[] methodParameters)
        {
            return O2AscxGUI.invokeOnAscxControl(ascxName, methodToExecute, methodParameters);
        }
    }
}
