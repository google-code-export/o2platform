using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace O2.Kernel.CodeUtils
{
    public static class Logging_ExtensionMethods
    {
        public static void info(this object _object, string infoMessage)
        {
            PublicDI.log.info("[{0}] {1}", _object.type().Name, infoMessage);
        }
    }
}
