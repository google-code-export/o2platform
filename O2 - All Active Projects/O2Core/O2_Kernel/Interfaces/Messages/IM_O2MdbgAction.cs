using System.Reflection;

namespace O2.Kernel.Interfaces.Messages
{
    public enum IM_O2MdbgActions 
    {
        startDebugSession,
        endDebugSession,
        breakEvent,
        debugProcessRequest,
        debugMethodInfoRequest,
        commandExecutionMessage
    }
    public interface IM_O2MdbgAction : IO2Message
    {
        IM_O2MdbgActions o2MdbgAction { get; set; }
        string filename { get; set; }
        MethodInfo method { get; set; }
        int line { get; set; }
        string loadDllsFrom { get; set; }
        string lastCommandExecutionMessage { get; set; }
            
    }
}
