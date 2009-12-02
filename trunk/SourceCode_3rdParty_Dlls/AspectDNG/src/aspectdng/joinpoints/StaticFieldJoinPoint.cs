/* 
	Author 	: Thomas GIL (DotNetGuru.org)
	Date 	: 17 september 2006
	License : Public Domain
*/
using System;
using System.Reflection;

namespace DotNetGuru.AspectDNG.Joinpoints {
	public class StaticFieldJoinPoint : JoinPoint{
		public FieldInfo TargetField;

		public StaticFieldJoinPoint(FieldInfo handle) : base(null){
			TargetField = handle;
		}

		protected override object ProceedImpl(){
            return TargetField.GetValue(null);
        }
	}
}