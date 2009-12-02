/* 
	Author 	: Thomas GIL (DotNetGuru.org)
	Date 	: 17 september 2006
	License : Public Domain
*/
using DotNetGuru.AspectDNG.Config;
using DotNetGuru.AspectDNG.Util;
using DotNetGuru.AspectDNG.XPath;

using Mono.Cecil;

using System;
using System.Collections;
using System.Text;
using System.IO;
using System.Reflection;
using System.CodeDom;
using System.CodeDom.Compiler;

using Microsoft.CSharp;

namespace DotNetGuru.AspectDNG.MetaAspects {
    public class Generic : MetaAspect {
        public int Priority { get { return 20; } }
        public string XPathBase { get { return "/Assembly/Type"; } }
        public string XPathConstraint { get { return string.Empty; } }

        // CodeDom utilities
        private static string m_Path = "AspectDngDynamicClassName";
        public const string DynamicClassName = "AspectDngDynamicClassName";
        private static int m_ClassIndex = 0;
        private static CompilerParameters m_CompilerParams;
        private static ICodeCompiler m_Compiler;

        static Generic() {
            CodeDomProvider provider = new CSharpCodeProvider();
            m_Compiler = provider.CreateCompiler();
            m_CompilerParams = new CompilerParameters(new string[] { "System.dll" }, m_Path);
        }

        public void Execute(AdviceSpec spec) {
            foreach (Navigator aspectNav in spec.AspectNavigators) {
                MethodDefinition aspectMetaMethod = Narrow.Method(aspectNav, spec);

                MethodInfo aspectMethod = Cil.Aspects.GetType(aspectMetaMethod.DeclaringType.FullName).
                    GetMethod(aspectMetaMethod.Name);

                bool preconditions = aspectMethod.IsStatic &&
                    aspectMethod.ReturnType.FullName == typeof(string).FullName &&
                    aspectMethod.GetParameters().Length == 1 &&
                    aspectMethod.GetParameters()[0].ParameterType.FullName == typeof(TypeDefinition).FullName;
                if (!preconditions)
                    throw new AdviceException("Aspect method signature must match : \n" +
                        "public static string YourMethod(Mono.Cecil.TypeDefinition td)", spec);

                foreach (Navigator targetNav in spec.TargetNavigators) {
                    TypeDefinition targetType = Narrow.TypeDefinition(targetNav, spec);

                    // Only insert members on classes (not structs because fields MUST be initialized in the ctor, no field initializer)
                    preconditions = !targetType.FullName.StartsWith("<")
                        && !targetType.IsValueType
                        && !targetType.IsInterface
                        && !(targetType.BaseType != null && targetType.BaseType.Name == "Delegate");

                    if (preconditions) {
                        string code = aspectMethod.Invoke(null, new object[] { targetType }) as string;

                        if (code != null) {
                            string className = DynamicClassName + ++m_ClassIndex;
                            code = new StringBuilder("public class ").
                                Append(className).Append(" { \n").
                                Append(code).Append("\n}").ToString();

                            try {
                                // CodeDom
                                CompilerResults results = m_Compiler.CompileAssemblyFromSource(m_CompilerParams, code);
                                TypeDefinition dynamicType = AssemblyFactory.GetAssembly(results.PathToAssembly).
                                    MainModule.Types[className];

                                // Copy every method and field from the dynamic type to the targetTypeDef
                                dynamicType = Cil.TargetMainModule.Inject(dynamicType);

                                foreach (MethodDefinition method in dynamicType.Methods)
                                    targetType.Methods.Add(method.Clone());

                                foreach (FieldDefinition field in dynamicType.Fields)
                                    targetType.Fields.Add(field.Clone());

                                if (File.Exists(m_Path))
                                    File.Delete(m_Path);

                            } catch (Exception e) {
                                Console.WriteLine("error " + e);
                            }
                        }
                    }
                }

                // Remove aspect method from target assembly
                TypeDefinition typeDef = (TypeDefinition)aspectMetaMethod.DeclaringType;
                typeDef.Methods.Remove(aspectMetaMethod);
            }
        }

        public void Cleanup() {

        }
    }
}
