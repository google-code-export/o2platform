// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System.Collections.Generic;
using System.Linq;
using System.Text;
using O2.DotNetWrappers.Windows;
using O2.Kernel.Interfaces.XRules;

namespace O2.Core.XRules.XRulesEngine
{
    public class XRules_Config
    {
        
        static XRules_Config()
        {
            PathTo_XRulesDatabase_fromO2 = @"C:\O2\XRulesDatabase\_Rules";
            PathTo_XRulesDatabase_fromLocalDisk = @"C:\O2\_XRules_Local";
            PathTo_XRulesTemplates = @"C:\O2\XRulesDatabase\_Templates";
            PathTo_XRulesCompiledDlls = @"C:\O2\XRulesDatabase\_CompiledDlls";
            Files.checkIfDirectoryExistsAndCreateIfNot(PathTo_XRulesDatabase_fromO2);
            Files.checkIfDirectoryExistsAndCreateIfNot(PathTo_XRulesDatabase_fromLocalDisk);            
            Files.checkIfDirectoryExistsAndCreateIfNot(PathTo_XRulesTemplates);
            Files.checkIfDirectoryExistsAndCreateIfNot(PathTo_XRulesCompiledDlls);

            xRulesDatabase = null;
        }

        public static string PathTo_XRulesDatabase_fromO2 { get; set; }
        public static string PathTo_XRulesDatabase_fromLocalDisk { get; set; }
        public static string PathTo_XRulesTemplates { get; set; }
        public static string PathTo_XRulesCompiledDlls { get; set; }

        // this one will have to be DI
        public static IXRulesDatabase xRulesDatabase { get; set; }
    }
}
