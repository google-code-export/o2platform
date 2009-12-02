/* 
	Author 	: Thomas GIL (DotNetGuru.org)
	Date 	: 17 september 2006
	License : Public Domain
*/
using System;
using System.Collections;
using System.Reflection;
using DotNetGuru.AspectDNG.Config;
using DotNetGuru.AspectDNG.XPath;
using DotNetGuru.AspectDNG.Util;
using Mono.Cecil;

namespace DotNetGuru.AspectDNG.MetaAspects {
    public class MetaAspectFacade {
        public static readonly MetaAspectFacade Instance = new MetaAspectFacade();

        private Hashtable m_MetaAspects = new Hashtable();

        private MetaAspectFacade() {
            // Discover and instantiate every meta-aspects in the current assembly
            Type MetaAspectType = typeof(MetaAspect);
            foreach (Type t in typeof(MetaAspectFacade).Assembly.GetTypes())
                if (MetaAspectType.IsAssignableFrom(t) && !t.IsAbstract)
                    m_MetaAspects[t.Name] = Activator.CreateInstance(t);

            // Get attribute oriented specs
            foreach (Navigator declarationNav in Cil.AspectsNavigator.SelectList(Cil.AllDeclarations)) {
                foreach (Navigator xpoNav in declarationNav.SelectList("Attribute[contains(., '" + Cil.AdngNS + "')]")) {
                    CustomAttribute att = (CustomAttribute)xpoNav.Current;

                    // Get the attribute's name ("Insert", etc) and make it a config type
                    string typeName = att.Constructor.DeclaringType.FullName;
                    AdviceSpec spec = (AdviceSpec)Activator.CreateInstance(Type.GetType(typeName));

                    // Add definition to spec aspects
                    xpoNav.MoveToParent();
                    spec.AspectNavigators.Add(xpoNav);

                    string attParam = att.ConstructorParameters[0].ToString().Trim();
                    if (attParam.StartsWith("/")) spec.targetXPath = attParam;
                    else spec.targetRegExp = attParam;

                    AspectDngConfig.Instance.AddAdvice(spec, xpoNav.ToString());
                }
            }
        }

        private ArrayList m_NeedCleanup = new ArrayList();

        public void Execute(AdviceSpec spec) {
            // meta-aspect attributes/xml elements have the same name as the meta-aspect type
            MetaAspect meta = m_MetaAspects[spec.GetType().Name] as MetaAspect;
            if (meta == null) throw new AdviceException("This spec isn't supported", spec);
            else {
                Eval(spec, meta.XPathBase, meta.XPathConstraint);
                meta.Execute(spec);
                Log.LogExecutedSpec(spec);
                if (!m_NeedCleanup.Contains(meta)) m_NeedCleanup.Add(meta);
            }
        }

        class MetaAspectComparer : IComparer {
            public int Compare(object o1, object o2) {
                return ((MetaAspect)o2).Priority - ((MetaAspect)o1).Priority;
            }
        }

        public void Cleanup() {
            // Really weave here
            m_NeedCleanup.Sort(new MetaAspectComparer());
            foreach (MetaAspect meta in m_NeedCleanup)
                meta.Cleanup();

            // Remove signature from the weaved assembly
            Cil.TargetAssembly.Name.Flags &= ~AssemblyFlags.PublicKey;
            Cil.TargetAssembly.SecurityDeclarations.Clear();
            Cil.TargetAssembly.Name.PublicKey = null;
            Cil.TargetAssembly.Name.PublicKeyToken = null;
            Cil.TargetAssembly.Name.HashAlgorithm = AssemblyHashAlgorithm.None;
        }

        // MetaAspect Eval
        /// Some definitions may have been added from outside (typically for embedded advice)
        /// Just add the new definitions the expressions point to
        private void Eval(AdviceSpec spec, string XPathBase, string XPathConstraint) {
            // Evaluate the XPath expressions on Cecil object model
            if (spec.targetXPath != null) 
                spec.TargetNavigators.AddRange(Cil.TargetNavigator.SelectList(spec.targetXPath + XPathConstraint));
            else if (spec.targetRegExp != null) 
                foreach (Navigator nav in Cil.TargetNavigator.SelectList(XPathBase + XPathConstraint))
                    if (SimpleRegex.IsPreciseMatch(nav, spec.targetRegExp))
                        spec.TargetNavigators.Add(nav);

            if (spec.aspectXPath != null) 
                spec.AspectNavigators.AddRange(Cil.AspectsNavigator.SelectList(spec.aspectXPath));
            else if (spec.aspectRegExp != null) 
                foreach (Navigator nav in Cil.AspectsNavigator.SelectList(Cil.AllDeclarations)) 
                    if (SimpleRegex.IsPreciseMatch(nav, spec.aspectRegExp)) 
                        spec.AspectNavigators.Add(nav);
        }
    }
}