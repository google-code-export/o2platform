using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace O2.DotNetWrappers.ExtensionMethods
{
    public static class Crypto_ExtensionMethods
    {
        public static Random randomObject = new Random((int)DateTime.Now.Ticks);
         
        public static int random(this int maxValue)
        {
            return randomObject.Next(maxValue);
        }
    }
}
