using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using O2.DotNetWrappers.DotNet;
using O2.DotNetWrappers.O2Misc;
using O2.DotNetWrappers.Windows;
using O2.External.SharpDevelop;
using O2.ImportExport.OunceLabs.Ozasmt_OunceV6;
using O2.Kernel.Interfaces.O2Findings;
using O2.Legacy.OunceV6.SavedAssessmentFile.classes;

namespace O2.Tool.ViewAssessmentRun.Ascx
{
    public partial class ascx_ViewAssessmentRun : UserControl
    {
        //String sPathToLoadedAssessmentFile = "";
        private bool bExpandingFindingsTreeview;

        private Analysis.FindingFilter ffSelectedFindingFilter = Analysis.FindingFilter.SmartTraces;
        private List<string> lsSourceCode = new List<string>();

        public O2AssessmentData_OunceV6 oadAssessmentData;
        private String sPathTempFolder;
        private Analysis.SmartTraceFilter stfSmartTraceFilter = Analysis.SmartTraceFilter.MethodName;
        public String sVarToHoldObject = "viewAssessmentRun_Object";
        public String sVarWithCustomScripts = "userCustomScript";

        public ascx_ViewAssessmentRun()
        {
            InitializeComponent();
        }

        public ascx_ViewAssessmentRun(String sXmlAssessmentFileToLoad)
        {
            InitializeComponent();
            if (File.Exists(sXmlAssessmentFileToLoad))
                loadAssessmentRunXmlFile(sXmlAssessmentFileToLoad);
        }

        private void ascx_ViewAssessmentRun_Load(object sender, EventArgs e)
        {
            if (DesignMode == false)
            {
                dndDropArea.eStartExecution_Event += dndDropArea_eStartExecution_Event;
                sPathTempFolder = DI.config.O2TempDir;
                //   idfInvokeDynamicFilters.configureTargets(sVarToHoldObject, sVarWithCustomScripts, this);
                //   idfInvokeDynamicFilters.lbInvokeSource = lbFilter;
                dndDropArea.setText("Drop Saved Assessment file here");
                updateVisibleControlsColapseState();
            }
        }

        private void dndDropArea_eStartExecution_Event(object oVar)
        {
             DI.log.debug("data received of type: {0}", oVar.GetType().Name);
            if (oVar.GetType().Name == "DnDActionObjectData")
            {
                var fdndAction = (Dnd.DnDActionObjectData) oVar;
                switch (fdndAction.oPayload.GetType().Name)
                {
                        /*           case "AssessmentRun":
                        PrexisInterop.AssessmentRun arAssessmentRun = (PrexisInterop.AssessmentRun)fdndAction.oPayload;
                        processAssessmentRunReceived(arAssessmentRun);
                        break;
           */
                    case "String":
                        var sString = (String) fdndAction.oPayload;
                        if (File.Exists(sString))
                            loadAssessmentRunXmlFile(sString);
                        break;
                }
            }
        }

        /*  //DC removed due to CORE dlls dependency 
        public void processAssessmentRunReceived(AssessmentRun arAssessmentRun)
        {
            sPathToLoadedAssessmentFile = Path.Combine(sPathTempFolder, Utils.getFileSaveDateTime_Now() + ".xml");
            arAssessmentRun.export(sPathToLoadedAssessmentFile);
            loadAssessmentRunXmlFile(sPathToLoadedAssessmentFile);
            

            //Analysis.populateDictionariesWithXrefsToLoadedAssessment(
            //    ffSelectedFindingFilter,
            //    cbBrowseLoadedAssessmentFile_DropDuplicateSmartTraces.Checked, 
            //    cbBrowseLoadedAssessmentFile_IgnoreRootCallInvocation.Checked); 
        }*/

        public void loadAssessmentRunXmlFile(String sPathToLoadedAssessmentFile)
        {
            cleanLoadedFileStats();
            TimeSpan tsLoadTime = Analysis.loadAssessmentFile(sPathToLoadedAssessmentFile, true, ref oadAssessmentData);
            vars.set_(sVarToHoldObject, oadAssessmentData);
            displayLoadedFileStats(tsLoadTime);
            O2Forms.setObjectTextValueThreadSafe("Vulnerability Type", lbFilter);
            reloadTreeView();
        }

        private void cleanLoadedFileStats()
        {
            O2Forms.setObjectTextValueThreadSafe("", lbNumberOfAssessmentFiles);
            O2Forms.setObjectTextValueThreadSafe("", lbNumberOfFindings);
            O2Forms.setObjectTextValueThreadSafe("", lbNumberOfSmartTraces);
            O2Forms.setObjectTextValueThreadSafe("", lbNumberOfLostSinks);
            O2Forms.setObjectTextValueThreadSafe("", lbAssessmentFileLoaded);

            /*lbNumberOfAssessmentFiles.Text = "";
            lbNumberOfFindings.Text = "";
            lbNumberOfSmartTraces.Text = "";
            lbNumberOfLostSinks.Text = "";
            lbAssessmentFileLoaded.Text = "";*/
        }

