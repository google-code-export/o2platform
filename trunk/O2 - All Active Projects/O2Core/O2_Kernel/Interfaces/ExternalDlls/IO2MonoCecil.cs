using System.Collections.Generic;
using System.Windows.Forms;

namespace O2.Kernel.Interfaces.ExternalDlls
{
    public interface IO2MonoCecil
    {
        // Core MonoCeil
        object loadAssembly(string assemblyToLoad);
        bool canAssemblyBeLoaded(string assemblyToLoad);
        object getAssemblyEntryPoint(string p);
        //string getMethodDefinitionDeclaringTypeModuleName(object methodDefinition);
        //string getMethodDefinitionDeclaringTypeName(object methodDefinition);
        //string getMethodDefinitionName(object methodDefinition);

        // Using Cecil Decompiler
        string getIL(object methodDefinition);
        string getSourceCode(object methodDefinition);
        string getILfromClonedMethod(object targetMethod);

        // for Window Forms/ASCX
        List<TreeNode> populateTreeNodeWithObjectChilds(TreeNode node, object tag, bool populateFirstChildNodes);
    }
}