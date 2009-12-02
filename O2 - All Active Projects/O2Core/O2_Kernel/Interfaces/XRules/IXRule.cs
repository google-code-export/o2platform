using System.Collections.Generic;
using System.Reflection;
using System.Threading;

namespace O2.Kernel.Interfaces.XRules
{
    public interface IXRule
    {
        string Name { get; set; }
        string Description { get; set; }        
    }

    public class KXRule : IXRule
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public KXRule()
        {
            Name = "";
            Description = "O2 XRule";            
        }
                
        public override string ToString()
        {
            return Name;
        }        
    }
}