// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mono.Cecil;
using O2.Kernel.ExtensionMethods;
using O2.Interfaces.CIR;
using O2.Core.CIR.CirObjects;
using O2.DotNetWrappers.ExtensionMethods;
using O2.External.O2Mono.MonoCecil;

namespace O2.Core.CIR.CirCreator.DotNet
{
    public class CirFactoryUtils
    {

        public static string getFunctionUniqueSignatureFromMethodReference(IMemberReference methodReference)
        {
            try
            {
                if (methodReference.DeclaringType.Module != null )
                {                    
                    if (methodReference.DeclaringType.Scope.Name == methodReference.DeclaringType.Module.Name)                    
                        return String.Format("{0}!{1}", methodReference.DeclaringType.Module.Assembly.Name, methodReference);        // use the module name if the scope matches                                    
                    return String.Format("{0}!{1}", methodReference.DeclaringType.Scope, methodReference);         // use the scope name for external methods (like mscorlib.dll)                                                        
                }

                return String.Format("{0}!{1}", "[NullModule]", methodReference);   // when there is no module info
                //  methodReference.DeclaringType.Scope.MetadataToken.
                /*return String.Format("{0}!{1}",
                    ((methodReference.DeclaringType.Module != null) ? methodReference.DeclaringType.Module.Name : "[NullModule]")
                    , methodReference);                */
            }
            catch (Exception ex)
            {
                DI.log.ex(ex, "in CirFactoryUtils.getFunctionUniqueSignatureFromMethodReference",true);                
            }
            return null;
        }

        public static string getTypeUniqueSignatureFromTypeReference(TypeReference typeDefinition)
        {
            try
            {
                if ((typeDefinition.Module != null))
                {
                    // using the assembly full name as a reference
                    if ((typeDefinition.Scope.Name == typeDefinition.Module.Name))
                        return String.Format("{0}!{1}", typeDefinition.Module.Assembly, typeDefinition);
                    return String.Format("{0}!{1}", typeDefinition.Scope, typeDefinition);
                    /*if ((typeDefinition.Scope.Name == typeDefinition.Module.Name))
                        return String.Format("{0}!{1}", typeDefinition.Module.Name, typeDefinition);
                    return String.Format("{0}!{1}", typeDefinition.Scope.Name, typeDefinition);*/
                }
                return String.Format("{0}",  "[NullModule]",typeDefinition);
            }
            catch (Exception ex)
            {
                DI.log.ex(ex, "in CirFactoryUtils.getTypeUniqueSignatureFromTypeReference", true);                
            }
            return null;
        }


        public static void showCirDataStats(ICirData cirData)
        {
            DI.log.debug("*** CirData stats ***");
            DI.log.debug("   {0} Classes", cirData.dClasses_bySignature.Count);
            DI.log.debug("   {0} Functions", cirData.dFunctions_bySignature.Count);
        }

        // use for mapping a single class
        public static ICirClass processType(Type type)
        {
            var cirData = new CirData();
            var assemblyLocation = type.assemblyLocation();
            var assembly = CecilUtils.getAssembly(assemblyLocation);
            var cirFactory = new CirFactory();
            cirFactory.loadAndMapSymbols(assembly, assemblyLocation, false, "");
            var typeDefinition = CecilUtils.getType(assembly, type.Name);
            var cirClass = cirFactory.processTypeDefinition(cirData, typeDefinition);
            cirFactory.mapTypeInterfaces(cirData, typeDefinition);
            cirData.remapXRefs();
            return cirClass;
        }        
    }
}
    
