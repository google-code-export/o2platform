// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using O2.Core.CIR.CirObjects;
using O2.Core.CIR.CirUtils;
using O2.Interfaces.CIR;
using O2.Interfaces.Messages;
using O2.Kernel;
using O2.Kernel.CodeUtils;

namespace O2.Core.CIR.Ascx.OldVersions
{
    public partial class ascx_CirViewer_Class : UserControl
    {
        public Callbacks.dMethod_String eNodeEvent_SignatureSelected;
        public ICirDataAnalysis fcdAnalysis;
        private CirDataSearchResult fcdSearchResult;
        public List<String> lsSelectedClasses;

        /// <summary> 
        /// Required designer variable.
        /// </summary>        
        public ascx_CirViewer_Class()
        {
            InitializeComponent();
        }


        public void setO2CirDataAnalysisObject(ICirDataAnalysis cirDataAalysis)
        {
            fcdAnalysis = cirDataAalysis;
        }

        void o2MessageQueue_onMessages(IO2Message o2Message)
        {
            if (o2Message is IM_CirAction)
            {
                var cirAction = (IM_CirAction)o2Message;
                switch (cirAction.CirAction)
                {
                    case IM_CirActions.setCirDataAnalysis:
                        setO2CirDataAnalysisObject(cirAction.CirDataAnalysis);
                        break;
                }
            }
        }

        private void ascx_CirViewer_Class_Load(object sender, EventArgs e)
        {
            if (false == DesignMode)
            {
                PublicDI.o2MessageQueue.onMessages += o2MessageQueue_onMessages;

                afv_Functions.setViewName("Functions");
                afv_Functions.setNamespaceDepth(-1);
                afv_Functions.eNodeEvent_AfterSelect += event_NodeAfterSelect_functions;

                afv_MakesCallsTo.setViewName("Makes Calls To (external)");
                afv_MakesCallsTo.setNamespaceDepth(2);
                afv_MakesCallsTo.eNodeEvent_AfterSelect += invokeEvent_SignatureSelected;

                afv_DontHaveRuleInDb.setViewName("Don't have rule In Db");
                afv_DontHaveRuleInDb.setNamespaceDepth(2);
                afv_DontHaveRuleInDb.eNodeEvent_AfterSelect += invokeEvent_SignatureSelected;

                afv_HaveRuleInDb.setViewName("Have rule in Db");
                afv_HaveRuleInDb.setNamespaceDepth(2);
                afv_HaveRuleInDb.eNodeEvent_AfterSelect += invokeEvent_SignatureSelected;

                asv_SelectVisiblePanels.setVisibleControlsColapseState_4Panels_TopRight(scHostControl, scTop, scBottom,
                                                                                        "Functions",
                                                                                        "Makes Calls To (external)",
                                                                                        "Rule in DB", "No Rule in DB");
                asv_SelectVisiblePanels.setCheckBox_Left(3, 300);
            }
        }

        public void loadDataForClasses(List<String> _selectedClasses)
        {
            lsSelectedClasses = _selectedClasses;
            fcdSearchResult = new CirDataSearchResult(fcdAnalysis);
            CirSearch.executeSearch(fcdSearchResult,lsSelectedClasses);
            afv_Functions.showSignatures(fcdSearchResult.lsResult_Functions);
            load_MakesCallsTo();
            afv_HaveRuleInDb.showSignatures(fcdSearchResult.lsResult_CallsMadeToExternalMethods_HaveDbMapping);
            afv_DontHaveRuleInDb.showSignatures(fcdSearchResult.lsResult_CallsMadeToExternalMethods_DontHaveDbMapping);
        }

        public void load_MakesCallsTo()
        {           
            if (cbCallsMade_OnlyShowExternal.Checked)
                afv_MakesCallsTo.showSignatures(fcdSearchResult.lsResult_CallsMadeToExternalMethods);
            else
                afv_MakesCallsTo.showSignatures(fcdSearchResult.lsResult_CallsMade);
        }

        public void event_NodeAfterSelect_functions(String sSignature)
        {
            //  if (fcdAnalysis.dCirFunction_bySignature.ContainsKey(FunctionSignature))
            //  {
            if (cbOnlyShowFunctionsCalledBySelectedFunction.Checked)
            {

             /*   var asd = fcdAnalysis.dCirFunction_bySignature[sSignature].FunctionsCalledUniqueList;
                var aa = fcdAnalysis.dCirFunction_bySignature.ContainsKey(asd[0].FunctionSignature);
                var aaa = fcdAnalysis.dCirFunction_bySignature[asd[0].FunctionSignature];*/

                if (fcdAnalysis.dCirFunction_bySignature.ContainsKey(sSignature))
                    afv_MakesCallsTo.showSignatures(ViewHelpers.getCirFunctionStringList(
                                                        fcdAnalysis.dCirFunction_bySignature[sSignature].FunctionsCalledUniqueList));
                else
                    afv_MakesCallsTo.showSignatures(new List<string>());
            }

            invokeEvent_SignatureSelected(sSignature);
            //  }
        }

        public void invokeEvent_SignatureSelected(String sSignature)
        {
            if (eNodeEvent_SignatureSelected != null)
            {
                DI.log.debug("Function signature selected, invoking event:{0}", sSignature);
                foreach (Delegate dDelegate in eNodeEvent_SignatureSelected.GetInvocationList())
                    dDelegate.DynamicInvoke(new[] {sSignature});
            }
        }

        public void registerCallbackForNodeSignatureSelection(Callbacks.dMethod_String dDelegateString)
        {
            eNodeEvent_SignatureSelected += dDelegateString;
        }

        private void cbCallsMade_OnlyShowExternal_CheckedChanged(object sender, EventArgs e)
        {
            loadDataForClasses(lsSelectedClasses);
        }

        private void cbOnlyShowFunctionsCalledBySelectedFunction_CheckedChanged(object sender, EventArgs e)
        {
            if (false == cbOnlyShowFunctionsCalledBySelectedFunction.Checked)
                load_MakesCallsTo();
        }
    }
}
