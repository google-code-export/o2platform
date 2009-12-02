/* 
	Author 	: Thomas GIL (DotNetGuru.org)
	Date 	: 17 september 2006
	License : Public Domain
*/
using DotNetGuru.AspectDNG;
using DotNetGuru.AspectDNG.Util;
using DotNetGuru.AspectDNG.Config;
using DotNetGuru.AspectDNG.XPath;
using Mono.Cecil;
using Mono.Cecil.Cil;
using System;
using System.Collections;
using System.Collections.Specialized;

namespace DotNetGuru.AspectDNG.MetaAspects {
    public class AroundBody : MetaAspect {
        public int Priority { get { return 5; } }

        // Avoid to weave on a weaved (by around body) delegation operation
        public string XPathBase { get { return "/Assembly/Type/*"; } }
        public string XPathConstraint {
            // Only target operations that really have a body (*)
            get { return string.Format("[self::Method or self::Constructor][*]"); }
        }

        private static ListDictionary m_InterceptorsToWeave = new ListDictionary();
        private static void AddInterceptor(MethodDefinition targetMethod, IMethodReference aspectMethod) {
            ArrayList interceptors = m_InterceptorsToWeave[targetMethod] as ArrayList;
            if (interceptors == null) m_InterceptorsToWeave[targetMethod] = interceptors = new ArrayList();
            interceptors.Add(aspectMethod);
        }

        public void Execute(AdviceSpec spec) {
            foreach (Navigator aspectNav in spec.AspectNavigators) {
                IMethodDefinition aspectMethod = Narrow.Interceptor(aspectNav, spec);
                foreach (Navigator targetNav in spec.TargetNavigators) {
                    MethodDefinition targetMethod = Narrow.ConcreteMethod(targetNav, spec);
                    
                    // Prevent recursive weaving
                    if (targetMethod != aspectMethod && 
                        ! Cil.BelongsToAspectDng(targetMethod) &&
                        ! Cil.BelongsToAspectDng(aspectMethod) && 
                        targetMethod.GenericParameters.Count == 0 &&
                        targetMethod.DeclaringType.GenericParameters.Count == 0)

                        // Cannot keep semantics of "ref" parameters
                        if (targetMethod.ToString().IndexOf("&") == -1)
                            AddInterceptor(targetMethod, aspectMethod);
                }
            }
        }


        public void Cleanup() {
            foreach (DictionaryEntry entry in m_InterceptorsToWeave) {
                MethodDefinition containerMethod = entry.Key as MethodDefinition;
                TypeDefinition containerType = (TypeDefinition)containerMethod.DeclaringType;

                // Methods can be removed. In that case, they don't have a declaring type any more
                if (containerType != null) {
                    // Clone and rename the target method
                    MethodDefinition targetMethod = containerMethod.Clone();
                    targetMethod.Name += Joinpoints.OperationJoinPoint.WrappedMethodSuffix + "Body_" + Cil.GetNextId();
                    targetMethod.IsNewSlot = false;
                    targetMethod.IsFinal = false;
                    targetMethod.IsRuntimeSpecialName = targetMethod.IsSpecialName = false;
                    containerType.Methods.Add(targetMethod);

                    // Clear containerMethod
                    containerMethod.Body.InitLocals = true;
                    containerMethod.Body.ExceptionHandlers.Clear();
                    containerMethod.Body.Variables.Clear();
                    containerMethod.Body.Instructions.Clear();

                    // Reify arguments and call interceptors in the original (now target) method
                    EmittingContext ctx = new EmittingContext(containerMethod);
                    VariableDefinition tmpObj = null;

                    if (! Cil.IsStatic(targetMethod)) {
                        ctx.Emit(OpCodes.Ldarg_0);

                        if (targetMethod.DeclaringType.IsValueType) {
                            ctx.Emit(OpCodes.Ldobj, targetMethod.DeclaringType);
                            ctx.Emit(OpCodes.Box, targetMethod.DeclaringType);

                            // Tmp object to store boxed value types when passed along call interceptors and then unwrapped after calls
                            //tmpObj = new VariableDefinition(new ReferenceType(targetMethod.DeclaringType));
                            tmpObj = new VariableDefinition(Cil.GetTypeReference(typeof(object)));
                            containerMethod.Body.Variables.Add(tmpObj);
                            ctx.Emit(OpCodes.Stloc, tmpObj);
                            ctx.Emit(OpCodes.Ldloc, tmpObj);
                        }
                    }

                    VariableDefinition arrayDef = new VariableDefinition(Cil.GetTypeReference(typeof(object[])));
                    containerMethod.Body.Variables.Add(arrayDef);
                    ParameterDefinitionCollection containerParameters = containerMethod.Parameters;
                    ParameterDefinitionCollection targetParameters = targetMethod.Parameters;

                    // Instantiate an array of the right size
                    ctx.Emit(OpCodes.Ldc_I4, containerParameters.Count);
                    ctx.Emit(OpCodes.Newarr, Cil.GetTypeReference(typeof(object)));
                    ctx.Emit(OpCodes.Stloc, arrayDef);

                    // Load the array with data coming from the current parameters
                    for (int i = containerParameters.Count-1; i >= 0; i--) {
                        ParameterDefinition p = containerParameters[i];
                        ctx.Emit(OpCodes.Ldloc, arrayDef);
                        ctx.Emit(OpCodes.Ldc_I4, i);
                        ctx.Emit(OpCodes.Ldarg, p);
                        Cil.BoxIfRequired(p.ParameterType, ctx);
                        ctx.Emit(OpCodes.Stelem_Ref);
                    }

                    // Pass real parameter values (taken from the stack) as the next parameter
                    ctx.Emit(OpCodes.Ldloc, arrayDef);
                    // end of Reify parameters

                    ArrayList interceptors = entry.Value as ArrayList;
                    Cil.InvokeInterceptors(containerMethod, ctx, targetMethod, interceptors);
                    Cil.UnboxIfRequired(targetMethod.ReturnType.ReturnType, ctx);

                    if (!Cil.IsStatic(targetMethod) && targetMethod.DeclaringType.IsValueType) {
                        ctx.Emit(OpCodes.Ldarg_0);
                        ctx.Emit(OpCodes.Ldloc, tmpObj);
                        Cil.UnboxIfRequired(targetMethod.DeclaringType, ctx);
                        ctx.Emit(OpCodes.Stobj, targetMethod.DeclaringType);
                    }

                    // Cast (to satisfy PEVerify)
                    Cil.CastIfRequired(containerMethod.ReturnType.ReturnType, ctx);

                    ctx.Emit(OpCodes.Ret);
                }
            }
        }
    }
}