/* 
	Author 	: Thomas GIL (DotNetGuru.org)
	Date 	: 17 september 2006
	License : Public Domain
*/
using DotNetGuru.AspectDNG;
using DotNetGuru.AspectDNG.Config;
using DotNetGuru.AspectDNG.Util;
using DotNetGuru.AspectDNG.XPath;
using Mono.Cecil;
using Mono.Cecil.Cil;
using System;
using System.Collections;

// Instead of a method call, this meta-aspect calls an interceptor method whose signature must match:
// static object XXX::YYY(JoinPoint)
namespace DotNetGuru.AspectDNG.MetaAspects {
    public class ToBeWeaved {
        public ArrayList Interceptors = new ArrayList();
        public MethodDefinition ContainerMethod;
        public Instruction TargetCall;

        public ToBeWeaved(Instruction call, MethodDefinition container) {
            TargetCall = call;
            ContainerMethod = container;
        }
    }

    public class AroundCall : MetaAspect {
        public int Priority { get { return 10; } }
        public string XPathBase { get { return "/Assembly/Type/*/Call"; } }
        public string XPathConstraint { get { return "[self::Call]"; } }

        private static Hashtable m_InterceptorsToWeave = new Hashtable();
        private static void AddInterceptor(Instruction targetCall, MethodDefinition containerMethod, IMethodReference aspectMethod) {
            ToBeWeaved tbw = m_InterceptorsToWeave[targetCall] as ToBeWeaved;
            if (tbw == null)
                m_InterceptorsToWeave[targetCall] = tbw = new ToBeWeaved(targetCall, containerMethod);
            tbw.Interceptors.Add(aspectMethod);
        }

        public void Execute(AdviceSpec spec) {
            foreach (Navigator aspectNav in spec.AspectNavigators) {
                IMethodDefinition aspectMethod = Narrow.Interceptor(aspectNav, spec);
                foreach (Navigator targetNav in spec.TargetNavigators) {
                    Instruction targetCallInstruction = Narrow.CallInstruction(targetNav, spec);
                    MethodReference targetMethod = (MethodReference)targetCallInstruction.Operand;
                    MethodDefinition containerMethod = Narrow.Method(targetNav.Parent, spec);

                    // Prevent recursive weaving
                    if (targetMethod != aspectMethod &&
                        containerMethod != aspectMethod &&
                        !Cil.BelongsToAspectDng(targetMethod) &&
                        !Cil.BelongsToAspectDng(aspectMethod) &&
                        !Cil.BelongsToAspectDng(containerMethod) &&
                        targetMethod.GenericParameters.Count == 0 &&
                        targetMethod.DeclaringType.GenericParameters.Count == 0) {

                        // Cannot weave on acces to boxed arrays. Methods look like int32[0...,0...]::Get(int32, int32)
                        if (targetMethod.DeclaringType.Name.IndexOf("]") == -1
                            && targetMethod.ToString().IndexOf("&") == -1
                            // To prevent base.SameMethod() calls from child types, event if AroundBody already occurred
                            && Cil.GetOriginalMethodName(targetMethod) != Cil.GetOriginalMethodName(containerMethod)
                            ) {
                            AddInterceptor(targetCallInstruction, containerMethod, aspectMethod);
                        }  
                    }
                }
            }
        }

        public void Cleanup() {
            Hashtable templateMethods = new Hashtable();
            Hashtable templateMethodsInterceptors = new Hashtable();

            foreach (ToBeWeaved tbw in m_InterceptorsToWeave.Values) {
                Instruction call = tbw.TargetCall;
                MethodDefinition containerMethod = tbw.ContainerMethod;
                MethodReference targetMethod = (MethodReference)call.Operand;

                if (containerMethod != null && containerMethod.DeclaringType != null) {
                    MethodDefinition tplMethod = Cil.AspectTemplateCall.Clone();

                    tplMethod.Body.Instructions.Clear(); // remove ret
                    tplMethod.Name = targetMethod.Name + Joinpoints.OperationJoinPoint.WrappedMethodSuffix + "Call_" + Cil.GetNextId();
                    ((TypeDefinition)containerMethod.DeclaringType).Methods.Add(tplMethod);

                    PopulateInterceptor(tbw, call, targetMethod, tplMethod);

                    call.OpCode = OpCodes.Call;
                    call.Operand = tplMethod;
                }
            }
        }

