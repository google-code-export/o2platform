/* 
	Author 	: Thomas GIL (DotNetGuru.org)
	Date 	: 17 september 2006
	License : Public Domain
*/
using System;
using System.Reflection;
using System.IO;
using System.Collections;
using DotNetGuru.AspectDNG.XPath;
using DotNetGuru.AspectDNG.Config;
using DotNetGuru.AspectDNG.Joinpoints;
using Mono.Cecil;
using Mono.Cecil.Cil;
using Mono.Cecil.Binary;
using Mono.Cecil.Signatures;
using DotNetGuru.AspectDNG.MetaAspects;

namespace DotNetGuru.AspectDNG.Util {
    public abstract class Cil {
        public const string AllDeclarations = "/Assembly//*[self::Type or parent::Type]";
        public static readonly string AdngNS = typeof(JoinPoint).Namespace;

        public static string GetOriginalMethodName(MethodReference method) {
            int index = method.Name.IndexOf(Joinpoints.OperationJoinPoint.WrappedMethodSuffix);
            return index == -1 ? method.Name : method.Name.Substring(0, index);
        }

        private static long m_Id;
        public static long GetNextId() {
            return ++m_Id;
        }

        public static MethodReference
            InstanceMethodJoinPointFactory, StaticMethodJoinPointFactory, ConstructorJoinPointFactory,
            FieldSetterJoinPointFactory, FieldGetterJoinPointFactory,
            StaticFieldSetterJoinPointFactory, StaticFieldGetterJoinPointFactory,
            AddInterceptorMethod, ProceedMethod,
            GetMethodFromHandle, GetFieldFromHandle;

#if DOTNETTWO
        public static MethodReference
            GetGenericMethodFromHandle, GetGenericFieldFromHandle;
#endif

        private static string m_TargetPath;

        public static MethodDefinition AspectTemplateCall;
        private static string m_AspectsPath;
        private static Assembly m_Aspects;
        public static Assembly Aspects {
            get { 
                if (m_Aspects == null)
                    m_Aspects = Assembly.LoadFile(m_AspectsPath);
                return m_Aspects;
            }
        }
        public static AssemblyDefinition TargetAssembly, AspectsAssembly;
        public static ModuleDefinition TargetMainModule, AspectsMainModule;
        public static Navigator TargetNavigator, AspectsNavigator;

        static IList m_Assemblies = new ArrayList();
        static bool m_AssembliesLoaded = false;
        static Assembly CustomTypeResolver(object sender, ResolveEventArgs args) {
            if (!m_AssembliesLoaded) {
                m_Assemblies.Add(Aspects);
                m_Assemblies.Add(Assembly.LoadFile(m_TargetPath));
                foreach (AssemblyNameReference ar in TargetAssembly.MainModule.AssemblyReferences)
                    m_Assemblies.Add(Assembly.Load(ar.FullName));
            }
            string typeName = args.Name;
            Assembly result = null;
            foreach (Assembly a in m_Assemblies)
                if (a.GetType(typeName, false) != null) {
                    result = a;
                    break;
                }
            return result;
        }

        public static string GetFullPath(string path) {
            if (path != null) path = Path.GetFullPath(path);
            return path;
        }

