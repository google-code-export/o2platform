using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace O2.Kernel.CodeUtils
{
    public static class String_ExtensionMethods
    {
        public static bool valid(this string _string)
        {
            if (false == string.IsNullOrEmpty(_string))
                if (_string.Trim() != "")
                    return true;
            return false;
        }

    }
}
