using System;
using System.Collections.Generic;
using System.Text;
using Jint.Native;
using System.Reflection;
using System.Reflection.Emit;

namespace Jint
{
    public class DelegateWrapper
    {
        public ExecutionVisitor Visitor { get; set; }
        public JsFunction Function { get; set; }
        public JsDictionaryObject That { get; set; }

        public object Invoke(object[] parameters)
        {
            JsInstance[] arguments = new JsInstance[parameters.Length];
            for (int i = 0; i < parameters.Length; i++)
            {
                arguments[i] = Visitor.Global.WrapClr(parameters[i]);
            }

            Visitor.ExecuteFunction(Function, That, arguments);

            return Visitor.Returned == null ? null : Visitor.Returned.Value;
        }

        static Dictionary<Type, DynamicMethod> delegateCache = new Dictionary<Type, DynamicMethod>();
        static MethodInfo changeTypeMethodInfo = typeof(Convert).GetMethod("ChangeType", new Type[] { typeof(object), typeof(Type) });
        static MethodInfo invokeWrapper = typeof(DelegateWrapper).GetMethod("Invoke", new Type[] { typeof(object[]) });

        public static DynamicMethod GenerateDynamicMethod(Type delegateType)
        {
            lock (delegateCache)
            {
                if (delegateCache.ContainsKey(delegateType))
                {
                    return delegateCache[delegateType];
                }
            }

            MethodInfo mi = delegateType.GetMethod("Invoke");

            ParameterInfo[] parameters = mi.GetParameters();
            Type[] parametersType = new Type[parameters.Length + 1];
            parametersType[0] = typeof(DelegateWrapper);
            for (int i = 0; i < parameters.Length; i++)
            {
                parametersType[i + 1] = parameters[i].ParameterType;
            }

            DynamicMethod dm = new DynamicMethod("DynamicMethod", mi.ReturnType, parametersType, typeof(DelegateWrapper));

            ILGenerator il = dm.GetILGenerator();

            il.DeclareLocal(typeof(object[]));

            // args = new object[parameters.Length];
            il.Emit(OpCodes.Ldc_I4, parameters.Length);
            il.Emit(OpCodes.Newarr, typeof(object));
            il.Emit(OpCodes.Stloc_0);

            // args[i] = parameters[i];
            for (int i = 0; i < parameters.Length; i++)
            {
                il.Emit(OpCodes.Ldloc_0);
                il.Emit(OpCodes.Ldc_I4, i);
                il.Emit(OpCodes.Ldarg, i + 1);
                if (!parametersType[i].IsByRef)
                {
                    il.Emit(OpCodes.Box, parametersType[i + 1]);
                }
                il.Emit(OpCodes.Stelem_Ref);
            }

            il.Emit(OpCodes.Ldarg_0); // this
            il.Emit(OpCodes.Ldloc_0); // args
            il.EmitCall(OpCodes.Call, invokeWrapper, null); // this.Invoke(args);

            if (mi.ReturnType == typeof(void))
            {
                il.Emit(OpCodes.Pop);
            }
            else
            {
                //il.DeclareLocal(typeof(Type));
                //il.Emit(OpCodes.Ldtoken, mi.ReturnType);
                //il.Emit(OpCodes.Ldloc_1);
                //il.EmitCall(OpCodes.Call, changeTypeMethodInfo, null); // System.Convert.ChangeType( this.Invoke(args), mi.ReturnType) )

                if (!mi.ReturnType.IsByRef)
                {
                    il.Emit(OpCodes.Unbox_Any, mi.ReturnType);
                }
            }

            il.Emit(OpCodes.Ret);

            lock (delegateType)
            {
                return delegateCache[delegateType] = dm;
            }
        }
    }
}