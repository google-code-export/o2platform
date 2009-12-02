/* 
	Author 	: Thomas GIL (DotNetGuru.org)
	Date 	: 17 september 2006
	License : Public Domain
*/
using System;
using DotNetGuru.AspectDNG.Config;

namespace DotNetGuru.AspectDNG {
	public class AdviceException : ApplicationException {
		public readonly AdviceSpec Spec;

		public AdviceException(string message, AdviceSpec spec) : base(message) {
			Spec = spec;
		}
		
		public AdviceException(string message, AdviceSpec spec, Exception rootCause) : base(message, rootCause) {
			Spec = spec;
		}

		public override string Message {
			get{ 
				return string.Format("{0}{1}:{2}", 
					(base.InnerException != null) ? base.InnerException.Message : string.Empty,
					base.Message,Spec);
			}
		}
		public override string StackTrace {
			get{ 
				return (base.InnerException != null) ? base.InnerException.StackTrace : string.Empty
					+ base.StackTrace;
			}
		}
	}

    public class ForbiddenByAspect : AdviceException {
		public ForbiddenByAspect(string message, AdviceSpec spec) 
			: base("This usage if forbidden by an aspect:\n\t" + message, spec){}
	}
}