        private static void PopulateInterceptor(ToBeWeaved tbw, Instruction call, MethodReference targetMethod, MethodDefinition tplMethod) {
            EmittingContext ctx = new EmittingContext(tplMethod);
            VariableDefinition tmpObj = null;
            int offset = 0;

            // Interceptor signature must be : 
            if (call.OpCode == OpCodes.Newobj) { 
                // new object -> static ctorDeclaringType interceptor(ctorParametersList);
                tplMethod.ReturnType.ReturnType = targetMethod.DeclaringType;
                ctx.Emit(OpCodes.Ldnull); // create a brand new object, don't initialize an existing one
            } else if (Cil.IsStatic(targetMethod)) {  
                // call static method  -> static methodReturnType interceptor(methodParametersList);
                tplMethod.ReturnType.ReturnType = targetMethod.ReturnType.ReturnType;
            } else { 
                //call instance method  -> static methodReturnType interceptor(methodDeclaringType, methodParametersList);
                tplMethod.ReturnType.ReturnType = targetMethod.ReturnType.ReturnType;
                offset = 1;
                ctx.Emit(OpCodes.Ldarg_0); // load "this", which is the first parameter of the tplMethod
                if (!targetMethod.DeclaringType.IsValueType) {
                    tplMethod.Parameters.Add(new ParameterDefinition(targetMethod.DeclaringType));
                } else {
                    tplMethod.Parameters.Add(new ParameterDefinition(new ReferenceType(targetMethod.DeclaringType)));

                    ctx.Emit(OpCodes.Ldobj, targetMethod.DeclaringType);
                    ctx.Emit(OpCodes.Box, targetMethod.DeclaringType);

                    // Tmp object to store boxed value types when passed along call interceptors and then unwrapped after calls
                    //                    tmpObj = new VariableDefinition(new ReferenceType(targetMethod.DeclaringType));
                    tmpObj = new VariableDefinition(Cil.GetTypeReference(typeof(object)));
                    tplMethod.Body.Variables.Add(tmpObj);
                    ctx.Emit(OpCodes.Stloc, tmpObj);
                    ctx.Emit(OpCodes.Ldloc, tmpObj);

                }
            } 

            foreach (ParameterDefinition p in targetMethod.Parameters)
                tplMethod.Parameters.Add(new ParameterDefinition(p.ParameterType));

            ParameterDefinitionCollection tplParameters = tplMethod.Parameters;
            ParameterDefinitionCollection targetParameters = targetMethod.Parameters;

            int nbParams = tplParameters.Count - offset;
            if (nbParams == 0)
                // Load an empty array
                ctx.Emit(OpCodes.Ldnull);
            else {
                // Instantiate an array of the right size
                VariableDefinition arrayDef = new VariableDefinition(Cil.GetTypeReference(typeof(object[])));
                tplMethod.Body.Variables.Add(arrayDef);

                ctx.Emit(OpCodes.Ldc_I4, nbParams);
                ctx.Emit(OpCodes.Newarr, Cil.GetTypeReference(typeof(object)));
                ctx.Emit(OpCodes.Stloc, arrayDef);

                // Load the array with data coming from the current parameters
                for (int i = tplParameters.Count - 1; i >= offset; i--) {
                    ctx.Emit(OpCodes.Ldloc, arrayDef);
                    ctx.Emit(OpCodes.Ldc_I4, i - offset);
                    ParameterDefinition p = tplParameters[i];
                    ctx.Emit(OpCodes.Ldarg, p);
                    Cil.BoxIfRequired(p.ParameterType, ctx);
                    ctx.Emit(OpCodes.Stelem_Ref);
                }
                ctx.Emit(OpCodes.Ldloc, arrayDef);
            }
            // end of reify parameters

            Cil.InvokeInterceptors(tplMethod, ctx, targetMethod, tbw.Interceptors);

            if (call.OpCode != OpCodes.Newobj)
                Cil.UnboxIfRequired(targetMethod.ReturnType.ReturnType, ctx);

            if (call.OpCode != OpCodes.Newobj && !Cil.IsStatic(targetMethod) && targetMethod.DeclaringType.IsValueType) {
                ctx.Emit(OpCodes.Ldarg_0);
                ctx.Emit(OpCodes.Ldloc, tmpObj);
                ctx.Emit(OpCodes.Unbox, targetMethod.DeclaringType);
                ctx.Emit(OpCodes.Ldobj, targetMethod.DeclaringType);
                ctx.Emit(OpCodes.Stobj, targetMethod.DeclaringType);
            }

            // Cast (to satisfy PEVerify)
            Cil.CastIfRequired(tplMethod.ReturnType.ReturnType, ctx);

            ctx.Emit(OpCodes.Ret);
        }

    }
}
