/* 
	Author 	: Thomas GIL (DotNetGuru.org)
	Date 	: 17 september 2006
	License : Public Domain
*/
using System;
using System.Reflection;

namespace DotNetGuru.AspectDNG.Joinpoints {
	public class FieldJoinPoint : JoinPoint{
		public FieldInfo TargetField;

		public FieldJoinPoint(object realTarget, FieldInfo handle) : base(realTarget){
			TargetField = handle;
		}

		protected override object ProceedImpl(){
            return TargetField.GetValue(RealTarget);
		}

        public override string ToString() {
            return TargetField.ToString();
        }
    }
}