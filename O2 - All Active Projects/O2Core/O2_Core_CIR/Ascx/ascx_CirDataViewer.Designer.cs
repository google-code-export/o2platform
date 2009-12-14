// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using O2.Views.ASCX.DataViewers;

namespace O2.Core.CIR.Ascx
{
    partial class ascx_CirDataViewer
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ascx_CirDataViewer));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btSaveAsCirDataFile = new System.Windows.Forms.ToolStripButton();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.laNumberOfClasses = new System.Windows.Forms.ToolStripLabel();
            this.btViewCirClasses = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.laNumberOfFunctions = new System.Windows.Forms.ToolStripLabel();
            this.btViewCirFunctions = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel3 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.btFunctionInfo = new System.Windows.Forms.ToolStripButton();
            this.btViewSmartTrace = new System.Windows.Forms.ToolStripButton();
            this.btCreateO2AssessmentWithCallFlowTraces = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.btDeleteAllLoadedData = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.showRulesCreationTools = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.btDragFunctions = new System.Windows.Forms.ToolStripButton();
            this.llDragAllFunctions = new System.Windows.Forms.ToolStripLabel();
            this.llLoadCurrentAssembly = new System.Windows.Forms.ToolStripLabel();
            this.gbSelectedItemInfo = new System.Windows.Forms.GroupBox();
            this.scrollBarHorizontalSize = new System.Windows.Forms.HScrollBar();
            this.scrollBarVerticalSize = new System.Windows.Forms.VScrollBar();
            this.gbCirTrace = new System.Windows.Forms.GroupBox();
            this.gbRulesCreationTools = new System.Windows.Forms.GroupBox();
            this.llDrag_CurrentFilteredFunctions = new System.Windows.Forms.LinkLabel();
            this.llDrag_AllFunctions = new System.Windows.Forms.LinkLabel();
            this.llDrag_LostSinks = new System.Windows.Forms.LinkLabel();
            this.llDrag_LostSources = new System.Windows.Forms.LinkLabel();
            this.lbLoadingDroppedFile = new System.Windows.Forms.Label();
            this.rbShowAll = new System.Windows.Forms.RadioButton();
            this.rbOnlyShowFunctionsWithControlFlowGraphs = new System.Windows.Forms.RadioButton();
            this.rbOnlyShowExternalFunctionsThatAreInvokedFromCFG = new System.Windows.Forms.RadioButton();
            this.rbShowOnlyFunctionsWithCallersOrCallees = new System.Windows.Forms.RadioButton();
            this.contextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.addBreakpointOnSelectedMethodifBreakpointIsEnabledToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.whenSourceCodeReferencesAreAvailableToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showCodeSnippetOnMouseOverToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openSourceCodeFileAndSelectLineToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.whenSourceCodeIsNOTAvailableToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showFunctionsDecompiledCodeInSourceCodeViewerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.onFileDropOrLoadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.calculateTheCallersCalleswillLoadSlowerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.functionCallsForSelectedItem = new O2.Core.CIR.Ascx.ascx_FunctionCalls();
            this.cirTraceForSelectedItem = new O2.Core.CIR.Ascx.ascx_CirTrace();
            this.functionsViewer = new O2.Views.ASCX.DataViewers.ascx_FunctionsViewer();
            this.decompiledSourceCodeAndCreateTempSourceFilesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1.SuspendLayout();
            this.gbSelectedItemInfo.SuspendLayout();
            this.gbCirTrace.SuspendLayout();
            this.gbRulesCreationTools.SuspendLayout();
            this.contextMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btSaveAsCirDataFile,
            this.toolStripLabel1,
            this.laNumberOfClasses,
            this.btViewCirClasses,
            this.toolStripSeparator2,
            this.toolStripLabel2,
            this.laNumberOfFunctions,
            this.btViewCirFunctions,
            this.toolStripSeparator1,
            this.toolStripLabel3,
            this.toolStripSeparator3,
            this.btFunctionInfo,
            this.btViewSmartTrace,
            this.btCreateO2AssessmentWithCallFlowTraces,
            this.toolStripSeparator4,
            this.btDeleteAllLoadedData,
            this.toolStripSeparator5,
            this.toolStripSeparator6,
            this.showRulesCreationTools,
            this.toolStripSeparator7,
            this.btDragFunctions,
            this.llDragAllFunctions,
            this.llLoadCurrentAssembly});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(983, 25);
            this.toolStrip1.TabIndex = 8;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btSaveAsCirDataFile
            // 
            this.btSaveAsCirDataFile.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btSaveAsCirDataFile.Image = ((System.Drawing.Image)(resources.GetObject("btSaveAsCirDataFile.Image")));
            this.btSaveAsCirDataFile.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btSaveAsCirDataFile.Name = "btSaveAsCirDataFile";
            this.btSaveAsCirDataFile.Size = new System.Drawing.Size(23, 22);
            this.btSaveAsCirDataFile.Text = "Save As CirData file";
            this.btSaveAsCirDataFile.Click += new System.EventHandler(this.btSaveAsCirDataFile_Click);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(56, 22);
            this.toolStripLabel1.Text = "# classes:";
            // 
            // laNumberOfClasses
            // 
            this.laNumberOfClasses.Name = "laNumberOfClasses";
            this.laNumberOfClasses.Size = new System.Drawing.Size(19, 22);
            this.laNumberOfClasses.Text = "...";
            this.laNumberOfClasses.Click += new System.EventHandler(this.laNumberOfClasses_Click);
            // 
            // btViewCirClasses
            // 
            this.btViewCirClasses.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btViewCirClasses.Image = ((System.Drawing.Image)(resources.GetObject("btViewCirClasses.Image")));
            this.btViewCirClasses.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btViewCirClasses.Name = "btViewCirClasses";
            this.btViewCirClasses.Size = new System.Drawing.Size(23, 22);
            this.btViewCirClasses.Text = "View Cir Classes";
            this.btViewCirClasses.Click += new System.EventHandler(this.btViewCirClasses_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(62, 22);
            this.toolStripLabel2.Text = "# functions";
            // 
            // laNumberOfFunctions
            // 
            this.laNumberOfFunctions.Name = "laNumberOfFunctions";
            this.laNumberOfFunctions.Size = new System.Drawing.Size(19, 22);
            this.laNumberOfFunctions.Text = "...";
            this.laNumberOfFunctions.Click += new System.EventHandler(this.laNumberOfMethods_Click);
            // 
            // btViewCirFunctions
            // 
            this.btViewCirFunctions.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btViewCirFunctions.Image = ((System.Drawing.Image)(resources.GetObject("btViewCirFunctions.Image")));
            this.btViewCirFunctions.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btViewCirFunctions.Name = "btViewCirFunctions";
            this.btViewCirFunctions.Size = new System.Drawing.Size(23, 22);
            this.btViewCirFunctions.Text = "View Cir Functions";
            this.btViewCirFunctions.Click += new System.EventHandler(this.btViewCirFunctions_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripLabel3
            // 
            this.toolStripLabel3.Name = "toolStripLabel3";
            this.toolStripLabel3.Size = new System.Drawing.Size(36, 22);
            this.toolStripLabel3.Text = "config";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // btFunctionInfo
            // 
            this.btFunctionInfo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btFunctionInfo.Image = ((System.Drawing.Image)(resources.GetObject("btFunctionInfo.Image")));
            this.btFunctionInfo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btFunctionInfo.Name = "btFunctionInfo";
            this.btFunctionInfo.Size = new System.Drawing.Size(23, 22);
            this.btFunctionInfo.Text = "Function Info";
            this.btFunctionInfo.Click += new System.EventHandler(this.btFunctionInfo_Click);
            // 
            // btViewSmartTrace
            // 
            this.btViewSmartTrace.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btViewSmartTrace.Image = ((System.Drawing.Image)(resources.GetObject("btViewSmartTrace.Image")));
            this.btViewSmartTrace.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btViewSmartTrace.Name = "btViewSmartTrace";
            this.btViewSmartTrace.Size = new System.Drawing.Size(23, 22);
            this.btViewSmartTrace.Text = "View Smart Trace For Function";
            this.btViewSmartTrace.Click += new System.EventHandler(this.btViewSmartTrace_Click);
            // 
            // btCreateO2AssessmentWithCallFlowTraces
            // 
            this.btCreateO2AssessmentWithCallFlowTraces.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btCreateO2AssessmentWithCallFlowTraces.Image = ((System.Drawing.Image)(resources.GetObject("btCreateO2AssessmentWithCallFlowTraces.Image")));
            this.btCreateO2AssessmentWithCallFlowTraces.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btCreateO2AssessmentWithCallFlowTraces.Name = "btCreateO2AssessmentWithCallFlowTraces";
            this.btCreateO2AssessmentWithCallFlowTraces.Size = new System.Drawing.Size(23, 22);
            this.btCreateO2AssessmentWithCallFlowTraces.Text = "Create O2 Assessment with Call Flow Traces";
            this.btCreateO2AssessmentWithCallFlowTraces.Click += new System.EventHandler(this.btCreateO2AssessmentWithCallFlowTraces_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
            // 
            // btDeleteAllLoadedData
            // 
            this.btDeleteAllLoadedData.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btDeleteAllLoadedData.Image = ((System.Drawing.Image)(resources.GetObject("btDeleteAllLoadedData.Image")));
            this.btDeleteAllLoadedData.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btDeleteAllLoadedData.Name = "btDeleteAllLoadedData";
            this.btDeleteAllLoadedData.Size = new System.Drawing.Size(23, 22);
            this.btDeleteAllLoadedData.Text = "Delete All Loaded Data";
            this.btDeleteAllLoadedData.Click += new System.EventHandler(this.btDeleteAllLoadedData_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(6, 25);
            // 
            // showRulesCreationTools
            // 
            this.showRulesCreationTools.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.showRulesCreationTools.Image = ((System.Drawing.Image)(resources.GetObject("showRulesCreationTools.Image")));
            this.showRulesCreationTools.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.showRulesCreationTools.Name = "showRulesCreationTools";
            this.showRulesCreationTools.Size = new System.Drawing.Size(23, 22);
            this.showRulesCreationTools.Text = "Show Rules Creation Tools";
            this.showRulesCreationTools.Click += new System.EventHandler(this.showRulesCreationTools_Click);
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(6, 25);
            // 
            // btDragFunctions
            // 
            this.btDragFunctions.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btDragFunctions.Image = ((System.Drawing.Image)(resources.GetObject("btDragFunctions.Image")));
            this.btDragFunctions.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btDragFunctions.Name = "btDragFunctions";
            this.btDragFunctions.Size = new System.Drawing.Size(23, 22);
            this.btDragFunctions.Text = "toolStripButton1";
            this.btDragFunctions.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btDragFunctions_MouseDown);
            // 
            // llDragAllFunctions
            // 
            this.llDragAllFunctions.IsLink = true;
            this.llDragAllFunctions.Name = "llDragAllFunctions";
            this.llDragAllFunctions.Size = new System.Drawing.Size(76, 22);
            this.llDragAllFunctions.Text = "drag functions";
            this.llDragAllFunctions.MouseDown += new System.Windows.Forms.MouseEventHandler(this.llDrag_AllFunctions_MouseDown);
            // 
            // llLoadCurrentAssembly
            // 
            this.llLoadCurrentAssembly.IsLink = true;
            this.llLoadCurrentAssembly.Name = "llLoadCurrentAssembly";
            this.llLoadCurrentAssembly.Size = new System.Drawing.Size(145, 22);
            this.llLoadCurrentAssembly.Text = "demo: load current assembly";
            this.llLoadCurrentAssembly.Click += new System.EventHandler(this.llLoadCurrentAssembly_Click);
            // 
            // gbSelectedItemInfo
            // 
            this.gbSelectedItemInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gbSelectedItemInfo.Controls.Add(this.scrollBarHorizontalSize);
            this.gbSelectedItemInfo.Controls.Add(this.scrollBarVerticalSize);
            this.gbSelectedItemInfo.Controls.Add(this.functionCallsForSelectedItem);
            this.gbSelectedItemInfo.Location = new System.Drawing.Point(170, 218);
            this.gbSelectedItemInfo.Name = "gbSelectedItemInfo";
            this.gbSelectedItemInfo.Size = new System.Drawing.Size(810, 251);
            this.gbSelectedItemInfo.TabIndex = 9;
            this.gbSelectedItemInfo.TabStop = false;
            this.gbSelectedItemInfo.Visible = false;
            this.gbSelectedItemInfo.SizeChanged += new System.EventHandler(this.gbSelectedItemInfo_SizeChanged);
            // 
            // scrollBarHorizontalSize
            // 
            this.scrollBarHorizontalSize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.scrollBarHorizontalSize.Location = new System.Drawing.Point(755, 234);
            this.scrollBarHorizontalSize.Name = "scrollBarHorizontalSize";
            this.scrollBarHorizontalSize.Size = new System.Drawing.Size(38, 10);
            this.scrollBarHorizontalSize.SmallChange = 10;
            this.scrollBarHorizontalSize.TabIndex = 5;
            this.scrollBarHorizontalSize.Scroll += new System.Windows.Forms.ScrollEventHandler(this.scrollBarHorizontalSize_Scroll);
            // 
            // scrollBarVerticalSize
            // 
            this.scrollBarVerticalSize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.scrollBarVerticalSize.Location = new System.Drawing.Point(793, 206);
            this.scrollBarVerticalSize.Name = "scrollBarVerticalSize";
            this.scrollBarVerticalSize.Size = new System.Drawing.Size(10, 38);
            this.scrollBarVerticalSize.SmallChange = 10;
            this.scrollBarVerticalSize.TabIndex = 3;
            this.scrollBarVerticalSize.Scroll += new System.Windows.Forms.ScrollEventHandler(this.scrollBarVerticalSize_Scroll);
            // 
            // gbCirTrace
            // 
            this.gbCirTrace.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gbCirTrace.Controls.Add(this.cirTraceForSelectedItem);
            this.gbCirTrace.Location = new System.Drawing.Point(170, 57);
            this.gbCirTrace.Name = "gbCirTrace";
            this.gbCirTrace.Size = new System.Drawing.Size(807, 313);
            this.gbCirTrace.TabIndex = 10;
            this.gbCirTrace.TabStop = false;
            this.gbCirTrace.Text = "Cir Trace";
            this.gbCirTrace.Visible = false;
            // 
            // gbRulesCreationTools
            // 
            this.gbRulesCreationTools.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.gbRulesCreationTools.Controls.Add(this.llDrag_CurrentFilteredFunctions);
            this.gbRulesCreationTools.Controls.Add(this.llDrag_AllFunctions);
            this.gbRulesCreationTools.Controls.Add(this.llDrag_LostSinks);
            this.gbRulesCreationTools.Controls.Add(this.llDrag_LostSources);
            this.gbRulesCreationTools.Enabled = false;
            this.gbRulesCreationTools.Location = new System.Drawing.Point(326, 209);
            this.gbRulesCreationTools.Name = "gbRulesCreationTools";
            this.gbRulesCreationTools.Size = new System.Drawing.Size(338, 128);
            this.gbRulesCreationTools.TabIndex = 11;
            this.gbRulesCreationTools.TabStop = false;
            this.gbRulesCreationTools.Text = "Functions Filters (Disabled since these are not working 100% )";
            this.gbRulesCreationTools.Visible = false;
            // 
            // llDrag_CurrentFilteredFunctions
            // 
            this.llDrag_CurrentFilteredFunctions.AutoSize = true;
            this.llDrag_CurrentFilteredFunctions.Enabled = false;
            this.llDrag_CurrentFilteredFunctions.Location = new System.Drawing.Point(7, 97);
            this.llDrag_CurrentFilteredFunctions.Name = "llDrag_CurrentFilteredFunctions";
            this.llDrag_CurrentFilteredFunctions.Size = new System.Drawing.Size(153, 13);
            this.llDrag_CurrentFilteredFunctions.TabIndex = 3;
            this.llDrag_CurrentFilteredFunctions.TabStop = true;
            this.llDrag_CurrentFilteredFunctions.Text = "Drag Current Filtered Functions";
            this.llDrag_CurrentFilteredFunctions.MouseDown += new System.Windows.Forms.MouseEventHandler(this.llDrag_CurrentFilteredFunctions_MouseDown);
            // 
            // llDrag_AllFunctions
            // 
            this.llDrag_AllFunctions.AutoSize = true;
            this.llDrag_AllFunctions.Location = new System.Drawing.Point(7, 70);
            this.llDrag_AllFunctions.Name = "llDrag_AllFunctions";
            this.llDrag_AllFunctions.Size = new System.Drawing.Size(93, 13);
            this.llDrag_AllFunctions.TabIndex = 2;
            this.llDrag_AllFunctions.TabStop = true;
            this.llDrag_AllFunctions.Text = "Drag All Functions";
            this.llDrag_AllFunctions.MouseDown += new System.Windows.Forms.MouseEventHandler(this.llDrag_AllFunctions_MouseDown);
            // 
            // llDrag_LostSinks
            // 
            this.llDrag_LostSinks.AutoSize = true;
            this.llDrag_LostSinks.Location = new System.Drawing.Point(7, 44);
            this.llDrag_LostSinks.Name = "llDrag_LostSinks";
            this.llDrag_LostSinks.Size = new System.Drawing.Size(204, 13);
            this.llDrag_LostSinks.TabIndex = 1;
            this.llDrag_LostSinks.TabStop = true;
            this.llDrag_LostSinks.Text = "Drag Lost Sinks (dont call other functions)";
            this.llDrag_LostSinks.MouseDown += new System.Windows.Forms.MouseEventHandler(this.llCreateRulesWith_LostSinks_MouseDown);
            // 
            // llDrag_LostSources
            // 
            this.llDrag_LostSources.AutoSize = true;
            this.llDrag_LostSources.Location = new System.Drawing.Point(7, 20);
            this.llDrag_LostSources.Name = "llDrag_LostSources";
            this.llDrag_LostSources.Size = new System.Drawing.Size(255, 13);
            this.llDrag_LostSources.TabIndex = 0;
            this.llDrag_LostSources.TabStop = true;
            this.llDrag_LostSources.Text = "Drag Lost Sources (are not called by other functions)";
            this.llDrag_LostSources.MouseDown += new System.Windows.Forms.MouseEventHandler(this.llCreateRulesWith_LostSources_MouseDown);
            // 
            // lbLoadingDroppedFile
            // 
            this.lbLoadingDroppedFile.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lbLoadingDroppedFile.AutoSize = true;
            this.lbLoadingDroppedFile.BackColor = System.Drawing.SystemColors.Window;
            this.lbLoadingDroppedFile.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbLoadingDroppedFile.ForeColor = System.Drawing.SystemColors.AppWorkspace;
            this.lbLoadingDroppedFile.Location = new System.Drawing.Point(398, 237);
            this.lbLoadingDroppedFile.Name = "lbLoadingDroppedFile";
            this.lbLoadingDroppedFile.Size = new System.Drawing.Size(190, 29);
            this.lbLoadingDroppedFile.TabIndex = 22;
            this.lbLoadingDroppedFile.Text = "Loading File(s)";
            this.lbLoadingDroppedFile.Visible = false;
            // 
            // rbShowAll
            // 
            this.rbShowAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.rbShowAll.AutoSize = true;
            this.rbShowAll.Location = new System.Drawing.Point(7, 503);
            this.rbShowAll.Name = "rbShowAll";
            this.rbShowAll.Size = new System.Drawing.Size(66, 17);
            this.rbShowAll.TabIndex = 14;
            this.rbShowAll.Text = "Show All";
            this.rbShowAll.UseVisualStyleBackColor = true;
            this.rbShowAll.CheckedChanged += new System.EventHandler(this.rbShowAll_CheckedChanged);
            // 
            // rbOnlyShowFunctionsWithControlFlowGraphs
            // 
            this.rbOnlyShowFunctionsWithControlFlowGraphs.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.rbOnlyShowFunctionsWithControlFlowGraphs.AutoSize = true;
            this.rbOnlyShowFunctionsWithControlFlowGraphs.Checked = true;
            this.rbOnlyShowFunctionsWithControlFlowGraphs.Location = new System.Drawing.Point(304, 504);
            this.rbOnlyShowFunctionsWithControlFlowGraphs.Name = "rbOnlyShowFunctionsWithControlFlowGraphs";
            this.rbOnlyShowFunctionsWithControlFlowGraphs.Size = new System.Drawing.Size(245, 17);
            this.rbOnlyShowFunctionsWithControlFlowGraphs.TabIndex = 15;
            this.rbOnlyShowFunctionsWithControlFlowGraphs.TabStop = true;
            this.rbOnlyShowFunctionsWithControlFlowGraphs.Text = "Only Show Functions with Control Flow Graphs";
            this.rbOnlyShowFunctionsWithControlFlowGraphs.UseVisualStyleBackColor = true;
            this.rbOnlyShowFunctionsWithControlFlowGraphs.CheckedChanged += new System.EventHandler(this.rbOnlyShowFunctionsWithControlFlowGraphs_CheckedChanged);
            // 
            // rbOnlyShowExternalFunctionsThatAreInvokedFromCFG
            // 
            this.rbOnlyShowExternalFunctionsThatAreInvokedFromCFG.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.rbOnlyShowExternalFunctionsThatAreInvokedFromCFG.AutoSize = true;
            this.rbOnlyShowExternalFunctionsThatAreInvokedFromCFG.Location = new System.Drawing.Point(554, 504);
            this.rbOnlyShowExternalFunctionsThatAreInvokedFromCFG.Name = "rbOnlyShowExternalFunctionsThatAreInvokedFromCFG";
            this.rbOnlyShowExternalFunctionsThatAreInvokedFromCFG.Size = new System.Drawing.Size(366, 17);
            this.rbOnlyShowExternalFunctionsThatAreInvokedFromCFG.TabIndex = 16;
            this.rbOnlyShowExternalFunctionsThatAreInvokedFromCFG.Text = "Only Show external Functions that are invoked from Control Flow Graphs";
            this.rbOnlyShowExternalFunctionsThatAreInvokedFromCFG.UseVisualStyleBackColor = true;
            this.rbOnlyShowExternalFunctionsThatAreInvokedFromCFG.CheckedChanged += new System.EventHandler(this.rbOnlyShowExternalFunctionsThatAreInvokedFromCFG_CheckedChanged);
            // 
            // rbShowOnlyFunctionsWithCallersOrCallees
            // 
            this.rbShowOnlyFunctionsWithCallersOrCallees.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.rbShowOnlyFunctionsWithCallersOrCallees.AutoSize = true;
            this.rbShowOnlyFunctionsWithCallersOrCallees.Location = new System.Drawing.Point(77, 503);
            this.rbShowOnlyFunctionsWithCallersOrCallees.Name = "rbShowOnlyFunctionsWithCallersOrCallees";
            this.rbShowOnlyFunctionsWithCallersOrCallees.Size = new System.Drawing.Size(226, 17);
            this.rbShowOnlyFunctionsWithCallersOrCallees.TabIndex = 23;
            this.rbShowOnlyFunctionsWithCallersOrCallees.Text = "Show only Functions with callers or callees";
            this.rbShowOnlyFunctionsWithCallersOrCallees.UseVisualStyleBackColor = true;
            this.rbShowOnlyFunctionsWithCallersOrCallees.CheckedChanged += new System.EventHandler(this.rbShowOnlyFunctionsWithCallersOrCallees_CheckedChanged);
            // 
            // contextMenu
            // 
            this.contextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addBreakpointOnSelectedMethodifBreakpointIsEnabledToolStripMenuItem,
            this.whenSourceCodeReferencesAreAvailableToolStripMenuItem,
            this.whenSourceCodeIsNOTAvailableToolStripMenuItem,
            this.onFileDropOrLoadToolStripMenuItem});
            this.contextMenu.Name = "contextMenu";
            this.contextMenu.Size = new System.Drawing.Size(367, 114);
            this.contextMenu.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenu_Opening);
            // 
            // addBreakpointOnSelectedMethodifBreakpointIsEnabledToolStripMenuItem
            // 
            this.addBreakpointOnSelectedMethodifBreakpointIsEnabledToolStripMenuItem.Name = "addBreakpointOnSelectedMethodifBreakpointIsEnabledToolStripMenuItem";
            this.addBreakpointOnSelectedMethodifBreakpointIsEnabledToolStripMenuItem.Size = new System.Drawing.Size(366, 22);
            this.addBreakpointOnSelectedMethodifBreakpointIsEnabledToolStripMenuItem.Text = "Add breakpoint on selected method (if breakpoint is enabled)";
            this.addBreakpointOnSelectedMethodifBreakpointIsEnabledToolStripMenuItem.Click += new System.EventHandler(this.addBreakpointOnSelectedMethodifBreakpointIsEnabledToolStripMenuItem_Click);
            // 
            // whenSourceCodeReferencesAreAvailableToolStripMenuItem
            // 
            this.whenSourceCodeReferencesAreAvailableToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showCodeSnippetOnMouseOverToolStripMenuItem,
            this.openSourceCodeFileAndSelectLineToolStripMenuItem});
            this.whenSourceCodeReferencesAreAvailableToolStripMenuItem.Name = "whenSourceCodeReferencesAreAvailableToolStripMenuItem";
            this.whenSourceCodeReferencesAreAvailableToolStripMenuItem.Size = new System.Drawing.Size(366, 22);
            this.whenSourceCodeReferencesAreAvailableToolStripMenuItem.Text = "When Source Code references are available:";
            // 
            // showCodeSnippetOnMouseOverToolStripMenuItem
            // 
            this.showCodeSnippetOnMouseOverToolStripMenuItem.CheckOnClick = true;
            this.showCodeSnippetOnMouseOverToolStripMenuItem.Name = "showCodeSnippetOnMouseOverToolStripMenuItem";
            this.showCodeSnippetOnMouseOverToolStripMenuItem.Size = new System.Drawing.Size(249, 22);
            this.showCodeSnippetOnMouseOverToolStripMenuItem.Text = "Show code snippet on Mouse over";
            // 
            // openSourceCodeFileAndSelectLineToolStripMenuItem
            // 
            this.openSourceCodeFileAndSelectLineToolStripMenuItem.CheckOnClick = true;
            this.openSourceCodeFileAndSelectLineToolStripMenuItem.Name = "openSourceCodeFileAndSelectLineToolStripMenuItem";
            this.openSourceCodeFileAndSelectLineToolStripMenuItem.Size = new System.Drawing.Size(249, 22);
            this.openSourceCodeFileAndSelectLineToolStripMenuItem.Text = "Open source code file and select line";
            // 
            // whenSourceCodeIsNOTAvailableToolStripMenuItem
            // 
            this.whenSourceCodeIsNOTAvailableToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showFunctionsDecompiledCodeInSourceCodeViewerToolStripMenuItem,
            this.decompiledSourceCodeAndCreateTempSourceFilesToolStripMenuItem});
            this.whenSourceCodeIsNOTAvailableToolStripMenuItem.Name = "whenSourceCodeIsNOTAvailableToolStripMenuItem";
            this.whenSourceCodeIsNOTAvailableToolStripMenuItem.Size = new System.Drawing.Size(366, 22);
            this.whenSourceCodeIsNOTAvailableToolStripMenuItem.Text = "When Source Code is NOT available (DotNet only)";
            // 
            // showFunctionsDecompiledCodeInSourceCodeViewerToolStripMenuItem
            // 
            this.showFunctionsDecompiledCodeInSourceCodeViewerToolStripMenuItem.CheckOnClick = true;
            this.showFunctionsDecompiledCodeInSourceCodeViewerToolStripMenuItem.Name = "showFunctionsDecompiledCodeInSourceCodeViewerToolStripMenuItem";
            this.showFunctionsDecompiledCodeInSourceCodeViewerToolStripMenuItem.Size = new System.Drawing.Size(393, 22);
            this.showFunctionsDecompiledCodeInSourceCodeViewerToolStripMenuItem.Text = "Show Function\'s Decompiled Code in  Source Code Viewer";
            this.showFunctionsDecompiledCodeInSourceCodeViewerToolStripMenuItem.Visible = false;
            // 
            // onFileDropOrLoadToolStripMenuItem
            // 
            this.onFileDropOrLoadToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.calculateTheCallersCalleswillLoadSlowerToolStripMenuItem});
            this.onFileDropOrLoadToolStripMenuItem.Name = "onFileDropOrLoadToolStripMenuItem";
            this.onFileDropOrLoadToolStripMenuItem.Size = new System.Drawing.Size(366, 22);
            this.onFileDropOrLoadToolStripMenuItem.Text = "On file drop or load";
            // 
            // calculateTheCallersCalleswillLoadSlowerToolStripMenuItem
            // 
            this.calculateTheCallersCalleswillLoadSlowerToolStripMenuItem.Checked = true;
            this.calculateTheCallersCalleswillLoadSlowerToolStripMenuItem.CheckOnClick = true;
            this.calculateTheCallersCalleswillLoadSlowerToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.calculateTheCallersCalleswillLoadSlowerToolStripMenuItem.Name = "calculateTheCallersCalleswillLoadSlowerToolStripMenuItem";
            this.calculateTheCallersCalleswillLoadSlowerToolStripMenuItem.Size = new System.Drawing.Size(356, 22);
            this.calculateTheCallersCalleswillLoadSlowerToolStripMenuItem.Text = "Calculate the Callers && Callees references (will load slower)";
            // 
            // functionCallsForSelectedItem
            // 
            this.functionCallsForSelectedItem.currentCirClass = null;
            this.functionCallsForSelectedItem.Dock = System.Windows.Forms.DockStyle.Fill;
            this.functionCallsForSelectedItem.Location = new System.Drawing.Point(3, 16);
            this.functionCallsForSelectedItem.Name = "functionCallsForSelectedItem";
            this.functionCallsForSelectedItem.rootCirFunction = null;
            this.functionCallsForSelectedItem.Size = new System.Drawing.Size(804, 232);
            this.functionCallsForSelectedItem.TabIndex = 0;
            // 
            // cirTraceForSelectedItem
            // 
            this.cirTraceForSelectedItem.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cirTraceForSelectedItem.Location = new System.Drawing.Point(3, 16);
            this.cirTraceForSelectedItem.Name = "cirTraceForSelectedItem";
            this.cirTraceForSelectedItem.rootCirFunction = null;
            this.cirTraceForSelectedItem.Size = new System.Drawing.Size(801, 294);
            this.cirTraceForSelectedItem.TabIndex = 0;
            // 
            // functionsViewer
            // 
            this.functionsViewer._AdvancedModeViews = true;
            this.functionsViewer.AllowDrop = true;
            this.functionsViewer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.functionsViewer.BackColor = System.Drawing.SystemColors.Control;
            this.functionsViewer.ContextMenuStrip = this.contextMenu;
            this.functionsViewer.ForeColor = System.Drawing.Color.Black;
            this.functionsViewer.Location = new System.Drawing.Point(3, 28);
            this.functionsViewer.Name = "functionsViewer";
            this.functionsViewer.NamespaceDepthValue = 2;
            this.functionsViewer.Size = new System.Drawing.Size(977, 470);
            this.functionsViewer.TabIndex = 2;
            this.functionsViewer._onItemDrag += new O2.Kernel.CodeUtils.Callbacks.dMethod_Object(this.functionsViewer__onItemDrag);
            this.functionsViewer._onMouseMove += new System.Action<System.Windows.Forms.TreeNode>(this.functionsViewer__onMouseMove);
            this.functionsViewer._onDoubleClick += new O2.Kernel.CodeUtils.Callbacks.dMethod_Object(this.functionsViewer__onDoubleClick);
            this.functionsViewer._onAfterSelect += new O2.Kernel.CodeUtils.Callbacks.dMethod_Object(this.functionsViewer__onAfterSelect);
            this.functionsViewer._onDrop += new O2.Kernel.CodeUtils.Callbacks.dMethod_Object(this.functionsViewer__onDrop);
            // 
            // decompiledSourceCodeAndCreateTempSourceFilesToolStripMenuItem
            // 
            this.decompiledSourceCodeAndCreateTempSourceFilesToolStripMenuItem.CheckOnClick = true;
            this.decompiledSourceCodeAndCreateTempSourceFilesToolStripMenuItem.Name = "decompiledSourceCodeAndCreateTempSourceFilesToolStripMenuItem";
            this.decompiledSourceCodeAndCreateTempSourceFilesToolStripMenuItem.Size = new System.Drawing.Size(393, 22);
            this.decompiledSourceCodeAndCreateTempSourceFilesToolStripMenuItem.Text = "(On file Drop) decompile Source Code and create temp Source files";
            // 
            // ascx_CirDataViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lbLoadingDroppedFile);
            this.Controls.Add(this.gbSelectedItemInfo);
            this.Controls.Add(this.gbCirTrace);
            this.Controls.Add(this.rbOnlyShowExternalFunctionsThatAreInvokedFromCFG);
            this.Controls.Add(this.rbOnlyShowFunctionsWithControlFlowGraphs);
            this.Controls.Add(this.rbShowOnlyFunctionsWithCallersOrCallees);
            this.Controls.Add(this.rbShowAll);
            this.Controls.Add(this.gbRulesCreationTools);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.functionsViewer);
            this.Name = "ascx_CirDataViewer";
            this.Size = new System.Drawing.Size(983, 521);
            this.Load += new System.EventHandler(this.ascx_CirDataViewer_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.gbSelectedItemInfo.ResumeLayout(false);
            this.gbCirTrace.ResumeLayout(false);
            this.gbRulesCreationTools.ResumeLayout(false);
            this.gbRulesCreationTools.PerformLayout();
            this.contextMenu.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ascx_FunctionsViewer functionsViewer;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripLabel laNumberOfClasses;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private System.Windows.Forms.ToolStripLabel laNumberOfFunctions;
        private System.Windows.Forms.ToolStripButton btViewCirClasses;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton btViewCirFunctions;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel3;
        private System.Windows.Forms.ToolStripButton btFunctionInfo;
        private System.Windows.Forms.GroupBox gbSelectedItemInfo;
        private System.Windows.Forms.ToolStripButton btViewSmartTrace;
        private ascx_FunctionCalls functionCallsForSelectedItem;
        private System.Windows.Forms.GroupBox gbCirTrace;
        private ascx_CirTrace cirTraceForSelectedItem;
        private System.Windows.Forms.ToolStripButton btCreateO2AssessmentWithCallFlowTraces;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton btDeleteAllLoadedData;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripButton showRulesCreationTools;
        private System.Windows.Forms.GroupBox gbRulesCreationTools;
        private System.Windows.Forms.LinkLabel llDrag_LostSinks;
        private System.Windows.Forms.LinkLabel llDrag_LostSources;
        private System.Windows.Forms.LinkLabel llDrag_AllFunctions;
        private System.Windows.Forms.RadioButton rbShowAll;
        private System.Windows.Forms.RadioButton rbOnlyShowFunctionsWithControlFlowGraphs;
        private System.Windows.Forms.RadioButton rbOnlyShowExternalFunctionsThatAreInvokedFromCFG;
        private System.Windows.Forms.LinkLabel llDrag_CurrentFilteredFunctions;
        private System.Windows.Forms.Label lbLoadingDroppedFile;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.ToolStripLabel llDragAllFunctions;
        private System.Windows.Forms.RadioButton rbShowOnlyFunctionsWithCallersOrCallees;
        private System.Windows.Forms.ToolStripButton btDragFunctions;
        private System.Windows.Forms.VScrollBar scrollBarVerticalSize;
        private System.Windows.Forms.HScrollBar scrollBarHorizontalSize;
        private System.Windows.Forms.ToolStripButton btSaveAsCirDataFile;
        private System.Windows.Forms.ToolStripLabel llLoadCurrentAssembly;
        private System.Windows.Forms.ContextMenuStrip contextMenu;
        private System.Windows.Forms.ToolStripMenuItem whenSourceCodeReferencesAreAvailableToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showCodeSnippetOnMouseOverToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openSourceCodeFileAndSelectLineToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem onFileDropOrLoadToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem calculateTheCallersCalleswillLoadSlowerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addBreakpointOnSelectedMethodifBreakpointIsEnabledToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem whenSourceCodeIsNOTAvailableToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showFunctionsDecompiledCodeInSourceCodeViewerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem decompiledSourceCodeAndCreateTempSourceFilesToolStripMenuItem;
    }
}
