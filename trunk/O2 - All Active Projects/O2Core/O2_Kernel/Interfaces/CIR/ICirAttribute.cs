using System.Collections.Generic;

namespace O2.Kernel.Interfaces.CIR
{
    public interface ICirAttribute
    {
        string AttributeClass { get; set; }
        Dictionary<string, string> Parameters { get; set; } 
    }
}