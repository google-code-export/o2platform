using System.Reflection;

namespace O2.Kernel.Interfaces.Messages
{
    public interface IM_DotNetAssemblyAvailable : IO2Message
    {
        string pathToAssembly { get; set; }
    }
}