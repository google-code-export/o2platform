/* 
	Author 	: Thomas GIL (DotNetGuru.org)
	Date 	: 17 september 2006
	License : Public Domain
*/
using System;
using DotNetGuru.AspectDNG.Config;
using DotNetGuru.AspectDNG.XPath;
using DotNetGuru.AspectDNG.Util;

namespace DotNetGuru.AspectDNG.MetaAspects {
    public class Warning : MetaAspect {
        public int Priority { get { return 0; } }
        public string XPathBase { get { return Cil.AllDeclarations; } }
        public  string XPathConstraint { get { return string.Empty; } }

 		public virtual void Execute(AdviceSpec spec){
            foreach (Navigator targetNav in spec.TargetNavigators) 
                Log.LogWarning(targetNav.Current);
		}

        public void Cleanup() {}
	}

    public class Error : Warning {
		public override void Execute(AdviceSpec spec){
            Console.WriteLine("ok:" + Cil.AspectsNavigator.Select("//*[match('* *::ShouldBeAvoided(*)')]").Count);
            Console.WriteLine("ok:" + spec.TargetNavigators.Count);
            foreach (Navigator targetNav in spec.TargetNavigators)
				throw new ForbiddenByAspect(targetNav.ToString(), spec);
		}
	}	
}