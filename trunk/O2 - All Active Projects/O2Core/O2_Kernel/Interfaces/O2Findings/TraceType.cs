using System;

namespace O2.Kernel.Interfaces.O2Findings
{
    [Serializable]
    public enum TraceType
    {
        Type_0 = 0,
        Root_Call = 1,
        Source = 2,
        Known_Sink = 3,
        Type_4 = 4,
        Lost_Sink = 5,
        Type_6 = 6,
        O2JoinSink = 30,
        O2JoinSource = 31, 
        O2JoinLocation = 32,
        O2Info= 40
    }
}