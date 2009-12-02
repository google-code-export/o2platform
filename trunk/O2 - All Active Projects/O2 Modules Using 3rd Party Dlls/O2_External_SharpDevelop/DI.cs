using O2.DotNetWrappers.Windows;
using O2.Kernel;
using O2.Kernel.Interfaces.O2Core;
using O2.Kernel.InterfacesBaseImpl;

namespace O2.External.SharpDevelop
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