using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using O2.DotNetWrappers.DotNet;
using O2.DotNetWrappers.Filters;
using O2.DotNetWrappers.Windows;
using O2.Kernel;
using O2.Kernel.Interfaces.Views;
using O2.Views.ASCX.DataViewers;

namespace O2.Views.ASCX.CoreControls
{
    public partial class ascx_O2ObjectModel
    {
        public List<Assembly> assembliesLoaded = new List<Assembly>();
        public List<MethodInfo> methods = new List<MethodInfo>();
        public List<FilteredSignature> filteredSignatures = new List<FilteredSignature>();

        public bool runOnLoad = true;

        public void onLoad()
        {
            if (DesignMode == false && runOnLoad)
            {                
                assembliesLoaded = getDefaultLoadedAssemblies();
                filteredFunctionsViewer.setNamespaceDepth(0);                
                runOnLoad = false;
                refreshViews();                
            }
        }

        private static List<Assembly> getDefaultLoadedAssemblies()
        {
            var assembliesPaths = CompileEngine.getListOfO2AssembliesInExecutionDir();
            
            var assemblies = new List<Assembly>();
            foreach (var assemblyPath in assembliesPaths)
            {
             //   PublicDI.log.error("Loading file: {0}", assemblyPath);
                var assembly = PublicDI.reflection.getAssembly(assemblyPath);
                //PublicDI.reflection.getAssembliesInCurrentAppDomain()
                if (assembly != null)
                    assemblies.Add(assembly);
                else
                {
                    PublicDI.log.debug("could not load assembly: {0}", assemblyPath);
                }
            }            
            return assemblies;
        }

        public void refreshViews()
        {
            PublicDI.log.info("Refreshing Views");
            refreshLoadedAssembliesView();
            refreshO2ObjectModelView(cbHideCSharpGeneratedMethods.Checked);
            showFilteredMethods(null);
        }

        public void refreshLoadedAssembliesView()
        {
            tvAssembliesLoaded.invokeOnThread(
                () =>
                    {
                        tvAssembliesLoaded.Nodes.Clear();
                        //var o2FormsReflectionASCX = new O2FormsReflectionASCX();
                        foreach (var assembly in assembliesLoaded)                            
                            O2Forms.newTreeNode(tvAssembliesLoaded.Nodes, assembly.GetName().Name, 0, assembly,false);
                        //foreach (TreeNode treeNode in tvAssembliesLoaded.Nodes)
                        //    o2FormsReflectionASCX.populateTreeNodeWithObjectChilds(treeNode, treeNode.Tag, true);
                    });    
        }

        public void refreshO2ObjectModelView(bool hideCSharpGeneratedMethods)
        {
            PublicDI.log.debug("from loaded assemblies, calulating list of (reflection) method information");
            methods = new List<MethodInfo>();
            foreach (var assembly in assembliesLoaded)            
                methods.AddRange(PublicDI.reflection.getMethods(assembly).ToArray());
            PublicDI.log.debug("Convering method information into filtered signatures objects");
            filteredSignatures = getMethodFilteredSignatures(methods, hideCSharpGeneratedMethods);
            PublicDI.log.info("there are {0} O2 assemblies", assembliesLoaded.Count);
            PublicDI.log.info("there are {0} methods", methods.Count);
            var methodsSignature = getMethodSignatures(filteredSignatures, hideCSharpGeneratedMethods);
            
            PublicDI.log.info("there are {0} methods sigantures", methodsSignature.Count);

            functionsViewer.showSignatures(methodsSignature);
            //var functionsViewer = (ascx_FunctionsViewer)O2AscxGUI.openAscx(typeof(ascx_FunctionsViewer), O2DockState.Float, "O2 Object Model");
            //functionsViewer.showSignatures(methodsSignature);
            //return true;
        }

        public static List<FilteredSignature> getMethodFilteredSignatures(List<MethodInfo> methods, bool hideCSharpGeneratedMethods)
        {
            var filteredSignatures = new List<FilteredSignature>();
            foreach (var method in methods)
            {
                var filteredSignature = new FilteredSignature(method);
                if (hideCSharpGeneratedMethods == false || filteredSignature.sSignature.IndexOf("<>") == -1)
                    filteredSignatures.Add(filteredSignature);
                else
                {
                    PublicDI.log.info("Skipping: {0}", method.Name);
                }
            }
            return filteredSignatures;
        }

        public static List<String> getMethodSignatures(List<FilteredSignature> filteredSignatures, bool hideCSharpGeneratedMethods)
        {
            var methodsSignature = new List<String>();
            foreach (var filteredSignature in filteredSignatures)
                    methodsSignature.Add(filteredSignature.sSignature);                
            return methodsSignature;
        }

        public void showFilteredMethods(KeyEventArgs e)
        {
            if (e == null || e.KeyData == Keys.Enter)
                O2Thread.mtaThread(
                    () =>
                    showFilteredMethods(tbFilterBy_MethodType.Text, tbFilterBy_MethodName.Text,
                                        tbFilterBy_ParameterType.Text,
                                        tbFilterBy_ReturnType.Text, filteredSignatures, filteredFunctionsViewer)
                    );
        }

        // DC: need to find a more optimized way to do this
        public static void showFilteredMethods(string methodType, string methodName, string parameterType, string returnType, List<FilteredSignature> filteredSignatures, ascx_FunctionsViewer functionsViewer)
        {
            var typesFilter = new List<FilteredSignature>();
            var methodsFilter = new List<FilteredSignature>();
            var parametersFilter = new List<FilteredSignature>();
            var returnTypeFilter = new List<FilteredSignature>();

            // methodType
            if (methodType == "")
                typesFilter = filteredSignatures;
            else
                foreach (var filteredSignature in filteredSignatures)
                {
                    if (filteredSignature.sFunctionClass.Contains(methodType))
                        typesFilter.Add(filteredSignature);
                }

            // methodName
            if (methodName == "")
                methodsFilter = typesFilter;
            else
                foreach (var filteredSignature in typesFilter)
                {
                    if (filteredSignature.sFunctionName.Contains(methodName))
                        methodsFilter.Add(filteredSignature);
                }

            // parameterType
            if (parameterType == "")
                parametersFilter = methodsFilter;
            else
                foreach (var filteredSignature in methodsFilter)
                {
                    if (filteredSignature.sParameters.Contains(parameterType))
                        parametersFilter.Add(filteredSignature);
                }

            if (returnType == "")
                returnTypeFilter = parametersFilter;
            else
                foreach (var filteredSignature in parametersFilter)
                {
                    if (filteredSignature.sParameters.Contains(returnType))
                        returnTypeFilter.Add(filteredSignature);
                }
            // get list of signatures to show using the last filter result (returnTypeFilter)
            var signaturesToShow = new List<string>();
            foreach (var filteredSignature in returnTypeFilter)
                signaturesToShow.Add(filteredSignature.sSignature);
            functionsViewer.showSignatures(signaturesToShow);
        }
    }
}
