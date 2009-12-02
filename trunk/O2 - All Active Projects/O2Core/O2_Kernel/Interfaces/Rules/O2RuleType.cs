namespace O2.Kernel.Interfaces.Rules
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