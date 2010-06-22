using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using O2.DotNetWrappers.ExtensionMethods;
using System.Windows.Forms;

namespace O2.API.Visualization.ExtensionMethods
{
    public static class ElementHost_ExtensioMethods
    {
        public static Control getHost(this string xamlFileName)
        {
            if (xamlFileName.fileExists())
            { }
            return null;
        }


    }
}
