/* 
	Author 	: Thomas GIL (DotNetGuru.org)
	Date 	: 17 september 2006
	License : Public Domain
*/
using System;
using System.Reflection;

namespace DotNetGuru.AspectDNG.Joinpoints {
    public class StaticMethodJoinPoint : MethodJoinPoint {
        public StaticMethodJoinPoint(object[] parameters, MethodBase handle) : base(null, parameters, handle) { }

        public override string ToString() {
            return "static " + base.ToString();
        }
    }
}