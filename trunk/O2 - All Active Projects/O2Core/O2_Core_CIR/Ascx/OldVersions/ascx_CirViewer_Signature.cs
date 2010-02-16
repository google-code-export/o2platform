// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using O2.Core.CIR.Ascx.OldVersions;
using O2.Core.CIR.CirObjects;
using O2.Core.CIR.CirUtils;
using O2.DotNetWrappers.ExtensionMethods;
using O2.DotNetWrappers.Filters;
using O2.Interfaces.CIR;
using O2.Interfaces.Messages;
using O2.Kernel;

namespace O2.Core.CIR.Ascx
{
    public partial class ascx_CirViewer_Signature : UserControl
    {
        private ICirDataAnalysis cirDataAnalysis;

        public ascx_CirViewer_Signature()
        {
            InitializeComponent();
        }

        public void setO2CirDataAnalysisObject(ICirDataAnalysis _cirDataAnalysis)
        {
            cirDataAnalysis = _cirDataAnalysis;
            asv_CalledFunctions.setO2CirDataAnalysisObject(cirDataAnalysis);
            asv_IsCalledBy.setO2CirDataAnalysisObject(cirDataAnalysis);
            afv_Calls.setNamespaceDepth(-1);
            afv_IsCalledBy.setNamespaceDepth(-1);
        }

        public void showDataForSignature(String sSignatureToShow)
        {
            this.invokeOnThread(()=>showDataForSignature_thread(sSignatureToShow));
        }

        public void showDataForSignature_thread(String sSignatureToShow)
        {
            if (cirDataAnalysis == null)
                DI.log.error("in ascx_CirViewer_Signature.showDataForSignature , fcdAnalysis == null");
            else
            {
                lbClassesBeingViewed.Text = sSignatureToShow;
                if (false == cirDataAnalysis.dCirFunction_bySignature.ContainsKey(sSignatureToShow) ||
                    cirDataAnalysis.dCirFunction_bySignature.ContainsKey(sSignatureToShow) &&
                    cirDataAnalysis.dCirFunction_bySignature[sSignatureToShow].HasControlFlowGraph == false)
                {
                    DI.log.debug("{0} is not recognized as a full signature, trying to resolve it by name",
                                 sSignatureToShow);
                    foreach (CirFunction ccFunction in cirDataAnalysis.dCirFunction_bySignature.Values)
                    {
                        String sFunctionFullNameWithNoParamsAndReturnvalue =
                            new FilteredSignature(ccFunction.FunctionSignature).sFunctionFullName;
                        if (sFunctionFullNameWithNoParamsAndReturnvalue.IndexOf(sSignatureToShow) > -1)
                        {
                            DI.log.debug("Found a match:{0}", ccFunction.FunctionSignature);
                            sSignatureToShow = ccFunction.FunctionSignature;
                            break;
                        }
                    }
                }
                if (cirDataAnalysis.dCirFunction_bySignature.ContainsKey(sSignatureToShow))
                {
                    ICirFunction ccCirFunction = cirDataAnalysis.dCirFunction_bySignature[sSignatureToShow];                    
                    afv_Calls.showSignatures(ViewHelpers.getCirFunctionStringList(ccCirFunction.FunctionsCalledUniqueList));
                    afv_IsCalledBy.showSignatures(ViewHelpers.getCirFunctionStringList(ccCirFunction.FunctionIsCalledBy));

                    lbBoxSsaVariables.Items.Clear();
                    lBoxVariables.Items.Clear();

                    foreach (SsaVariable cfSSaVariable in ccCirFunction.dSsaVariables.Values)
                    {
                        String sVariableMapping = String.Format("{0} = {1}", cfSSaVariable.sBaseName,
                                                                cfSSaVariable.sPrintableType);
                        lbBoxSsaVariables.Items.Add(sVariableMapping);
                        //           DI.log.debug(sVariableMapping);
                    }
                    foreach (FunctionVariable fvVariable in ccCirFunction.dVariables.Values)
                    {
                        lBoxVariables.Items.Add(String.Format("[def:{0}] [ref:{1}] {2}", fvVariable.sSymbolDef,
                                                              fvVariable.refSymbol, fvVariable.sUniqueID));
                    }
                    ViewHelpers.showFunctionBlocksInWebBrower(((CirFunction)ccCirFunction).lcfgBasicBlocks,
                                                              wbControlFlowGraphsOFSelectedMethod);

                    asv_CalledFunctions.showDataForSignature(sSignatureToShow);
                    asv_IsCalledBy.showDataForSignature(sSignatureToShow);
                }
                else
                {
                    var lsEmpty = new List<string>();
                    afv_Calls.showSignatures(lsEmpty);
                    afv_IsCalledBy.showSignatures(lsEmpty);
                    lbBoxSsaVariables.Items.Clear();
                    lBoxVariables.Items.Clear();
                    asv_CalledFunctions.showDataForSignature(sSignatureToShow);
                    asv_IsCalledBy.showDataForSignature(sSignatureToShow);
                }
            }
            //    lBoxVariables.Items.Add(fcdO2CirData_ofThisFunction.getSymbol(sVariable));
        }

        private void ascx_CirViewer_Signature_Load(object sender, EventArgs e)
        {
            if (false == DesignMode)
            {
                PublicDI.o2MessageQueue.onMessages += o2MessageQueue_onMessages;

                asv_CalledFunctions.setSearchMode(ascx_SignatureViewer.SearchModes.CalledFunctions);
                asv_CalledFunctions.setViewMode(ascx_SignatureViewer.ViewModes.List);
                asv_IsCalledBy.setSearchMode(ascx_SignatureViewer.SearchModes.IsCalledBy);
                asv_CalledFunctions.setViewMode(ascx_SignatureViewer.ViewModes.List);

                asv_SelectVisiblePanels.setVisibleControlsColapseState_4Panels_TopRight(scHostControl, scTop, scBottom,
                                                                                        "Called Functions ",
                                                                                        "Called Functions (Traces)",
                                                                                        "Is called by",
                                                                                        "Is called by (Traces)");
            }
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
         
    }
}
