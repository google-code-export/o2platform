using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using O2.External.O2Mono.MonoCecil;
using O2.Kernel.Interfaces.XRules;
using O2.Kernel;
//O2Tag_AddReferenceFile:mono.cecil.dll

namespace O2.Core.XRules.Classes
{
    public class UnitTestSupport
    {
        public static string nUnit_ReferenceDll = "nunit.framework";
        public static string nUnit_ClassAttribute = "TestFixtureAttribute";
        public static string nUnit_MethodAttribute = "TestAttribute";


        public static IEnumerable<String> getAssembliesThatReferenceNUnit(IEnumerable<Assembly> assemblies)
        {
             var assembliesLocation = from Assembly assembly in assemblies select assembly.Location;
             return getAssembliesThatReferenceNUnit(assembliesLocation);
        }

        public static IEnumerable<String> getAssembliesThatReferenceNUnit(IEnumerable<string> filesToSearch)
        {
            var results = new List<string>();
            foreach (var file in filesToSearch)
            {
                if (doesAssemblyReferenceNUnit(file))
                    results.Add(file);                
            }
            return results;
        }

        public static bool doesAssemblyReferenceNUnit(string file)
        {        	        	
            var fileExtension = Path.GetExtension(file);
            if (fileExtension == ".dll" || fileExtension == ".exe")
                if (CecilUtils.isDotNetAssembly(file))
                {
                    //DI.log.info("file: {0}", file);
                    var dependencies = CecilAssemblyDependencies.getDictionaryOfDependenciesForAssembly_WithNoRecursiveSearch(file);
                    foreach (var dependency in dependencies.Values)
                    {
                        //  DI.log.debug("   d: {0}", dependency);
                        if (dependency == nUnit_ReferenceDll)
                            return true;
                    }
                }
            return false;
        }

        public static IEnumerable<ILoadedXRule> getXRulesWithUnitTests_FromAssemblies(IEnumerable<string> assembliesWithUnitTests)
        {
            var xRuleSource = "from Unit Tests";
            var xLoadedXRules = new List<ILoadedXRule>();
            try
            {                
                foreach (var file in assembliesWithUnitTests)
                {
                    var reflectionAssembly = PublicDI.reflection.getAssembly(file);     // we will need the reflection assembly object below
                    if (reflectionAssembly != null)
                    {
                        var nUnit_testFixtures = CecilCodeSearch.getTypesWithAttribute(file, nUnit_ClassAttribute);
                        foreach (var nUnit_testFixture in nUnit_testFixtures)
                        {
                            var reflectionType = PublicDI.reflection.getType(reflectionAssembly, nUnit_testFixture.FullName);
                            if (reflectionType != null)
                            {
                                var nUnit_tests = CecilCodeSearch.getMethodsWithAttribute(nUnit_testFixture,nUnit_MethodAttribute);
                                if (nUnit_tests.Count > 0)
                                {
                                    var xRule = new KXRule {Name = nUnit_testFixture.Name};
                                    var loadedXRule = new KLoadedXRule(xRule,xRuleSource );
                                    foreach (var nUnit_test in nUnit_tests)
                                    {
                                        var methodInfo = CecilConvert.getMethodInfoFromMethodDefinition(reflectionType, nUnit_test);
                                        if (methodInfo != null)
                                        {
                                            var xRuleAttribute = new XRuleAttribute {Name = nUnit_test.Name};
                                            loadedXRule.methods.Add(xRuleAttribute, methodInfo);
                                        }
                                        //    loadedXRule.methods.Add(xRuleAttribute,test.);
                                    }
                                    xLoadedXRules.Add(loadedXRule);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                PublicDI.log.error("In getXRulesWithUnitTests_FromAssemblies: {0}", ex.Message);
            }
            return xLoadedXRules;
        }

        public static List<String> getAssembliesWithUnitTest(IEnumerable<string> filesToSearch)
        {
            var assembliesWithUnitTest = new List<String>();
            var unitTestAttribute = "TestAttribute";
            var assembliesThatReferenceNUnit = getAssembliesThatReferenceNUnit(filesToSearch);
            foreach (var file in assembliesThatReferenceNUnit)
            {
                if (CecilCodeSearch.findInAssembly_CustomAttribute(file, unitTestAttribute))
                    assembliesWithUnitTest.Add(file);
            }
            return assembliesWithUnitTest;
        }

        public static List<MethodInfo> getMethodsToExecuteFromTreeView(TreeView tvXRules)
        {
            var methodsToExecute = new List<MethodInfo>();
            foreach (TreeNode treeNode in tvXRules.Nodes)
                methodsToExecute.AddRange(getMethodsFromObject(treeNode.Tag));
            return methodsToExecute;
        }


        public static List<MethodInfo> getMethodsToExecuteFromSelectedTreeViewNode(TreeView tvXRules)
        {
            var methodsToExecute = new List<MethodInfo>();
            if (tvXRules.SelectedNode != null && tvXRules.SelectedNode.Tag != null)
            {
                methodsToExecute.AddRange(getMethodsFromObject(tvXRules.SelectedNode.Tag));
            }
            return methodsToExecute;
        }

        private static IEnumerable<MethodInfo> getMethodsFromObject(object xRuleObject)
        {
            var methods = new List<MethodInfo>();
            if (xRuleObject != null)
                if (xRuleObject is ILoadedXRule)
                    methods.AddRange(((ILoadedXRule) xRuleObject).methods.Values);
                else if (xRuleObject is MethodInfo)
                    methods.Add((MethodInfo) xRuleObject);
            return methods;
        }
    }
}
