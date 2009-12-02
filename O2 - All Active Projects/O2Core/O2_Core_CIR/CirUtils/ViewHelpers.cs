using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using O2.Core.CIR.CirObjects;
using O2.Core.CIR.Xsd;
using O2.DotNetWrappers.DotNet;
using O2.DotNetWrappers.Windows;
using O2.Kernel.CodeUtils;
using O2.Kernel.Interfaces.CIR;

namespace O2.Core.CIR.CirUtils
{
    public class ViewHelpers
    {
        public static void showFunctionBlocksInWebBrower(List<ControlFlowGraphBasicBlock> lcfgBasicBlocks,
                                                         WebBrowser webBrowser)
        {
            if (lcfgBasicBlocks != null && lcfgBasicBlocks.Count > 0)
            {
                var blockXml = new StringBuilder();
                foreach (ControlFlowGraphBasicBlock basicBlock in lcfgBasicBlocks)
                {
                    //var tempFile = DI.o2CorLibConfig.TempFileNameInTempDirectory;
                    string xmlContentOfBasicBlock = Serialize.createSerializedXmlStringFromObject(basicBlock,null);
                    if (xmlContentOfBasicBlock != "")
                    {
                        blockXml.AppendLine("<BASICBLOCK>" + xmlContentOfBasicBlock + "</BASICBLOCK>");
                    }
                }

                // Hack to create Xml Doc (replace with proper viewer)
                var xmlDocument = new XmlDocument();

                //var root = xmlDocument.DocumentElement;
                XmlElement childNode = xmlDocument.CreateElement("Content");
                childNode.InnerXml = blockXml.ToString().Replace("<?xml version=\"1.0\"?>", "");
                xmlDocument.AppendChild(childNode);

                string tempXmlFile = DI.config.TempFileNameInTempDirectory + ".xml";
                xmlDocument.Save(tempXmlFile);
                webBrowser.Navigate(tempXmlFile);
            }
        }

        public static List<string> getCirFunctionStringList(List<ICirFunctionCall> cirFunctionsCalls)
        {
            var functionsList = new List<string>();
            foreach (ICirFunctionCall cirFunctionCall in cirFunctionsCalls)
                functionsList.Add(cirFunctionCall.cirFunction.FunctionSignature);
            return functionsList;
        }

        public static List<string> getCirFunctionStringList(List<ICirFunction> cirFunctions)
        {
            var functionsList = new List<string>();
            foreach(ICirFunction cirFunction in cirFunctions)
                functionsList.Add(cirFunction.FunctionSignature);
            return functionsList;
        }

        internal static List<string> getCirParameterTypeStringList(List<ICirFunctionParameter> functionParameters)
        {
            var parameterTypes = new List<string>();
            foreach (ICirFunctionParameter functionParameter in functionParameters)
                parameterTypes.Add(functionParameter.ParameterType);
            return parameterTypes;
        }

        public static TreeNode addCirFunctionToTreeNodeCollection(ICirFunction cirFunction, string treeNodeNamePrefix, TreeNodeCollection treeNodeCollection, bool addDummyChildNode)
        {
            int mappedLineNumber = GetMappedLineNumber(cirFunction.FunctionName, cirFunction.File, cirFunction.FileLine, false, true);
            return addCirFunctionToTreeNodeCollection(new CirFunctionCall(cirFunction, cirFunction.File, mappedLineNumber), treeNodeNamePrefix, treeNodeCollection, addDummyChildNode);
        }

        public static TreeNode addCirFunctionToTreeNodeCollection(ICirFunctionCall functionCall, string treeNodeNamePrefix, TreeNodeCollection treeNodeCollection, bool addDummyChildNode)
        {
            if (functionCall == null || functionCall.cirFunction == null)
                return null;
            var cirFunction = functionCall.cirFunction;
            
            var treeNodeName = String.Format(" {0}{1} :                {2}", treeNodeNamePrefix, cirFunction.FunctionName, cirFunction.ClassNameFunctionNameAndParameters);
            var newTreeNode = O2Forms.newTreeNode(treeNodeCollection, treeNodeName, cirFunction.FunctionSignature, 0, functionCall);
            if (addDummyChildNode)
                newTreeNode.Nodes.Add(""); // Dummy node so that we have the plus sign
            if (functionCall.fileName != null && functionCall.lineNumber > 0)
                newTreeNode.ForeColor = System.Drawing.Color.DarkGreen;
            else
                newTreeNode.ForeColor = System.Drawing.Color.Red;
            return newTreeNode;            
        }

        public static TreeNode addCirClassToTreeNodeCollection(ICirClass cirClass, string treeNodeNamePrefix, TreeNodeCollection treeNodeCollection, bool addDummyChildNode)
        {
            if (cirClass == null)
                return null;
            //if (cirFunction.FunctionSignature != "")
            //{
            var treeNodeName = String.Format(" => {0}",
                                             cirClass.Signature);
            var newTreeNode = O2Forms.newTreeNode(treeNodeCollection, treeNodeName, cirClass.Signature, 0,
                                                      cirClass);
                if (addDummyChildNode)
                    newTreeNode.Nodes.Add(""); // Dummy node so that we have the plus sign
                return newTreeNode;
            //}
        }

