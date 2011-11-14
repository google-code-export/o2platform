// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using FluentSharp.O2.DotNetWrappers.Windows;
using FluentSharp.O2.Interfaces.O2Core;
using FluentSharp.O2.Kernel;
using FluentSharp.O2.Kernel.InterfacesBaseImpl;

namespace V2.O2.External.SharpDevelop
{
    internal class DI
    {        

        static DI()
        {
            config = PublicDI.config;
            log = PublicDI.log;
            //reflection = PublicDI.reflection;       
            reflection = new O2FormsReflectionASCX();
                       
        }

        // DI which will need to be injected 

        public static IO2Config config { get; set; } 
        public static IO2Log log { get; set; }

        public static O2FormsReflectionASCX reflection;                
        
        // public local global vars
        public static string sDefaultO2Scripts = @"_o2_Scripts\";
    }
}
