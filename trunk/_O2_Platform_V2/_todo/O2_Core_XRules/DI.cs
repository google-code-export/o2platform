// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System.IO;
using O2.Interfaces.O2Core;
using O2.Kernel;

namespace O2.Core.XRules
{
    class DI
    {
        static DI()
        {
            log = PublicDI.log;// new WinFormsUILog();            
            config = PublicDI.config; //new KO2Config();    
            reflection = PublicDI.reflection;
            PathToLocalUnitTestsFiles = Path.Combine(config.hardCodedO2LocalSourceCodeDir, @"O2Core\O2_Core_XRules\_UnitTests");
            PathToLocalXRulesUnitTestsFiles = Path.Combine(config.hardCodedO2LocalSourceCodeDir, @"_O2_UnitTests\Standalone");
            SvnXRulesDatabaseUrl = "O2%20-%20All%20Active%20Projects/O2_XRules_Database/_Rules/";
        }

        public static IO2Log log { get; set; }
        public static IO2Config config { get; set; }
        public static IReflection reflection { get; set; }
        //public string hardCodedO2LocalTempFolder { get; set; }
        //public string hardCodedO2LocalBuildDir { get; set; }
        public static string PathToLocalUnitTestsFiles { get; set; }
        public static string PathToLocalXRulesUnitTestsFiles { get; set; }
        public static string SvnXRulesDatabaseUrl { get; set; }
    }
}