        public static void Init(string targetPath, string aspectsPath) {
            AppDomain.CurrentDomain.TypeResolve += new ResolveEventHandler(CustomTypeResolver);
            m_TargetPath = targetPath = GetFullPath(targetPath);
            m_AspectsPath = aspectsPath = GetFullPath(aspectsPath);

            // Add some locations to the lookup locations
            AppDomain.CurrentDomain.AppendPrivatePath(targetPath);
            AppDomain.CurrentDomain.AppendPrivatePath(aspectsPath);
            if (AspectDngConfig.Instance.PrivateLocations != null) 
                foreach (string path in AspectDngConfig.Instance.PrivateLocations) 
                    AppDomain.CurrentDomain.AppendPrivatePath(path);

            // Load target assembly
            TargetAssembly = AssemblyFactory.GetAssembly(targetPath);
            TargetMainModule = TargetAssembly.MainModule;
            TargetNavigator = new Navigator(TargetAssembly);

            // Load aspects assembly 
            if (targetPath == aspectsPath || aspectsPath == null) { // Single assembly
                AspectsAssembly = TargetAssembly;
                AspectsMainModule = TargetMainModule;
                AspectsNavigator = TargetNavigator;
            } else { // Multiple assemblies
                AspectsAssembly = AssemblyFactory.GetAssembly(aspectsPath);
                AspectsMainModule = AspectsAssembly.MainModule;
                AspectsNavigator = new Navigator(AspectsAssembly);
            }

            AspectTemplateCall = new MethodDefinition("CallTemplate", Mono.Cecil.MethodAttributes.Static| Mono.Cecil.MethodAttributes.Public, TargetMainModule.Import(typeof(void)));
            AspectTemplateCall.Body.CilWorker.Emit(OpCodes.Ret);
            AspectTemplateCall.Body.InitLocals = true;

            FieldSetterJoinPointFactory = TargetMainModule.Import(typeof(FieldSetterJoinPoint).GetConstructors()[0]);
            FieldGetterJoinPointFactory = TargetMainModule.Import(typeof(FieldJoinPoint).GetConstructors()[0]);
            StaticFieldGetterJoinPointFactory = TargetMainModule.Import(typeof(StaticFieldJoinPoint).GetConstructors()[0]);
            FieldSetterJoinPointFactory = TargetMainModule.Import(typeof(FieldSetterJoinPoint).GetConstructors()[0]);
            StaticFieldSetterJoinPointFactory = TargetMainModule.Import(typeof(StaticFieldSetterJoinPoint).GetConstructors()[0]);

            InstanceMethodJoinPointFactory = TargetMainModule.Import(typeof(InstanceMethodJoinPoint).GetConstructors()[0]);
            StaticMethodJoinPointFactory = TargetMainModule.Import(typeof(StaticMethodJoinPoint).GetConstructors()[0]);
            ConstructorJoinPointFactory = TargetMainModule.Import(typeof(ConstructorJoinPoint).GetConstructors()[0]);

            AddInterceptorMethod = TargetMainModule.Import(typeof(JoinPoint).GetMethod("AddInterceptor"));
            ProceedMethod = TargetMainModule.Import(typeof(JoinPoint).GetMethod("Proceed"));

            GetMethodFromHandle = TargetMainModule.Import(typeof(MethodBase).GetMethod("GetMethodFromHandle", new Type[] { typeof(RuntimeMethodHandle)}));
            GetFieldFromHandle = TargetMainModule.Import(typeof(FieldInfo).GetMethod("GetFieldFromHandle", new Type[] { typeof(RuntimeFieldHandle)}));

#if DOTNETTWO
            GetGenericMethodFromHandle = TargetMainModule.Import(typeof(MethodBase).GetMethod("GetMethodFromHandle", new Type[] { typeof(RuntimeMethodHandle), typeof(RuntimeTypeHandle) }));
            GetGenericFieldFromHandle = TargetMainModule.Import(typeof(FieldInfo).GetMethod("GetFieldFromHandle", new Type[] { typeof(RuntimeFieldHandle), typeof(RuntimeTypeHandle) }));
#endif
        }


        private static TypeReference GetTargetTypeIfExists(TypeReference reference) {
            TypeReference result = reference;
            TypeReference targetTypeRef = TargetMainModule.Types[reference.FullName];
            if (targetTypeRef != null)
                result = targetTypeRef;
            return result;
        }

        public static void CopyTo(object target, ICloneable aspect) {
            bool preconditions = target != null && aspect != null;

            if (preconditions && target == Cil.TargetMainModule && aspect is TypeDefinition) {
                TypeDefinition aspectTypeDef = (TypeDefinition)aspect;
                if (Cil.TargetMainModule.Types[aspectTypeDef.FullName] == null)
                    Cil.TargetMainModule.Types.Add(TargetMainModule.Inject(aspectTypeDef));
            } else if (target is TypeDefinition) {
                TypeDefinition targetType = (TypeDefinition)target;

                // Only insert members on classes or structs
                preconditions = !targetType.FullName.StartsWith("<")
                    && !targetType.IsEnum
                    && !targetType.IsInterface
                    && !(targetType.BaseType != null && targetType.BaseType.Name == "Delegate");

                if (preconditions) {
                    if (aspect is FieldDefinition) {
                        FieldDefinition aspectFieldDef = (FieldDefinition)aspect;
                        bool contains = false;
                        foreach(FieldDefinition fieldDef in targetType.Fields)
                            if (fieldDef.Name == aspectFieldDef.Name){
                                contains = true;
                                break;
                            }
                        if (! contains) 
                            targetType.Fields.Add(TargetMainModule.Inject(aspectFieldDef));
                    } else if (aspect is MethodDefinition) {
                        MethodDefinition aspectMethodDef = (MethodDefinition)aspect;
                        bool contains = false;
                        foreach (MethodDefinition methodDef in targetType.Methods)
                            if (methodDef.Name == aspectMethodDef.Name) {
                                contains = true;
                                break;
                            }
                        if (!contains) {
                            MethodDefinition clone = TargetMainModule.Inject(aspectMethodDef);
                            targetType.Methods.Add(clone);

                            // Update field, method or type references in the instructions of the copied method
                            if (clone.Body != null) {
                                clone.Body.InitLocals = true;

                                MethodReference aspectMethod = (MethodReference)aspect;

                                foreach (Instruction instr in clone.Body.Instructions)
                                    if (instr.Operand is FieldReference) {
                                        FieldReference fieldRef = (FieldReference)instr.Operand;
                                        if (fieldRef.DeclaringType.FullName == aspectMethod.DeclaringType.FullName)
                                            instr.Operand = targetType.Fields.GetField(fieldRef.Name);
                                    } else if (instr.Operand is MethodReference) {
                                        MethodReference methodRef = (MethodReference)instr.Operand;
                                        if (methodRef.DeclaringType.FullName == aspectMethod.DeclaringType.FullName)
                                            instr.Operand = targetType.Methods.GetMethod(methodRef.Name, methodRef.Parameters);
                                    } else if (instr.Operand is TypeReference) {
                                        TypeReference typeRef = (TypeReference)instr.Operand;
                                        if (typeRef.FullName == aspectMethod.DeclaringType.FullName)
                                            instr.Operand = targetType;
                                    }
                            }
                        }
                    } else if (aspect is TypeDefinition) {
                        TypeDefinition clone = Cil.TargetMainModule.Inject((TypeDefinition)aspect);
                        targetType.NestedTypes.Add(clone);
                    }
                }
            }
        }

