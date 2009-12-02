using System;
using System.Collections.Generic;
using System.Windows.Forms;
using O2.Core.CIR.CirObjects;
using O2.Core.CIR.CirUtils;
using O2.DotNetWrappers.DotNet;
using O2.Kernel;
using O2.Kernel.CodeUtils;
using O2.Kernel.Interfaces.CIR;
using O2.Kernel.Interfaces.Messages;

namespace O2.Core.CIR.Ascx
{
    public partial class ascx_CirViewer_CirData : UserControl
    {
        public static ICirDataAnalysis cirDataAnalysis = new CirDataAnalysis();

        public ascx_CirViewer_CirData()
        {
            InitializeComponent();
        }

        private void ascx_CirViewer_O2CirData_Load(object sender, EventArgs e)
        {
            if (false == DesignMode)
            {
                PublicDI.o2MessageQueue.onMessages += o2MessageQueue_onMessages;

                afv_Classes.setViewName("Classes");
                afv_Classes.setCheckBoxesState(true);
                afv_Classes.bRemoveEmptyRootNodes = false;
                afv_Classes.setNamespaceDepth(2);
                afv_Classes.eNodeEvent_CheckClickEvent += event_ClassClickEvent;

                acv_Class.registerCallbackForNodeSignatureSelection(acv_Signature.showDataForSignature);
                afv_SuperClasses.setViewName("SuperClasses mappings");
                afv_SuperClasses.eNodeEvent_AfterSelect += event_IsSuperClasedByNodeSelectEvent;
                asvp_Panels.setVisibleControlsColapseState_4Panels_TopRight(scHost, scTop, scBottom, "Classes",
                                                                            "SupperClasses", "Functions", "Signatures");
                asvp_Panels.setCheckBox_Checked(3, true);
            }
        }

        

        public void LoadClassInfo()
        {
            if (this.okThread(delegate { LoadClassInfo(); }))
            {
                ICirDataSearchResult fcdSearchResult = CirDataAnalysisUtils.executeSearch(cirDataAnalysis);
                if (cbOnlyShowClassesWithFunctionsWithControlFlowGraphs.Checked)
                    afv_Classes.showSignatures(fcdSearchResult.lsResult_Classes_WithControlFlowGraphs);
                else
                    afv_Classes.showSignatures(fcdSearchResult.lsResult_Classes);
                if (cbCalculateSuperClasses.Checked)
                {
                    //afv_Classes.showSignatures(fcdSearchResult.lsResult_Classes);
                    afv_SuperClasses.iMaxItemsToShow = 10000;
                    afv_SuperClasses.showSignatures(cirDataAnalysis.lCirClass_bySuperClass);
                }
            }
            //  classNodeCheckClickEvent(new List<String>());       // this will clear all previous results;
        }

        public void event_ClassClickEvent(List<String> lsSelectedClasses)
        {
            acv_Class.loadDataForClasses(lsSelectedClasses);
            //loadDataForClasses
        }

        public void event_IsSuperClasedByNodeSelectEvent(String sSelectedClasses)
        {
            if (sSelectedClasses.IndexOf('_') > -1)
            {
                acv_Class.loadDataForClasses(new List<String>(new[] {sSelectedClasses.Replace('_', '.')}));
            }
        }

        

       

        private void cbCalculateSuperClasses_CheckedChanged(object sender, EventArgs e)
        {
            LoadClassInfo();
        }

        private void cbOnlyShowClassesWithFunctionsWithControlFlowGraphs_CheckedChanged(object sender, EventArgs e)
        {
            LoadClassInfo();
        }

        private void asvp_Panels_Load(object sender, EventArgs e)
        {
        }

        void o2MessageQueue_onMessages(IO2Message o2Message)
        {
            if (o2Message is IM_CirAction)
            {
                var cirAction = (IM_CirAction)o2Message;
                switch (cirAction.CirAction)
                {
                    case IM_CirActions.setCirDataAnalysis:
                        cirDataAnalysis = cirAction.CirDataAnalysis;
                        LoadClassInfo();                       
                        break;
                }
            }
        }
    }
}