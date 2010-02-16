// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using O2.Core.CIR.CirObjects;
using O2.Core.CIR.CirUtils;
using O2.Interfaces.CIR;

namespace O2.Core.CIR.Ascx.OldVersions
{
    public partial class ascx_SignatureViewer : UserControl
    {
        #region SearchModes enum

        public enum SearchModes
        {
            CalledFunctions,
            IsCalledBy
        }

        #endregion

        #region ViewModes enum

        public enum ViewModes
        {
            List,
            Traces
        }

        #endregion

        private ICirDataAnalysis cirDataAnalysis;

        /// <summary> 
        /// Required designer variable.
        /// </summary>        
        public ascx_SignatureViewer()
        {
            InitializeComponent();
        }

        public void setO2CirDataAnalysisObject(ICirDataAnalysis _cirDataAnalysis)
        {
            cirDataAnalysis = _cirDataAnalysis;
        }

        private void ascx_SignatureViewer_Load(object sender, EventArgs e)
        {
            if (false == DesignMode)
            {
                afv_Functions.setViewName("Called Functions");
                afv_Functions.setNamespaceDepth(-1);
                cbSearchMode.Items.Add(SearchModes.CalledFunctions);
                cbSearchMode.Items.Add(SearchModes.IsCalledBy);
                cbSearchMode.SelectedItem = SearchModes.CalledFunctions;

                cbViewMode.Items.Add(ViewModes.List);
                cbViewMode.Items.Add(ViewModes.Traces);
                cbViewMode.SelectedItem = ViewModes.List;
                setupSelectedViewMode();
            }
        }

        public void setViewMode(ViewModes vmViewMode)
        {
            cbViewMode.SelectedItem = vmViewMode;
            setupSelectedViewMode();
        }

        public void setSearchMode(SearchModes smSearchMode)
        {
            cbSearchMode.SelectedItem = smSearchMode;
        }

        public void showDataForSignature(String sSignature)
        {
            // check if this function is on the current list (if not this is an edge)
            if (false == cirDataAnalysis.dCirFunction_bySignature.ContainsKey(sSignature))
                // this case do a hack by adding this edge as a function
            {
                var lsFunctionsThatCallSignature = new List<ICirFunctionCall>();
                // first find everybody that calls into it
                foreach (ICirFunction ccFunction in cirDataAnalysis.dCirFunction_bySignature.Values)
                    if (cirDataAnalysis.dCirFunction_bySignature.ContainsKey(sSignature))
                        if (ccFunction.FunctionsCalledUniqueList.Contains( cirDataAnalysis.dCirFunction_bySignature[sSignature]))
                            lsFunctionsThatCallSignature.Add(new CirFunctionCall(ccFunction));

                var ccNewCirFunction = new CirFunction
                                           {
                                               FunctionSignature = sSignature,
                                               FunctionIsCalledBy = lsFunctionsThatCallSignature
                                           };
                cirDataAnalysis.dCirFunction_bySignature.Add(sSignature, ccNewCirFunction);
            }
            var tvTraces = new TreeView();
            var lFunctionsCalled = new List<string>();
            const string sFilter_Signature = "";
            const string sFilter_Parameter = "";
            if (cbSearchMode.SelectedItem.ToString() == SearchModes.CalledFunctions.ToString())
                TraceAnalysis.calculateAllTracesFromFunction(sSignature, tvTraces.Nodes, lFunctionsCalled,
                                                             sFilter_Signature, sFilter_Parameter, false,
                                                             cirDataAnalysis);
            else
                TraceAnalysis.calculateAllTracesFromFunction(sSignature, tvTraces.Nodes, lFunctionsCalled,
                                                             sFilter_Signature, sFilter_Parameter, true, cirDataAnalysis);
            if (cbViewMode.SelectedItem.ToString() == ViewModes.List.ToString())
                afv_Functions.showSignatures(lFunctionsCalled);
            else
            {
                replaceTreeView(this, ref tvTreeView, tvTraces);
                if (cbSearchMode.SelectedItem.ToString() == SearchModes.IsCalledBy.ToString())
                    tvTreeView.ExpandAll();
            }
        }


        public void replaceTreeView(Control cHostControl, ref TreeView tvTarget, TreeView tvNew)
        {
            tvNew.Width = tvTarget.Width;
            tvNew.Height = tvTarget.Height;
            tvNew.Top = tvTarget.Top;
            tvNew.Left = tvTarget.Left;
            tvNew.Anchor = tvTarget.Anchor;
            cHostControl.Controls.Remove(tvTarget);
            cHostControl.Controls.Add(tvNew);
            tvTarget = tvNew;
        }


        private void cbViewMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            setupSelectedViewMode();
        }

        private void setupSelectedViewMode()
        {
            if (cbViewMode.SelectedItem.ToString() == ViewModes.List.ToString())
            {
                tvTreeView.Visible = false;
                afv_Functions.Visible = true;
            }
            else
            {
                tvTreeView.Visible = true;
                afv_Functions.Visible = false;
            }
        }
    }
}
