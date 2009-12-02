using System;
using System.Reflection;
using O2.Kernel.Interfaces.Messages;

namespace O2.Kernel.InterfacesBaseImpl
{
    public class KM_DotNetAssemblyAvailable : KO2Message, IM_DotNetAssemblyAvailable 
    {        
        public string pathToAssembly { get; set; }

        public KM_DotNetAssemblyAvailable(string _pathToAssembly)
        {            
            messageText = "KM_DotNetAssemblyAvailable";
            pathToAssembly = _pathToAssembly;
        }
    }
}