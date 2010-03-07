using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using O2.Interfaces.CIR;
using O2.DotNetWrappers.ExtensionMethods;
using O2.External.O2Mono.MonoCecil;
using O2.Kernel.ExtensionMethods;
using O2.Core.CIR.CirObjects;
using O2.Core.CIR.CirCreator.DotNet;
using O2.Core.CIR.ExtensionMethods;

namespace O2.Core.CIR.ExtensionMethods
{
    public static class Cir_ExtensionMethods
    {
        public static bool isDotNet(this string _string)
        {
            if (_string.fileExists())
                return (CecilUtils.isDotNetAssembly(_string));
            return false;
        }

        public static ICirData toCir(this string _string)
        {
            if (_string.isDotNet())
            {
                var cirData = new CirData();
                new CirFactory().processAssemblyDefinition(cirData, _string);
                cirData.remapXRefs();
                return cirData;
            }
            return null;
        }

        public static ICirClass toCir(this Type type)
        {
            var cirData = new CirData();
            var assemblyLocation = type.assemblyLocation();
            var assembly = CecilUtils.getAssembly(assemblyLocation);
            var cirFactory = new CirFactory();
            cirFactory.loadAndMapSymbols(assembly, assemblyLocation, false, "");
            var typeDefinition = CecilUtils.getType(assembly, type.Name);
            var cirType = cirFactory.processTypeDefinition(cirData, typeDefinition);
            cirData.remapXRefs();
            return cirType;
        }

        public static ICirClass clazz(this ICirData cirData, string className)
        {
            if (cirData.dClasses_bySignature.ContainsKey(className))
                return cirData.dClasses_bySignature[className];
            else
            {
                foreach (ICirClass cirClass in cirData.dClasses_bySignature.Values)
                    if (cirClass.Name == className)
                        return cirClass;
                "in clazz couls not find class: {0}".format(className).debug();
            }
            return null;
        }

        public static List<ICirClass> classes(this ICirData cirData)
        {
            return cirData.dClasses_bySignature.Values.ToList();
        }

        public static List<ICirFunction> functions(this ICirData cirData)
        {
            return cirData.dFunctions_bySignature.Values.ToList();
        }

        public static List<ICirFunction> functions(this ICirClass cirClass)
        {
            return cirClass.dFunctions.Values.ToList();
        }

        public static List<String> functionNames(this ICirClass cirClass)
        {
            return cirClass.dFunctions.Keys.ToList();
        }

        public static string stats(this ICirData cirData)
        {
            return ("".line().line() +
                    "CirData Stats:".line() +
                    "  {0} classes".line() +
                    "  {1} funtions".line())
                    .format(cirData.dClasses_bySignature.Count,
                            cirData.dFunctions_bySignature.Count);
        }
    }
}
