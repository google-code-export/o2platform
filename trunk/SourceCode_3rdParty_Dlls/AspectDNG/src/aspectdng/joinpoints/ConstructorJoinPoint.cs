/* 
	Author 	: Thomas GIL (DotNetGuru.org)
	Date 	: 17 september 2006
	License : Public Domain
*/
using System;
using System.Reflection;

namespace DotNetGuru.AspectDNG.Joinpoints {
    public class ConstructorJoinPoint : OperationJoinPoint {
        private ConstructorInfo m_TargetConstructor;
        public override MethodBase TargetOperation { get { return m_TargetConstructor; } }

        public ConstructorJoinPoint(object target, object[] parameters, MethodBase handle)
            : base(target, parameters) {
            m_TargetConstructor = (ConstructorInfo) handle;
        }

        protected override object ProceedImpl() {
            return (RealTarget == null) ?
                m_TargetConstructor.Invoke(Arguments) :
                m_TargetConstructor.Invoke(RealTarget, Arguments);
        }

        public override string ToString() {
            return "constructor " + m_TargetConstructor.DeclaringType.Name + base.ToString();
        }
    }
}