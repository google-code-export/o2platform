using System;

namespace O2.Kernel.Interfaces.Rules
{
    public interface IO2Rule
    {
        O2RuleType RuleType { get; set; }
        string DbId { get; set; }
        string Severity { get; set; }
        string Signature { get; set; }
        string VulnType { get; set; }
        string Param { get; set; }
        string Return { get; set; }
        string FromArgs { get; set; }
        string ToArgs { get; set; }
        string Comments { get; set; }
        bool FromDb { get; set; }
        bool Tagged { get; set; }       // to be used on drag and drop of functions
    }
}