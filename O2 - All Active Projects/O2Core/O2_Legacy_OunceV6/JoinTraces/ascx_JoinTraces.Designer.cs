// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using O2.Kernel.CodeUtils;
using O2.Legacy.OunceV6.TraceViewer;
using O2.Views.ASCX.CoreControls;

namespace O2.Legacy.OunceV6.JoinTraces
{
    partial class ascx_JoinTraces
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
            this.scHost = new System.Windows.Forms.SplitContainer();
            this.scLeft = new System.Windows.Forms.SplitContainer();
            this.laNumberOfTracesProcessed = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.btProcessLoadedFiles = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.tbMaxTracesPerFile = new System.Windows.Forms.TextBox();
            this.cbOnlyProcessTracesWithNoCallers = new System.Windows.Forms.CheckBox();
            this.lbCreatedAssessmentFile = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.btSaveCreatedAssessmentFileIntoFolder = new System.Windows.Forms.Button();
            this.cbLoadCirDumpOnFolderDrop = new System.Windows.Forms.CheckBox();
            this.lbCirFileLoaded = new System.Windows.Forms.Label();
            this.lbTargetSavedAssessmentFiles = new System.Windows.Forms.ListBox();
            this.label8 = new System.Windows.Forms.Label();
            this.ascx_DropObject1 = new O2.Views.ASCX.CoreControls.ascx_DropObject();
            this.label1 = new System.Windows.Forms.Label();
            this.tbFolderToSaveAssessment = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.btCreateTraces = new System.Windows.Forms.Button();
            this.findingsViewerfor_JoinnedTraces = new O2.Views.ASCX.O2Findings.ascx_FindingsViewer();
            this.scRight = new System.Windows.Forms.SplitContainer();
            this.scRawViewAnalysis = new System.Windows.Forms.SplitContainer();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.tbRawDataFilter = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.cbShowRawData = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tvAllTraces = new System.Windows.Forms.TreeView();
            this.splitContainer4 = new System.Windows.Forms.SplitContainer();
            this.cbSourcesView_OnlyShowEdges = new System.Windows.Forms.CheckBox();
            this.cbShowSourcesView = new System.Windows.Forms.CheckBox();
            this.splitContainer5 = new System.Windows.Forms.SplitContainer();
            this.tvSourcesView = new System.Windows.Forms.TreeView();
            this.ascx_TraceViewer1 = new O2.Legacy.OunceV6.TraceViewer.ascx_TraceViewer();
            this.label2 = new System.Windows.Forms.Label();
            this.cbSinksView_OnlyShowEdges = new System.Windows.Forms.CheckBox();
            this.cbShowSinksView = new System.Windows.Forms.CheckBox();
            this.splitContainer6 = new System.Windows.Forms.SplitContainer();
            this.tbSinksViewFilter = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.tvSinksView = new System.Windows.Forms.TreeView();
            this.ascx_TraceViewer2 = new O2.Legacy.OunceV6.TraceViewer.ascx_TraceViewer();
            this.label3 = new System.Windows.Forms.Label();
            this.splitContainer7 = new System.Windows.Forms.SplitContainer();
            this.splitContainer8 = new System.Windows.Forms.SplitContainer();
            this.tvNormalizedTracesView = new System.Windows.Forms.TreeView();
            this.tvProcessedNormalizedTraces = new System.Windows.Forms.TreeView();
            this.ascx_TraceViewer_NormalizedTraces = new O2.Legacy.OunceV6.TraceViewer.ascx_TraceViewer();
            this.btShowNormalizedTracesFor = new System.Windows.Forms.Button();
            this.tbNormalizedTracesFor = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btFindSpringAttributes = new System.Windows.Forms.Button();
            this.cbCalculateSinksView = new System.Windows.Forms.CheckBox();
            this.cbMakeLostSinksIntoSinks = new System.Windows.Forms.CheckBox();
            this.cbCalculateSourcesView = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cbCreateFileWithAllTraces = new System.Windows.Forms.CheckBox();
            this.cbCreateFileWithUniqueTraces = new System.Windows.Forms.CheckBox();
            this.cbDropDuplicateSmartTraces = new System.Windows.Forms.CheckBox();
            this.cbIgnoreRootCallInvocation = new System.Windows.Forms.CheckBox();
            this.cbPreviewCreatedTraces = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cbMapJavaInterfaces = new System.Windows.Forms.CheckBox();
            this.cbSpecialFilter_MapDotNetWebServices = new System.Windows.Forms.CheckBox();
            this.cbAddGluedTracesAsRealTraces = new System.Windows.Forms.CheckBox();
            this.cbAddSuportForDynamicMethodsOnSinks = new System.Windows.Forms.CheckBox();
            this.ascxTraceViewer_JoinnedTraces = new O2.Legacy.OunceV6.TraceViewer.ascx_TraceViewer();
            this.tvTempTreeView = new System.Windows.Forms.TreeView();
            this.tbCreateTracesForKeyword = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.ascx_SelectVisiblePanels1 = new O2.Views.ASCX.CoreControls.ascx_SelectVisiblePanels();
            this.btApplySpecialFilters = new System.Windows.Forms.Button();
            this.scHost.Panel1.SuspendLayout();
            this.scHost.Panel2.SuspendLayout();
            this.scHost.SuspendLayout();
            this.scLeft.Panel1.SuspendLayout();
            this.scLeft.Panel2.SuspendLayout();
            this.scLeft.SuspendLayout();
            this.scRight.Panel1.SuspendLayout();
            this.scRight.Panel2.SuspendLayout();
            this.scRight.SuspendLayout();
            this.scRawViewAnalysis.Panel1.SuspendLayout();
            this.scRawViewAnalysis.Panel2.SuspendLayout();
            this.scRawViewAnalysis.SuspendLayout();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            this.splitContainer4.Panel1.SuspendLayout();
            this.splitContainer4.Panel2.SuspendLayout();
            this.splitContainer4.SuspendLayout();
            this.splitContainer5.Panel1.SuspendLayout();
            this.splitContainer5.Panel2.SuspendLayout();
            this.splitContainer5.SuspendLayout();
            this.splitContainer6.Panel1.SuspendLayout();
            this.splitContainer6.Panel2.SuspendLayout();
            this.splitContainer6.SuspendLayout();
            this.splitContainer7.Panel1.SuspendLayout();
            this.splitContainer7.Panel2.SuspendLayout();
            this.splitContainer7.SuspendLayout();
            this.splitContainer8.Panel1.SuspendLayout();
            this.splitContainer8.Panel2.SuspendLayout();
            this.splitContainer8.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // scHost
            // 
            this.scHost.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.scHost.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.scHost.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.scHost.Location = new System.Drawing.Point(4, 50);
            this.scHost.Name = "scHost";
            // 
            // scHost.Panel1
            // 
            this.scHost.Panel1.Controls.Add(this.scLeft);
            // 
            // scHost.Panel2
            // 
            this.scHost.Panel2.Controls.Add(this.scRight);
            this.scHost.Size = new System.Drawing.Size(1301, 568);
            this.scHost.SplitterDistance = 473;
            this.scHost.TabIndex = 0;
            // 
            // scLeft
            // 
            this.scLeft.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.scLeft.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scLeft.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.scLeft.Location = new System.Drawing.Point(0, 0);
            this.scLeft.Name = "scLeft";
            // 
            // scLeft.Panel1
            // 
            this.scLeft.Panel1.Controls.Add(this.laNumberOfTracesProcessed);
            this.scLeft.Panel1.Controls.Add(this.label14);
            this.scLeft.Panel1.Controls.Add(this.btProcessLoadedFiles);
            this.scLeft.Panel1.Controls.Add(this.label9);
            this.scLeft.Panel1.Controls.Add(this.tbMaxTracesPerFile);
            this.scLeft.Panel1.Controls.Add(this.cbOnlyProcessTracesWithNoCallers);
            this.scLeft.Panel1.Controls.Add(this.lbCreatedAssessmentFile);
            this.scLeft.Panel1.Controls.Add(this.label12);
            this.scLeft.Panel1.Controls.Add(this.btSaveCreatedAssessmentFileIntoFolder);
            this.scLeft.Panel1.Controls.Add(this.cbLoadCirDumpOnFolderDrop);
            this.scLeft.Panel1.Controls.Add(this.lbCirFileLoaded);
            this.scLeft.Panel1.Controls.Add(this.lbTargetSavedAssessmentFiles);
            this.scLeft.Panel1.Controls.Add(this.label8);
            this.scLeft.Panel1.Controls.Add(this.ascx_DropObject1);
            this.scLeft.Panel1.Controls.Add(this.label1);
            this.scLeft.Panel1.Controls.Add(this.tbFolderToSaveAssessment);
            this.scLeft.Panel1.Controls.Add(this.label10);
            this.scLeft.Panel1.Controls.Add(this.btCreateTraces);
            // 
            // scLeft.Panel2
            // 
            this.scLeft.Panel2.Controls.Add(this.findingsViewerfor_JoinnedTraces);
            this.scLeft.Panel2.Paint += new System.Windows.Forms.PaintEventHandler(this.scLeft_Panel2_Paint);
            this.scLeft.Size = new System.Drawing.Size(473, 568);
            this.scLeft.SplitterDistance = 312;
            this.scLeft.TabIndex = 0;
            // 
            // laNumberOfTracesProcessed
            // 
            this.laNumberOfTracesProcessed.AutoSize = true;
            this.laNumberOfTracesProcessed.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.laNumberOfTracesProcessed.Location = new System.Drawing.Point(158, 214);
            this.laNumberOfTracesProcessed.Name = "laNumberOfTracesProcessed";
            this.laNumberOfTracesProcessed.Size = new System.Drawing.Size(15, 13);
            this.laNumberOfTracesProcessed.TabIndex = 44;
            this.laNumberOfTracesProcessed.Text = "..";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(4, 215);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(148, 13);
            this.label14.TabIndex = 43;
            this.label14.Text = "Number of Traces Processed:";
            // 
            // btProcessLoadedFiles
            // 
            this.btProcessLoadedFiles.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.btProcessLoadedFiles.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btProcessLoadedFiles.Location = new System.Drawing.Point(7, 174);
            this.btProcessLoadedFiles.Name = "btProcessLoadedFiles";
            this.btProcessLoadedFiles.Size = new System.Drawing.Size(298, 29);
            this.btProcessLoadedFiles.TabIndex = 42;
            this.btProcessLoadedFiles.Text = "Step 1: Process Loaded Files";
            this.btProcessLoadedFiles.UseVisualStyleBackColor = true;
            this.btProcessLoadedFiles.Click += new System.EventHandler(this.btProcessLoadedFiles_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(6, 520);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(93, 13);
            this.label9.TabIndex = 41;
            this.label9.Text = "Max traces per file";
            // 
            // tbMaxTracesPerFile
            // 
            this.tbMaxTracesPerFile.Location = new System.Drawing.Point(106, 517);
            this.tbMaxTracesPerFile.Name = "tbMaxTracesPerFile";
            this.tbMaxTracesPerFile.Size = new System.Drawing.Size(100, 20);
            this.tbMaxTracesPerFile.TabIndex = 40;
            // 
            // cbOnlyProcessTracesWithNoCallers
            // 
            this.cbOnlyProcessTracesWithNoCallers.AutoSize = true;
            this.cbOnlyProcessTracesWithNoCallers.Location = new System.Drawing.Point(12, 485);
            this.cbOnlyProcessTracesWithNoCallers.Name = "cbOnlyProcessTracesWithNoCallers";
            this.cbOnlyProcessTracesWithNoCallers.Size = new System.Drawing.Size(194, 17);
            this.cbOnlyProcessTracesWithNoCallers.TabIndex = 31;
            this.cbOnlyProcessTracesWithNoCallers.Text = "Only Process Traces with no callers";
            this.cbOnlyProcessTracesWithNoCallers.UseVisualStyleBackColor = true;
            // 
            // lbCreatedAssessmentFile
            // 
            this.lbCreatedAssessmentFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lbCreatedAssessmentFile.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbCreatedAssessmentFile.ForeColor = System.Drawing.Color.Red;
            this.lbCreatedAssessmentFile.Location = new System.Drawing.Point(6, 403);
            this.lbCreatedAssessmentFile.Name = "lbCreatedAssessmentFile";
            this.lbCreatedAssessmentFile.Size = new System.Drawing.Size(301, 58);
            this.lbCreatedAssessmentFile.TabIndex = 38;
            this.lbCreatedAssessmentFile.Text = "Created Saved Assessment File:";
            this.lbCreatedAssessmentFile.Visible = false;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(6, 388);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(189, 13);
            this.label12.TabIndex = 37;
            this.label12.Text = "Created Saved Assessment File:";
            // 
            // btSaveCreatedAssessmentFileIntoFolder
            // 
            this.btSaveCreatedAssessmentFileIntoFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btSaveCreatedAssessmentFileIntoFolder.Location = new System.Drawing.Point(200, 363);
            this.btSaveCreatedAssessmentFileIntoFolder.Name = "btSaveCreatedAssessmentFileIntoFolder";
            this.btSaveCreatedAssessmentFileIntoFolder.Size = new System.Drawing.Size(110, 23);
            this.btSaveCreatedAssessmentFileIntoFolder.TabIndex = 39;
            this.btSaveCreatedAssessmentFileIntoFolder.Text = "Copy File to Folder";
            this.btSaveCreatedAssessmentFileIntoFolder.UseVisualStyleBackColor = true;
            this.btSaveCreatedAssessmentFileIntoFolder.Click += new System.EventHandler(this.btSaveCreatedAssessmentFileIntoFolder_Click);
            // 
            // cbLoadCirDumpOnFolderDrop
            // 
            this.cbLoadCirDumpOnFolderDrop.AutoSize = true;
            this.cbLoadCirDumpOnFolderDrop.Location = new System.Drawing.Point(11, 133);
            this.cbLoadCirDumpOnFolderDrop.Name = "cbLoadCirDumpOnFolderDrop";
            this.cbLoadCirDumpOnFolderDrop.Size = new System.Drawing.Size(166, 17);
            this.cbLoadCirDumpOnFolderDrop.TabIndex = 36;
            this.cbLoadCirDumpOnFolderDrop.Text = "Load CirDump on Folder Drop";
            this.cbLoadCirDumpOnFolderDrop.UseVisualStyleBackColor = true;
            // 
            // lbCirFileLoaded
            // 
            this.lbCirFileLoaded.AutoSize = true;
            this.lbCirFileLoaded.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbCirFileLoaded.Location = new System.Drawing.Point(111, 155);
            this.lbCirFileLoaded.Name = "lbCirFileLoaded";
            this.lbCirFileLoaded.Size = new System.Drawing.Size(15, 13);
            this.lbCirFileLoaded.TabIndex = 30;
            this.lbCirFileLoaded.Text = "..";
            // 
            // lbTargetSavedAssessmentFiles
            // 
            this.lbTargetSavedAssessmentFiles.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lbTargetSavedAssessmentFiles.FormattingEnabled = true;
            this.lbTargetSavedAssessmentFiles.Location = new System.Drawing.Point(4, 45);
            this.lbTargetSavedAssessmentFiles.Name = "lbTargetSavedAssessmentFiles";
            this.lbTargetSavedAssessmentFiles.Size = new System.Drawing.Size(298, 82);
            this.lbTargetSavedAssessmentFiles.TabIndex = 24;
            this.lbTargetSavedAssessmentFiles.DoubleClick += new System.EventHandler(this.lbTargetSavedAssessmentFiles_DoubleClick);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(7, 155);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(98, 13);
            this.label8.TabIndex = 29;
            this.label8.Text = "CirData fiile loaded:";
            // 
            // ascx_DropObject1
            // 
            this.ascx_DropObject1.AllowDrop = true;
            this.ascx_DropObject1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ascx_DropObject1.BackColor = System.Drawing.Color.Maroon;
            this.ascx_DropObject1.ForeColor = System.Drawing.Color.White;
            this.ascx_DropObject1.Location = new System.Drawing.Point(129, 7);
            this.ascx_DropObject1.Name = "ascx_DropObject1";
            this.ascx_DropObject1.Size = new System.Drawing.Size(173, 32);
            this.ascx_DropObject1.TabIndex = 23;
            this.ascx_DropObject1.Text = "Drop Ozasmt files Here!!";
            this.ascx_DropObject1.eDnDAction_ObjectDataReceived_Event += new O2.Kernel.CodeUtils.Callbacks.dMethod_Object(this.ascx_DropObject1_eDnDAction_ObjectDataReceived_Event);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(98, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Findings to process";
            // 
            // tbFolderToSaveAssessment
            // 
            this.tbFolderToSaveAssessment.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbFolderToSaveAssessment.Location = new System.Drawing.Point(11, 365);
            this.tbFolderToSaveAssessment.Name = "tbFolderToSaveAssessment";
            this.tbFolderToSaveAssessment.Size = new System.Drawing.Size(183, 20);
            this.tbFolderToSaveAssessment.TabIndex = 35;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(8, 349);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(148, 13);
            this.label10.TabIndex = 34;
            this.label10.Text = "Folder to save assessment file";
            // 
            // btCreateTraces
            // 
            this.btCreateTraces.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.btCreateTraces.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btCreateTraces.Location = new System.Drawing.Point(7, 274);
            this.btCreateTraces.Name = "btCreateTraces";
            this.btCreateTraces.Size = new System.Drawing.Size(298, 29);
            this.btCreateTraces.TabIndex = 26;
            this.btCreateTraces.Text = "Step 2: Create Assessment File WIth ALL Traces";
            this.btCreateTraces.UseVisualStyleBackColor = true;
            this.btCreateTraces.Click += new System.EventHandler(this.btCreateTraces_Click);
            // 
            // findingsViewerfor_JoinnedTraces
            // 
            this.findingsViewerfor_JoinnedTraces.Dock = System.Windows.Forms.DockStyle.Fill;
            this.findingsViewerfor_JoinnedTraces.Location = new System.Drawing.Point(0, 0);
            this.findingsViewerfor_JoinnedTraces.Name = "findingsViewerfor_JoinnedTraces";
            this.findingsViewerfor_JoinnedTraces.Size = new System.Drawing.Size(153, 564);
            this.findingsViewerfor_JoinnedTraces.TabIndex = 0;
            // 
            // scRight
            // 
            this.scRight.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.scRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scRight.Location = new System.Drawing.Point(0, 0);
            this.scRight.Name = "scRight";
            this.scRight.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // scRight.Panel1
            // 
            this.scRight.Panel1.Controls.Add(this.scRawViewAnalysis);
            // 
            // scRight.Panel2
            // 
            this.scRight.Panel2.Controls.Add(this.groupBox3);
            this.scRight.Panel2.Controls.Add(this.groupBox2);
            this.scRight.Panel2.Controls.Add(this.cbPreviewCreatedTraces);
            this.scRight.Panel2.Controls.Add(this.cbAddGluedTracesAsRealTraces);
            this.scRight.Panel2.Controls.Add(this.groupBox1);
            this.scRight.Panel2.Controls.Add(this.ascxTraceViewer_JoinnedTraces);
            this.scRight.Panel2.Controls.Add(this.tvTempTreeView);
            this.scRight.Panel2.Controls.Add(this.tbCreateTracesForKeyword);
            this.scRight.Panel2.Controls.Add(this.label11);
            this.scRight.Size = new System.Drawing.Size(824, 568);
            this.scRight.SplitterDistance = 210;
            this.scRight.TabIndex = 0;
            // 
            // scRawViewAnalysis
            // 
            this.scRawViewAnalysis.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.scRawViewAnalysis.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scRawViewAnalysis.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.scRawViewAnalysis.Location = new System.Drawing.Point(0, 0);
            this.scRawViewAnalysis.Name = "scRawViewAnalysis";
            this.scRawViewAnalysis.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // scRawViewAnalysis.Panel1
            // 
            this.scRawViewAnalysis.Panel1.Controls.Add(this.splitContainer3);
            // 
            // scRawViewAnalysis.Panel2
            // 
            this.scRawViewAnalysis.Panel2.Controls.Add(this.splitContainer7);
            this.scRawViewAnalysis.Panel2.Controls.Add(this.btShowNormalizedTracesFor);
            this.scRawViewAnalysis.Panel2.Controls.Add(this.tbNormalizedTracesFor);
            this.scRawViewAnalysis.Panel2.Controls.Add(this.label5);
            this.scRawViewAnalysis.Size = new System.Drawing.Size(824, 210);
            this.scRawViewAnalysis.SplitterDistance = 166;
            this.scRawViewAnalysis.TabIndex = 1;
            // 
            // splitContainer3
            // 
            this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer3.Location = new System.Drawing.Point(0, 0);
            this.splitContainer3.Name = "splitContainer3";
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.tbRawDataFilter);
            this.splitContainer3.Panel1.Controls.Add(this.label7);
            this.splitContainer3.Panel1.Controls.Add(this.cbShowRawData);
            this.splitContainer3.Panel1.Controls.Add(this.label4);
            this.splitContainer3.Panel1.Controls.Add(this.tvAllTraces);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.splitContainer4);
            this.splitContainer3.Size = new System.Drawing.Size(820, 162);
            this.splitContainer3.SplitterDistance = 220;
            this.splitContainer3.TabIndex = 1;
            // 
            // tbRawDataFilter
            // 
            this.tbRawDataFilter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbRawDataFilter.Location = new System.Drawing.Point(32, 137);
            this.tbRawDataFilter.Name = "tbRawDataFilter";
            this.tbRawDataFilter.Size = new System.Drawing.Size(185, 20);
            this.tbRawDataFilter.TabIndex = 5;
            this.tbRawDataFilter.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbRawDataFilter_KeyPress);
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(1, 139);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(26, 13);
            this.label7.TabIndex = 4;
            this.label7.Text = "filter";
            // 
            // cbShowRawData
            // 
            this.cbShowRawData.AutoSize = true;
            this.cbShowRawData.Location = new System.Drawing.Point(65, 6);
            this.cbShowRawData.Name = "cbShowRawData";
            this.cbShowRawData.Size = new System.Drawing.Size(123, 17);
            this.cbShowRawData.TabIndex = 3;
            this.cbShowRawData.Text = "Show raw data View";
            this.cbShowRawData.UseVisualStyleBackColor = true;
            this.cbShowRawData.CheckedChanged += new System.EventHandler(this.cbShowRawData_CheckedChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 6);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(55, 13);
            this.label4.TabIndex = 2;
            this.label4.Text = "Raw Data";
            // 
            // tvAllTraces
            // 
            this.tvAllTraces.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tvAllTraces.HideSelection = false;
            this.tvAllTraces.Location = new System.Drawing.Point(4, 24);
            this.tvAllTraces.Name = "tvAllTraces";
            this.tvAllTraces.Size = new System.Drawing.Size(213, 108);
            this.tvAllTraces.TabIndex = 0;
            this.tvAllTraces.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterSelect);
            // 
            // splitContainer4
            // 
            this.splitContainer4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer4.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer4.Location = new System.Drawing.Point(0, 0);
            this.splitContainer4.Name = "splitContainer4";
            this.splitContainer4.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer4.Panel1
            // 
            this.splitContainer4.Panel1.Controls.Add(this.cbSourcesView_OnlyShowEdges);
            this.splitContainer4.Panel1.Controls.Add(this.cbShowSourcesView);
            this.splitContainer4.Panel1.Controls.Add(this.splitContainer5);
            this.splitContainer4.Panel1.Controls.Add(this.label2);
            // 
            // splitContainer4.Panel2
            // 
            this.splitContainer4.Panel2.Controls.Add(this.cbSinksView_OnlyShowEdges);
            this.splitContainer4.Panel2.Controls.Add(this.cbShowSinksView);
            this.splitContainer4.Panel2.Controls.Add(this.splitContainer6);
            this.splitContainer4.Panel2.Controls.Add(this.label3);
            this.splitContainer4.Size = new System.Drawing.Size(596, 162);
            this.splitContainer4.SplitterDistance = 25;
            this.splitContainer4.TabIndex = 0;
            // 
            // cbSourcesView_OnlyShowEdges
            // 
            this.cbSourcesView_OnlyShowEdges.AutoSize = true;
            this.cbSourcesView_OnlyShowEdges.Location = new System.Drawing.Point(210, 4);
            this.cbSourcesView_OnlyShowEdges.Name = "cbSourcesView_OnlyShowEdges";
            this.cbSourcesView_OnlyShowEdges.Size = new System.Drawing.Size(372, 17);
            this.cbSourcesView_OnlyShowEdges.TabIndex = 5;
            this.cbSourcesView_OnlyShowEdges.Text = "Only Show Edges (Traces that start on a method that has makes no calls)";
            this.cbSourcesView_OnlyShowEdges.UseVisualStyleBackColor = true;
            this.cbSourcesView_OnlyShowEdges.CheckedChanged += new System.EventHandler(this.cbSourcesView_OnlyShowEdges_CheckedChanged);
            // 
            // cbShowSourcesView
            // 
            this.cbShowSourcesView.AutoSize = true;
            this.cbShowSourcesView.Location = new System.Drawing.Point(82, 3);
            this.cbShowSourcesView.Name = "cbShowSourcesView";
            this.cbShowSourcesView.Size = new System.Drawing.Size(121, 17);
            this.cbShowSourcesView.TabIndex = 4;
            this.cbShowSourcesView.Text = "Show Sources View";
            this.cbShowSourcesView.UseVisualStyleBackColor = true;
            this.cbShowSourcesView.CheckedChanged += new System.EventHandler(this.cbShowSourcesView_CheckedChanged);
            // 
            // splitContainer5
            // 
            this.splitContainer5.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer5.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer5.Location = new System.Drawing.Point(3, 23);
            this.splitContainer5.Name = "splitContainer5";
            // 
            // splitContainer5.Panel1
            // 
            this.splitContainer5.Panel1.Controls.Add(this.tvSourcesView);
            // 
            // splitContainer5.Panel2
            // 
            this.splitContainer5.Panel2.Controls.Add(this.ascx_TraceViewer1);
            this.splitContainer5.Size = new System.Drawing.Size(586, 43);
            this.splitContainer5.SplitterDistance = 280;
            this.splitContainer5.TabIndex = 2;
            // 
            // tvSourcesView
            // 
            this.tvSourcesView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvSourcesView.HideSelection = false;
            this.tvSourcesView.Location = new System.Drawing.Point(0, 0);
            this.tvSourcesView.Name = "tvSourcesView";
            this.tvSourcesView.Size = new System.Drawing.Size(276, 39);
            this.tvSourcesView.TabIndex = 0;
            this.tvSourcesView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvSourcesView_AfterSelect);
            // 
            // ascx_TraceViewer1
            // 
            this.ascx_TraceViewer1.BackColor = System.Drawing.SystemColors.Control;
            this.ascx_TraceViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ascx_TraceViewer1.ForeColor = System.Drawing.Color.Black;
            this.ascx_TraceViewer1.Location = new System.Drawing.Point(0, 0);
            this.ascx_TraceViewer1.Name = "ascx_TraceViewer1";
            this.ascx_TraceViewer1.Size = new System.Drawing.Size(298, 39);
            this.ascx_TraceViewer1.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(4, 5);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Sources View";
            // 
            // cbSinksView_OnlyShowEdges
            // 
            this.cbSinksView_OnlyShowEdges.AutoSize = true;
            this.cbSinksView_OnlyShowEdges.Location = new System.Drawing.Point(186, 4);
            this.cbSinksView_OnlyShowEdges.Name = "cbSinksView_OnlyShowEdges";
            this.cbSinksView_OnlyShowEdges.Size = new System.Drawing.Size(350, 17);
            this.cbSinksView_OnlyShowEdges.TabIndex = 6;
            this.cbSinksView_OnlyShowEdges.Text = "Only Show Edges (Traces that start on a method that has no callees)";
            this.cbSinksView_OnlyShowEdges.UseVisualStyleBackColor = true;
            this.cbSinksView_OnlyShowEdges.CheckedChanged += new System.EventHandler(this.cbSinksView_OnlyShowEdges_CheckedChanged);
            // 
            // cbShowSinksView
            // 
            this.cbShowSinksView.AutoSize = true;
            this.cbShowSinksView.Location = new System.Drawing.Point(72, 3);
            this.cbShowSinksView.Name = "cbShowSinksView";
            this.cbShowSinksView.Size = new System.Drawing.Size(108, 17);
            this.cbShowSinksView.TabIndex = 5;
            this.cbShowSinksView.Text = "Show Sinks View";
            this.cbShowSinksView.UseVisualStyleBackColor = true;
            this.cbShowSinksView.CheckedChanged += new System.EventHandler(this.cbShowSinksView_CheckedChanged);
            // 
            // splitContainer6
            // 
            this.splitContainer6.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer6.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer6.Location = new System.Drawing.Point(-2, 20);
            this.splitContainer6.Name = "splitContainer6";
            // 
            // splitContainer6.Panel1
            // 
            this.splitContainer6.Panel1.Controls.Add(this.tbSinksViewFilter);
            this.splitContainer6.Panel1.Controls.Add(this.label6);
            this.splitContainer6.Panel1.Controls.Add(this.tvSinksView);
            // 
            // splitContainer6.Panel2
            // 
            this.splitContainer6.Panel2.Controls.Add(this.ascx_TraceViewer2);
            this.splitContainer6.Size = new System.Drawing.Size(598, 111);
            this.splitContainer6.SplitterDistance = 195;
            this.splitContainer6.TabIndex = 2;
            // 
            // tbSinksViewFilter
            // 
            this.tbSinksViewFilter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbSinksViewFilter.Location = new System.Drawing.Point(34, 84);
            this.tbSinksViewFilter.Name = "tbSinksViewFilter";
            this.tbSinksViewFilter.Size = new System.Drawing.Size(153, 20);
            this.tbSinksViewFilter.TabIndex = 2;
            this.tbSinksViewFilter.TextChanged += new System.EventHandler(this.tbSinksViewFilter_TextChanged);
            this.tbSinksViewFilter.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbSinksViewFilter_KeyPress);
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(2, 86);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(26, 13);
            this.label6.TabIndex = 1;
            this.label6.Text = "filter";
            // 
            // tvSinksView
            // 
            this.tvSinksView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tvSinksView.HideSelection = false;
            this.tvSinksView.Location = new System.Drawing.Point(5, 0);
            this.tvSinksView.Name = "tvSinksView";
            this.tvSinksView.Size = new System.Drawing.Size(186, 79);
            this.tvSinksView.TabIndex = 0;
            this.tvSinksView.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(this.tvSinksView_BeforeExpand);
            this.tvSinksView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvSinksView_AfterSelect);
            // 
            // ascx_TraceViewer2
            // 
            this.ascx_TraceViewer2.BackColor = System.Drawing.SystemColors.Control;
            this.ascx_TraceViewer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ascx_TraceViewer2.ForeColor = System.Drawing.Color.Black;
            this.ascx_TraceViewer2.Location = new System.Drawing.Point(0, 0);
            this.ascx_TraceViewer2.Name = "ascx_TraceViewer2";
            this.ascx_TraceViewer2.Size = new System.Drawing.Size(395, 107);
            this.ascx_TraceViewer2.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 4);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "Sinks View";
            // 
            // splitContainer7
            // 
            this.splitContainer7.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer7.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer7.Location = new System.Drawing.Point(6, 34);
            this.splitContainer7.Name = "splitContainer7";
            // 
            // splitContainer7.Panel1
            // 
            this.splitContainer7.Panel1.Controls.Add(this.splitContainer8);
            // 
            // splitContainer7.Panel2
            // 
            this.splitContainer7.Panel2.Controls.Add(this.ascx_TraceViewer_NormalizedTraces);
            this.splitContainer7.Size = new System.Drawing.Size(809, 0);
            this.splitContainer7.SplitterDistance = 514;
            this.splitContainer7.TabIndex = 7;
            // 
            // splitContainer8
            // 
            this.splitContainer8.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer8.Location = new System.Drawing.Point(0, 0);
            this.splitContainer8.Name = "splitContainer8";
            this.splitContainer8.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer8.Panel1
            // 
            this.splitContainer8.Panel1.Controls.Add(this.tvNormalizedTracesView);
            // 
            // splitContainer8.Panel2
            // 
            this.splitContainer8.Panel2.Controls.Add(this.tvProcessedNormalizedTraces);
            this.splitContainer8.Size = new System.Drawing.Size(514, 0);
            this.splitContainer8.SplitterDistance = 25;
            this.splitContainer8.TabIndex = 5;
            // 
            // tvNormalizedTracesView
            // 
            this.tvNormalizedTracesView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvNormalizedTracesView.HideSelection = false;
            this.tvNormalizedTracesView.Location = new System.Drawing.Point(0, 0);
            this.tvNormalizedTracesView.Name = "tvNormalizedTracesView";
            this.tvNormalizedTracesView.Size = new System.Drawing.Size(510, 21);
            this.tvNormalizedTracesView.TabIndex = 4;
            this.tvNormalizedTracesView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvNormalizedTracesView_AfterSelect);
            // 
            // tvProcessedNormalizedTraces
            // 
            this.tvProcessedNormalizedTraces.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvProcessedNormalizedTraces.HideSelection = false;
            this.tvProcessedNormalizedTraces.Location = new System.Drawing.Point(0, 0);
            this.tvProcessedNormalizedTraces.Name = "tvProcessedNormalizedTraces";
            this.tvProcessedNormalizedTraces.Size = new System.Drawing.Size(510, 21);
            this.tvProcessedNormalizedTraces.TabIndex = 5;
            this.tvProcessedNormalizedTraces.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvProcessedNormalizedTraces_AfterSelect);
            // 
            // ascx_TraceViewer_NormalizedTraces
            // 
            this.ascx_TraceViewer_NormalizedTraces.BackColor = System.Drawing.SystemColors.Control;
            this.ascx_TraceViewer_NormalizedTraces.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ascx_TraceViewer_NormalizedTraces.ForeColor = System.Drawing.Color.Black;
            this.ascx_TraceViewer_NormalizedTraces.Location = new System.Drawing.Point(0, 0);
            this.ascx_TraceViewer_NormalizedTraces.Name = "ascx_TraceViewer_NormalizedTraces";
            this.ascx_TraceViewer_NormalizedTraces.Size = new System.Drawing.Size(25, 0);
            this.ascx_TraceViewer_NormalizedTraces.TabIndex = 3;
            // 
            // btShowNormalizedTracesFor
            // 
            this.btShowNormalizedTracesFor.Location = new System.Drawing.Point(381, 4);
            this.btShowNormalizedTracesFor.Name = "btShowNormalizedTracesFor";
            this.btShowNormalizedTracesFor.Size = new System.Drawing.Size(143, 23);
            this.btShowNormalizedTracesFor.TabIndex = 6;
            this.btShowNormalizedTracesFor.Text = "Show Normalized Traces";
            this.btShowNormalizedTracesFor.UseVisualStyleBackColor = true;
            this.btShowNormalizedTracesFor.Click += new System.EventHandler(this.btShowNormalizedTracesFor_Click);
            // 
            // tbNormalizedTracesFor
            // 
            this.tbNormalizedTracesFor.Location = new System.Drawing.Point(119, 8);
            this.tbNormalizedTracesFor.Name = "tbNormalizedTracesFor";
            this.tbNormalizedTracesFor.Size = new System.Drawing.Size(241, 20);
            this.tbNormalizedTracesFor.TabIndex = 5;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 9);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(110, 13);
            this.label5.TabIndex = 3;
            this.label5.Text = "Normalized Traces for";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.cbCalculateSinksView);
            this.groupBox3.Controls.Add(this.cbMakeLostSinksIntoSinks);
            this.groupBox3.Controls.Add(this.cbCalculateSourcesView);
            this.groupBox3.Location = new System.Drawing.Point(494, 16);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(233, 171);
            this.groupBox3.TabIndex = 41;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Development Tests";
            // 
            // btFindSpringAttributes
            // 
            this.btFindSpringAttributes.Location = new System.Drawing.Point(144, 21);
            this.btFindSpringAttributes.Name = "btFindSpringAttributes";
            this.btFindSpringAttributes.Size = new System.Drawing.Size(102, 35);
            this.btFindSpringAttributes.TabIndex = 35;
            this.btFindSpringAttributes.Text = "Step 1b: Find Spring Attributes";
            this.btFindSpringAttributes.UseVisualStyleBackColor = true;
            this.btFindSpringAttributes.Click += new System.EventHandler(this.btFindSpringAttributes_Click);
            // 
            // cbCalculateSinksView
            // 
            this.cbCalculateSinksView.AutoSize = true;
            this.cbCalculateSinksView.Location = new System.Drawing.Point(9, 47);
            this.cbCalculateSinksView.Name = "cbCalculateSinksView";
            this.cbCalculateSinksView.Size = new System.Drawing.Size(125, 17);
            this.cbCalculateSinksView.TabIndex = 27;
            this.cbCalculateSinksView.Text = "Calculate Sinks View";
            this.cbCalculateSinksView.UseVisualStyleBackColor = true;
            this.cbCalculateSinksView.Visible = false;
            this.cbCalculateSinksView.CheckedChanged += new System.EventHandler(this.cbCalculateSinksView_CheckedChanged);
            // 
            // cbMakeLostSinksIntoSinks
            // 
            this.cbMakeLostSinksIntoSinks.AutoSize = true;
            this.cbMakeLostSinksIntoSinks.Checked = true;
            this.cbMakeLostSinksIntoSinks.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbMakeLostSinksIntoSinks.Location = new System.Drawing.Point(9, 119);
            this.cbMakeLostSinksIntoSinks.Name = "cbMakeLostSinksIntoSinks";
            this.cbMakeLostSinksIntoSinks.Size = new System.Drawing.Size(151, 17);
            this.cbMakeLostSinksIntoSinks.TabIndex = 34;
            this.cbMakeLostSinksIntoSinks.Text = "Make LostSinks into Sinks";
            this.cbMakeLostSinksIntoSinks.UseVisualStyleBackColor = true;
            // 
            // cbCalculateSourcesView
            // 
            this.cbCalculateSourcesView.AutoSize = true;
            this.cbCalculateSourcesView.Location = new System.Drawing.Point(9, 69);
            this.cbCalculateSourcesView.Name = "cbCalculateSourcesView";
            this.cbCalculateSourcesView.Size = new System.Drawing.Size(138, 17);
            this.cbCalculateSourcesView.TabIndex = 28;
            this.cbCalculateSourcesView.Text = "Calculate Sources View";
            this.cbCalculateSourcesView.UseVisualStyleBackColor = true;
            this.cbCalculateSourcesView.Visible = false;
            this.cbCalculateSourcesView.CheckedChanged += new System.EventHandler(this.cbCalculateSourcesView_CheckedChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cbCreateFileWithAllTraces);
            this.groupBox2.Controls.Add(this.cbCreateFileWithUniqueTraces);
            this.groupBox2.Controls.Add(this.cbDropDuplicateSmartTraces);
            this.groupBox2.Controls.Add(this.cbIgnoreRootCallInvocation);
            this.groupBox2.Location = new System.Drawing.Point(276, 34);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(212, 97);
            this.groupBox2.TabIndex = 40;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Create 1 file with ALL filtered findings";
            // 
            // cbCreateFileWithAllTraces
            // 
            this.cbCreateFileWithAllTraces.AutoSize = true;
            this.cbCreateFileWithAllTraces.Checked = true;
            this.cbCreateFileWithAllTraces.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbCreateFileWithAllTraces.Location = new System.Drawing.Point(7, 33);
            this.cbCreateFileWithAllTraces.Name = "cbCreateFileWithAllTraces";
            this.cbCreateFileWithAllTraces.Size = new System.Drawing.Size(159, 17);
            this.cbCreateFileWithAllTraces.TabIndex = 35;
            this.cbCreateFileWithAllTraces.Text = "Create File With ALL Traces";
            this.cbCreateFileWithAllTraces.UseVisualStyleBackColor = true;
            // 
            // cbCreateFileWithUniqueTraces
            // 
            this.cbCreateFileWithUniqueTraces.AutoSize = true;
            this.cbCreateFileWithUniqueTraces.Location = new System.Drawing.Point(7, 16);
            this.cbCreateFileWithUniqueTraces.Name = "cbCreateFileWithUniqueTraces";
            this.cbCreateFileWithUniqueTraces.Size = new System.Drawing.Size(174, 17);
            this.cbCreateFileWithUniqueTraces.TabIndex = 32;
            this.cbCreateFileWithUniqueTraces.Text = "Create File With Unique Traces";
            this.cbCreateFileWithUniqueTraces.UseVisualStyleBackColor = true;
            // 
            // cbDropDuplicateSmartTraces
            // 
            this.cbDropDuplicateSmartTraces.AutoSize = true;
            this.cbDropDuplicateSmartTraces.Location = new System.Drawing.Point(7, 50);
            this.cbDropDuplicateSmartTraces.Name = "cbDropDuplicateSmartTraces";
            this.cbDropDuplicateSmartTraces.Size = new System.Drawing.Size(160, 17);
            this.cbDropDuplicateSmartTraces.TabIndex = 33;
            this.cbDropDuplicateSmartTraces.Text = "Drop Duplicate SmartTraces";
            this.cbDropDuplicateSmartTraces.UseVisualStyleBackColor = true;
            // 
            // cbIgnoreRootCallInvocation
            // 
            this.cbIgnoreRootCallInvocation.AutoSize = true;
            this.cbIgnoreRootCallInvocation.Location = new System.Drawing.Point(7, 69);
            this.cbIgnoreRootCallInvocation.Name = "cbIgnoreRootCallInvocation";
            this.cbIgnoreRootCallInvocation.Size = new System.Drawing.Size(155, 17);
            this.cbIgnoreRootCallInvocation.TabIndex = 34;
            this.cbIgnoreRootCallInvocation.Text = "Ignore Root Call Invocation";
            this.cbIgnoreRootCallInvocation.UseVisualStyleBackColor = true;
            // 
            // cbPreviewCreatedTraces
            // 
            this.cbPreviewCreatedTraces.AutoSize = true;
            this.cbPreviewCreatedTraces.Location = new System.Drawing.Point(8, 164);
            this.cbPreviewCreatedTraces.Name = "cbPreviewCreatedTraces";
            this.cbPreviewCreatedTraces.Size = new System.Drawing.Size(140, 17);
            this.cbPreviewCreatedTraces.TabIndex = 34;
            this.cbPreviewCreatedTraces.Text = "Preview Created Traces";
            this.cbPreviewCreatedTraces.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btFindSpringAttributes);
            this.groupBox1.Controls.Add(this.btApplySpecialFilters);
            this.groupBox1.Controls.Add(this.cbMapJavaInterfaces);
            this.groupBox1.Controls.Add(this.cbSpecialFilter_MapDotNetWebServices);
            this.groupBox1.Controls.Add(this.cbAddSuportForDynamicMethodsOnSinks);
            this.groupBox1.Location = new System.Drawing.Point(8, 29);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(257, 129);
            this.groupBox1.TabIndex = 36;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Special Filters";
            // 
            // cbMapJavaInterfaces
            // 
            this.cbMapJavaInterfaces.AutoSize = true;
            this.cbMapJavaInterfaces.Location = new System.Drawing.Point(13, 89);
            this.cbMapJavaInterfaces.Name = "cbMapJavaInterfaces";
            this.cbMapJavaInterfaces.Size = new System.Drawing.Size(126, 17);
            this.cbMapJavaInterfaces.TabIndex = 30;
            this.cbMapJavaInterfaces.Text = "Java: Map Interfaces";
            this.cbMapJavaInterfaces.UseVisualStyleBackColor = true;
            // 
            // cbSpecialFilter_MapDotNetWebServices
            // 
            this.cbSpecialFilter_MapDotNetWebServices.AutoSize = true;
            this.cbSpecialFilter_MapDotNetWebServices.Location = new System.Drawing.Point(13, 106);
            this.cbSpecialFilter_MapDotNetWebServices.Name = "cbSpecialFilter_MapDotNetWebServices";
            this.cbSpecialFilter_MapDotNetWebServices.Size = new System.Drawing.Size(148, 17);
            this.cbSpecialFilter_MapDotNetWebServices.TabIndex = 32;
            this.cbSpecialFilter_MapDotNetWebServices.Text = ".NET : Map WebServices";
            this.cbSpecialFilter_MapDotNetWebServices.UseVisualStyleBackColor = true;
            // 
            // cbAddGluedTracesAsRealTraces
            // 
            this.cbAddGluedTracesAsRealTraces.AutoSize = true;
            this.cbAddGluedTracesAsRealTraces.Checked = true;
            this.cbAddGluedTracesAsRealTraces.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbAddGluedTracesAsRealTraces.Location = new System.Drawing.Point(283, 141);
            this.cbAddGluedTracesAsRealTraces.Name = "cbAddGluedTracesAsRealTraces";
            this.cbAddGluedTracesAsRealTraces.Size = new System.Drawing.Size(178, 17);
            this.cbAddGluedTracesAsRealTraces.TabIndex = 31;
            this.cbAddGluedTracesAsRealTraces.Text = "Add Glued traces as \'real\' traces";
            this.cbAddGluedTracesAsRealTraces.UseVisualStyleBackColor = true;
            this.cbAddGluedTracesAsRealTraces.CheckedChanged += new System.EventHandler(this.cbAddGluedTracesAsRealTraces_CheckedChanged);
            // 
            // cbAddSuportForDynamicMethodsOnSinks
            // 
            this.cbAddSuportForDynamicMethodsOnSinks.AutoSize = true;
            this.cbAddSuportForDynamicMethodsOnSinks.Location = new System.Drawing.Point(13, 71);
            this.cbAddSuportForDynamicMethodsOnSinks.Name = "cbAddSuportForDynamicMethodsOnSinks";
            this.cbAddSuportForDynamicMethodsOnSinks.Size = new System.Drawing.Size(210, 17);
            this.cbAddSuportForDynamicMethodsOnSinks.TabIndex = 33;
            this.cbAddSuportForDynamicMethodsOnSinks.Text = "Java: add  Dynamic Methods On Sinks";
            this.cbAddSuportForDynamicMethodsOnSinks.UseVisualStyleBackColor = true;
            // 
            // ascxTraceViewer_JoinnedTraces
            // 
            this.ascxTraceViewer_JoinnedTraces.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ascxTraceViewer_JoinnedTraces.BackColor = System.Drawing.SystemColors.Control;
            this.ascxTraceViewer_JoinnedTraces.ForeColor = System.Drawing.Color.Black;
            this.ascxTraceViewer_JoinnedTraces.Location = new System.Drawing.Point(276, 202);
            this.ascxTraceViewer_JoinnedTraces.Name = "ascxTraceViewer_JoinnedTraces";
            this.ascxTraceViewer_JoinnedTraces.Size = new System.Drawing.Size(541, 147);
            this.ascxTraceViewer_JoinnedTraces.TabIndex = 30;
            // 
            // tvTempTreeView
            // 
            this.tvTempTreeView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.tvTempTreeView.Location = new System.Drawing.Point(6, 189);
            this.tvTempTreeView.Name = "tvTempTreeView";
            this.tvTempTreeView.Size = new System.Drawing.Size(264, 155);
            this.tvTempTreeView.TabIndex = 29;
            this.tvTempTreeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvTempTreeView_AfterSelect);
            // 
            // tbCreateTracesForKeyword
            // 
            this.tbCreateTracesForKeyword.Location = new System.Drawing.Point(158, 3);
            this.tbCreateTracesForKeyword.Name = "tbCreateTracesForKeyword";
            this.tbCreateTracesForKeyword.Size = new System.Drawing.Size(100, 20);
            this.tbCreateTracesForKeyword.TabIndex = 28;
            this.tbCreateTracesForKeyword.TextChanged += new System.EventHandler(this.tbCreateTracesForKeyword_TextChanged);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(5, 6);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(138, 13);
            this.label11.TabIndex = 27;
            this.label11.Text = "Create Traces for (keyword)";
            // 
            // ascx_SelectVisiblePanels1
            // 
            this.ascx_SelectVisiblePanels1.BackColor = System.Drawing.SystemColors.Control;
            this.ascx_SelectVisiblePanels1.ForeColor = System.Drawing.Color.Black;
            this.ascx_SelectVisiblePanels1.Location = new System.Drawing.Point(4, 4);
            this.ascx_SelectVisiblePanels1.Name = "ascx_SelectVisiblePanels1";
            this.ascx_SelectVisiblePanels1.Size = new System.Drawing.Size(570, 40);
            this.ascx_SelectVisiblePanels1.TabIndex = 2;
            // 
            // btApplySpecialFilters
            // 
            this.btApplySpecialFilters.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.btApplySpecialFilters.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btApplySpecialFilters.Location = new System.Drawing.Point(6, 19);
            this.btApplySpecialFilters.Name = "btApplySpecialFilters";
            this.btApplySpecialFilters.Size = new System.Drawing.Size(134, 37);
            this.btApplySpecialFilters.TabIndex = 45;
            this.btApplySpecialFilters.Text = "Step 1a: Apply Special Filters";
            this.btApplySpecialFilters.UseVisualStyleBackColor = true;
            this.btApplySpecialFilters.Click += new System.EventHandler(this.btApplySpecialFilters_Click);
            // 
            // ascx_JoinTraces
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.ascx_SelectVisiblePanels1);
            this.Controls.Add(this.scHost);
            this.Name = "ascx_JoinTraces";
            this.Size = new System.Drawing.Size(1308, 621);
            this.Load += new System.EventHandler(this.ascx_JoinTraces_Load);
            this.scHost.Panel1.ResumeLayout(false);
            this.scHost.Panel2.ResumeLayout(false);
            this.scHost.ResumeLayout(false);
            this.scLeft.Panel1.ResumeLayout(false);
            this.scLeft.Panel1.PerformLayout();
            this.scLeft.Panel2.ResumeLayout(false);
            this.scLeft.ResumeLayout(false);
            this.scRight.Panel1.ResumeLayout(false);
            this.scRight.Panel2.ResumeLayout(false);
            this.scRight.Panel2.PerformLayout();
            this.scRight.ResumeLayout(false);
            this.scRawViewAnalysis.Panel1.ResumeLayout(false);
            this.scRawViewAnalysis.Panel2.ResumeLayout(false);
            this.scRawViewAnalysis.Panel2.PerformLayout();
            this.scRawViewAnalysis.ResumeLayout(false);
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel1.PerformLayout();
            this.splitContainer3.Panel2.ResumeLayout(false);
            this.splitContainer3.ResumeLayout(false);
            this.splitContainer4.Panel1.ResumeLayout(false);
            this.splitContainer4.Panel1.PerformLayout();
            this.splitContainer4.Panel2.ResumeLayout(false);
            this.splitContainer4.Panel2.PerformLayout();
            this.splitContainer4.ResumeLayout(false);
            this.splitContainer5.Panel1.ResumeLayout(false);
            this.splitContainer5.Panel2.ResumeLayout(false);
            this.splitContainer5.ResumeLayout(false);
            this.splitContainer6.Panel1.ResumeLayout(false);
            this.splitContainer6.Panel1.PerformLayout();
            this.splitContainer6.Panel2.ResumeLayout(false);
            this.splitContainer6.ResumeLayout(false);
            this.splitContainer7.Panel1.ResumeLayout(false);
            this.splitContainer7.Panel2.ResumeLayout(false);
            this.splitContainer7.ResumeLayout(false);
            this.splitContainer8.Panel1.ResumeLayout(false);
            this.splitContainer8.Panel2.ResumeLayout(false);
            this.splitContainer8.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer scHost;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox lbTargetSavedAssessmentFiles;
        private ascx_DropObject ascx_DropObject1;
        private System.Windows.Forms.SplitContainer scRawViewAnalysis;
        private System.Windows.Forms.TreeView tvAllTraces;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private System.Windows.Forms.SplitContainer splitContainer4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TreeView tvSourcesView;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TreeView tvSinksView;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.SplitContainer splitContainer5;
        private ascx_TraceViewer ascx_TraceViewer1;
        private System.Windows.Forms.SplitContainer splitContainer6;
        private ascx_TraceViewer ascx_TraceViewer2;
        private System.Windows.Forms.CheckBox cbShowRawData;
        private System.Windows.Forms.CheckBox cbShowSourcesView;
        private System.Windows.Forms.CheckBox cbShowSinksView;
        private System.Windows.Forms.Button btShowNormalizedTracesFor;
        private System.Windows.Forms.TextBox tbNormalizedTracesFor;
        private System.Windows.Forms.TreeView tvNormalizedTracesView;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox cbSourcesView_OnlyShowEdges;
        private System.Windows.Forms.CheckBox cbSinksView_OnlyShowEdges;
        private System.Windows.Forms.CheckBox cbCalculateSourcesView;
        private System.Windows.Forms.CheckBox cbCalculateSinksView;
        private System.Windows.Forms.TextBox tbSinksViewFilter;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox tbRawDataFilter;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.SplitContainer splitContainer7;
        private ascx_TraceViewer ascx_TraceViewer_NormalizedTraces;
        private System.Windows.Forms.SplitContainer splitContainer8;
        private System.Windows.Forms.TreeView tvProcessedNormalizedTraces;
        private System.Windows.Forms.Label lbCirFileLoaded;
        private System.Windows.Forms.Label label8;
        private ascx_SelectVisiblePanels ascx_SelectVisiblePanels1;
        private System.Windows.Forms.SplitContainer scLeft;
        private System.Windows.Forms.SplitContainer scRight;
        private System.Windows.Forms.Button btCreateTraces;
        private System.Windows.Forms.CheckBox cbMapJavaInterfaces;
        private System.Windows.Forms.TextBox tbCreateTracesForKeyword;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TreeView tvTempTreeView;
        private ascx_TraceViewer ascxTraceViewer_JoinnedTraces;
        private System.Windows.Forms.CheckBox cbOnlyProcessTracesWithNoCallers;
        private System.Windows.Forms.CheckBox cbSpecialFilter_MapDotNetWebServices;
        private System.Windows.Forms.CheckBox cbAddSuportForDynamicMethodsOnSinks;
        private System.Windows.Forms.CheckBox cbAddGluedTracesAsRealTraces;
        private System.Windows.Forms.CheckBox cbMakeLostSinksIntoSinks;
        private System.Windows.Forms.Button btFindSpringAttributes;
        private System.Windows.Forms.TextBox tbFolderToSaveAssessment;
        private System.Windows.Forms.CheckBox cbPreviewCreatedTraces;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lbCreatedAssessmentFile;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button btSaveCreatedAssessmentFileIntoFolder;
        private System.Windows.Forms.CheckBox cbLoadCirDumpOnFolderDrop;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox cbCreateFileWithAllTraces;
        private System.Windows.Forms.CheckBox cbCreateFileWithUniqueTraces;
        private System.Windows.Forms.CheckBox cbDropDuplicateSmartTraces;
        private System.Windows.Forms.CheckBox cbIgnoreRootCallInvocation;
        private O2.Views.ASCX.O2Findings.ascx_FindingsViewer findingsViewerfor_JoinnedTraces;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label laNumberOfTracesProcessed;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Button btProcessLoadedFiles;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox tbMaxTracesPerFile;
        private System.Windows.Forms.Button btApplySpecialFilters;
    }
}
