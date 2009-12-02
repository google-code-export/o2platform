using System;
using System.Collections.Generic;
using System.Text;

namespace O2.Kernel.Interfaces.FrameworkSupport.J2EE
{
    public interface ITilesDefinitions
    {
        List<ITilesDefinition> definitions { get; set;} 
    }

    public interface  ITilesDefinition
    {
        string name { get; set; }
        string path { get; set; }
        string page { get; set; }
        string extends { get; set; }
        Dictionary<string, string> puts { get; set; }
    }
}