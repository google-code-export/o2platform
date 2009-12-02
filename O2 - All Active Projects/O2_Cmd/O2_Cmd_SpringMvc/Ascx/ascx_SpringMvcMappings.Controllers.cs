// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using O2.Cmd.SpringMvc.Classes;
using O2.Cmd.SpringMvc.Objects;
using O2.Core.CIR.CirObjects;
using O2.Core.CIR.CirUtils;
using O2.DotNetWrappers.DotNet;
using O2.DotNetWrappers.Windows;
using O2.Kernel.Interfaces.CIR;

namespace O2.Cmd.SpringMvc.Ascx
{
    partial class ascx_SpringMvcMappings
    {
        public event O2Thread.FuncVoidT1<TreeView> _onTreeViewSelect;

        private bool runOnLoad = true;
        //SpringMvcMappings springMvcMappings = new SpringMvcMappings();

        public ICirData cirData;        // to replace with springMvcMappings

        public Dictionary<SpringMvcController, TreeNode> treeNodesForloadedSpingMvcControllers = new Dictionary<SpringMvcController, TreeNode>();
        public List<SpringMvcController> loadedSpringMvcControllers = new List<SpringMvcController>();   // to replace with springMvcMappings


        public void onLoad()
        {
            if (!DesignMode && runOnLoad)
            {
                //   directoryWithXmlFilesWithJavaMetadata.openDirectory(AnnotationsHelper.tempFolderForAnnotationsXmlFiles);
                setContollersViewMode();
                runOnLoad = false;
            }
        }

        public void showLoadedControllers()
        {
            this.invokeOnThread(() => showLoadedControllers(cbLoadedControlersViewMode.Text));
        }
       
        public void showSpringMvcControllers(List<SpringMvcController> springMvcControllers)
        {
            treeNodesForloadedSpingMvcControllers = new Dictionary<SpringMvcController, TreeNode>();
            loadedSpringMvcControllers = springMvcControllers;
            createTreeNodesForLoadedSpringMvcControllers();
            showLoadedControllers();
        }


        public void createTreeNodesForLoadedSpringMvcControllers()
        {
            treeNodesForloadedSpingMvcControllers = new Dictionary<SpringMvcController, TreeNode>();
            foreach (var springMvcController in loadedSpringMvcControllers)
            {
                if (springMvcController.JavaClassAndFunction != null)
                {

                    var nodeText = springMvcController.HttpRequestUrl;
                    var newTreeNode = O2Forms.newTreeNode(nodeText, nodeText, 0, springMvcController);
                    //newTreeNode.Nodes.Add("HttpMappingParameter: " + springMvcController.HttpMappingParameter ?? "");
                    newTreeNode.Nodes.Add("HttpRequestMethod: " + springMvcController.HttpRequestMethod ??
                                          "");
                    newTreeNode.Nodes.Add("HttpRequestUrl: " + springMvcController.HttpRequestUrl ?? "");
                    newTreeNode.Nodes.Add("HttpMappingParameter: " +
                                          springMvcController.HttpMappingParameter ?? "");
                    //newTreeNode.Nodes.Add("JavaClass: " + springMvcController.JavaClass ?? "");
                    newTreeNode.Nodes.Add("JavaMethod: " + springMvcController.JavaClassAndFunction ?? "");
                    newTreeNode.Nodes.Add(string.Format("File (LineNumber): {0} ({1})", Path.GetFileName(springMvcController.FileName), springMvcController.LineNumber));
                    var httpMappingsParameters = new TreeNode("AutoWiredJavaObjects");
                    for (int parameterIndex = 0;
                         parameterIndex < springMvcController.AutoWiredJavaObjects.Count;
                         parameterIndex++)
                    {
                        var springMvcParameter =
                            springMvcController.AutoWiredJavaObjects[parameterIndex];
                        httpMappingsParameters.Nodes.Add(
                            SpringMvcUtils.getTreeNodeWithAutoWiredObject(cirData, springMvcController.JavaClassAndFunction, springMvcParameter, parameterIndex));
                        //httpMappingsParameters.Nodes.Add(childNodeText);
                    }
                    newTreeNode.Nodes.Add(httpMappingsParameters);
                    treeNodesForloadedSpingMvcControllers.Add(springMvcController, newTreeNode);
                }
            }

        }

        

        /*      
                public TreeNode[] getNodes_()
                {
                    var nodes = new List<TreeNode>();
           
                    return nodes.ToArray();
                }
        */


