using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace O2.External.O2Mono.MonoCecil
{
    public class CecilConvert
    {
        public static MethodInfo getMethodInfoFromMethodDefinition(Type reflectionType, Mono.Cecil.MethodDefinition nUnit_test)
        {
            return DI.reflection.getMethod(reflectionType, nUnit_test.Name);    
        }
    }
}
