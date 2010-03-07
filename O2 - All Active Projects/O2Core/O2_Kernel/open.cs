using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using O2.Kernel.ExtensionMethods;

namespace O2.Kernel
{
    public class open
    {
        public static Control directory()
        {
            var directory = viewAscx("ascx_Directory");
            directory.invoke("simpleMode_withAddressBar");
            return directory;
        }

        public static Control viewAscx(string controlName)
        {
            return controlName.viewsAscxControl();
        }

        public static Control viewAscx(string controlName, string title, int width, int height)
        {
            return controlName.viewsAscxControl(title, width, height);
        }
    }
}
