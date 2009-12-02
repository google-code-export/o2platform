using O2.Kernel.Interfaces.CIR;

namespace O2.Kernel.Interfaces.DotNet
{
    public interface IGacDll
    {
        string name {get;set;}
        string version { get; set; }
        string fullPath { get; set; }
        ICirData cirData { get; set; }
        PostSharpHookStatus PostSharpHooks { get; set; }
    }
}