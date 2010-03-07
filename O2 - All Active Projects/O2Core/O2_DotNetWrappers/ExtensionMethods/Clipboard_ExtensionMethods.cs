using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace O2.DotNetWrappers.ExtensionMethods
{
    public static class Clipboard_ExtensionMethods
    {
        public static void toClipboard(this string _string)
        {
            Clipboard.SetText(_string);
        }
    }
}
