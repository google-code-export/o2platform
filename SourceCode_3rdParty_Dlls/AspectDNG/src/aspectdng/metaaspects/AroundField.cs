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
using System.Reflection;
using System.Collections;
using System.Collections.Specialized;

// Instead of reading a field, this meta-aspect calls an interceptor method whose signature must match:
// - static object XXX::YYY(object targetObject, System.Reflection.FieldInfo targetField)
namespace DotNetGuru.AspectDNG.MetaAspects {
    public class AroundFieldRead : MetaAspect {
        public int Priority { get { return 10; } }
        string MetaAspect.XPathBase { get { return "/Assembly/Type/*/LdFld"; } }
        string MetaAspect.XPathConstraint { get { return "[self::LdFld]"; } }
		
		protected virtual bool IsGetter { get{ return true; } }

		private static ListDictionary m_GetterInterceptorsToWeave = new ListDictionary();
		protected virtual ListDictionary Map { get{ return m_GetterInterceptorsToWeave; } }

		private void AddInterceptor(Instruction targetCall, MethodDefinition targetMethod, IMethodReference aspectMethod){
			ToBeWeaved tbw = Map[targetCall] as ToBeWeaved;
			if (tbw == null){
				Map[targetCall] = tbw = new ToBeWeaved(targetCall, targetMethod);
			}
			tbw.Interceptors.Add(aspectMethod);
		}

		public void Execute(AdviceSpec spec){
			foreach(Navigator aspectNav in spec.AspectNavigators){
				MethodDefinition aspectMethod = Narrow.Interceptor(aspectNav, spec);
				foreach(Navigator targetNav in spec.TargetNavigators){
                    Instruction targetCallInstruction = Narrow.Instruction(targetNav, spec);
                    MethodDefinition targetMethod = Narrow.Method(targetNav.Parent, spec);

                    // We don't want to intercept volatile field accessors (marshalling problem)
                    string fieldTypeName = ((FieldReference)targetCallInstruction.Operand).FieldType.Name;
                    if (fieldTypeName.IndexOf(typeof(System.Runtime.CompilerServices.IsVolatile).Name) == -1)
    					AddInterceptor(targetCallInstruction, targetMethod, aspectMethod);
				}
			}
		}

		public void Cleanup(){
			foreach(ToBeWeaved tbw in Map.Values){
				Instruction fieldAccessInstruction = tbw.TargetCall;
				MethodDefinition containerMethod = tbw.ContainerMethod;
				FieldReference field = (FieldReference) fieldAccessInstruction.Operand;

                MethodDefinition tplMethod = Cil.AspectTemplateCall.Clone();
                tplMethod.Name = field.Name + "_" + (IsGetter ? "Getter" : "Setter") + Cil.GetNextId();
                ((TypeDefinition)containerMethod.DeclaringType).Methods.Add(tplMethod);
                tplMethod.Body.InitLocals = true;
                tplMethod.Body.Instructions.Clear(); // remove ret

                // *** Inside interceptor ***
                PopulateInterceptor(tbw, fieldAccessInstruction, field, tplMethod);
            
                // Do nothing instead of the the previous behavior
                fieldAccessInstruction.OpCode = OpCodes.Call;
                fieldAccessInstruction.Operand = tplMethod;

            }
		}

