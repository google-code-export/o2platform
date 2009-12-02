/* 
	Author 	: Thomas GIL (DotNetGuru.org)
	Date 	: 17 september 2006
	License : Public Domain
*/
using System;
using System.Reflection;
using System.Collections;
using System.Globalization;
using System.IO;

namespace DotNetGuru.AspectDNG.Joinpoints {
    public abstract class JoinPoint {
        // Custom type resolving mechanism. This is required because we introduce assemblies indirections
        // This ensures all assemblies are looked up by any "Type.GetType() like" method
        static JoinPoint() {
            AppDomain.CurrentDomain.TypeResolve += new ResolveEventHandler(CurrentDomain_TypeResolve);
        }
        static Assembly CurrentDomain_TypeResolve(object sender, ResolveEventArgs args) {
            string typeName = args.Name;
            foreach (Assembly a in AppDomain.CurrentDomain.GetAssemblies())
                if (a.GetType(typeName) != null)
                    return a;
            return null;
        }


        public readonly object RealTarget;

        protected ArrayList m_Interceptors = new ArrayList();
        protected int m_InterceptorIndex = -1;

        public JoinPoint(object theRealTarget) {
            RealTarget = theRealTarget;
        }

        public JoinPoint AddInterceptor(MethodBase handle) {
            m_Interceptors.Add(handle);
            return this;
        }

        // <<Template method>>
        public object Proceed() {
            object result = null;
            ++m_InterceptorIndex;
            try {
                if (m_InterceptorIndex < m_Interceptors.Count) {
                    MethodBase interceptor = (MethodBase)m_Interceptors[m_InterceptorIndex];

                    //Console.WriteLine("invoking interceptor from : " + this);
                    //Console.WriteLine("   real target : " + RealTarget);
                    //if (this is OperationJoinPoint) {
                    //    Console.WriteLine("   target method : " + ((OperationJoinPoint)this).TargetOperation);
                    //    Console.WriteLine("   target method declaring type: " + ((OperationJoinPoint)this).TargetOperation.DeclaringType);
                    //    Console.WriteLine("   interceptor: " + interceptor);
                    //}

                    result = interceptor.Invoke(null, new object[] { this });
                } else {
                    //Console.WriteLine("before last call : " + this);
                    result = ProceedImpl();
                }
            } catch (TargetInvocationException e) {
                Exception ex = e;
                while (ex is TargetInvocationException && ex.InnerException != null)
                    ex = ex.InnerException;
                throw ex;
            } 
            --m_InterceptorIndex;
            return result;
        }

        protected abstract object ProceedImpl();
    }
}