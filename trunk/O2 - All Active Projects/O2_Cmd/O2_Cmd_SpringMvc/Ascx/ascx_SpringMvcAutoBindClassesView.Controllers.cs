// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using O2.Core.CIR.CirObjects;
using O2.DotNetWrappers.DotNet;
using O2.DotNetWrappers.ExtensionMethods;
using O2.DotNetWrappers.Filters;
using O2.DotNetWrappers.Windows;
using O2.Interfaces.CIR;
using O2.Kernel.CodeUtils;

namespace O2.Cmd.SpringMvc.Ascx
{
    public partial class  ascx_SpringMvcAutoBindClassesView
    {
        public ICirData cirData;
        public ICirClass cirRootClass;
        public List<ICirClass> cirClassMapped = new List<ICirClass>();
        public O2Thread.FuncVoid onMapClassComplete;

        public void mapClass(ICirClass cirClass, ICirData _cirData)
        {
            mapClass(cirClass, _cirData, true);
        }

        public void mapClass(ICirClass cirClass, ICirData _cirData, bool clearPreviousList)
        {

            cirData = _cirData;
            cirRootClass = cirClass;
            mapCurrentClass(clearPreviousList);
        }

        public void mapCurrentClass(bool clearPreviousList)
        {
            this.invokeOnThread(() => loadLoadBindableFieldsIntoTreeView(tvBindableFields, cbHideGetAndSetStrings.Checked, clearPreviousList));
            //                                       dgvResolvedCommandName.SelectedRows[0].Cells["value"].Value.
            //                                           ToString(), cbHideGetAndSetStrings.Checked, fadCirData);
        }

        private int numberOfItemsProcessed = 0;

        public void loadLoadBindableFieldsIntoTreeView(TreeView tvTargetTreeView, bool bHideGetAndSetStrings, bool clearPreviousList)
        {
            numberOfItemsProcessed = 0;
            cirClassMapped = new List<ICirClass>();
            if (clearPreviousList)
                tvTargetTreeView.Nodes.Clear();
            // make the first char upper case
            addBindableFieldsIntoTreeView_Recursive(tvTargetTreeView.Nodes, cirRootClass, bHideGetAndSetStrings);
            tvTargetTreeView.ExpandAll();
            Callbacks.raiseRegistedCallbacks(onMapClassComplete);
        }