        private void PopulateInterceptor(ToBeWeaved tbw, Instruction fieldAccessInstruction, FieldReference field, MethodDefinition tplMethod) {
            tplMethod.ReturnType.ReturnType = IsGetter ? field.FieldType : Cil.GetTypeReference(typeof(void));

            bool isStaticField = fieldAccessInstruction.OpCode == OpCodes.Stsfld || fieldAccessInstruction.OpCode == OpCodes.Ldsfld;

            // The target object is already on the stack if we access an instance field
            if (!isStaticField)
                if (field.DeclaringType.IsValueType)
                    tplMethod.Parameters.Add(new ParameterDefinition(new ReferenceType(field.DeclaringType)));
                else
                    tplMethod.Parameters.Add(new ParameterDefinition(field.DeclaringType));

            // The value to assign is already on the stack as well
            if (!IsGetter)
                tplMethod.Parameters.Add(new ParameterDefinition(field.FieldType));

            EmittingContext ctx = new EmittingContext(tplMethod);

            VariableDefinition tmpObj = null;
            if (!isStaticField) {
                ctx.Emit(OpCodes.Ldarg_0); // load "this", which is the first parameter of the tplMethod

                if (field.DeclaringType.IsValueType) {
                    ctx.Emit(OpCodes.Ldobj, field.DeclaringType);
                    ctx.Emit(OpCodes.Box, field.DeclaringType);

                    // Tmp object to store boxed value types when passed along call interceptors and then unwrapped after calls
//                    tmpObj = new VariableDefinition(new ReferenceType(field.DeclaringType));
                    tmpObj = new VariableDefinition(Cil.GetTypeReference(typeof(object)));
                    tplMethod.Body.Variables.Add(tmpObj);
                    ctx.Emit(OpCodes.Stloc, tmpObj);
                    ctx.Emit(OpCodes.Ldloc, tmpObj);
                }

                if (!IsGetter) {
                    ctx.Emit(OpCodes.Ldarg, tplMethod.Parameters[1]);
                    Cil.BoxIfRequired(field.FieldType, ctx);
                }
            } else { // Static field
                if (!IsGetter) {
                    ctx.Emit(OpCodes.Ldarg, tplMethod.Parameters[0]);
                    Cil.BoxIfRequired(field.FieldType, ctx);
                }
            }

            // Pass the field representation as the last parameter
            ctx.Emit(OpCodes.Ldtoken, field);
#if DOTNETTWO
            ctx.Emit(OpCodes.Ldtoken, field.DeclaringType);
            ctx.Emit(OpCodes.Call, Cil.GetGenericFieldFromHandle);
#else
            ctx.Emit(OpCodes.Call, Cil.GetFieldFromHandle);
#endif

            // Create the JoinPoint
            MethodReference joinPointFactory;
            OpCode opCode = fieldAccessInstruction.OpCode;
            if (opCode == OpCodes.Stsfld) joinPointFactory = Cil.StaticFieldSetterJoinPointFactory;
            else if (opCode == OpCodes.Ldsfld) joinPointFactory = Cil.StaticFieldGetterJoinPointFactory;
            else if (opCode == OpCodes.Stfld) joinPointFactory = Cil.FieldSetterJoinPointFactory;
            else if (opCode == OpCodes.Ldfld) joinPointFactory = Cil.FieldGetterJoinPointFactory;
            else throw new NotSupportedException("This kind of field accessor is not supported : " + opCode);

            ctx.Emit(OpCodes.Newobj, joinPointFactory);

            // Add each interceptor
            foreach (MethodReference aspectMethod in tbw.Interceptors) {
                ctx.Emit(OpCodes.Ldtoken, Cil.TargetMainModule.Import(aspectMethod));
                ctx.Emit(OpCodes.Call, Cil.GetMethodFromHandle);
                ctx.Emit(OpCodes.Call, Cil.AddInterceptorMethod);
            }

            // Invoke "proceed" on the joinpoint
            ctx.Emit(OpCodes.Call, Cil.ProceedMethod);
            Cil.UnboxIfRequired(tplMethod.ReturnType.ReturnType, ctx);

            if (! isStaticField && field.DeclaringType.IsValueType) { // Unbox and update "this"
                ctx.Emit(OpCodes.Ldarg_0);
                ctx.Emit(OpCodes.Ldloc, tmpObj);
                ctx.Emit(OpCodes.Unbox, field.DeclaringType);
                ctx.Emit(OpCodes.Ldobj, field.DeclaringType);
                ctx.Emit(OpCodes.Stobj, field.DeclaringType);
            }

            // Cast (to satisfy PEVerify)
            Cil.CastIfRequired(tplMethod.ReturnType.ReturnType, ctx);

            ctx.Emit(OpCodes.Ret);
        }
	}

    public class AroundFieldWrite : AroundFieldRead, MetaAspect {
        string MetaAspect.XPathBase { get { return "/Assembly/Type/*/StFld"; } }
        string MetaAspect.XPathConstraint { get { return "[self::StFld]"; } }
        protected override bool IsGetter { get { return false; } }

		private static ListDictionary m_SetterInterceptorsToWeave = new ListDictionary();
		protected override ListDictionary Map { get{ return m_SetterInterceptorsToWeave; } }
	}
}