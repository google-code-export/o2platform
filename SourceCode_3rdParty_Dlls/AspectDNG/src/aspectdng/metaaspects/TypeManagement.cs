/* 
	Author 	: Thomas GIL (DotNetGuru.org)
	Date 	: 17 september 2006
	License : Public Domain
*/
using System;
using System.Collections;
using DotNetGuru.AspectDNG.XPath;
using DotNetGuru.AspectDNG.Config;
using DotNetGuru.AspectDNG.Util;
using Mono.Cecil;

namespace DotNetGuru.AspectDNG.MetaAspects {
    public class MakeSerializable : MetaAspect {
        public int Priority { get { return 0; } }
        public string XPathBase { get { return "/Assembly/Type"; } }
        public string XPathConstraint { get { return string.Empty; } }

		public void Execute(AdviceSpec spec){
			foreach (Navigator targetNav in spec.TargetNavigators)
				Narrow.TypeDefinition(targetNav, spec).Attributes |= TypeAttributes.Serializable;
		}
        public void Cleanup() { }
    }

    public class SetBaseType : MetaAspect {
        public int Priority { get { return 0; } }
        public string XPathBase { get { return "/Assembly/Type"; } }
        public string XPathConstraint { get { return string.Empty; } }

		public void Execute(AdviceSpec spec){
            if (spec.AspectNavigators.Count != 1)
                throw new AdviceException("One and only one base type can be set to a target type, but got " + spec.AspectNavigators.Count, spec);

        	TypeDefinition parentTypeDef = Narrow.TypeDefinition((Navigator)spec.AspectNavigators[0], spec);
			
			foreach (Navigator targetNav in spec.TargetNavigators)
				Narrow.TypeDefinition(targetNav, spec).BaseType = Cil.TargetMainModule.Import(parentTypeDef);
		}
        public void Cleanup() { }
    }

    public class ImplementInterface : MetaAspect {
        public int Priority { get { return 0; } }
        public string XPathBase { get { return "/Assembly/Type"; } }
        public string XPathConstraint { get { return string.Empty; } }

		public void Execute(AdviceSpec spec){
			foreach (Navigator targetNav in spec.TargetNavigators){
				TypeDefinition typeDef = Narrow.TypeDefinition(targetNav, spec);
				foreach(Navigator aspectNav in spec.AspectNavigators)
					typeDef.Interfaces.Add(Cil.TargetMainModule.Import(Narrow.InterfaceDefinition(aspectNav, spec)));
			}
		}
        public void Cleanup() { }
    }
}