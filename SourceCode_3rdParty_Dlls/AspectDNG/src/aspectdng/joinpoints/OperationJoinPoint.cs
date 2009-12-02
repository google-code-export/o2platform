/* 
	Author 	: Thomas GIL (DotNetGuru.org)
	Date 	: 17 september 2006
	License : Public Domain
*/
using System;
using System.Text;
using System.Reflection;

namespace DotNetGuru.AspectDNG.Joinpoints {
	public abstract class OperationJoinPoint : JoinPoint{
        public const string WrappedMethodSuffix = "_AspectDNG_";

        private static readonly object[] EmptyArray = new object[] { };

		public readonly object[] Arguments;

		protected OperationJoinPoint(object realTarget, object[] parameters) : base(realTarget){
            Arguments = parameters == null ? EmptyArray : parameters;
		}

		public int NbParameters{ 
			get{ return Arguments.Length;}
		}

		public object this[int index]{ 
			get{ return Arguments[index];}
			set{ Arguments[index] = value;}
		}

        public abstract MethodBase TargetOperation { get; }
        public string TargetOperationName { 
            get{
                string name = TargetOperation.Name;
                int index = name.LastIndexOf(WrappedMethodSuffix);
                return index > -1 ? name.Substring(0, index) : name;
            } 
        }

		public object this[string name]{ 
			get{
				object result = null;
				int index = -1;
				foreach(ParameterInfo pi in TargetOperation.GetParameters()){
					index++;
					if (pi.Name == name){
						result = Arguments[index];
                        break;
					}
				}
				return result;
			}
			set{ 
				int index = -1;
				foreach(ParameterInfo pi in TargetOperation.GetParameters()){
					index++;
					if (pi.Name == name){
						Arguments[index] = value;
                        break;
                    }
				}
			}
		}

        public override string ToString() {
            StringBuilder sb = new StringBuilder("(");
            for (int i = 0; i < NbParameters; i++) {
                if (i > 0) sb.Append(","); 
                object parameter = this[i];
                if (parameter == null) sb.Append("null");
                else 
                try{
                    sb.Append(parameter);
                } catch{
                    sb.Append("???");
                }
            }
            sb.Append(")");
            return sb.ToString();
        }
	}
}