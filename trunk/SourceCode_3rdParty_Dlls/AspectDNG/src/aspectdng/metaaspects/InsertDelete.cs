/* 
	Author 	: Thomas GIL (DotNetGuru.org)
	Date 	: 17 september 2006
	License : Public Domain
*/
using System;
using System.Reflection;
using DotNetGuru.AspectDNG;
using DotNetGuru.AspectDNG.Config;
using DotNetGuru.AspectDNG.XPath;
using DotNetGuru.AspectDNG.Util;
using Mono.Cecil;
using Mono.Cecil.Cil;
using MethodBody = Mono.Cecil.Cil.MethodBody;

namespace DotNetGuru.AspectDNG.MetaAspects {
    public class Insert : MetaAspect {
        public int Priority { get { return 15; } }
        public string XPathBase { get { return Cil.AllDeclarations; } }
        public string XPathConstraint { get { return string.Empty; } }

        public void Execute(AdviceSpec spec) {
            foreach (Navigator targetNav in spec.TargetNavigators) 
                foreach (Navigator aspectNav in spec.AspectNavigators)
                    Cil.CopyTo(targetNav.Current, (ICloneable)aspectNav.Current);
        }

        public void Cleanup() { }
    }

    // Delete every thing from its container 
    public class Delete : MetaAspect {
        public int Priority { get { return 15; } }
        public string XPathBase { get { return Cil.AllDeclarations; } }
        public string XPathConstraint { get { return string.Empty; } }

        public void Execute(AdviceSpec spec) {
            foreach (Navigator targetNav in spec.TargetNavigators)
                targetNav.Remove();
        }

        public void Cleanup() { }
    }
}