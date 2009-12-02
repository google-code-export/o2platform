// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System.Collections.Generic;
using System.Reflection;
using System.Windows.Forms;
using NUnit.Framework;
using O2.DotNetWrappers.Windows;
using O2.External.O2Mono.Ascx;

namespace O2.UnitTests.Test_O2CoreLib
{
    [TestFixture]
    public class Test_ascx_O2Reflector
    {
        private readonly ascx_O2Reflector o2Reflector = new ascx_O2Reflector();

        private readonly string testAssembly = Assembly.GetExecutingAssembly().Location; // DI.config.hardCodedPathToO2UnitTestsDll;
        //private readonly string testAssembly =   // Config.getExecutingAssembly();

        public void testEngineNode(TreeNode nodeToTest, string expectedType, string nodeKeyword,
                                   ascx_O2Reflector.AvailableEngines engineToTest, bool testIfNodeHasChildNodes)
        {
            Assert.IsNotNull(nodeToTest,
                             "for " + engineToTest + ", engine , the " + nodeKeyword + " Node was null - ");
            Assert.IsNotNull(nodeToTest.Tag,
                             "for " + engineToTest + ", engine , the " + nodeKeyword + " Node Tag was null - ");
            Assert.IsTrue(nodeToTest.Tag.GetType().Name == expectedType,
                          "for " + engineToTest + ", engine , the root " + nodeKeyword + " Tag should be an " +
                          expectedType +
                          " Type , and it was of Type " + nodeToTest.Tag.GetType().Name + " - ");
            if (testIfNodeHasChildNodes)
                Assert.IsTrue(nodeToTest.Nodes.Count > 0,
                              "for " + engineToTest + ", on the " + nodeKeyword +
                              " node, there are no child nodes in first node - ");
        }

        public void testEnginesResultsAsMappedToTheGuiTreeViewObjects(ascx_O2Reflector.AvailableEngines engineToTest,
                                                                      string typeOfRootNode,
                                                                      List<string> expectedChildType,
                                                                      List<string> expectedChildValue)
        {
            o2Reflector.setCurrentEngine(engineToTest);
            TreeView tvAssemblyBrowser = o2Reflector.getTreeView_AssemblyBrowser();
            TreeView LoadedAssemblies = o2Reflector.getTreeView_LoadedAssemblies();
            Assert.IsTrue(LoadedAssemblies.Nodes.Count > 0,
                          "for " + engineToTest + ", there are no nodes in LoadedAssemblies TreeView - ");

            Assert.IsTrue(o2Reflector.getCurrentEngine() == engineToTest, "getCurrentEngine != " + engineToTest);
            Assert.IsTrue(tvAssemblyBrowser.Nodes.Count > 0,
                          "for " + engineToTest + ", there are no nodes in AssemblyBrowser TreeView - ");

            // testing the root node
            TreeNode currentNode = tvAssemblyBrowser.Nodes[0];
            testEngineNode(currentNode, typeOfRootNode, "root", engineToTest, true);

            // check node types
            for (int i = 0; i < expectedChildType.Count; i++)
            {
                currentNode = currentNode.Nodes[0];
                testEngineNode(currentNode, expectedChildType[i], "child #" + i, engineToTest,
                               (i < expectedChildType.Count - 1));
                o2Reflector.populateNode(currentNode);
            }

            // check node values
            currentNode = tvAssemblyBrowser.Nodes[0];
            for (int i = 0; i < expectedChildValue.Count; i++)
            {               
                currentNode = checkIfNodeHasType(currentNode, expectedChildValue[i], expectedChildType[i], engineToTest);
                o2Reflector.populateNode(currentNode);
            }
        }

        public TreeNode checkIfNodeHasType(TreeNode nodeToSearch, string nodeName, string expectedNodeType,
                                           ascx_O2Reflector.AvailableEngines engineToTest)
        {
            TreeNode node = nodeToSearch.Nodes[nodeName];
            testEngineNode(node, expectedNodeType, nodeName, engineToTest, false);
            return node;
        }

        [Test]
        public void loadGoodAndBadAssembly()
        {
            string goodAssembly = testAssembly;
            string badAssembly = goodAssembly + ".txt";
            Files.WriteFileContent(badAssembly, "bad assembly contents");
            o2Reflector.loadAssembly(goodAssembly);
            Assert.IsTrue(o2Reflector.loadedAssemblies.Count == 1,
                          "after goodAssembly, there should only be 1 assembly loaded - ");
            o2Reflector.loadAssembly(badAssembly);
            Assert.IsTrue(o2Reflector.loadedAssemblies.Count == 1,
                          "after badAssembly, there should only be 1 assembly loaded - ");
        }

        [Test]
        public void settingEnginesToUse()
        {
            /* DI.config.setDI("O2_Views_ASCX.dll", "DI", "assemblyAnalysis", new AssemblyAnalysisImpl());
            DI.config.setDI("O2_Views_Controlers.dll", "DI", "monoCecil", new O2MonoCecil());
            DI.config.setDI("O2_Views_Controlers.dll", "DI", "reflectionASCX", new O2FormsReflectionASCX());*/
            
            
            o2Reflector.removeAllLoadedAssemblies();
            Assert.IsTrue(o2Reflector.loadedAssemblies.Count == 0,
                          "There should no assemblies loaded after removeAllLoadedAssemblies - ");
            o2Reflector.loadAssembly(testAssembly);
            var expectedChildValues = new List<string>(new[]
                                                           {
                                                               "_O2_UnitTests_TestO2CoreLib.dll",                                                               
                                                               "O2.UnitTests.Test_O2CoreLib",
                                                               "Test_ascx_O2Reflector",
                                                               "checkIfNodeHasType(TreeNode, String, String, AvailableEngines) : TreeNode"
                                                           });
            testEnginesResultsAsMappedToTheGuiTreeViewObjects(ascx_O2Reflector.AvailableEngines.Reflection, "Assembly",
                                                              new List<string>(new[]
                                                                                   {
                                                                                       "Module",
                                                                                       "List`1",
                                                                                       "RuntimeType",
                                                                                       "RuntimeMethodInfo"
                                                                                   }), expectedChildValues);

            testEnginesResultsAsMappedToTheGuiTreeViewObjects(ascx_O2Reflector.AvailableEngines.MonoCecil,
                                                              "AssemblyDefinition",
                                                              new List<string>(new[]
                                                                                   {
                                                                                       "ModuleDefinition",
                                                                                       "List`1",
                                                                                       "TypeDefinition",
                                                                                       "MethodDefinition"
                                                                                   }), expectedChildValues);
            // to implement
            //testEnginesResultsAsMappedToTheGuiTreeViewObjects(AssemblyAnalysis.AvailableEngines.Cir, "AssemblyDefinition");                 
        }
    }
}
