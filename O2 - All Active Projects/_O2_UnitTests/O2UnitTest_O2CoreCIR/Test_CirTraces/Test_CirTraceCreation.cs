using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NUnit.Framework;
using O2.Core.CIR.Ascx;
using O2.DotNetWrappers.DotNet;
using O2.External.WinFormsUI.Forms;
using O2.Kernel.Interfaces.Views;
using O2.Views.ASCX.O2Findings;

namespace O2.UnitTests.Test_O2CoreCIR.Test_CirTraces
{
    [TestFixture]
    public class Test_CirTraceCreation
    {
        private const string cirDataViewerControl = "ascx_CirDataViewer";
        private const string traceTreeViewControl = "ascx_TraceTreeView";
        private string targetAssembly = DI.config.ExecutingAssembly;

        [SetUp]
        public void openGui()
        {
            O2AscxGUI.launch();
            O2AscxGUI.openAscx(typeof(ascx_CirDataViewer), O2DockState.Document, cirDataViewerControl);
            O2AscxGUI.openAscx(typeof(ascx_TraceTreeView), O2DockState.Document, traceTreeViewControl);
                
        }

        [Test]
        public void test_createTraceFromCir()
        {
            var cirDataViewer = (ascx_CirDataViewer)O2AscxGUI.getAscx(cirDataViewerControl);
            cirDataViewer.loadFile(targetAssembly);    
            cirDataViewer.showLoadedFunctions();
            var functionsViewer = cirDataViewer.getFunctionsViewerControl();
            functionsViewer.NamespaceDepthValue = -1;
            var thread = functionsViewer.showView();
            thread.Join();
            var functionsViewerTreeView = functionsViewer.getObject_TreeView();
            Assert.That(functionsViewerTreeView.Nodes.Count > 0, "functionsViewerTreeView.Nodes.Count == 0");
            functionsViewerTreeView.invokeOnThread(
                () => functionsViewerTreeView.SelectedNode = functionsViewerTreeView.Nodes[0]);            
        }

        [TearDown]
        public void closeGui()
        {
            //O2AscxGUI.waitForAscxGuiClose();
            O2AscxGUI.close();
        }
    }
}
