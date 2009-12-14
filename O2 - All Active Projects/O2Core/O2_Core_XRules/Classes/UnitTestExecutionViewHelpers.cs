// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using O2.DotNetWrappers.DotNet;
using O2.DotNetWrappers.Windows;
using O2.Kernel.Interfaces.XRules;

namespace O2.Core.XRules.Classes
{
    public class UnitTestExecutionViewHelpers
    {
        public static void setPanelWithTestResultBackColor(Panel panel, Color color)
        {   
            panel.invokeOnThread(() => panel.BackColor = color);
        }

        public static void addResultToFlowLayoutPanel(FlowLayoutPanel flowLayoutPanel, object returnData, Color color)
        {

            /*var result = new Panel
                             {
                                 BackColor = color,
                                 Height = 10,
                                 Text = ((returnData != null) ? returnData.ToString() : "")
                             };*/
            //flowLayoutPanel.ts_AddControl(result);

            //if (returnData != null)
            //{
            var textBox = new TextBox
                              {
                                  Width =
                                      (flowLayoutPanel.Width > 20) ? flowLayoutPanel.Width - 22 : flowLayoutPanel.Width,
                                  // 6 works except when there are scrool bars,
                                  WordWrap = false,
                                  Multiline = true,
                                  BackColor = color,
                                  ScrollBars = ScrollBars.None
                              };

            var heigthItem = 14;
            var heigthPad = 4;
            if (returnData == null)
            {
                textBox.Text = ""; //"[null]";
            }
            else if (returnData is List<string>)
            {
                var stringList = (List<string>) returnData;
                textBox.Lines = stringList.ToArray();
                textBox.Height = heigthItem*stringList.Count + heigthPad;
            }
            else
            {
                var rawText = returnData.ToString();
                var splittedText = rawText.Split(new[] {Environment.NewLine},
                                                 StringSplitOptions.RemoveEmptyEntries);
                if (splittedText.Length == 0)
                    textBox.Text = "";
                else
                {
                    textBox.Lines = splittedText;
                    textBox.Height = heigthItem*splittedText.Length + heigthPad;
                }
            }
            //Text = 
            //  textBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;

            
            flowLayoutPanel.ts_AddControl(textBox);
            // }

            //   flowLayoutPanel.Controls.Add(okPanel);

        }

        /*public static void clearFlowLayoutPanel(FlowLayoutPanel flowLayoutPanel)
        {
            flowLayoutPanel.ts_Clear();
            //flowLayoutPanel.invokeOnThread(() => flowLayoutPanel.Controls.Clear());
        }*/
        


        public static IEnumerable<ILoadedXRule> mapUnitTestToXRules(IEnumerable<string> assembliesWithUnitTests)
        {
            return UnitTestSupport.getXRulesWithUnitTests_FromAssemblies(assembliesWithUnitTests);
        }

        public static void addXLoadedRulesToTreeView(IEnumerable<ILoadedXRule> xLoadedRules, TreeView treeView, bool clearLoadedList)
        {
            treeView.invokeOnThread(
                () =>
                {
                    if (clearLoadedList)
                        treeView.Nodes.Clear();
                    treeView.Tag = xLoadedRules;
                    if (xLoadedRules != null)
                        foreach (var xLoadedRule in xLoadedRules)
                        {
                            var newTreeNode = O2Forms.newTreeNode(treeView.Nodes, xLoadedRule.ToString(), 0,
                                                                  xLoadedRule);
                            foreach (var method in xLoadedRule.methods)
                                // only add methods that have no parameters
                                if (DI.reflection.getParametersType(method.Value).Count == 0)
                                    O2Forms.newTreeNode(newTreeNode.Nodes, method.Key.Name, 0, method.Value);
                        }
                });
        }

        public static void addAssembliesWithUnitTestsToTreeView(List<string> assembliesWithUnitTests, TreeView treeView, bool clearLoadedList)
        {
            treeView.invokeOnThread(
                () =>
                {
                    if (clearLoadedList)
                        treeView.Nodes.Clear();
                    treeView.Tag = assembliesWithUnitTests;
                    foreach (var assembly in assembliesWithUnitTests)
                        treeView.Nodes.Add(assembly);
                });
        }

        public static void mapUnitTestToXRules(string assemblyWithUnitTest, TreeView tvTarget_XLoadedRules)
        {
            if (Files.IsExtension(assemblyWithUnitTest,".cs"))
            {
                var compiledAssembly = new CompileEngine().compileSourceFile(assemblyWithUnitTest);
                if (compiledAssembly == null)
                    DI.log.error("aborting mapUnitTestToXRules since could not compile CSharp file: {0}",
                                 assemblyWithUnitTest);
                else
                {
                    DI.log.info("Mapping dynamically compiled CSharp file: {0}", compiledAssembly.Location);
                    mapAssembliesIntoXRules(new List<string>() {compiledAssembly.Location}, tvTarget_XLoadedRules);
                }
            }
            else
                mapAssembliesIntoXRules(new List<string>() { assemblyWithUnitTest }, tvTarget_XLoadedRules);
        }

        public static void mapAssembliesIntoXRules(IEnumerable<String> filesToProcess, TreeView tvTarget_XLoadedRules)
        {
            var loadedXRules = mapUnitTestToXRules(filesToProcess).ToList();
            // refactor into separate method
            foreach (var fileToProcess in filesToProcess)
            {
                var file_xRules = XRulesEngine.XRules_Compilation.createXRulesFromAssembly(DI.reflection.getAssembly(fileToProcess));
                var file_LoadedXRules = XRulesEngine.XRules_Execution.getLoadedXRules(file_xRules);
                loadedXRules.AddRange(file_LoadedXRules);
            }
            addXLoadedRulesToTreeView(loadedXRules, tvTarget_XLoadedRules, true);
        }

        public static void mapAssembliesIntoXRules(TreeView tvSource_Assemblies, TreeView tvTarget_XLoadedRules)
        {
            tvSource_Assemblies.invokeOnThread(
                () =>
                {
                    if (tvSource_Assemblies.Tag != null &&
                        tvSource_Assemblies.Tag is List<string>)
                    {
                        var xLoadedRules = mapUnitTestToXRules((List<string>)tvSource_Assemblies.Tag);
                        addXLoadedRulesToTreeView(xLoadedRules, tvTarget_XLoadedRules, true);
                    }
                });
        }

        public static void mapLoadedAssembliesUnitTestsToXRules(TreeView tvTarget_XLoadedRules)
        {
            var assembliesInCurrentAppDomain = DI.reflection.getAssembliesInCurrentAppDomain();
            var assembliesThatReferenceNUnit = Classes.UnitTestSupport.getAssembliesThatReferenceNUnit(assembliesInCurrentAppDomain);
            mapAssembliesIntoXRules(assembliesThatReferenceNUnit, tvTarget_XLoadedRules);
        }
    }
}