        public static bool BelongsToAspectDng(IMethodReference method) {
            return method.DeclaringType.Namespace.StartsWith("DotNetGuru.AspectDNG");
        }

        public static TypeReference GetTypeReference(Type type) {
            return TargetMainModule.Import(type);
        }

        public static void ReifyParameters(MethodDefinition containerMethod, EmittingContext ctx, IMethodReference targetMethod) {
            int nbTargetParams = targetMethod.Parameters.Count;

            // Interceptor called from a modified constructor (around body) : the first argument is "this", not a param
            if (nbTargetParams == 1 && containerMethod.IsConstructor) 
                ctx.Emit(OpCodes.Ldnull);
            else if (nbTargetParams == 0){
                VariableDefinition arrayDef = new VariableDefinition(GetTypeReference(typeof(object[])));
                containerMethod.Body.Variables.Add(arrayDef);

                ParameterDefinitionCollection currentParameters = containerMethod.Parameters;

                // Instantiate an array of the right size
                ctx.Emit(OpCodes.Ldc_I4, currentParameters.Count);
                ctx.Emit(OpCodes.Newarr, GetTypeReference(typeof(object)));
                ctx.Emit(OpCodes.Stloc, arrayDef);

                // Load the array with data coming from the current parameters
                for (int i = currentParameters.Count - 1; i >= 0; i--) {
                    ctx.Emit(OpCodes.Ldloc, arrayDef);
                    ctx.Emit(OpCodes.Ldc_I4, i);
                    ParameterDefinition p = currentParameters[i];
                    ctx.Emit(OpCodes.Ldarg, p);
                    BoxIfRequired(p.ParameterType, ctx);
                    ctx.Emit(OpCodes.Stelem_Ref);
                }

                // Pass real parameter values (taken from the stack) as the next parameter
                ctx.Emit(OpCodes.Ldloc, arrayDef);
            }
        }

        public static bool IsConstructor(IMethodReference reference) {
            return reference.Name == ".ctor";
        }

        public static bool IsStatic(IMethodReference reference) {
            return ! reference.HasThis;
        }

        public static void UnboxIfRequired(TypeReference typeRef, EmittingContext ctx) {
            if (typeRef.FullName == "System.Void") ctx.Emit(OpCodes.Pop);
            else if (typeRef.IsValueType && !typeRef.Name.EndsWith("]")) {
                ctx.Emit(OpCodes.Unbox, typeRef);

                OpCode op = OpCodes.Nop;
                switch (typeRef.FullName) {
                    // Unsigned integers
                    case "System.Boolean":
                    case "System.Byte": op = OpCodes.Ldind_U1; break;
                    case "System.UInt16": op = OpCodes.Ldind_U2; break;
                    case "System.UInt32": op = OpCodes.Ldind_U4; break;
                    case "System.UInt64": op = OpCodes.Ldind_R8; break; // ind U8 is an alias to ind R8. Same opcode

                    // Signed integers
                    case "System.SByte": op = OpCodes.Ldind_I1; break;
                    case "System.Int16": op = OpCodes.Ldind_I2; break;
                    case "System.Int32": op = OpCodes.Ldind_I4; break;
                    case "System.Int64": op = OpCodes.Ldind_I8; break;

                    // Unsigned floating point
                    case "System.Single": op = OpCodes.Ldind_R4; break;
                    case "System.Double": op = OpCodes.Ldind_R8; break;

                    // Unsigned floating point
                    case "System.IntPtr": op = OpCodes.Ldind_I; break;
                }

                if (op != OpCodes.Nop) ctx.Emit(op);
                else { // handle struct unbox
                    ctx.Emit(OpCodes.Ldobj, typeRef);
                    VariableDefinition tmpVar = new VariableDefinition(typeRef);
                    ctx.Method.Body.Variables.Add(tmpVar);
                    ctx.Emit(OpCodes.Stloc, tmpVar);
                    ctx.Emit(OpCodes.Ldloc, tmpVar);
                }
            }
        }

