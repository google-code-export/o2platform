using System;
using System.Collections.Generic;
using System.Reflection;

namespace O2.DotNetWrappers.O2CmdShell
{
    public class O2CmdApi
    {
        public static List<Type> typesWithCommands = new List<Type>();

        public static MethodInfo getMethod(string methodName, string[] methodParameter)
        {
            foreach (Type type in typesWithCommands)
            {
                MethodInfo methodToexecute = DI.reflection.getMethod(type, methodName, methodParameter);
                if (methodToexecute != null)
                    return methodToexecute;
            }
            return null;
        }
    }
}