        public static void onBeforeExpand_tvFunctionIsCalledBy(TreeNode selectedTreeNode, bool dontAddRecursiveCalls)
        {
            onBeforeExpand(selectedTreeNode, addToNode_IsCalledBy, dontAddRecursiveCalls);

           // if (selectedTreeNode != null && selectedTreeNode.Tag is ICirFunction)
           // {
                /*selectedTreeNode.Nodes.Clear();
                var cirFunction = (ICirFunction) selectedTreeNode.Tag;
                addToNode_IsCalledBy(cirFunction, selectedTreeNode, false);*/
                //        selectedTreeNode.Expand();
           // }
        }

        public static void onBeforeExpand_tvFunctionMakesCallsTo(TreeNode selectedTreeNode, bool dontAddRecursiveCalls)
        {
            onBeforeExpand(selectedTreeNode, addToNode_MakesCallsTo, dontAddRecursiveCalls);
        }
        

        public static void onBeforeExpand(TreeNode selectedTreeNode, O2Thread.FuncVoidT1T2T3T4 <ICirFunctionCall, TreeNode, bool,bool> addToNodeFunction, bool dontAddRecursiveCalls)
        {
            if (selectedTreeNode != null)
            {
                ICirFunctionCall functionCall = null;
                if (selectedTreeNode.Tag is ICirFunctionCall)
                {
                    functionCall = (ICirFunctionCall)selectedTreeNode.Tag;
                    //addToNode_MakesCallsTo(functionCall, selectedTreeNode, false);
                }
                else if (selectedTreeNode.Tag is ICirFunction)
                {
                    selectedTreeNode.Nodes.Clear();
                    functionCall = new CirFunctionCall((ICirFunction)selectedTreeNode.Tag);

                }
                if (functionCall != null)
                {
                    selectedTreeNode.Nodes.Clear();
                    addToNodeFunction(functionCall, selectedTreeNode, false, dontAddRecursiveCalls);
                }
            }
        }

        public static void onBeforeExpand_tvFunctionInfo(TreeNode selectedTreeNode)
        {
            /*if (selectedTreeNode != null && selectedTreeNode.Tag is ICirFunction)
            {
                selectedTreeNode.Nodes.Clear();
                var cirFunction = (ICirFunction)selectedTreeNode.Tag;
                addToNode_IsCalledBy(cirFunction, selectedTreeNode, true);
                addToNode_MakesCallsTo(cirFunction, selectedTreeNode,true);
            }*/
        }

        public static void addToNode_IsCalledBy(ICirFunctionCall functionCall, TreeNode targetTreeNode, bool alwaysAddDummyChildNode, bool dontAddRecursiveCalls)
        {
            // NOTE: dontAddRecursiveCalls not implemented (yet)
            var cirFunction = functionCall.cirFunction;
            foreach (var isCalledBy in cirFunction.FunctionIsCalledBy)
                addCirFunctionToTreeNodeCollection(isCalledBy,  "<= ", targetTreeNode.Nodes,
                    alwaysAddDummyChildNode || isCalledBy.cirFunction.FunctionIsCalledBy.Count > 0);                            
        }

        public static void addToNode_MakesCallsTo(ICirFunctionCall functionCall, TreeNode targetTreeNode, bool alwaysAddDummyChildNode, bool addDontAddRecursiveCalls)
        {
            var cirFunction = functionCall.cirFunction;            
            var parentNodes =  O2Forms.getStringListWithAllParentNodesName(targetTreeNode);                        

            //foreach (var makesCallsTo in cirFunction.FunctionsCalledUniqueList)
            foreach (var makesCallsTo in cirFunction.FunctionsCalled)
            {
                var recursiveCall = parentNodes.Contains(makesCallsTo.cirFunction.FunctionSignature);
                if (recursiveCall && addDontAddRecursiveCalls)
                {
                    var nodeText = string.Format("R: {0} : {1} : {2}", makesCallsTo.cirFunction.FunctionName, "....(Recursive Call so not expanding child calls", makesCallsTo.cirFunction.FunctionSignature);
                    targetTreeNode.Nodes.Add(nodeText);
                }
                else
                    addCirFunctionToTreeNodeCollection(makesCallsTo, "=> ", targetTreeNode.Nodes,
                        alwaysAddDummyChildNode || makesCallsTo.cirFunction.FunctionsCalled.Count > 0);
            }
        }


        public static void onBeforeExpand_tvClassSuperClasses(TreeNode selectedTreeNode)
        {
            if (selectedTreeNode != null && selectedTreeNode.Tag is ICirClass)
            {
                selectedTreeNode.Nodes.Clear();
                var cirClass = (ICirClass)selectedTreeNode.Tag;
                foreach (var superClass in cirClass.dSuperClasses.Values)
                    addCirClassToTreeNodeCollection(superClass, " => ", selectedTreeNode.Nodes,superClass.dSuperClasses.Count > 0);                
            }
        }

