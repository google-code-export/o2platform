using System;
using System.Collections.Generic;
using O2.Kernel.Interfaces.FrameworkSupport.J2EE;

namespace O2.Core.FileViewers.J2EE
{
    [Serializable]
    public class KTilesDefinitions : ITilesDefinitions
    {
        public List<ITilesDefinition> definitions { get; set;} 

        public KTilesDefinitions()
        {
            definitions = new List<ITilesDefinition>();
        }
    }

    [Serializable]
    public class KTilesDefinition : ITilesDefinition
    {
        public string name { get; set; }
        public string path { get; set; }
        public string page { get; set; }
        public string extends { get; set; }
        public Dictionary<string, string> puts { get; set; }

        public KTilesDefinition()
        {
            puts = new Dictionary<string, string>();
        }
        
    }
}