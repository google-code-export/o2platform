// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using O2.Core.CIR.CirObjects;

namespace O2.Cmd.SpringMvc.Objects
{
    [Serializable]
    public class SpringMvcMappings
    {
        public string id { get; set; }
        public List<SpringMvcController> controllers { get; set; }
        public string cirDataFile { get; set; }
        //public Dictionary<SpringMvcParameter, SpringMvcController> parameters { get; set; }
    }
}