        public static void onBeforeExpand_tvClassIsSuperClassedBy(TreeNode selectedTreeNode)
        {
            if (selectedTreeNode != null && selectedTreeNode.Tag is ICirClass)
            {
                selectedTreeNode.Nodes.Clear();
                var cirClass = (ICirClass)selectedTreeNode.Tag;
                foreach (var superClass in cirClass.dIsSuperClassedBy.Values)
                    addCirClassToTreeNodeCollection(superClass, " <= ", selectedTreeNode.Nodes, superClass.dIsSuperClassedBy.Count > 0);
            }
            
        }

        public static void raiseSourceCodeReferenceEvent(bool raiseEvent, TreeView treeView, bool remapLineNumber)
        {            
            if (treeView.SelectedNode != null)
             if (treeView.SelectedNode.Tag is ICirFunction)
                 raiseSourceCodeReferenceEvent(raiseEvent, (ICirFunction)treeView.SelectedNode.Tag, remapLineNumber);
             else if (treeView.SelectedNode.Tag is ICirFunctionCall)
                 raiseSourceCodeReferenceEvent(raiseEvent, (ICirFunctionCall)treeView.SelectedNode.Tag, remapLineNumber);
        }

        public static void raiseSourceCodeReferenceEvent(bool raiseEvent, ICirFunctionCall functionCall, bool remapLineNumber)
        {
            if (raiseEvent)
                if (functionCall.cirFunction != null && functionCall.fileName != null && functionCall.lineNumber > -1)
                {

                    int mappedLineNumber = GetMappedLineNumber(functionCall.cirFunction.FunctionName, functionCall.fileName, functionCall.lineNumber, false, remapLineNumber);
                    O2Messages.fileOrFolderSelected(functionCall.fileName, mappedLineNumber);
                }
        }
        

        public static void raiseSourceCodeReferenceEvent(bool raiseEvent, ICirFunction cirFunction, bool remapLineNumber)
        {
            if (raiseEvent)
                if (cirFunction.File != null && cirFunction.FileLine != null)
                {
                    int mappedLineNumber = GetMappedLineNumber(cirFunction.FunctionName, cirFunction.File, cirFunction.FileLine, false, remapLineNumber);
                    O2Messages.fileOrFolderSelected(cirFunction.File, mappedLineNumber);
                }
        }

        public static void raiseSourceCodeReferenceEvent(bool raiseEvent, ICirClass cirClass, bool remapLineNumber)
        {
            if (raiseEvent)
                if (cirClass.File != null && cirClass.FileLine != null)
                {
                    int mappedLineNumber = GetMappedLineNumber(cirClass.Name, cirClass.File, cirClass.FileLine, true, remapLineNumber);
                    O2Messages.fileOrFolderSelected(cirClass.File, mappedLineNumber);
                }
        }


        /// <summary>
        /// hack to make the filenumber reference point to the function name (and not the first method of the function                    
        /// </summary>        
        /// <param name="textToFind"></param>
        /// <param name="fileName"></param>
        /// <param name="lineNumber"></param>
        /// <param name="ascendingSearch"></param>
        /// <param name="remapLineNumber"></param>
        /// <returns></returns>
        public static int GetMappedLineNumber(string textToFind, String fileName, int lineNumber, bool ascendingSearch, bool remapLineNumber)
        {
            try
            {
                if (!remapLineNumber || textToFind == "append" || textToFind == "toString")
                    return lineNumber;
                var mappedLineNumber = lineNumber > 0  ? lineNumber : 1;
                while (mappedLineNumber > 0  && mappedLineNumber < Files.getFileSize(fileName))
                {
                    var lineContents = Files.getLineFromSourceCode(fileName, (uint) mappedLineNumber);
                    if (lineContents.IndexOf(textToFind) == -1)
                        if (ascendingSearch)
                            mappedLineNumber++;
                        else
                            mappedLineNumber--;
                    else
                        return mappedLineNumber;        // if we have a match return mappedLineNumber
                }

                return lineNumber;                      // if we don't have a match return the original lineNumber
            }
            catch (Exception)
            {

                return 0;
            }
        }

        public static int GetMappedLineNumber(string textToFind, string fileName, string lineNumber, bool ascendingSearch, bool remapLineNumber)
        {
            try
            {
                int convertedLineNumber = Int32.Parse(lineNumber);
                if (remapLineNumber)
                    return GetMappedLineNumber(textToFind, fileName, convertedLineNumber, ascendingSearch, true);
                
                return convertedLineNumber;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public static string GetMappedLineText(string textToFind, string fileName, string lineNumber, bool ascendingSearch, bool remapLineNumber)
        {
            var remappedLineNumber = GetMappedLineNumber(textToFind, fileName, lineNumber, ascendingSearch, remapLineNumber);
            if (remappedLineNumber != 0)
                return Files.getLineFromSourceCode(fileName, (uint)remappedLineNumber);
            return "";
        }
    }
}