        public bool addBindableFieldsIntoTreeView_Recursive(TreeNodeCollection tncTreeNodes, ICirClass targetCirClass, bool bHideGetAndSetStrings)
        {
            if (cirClassMapped.Contains(targetCirClass))
            {
                //tncTreeNodes.Add(targetCirClass.Signature + " : RECURSIVE CALL");
                return true;
            }


            if (targetCirClass.Signature != "java.util.List")
                cirClassMapped.Add(targetCirClass);

            //var classSourceCodeLine = O2.Core.CIR.CirUtils.ViewHelpers.GetMappedLineNumber(targetCirClass.Name, targetCirClass.File, targetCirClass.FileLine, false, true);
            //if (classSourceCodeLine > 0)
            //{
            //    var classSourceCodetext = Files.getLineFromSourceCode(targetCirClass.File, (uint)classSourceCodeLine);
            //}            
            // check for max depth
            var nodeCountToStopRecursion = 200;
            if (tvBindableFields.GetNodeCount(true) > nodeCountToStopRecursion)
            //if (tncTreeNodes.Count > 0 && O2Forms.getStringListWithAllParentNodesName(tncTreeNodes[0]).Count > maxDepth)
            {
                tncTreeNodes.Add("***** MAX node count (for recursive Search) reached: " + nodeCountToStopRecursion);
                return false;
            }

            if (numberOfItemsProcessed++ > 20)
            //if (tncTreeNodes.Count > 0 && O2Forms.getStringListWithAllParentNodesName(tncTreeNodes[0]).Count > maxDepth)
            {
                tncTreeNodes.Add("***** MAX numberOfItemsProcessed count (for recursive Search) reached: " + nodeCountToStopRecursion);
                numberOfItemsProcessed = 0;
                return false;
            }



            //String sFixedClassToFind = sClassToFind[0].ToString().ToUpper() + sClassToFind.Substring(1);
            //foreach (ICirClass ccCirClass in fadCirData.dClasses_bySignature.Values)
            //{
            //   if (ccCirClass.Name == sFixedClassToFind)
            {
                var tnClass = new TreeNode(targetCirClass.Signature);

                foreach (ICirFunction cfCirFunction in O2.Core.CIR.CirUtils.CirDataAnalysisUtils.getListOfInheritedMethods(targetCirClass, true))
                {
                    String sFunctionName = cfCirFunction.FunctionName; //new FilteredSignature(cfCirFunction.FunctionSignature).sFunctionName;
                    switch (sFunctionName.Substring(0, 3))
                    {
                        case "get":
                            if (cfCirFunction.FunctionParameters.Count == 0 && cfCirFunction.FunctionNameAndParameters.IndexOf("()") > -1)        // spring will only invoke this is the getter has no parameters
                            {
                                var tnSubObject = new TreeNode(sFunctionName);
                                String sFixedSubObjectName = sFunctionName.Replace("get", "");
                                if (bHideGetAndSetStrings)
                                    tnSubObject.Text = sFixedSubObjectName;

                                //tnSubObject = tnClass;
                                var returnType = cfCirFunction.ReturnType.Replace("&", "").Replace("[]", "");
                                if (returnType != "char" && returnType != "char[]" && returnType != "void" && returnType != "long" && returnType != "int" && returnType != "bool" &&
                                     returnType != "wchar_t" && returnType != "double" && returnType != "float" && returnType != "short" && returnType != "java.math.BigDecimal" &&
                                    returnType != "java.lang.String" && returnType != "java.util.Date" && returnType != "java.lang.Object" /* && returnType != "java.util.List"*/ &&
                                    returnType != "java.lang.Integer" && returnType != "java.lang.Long" && returnType != "java.lang.Double" &&
                                    returnType != "org.joda.time.LocalTime" && returnType != "org.joda.time.LocalDate" && returnType != "org.joda.time.LocalDateTime")
                                {
                                    if (cirData.dClasses_bySignature.ContainsKey(returnType))
                                    {
                                        switch (returnType)
                                        {
                                            case "java.lang.Class":             // special class for if we get a Class back (need to try exploitability)

                                                tnSubObject.Text += "   - returns  java.lang.Class ";
                                                if (bHideGetAndSetStrings)
                                                    tncTreeNodes.Add(tnSubObject);
                                                else
                                                    tnClass.Nodes.Add(tnSubObject);
                                                break;
                                            case "java.util.List":
                                            case "java.util.Set":
                                                // lets see if the source code as a clue about which class this list maps to:
                                                ICirClass resolvedClass = null;
                                                try
                                                {
                                                    if (cfCirFunction.File != null && cfCirFunction.FileLine != null)
                                                    {
                                                        var sourceCodeLine = O2.DotNetWrappers.Windows.Files.getLineFromSourceCode(cfCirFunction.File, UInt32.Parse(cfCirFunction.FileLine),false);
                                                        var specialTagTextStart = "O2Helper:MVCAutoBindListObject:";
                                                        var indexOfSpecialTagStart = sourceCodeLine.IndexOf(specialTagTextStart);
                                                        if (indexOfSpecialTagStart > -1)
                                                        {
                                                            indexOfSpecialTagStart += specialTagTextStart.Length;
                                                            int indexOfSpecialTagEnd = sourceCodeLine.LastIndexOf("*/");
                                                            if (indexOfSpecialTagEnd > indexOfSpecialTagStart)
                                                            {
                                                                var specialTag = sourceCodeLine.Substring(indexOfSpecialTagStart, indexOfSpecialTagEnd - indexOfSpecialTagStart);
                                                                if (cirData.dClasses_bySignature.ContainsKey(specialTag))
                                                                {
                                                                    resolvedClass = cirData.dClasses_bySignature[specialTag];
                                                                }
                                                            }
                                                        }

                                                    }
                                                }
                                                catch (Exception ex)
                                                {
                                                    DI.log.error("in addBindableFieldsIntoTreeView_Recursive, while mapping java.util.List:", ex.Message);
                                                }
                                                if (resolvedClass == null)
                                                {
                                                    tnSubObject.Text += " -- JAVA.UTIL.LIST (you must add a Tag like /*[O2Helper:MVCAutoBindListObject:*/ to the source code so that we can calculate the corrent object binding";
                                                    tncTreeNodes.Add(tnSubObject);
                                                }
                                                else
                                                {
                                                    //   tnSubObject.Text += "GOT MAPPING";
                                                    //   tncTreeNodes.Add(tnSubObject);                                                        
                                                    cirClassMapped.Remove(targetCirClass);  // take it out since we haven't really added it
                                                    addBindableFieldsIntoTreeView_Recursive(tnSubObject.Nodes, resolvedClass, bHideGetAndSetStrings);
                                                    //cirClassMapped.Add(targetCirClass);     // and add it here
                                                    if (bHideGetAndSetStrings)
                                                        tncTreeNodes.Add(tnSubObject);
                                                    else
                                                        tnClass.Nodes.Add(tnSubObject);
                                                }
                                                break;
                                            default:
                                                //var targetTreeNodeCollection = (bHideGetAndSetStrings) ? tncTreeNodes : tnClass.Nodes;
                                                var repeatedClass = addBindableFieldsIntoTreeView_Recursive(tnSubObject.Nodes, cirData.dClasses_bySignature[returnType], bHideGetAndSetStrings);
                                                if (repeatedClass)
                                                    tnSubObject.Text += "    - repeated class': " + returnType;

                                                if (repeatedClass || tnSubObject.Nodes.Count > 0)
                                                    if (bHideGetAndSetStrings)
                                                        tncTreeNodes.Add(tnSubObject);
                                                    else
                                                        tnClass.Nodes.Add(tnSubObject);
                                                break;
                                        }
                                    }
                                    else
                                        if (returnType != "")
                                            DI.log.error("On function {0} Could not find return type {1}", cfCirFunction.FunctionSignature, returnType);
                                }
                            }
                            break;
                        case "set":
                            //String sSetNodeText = new FilteredSignature(cfCirFunction.FunctionSignature).sFunctionName;
                            String sSetNodeText = cfCirFunction.FunctionNameAndParameters;
                            if (bHideGetAndSetStrings)
                                sSetNodeText = sSetNodeText.Substring(3); //.Replace("set", "");                                
                            var newNode = new TreeNode(sSetNodeText);
                            newNode.ForeColor = System.Drawing.Color.Red;

                            if (bHideGetAndSetStrings)
                                tncTreeNodes.Add(newNode);
                            else
                                tnClass.Nodes.Add(newNode);

                            break;
                        default:
                            break;
                    }
                }
                if (tnClass.Nodes.Count > 0)
                    tncTreeNodes.Add(tnClass);
                //}
                //    String sClassName = 
            }
            return false;
        }

