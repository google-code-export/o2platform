// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using O2.Kernel.Interfaces.O2Core;
using System.IO;

namespace O2.Scanner.DotNet
{
    class DI
    {
        static DI()
        {
            
            VirtualPathToExternalPostSharpAssembly = @"..\..\..\O2_External_PostSharp\bin\Debug\O2_External_PostSharp.dll";
            PathToExternalPostSharpAssembly = Path.Combine(config.CurrentExecutableDirectory, VirtualPathToExternalPostSharpAssembly);
            PathToLocalUnitTestsFiles = Path.Combine(config.hardCodedO2LocalSourceCodeDir, @"Scanners\O2_Scanner_DotNet\_UnitTests");
        }

        public static IO2Log log = O2.Kernel.PublicDI.log;
        public static IO2Config config = O2.Kernel.PublicDI.config;
        
        public static string VirtualPathToExternalPostSharpAssembly { get; set;}
        public static string PathToExternalPostSharpAssembly { get; set; }
        public static string PathToLocalUnitTestsFiles { get; set; }

    }
}
