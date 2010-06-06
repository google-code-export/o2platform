// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using O2.DotNetWrappers.Filters;
using O2.DotNetWrappers.O2Misc;
using O2.DotNetWrappers.Windows;
using O2.DotNetWrappers.Xsd;
using O2.Interfaces.O2Core;
using O2.Kernel;
using O2.Kernel.InterfacesBaseImpl;
using System.IO;

namespace O2.DotNetWrappers
{
    // used for direct DI (Dependency Injection) objects and local cache objects
    internal class DI
    {
        static DI()
        {
            log = PublicDI.log;
            reflection = new O2FormsReflectionASCX();
            config = PublicDI.config;

            dFilteredFuntionSignatures = new Dictionary<string, FilteredSignature>();
            dFilesLines = new Dictionary<string, List<string>>();

            dO2Vars = new Dictionary<string, object>();
            dRegExes = new Dictionary<string, Regex>();

            sourceCodeMappingFileName = "SourceCodeMappingsFile.xml";
            sourceCodeMappings = SourceCodeMappingsUtils.getSourceCodeMappings();
            PathToGac = Path.Combine(Environment.GetEnvironmentVariable("windir") ?? "", "Assembly\\GAC_MSIL");            
        }

        // DI Targets
        public static IReflectionASCX reflection  { get; set; }
        public static IO2Log log  { get; set; }
        public static Form windowsFormMainO2Form { get; set; }
        public static IO2Config config { get; set; }


        // local global variables
        public static Dictionary<String, List<String>> dFilesLines;
        public static Dictionary<String, FilteredSignature> dFilteredFuntionSignatures;            

        public static Dictionary<String, Object> dO2Vars;
        public static Dictionary<String, Regex> dRegExes;
        
        public static string sourceCodeMappingFileName;
        public static SourceCodeMappings sourceCodeMappings;
      
        public static string PathToGac { get; set; }
        
    }
}
