/* 
	Author 	: Thomas GIL (DotNetGuru.org)
	Date 	: 17 september 2006
	License : Public Domain
*/
using System;
using System.Reflection;
using System.Globalization;

namespace DotNetGuru.AspectDNG.Joinpoints {
	public class MethodJoinPoint : OperationJoinPoint{
		private MethodInfo m_TargetMethod;
		public override MethodBase TargetOperation{ get{ return m_TargetMethod; } }

        public MethodJoinPoint(object realTarget, object[] parameters, MethodBase handle)
            : base(realTarget, parameters) {

            m_TargetMethod = (MethodInfo)handle;
		}

		protected override object ProceedImpl(){
            return m_TargetMethod.Invoke(RealTarget, Arguments);
        }

        public override string ToString() {
            return "method " + TargetOperation.DeclaringType.FullName + "::" + TargetOperationName + base.ToString();
        }
	}
}