        public void displayLoadedFileStats(TimeSpan tsLoadTime)
        {
            int iFindings = 0,
                iAssessmentFiles = 0,
                iSmartTraces = 0,
                iLostSinks = 0,
                iSmartTraces_NotDuplicate = 0,
                iSmartTraces_NotDuplicate_IgnoreRoot = 0,
                iLostSinks_NotDuplicate = 0,
                iLostSinks_NotDuplicate_IgnoreRoot = 0;

            DateTime dtStartTime = DateTime.Now;

            Analysis.calculateFindingsStatistics(oadAssessmentData.arAssessmentRun,
                                                 ref iFindings, ref iAssessmentFiles,
                                                 ref iSmartTraces, ref iLostSinks,
                                                 ref iSmartTraces_NotDuplicate, ref iSmartTraces_NotDuplicate_IgnoreRoot,
                                                 ref iLostSinks_NotDuplicate, ref iLostSinks_NotDuplicate_IgnoreRoot);

            DateTime dtEndTime = DateTime.Now;
            TimeSpan tsStatisticsTime = dtEndTime - dtStartTime;

             DI.log.info(
                "Loaded file Stats: {0} Assesment files, {1} Findings, {2} Smart traces (DD={3} - IR={4}) , {5} Lost Sinks (ND={6} - IR={7})",
                iAssessmentFiles.ToString(), iFindings.ToString()
                , iSmartTraces.ToString(), iSmartTraces_NotDuplicate.ToString(),
                iSmartTraces_NotDuplicate_IgnoreRoot.ToString()
                , iLostSinks.ToString(), iLostSinks_NotDuplicate.ToString(),
                iLostSinks_NotDuplicate_IgnoreRoot.ToString());

            O2Forms.setObjectTextValueThreadSafe(iAssessmentFiles.ToString(), lbNumberOfAssessmentFiles);
            O2Forms.setObjectTextValueThreadSafe(iFindings.ToString(), lbNumberOfFindings);
            O2Forms.setObjectTextValueThreadSafe(
                iSmartTraces + "     (DR = " + iSmartTraces_NotDuplicate + " , IR = " +
                iSmartTraces_NotDuplicate_IgnoreRoot + ")", lbNumberOfSmartTraces);
            O2Forms.setObjectTextValueThreadSafe(
                iLostSinks + "     (DR = " + iLostSinks_NotDuplicate + " , IR = " + iLostSinks_NotDuplicate_IgnoreRoot +
                ")", lbNumberOfLostSinks);
            O2Forms.setObjectTextValueThreadSafe(Analysis.sFileLoaded, lbAssessmentFileLoaded);
            //lbNumberOfAssessmentFiles.Text = iAssessmentFiles.ToString();
            //lbNumberOfFindings.Text = iFindings.ToString();
            //lbNumberOfSmartTraces.Text = iSmartTraces.ToString() + "     (DR = " + iSmartTraces_NotDuplicate.ToString() + " , IR = " + iSmartTraces_NotDuplicate_IgnoreRoot + ")";
            //lbNumberOfLostSinks.Text = iLostSinks.ToString() + "     (DR = " + iLostSinks_NotDuplicate.ToString() + " , IR = " + iLostSinks_NotDuplicate_IgnoreRoot + ")";
            //lbAssessmentFileLoaded.Text = Analysis.sFileLoaded;
            if (tsLoadTime.ToString() != "00:00:00")
                O2Forms.setObjectTextValueThreadSafe(
                    String.Format("{0}.{1}  |  {2}.{3}  (file load | assessment stats)", tsLoadTime.Seconds,
                                  tsLoadTime.Milliseconds, tsStatisticsTime.Seconds, tsStatisticsTime.Milliseconds),
                    lbLoadTime);
        }

        public void showFilter_CustomScript()
        {
            try
            {
                vars.set_("FindingsTreeView", tvFindingsList);
                vars.set_("SmartTraceTreeView", tvSmartTrace);
                 DI.log.debug("in showFilter_CustomScript");

                var tCustomScrypt = (Type) vars.get(sVarWithCustomScripts);
                var sResult =
                    (String)
                    tCustomScrypt.InvokeMember("ping",
                                               BindingFlags.Public | BindingFlags.Static | BindingFlags.InvokeMethod,
                                               null, null, new object[] {});
                 DI.log.debug("userCustomScript.ping  = {0}", sResult);

                sResult =
                    (String)
                    DI.reflection.invokeMethod_Static(tCustomScrypt, "filterTreeView",
                                                   new object[] {tvFindingsList, oadAssessmentData});
                 DI.log.debug("userCustomScript.filterTreeView  = {0}", sResult);

/*                sResult = (String)tCustomScrypt.InvokeMember(
                    "filterTreeView", 
                    System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.InvokeMethod, null, null, new object[] { tvFindingsList });
 * */

                //String sResult = (String)reflection.invokeMethod(tCustomScrypt, "ping", new object[] { });                

                // DI.log.debug("back to showFilter_CustomScript");
            }
            catch (Exception ex)
            {
                 DI.log.error("in showFilter_CustomScript: {0}", ex.Message);
            }
        }

        public void showFilter_byFiles()
        {
            tvFindingsList.Nodes.Clear();
            //List<String> lsAssessmentFiles = Analysis.getListOfAssessmentFiles();
            foreach (AssessmentAssessmentFile afAssessmentFile in oadAssessmentData.dAssessmentFiles.Keys)
            {
                String sNodeName = Path.GetFileName(afAssessmentFile.filename) + " (" +
                                   oadAssessmentData.dAssessmentFiles[afAssessmentFile].Count + ")";
                var tnNewNode = new TreeNode(sNodeName);
                tnNewNode.Tag = afAssessmentFile;
                tnNewNode.ForeColor = Color.DarkBlue;
                tvFindingsList.Nodes.Add(tnNewNode);
            }
            tvFindingsList.Sort();
        }

        public void showFilter_bySelectedFilter()
        {
            tvFindingsList.Nodes.Clear();

            //foreach (AssessmentAssessmentFileFinding fFinding in fadF1AssessmentData.dFindings.Keys)
            foreach (AssessmentAssessmentFileFinding fFinding in oadAssessmentData.lfAllFindingsThatMatchCriteria)
            {
                String sNodeText = getNodeTextBasedOnSelectedFilter(cbActionObject_NameFilter.Text, fFinding);
                if (sNodeText != null)
                {
                    sNodeText = sNodeText.Trim();
                    if (sNodeText != "")
                    {
                        if (rbFindingsFilter_AllFindings.Checked ||
                            (rbFindingsFilter_AllFindings.Checked == false && fFinding.Trace != null))
                        {
                            var tnNewNode = new TreeNode(sNodeText);
                            tnNewNode.Tag = fFinding;
                            tnNewNode.Name = sNodeText;
                            if (fFinding.Trace != null) // if there is a trace make the node text gree
                                tnNewNode.ForeColor = Color.DarkGreen;

                            TreeNode tnParentNode = tvFindingsList.Nodes[sNodeText];
                            if (tnParentNode == null)
                            {
                                tvFindingsList.Nodes.Add(tnNewNode);
                                if (tvFindingsList.Nodes.Count > 3000)
                                {
                                     DI.log.debug("Too many nodes to show in tvFindingsList:{0}. Aborting view",
                                                    tvFindingsList.Nodes.Count);
                                    return;
                                }
                            }
                            else
                            {
                                tnParentNode.Nodes.Add(tnNewNode);
                                if (tnParentNode.Nodes.Count > 3000)
                                {
                                     DI.log.debug("Too many nodes to show in tnParentNode:{0}. Aborting view",
                                                    tnParentNode.Nodes.Count);
                                    return;
                                }
                            }
                        }
                    }
                }
            }
        }

