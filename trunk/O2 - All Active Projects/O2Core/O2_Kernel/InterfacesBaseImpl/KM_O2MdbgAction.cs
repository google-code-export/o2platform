using System.Reflection;
using O2.Kernel.Interfaces.Messages;
using O2.Kernel.InterfacesBaseImpl;

namespace O2.Kernel.InterfacesBaseImpl
{
    class KM_O2MdbgAction : KO2Message, IM_O2MdbgAction
    {
        public IM_O2MdbgActions o2MdbgAction { get; set; }
        public string filename { get; set; }
        public MethodInfo method { get; set; }
        public int line { get; set; }
        public string loadDllsFrom { get; set; }
        public string lastCommandExecutionMessage { get; set; }                
    }
}