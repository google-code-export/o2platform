/* 
	Author 	: Thomas GIL (DotNetGuru.org)
	Date 	: 17 september 2006
	License : Public Domain
*/
using System;
using Mono.Cecil;
using Mono.Cecil.Cil;
using Mono.Cecil.Binary;
using DotNetGuru.AspectDNG.Config;
using DotNetGuru.AspectDNG.Util;
using DotNetGuru.AspectDNG.XPath;

namespace DotNetGuru.AspectDNG.MetaAspects {
    public interface MetaAspect {
        int Priority { get; }
        string XPathBase { get; }
        string XPathConstraint { get; }

		void Execute(AdviceSpec spec); // returns true if it need cleanup
        void Cleanup();
	}
}