        public void showFilter_byVulnerabilityType()
        {
            // populate VulnerabilityType treeView
            O2Forms.executeMethodThreadSafe(tvFindingsList, tvFindingsList.Nodes, "Clear", new object[] {});
            //tvFindingsList.Nodes.Clear();
            List<String> lsVulnerabilityType = OzasmtUtils_OunceV6.getListOfVulnerabilityTypes(oadAssessmentData);
            foreach (String sVulnerabilityType in lsVulnerabilityType)
            {
                TreeNode tnNewNode;
                if (sVulnerabilityType.IndexOf("Vulnerability.") > -1) //  normal case
                {
                    tnNewNode = new TreeNode(Path.GetFileName(sVulnerabilityType.Replace("Vulnerability.", "")));
                    tnNewNode.Tag = sVulnerabilityType.Substring(0, sVulnerabilityType.IndexOf('(')).Trim();
                }
                else // case where the vuln type has been changed (for example using the f1 custom filters
                {
                    tnNewNode = new TreeNode(sVulnerabilityType);
                    if (oadAssessmentData.dVulnerabilityType[sVulnerabilityType].Count == 1)
                        // if there is only one, then make it the default on the treenode
                        tnNewNode.Tag = oadAssessmentData.dVulnerabilityType[sVulnerabilityType][0];
                    else
                        tnNewNode.Tag = sVulnerabilityType;
                }
                tnNewNode.ForeColor = Color.DarkBlue;
                O2Forms.executeMethodThreadSafe(tvFindingsList, tvFindingsList.Nodes, "Add", new object[] {tnNewNode});
                //tvFindingsList.Nodes.Add(tnNewNode);
            }
            O2Forms.executeMethodThreadSafe(tvFindingsList, tvFindingsList, "Sort", new object[] {});
            //tvFindingsList.Sort();
        }

        public void showFilter_byActionObject()
        {
             DI.log.error("Not implemented in this version");

            /*
            // populate ActionObject treeView
            tvFindingsList.Nodes.Clear();
            int iNumberOfItemsInActionObjectNodes = 0;
            List<String> lsActionObjects = Analysis.getListWithUsedActionObjects(oadAssessmentData);
            //List<String> lsVulnerabilityType = Analysis.getListOfVulnerabilityTypes();
            foreach (String sActionObjectId in lsActionObjects)
            {
                if (oadAssessmentData.dActionObjects.ContainsKey(UInt32.Parse(sActionObjectId)))
                {
                    String sNodeName = Lddb.getActionObjectName(sActionObjectId);
                    if (sNodeName == "")
                         DI.log.error("Could not resolve (on database) the ActionId # {0}", sActionObjectId);
                    else
                    {
                        iNumberOfItemsInActionObjectNodes += oadAssessmentData.dActionObjects[UInt32.Parse(sActionObjectId)].Count;
                        sNodeName += " (" + oadAssessmentData.dActionObjects[UInt32.Parse(sActionObjectId)].Count.ToString() + ")";
                        TreeNode tnNewNode = new TreeNode(sNodeName);
                        tnNewNode.Tag = UInt32.Parse(sActionObjectId);
                        tnNewNode.ForeColor = System.Drawing.Color.DarkBlue;
                        tvFindingsList.Nodes.Add(tnNewNode);
                    }
                }
            }
            tvFindingsList.Sort();
            lbBrowseAssessment_NumberOfItemsLoadedIn_ByActionObject.Text = iNumberOfItemsInActionObjectNodes.ToString();
             * */
        }

        private void btFilterByActionObject_Click(object sender, EventArgs e)
        {
            setActionObjectFilterObjectsVisibility(true);
            showFilter_byActionObject();
            lbFilter.Text = "Action Object";
        }

        private void btViewByFiles_Click(object sender, EventArgs e)
        {
            setActionObjectFilterObjectsVisibility(false);
            showFilter_byFiles();
            lbFilter.Text = "Files";
        }

        private void btViewByVulnerabilityType_Click(object sender, EventArgs e)
        {
            setActionObjectFilterObjectsVisibility(false);
            showFilter_byVulnerabilityType();
            lbFilter.Text = "Vulnerability Type";
        }

        private void btFilterByCustomScript_Click(object sender, EventArgs e)
        {
            setActionObjectFilterObjectsVisibility(false);
            showFilter_CustomScript();
            lbFilter.Text = "Custom Script";
        }

        private void setActionObjectFilterObjectsVisibility(bool bVisibility)
        {
            lbActionObjectFilter.Visible = bVisibility;
            cbActionObject_NameFilter.Visible = bVisibility;
            lbBrowseAssessment_NumberOfItemsLoadedIn_ByActionObject.Visible = bVisibility;
            lnActionObjectNodes.Visible = bVisibility;
        }

        public void reloadTreeView()
        {
            Analysis.populateDictionariesWithXrefsToLoadedAssessment(ffSelectedFindingFilter,
                                                                     cbBrowseLoadedAssessmentFile_DropDuplicateSmartTraces
                                                                         .Checked,
                                                                     cbBrowseLoadedAssessmentFile_IgnoreRootCallInvocation
                                                                         .Checked, oadAssessmentData);
            // recalculate this data
            switch (lbFilter.Text)
            {
                case "Files":
                    showFilter_byFiles();
                    break;
                case "Action Object":
                    showFilter_byActionObject();
                    break;
                case "Vulnerability Type":
                default:
                    // if not specified default to "Vulnerability Type"  (filter generated content will fall into this category
                    showFilter_byVulnerabilityType();
                    break;
            }
        }

        private void rbFindingsFilter_AllFindings_CheckedChanged(object sender, EventArgs e)
        {
            if (rbFindingsFilter_AllFindings.Checked)
            {
                ffSelectedFindingFilter = Analysis.FindingFilter.AllFindings;
                reloadTreeView();
            }
        }

        private void rbFindingsFilter_SmartTraces_CheckedChanged(object sender, EventArgs e)
        {
            if (rbFindingsFilter_SmartTraces.Checked)
            {
                ffSelectedFindingFilter = Analysis.FindingFilter.SmartTraces;
                reloadTreeView();
            }
        }

        private void rbFindingsFilter_SmartTraces_Lost_Sinks_CheckedChanged(object sender, EventArgs e)
        {
            if (rbFindingsFilter_SmartTraces_Lost_Sinks.Checked)
            {
                ffSelectedFindingFilter = Analysis.FindingFilter.SmartTraces_LostSink;
                reloadTreeView();
            }
        }

        private void rbFindingsFilter_SmartTraces_Unique_Lost_Sinks_CheckedChanged(object sender, EventArgs e)
        {
            if (rbFindingsFilter_SmartTraces_Unique_Lost_Sinks.Checked)
            {
                ffSelectedFindingFilter = Analysis.FindingFilter.SmartTraces_LostSink_Unique;
                reloadTreeView();
            }
        }

        private void rbFindingsFilter_NoTraces_CheckedChanged(object sender, EventArgs e)
        {
            if (rbFindingsFilter_NoTraces.Checked)
            {
                ffSelectedFindingFilter = Analysis.FindingFilter.NoSmartTraces;
                reloadTreeView();
            }
        }

