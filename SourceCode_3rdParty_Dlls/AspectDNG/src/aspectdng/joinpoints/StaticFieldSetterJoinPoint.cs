/* 
	Author 	: Thomas GIL (DotNetGuru.org)
	Date 	: 17 september 2006
	License : Public Domain
*/
using System;
using System.Reflection;

namespace DotNetGuru.AspectDNG.Joinpoints {
	public class StaticFieldSetterJoinPoint : FieldJoinPoint{
		public object Value;

        public StaticFieldSetterJoinPoint(object parameter, FieldInfo handle) : base(null, handle) {
			Value = parameter;
		}

		protected override object ProceedImpl(){
            TargetField.SetValue(null, Value);
			return null;
		}
	}
}