        public void showLoadedControllers(string viewMode)
        {
            tvControllers.Nodes.Clear();
            tvControllers.Sorted = true;
            try
            {
                switch (viewMode)
                {
                    case "All Loaded Controllers":
                        foreach (var treeNode in treeNodesForloadedSpingMvcControllers.Values)
                            tvControllers.Nodes.Add(treeNode);
                        break;
                    case "With Functions Attributes":
                        tvControllers.Nodes.AddRange(SpringMvcUtils.getNodes_WithFunctionsAttributes(treeNodesForloadedSpingMvcControllers));
                        break;
                    case "Without Functions Attributes":
                        tvControllers.Nodes.AddRange(SpringMvcUtils.getNodes_WithoutFunctionsAttributes(treeNodesForloadedSpingMvcControllers));
                        break;
                    case "Using ModelAttribute":
                        tvControllers.Nodes.AddRange(SpringMvcUtils.getNodes_UsingModelAttribute(treeNodesForloadedSpingMvcControllers));
                        break;
                    case "Using RequestParam":
                        foreach (SpringMvcController springMcvController in treeNodesForloadedSpingMvcControllers.Keys)
                            if (SpringMvcUtils.isMethodUsedInController(springMcvController, "RequestParam"))
                                tvControllers.Nodes.Add(O2Forms.cloneTreeNode(treeNodesForloadedSpingMvcControllers[springMcvController]));
                        break;
                    case "With Functions Attributes and not using ModelAttribbute or RequestParam":
                        foreach (SpringMvcController springMcvController in treeNodesForloadedSpingMvcControllers.Keys)
                            if (springMcvController.AutoWiredJavaObjects.Count > 0 && !SpringMvcUtils.isMethodUsedInController(springMcvController, "RequestParam") && !SpringMvcUtils.isMethodUsedInController(springMcvController, "ModelAttribute"))
                                tvControllers.Nodes.Add(O2Forms.cloneTreeNode(treeNodesForloadedSpingMvcControllers[springMcvController]));
                        break;
                    case "Use GetParameter":
                        foreach (SpringMvcController springMcvController in treeNodesForloadedSpingMvcControllers.Keys)
                        {
                            if (SpringMvcUtils.doesControllerFunctionCallFunction(cirData, springMcvController, CreateFindingsFromMvcData.javaxGetParameterSignatures, false))
                                tvControllers.Nodes.Add(O2Forms.cloneTreeNode(treeNodesForloadedSpingMvcControllers[springMcvController]));
                        }
                        break;
                    case "Use GetParameter (recursive function search)":
                        tvControllers.Nodes.AddRange(CreateFindingsFromMvcData.getNodes_ThatUseGetParameter_RecursiveSearch(cirData, treeNodesForloadedSpingMvcControllers));
                        break;
                    case "Classes binded via ModelAttribute":
                        var listOfClassesBindedViaModelAttribute = new Dictionary<ICirClass, List<TreeNode>>();
                        foreach (SpringMvcController springMcvController in treeNodesForloadedSpingMvcControllers.Keys)
                        {
                            foreach (var springMvcParameter in springMcvController.AutoWiredJavaObjects)
                                if (springMvcParameter.autoWiredMethodUsed == "ModelAttribute")
                                {
                                    if (springMvcParameter.className == null)
                                    {
                                        DI.log.error("in showLoadedControllers, springMvcParameter.autoWiredMethodUsed = ModelAttribute , but springMvcParameter.className == null");
                                    }
                                    else
                                    if (cirData.dClasses_bySignature.ContainsKey(springMvcParameter.className))
                                    {
                                        var bindedCirClass = cirData.dClasses_bySignature[springMvcParameter.className];
                                        if (false == listOfClassesBindedViaModelAttribute.ContainsKey(bindedCirClass))
                                            listOfClassesBindedViaModelAttribute.Add(bindedCirClass, new List<TreeNode>());
                                        listOfClassesBindedViaModelAttribute[bindedCirClass].Add(O2Forms.cloneTreeNode(treeNodesForloadedSpingMvcControllers[springMcvController]));
                                    }
                                }
                        }
                        foreach (var bindedCirClass in listOfClassesBindedViaModelAttribute.Keys)
                            O2Forms.newTreeNode(tvControllers.Nodes, bindedCirClass.Signature, 0, bindedCirClass).Nodes.AddRange(listOfClassesBindedViaModelAttribute[bindedCirClass].ToArray());
                        break;
                    case "Url => Classes binded via ModelAttribute":

                        foreach (SpringMvcController springMcvController in treeNodesForloadedSpingMvcControllers.Keys)
                            foreach (var springMvcParameter in springMcvController.AutoWiredJavaObjects)
                                if (springMvcParameter.autoWiredMethodUsed == "ModelAttribute")
                                    if (cirData.dClasses_bySignature.ContainsKey(springMvcParameter.className))
                                    {
                                        var bindedCirClass = cirData.dClasses_bySignature[springMvcParameter.className];
                                        var nodeText = string.Format("{0}       =>       {1}", springMcvController.HttpRequestUrl, bindedCirClass.Signature);

                                        O2Forms.newTreeNode(tvControllers.Nodes, nodeText, 0, bindedCirClass);
                                    }
                        break;
                    default:
                        setContollersViewMode();
                        break;
                }
                lbNumberOfControllersLoaded.Text = loadedSpringMvcControllers.Count.ToString();
                lbNumberOfNodesShown.Text = string.Format("Showing {0} of {1} ", tvControllers.Nodes.Count,
                                                             treeNodesForloadedSpingMvcControllers.Keys.Count);
            }
            catch (Exception ex)
            {
                DI.log.error("in showLoadedControllers:{0}", ex.Message);
            }
        }

        

