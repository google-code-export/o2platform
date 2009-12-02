using O2.Kernel.Interfaces.CIR;
using O2.Kernel.Interfaces.DotNet;

namespace O2.Kernel.InterfacesBaseImpl
{
    public class KGacDll : IGacDll
    {
        public string name { get; set; }
        public string version { get; set; }
        public string fullPath { get; set; }
        public ICirData cirData { get; set; }
        public PostSharpHookStatus PostSharpHooks { get; set; }

        public KGacDll(string _name, string _version, string _fullPath)
        {
            name = _name;
            version = _version;
            fullPath = _fullPath;
            cirData = null;
            PostSharpHooks = PostSharpHookStatus.NotCalculated;
        }

        public override string ToString()
        {
            return name;
        }

    }
}