// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
namespace FluentSharp.O2.Interfaces.Rules
{
    public enum O2RuleType
    {
        All,
        Source,
        NotASource,
        LostSource,
        Sink,      
        NotASink,
        LostSink,
        Callback,
        PropageTaint,
        DontPropagateTaint,        
        NotMapped,
        ToBeDeleted
    }
}