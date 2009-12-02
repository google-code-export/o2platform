using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using O2.Core.CIR.CirUtils;
using O2.DotNetWrappers.DotNet;
using O2.DotNetWrappers.Filters;
using O2.DotNetWrappers.Windows;
using O2.Kernel.CodeUtils;
using O2.Kernel.Interfaces.CIR;
using O2.Kernel.Interfaces.Views;
using O2.Views.ASCX.DataViewers;

namespace O2.Core.CIR.Ascx
{
    public partial class ascx_FunctionCalls
	{        
        public ICirFunction rootCirFunction { get; set; }
        public ICirClass currentCirClass { get; set; }
     //   public ICirDataAnalysis cirDataAnalysis { get; set; }

        /*public static void viewCirFunctionSignature(string functionSignature, ICirDataAnalysis _cirDataAnalysis)
        {
            if (_cirDataAnalysis.dCirFunction_bySignature.ContainsKey(functionSignature))
                viewCirFunctionSignature(_cirDataAnalysis.dCirFunction_bySignature[functionSignature], _cirDataAnalysis); 
        }*/
        public void viewCirFunction(ICirFunction _rootCirFunction)
        {
            if (_rootCirFunction != null)
                this.invokeOnThread(
                    () =>
                        {
                            showFunctionAttributes(_rootCirFunction);
                            laFunctionViewed.Text = _rootCirFunction.FunctionSignature;
                            rootCirFunction = _rootCirFunction;
                            currentCirClass = _rootCirFunction.ParentClass;
                            
                            //tvFunctionIsCalledBy
                            tvFunctionIsCalledBy.Nodes.Clear();
                            ViewHelpers.addCirFunctionToTreeNodeCollection(rootCirFunction, "",
                                                                           tvFunctionIsCalledBy.Nodes,
                                                                           rootCirFunction.FunctionIsCalledBy.Count > 0);
                            O2Forms.expandNodes(tvFunctionIsCalledBy);

                            //tvFunctionMakesCallsTo
                            tvFunctionMakesCallsTo.Nodes.Clear();
                            ViewHelpers.addCirFunctionToTreeNodeCollection(rootCirFunction, "",
                                                                           tvFunctionMakesCallsTo.Nodes,
                                                                           rootCirFunction.FunctionsCalledUniqueList.Count > 0);
                            O2Forms.expandNodes(tvFunctionMakesCallsTo);

                            //tvFunctionInfo
                            tvFunctionInfo.Nodes.Clear();
                            ViewHelpers.addCirFunctionToTreeNodeCollection(rootCirFunction, "", tvFunctionInfo.Nodes,
                                                                           rootCirFunction.FunctionsCalledUniqueList.Count > 0 || rootCirFunction.FunctionIsCalledBy.Count > 0);
                            O2Forms.expandNodes(tvFunctionInfo);

                            //tvClassSuperClasses
                            tvClassSuperClasses.Nodes.Clear();
                            if (rootCirFunction.ParentClass != null)
                            {
                                ViewHelpers.addCirClassToTreeNodeCollection(rootCirFunction.ParentClass, "", tvClassSuperClasses.Nodes,
                                                                            rootCirFunction.ParentClass.dSuperClasses.Count > 0);
                                O2Forms.expandNodes(tvClassSuperClasses);
                            }

                            //tvClassSuperClasses
                            tvClassIsSuperClassedBy.Nodes.Clear();
                            if (rootCirFunction.ParentClass != null)
                            {
                                ViewHelpers.addCirClassToTreeNodeCollection(rootCirFunction.ParentClass, "", tvClassIsSuperClassedBy.Nodes,
                                                                            rootCirFunction.ParentClass.dIsSuperClassedBy.Count > 0);
                                O2Forms.expandNodes(tvClassIsSuperClassedBy);
                            }

                            cbCirFunction_IsTainted.Checked = rootCirFunction.IsTainted;

                            viewClassMethods();

                        });
        }

        

        public void viewClassMethods()
        {
            if (currentCirClass!=  null)
                this.invokeOnThread(() => viewClassMethods(functionViewerForClassMethods, currentCirClass, cbViewInheritedMethods.Checked, cbIgnoreCoreObjectClass.Checked));
        }
        

        public static void viewCirFunctionSignatureOnNewForm(ICirFunction cirFunction)
        {
            O2Thread.mtaThread(
                () =>
                {
                    var windowName = string.Format("Function Viewer: {0}",cirFunction.FunctionSignature);                    
                    O2Messages.openControlInGUISync(typeof(ascx_FunctionCalls), O2DockState.Float, windowName);
                    O2Messages.getAscx(windowName,
                                       ascxControl =>
                                       ((ascx_FunctionCalls)ascxControl).viewCirFunction(cirFunction));
                }
                );
        }


