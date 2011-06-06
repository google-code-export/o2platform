// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.Collections.Generic;
using System.Text;

namespace FluentSharp.O2.Interfaces.FrameworkSupport.J2EE
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