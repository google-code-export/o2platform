using System;

namespace O2.Kernel.Interfaces.XRules
{
    public class XRuleAttribute : Attribute
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public override string ToString()
        {
            return Name ?? "(XRuleAttribute)";
        }
    }
}