        public void setContollersViewMode()
        {
            cbLoadedControlersViewMode.Items.Clear();
            cbLoadedControlersViewMode.Items.Add("All Loaded Controllers");
            cbLoadedControlersViewMode.Items.Add("Classes binded via ModelAttribute");
            cbLoadedControlersViewMode.Items.Add("Url => Classes binded via ModelAttribute");
            cbLoadedControlersViewMode.Items.Add("With Functions Attributes");
            cbLoadedControlersViewMode.Items.Add("Without Functions Attributes");
            cbLoadedControlersViewMode.Items.Add("Using ModelAttribute");
            cbLoadedControlersViewMode.Items.Add("Using RequestParam");
            cbLoadedControlersViewMode.Items.Add("With Functions Attributes and not using ModelAttribbute or RequestParam");
            cbLoadedControlersViewMode.Items.Add("Use GetParameter");
            cbLoadedControlersViewMode.Items.Add("Use GetParameter (recursive function search)");
            cbLoadedControlersViewMode.Items.Add("refresh setContollersViewMode");
            cbLoadedControlersViewMode.SelectedIndex = 0;
        }
      

        private void handleDrop(string fileOrFolderToProcess)
        {
            if (File.Exists(fileOrFolderToProcess))
            {
                var springMvcMappings = (SpringMvcMappings) Serialize.getDeSerializedObjectFromXmlFile(fileOrFolderToProcess, typeof (SpringMvcMappings));
                if (springMvcMappings == null)
                {
                    DI.log.error("in handleDrop: Could not create SpringMvcMappings object from :{0}", fileOrFolderToProcess);
                    return;
                }

                var cirDataFileToLoad = Path.Combine(Path.GetDirectoryName(fileOrFolderToProcess), springMvcMappings.cirDataFile);
                if (false == File.Exists(cirDataFileToLoad))
                {
                    DI.log.error("in handleDrop: Could not find cirData object to load :{0}", cirDataFileToLoad);
                    return;
                }
                cirData = CirLoad.loadSerializedO2CirDataObject(cirDataFileToLoad);
                if (cirData == null)
                {
                    DI.log.error("in handleDrop: Could not create CirData object from :{0}", cirDataFileToLoad);
                    return;
                }
                loadedSpringMvcControllers = springMvcMappings.controllers;
                createTreeNodesForLoadedSpringMvcControllers();
                showLoadedControllers();
            }        
        }


        public void saveMappedControllers(string targetFolder)
        {
            SpringMvcUtils.saveMappedControllers(targetFolder,loadedSpringMvcControllers,cirData);
        }

        public void loadMappedControllers(ICirData _cirData, List<SpringMvcController> _springMvcControllers)
        {
            cirData = _cirData;
            loadedSpringMvcControllers = _springMvcControllers;
            createTreeNodesForLoadedSpringMvcControllers();
            showLoadedControllers();
        }

        public void loadMappedControllers(string fileToLoad)
        {
            handleDrop(fileToLoad);
        }

        public void selectControllerThatMatchesUri(Uri uriToMatch, string urlOfWebApplicationRoot, bool onlyMapToPOST)
        {
            this.invokeOnThread(
                () =>
                    {
                        var normalizedRequest = uriToMatch.AbsoluteUri.Replace(urlOfWebApplicationRoot, "");
                        if (normalizedRequest.IndexOf(';') > -1)
                            normalizedRequest = normalizedRequest.Split(';')[0];
                        if (normalizedRequest.IndexOf('?') > -1)
                            normalizedRequest = normalizedRequest.Split('?')[0];
                        DI.log.info("trying to find mapping to: {0} {0}",uriToMatch.Scheme, normalizedRequest);

                        foreach(TreeNode node in tvControllers.Nodes)
                        {
                            if (node.Tag != null && node.Tag is SpringMvcController)
                            {
                                var springMvcController = (SpringMvcController) node.Tag;
                                if (springMvcController.HttpRequestUrl == normalizedRequest)
                                {
                                    if (onlyMapToPOST == false || springMvcController.HttpRequestMethod == "POST")
                                    {
                                        tvControllers.SelectedNode = node;
                                        return;
                                    }
                                }
                            }
                        }


                    });
        }
    }
}