        private void cbActionObject_NameFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (TreeNode tnTreeNode in tvFindingsList.Nodes)
                tnTreeNode.Nodes.Clear();
            tvFindingsList.SelectedNode = null;
            showFilter_bySelectedFilter();
            //btFilterByActionObject_Click(null, null);         
        }

        private void tvFindingsList_AfterSelect(object sender, TreeViewEventArgs e)
        {
            // clean previous data            
            tvSmartTrace.Nodes.Clear();
            aGLEE.loadGraph();
            dgvFindingData.Rows.Clear();
            switch (lbFilter.Text) // then process the f1 default filters (which in time should all be moved to scripts
            {
                case "Filter":
                    if (e.Node.Tag != null && e.Node.Tag.GetType().Name == "AssessmentAssessmentFileFinding")
                        loadDetailsForFindingObject((AssessmentAssessmentFileFinding) e.Node.Tag);
                    else
                        treeNode_AfterSelect_byVulnerabilityType(sender, e); //DC need to make this more explicit
                    break;
                case "Files":
                    treeNode_AfterSelect_byFile(sender, e);
                    break;
                case "Vulnerability Type":
                    treeNode_AfterSelect_byVulnerabilityType(sender, e);
                    break;
                    //case "Action Object":
                default:
                    treeNode_AfterSelect_byActionObject(sender, e);
                    break;
            }
            loadAllChildNodesInGleeViewer(e.Node.Nodes);
            tvFindingsList.Focus();
        }

