// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using O2.Cmd.SpringMvc.Objects;
using O2.Core.CIR.CirObjects;
using O2.Core.CIR.CirUtils;
using O2.DotNetWrappers.DotNet;
using O2.DotNetWrappers.Windows;
using O2.Kernel.Interfaces.CIR;

namespace O2.Cmd.SpringMvc.Classes
{
    public class SpringMvcUtils
    {

        public static TreeNode getTreeNodeWithAutoWiredObject(ICirData cirData, string targetFunction, SpringMvcParameter springMvcParameter, int parameterIndex)
        {
            try
            {

                if (cirData != null && cirData.dFunctions_bySignature.ContainsKey(targetFunction))
                {
                    var cirFunction = cirData.dFunctions_bySignature[targetFunction];
                    if (cirFunction.FunctionParameters.Count <= parameterIndex)
                    {
                        var filteredSignature = new O2.DotNetWrappers.Filters.FilteredSignature(targetFunction);
                        if (filteredSignature.lsParameters_Parsed.Count > parameterIndex)
                            springMvcParameter.className = filteredSignature.lsParameters_Parsed[parameterIndex];
                        else
                            DI.log.error("in getTreeNodeWithAutoWiredObject, requested parameter index not found in function: {0}", targetFunction);
                    }
                    else
                    {
                        var functionParameter = cirFunction.FunctionParameters[parameterIndex];
                        springMvcParameter.className = functionParameter.ParameterType.Replace("&", "");
                    }
                    if (springMvcParameter.className != "")
                    {

                        // Hack to handle int Java mappings 
                        if (springMvcParameter.className == "int")
                            springMvcParameter.className = "java.lang.Integer";
                        if (cirData.dClasses_bySignature.ContainsKey(springMvcParameter.className))
                        {
                            var childNodeText = string.Format("{0} - {1} - {2}", springMvcParameter.autoWiredMethodUsed, springMvcParameter.name, springMvcParameter.className);
                            return O2Forms.newTreeNode(childNodeText, childNodeText, 0, cirData.dClasses_bySignature[springMvcParameter.className]);
                        }

                        DI.log.error("in getTreeNodeWithAutoWiredObject, parameter type not found in cirData class list:{0}", springMvcParameter.className);
                    }
                }
                else
                    DI.log.error("in getTreeNodeWithAutoWiredObject, loaded cirData did not contained signature :{0}", targetFunction);
            }
            catch (Exception ex)
            {
                DI.log.error("in getTreeNodeWithAutoWiredObject:", ex.Message);
            }
            return new TreeNode();
        }

        public static TreeNode[] getNodes_WithFunctionsAttributes(Dictionary<SpringMvcController, TreeNode> treeNodesForloadedSpingMvcControllers)
        {
            var nodes = new List<TreeNode>();
            foreach (SpringMvcController springMcvController in treeNodesForloadedSpingMvcControllers.Keys)
                if (springMcvController.AutoWiredJavaObjects.Count > 0)
                    nodes.Add(O2Forms.cloneTreeNode(treeNodesForloadedSpingMvcControllers[springMcvController]));
            return nodes.ToArray();
        }

        public static TreeNode[] getNodes_WithoutFunctionsAttributes(Dictionary<SpringMvcController, TreeNode> treeNodesForloadedSpingMvcControllers)
        {
            var nodes = new List<TreeNode>();
            foreach (SpringMvcController springMcvController in treeNodesForloadedSpingMvcControllers.Keys)
                if (springMcvController.AutoWiredJavaObjects.Count == 0)
                    nodes.Add(O2Forms.cloneTreeNode(treeNodesForloadedSpingMvcControllers[springMcvController]));
            return nodes.ToArray();
        }

        public static TreeNode[] getNodes_UsingModelAttribute(Dictionary<SpringMvcController, TreeNode> treeNodesForloadedSpingMvcControllers)
        {
            var nodes = new List<TreeNode>();
            foreach (SpringMvcController springMcvController in treeNodesForloadedSpingMvcControllers.Keys)
                if (SpringMvcUtils.isMethodUsedInController(springMcvController, "ModelAttribute"))
                    nodes.Add(O2Forms.cloneTreeNode(treeNodesForloadedSpingMvcControllers[springMcvController]));
            return nodes.ToArray();
        }        

        public static bool doesControllerFunctionCallFunction(ICirData cirData, SpringMvcController springMcvController, List<string> functionsToSearch, bool recursiveFunctionSearch)
        {
            if (cirData == null)
                return false;
            if (cirData.dFunctions_bySignature.ContainsKey(springMcvController.JavaClassAndFunction))
            {
                var cirFunction = cirData.dFunctions_bySignature[springMcvController.JavaClassAndFunction];
                var targetFunctions = recursiveFunctionSearch ? CirSearch.calculateListOfAllFunctionsCalled(cirFunction) : cirFunction.FunctionsCalledUniqueList;
                DI.log.info("{0} called functions in {1}", targetFunctions.Count, cirFunction.FunctionSignature);
                foreach (var calledCirFunction in targetFunctions)
                {
                    if (functionsToSearch.Contains(calledCirFunction.FunctionSignature))
                        return true;
                }
            }
            else
                DI.log.error("could not find function: {0}", springMcvController.JavaClassAndFunction);
            return false;
        }

        public static bool isMethodUsedInController(SpringMvcController springMcvController, string methodToFind)
        {
            foreach (var springMvcParameter in springMcvController.AutoWiredJavaObjects)
                if (springMvcParameter.autoWiredMethodUsed == methodToFind)
                    return true;
            return false;
        }

        public static SpringMvcParameter getMethodUsedInController(SpringMvcController springMcvController, string methodToFind)
        {
            foreach (var springMvcParameter in springMcvController.AutoWiredJavaObjects)
                if (springMvcParameter.autoWiredMethodUsed == methodToFind)
                    return springMvcParameter;
            return null;
        }


        public void saveMappedControllers(List<SpringMvcController> loadedSpingMvcControllers, ICirData cirData)
        {
            saveMappedControllers(DI.config.getTempFileInTempDirectory(".MappedSpringMvcControllers"), loadedSpingMvcControllers, cirData);
        }

        public static void saveMappedControllers(string targetFolder, List<SpringMvcController> loadedSpingMvcControllers, ICirData cirData)
        {
            Files.checkIfDirectoryExistsAndCreateIfNot(targetFolder);
            if (Directory.Exists(targetFolder) == false)
            {
                DI.log.error("Could find or create target folder: {0}", targetFolder);
                return;
            }
            var targetFile = Path.Combine(targetFolder, Files.getTempFileName() + ".SpringMvcControllers");
            var springMvcMappings = new SpringMvcMappings { id = "test", controllers = loadedSpingMvcControllers };
            var targetSavedCirDataFile = Path.Combine(targetFolder, Files.getTempFileName() + ".CirData");
            CirDataUtils.saveSerializedO2CirDataObjectToFile(cirData, targetSavedCirDataFile);
            if (File.Exists(targetSavedCirDataFile) == false)
                DI.log.error("In saveMappedControllers, there was an error creating the CirData file");
            springMvcMappings.cirDataFile = Path.GetFileName(targetSavedCirDataFile);


            if (Serialize.createSerializedXmlFileFromObject(springMvcMappings, targetFile))
                DI.log.info("springMvcMappings saved to: {0}", targetFile);
            else
                DI.log.error("Could NOT save serialized springMvcMappings object to: {0}", targetFile);
        }
    }
}