        public void showClass(ICirClass cirClass, ICirData _cirData)
        {
            mapClass(cirClass, _cirData);
        }

        public void setHideOrShowGetAndSetStrings(bool value)
        {
            this.invokeOnThread(() => cbHideGetAndSetStrings.Checked = value);
        }

        public List<string> getResolvedListOfVariablesInvokable()
        {
            var resolvedVariables = new List<string>();
            resolveListOfVariablesInvokable_Recursive(tvBindableFields.Nodes, resolvedVariables, "");
            return resolvedVariables;
        }

        public void resolveListOfVariablesInvokable_Recursive(TreeNodeCollection nodes, List<String> resolvedVariables, string currentVarPrefix)
        {
            if (nodes != null)
                foreach(TreeNode node in nodes)
                {
                    var nodeNameWithLowerCaseFirstLeter = node.Text[0].ToString().ToLower() + node.Text.Substring(1);
                    var currentVarName = currentVarPrefix + ((currentVarPrefix != "") ? "." : "") + nodeNameWithLowerCaseFirstLeter;
                    if (node.Nodes.Count == 0)
                    {                        
                        var indexOfLeftParentisis = currentVarName.IndexOf('(');
                        if (indexOfLeftParentisis > -1)
                            resolvedVariables.Add(currentVarName.Substring(0, indexOfLeftParentisis));
                        else
                            resolvedVariables.Add(currentVarName);
                    }
                    else
                        resolveListOfVariablesInvokable_Recursive(node.Nodes, resolvedVariables, currentVarName);
                }
        }

        public void clearLoadedData()
        {
            this.invokeOnThread(() =>
                                    {
                                        cirData = null;
                                        cirRootClass = null;
                                        cirClassMapped = new List<ICirClass>();
                                        tvBindableFields.Nodes.Clear();
                                    });
        }
    }
}
