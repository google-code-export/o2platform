using System;
using System.Collections.Generic;
using O2.Core.XRules;
using O2.DotNetWrappers.DotNet;
using O2.DotNetWrappers.Windows;
using O2.Kernel.Interfaces.XRules;
using System.Reflection;

namespace O2.Core.XRules.XRulesEngine
{
    public class XRules_Execution
    {
        public static List<ILoadedXRule> getLoadedXRules(List<IXRule> xRules)
        {
            return getLoadedXRules(xRules, "");
        }

        public static List<ILoadedXRule> getLoadedXRules(List<IXRule> xRules, string xRuleSource)
        {
            var loadedXRules = new List<ILoadedXRule>();
            foreach(var xRule in xRules)
                loadedXRules.Add(new KLoadedXRule(xRule, xRuleSource));
            return loadedXRules;
        }
    }
}