        /*    private void onSelectedItemTreeViewBeforeExpand(TreeNode selectedTreeNode)
        {
            //return;
            if (selectedTreeNode != null && selectedTreeNode.Tag is ICirFunction)
            {
                selectedTreeNode.Nodes.Clear();

                /*     var filteredSignature = (FilteredSignature)selectedTreeNode.Tag;
                     var signature = filteredSignature.sOriginalSignature;
                     if (cirDataAnalysis.dCirFunction_bySignature.ContainsKey(signature))
                     {* /
                var cirFunction = (ICirFunction)selectedTreeNode.Tag;
                // makesCallToNode
                //var makesCallToNode = new TreeNode("Makes calls to:");
                foreach (var makesCallsTo in cirFunction.FunctionsCalledUniqueList)
                    addCirFunctionToTreeNodeCollection(makesCallsTo, "=>", selectedTreeNode.Nodes);
                // isCalledByNode

                //var isCalledByNode = new TreeNode("Is called by:");
                foreach (var isCalledBy in cirFunction.FunctionIsCalledBy)
                    addCirFunctionToTreeNodeCollection(isCalledBy, "<=", selectedTreeNode.Nodes);
                /*  }
                  else
                      selectedTreeNode.Nodes.Add("Could not find the signature:" + signature);*/
                /*     var functionNode = new TreeNode("Function: " + cirFunction.FunctionName);

                     // makesCallToNode
                     var makesCallToNode = new TreeNode("Makes calls to:");
                     foreach (var makesCallsTo in cirFunction.FunctionsCalledUniqueList)
                         makesCallToNode.Nodes.Add(makesCallsTo.FunctionName);
                     // isCalledByNode
                     var isCalledByNode = new TreeNode("Is called by:");
                     foreach (var isCalledBy in cirFunction.FunctionIsCalledBy)
                         isCalledByNode.Nodes.Add(isCalledBy.FunctionName);

                     functionNode.Nodes.Add(makesCallToNode);
                     functionNode.Nodes.Add(isCalledByNode);
                     tvSelectedItemInfo.Nodes.Add(functionNode);
                     ;* /
            }
        }

        */


        public void viewCirClass(ICirClass cirClass)
        {
            if (cirClass != null)
                this.invokeOnThread(
                    () =>
                    {
                        showClassAttributes(cirClass);
                        laFunctionViewed.Text = cirClass.Signature;
                        rootCirFunction = null;
                        currentCirClass = cirClass;
                        
                        tvFunctionIsCalledBy.Nodes.Clear();                        
                        tvFunctionMakesCallsTo.Nodes.Clear();                        
                        tvFunctionInfo.Nodes.Clear();

                        //tvClassSuperClasses
                        tvClassSuperClasses.Nodes.Clear();
                        ViewHelpers.addCirClassToTreeNodeCollection(cirClass, "", tvClassSuperClasses.Nodes,
                                                                       cirClass.dSuperClasses.Count > 0);
                        O2Forms.expandNodes(tvClassSuperClasses);

                        //tvClassIsSuperClassedBy
                        tvClassIsSuperClassedBy.Nodes.Clear();
                        ViewHelpers.addCirClassToTreeNodeCollection(cirClass, "", tvClassIsSuperClassedBy.Nodes,
                                                                       cirClass.dIsSuperClassedBy.Count > 0);
                        O2Forms.expandNodes(tvClassIsSuperClassedBy);

                        viewClassMethods(functionViewerForClassMethods, cirClass, cbViewInheritedMethods.Checked, cbIgnoreCoreObjectClass.Checked);

                        cbCirFunction_IsTainted.Checked = false;


                    });
        }
        

        public static void viewClassMethods(ascx_FunctionsViewer targetFunctionsViewer , ICirClass targetClass, bool showInheritedMethods, bool ignoreCoreObjectClass)
        {
            if (targetClass != null)
            {
                O2Thread.mtaThread(
                    () =>
                        {
                            targetFunctionsViewer.clearLoadedSignatures();
                            targetFunctionsViewer.setNamespaceDepth(0);
                            var signaturesToShow = new List<string>();
                            if (showInheritedMethods)
                            {
                                List<ICirFunction> inheritedSignatures = CirDataAnalysisUtils.getListOfInheritedMethods(targetClass, ignoreCoreObjectClass);
                                foreach (var inheritedSignature in inheritedSignatures)
                                    signaturesToShow.Add(inheritedSignature.FunctionSignature);
                            }
                            else
                                signaturesToShow.AddRange(targetClass.dFunctions.Keys.ToList());
                            
                            targetFunctionsViewer.showSignatures(signaturesToShow);
                            /*var thread = targetFunctionsViewer.showSignatures(signaturesToShow);
                            if (thread != null)
                            {
                                thread.Join();
                                targetFunctionsViewer.expandNodes();
                            } */                                                     
                        });
            }

        }

        private void showClassAttributes(ICirClass cirClass)
        {
            if (cirClass != null)
            {
                tvClassAttributes.Nodes.Clear();
                foreach (var attribute in cirClass.ClassAttributes)
                {
                    var signature = new FilteredSignature(attribute.AttributeClass).sSignature;
                    var treeNode = tvClassAttributes.Nodes.Add(signature);
                    foreach (var parameter in attribute.Parameters)
                        treeNode.Nodes.Add(parameter.Key);
                }
            }
        }

        private void showFunctionAttributes(ICirFunction _rootCirFunction)
        {
            showClassAttributes(_rootCirFunction.ParentClass);
            tvFunctionAttributes.Nodes.Clear();
            foreach (var attribute in _rootCirFunction.FunctionAttributes)
            {
                var signature = new FilteredSignature(attribute.AttributeClass).sSignature;
                var treeNode = tvFunctionAttributes.Nodes.Add(signature);
                foreach (var parameter in attribute.Parameters)
                    treeNode.Nodes.Add(parameter.Key);
            }
        }
        
	}
}
