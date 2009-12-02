// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace O2.Cmd.SpringMvc.Objects
{
    [Serializable]
    public class SpringMvcParameter
    {
        public string name { get; set; }
        public string className { get; set; }
        //public bool autoWiredObject { get; set; }
        public string autoWiredMethodUsed { get; set; }
    }
}
