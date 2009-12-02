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