        public static void BoxIfRequired(TypeReference typeRef, EmittingContext ctx) {
            if ((typeRef.IsValueType && !typeRef.Name.EndsWith("]")) || typeRef.Name.EndsWith("&")) 
                ctx.Emit(OpCodes.Box, typeRef);
        }

        public static void CastIfRequired(TypeReference someType, EmittingContext ctx) {
            if ((someType.FullName != "System.Void" && !someType.IsValueType) || someType.Name.EndsWith("]"))
                ctx.Emit(OpCodes.Castclass, someType);
        }

        public static void InvokeInterceptors(MethodDefinition containerMethod, EmittingContext ctx, MethodReference targetMethod, IList interceptors) {
            // Pass the target method meta-data as the next parameter
            ctx.Emit(OpCodes.Ldtoken, targetMethod);
#if DOTNETTWO
            ctx.Emit(OpCodes.Ldtoken, targetMethod.DeclaringType);
            ctx.Emit(OpCodes.Call, GetGenericMethodFromHandle);
#else
            ctx.Emit(OpCodes.Call, GetMethodFromHandle);
#endif

            // Create the joinpoint
            MethodReference joinPointFactory = 
                IsConstructor(targetMethod) ? ConstructorJoinPointFactory:
                IsStatic(targetMethod) ? StaticMethodJoinPointFactory:
                InstanceMethodJoinPointFactory;
            ctx.Emit(OpCodes.Newobj, joinPointFactory);

            // Duplicate the joinpoint and add each interceptor
            foreach (MethodReference aspectMethod in interceptors) {
                ctx.Emit(OpCodes.Ldtoken, TargetMainModule.Import(aspectMethod));
                ctx.Emit(OpCodes.Call, GetMethodFromHandle);
                ctx.Emit(OpCodes.Call, Cil.AddInterceptorMethod);
            }

            // Invoke "Proceed" on the joinpoint
            ctx.Emit(OpCodes.Call, Cil.ProceedMethod);
        }

        public static void SaveTo(string assemblyPath) {
            AssemblyFactory.SaveAssembly(TargetAssembly, assemblyPath);
        }
    }

    /**************************************/
    // Instructions emitting context
    public class EmittingContext {
        private MethodDefinition m_Method;
        public MethodDefinition Method { get { return m_Method; } }

        private CilWorker m_CilWorker;
        private Instruction m_CurrentInstruction;
        public EmittingContext(MethodDefinition method) {
            m_Method = method;
            m_CilWorker = method.Body.CilWorker;
        }
        public EmittingContext(MethodDefinition method, Instruction ctx) {
            m_Method = method;
            m_CilWorker = method.Body.CilWorker;
            m_CurrentInstruction = ctx;
        }

        public void Emit(OpCode op) { Emit(m_CilWorker.Create(op)); }
        public void Emit(OpCode op, byte operand) { Emit(m_CilWorker.Create(op, operand)); }
        public void Emit(OpCode op, double operand) { Emit(m_CilWorker.Create(op, operand)); }
        public void Emit(OpCode op, FieldReference operand) { Emit(m_CilWorker.Create(op, operand)); }
        public void Emit(OpCode op, float operand) { Emit(m_CilWorker.Create(op, operand)); }
        public void Emit(OpCode op, Instruction[] operand) { Emit(m_CilWorker.Create(op, operand)); }
        public void Emit(OpCode op, int operand) { Emit(m_CilWorker.Create(op, operand)); }
        public void Emit(OpCode op, long operand) { Emit(m_CilWorker.Create(op, operand)); }
        public void Emit(OpCode op, MethodReference operand) { Emit(m_CilWorker.Create(op, operand)); }
        public void Emit(OpCode op, ParameterDefinition operand) { Emit(m_CilWorker.Create(op, operand)); }
        public void Emit(OpCode op, sbyte operand) { Emit(m_CilWorker.Create(op, operand)); }
        public void Emit(OpCode op, string operand) { Emit(m_CilWorker.Create(op, operand)); }
        public void Emit(OpCode op, TypeReference operand) { Emit(m_CilWorker.Create(op, operand)); }
        public void Emit(OpCode op, VariableDefinition operand) { Emit(m_CilWorker.Create(op, operand)); }

        public void Emit(Instruction instr) {
            if (m_CurrentInstruction != null) m_CilWorker.InsertAfter(m_CurrentInstruction, instr);
            else m_CilWorker.Append(instr);
            m_CurrentInstruction = instr;
        }
    }
}