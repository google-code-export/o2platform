using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentSharp.O2.Kernel;
using FluentSharp.O2.Kernel.ExtensionMethods;
using FluentSharp.O2.DotNetWrappers.ExtensionMethods;
using FluentSharp.O2.DotNetWrappers.DotNet;

namespace FluentSharp.O2.DotNetWrappers.ExtensionMethods
{
    public static class CompileEngine_ExtensionMethods
    {
        public static string local(this string fileName)
        {
            return fileName.localScript();
        }

        public static string localScript(this string fileName)
        {
            return CompileEngine.findFileOnLocalScriptFolder(fileName);
        }
    }
}
