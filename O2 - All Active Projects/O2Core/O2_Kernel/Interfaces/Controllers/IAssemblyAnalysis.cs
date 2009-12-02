using System.Windows.Forms;
using O2.Kernel.CodeUtils;

namespace O2.Kernel.Interfaces.Controllers
{
    public interface IAssemblyAnalysis
    {
        event Callbacks.dMethod_String onMethodSelectedGetILCode;
        event Callbacks.dMethod_String onMethodSelectedGetSourceCode;

        bool canAssemblyBeLoaded(string assemblyToLoad);

        void processTreeNodeAndRaiseCallbacks(TreeNode node);
        void populateTreeNodeWithObjectChilds(TreeNode node);
        void populateTreeNodeWithObjectChilds(TreeNode node, object tag, bool populateFirstChildNodes);

        object loadAssemblyUsingMonoCecil(string assemblyToLoad);
        object loadAssemblyUsingReflection(string assemblyToLoad);
        //object loadAssemblyUsingCir(string assemblyToLoad);    
    }
}