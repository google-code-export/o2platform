using System.Reflection;

namespace O2.Kernel.Interfaces.Messages
{
    public interface IM_SelectedTypeOrMethod : IO2Message
    {
        string assemblyName { get; set; }
        string typeName { get; set; }
        string methodName { get; set; }
        object[] methodParameters { get; set; }
        MethodInfo methodInfo { get; set; }
    }
}