        private void treeNode_AfterSelect_byFile(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Tag != null)
            {
                switch (e.Node.Tag.GetType().Name)
                {
                    case "AssessmentAssessmentFile":
                        //List<String> lsFindings = Analysis.getListOfFindingsPerFile((String)e.Node.Tag);
                        e.Node.Nodes.Clear();
                        var asAssessmentFile = (AssessmentAssessmentFile) e.Node.Tag;
                        foreach (
                            AssessmentAssessmentFileFinding fFinding in
                                oadAssessmentData.dAssessmentFiles[asAssessmentFile])
                            //foreach (String sFinding in lsFindings)
                        {
                            var tnNewNode =
                                new TreeNode(fFinding.vuln_name ?? OzasmtUtils_OunceV6.getStringIndexValue(UInt32.Parse(fFinding.vuln_name_id),
                                                                                                           oadAssessmentData));
                            tnNewNode.Tag = fFinding;
                            e.Node.Nodes.Add(tnNewNode);
                        }
                        break;
                    case "AssessmentAssessmentFileFinding":
                        var fSelectedFinding = (AssessmentAssessmentFileFinding) e.Node.Tag;
                        var asParentAssessmentFile = (AssessmentAssessmentFile) e.Node.Parent.Tag;
                        showFindingDetailsInDataGridViewAndTreeView(fSelectedFinding, asParentAssessmentFile.filename);
                        break;
                    default:

                        break;
                }

                //    
                e.Node.Expand();
            }
        }

        private void treeNode_AfterSelect_byVulnerabilityType(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Tag != null)
            {
                //String s = e.Node.Tag.GetType().Name;
                switch (e.Node.Tag.GetType().Name)
                {
                    case "String":
                        e.Node.Nodes.Clear();
                        if (oadAssessmentData.dVulnerabilityType.ContainsKey((String) e.Node.Tag))
                        {
                            foreach (
                                AssessmentAssessmentFileFinding fFinding in
                                    oadAssessmentData.dVulnerabilityType[(String) e.Node.Tag])
                            {
                                TreeNode tnNewNode;
                                if (fFinding.context != null)
                                    tnNewNode = new TreeNode(fFinding.context);
                                else
                                    tnNewNode =
                                        new TreeNode(fFinding.vuln_name ?? OzasmtUtils_OunceV6.getStringIndexValue(
                                                                               UInt32.Parse(fFinding.vuln_name_id), oadAssessmentData));
                                tnNewNode.Tag = fFinding;
                                e.Node.Nodes.Add(tnNewNode);
                            }
                        }
                        e.Node.Expand();
                        break;
                    case "AssessmentAssessmentFileFinding":
                        loadDetailsForFindingObject((AssessmentAssessmentFileFinding) e.Node.Tag);
                        break;
                    default:
                        break;
                }
            }
        }

        public void loadAllChildNodesInGleeViewer(TreeNodeCollection tncTreeNodeCollection)
        {
            aGLEE.setAssessmentData(oadAssessmentData);
            if (aGLEE.inMultiNodeMode())
            {
                //aGLEE.clearGraph();

                foreach (TreeNode tnTreeNode in tncTreeNodeCollection)
                {
                    var fFinding = (AssessmentAssessmentFileFinding) tnTreeNode.Tag;
                    var tvTempTreeView = new TreeView(); // temp TreeView to calculate trace
                    showSmartTraceInTreeView(tvTempTreeView, fFinding.Trace, fFinding);
                    if (tvTempTreeView.Nodes[0].Nodes.Count > 0)
                        aGLEE.addNodeToGraph(tvTempTreeView.Nodes[0].Nodes[0], fFinding);
                }
            }
            aGLEE.showLoadedTracesInGleeViewer();
        }

        public void loadDetailsForFindingObject(AssessmentAssessmentFileFinding fFinding)
        {
            if (bExpandingFindingsTreeview == false)
                // only load details if we are not during the process of expanding the Findings treeview
            {
                if (oadAssessmentData.dFindings.ContainsKey(fFinding))
                {
                    String sPathToAssessmentFile = oadAssessmentData.dFindings[fFinding].filename;
                    showFindingDetailsInDataGridViewAndTreeView(fFinding, sPathToAssessmentFile);
                    // MySqlEvents.raiseEvent_ShowCustomRulesDetails_MethodSignature(fFinding.actionobject_id.ToString());
                }
            }
        }

        private string getNodeTextBasedOnSelectedFilter(String sFilter, AssessmentAssessmentFileFinding fFinding)
        {
            String sNodeText = "";
            switch (sFilter)
            {
                case "caller_name":
                    sNodeText = fFinding.caller_name ?? OzasmtUtils_OunceV6.getStringIndexValue(UInt32.Parse(fFinding.caller_name_id),
                                                                                                oadAssessmentData);
                    /*  if (null != fFinding.caller_name)
                        sNodeText = fFinding.caller_name;                        
                    else
                        if (fFinding.caller_name_id != null)
                            sNodeText = Analysis.getStringIndexValue(UInt32.Parse(fFinding.caller_name_id), oadAssessmentData);
                        else
                            sNodeText = "";*/
                    break;
                case "lost_sink":
                    sNodeText = Analysis.getSmartTraceNameOfTraceType(fFinding.Trace, TraceType.Lost_Sink,
                                                                      oadAssessmentData);
                    break;
                case "source":
                    sNodeText = Analysis.getSmartTraceNameOfTraceType(fFinding.Trace, TraceType.Source,
                                                                      oadAssessmentData);
                    break;
                case "known_sink":
                    sNodeText = Analysis.getSmartTraceNameOfTraceType(fFinding.Trace, TraceType.Known_Sink,
                                                                      oadAssessmentData);
                    break;
                case "source_code":
                    AssessmentAssessmentFile afAssessmentFile = oadAssessmentData.dFindings[fFinding];
                    lsSourceCode = Files.loadSourceFileIntoList(afAssessmentFile.filename);
                    if (fFinding.line_number > 0 && lsSourceCode.Count > fFinding.line_number - 1)
                        sNodeText = lsSourceCode[(Int32) fFinding.line_number - 1].Replace("\t", "");
                    ;
                    break;

                case "vuln_type":
                default:
                    sNodeText = fFinding.vuln_type;
                    if (sNodeText == null)
                        sNodeText = OzasmtUtils_OunceV6.getStringIndexValue(UInt32.Parse(fFinding.vuln_type_id),
                                                                    oadAssessmentData);
                    break;
            }
            return sNodeText;
        }

        private void treeNode_AfterSelect_byActionObject(object sender, TreeViewEventArgs e)
        {
            if (e != null && e.Node.Tag != null)
            {
                //   if (false == bExpandingFindingsTreeview)     // don't raise event when were expanding the tree
                //       MySqlEvents.raiseEvent_ShowCustomRulesDetails_MethodSignature(e.Node.Tag.ToString());
                switch (e.Node.Tag.GetType().Name)
                {
                    case "UInt32":
                        e.Node.Nodes.Clear();
                        //      AssessmentAssessmentFileFinding fFirstFindingInActionObject = null;
                        foreach (
                            AssessmentAssessmentFileFinding fFinding in
                                oadAssessmentData.dActionObjects[(UInt32) e.Node.Tag])
                        {
                            String sNodeText = getNodeTextBasedOnSelectedFilter(cbActionObject_NameFilter.Text, fFinding);

                            if (sNodeText != "")
                            {
                                var tnNewNode = new TreeNode(sNodeText);
                                tnNewNode.Tag = fFinding;
                                e.Node.Nodes.Add(tnNewNode);
                            }
                            //            if (null == fFirstFindingInActionObject) // store a ref to the first one in this list
                            //                fFirstFindingInActionObject = fFinding;
                        }
// DC                        setEditCustomRulesVariables(fFirstFindingInActionObject);
                        if (e.Node.Nodes.Count > 0)
                            e.Node.Expand();
                        else
                        {
                            e.Node.Nodes.Clear();
                             DI.log.debug("... no lost sinks in this Action Object...");
                        }
                        //     tvBrowseLoadedAssessment_byActionObject.Sort();

                        break;
                    case "AssessmentAssessmentFileFinding":
                        if (false == bExpandingFindingsTreeview) // don't update if expanding the tree
                        {
                            var fSelectedFinding = (AssessmentAssessmentFileFinding) e.Node.Tag;
                            String sPathToAssessmentFile = oadAssessmentData.dFindings[fSelectedFinding].filename;
                            //  loadSourceFileIntoList(sPathToAssessmentFile);
                            showFindingDetailsInDataGridViewAndTreeView(fSelectedFinding, sPathToAssessmentFile);
                        }
                        break;
                    default:
                        break;
                }
            }
        }


        public void showCallInSourceCodeEditor(String sSourceCodeFile, UInt32 uline_number)
        {
            //sceSourceCodeEditor.loadSourceCodeFile(sSourceCodeFile);            
            sceSourceCodeEditor.gotoLine(sSourceCodeFile, (int) uline_number);
        }

        public void showFindingDetailsInDataGridViewAndTreeView(AssessmentAssessmentFileFinding fSelectedFinding,
                                                                String sPathToSourceFile)
        {
            try
            {
                FindingsView.showFindingDetailsInDataGridView(dgvFindingData, fSelectedFinding, oadAssessmentData);
                /*dgvFindingData.Rows.Clear();
                dgvFindingData.Rows.Add("vuln Name", (fSelectedFinding.vuln_name != null) ? fSelectedFinding.vuln_name : Analysis.getStringIndexValue(UInt32.Parse(fSelectedFinding.vuln_name_id), oadAssessmentData));
                dgvFindingData.Rows.Add("Vuln Type", (fSelectedFinding.vuln_type != null) ? fSelectedFinding.vuln_type : Analysis.getStringIndexValue(UInt32.Parse(fSelectedFinding.vuln_type_id), oadAssessmentData));
                if (fSelectedFinding.context!= null) dgvFindingData.Rows.Add("Context", fSelectedFinding.context.ToString());

                dgvFindingData.Rows.Add("Severity", fSelectedFinding.severity.ToString());
                dgvFindingData.Rows.Add("Confidence", fSelectedFinding.confidence.ToString());
                 */
                //       dgvFindingData.Rows.Add("Action Object", Lddb.getActionObjectName(fSelectedFinding.actionobject_id.ToString()));

                //loadSourceFileIntoList(sPathToSourceFile);

                //showFindingInWebBrowser(wbSourceCodeSnippet_Finding, fSelectedFinding.line_number);

                showCallInSourceCodeEditor(sPathToSourceFile, fSelectedFinding.line_number);


                if (fSelectedFinding.Trace != null)
                {
                    showSmartTraceInTreeView(tvSmartTrace, fSelectedFinding.Trace, fSelectedFinding);
                    aGLEE.addTreeNodeToComboxWithNodesToPlot(tvSmartTrace.Nodes[0], fSelectedFinding, oadAssessmentData);
                    //   the way the Smart traces are build we want to add the 1st child
                }
            }
            catch (Exception ex)
            {
                 DI.log.error("in showFindingDetailsInDataGridViewAndTreeView :{0}", ex.Message);
            }
        }


        /* public void showFindingInWebBrowser(WebBrowser wbTargetWebBrowser, UInt32 uLineNumber)
        {

            if (uLineNumber > 0)
            {
                uLineNumber--;
                if (uLineNumber > lsSourceCode.Count)
                {
                     DI.log.error("In showFindingInWebBrowser uLineNumber > lsSourceCode.Count");
                    return;
                }
                else
                {
                    lsSourceCode[(int)uLineNumber] = "<font color='red'><b>" + lsSourceCode[(int)uLineNumber] + "</b></font>";
                    int iNumberOfLinesToShowBefore = 15;
                    int iNumberOfLinesToShowAfter = 20;
                    int iNumberOfLinesToShow = iNumberOfLinesToShowBefore + iNumberOfLinesToShowAfter;
                    String sConvertedSourceCode = "";
                    int iStartSection = ((int)uLineNumber - iNumberOfLinesToShowBefore > 0) ? (int)uLineNumber - iNumberOfLinesToShowBefore : 0;
                    int iSectionLength = (lsSourceCode.Count - ((int)uLineNumber + iNumberOfLinesToShow) < 1) ? lsSourceCode.Count - (int)uLineNumber + iNumberOfLinesToShowBefore : iNumberOfLinesToShow;
                    if (iSectionLength > lsSourceCode.Count - iStartSection)
                        iSectionLength = lsSourceCode.Count - iStartSection - 1;
                    for (int i = iStartSection; i < (iStartSection + iSectionLength); i++)
                    {
                        int iIndexOfComment = lsSourceCode[i].IndexOf("//");
                        if (iIndexOfComment != -1)
                            lsSourceCode[i] = lsSourceCode[i].Substring(0, iIndexOfComment) + "<font color='darkgreen'>" + lsSourceCode[i].Substring(iIndexOfComment) + "</font>";
                        int iIndexOfDot = lsSourceCode[i].IndexOf('.');
                        if (iIndexOfDot != -1)
                        {
                            int iIndexOfParentis = lsSourceCode[i].Substring(iIndexOfDot).IndexOf('(');
                            if (iIndexOfParentis != -1)
                            {
                                String sToReplace = lsSourceCode[i].Substring(iIndexOfDot, iIndexOfParentis);
                                lsSourceCode[i] = lsSourceCode[i].Replace(sToReplace, "<b>" + sToReplace + "</b>");
                            }
                        }

                    }
                    for (int i = iStartSection; i < (iStartSection + iSectionLength); i++)
                        sConvertedSourceCode += i.ToString() + "  :  " + lsSourceCode[i].Replace("\t", "&nbsp&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;") + "<br/>";

                    // apply global formating (this should be done in a complete different way (at least RegEx should be used :)  )
                    sConvertedSourceCode = "<font face='Verdana' size='1'>" + sConvertedSourceCode + "</font>";
                    sConvertedSourceCode = sConvertedSourceCode.Replace("{", "<font color='gray'>{</font>").Replace("}", "<font color='gray'>}</font>");
                    sConvertedSourceCode = sConvertedSourceCode.Replace("try", "<font color='darkblue'><b>try</b></font>");
                    sConvertedSourceCode = sConvertedSourceCode.Replace("catch", "<font color='darkblue'><b>catch</b></font>");
                    sConvertedSourceCode = sConvertedSourceCode.Replace("public", "<font color='darkblue'><b>public</b></font>");
                    sConvertedSourceCode = sConvertedSourceCode.Replace("private", "<font color='darkblue'><b>private</b></font>");
                    wbSourceCodeSnippet_Finding.DocumentText = sConvertedSourceCode;
                }
            }
            else
            {
                 DI.log.error("In showFindingInWebBrowser uLineNumber was <1 ");
                wbSourceCodeSnippet_Finding.DocumentText = "";
            }
        }
        */

        public void showSmartTraceInTreeView(TreeView tvTargetTreeView, CallInvocation[] cCallInvocations,
                                             AssessmentAssessmentFileFinding fSelectedFinding)
        {
            tvTargetTreeView.Nodes.Clear();
            //String sNodeText = (fSelectedFinding.caller_name != null) ? fSelectedFinding.caller_name : Analysis.getStringIndexValue(UInt32.Parse(fSelectedFinding.caller_name_id), oadAssessmentData); 
            String sNodeText = "O2 Trace";
            var tnRootNode = new TreeNode(sNodeText);
            tnRootNode.Tag = fSelectedFinding;
            AnalysisUtils.addCallsToNode_Recursive(cCallInvocations, tnRootNode, oadAssessmentData, stfSmartTraceFilter);
            tvTargetTreeView.Nodes.Add(tnRootNode.Nodes[0]);
            tvTargetTreeView.ExpandAll();
        }


        private void tvSmartTrace_AfterSelect(object sender, TreeViewEventArgs e)
        {
            switch (e.Node.Tag.GetType().Name)
            {
                case "CallInvocation":
                    if (oadAssessmentData.arAssessmentRun == null)
                        return;
                    var cCall = (CallInvocation) e.Node.Tag;
                    if (oadAssessmentData.arAssessmentRun.FileIndeces.Length < cCall.fn_id)
                        break;
                    String sSourceCodeFile = OzasmtUtils_OunceV6.getFileIndexValue(cCall.fn_id, oadAssessmentData);
                    //oadAssessmentData.arAssessmentRun.FileIndeces[cCall.fn_id - 1].value;
                     DI.log.info("Showing call info on call {0}", e.Node.Text);
                    if (cCall.cxt_id != 0)
                         DI.log.debug("cxt_id {0} = {1}", (cCall.cxt_id - 1).ToString(),
                                        OzasmtUtils_OunceV6.getStringIndexValue(cCall.cxt_id, oadAssessmentData));
                     DI.log.debug("cn_id {0} = {1}", (cCall.cn_id - 1).ToString(),
                                    OzasmtUtils_OunceV6.getStringIndexValue(cCall.cn_id, oadAssessmentData));
                     DI.log.debug("fn_id {0} = {1}", (cCall.fn_id - 1).ToString(),
                                    OzasmtUtils_OunceV6.getFileIndexValue(cCall.fn_id, oadAssessmentData));
                     DI.log.debug("mn_id {0}= {1}", (cCall.mn_id - 1).ToString(),
                                    OzasmtUtils_OunceV6.getStringIndexValue(cCall.mn_id, oadAssessmentData));
                     DI.log.debug("sig_id {0}= {1}", (cCall.sig_id - 1).ToString(),
                                    OzasmtUtils_OunceV6.getStringIndexValue(cCall.sig_id, oadAssessmentData));
                     DI.log.debug("taintpropagation  {0}", cCall.taint_propagation.ToString());
                     DI.log.debug("trace_type  {0}", cCall.trace_type.ToString());
                     DI.log.debug("linenumber  {0}", cCall.line_number.ToString());
                     DI.log.debug("columnnumber  {0}", cCall.column_number.ToString());

                    //loadSourceFileIntoList(sSourceCodeFile);

                    //showFindingInWebBrowser(wbSourceCodeSnippet_Finding, cCall.line_number);                    
                    showCallInSourceCodeEditor(sSourceCodeFile, cCall.line_number);
                    aGLEE.showCallInGlee(e.Node.Text);
                    String sSignature = OzasmtUtils_OunceV6.getStringIndexValue(cCall.sig_id, oadAssessmentData);
//                    ascx_RulesCreator1.addMethodToTargetsList(oadAssessmentData.sDb_id, sSignature, true);
//                    MySqlEvents.raiseEvent_ShowCustomRulesDetails_MethodSignature(oadAssessmentData.sDb_id, sSignature);

                    break;
                case "AssessmentAssessmentFileFinding":
                    break;
                default:
                    break;
            }
            tvSmartTrace.Focus();
        }

        private void rbSmartTraceFilter_MethodName_CheckedChanged(object sender, EventArgs e)
        {
            if (rbSmartTraceFilter_MethodName.Checked)
            {
                stfSmartTraceFilter = Analysis.SmartTraceFilter.MethodName;
                refreshSmartTraceTreeView();
            }
        }

        private void rbSmartTraceFilter_Context_CheckedChanged(object sender, EventArgs e)
        {
            if (rbSmartTraceFilter_Context.Checked)
            {
                stfSmartTraceFilter = Analysis.SmartTraceFilter.Context;
                refreshSmartTraceTreeView();
            }
        }

        private void rbSmartTraceFilter_SourceCode_CheckedChanged(object sender, EventArgs e)
        {
            if (rbSmartTraceFilter_SourceCode.Checked)
            {
                stfSmartTraceFilter = Analysis.SmartTraceFilter.SourceCode;
                refreshSmartTraceTreeView();
            }
        }


        public void refreshSmartTraceTreeView()
        {
            if (tvSmartTrace.Nodes != null)
                if (tvSmartTrace.Nodes.Count > 0)
                    if (tvSmartTrace.Nodes[0].Tag != null)
                        if (tvSmartTrace.Nodes[0].Tag.GetType().Name == "AssessmentAssessmentFileFinding")
                        {
                            var fSelectedFinding = (AssessmentAssessmentFileFinding) tvSmartTrace.Nodes[0].Tag;
                            // tvBrowseLoadedAssessment_byFile.SelectedNode.Tag;
                            if (fSelectedFinding.Trace != null)
                            {
                                String sSourceCodeFile =
                                    oadAssessmentData.arAssessmentRun.FileIndeces[fSelectedFinding.Trace[0].fn_id - 1].
                                        value;
                                lsSourceCode = Files.loadSourceFileIntoList(sSourceCodeFile);
                                showSmartTraceInTreeView(tvSmartTrace, fSelectedFinding.Trace, fSelectedFinding);
                                if (cbVisibleControls_GraphStats.Checked)
                                    aGLEE.addTreeNodeToComboxWithNodesToPlot(tvSmartTrace.Nodes[0].Nodes[0],
                                                                             fSelectedFinding, oadAssessmentData);
                                    //   the way the Smart traces are build we want to add the 2nd child
                                else
                                    aGLEE.clearGraph();
                            }
                        }
        }


        public String getLineFromSourceCode(UInt32 uLineNumeber)
        {
            if (uLineNumeber > 0 && uLineNumeber < lsSourceCode.Count)
                return lsSourceCode[(int) uLineNumeber - 1];
            else
                 DI.log.error("In getLineFromSourceCode uLineNumeber==0 || uLineNumeber >= lsSourceCode.Count");
            return "";
        }

        private void dndDropArea_Load(object sender, EventArgs e)
        {
        }

        private void dndDropArea_eDnDAction_ObjectDataReceived_Event(object oVar)
        {
            try
            {
                String o = oVar.GetType().Name;
                if (oVar.GetType().Name == "List`1")
                {
                    var aoDataReceveived = (List<Object>) oVar;
                    foreach (Object oDataReceived in aoDataReceveived)
                        //String sString = (String)fdndAction.oPayload;
                    {
                        if (oDataReceived.GetType().Name == "String[]")
                        {
                            var asDataReceived = (String[]) oDataReceived;
                            foreach (String sDataReceived in asDataReceived)
                                if (File.Exists(sDataReceived))
                                {
                                    loadAssessmentRunXmlFile(sDataReceived);
                                    return;
                                }
                        }
                        else if (oDataReceived.GetType().Name == "O2AssessmentData")
                        {
                            oadAssessmentData = (O2AssessmentData_OunceV6) oDataReceived;
                            displayLoadedFileStats(new TimeSpan());
                            reloadTreeView();
                        }

                        else
                        {
                            if (File.Exists(oDataReceived.ToString()))
                            {
                                loadAssessmentRunXmlFile(oDataReceived.ToString());
                                return;
                            }
                        }
                    }
                }
                else
                {
                    switch (oVar.GetType().Name)
                    {
                        case "AssessmentRun":
                            //com.ouncelabs.presentation.datalayer.AssessmentRun arAssessmentRun = (com.ouncelabs.presentation.datalayer.AssessmentRun)oVar;
                            //processAssessmentRunReceived(arAssessmentRun);
                             DI.log.error("AssessmentRun Objects not supported");
                            return;
                        case "String":
                            var sString = (String) oVar;
                            if (File.Exists(sString))
                                loadAssessmentRunXmlFile(sString);
                            return;
                        default:
                            if (File.Exists(oVar.ToString()))
                                loadAssessmentRunXmlFile(oVar.ToString());
                            else
                                 DI.log.error("Unrecognized type: {0}", oVar.GetType().Name);
                            return;
                    }
                }
            }
            catch (Exception ex)
            {
                 DI.log.error("in dndDropArea_eDnDAction_ObjectDataReceived_Event: {0}", ex.Message);
            }
        }


        public TreeView _getObject_tvFindingsList()
        {
            return tvFindingsList;
        }

        public TreeView _getObject_tvSmartTrace()
        {
            return tvSmartTrace;
        }

        public TreeView _getObject_tvGLEE_NodesToGraph()
        {
            return aGLEE._getObject_tvGLEE_NodesToGraph();
        }

        /* public Microsoft.Glee.GraphViewerGdi.GViewer _getObject_gvSmartTrace()
        {            
            return aGLEE._getObject_gvSmartTrace();
        }*/


        private void btExpandAll_Click(object sender, EventArgs e)
        {
            bExpandingFindingsTreeview = true; // so that the updates don't fire during the expand all
            foreach (TreeNode tnTreeNnode in tvFindingsList.Nodes)
                tvFindingsList.SelectedNode = tnTreeNnode;
            tvFindingsList.ExpandAll();
            btCollapseAll.Visible = true;
            btExpandAll.Visible = false;
            bExpandingFindingsTreeview = false;
        }

        private void btCollapseAll_Click(object sender, EventArgs e)
        {
            tvFindingsList.CollapseAll();
            btCollapseAll.Visible = false;
            btExpandAll.Visible = true;
        }


        private void cbGLEE_MultiNodes_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void btGLEE_PlotAllTracesInInAnewWindow_Click(object sender, EventArgs e)
        {
             DI.log.error(
                "In btGLEE_PlotAllTracesInInAnewWindow_Click - NOT Implemented in this version of the f1AddOn");
            return;
            /*
             DI.log.debug("Loading all traces into new Glee Window");
            btExpandAll_Click(null, null);
            List<TreeNode> ltnNodes = forms.getListWithAllNodesFromTreeView(tvFindingsList.Nodes);
            if (ltnNodes.Count == tvFindingsList.GetNodeCount(true))
            {
                f1Cmd.open("Glee");
                if (globalStaticVars.dF1Controls.ContainsKey("Glee"))
                {                    
                    formGenericAscxHost fGlee = (formGenericAscxHost)globalStaticVars.dF1LoadedForms["Glee"];
                    ascx_Glee ascxGlee = (ascx_Glee)fGlee.cLoadedAscx;
                    ascxGlee.setViewMode(ascx_Glee.viewMode.allTraces);
                    ascxGlee.clearGraph();
                    ascxGlee.setFadF1AssessmentData(fadF1AssessmentData);
                    foreach (TreeNode tnTreeNodeToAdd in ltnNodes)
                    {
                        if (tnTreeNodeToAdd.Tag != null && "AssessmentAssessmentFileFinding" == tnTreeNodeToAdd.Tag.GetType().Name)
                        {
                            AssessmentAssessmentFileFinding fFinding = (AssessmentAssessmentFileFinding)tnTreeNodeToAdd.Tag;
                            TreeNode tnTrace = new TreeNode(fFinding.vuln_name);
                            if (fFinding.Trace != null)
                            {
                                Analysis.addCallsToNode_Recursive(fFinding.Trace, tnTrace, fadF1AssessmentData, stfSmartTraceFilter);
                            }
                            if (tnTrace.Nodes.Count > 0)
                                ascxGlee.addNodeToGraph(tnTrace.Nodes[0], fFinding);      // don't add the first one since that is the file name or the action object

                        }
                    }
                    
                  //  ascxGlee.loadSmartTraceGraphInGleeViewer(fadF1AssessmentData);
                     //ascxGlee.loadGraph(gvSmartTrace.Graph);
                    ((Form)ascxGlee.Parent).WindowState = FormWindowState.Maximized;
                }
            }                                
            else
                 DI.log.error("Something is wronng with getListWithAllNodesFromTreeView, this should be true ltnNodes.Count == tvGLEE_NodesToGraph.GetNodeCount(true)");
             */
        }

        private void btFilterBySelectedFilter_Click(object sender, EventArgs e)
        {
            setActionObjectFilterObjectsVisibility(true);
            showFilter_bySelectedFilter();
            lbFilter.Text = "Selected filter:";
        }

        private void cbVisibleControls_StatsAndFilters_CheckedChanged(object sender, EventArgs e)
        {
            updateVisibleControlsColapseState();
        }

        private void cbVisibleControls_GraphStats_CheckedChanged(object sender, EventArgs e)
        {
            updateVisibleControlsColapseState();
        }

        private void cbVisibleControls_TraceDetails_CheckedChanged(object sender, EventArgs e)
        {
            updateVisibleControlsColapseState();
        }

        private void cbVisibleControls_SourceCode_CheckedChanged(object sender, EventArgs e)
        {
            updateVisibleControlsColapseState();
        }

        private void cbVisibleControls_CustomRules_CheckedChanged(object sender, EventArgs e)
        {
            updateVisibleControlsColapseState();
        }

        private void updateVisibleControlsColapseState()
        {
            if (cbVisibleControls_StatsAndFilters.Checked)
                scTopLevelContainer_StatsAndRest.Panel1Collapsed = false;
            else
                scTopLevelContainer_StatsAndRest.Panel1Collapsed = true;

            if (false == cbVisibleControls_TraceDetails.Checked && false == cbVisibleControls_SourceCode.Checked &&
                false == cbVisibleControls_GraphStats.Checked && false == cbVisibleControls_CustomRules.Checked)
            {
                splitContainer1.Panel2Collapsed = true;
            }
            else
            {
                splitContainer1.Panel2Collapsed = false;
                if (false == cbVisibleControls_TraceDetails.Checked && false == cbVisibleControls_SourceCode.Checked)
                {
                    scHostsTracesRulesGlee.Panel1Collapsed = true;
                }
                else
                {
                    scHostsTracesRulesGlee.Panel1Collapsed = false;
                    if (cbVisibleControls_TraceDetails.Checked)
                        scTraceDetailAndSourceCode.Panel1Collapsed = false;
                    else
                        scTraceDetailAndSourceCode.Panel1Collapsed = true;

                    if (cbVisibleControls_SourceCode.Checked)
                        scTraceDetailAndSourceCode.Panel2Collapsed = false;
                    else
                        scTraceDetailAndSourceCode.Panel2Collapsed = true;
                }
                if (false == cbVisibleControls_GraphStats.Checked && false == cbVisibleControls_CustomRules.Checked)
                {
                    scHostsTracesRulesGlee.Panel2Collapsed = true;
                }
                else
                {
                    scHostsTracesRulesGlee.Panel2Collapsed = false;
                    if (cbVisibleControls_GraphStats.Checked)
                        scCustomRulesGlee.Panel2Collapsed = false;
                    else
                        scCustomRulesGlee.Panel2Collapsed = true;

                    if (cbVisibleControls_CustomRules.Checked)
                        scCustomRulesGlee.Panel1Collapsed = false;
                    else
                        scCustomRulesGlee.Panel1Collapsed = true;
                }
            }
            /*
            if (cbVisibleControls_GraphStats.Checked)
                splitContainer1.Panel1Collapsed = true;
            else
                splitContainer1.Panel1Collapsed = false;*/
        }

        private void idfInvokeDynamicFilters_Load(object sender, EventArgs e)
        {
        }

        private void dgvFindingData_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void cbAutoTryToResolveSourceCodePaths_CheckedChanged(object sender, EventArgs e)
        {
            HandleO2MessageOnSD.autoTryToFixSourceCodeFileReferences = cbAutoTryToResolveSourceCodePaths.Checked;
        }
    }
}