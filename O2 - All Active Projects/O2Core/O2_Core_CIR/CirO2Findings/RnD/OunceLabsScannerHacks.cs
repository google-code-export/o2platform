// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Mono.Cecil;

namespace O2.Core.CIR.CirCreator
{
    public class OunceLabsScannerHacks
    {
        // normal methods
        public static String fixDotNetSignature(MethodInfo miMethodInfo)
        {
            var sReturnParameter =
                (String)
                DI.reflection.invokeMethod_InstanceStaticPublicNonPublic(miMethodInfo.ReturnType, "SigToString",
                                                                         new object[] { });
            return fixDotNetSignature(miMethodInfo, sReturnParameter);
        }

        // when we don't provide a MethodBase we will assume it is a constructor and make the return parameter = void.this
        public static String fixDotNetSignature(MethodBase mbMethodBase)
        {
            return fixDotNetSignature(mbMethodBase, "void.this");
        }

        // calculate signature so that is is compatible with CORE
        public static String fixDotNetSignature(MethodBase mbMethodBase, String sReturnParameter)
        {
            try
            {
                // internal dotnet method that already has all paramters calculated
                //                String sMethodSignature = (String)reflection.invokeMethod_InstanceStaticPublicNonPublic(mbMethodBase, "ConstructName", new object[] { mbMethodBase });
                var sMethodSignature =
                    (String)
                    DI.reflection.invokeMethod_Static("System.Reflection.RuntimeMethodInfo", "ConstructName",
                                                      new object[] { mbMethodBase });
                if (sMethodSignature == null)
                {
                    DI.log.error("in fixDotNetSignature could not resolve method signature for :{0}",
                                 mbMethodBase.ToString());
                    sMethodSignature = "(COULD NOT RESOLVE SIGNATURE): " + mbMethodBase;
                }
                sMethodSignature = sMethodSignature.Replace(", ", ";");
                String sClass = mbMethodBase.ReflectedType.FullName;
                String sFullSignature = String.Format("{0}.{1}:{2}", sClass, sMethodSignature, sReturnParameter);

                // specific method fixes:                
                sFullSignature = sFullSignature.Replace("System.String", "string");
                sFullSignature = sFullSignature.Replace("System.Object", "object");
                sFullSignature = sFullSignature.Replace("Int16", "short");
                sFullSignature = sFullSignature.Replace("Int32", "int");
                sFullSignature = sFullSignature.Replace("Int64", "long");
                sFullSignature = sFullSignature.Replace("Boolean", "bool");
                sFullSignature = sFullSignature.Replace("Double", "double");
                sFullSignature = sFullSignature.Replace("Void", "void");
                sFullSignature = sFullSignature.Replace(" ByRef", "");
                // this doesn't seem to be included in the CIR signature                 

                return sFullSignature;
            }
            catch (Exception ex)
            {
                DI.log.error("In fixDotNetSignature:{0}", ex.Message);
                return "";
            }
        }
    }

}
