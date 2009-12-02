using System;
using System.Collections.Generic;
using System.Text;

namespace O2.Kernel.Interfaces.XRules
{
    public interface IXRulesDatabase
    {
        void installXRulesDatabase(string pathTo_XRulesDatabase_fromO2, string pathTo_XRulesTemplates);
        bool loadArtifact(string fileToLoad, Dictionary<Type, object> loadedArtifaces, bool loadFileAsObject);
    }
}
