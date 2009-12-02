/* 
	Author 	: Thomas GIL (DotNetGuru.org)
	Date 	: 17 september 2006
	License : Public Domain
*/
using System;
using System.Reflection;

namespace DotNetGuru.AspectDNG.Joinpoints {
	public class FieldSetterJoinPoint : FieldJoinPoint{
		public object Value;

		public FieldSetterJoinPoint(object realTarget, object parameter, FieldInfo handle): base(realTarget, handle){
			Value = parameter;
		}

		protected override object ProceedImpl(){
            TargetField.SetValue(RealTarget, Value);
			return null;
		}
	}
}