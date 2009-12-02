/* 
	Author 	: Thomas GIL (DotNetGuru.org)
	Date 	: 17 september 2006
	License : Public Domain
*/
using DotNetGuru.AspectDNG.Config;
using System;

namespace DotNetGuru.AspectDNG.Joinpoints {
	[AttributeUsage(AttributeTargets.Method, AllowMultiple=true)]
	public class AroundBody : AdviceSpec {
		public AroundBody() {} 
		public AroundBody(string expr) {} 
	}

    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class AroundCall : AdviceSpec {
        public AroundCall() { }
        public AroundCall(string expr) { }
    }

    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class AroundFieldRead : AdviceSpec {
        public AroundFieldRead() { }
        public AroundFieldRead(string expr) { }
    }

    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class AroundFieldWrite : AdviceSpec {
        public AroundFieldWrite() { }
        public AroundFieldWrite(string expr) { }
    }

    [AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
    public class Warning : AdviceSpec {
        public Warning() { }
        public Warning(string expr) { }
    }

    [AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
    public class Error : AdviceSpec {
        public Error() { }
        public Error(string expr) { }
    }

    [AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
    public class Insert : AdviceSpec {
        public Insert() { }
        public Insert(string expr) { }
    }

    [AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
    public class Delete : AdviceSpec {
        public Delete() { }
        public Delete(string expr) { }
    }

    [AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
    public class MakeSerializable : AdviceSpec {
        public MakeSerializable() { }
        public MakeSerializable(string expr) { }
    }

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, AllowMultiple = true)]
    public class SetBaseType : AdviceSpec {
        public SetBaseType() { }
        public SetBaseType(string expr) { }
    }

    [AttributeUsage(AttributeTargets.Interface, AllowMultiple = true)]
    public class ImplementInterface : AdviceSpec {
        public ImplementInterface() { }
        public ImplementInterface(string expr) { }
    }

    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class Generic : AdviceSpec {
        public Generic() { }
        public Generic(string expr) { }
    }
}