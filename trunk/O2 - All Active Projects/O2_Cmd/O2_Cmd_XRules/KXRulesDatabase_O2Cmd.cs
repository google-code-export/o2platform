// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using O2.Kernel.Interfaces.XRules;


namespace O2.Cmd.XRules
{
    public class KXRulesDatabase_O2Cmd: IXRulesDatabase
    {         	
        public void installXRulesDatabase(string pathTo_XRulesDatabase_fromO2, string pathTo_XRulesTemplates)
        {
            //InstallXRules.installXRulesDatabase(pathTo_XRulesDatabase_fromO2, pathTo_XRulesTemplates);
            //throw new System.NotImplementedException();
        }

        public bool loadArtifact(string fileOrFolder, Dictionary<Type, object> currentArtifacts, bool loadFileAsObject)
        {
            return false; 
        }
    }
}
