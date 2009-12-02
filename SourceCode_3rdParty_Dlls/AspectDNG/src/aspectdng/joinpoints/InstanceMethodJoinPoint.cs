/* 
	Author 	: Thomas GIL (DotNetGuru.org)
	Date 	: 17 september 2006
	License : Public Domain
*/
using System;
using System.Reflection;

namespace DotNetGuru.AspectDNG.Joinpoints {
	public class InstanceMethodJoinPoint : MethodJoinPoint{
        public InstanceMethodJoinPoint(object target, object[] parameters, MethodBase handle)
			: base(target, parameters, handle){
		}

        public override string ToString() {
            return "instance " + base.ToString();
        }

	}
}