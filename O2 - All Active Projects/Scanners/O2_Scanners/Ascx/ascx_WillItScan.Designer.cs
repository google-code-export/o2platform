using O2.Views.ASCX.CoreControls;

namespace O2.Scanners.Ascx
{
    partial class ascx_WillItScan
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.llTargetFiles_DeleteSelected = new System.Windows.Forms.LinkLabel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.llDownloadDemoFile_HacmeBank_Website = new System.Windows.Forms.LinkLabel();
            this.llDownloadDemoFile_HacmeBank_WebServices = new System.Windows.Forms.LinkLabel();
            this.label6 = new System.Windows.Forms.Label();
            this.cbDoubleClickToDeleteItem = new System.Windows.Forms.CheckBox();
            this.lbTargetFiles = new System.Windows.Forms.ListBox();
            this.ascx_DropObject1 = new O2.Views.ASCX.CoreControls.ascx_DropObject();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.llRefreshDirectory = new System.Windows.Forms.LinkLabel();
            this.llDeleteSeletedFile = new System.Windows.Forms.LinkLabel();
            this.adManualTestTempFiles = new O2.Views.ASCX.CoreControls.ascx_Directory();
            this.cbDeleteDirectoryContentsRecursive = new System.Windows.Forms.CheckBox();
            this.llDeleteDirectoryContents = new System.Windows.Forms.LinkLabel();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.lbMissingDependencies = new System.Windows.Forms.ListBox();
            this.label8 = new System.Windows.Forms.Label();
            this.ado_AddFilesOrDirectoryToScanBundle = new O2.Views.ASCX.CoreControls.ascx_DropObject();
            this.label7 = new System.Windows.Forms.Label();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cbStoreControlFlowBlockRawDataInsideCirDataFile = new System.Windows.Forms.CheckBox();
            this.btCancelOunceCLIScan = new System.Windows.Forms.Button();
            this.label11 = new System.Windows.Forms.Label();
            this.gbNoRulesScanMode_Manual = new System.Windows.Forms.GroupBox();
            this.tbPathToRawCirDataFiles = new System.Windows.Forms.TextBox();
            this.llRefreshRulesNumber = new System.Windows.Forms.LinkLabel();
            this.llDeleteDatabase = new System.Windows.Forms.LinkLabel();
            this.llPreCirDumps = new System.Windows.Forms.LinkLabel();
            this.llPostCirDumps = new System.Windows.Forms.LinkLabel();
            this.llCreateCirData = new System.Windows.Forms.LinkLabel();
            this.rbScannerMSCatNet = new System.Windows.Forms.RadioButton();
            this.rbScannerOunceCore = new System.Windows.Forms.RadioButton();
            this.btCreateCirDumpWithExistingRules = new System.Windows.Forms.Button();
            this.lbNumberOfRulesInDatabase = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.btScanWithExistingRules = new System.Windows.Forms.Button();
            this.lnDependenciesStatus = new System.Windows.Forms.Label();
            this.cbEnableNoOutOfTheBoxRules = new System.Windows.Forms.CheckBox();
            this.gbNoOutOfTheBoxRules = new System.Windows.Forms.GroupBox();
            this.btCreateO2RulePacks = new System.Windows.Forms.Button();
            this.btCreateCirDumpForSelectedFile = new System.Windows.Forms.Button();
            this.btCreateAssessmentFiles = new System.Windows.Forms.Button();
            this.cbCallBacksOnControlFlowGraphs_And_ExternalSinks = new System.Windows.Forms.CheckBox();
            this.cbCallBacksOnEdges_And_ExternalSinks = new System.Windows.Forms.CheckBox();
            this.cbSourcesAndSinks = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cbAutoAppendTargetName = new System.Windows.Forms.CheckBox();
            this.tbWorkDirectory = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.lbManualTestTargetFile = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.scanProcessBar = new System.Windows.Forms.ProgressBar();
            this.cbShowLogExecutionLog = new System.Windows.Forms.CheckBox();
            this.label10 = new System.Windows.Forms.Label();
            this.rtbLogFileForCurrentScan = new System.Windows.Forms.RichTextBox();
            this.rbScannerAppScanDE = new System.Windows.Forms.RadioButton();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.gbNoRulesScanMode_Manual.SuspendLayout();
            this.gbNoOutOfTheBoxRules.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.llTargetFiles_DeleteSelected);
            this.splitContainer1.Panel1.Controls.Add(this.groupBox1);
            this.splitContainer1.Panel1.Controls.Add(this.label6);
            this.splitContainer1.Panel1.Controls.Add(this.cbDoubleClickToDeleteItem);
            this.splitContainer1.Panel1.Controls.Add(this.lbTargetFiles);
            this.splitContainer1.Panel1.Controls.Add(this.ascx_DropObject1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(1012, 577);
            this.splitContainer1.SplitterDistance = 222;
            this.splitContainer1.TabIndex = 0;
            // 
            // llTargetFiles_DeleteSelected
            // 
            this.llTargetFiles_DeleteSelected.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.llTargetFiles_DeleteSelected.AutoSize = true;
            this.llTargetFiles_DeleteSelected.Location = new System.Drawing.Point(6, 431);
            this.llTargetFiles_DeleteSelected.Name = "llTargetFiles_DeleteSelected";
            this.llTargetFiles_DeleteSelected.Size = new System.Drawing.Size(79, 13);
            this.llTargetFiles_DeleteSelected.TabIndex = 48;
            this.llTargetFiles_DeleteSelected.TabStop = true;
            this.llTargetFiles_DeleteSelected.Text = "delete selected";
            this.llTargetFiles_DeleteSelected.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llTargetFiles_DeleteSelected_LinkClicked);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox1.Controls.Add(this.llDownloadDemoFile_HacmeBank_Website);
            this.groupBox1.Controls.Add(this.llDownloadDemoFile_HacmeBank_WebServices);
            this.groupBox1.Location = new System.Drawing.Point(7, 510);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(206, 60);
            this.groupBox1.TabIndex = 41;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Load demo data:";
            // 
            // llDownloadDemoFile_HacmeBank_Website
            // 
            this.llDownloadDemoFile_HacmeBank_Website.AutoSize = true;
            this.llDownloadDemoFile_HacmeBank_Website.Location = new System.Drawing.Point(9, 37);
            this.llDownloadDemoFile_HacmeBank_Website.Name = "llDownloadDemoFile_HacmeBank_Website";
            this.llDownloadDemoFile_HacmeBank_Website.Size = new System.Drawing.Size(146, 13);
            this.llDownloadDemoFile_HacmeBank_Website.TabIndex = 1;
            this.llDownloadDemoFile_HacmeBank_Website.TabStop = true;
            this.llDownloadDemoFile_HacmeBank_Website.Text = "HacmeBank  - WebSite (.net)";
            this.llDownloadDemoFile_HacmeBank_Website.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llDownloadDemoFile_HacmeBank_Website_LinkClicked);
            // 
            // llDownloadDemoFile_HacmeBank_WebServices
            // 
            this.llDownloadDemoFile_HacmeBank_WebServices.AutoSize = true;
            this.llDownloadDemoFile_HacmeBank_WebServices.Location = new System.Drawing.Point(9, 16);
            this.llDownloadDemoFile_HacmeBank_WebServices.Name = "llDownloadDemoFile_HacmeBank_WebServices";
            this.llDownloadDemoFile_HacmeBank_WebServices.Size = new System.Drawing.Size(166, 13);
            this.llDownloadDemoFile_HacmeBank_WebServices.TabIndex = 0;
            this.llDownloadDemoFile_HacmeBank_WebServices.TabStop = true;
            this.llDownloadDemoFile_HacmeBank_WebServices.Text = "HacmeBank - WebServices (.net)";
            this.llDownloadDemoFile_HacmeBank_WebServices.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llDownloadDemoFile_HacmeBank_WebServices_LinkClicked);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(6, 78);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(121, 13);
            this.label6.TabIndex = 28;
            this.label6.Text = "Select Scan Target:";
            // 
            // cbDoubleClickToDeleteItem
            // 
            this.cbDoubleClickToDeleteItem.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cbDoubleClickToDeleteItem.AutoSize = true;
            this.cbDoubleClickToDeleteItem.Location = new System.Drawing.Point(9, 447);
            this.cbDoubleClickToDeleteItem.Name = "cbDoubleClickToDeleteItem";
            this.cbDoubleClickToDeleteItem.Size = new System.Drawing.Size(160, 17);
            this.cbDoubleClickToDeleteItem.TabIndex = 27;
            this.cbDoubleClickToDeleteItem.Text = "Double Click to delete target";
            this.cbDoubleClickToDeleteItem.UseVisualStyleBackColor = true;
            // 
            // lbTargetFiles
            // 
            this.lbTargetFiles.AllowDrop = true;
            this.lbTargetFiles.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lbTargetFiles.FormattingEnabled = true;
            this.lbTargetFiles.Location = new System.Drawing.Point(7, 99);
            this.lbTargetFiles.Name = "lbTargetFiles";
            this.lbTargetFiles.Size = new System.Drawing.Size(206, 329);
            this.lbTargetFiles.TabIndex = 26;
            this.lbTargetFiles.SelectedIndexChanged += new System.EventHandler(this.lbTargetFiles_SelectedIndexChanged);
            this.lbTargetFiles.DragDrop += new System.Windows.Forms.DragEventHandler(this.lbTargetFiles_DragDrop);
            this.lbTargetFiles.DoubleClick += new System.EventHandler(this.lbTargetFiles_DoubleClick);
            this.lbTargetFiles.DragEnter += new System.Windows.Forms.DragEventHandler(this.lbTargetFiles_DragEnter);
            // 
            // ascx_DropObject1
            // 
            this.ascx_DropObject1.AllowDrop = true;
            this.ascx_DropObject1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ascx_DropObject1.BackColor = System.Drawing.Color.Maroon;
            this.ascx_DropObject1.ForeColor = System.Drawing.Color.White;
            this.ascx_DropObject1.Location = new System.Drawing.Point(9, 4);
            this.ascx_DropObject1.Name = "ascx_DropObject1";
            this.ascx_DropObject1.Size = new System.Drawing.Size(206, 67);
            this.ascx_DropObject1.TabIndex = 25;
            this.ascx_DropObject1.Text = "Drop Content Here!!";
            this.ascx_DropObject1.eDnDAction_ObjectDataReceived_Event += new O2.Kernel.CodeUtils.Callbacks.dMethod_Object(this.ascx_DropObject1_eDnDAction_ObjectDataReceived_Event);
            // 
            // splitContainer2
            // 
            this.splitContainer2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.rbScannerAppScanDE);
            this.splitContainer2.Panel1.Controls.Add(this.tabControl1);
            this.splitContainer2.Panel1.Controls.Add(this.btCancelOunceCLIScan);
            this.splitContainer2.Panel1.Controls.Add(this.label11);
            this.splitContainer2.Panel1.Controls.Add(this.gbNoRulesScanMode_Manual);
            this.splitContainer2.Panel1.Controls.Add(this.rbScannerMSCatNet);
            this.splitContainer2.Panel1.Controls.Add(this.rbScannerOunceCore);
            this.splitContainer2.Panel1.Controls.Add(this.btCreateCirDumpWithExistingRules);
            this.splitContainer2.Panel1.Controls.Add(this.lbNumberOfRulesInDatabase);
            this.splitContainer2.Panel1.Controls.Add(this.label9);
            this.splitContainer2.Panel1.Controls.Add(this.btScanWithExistingRules);
            this.splitContainer2.Panel1.Controls.Add(this.lnDependenciesStatus);
            this.splitContainer2.Panel1.Controls.Add(this.cbEnableNoOutOfTheBoxRules);
            this.splitContainer2.Panel1.Controls.Add(this.gbNoOutOfTheBoxRules);
            this.splitContainer2.Panel1.Controls.Add(this.cbAutoAppendTargetName);
            this.splitContainer2.Panel1.Controls.Add(this.tbWorkDirectory);
            this.splitContainer2.Panel1.Controls.Add(this.label5);
            this.splitContainer2.Panel1.Controls.Add(this.lbManualTestTargetFile);
            this.splitContainer2.Panel1.Controls.Add(this.label1);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.scanProcessBar);
            this.splitContainer2.Panel2.Controls.Add(this.cbShowLogExecutionLog);
            this.splitContainer2.Panel2.Controls.Add(this.label10);
            this.splitContainer2.Panel2.Controls.Add(this.rtbLogFileForCurrentScan);
            this.splitContainer2.Size = new System.Drawing.Size(786, 577);
            this.splitContainer2.SplitterDistance = 460;
            this.splitContainer2.TabIndex = 0;
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Location = new System.Drawing.Point(315, 151);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(465, 300);
            this.tabControl1.TabIndex = 47;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.llRefreshDirectory);
            this.tabPage1.Controls.Add(this.llDeleteSeletedFile);
            this.tabPage1.Controls.Add(this.adManualTestTempFiles);
            this.tabPage1.Controls.Add(this.cbDeleteDirectoryContentsRecursive);
            this.tabPage1.Controls.Add(this.llDeleteDirectoryContents);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(457, 274);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Scan Results (Temp Folder)";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // llRefreshDirectory
            // 
            this.llRefreshDirectory.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.llRefreshDirectory.AutoSize = true;
            this.llRefreshDirectory.BackColor = System.Drawing.Color.Transparent;
            this.llRefreshDirectory.Location = new System.Drawing.Point(412, 8);
            this.llRefreshDirectory.Name = "llRefreshDirectory";
            this.llRefreshDirectory.Size = new System.Drawing.Size(39, 13);
            this.llRefreshDirectory.TabIndex = 8;
            this.llRefreshDirectory.TabStop = true;
            this.llRefreshDirectory.Text = "refresh";
            this.llRefreshDirectory.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llRefreshDirectory_LinkClicked);
            // 
            // llDeleteSeletedFile
            // 
            this.llDeleteSeletedFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.llDeleteSeletedFile.AutoSize = true;
            this.llDeleteSeletedFile.Location = new System.Drawing.Point(6, 255);
            this.llDeleteSeletedFile.Name = "llDeleteSeletedFile";
            this.llDeleteSeletedFile.Size = new System.Drawing.Size(52, 13);
            this.llDeleteSeletedFile.TabIndex = 7;
            this.llDeleteSeletedFile.TabStop = true;
            this.llDeleteSeletedFile.Text = "delete file";
            this.llDeleteSeletedFile.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llDeleteSeletedFile_LinkClicked);
            // 
            // adManualTestTempFiles
            // 
            this.adManualTestTempFiles._ProcessDroppedObjects = true;
            this.adManualTestTempFiles._ShowFileSize = false;
            this.adManualTestTempFiles._ShowLinkToUpperFolder = true;
            this.adManualTestTempFiles._ViewMode = O2.Views.ASCX.CoreControls.ascx_Directory.ViewMode.Simple_With_LocationBar;
            this.adManualTestTempFiles._WatchFolder = true;
            this.adManualTestTempFiles.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.adManualTestTempFiles.BackColor = System.Drawing.SystemColors.Control;
            this.adManualTestTempFiles.ForeColor = System.Drawing.Color.Black;
            this.adManualTestTempFiles.Location = new System.Drawing.Point(2, 24);
            this.adManualTestTempFiles.Name = "adManualTestTempFiles";
            this.adManualTestTempFiles.Size = new System.Drawing.Size(449, 229);
            this.adManualTestTempFiles.TabIndex = 3;
            this.adManualTestTempFiles.eDirectoryEvent_DoubleClick += new O2.Views.ASCX.CoreControls.ascx_Directory.dDirectoryEvent(this.adManualTestTempFiles_eDirectoryEvent_DoubleClick);
            // 
            // cbDeleteDirectoryContentsRecursive
            // 
            this.cbDeleteDirectoryContentsRecursive.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cbDeleteDirectoryContentsRecursive.AutoSize = true;
            this.cbDeleteDirectoryContentsRecursive.Location = new System.Drawing.Point(253, 254);
            this.cbDeleteDirectoryContentsRecursive.Name = "cbDeleteDirectoryContentsRecursive";
            this.cbDeleteDirectoryContentsRecursive.Size = new System.Drawing.Size(69, 17);
            this.cbDeleteDirectoryContentsRecursive.TabIndex = 6;
            this.cbDeleteDirectoryContentsRecursive.Text = "recursive";
            this.cbDeleteDirectoryContentsRecursive.UseVisualStyleBackColor = true;
            // 
            // llDeleteDirectoryContents
            // 
            this.llDeleteDirectoryContents.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.llDeleteDirectoryContents.AutoSize = true;
            this.llDeleteDirectoryContents.Location = new System.Drawing.Point(331, 255);
            this.llDeleteDirectoryContents.Name = "llDeleteDirectoryContents";
            this.llDeleteDirectoryContents.Size = new System.Drawing.Size(123, 13);
            this.llDeleteDirectoryContents.TabIndex = 5;
            this.llDeleteDirectoryContents.TabStop = true;
            this.llDeleteDirectoryContents.Text = "delete directory contents";
            this.llDeleteDirectoryContents.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llDeleteDirectoryContents_LinkClicked);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.lbMissingDependencies);
            this.tabPage2.Controls.Add(this.label8);
            this.tabPage2.Controls.Add(this.ado_AddFilesOrDirectoryToScanBundle);
            this.tabPage2.Controls.Add(this.label7);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(457, 274);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Add Missing .NET Dependencies";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // lbMissingDependencies
            // 
            this.lbMissingDependencies.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lbMissingDependencies.FormattingEnabled = true;
            this.lbMissingDependencies.Location = new System.Drawing.Point(6, 58);
            this.lbMissingDependencies.Name = "lbMissingDependencies";
            this.lbMissingDependencies.Size = new System.Drawing.Size(445, 212);
            this.lbMissingDependencies.TabIndex = 29;
            // 
            // label8
            // 
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(2, 3);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(253, 30);
            this.label8.TabIndex = 30;
            this.label8.Text = "Missing Dependencies (you need to find these files and add them to the Scan Bundl" +
                "e)";
            // 
            // ado_AddFilesOrDirectoryToScanBundle
            // 
            this.ado_AddFilesOrDirectoryToScanBundle.AllowDrop = true;
            this.ado_AddFilesOrDirectoryToScanBundle.BackColor = System.Drawing.Color.Maroon;
            this.ado_AddFilesOrDirectoryToScanBundle.ForeColor = System.Drawing.Color.White;
            this.ado_AddFilesOrDirectoryToScanBundle.Location = new System.Drawing.Point(261, 21);
            this.ado_AddFilesOrDirectoryToScanBundle.Name = "ado_AddFilesOrDirectoryToScanBundle";
            this.ado_AddFilesOrDirectoryToScanBundle.Size = new System.Drawing.Size(190, 31);
            this.ado_AddFilesOrDirectoryToScanBundle.TabIndex = 28;
            this.ado_AddFilesOrDirectoryToScanBundle.Text = "Drop Content Here!!";
            this.ado_AddFilesOrDirectoryToScanBundle.eDnDAction_ObjectDataReceived_Event += new O2.Kernel.CodeUtils.Callbacks.dMethod_Object(this.ado_AddFilesOrDirectoryToScanBundle_eDnDAction_ObjectDataReceived_Event);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(258, 5);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(178, 13);
            this.label7.TabIndex = 29;
            this.label7.Text = "Add Files or Folders to Scan Bundle:";
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.groupBox2);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(457, 274);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Options";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cbStoreControlFlowBlockRawDataInsideCirDataFile);
            this.groupBox2.Location = new System.Drawing.Point(5, 6);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(412, 57);
            this.groupBox2.TabIndex = 23;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "CirData Creation options";
            // 
            // cbStoreControlFlowBlockRawDataInsideCirDataFile
            // 
            this.cbStoreControlFlowBlockRawDataInsideCirDataFile.Location = new System.Drawing.Point(9, 13);
            this.cbStoreControlFlowBlockRawDataInsideCirDataFile.Name = "cbStoreControlFlowBlockRawDataInsideCirDataFile";
            this.cbStoreControlFlowBlockRawDataInsideCirDataFile.Size = new System.Drawing.Size(395, 38);
            this.cbStoreControlFlowBlockRawDataInsideCirDataFile.TabIndex = 22;
            this.cbStoreControlFlowBlockRawDataInsideCirDataFile.Text = "Store Control Flow Block Raw Data Inside CirData File (this will make the file co" +
                "nsiderable bigger and it only needed when direct CirData analysis is needed";
            this.cbStoreControlFlowBlockRawDataInsideCirDataFile.UseVisualStyleBackColor = true;
            // 
            // btCancelOunceCLIScan
            // 
            this.btCancelOunceCLIScan.Location = new System.Drawing.Point(319, 64);
            this.btCancelOunceCLIScan.Name = "btCancelOunceCLIScan";
            this.btCancelOunceCLIScan.Size = new System.Drawing.Size(117, 41);
            this.btCancelOunceCLIScan.TabIndex = 46;
            this.btCancelOunceCLIScan.Text = "Cancel Current Ounce CLI Scan";
            this.btCancelOunceCLIScan.UseVisualStyleBackColor = true;
            this.btCancelOunceCLIScan.Click += new System.EventHandler(this.btCancelOunceCLIScan_Click);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(2, 4);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(104, 13);
            this.label11.TabIndex = 26;
            this.label11.Text = "Selected Engine:";
            // 
            // gbNoRulesScanMode_Manual
            // 
            this.gbNoRulesScanMode_Manual.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.gbNoRulesScanMode_Manual.Controls.Add(this.tbPathToRawCirDataFiles);
            this.gbNoRulesScanMode_Manual.Controls.Add(this.llRefreshRulesNumber);
            this.gbNoRulesScanMode_Manual.Controls.Add(this.llDeleteDatabase);
            this.gbNoRulesScanMode_Manual.Controls.Add(this.llPreCirDumps);
            this.gbNoRulesScanMode_Manual.Controls.Add(this.llPostCirDumps);
            this.gbNoRulesScanMode_Manual.Controls.Add(this.llCreateCirData);
            this.gbNoRulesScanMode_Manual.Enabled = false;
            this.gbNoRulesScanMode_Manual.Location = new System.Drawing.Point(7, 380);
            this.gbNoRulesScanMode_Manual.Name = "gbNoRulesScanMode_Manual";
            this.gbNoRulesScanMode_Manual.Size = new System.Drawing.Size(301, 72);
            this.gbNoRulesScanMode_Manual.TabIndex = 45;
            this.gbNoRulesScanMode_Manual.TabStop = false;
            this.gbNoRulesScanMode_Manual.Text = "\'No Rules\' Mode -- Manual";
            // 
            // tbPathToRawCirDataFiles
            // 
            this.tbPathToRawCirDataFiles.Location = new System.Drawing.Point(93, 38);
            this.tbPathToRawCirDataFiles.Name = "tbPathToRawCirDataFiles";
            this.tbPathToRawCirDataFiles.Size = new System.Drawing.Size(100, 20);
            this.tbPathToRawCirDataFiles.TabIndex = 47;
            // 
            // llRefreshRulesNumber
            // 
            this.llRefreshRulesNumber.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.llRefreshRulesNumber.AutoSize = true;
            this.llRefreshRulesNumber.Location = new System.Drawing.Point(187, 20);
            this.llRefreshRulesNumber.Name = "llRefreshRulesNumber";
            this.llRefreshRulesNumber.Size = new System.Drawing.Size(74, 13);
            this.llRefreshRulesNumber.TabIndex = 46;
            this.llRefreshRulesNumber.TabStop = true;
            this.llRefreshRulesNumber.Text = "refresh rules #";
            this.llRefreshRulesNumber.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llRefreshRulesNumber_LinkClicked);
            // 
            // llDeleteDatabase
            // 
            this.llDeleteDatabase.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.llDeleteDatabase.AutoSize = true;
            this.llDeleteDatabase.Location = new System.Drawing.Point(92, 20);
            this.llDeleteDatabase.Name = "llDeleteDatabase";
            this.llDeleteDatabase.Size = new System.Drawing.Size(85, 13);
            this.llDeleteDatabase.TabIndex = 45;
            this.llDeleteDatabase.TabStop = true;
            this.llDeleteDatabase.Text = "delete Database";
            this.llDeleteDatabase.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llDeleteDatabase_LinkClicked);
            // 
            // llPreCirDumps
            // 
            this.llPreCirDumps.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.llPreCirDumps.AutoSize = true;
            this.llPreCirDumps.Location = new System.Drawing.Point(9, 20);
            this.llPreCirDumps.Name = "llPreCirDumps";
            this.llPreCirDumps.Size = new System.Drawing.Size(73, 13);
            this.llPreCirDumps.TabIndex = 42;
            this.llPreCirDumps.TabStop = true;
            this.llPreCirDumps.Text = "pre Cir Dumps";
            this.llPreCirDumps.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llPreCirDumps_LinkClicked);
            // 
            // llPostCirDumps
            // 
            this.llPostCirDumps.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.llPostCirDumps.AutoSize = true;
            this.llPostCirDumps.Location = new System.Drawing.Point(9, 44);
            this.llPostCirDumps.Name = "llPostCirDumps";
            this.llPostCirDumps.Size = new System.Drawing.Size(78, 13);
            this.llPostCirDumps.TabIndex = 43;
            this.llPostCirDumps.TabStop = true;
            this.llPostCirDumps.Text = "post Cir Dumps";
            this.llPostCirDumps.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llPostCirDumps_LinkClicked);
            // 
            // llCreateCirData
            // 
            this.llCreateCirData.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.llCreateCirData.AutoSize = true;
            this.llCreateCirData.Location = new System.Drawing.Point(200, 44);
            this.llCreateCirData.Name = "llCreateCirData";
            this.llCreateCirData.Size = new System.Drawing.Size(75, 13);
            this.llCreateCirData.TabIndex = 44;
            this.llCreateCirData.TabStop = true;
            this.llCreateCirData.Text = "create CirData";
            this.llCreateCirData.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llCreateCirData_LinkClicked);
            // 
            // rbScannerMSCatNet
            // 
            this.rbScannerMSCatNet.AutoSize = true;
            this.rbScannerMSCatNet.Location = new System.Drawing.Point(19, 43);
            this.rbScannerMSCatNet.Name = "rbScannerMSCatNet";
            this.rbScannerMSCatNet.Size = new System.Drawing.Size(117, 17);
            this.rbScannerMSCatNet.TabIndex = 25;
            this.rbScannerMSCatNet.Text = "Microsoft CAT.NET";
            this.rbScannerMSCatNet.UseVisualStyleBackColor = true;
            // 
            // rbScannerOunceCore
            // 
            this.rbScannerOunceCore.AutoSize = true;
            this.rbScannerOunceCore.Checked = true;
            this.rbScannerOunceCore.Location = new System.Drawing.Point(19, 20);
            this.rbScannerOunceCore.Name = "rbScannerOunceCore";
            this.rbScannerOunceCore.Size = new System.Drawing.Size(82, 17);
            this.rbScannerOunceCore.TabIndex = 24;
            this.rbScannerOunceCore.Text = "Ounce Core";
            this.rbScannerOunceCore.UseVisualStyleBackColor = true;
            // 
            // btCreateCirDumpWithExistingRules
            // 
            this.btCreateCirDumpWithExistingRules.Enabled = false;
            this.btCreateCirDumpWithExistingRules.Location = new System.Drawing.Point(179, 64);
            this.btCreateCirDumpWithExistingRules.Name = "btCreateCirDumpWithExistingRules";
            this.btCreateCirDumpWithExistingRules.Size = new System.Drawing.Size(117, 41);
            this.btCreateCirDumpWithExistingRules.TabIndex = 21;
            this.btCreateCirDumpWithExistingRules.Text = "Create Cir Data file";
            this.btCreateCirDumpWithExistingRules.UseVisualStyleBackColor = true;
            this.btCreateCirDumpWithExistingRules.Click += new System.EventHandler(this.btCreateCirDumpWithExistingRules_Click);
            // 
            // lbNumberOfRulesInDatabase
            // 
            this.lbNumberOfRulesInDatabase.AutoSize = true;
            this.lbNumberOfRulesInDatabase.Location = new System.Drawing.Point(103, 108);
            this.lbNumberOfRulesInDatabase.Name = "lbNumberOfRulesInDatabase";
            this.lbNumberOfRulesInDatabase.Size = new System.Drawing.Size(16, 13);
            this.lbNumberOfRulesInDatabase.TabIndex = 20;
            this.lbNumberOfRulesInDatabase.Text = "...";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(4, 108);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(97, 13);
            this.label9.TabIndex = 19;
            this.label9.Text = "Rules in Database:";
            // 
            // btScanWithExistingRules
            // 
            this.btScanWithExistingRules.Enabled = false;
            this.btScanWithExistingRules.Location = new System.Drawing.Point(5, 64);
            this.btScanWithExistingRules.Name = "btScanWithExistingRules";
            this.btScanWithExistingRules.Size = new System.Drawing.Size(168, 41);
            this.btScanWithExistingRules.TabIndex = 18;
            this.btScanWithExistingRules.Text = "Scan using existing rules";
            this.btScanWithExistingRules.UseVisualStyleBackColor = true;
            this.btScanWithExistingRules.Click += new System.EventHandler(this.btScanWithExistingRules_Click);
            // 
            // lnDependenciesStatus
            // 
            this.lnDependenciesStatus.AutoSize = true;
            this.lnDependenciesStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lnDependenciesStatus.Location = new System.Drawing.Point(316, 41);
            this.lnDependenciesStatus.Name = "lnDependenciesStatus";
            this.lnDependenciesStatus.Size = new System.Drawing.Size(19, 13);
            this.lnDependenciesStatus.TabIndex = 17;
            this.lnDependenciesStatus.Text = "...";
            // 
            // cbEnableNoOutOfTheBoxRules
            // 
            this.cbEnableNoOutOfTheBoxRules.AutoSize = true;
            this.cbEnableNoOutOfTheBoxRules.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbEnableNoOutOfTheBoxRules.Location = new System.Drawing.Point(5, 125);
            this.cbEnableNoOutOfTheBoxRules.Name = "cbEnableNoOutOfTheBoxRules";
            this.cbEnableNoOutOfTheBoxRules.Size = new System.Drawing.Size(252, 17);
            this.cbEnableNoOutOfTheBoxRules.TabIndex = 16;
            this.cbEnableNoOutOfTheBoxRules.Text = "Enable mode: \'NO Out-of-the-box Rules\'";
            this.cbEnableNoOutOfTheBoxRules.UseVisualStyleBackColor = true;
            this.cbEnableNoOutOfTheBoxRules.CheckedChanged += new System.EventHandler(this.cbEnableNoOutOfTheBoxRules_CheckedChanged);
            // 
            // gbNoOutOfTheBoxRules
            // 
            this.gbNoOutOfTheBoxRules.Controls.Add(this.btCreateO2RulePacks);
            this.gbNoOutOfTheBoxRules.Controls.Add(this.btCreateCirDumpForSelectedFile);
            this.gbNoOutOfTheBoxRules.Controls.Add(this.btCreateAssessmentFiles);
            this.gbNoOutOfTheBoxRules.Controls.Add(this.cbCallBacksOnControlFlowGraphs_And_ExternalSinks);
            this.gbNoOutOfTheBoxRules.Controls.Add(this.cbCallBacksOnEdges_And_ExternalSinks);
            this.gbNoOutOfTheBoxRules.Controls.Add(this.cbSourcesAndSinks);
            this.gbNoOutOfTheBoxRules.Controls.Add(this.label2);
            this.gbNoOutOfTheBoxRules.Enabled = false;
            this.gbNoOutOfTheBoxRules.Location = new System.Drawing.Point(5, 142);
            this.gbNoOutOfTheBoxRules.Name = "gbNoOutOfTheBoxRules";
            this.gbNoOutOfTheBoxRules.Size = new System.Drawing.Size(304, 232);
            this.gbNoOutOfTheBoxRules.TabIndex = 15;
            this.gbNoOutOfTheBoxRules.TabStop = false;
            // 
            // btCreateO2RulePacks
            // 
            this.btCreateO2RulePacks.Location = new System.Drawing.Point(175, 79);
            this.btCreateO2RulePacks.Name = "btCreateO2RulePacks";
            this.btCreateO2RulePacks.Size = new System.Drawing.Size(123, 32);
            this.btCreateO2RulePacks.TabIndex = 13;
            this.btCreateO2RulePacks.Text = "Create Rule Packs";
            this.btCreateO2RulePacks.UseVisualStyleBackColor = true;
            this.btCreateO2RulePacks.Click += new System.EventHandler(this.btCreateO2RulePacks_Click);
            // 
            // btCreateCirDumpForSelectedFile
            // 
            this.btCreateCirDumpForSelectedFile.Location = new System.Drawing.Point(6, 79);
            this.btCreateCirDumpForSelectedFile.Name = "btCreateCirDumpForSelectedFile";
            this.btCreateCirDumpForSelectedFile.Size = new System.Drawing.Size(162, 32);
            this.btCreateCirDumpForSelectedFile.TabIndex = 2;
            this.btCreateCirDumpForSelectedFile.Text = "Step 1) Create Cir Data File";
            this.btCreateCirDumpForSelectedFile.UseVisualStyleBackColor = true;
            this.btCreateCirDumpForSelectedFile.Click += new System.EventHandler(this.btCreateCirDumpForSelectedFile_Click);
            // 
            // btCreateAssessmentFiles
            // 
            this.btCreateAssessmentFiles.Location = new System.Drawing.Point(7, 179);
            this.btCreateAssessmentFiles.Name = "btCreateAssessmentFiles";
            this.btCreateAssessmentFiles.Size = new System.Drawing.Size(161, 43);
            this.btCreateAssessmentFiles.TabIndex = 6;
            this.btCreateAssessmentFiles.Text = "Step 2) Create Assessment Files";
            this.btCreateAssessmentFiles.UseVisualStyleBackColor = true;
            this.btCreateAssessmentFiles.Click += new System.EventHandler(this.btCreateAssessmentFiles_Click);
            // 
            // cbCallBacksOnControlFlowGraphs_And_ExternalSinks
            // 
            this.cbCallBacksOnControlFlowGraphs_And_ExternalSinks.AutoSize = true;
            this.cbCallBacksOnControlFlowGraphs_And_ExternalSinks.Checked = true;
            this.cbCallBacksOnControlFlowGraphs_And_ExternalSinks.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbCallBacksOnControlFlowGraphs_And_ExternalSinks.Location = new System.Drawing.Point(7, 117);
            this.cbCallBacksOnControlFlowGraphs_And_ExternalSinks.Name = "cbCallBacksOnControlFlowGraphs_And_ExternalSinks";
            this.cbCallBacksOnControlFlowGraphs_And_ExternalSinks.Size = new System.Drawing.Size(271, 17);
            this.cbCallBacksOnControlFlowGraphs_And_ExternalSinks.TabIndex = 7;
            this.cbCallBacksOnControlFlowGraphs_And_ExternalSinks.Text = "CallBacksOnControlFlowGraphs_And_ExternalSinks";
            this.cbCallBacksOnControlFlowGraphs_And_ExternalSinks.UseVisualStyleBackColor = true;
            // 
            // cbCallBacksOnEdges_And_ExternalSinks
            // 
            this.cbCallBacksOnEdges_And_ExternalSinks.AutoSize = true;
            this.cbCallBacksOnEdges_And_ExternalSinks.Checked = true;
            this.cbCallBacksOnEdges_And_ExternalSinks.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbCallBacksOnEdges_And_ExternalSinks.Location = new System.Drawing.Point(7, 137);
            this.cbCallBacksOnEdges_And_ExternalSinks.Name = "cbCallBacksOnEdges_And_ExternalSinks";
            this.cbCallBacksOnEdges_And_ExternalSinks.Size = new System.Drawing.Size(212, 17);
            this.cbCallBacksOnEdges_And_ExternalSinks.TabIndex = 8;
            this.cbCallBacksOnEdges_And_ExternalSinks.Text = "CallBacksOnEdges_And_ExternalSinks";
            this.cbCallBacksOnEdges_And_ExternalSinks.UseVisualStyleBackColor = true;
            // 
            // cbSourcesAndSinks
            // 
            this.cbSourcesAndSinks.AutoSize = true;
            this.cbSourcesAndSinks.Checked = true;
            this.cbSourcesAndSinks.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbSourcesAndSinks.Location = new System.Drawing.Point(7, 156);
            this.cbSourcesAndSinks.Name = "cbSourcesAndSinks";
            this.cbSourcesAndSinks.Size = new System.Drawing.Size(110, 17);
            this.cbSourcesAndSinks.TabIndex = 9;
            this.cbSourcesAndSinks.Text = "SourcesAndSinks";
            this.cbSourcesAndSinks.UseVisualStyleBackColor = true;
            this.cbSourcesAndSinks.CheckedChanged += new System.EventHandler(this.cbSourcesAndSinks_CheckedChanged);
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.Location = new System.Drawing.Point(6, 18);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(292, 58);
            this.label2.TabIndex = 10;
            this.label2.Text = "WARNING: This O2 Scan Technique will DELETE your entire Rules database (Please en" +
                "sure that you have made a backup of the MySql Data folder";
            // 
            // cbAutoAppendTargetName
            // 
            this.cbAutoAppendTargetName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbAutoAppendTargetName.AutoSize = true;
            this.cbAutoAppendTargetName.Checked = true;
            this.cbAutoAppendTargetName.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbAutoAppendTargetName.Location = new System.Drawing.Point(631, 23);
            this.cbAutoAppendTargetName.Name = "cbAutoAppendTargetName";
            this.cbAutoAppendTargetName.Size = new System.Drawing.Size(153, 17);
            this.cbAutoAppendTargetName.TabIndex = 14;
            this.cbAutoAppendTargetName.Text = "Auto Append Target Name";
            this.cbAutoAppendTargetName.UseVisualStyleBackColor = true;
            // 
            // tbWorkDirectory
            // 
            this.tbWorkDirectory.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbWorkDirectory.Location = new System.Drawing.Point(419, 20);
            this.tbWorkDirectory.Name = "tbWorkDirectory";
            this.tbWorkDirectory.Size = new System.Drawing.Size(206, 20);
            this.tbWorkDirectory.TabIndex = 13;
            this.tbWorkDirectory.TextChanged += new System.EventHandler(this.tbWorkDirectory_TextChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(312, 23);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(92, 13);
            this.label5.TabIndex = 12;
            this.label5.Text = "Work Directory";
            // 
            // lbManualTestTargetFile
            // 
            this.lbManualTestTargetFile.AutoSize = true;
            this.lbManualTestTargetFile.Location = new System.Drawing.Point(420, 4);
            this.lbManualTestTargetFile.Name = "lbManualTestTargetFile";
            this.lbManualTestTargetFile.Size = new System.Drawing.Size(16, 13);
            this.lbManualTestTargetFile.TabIndex = 1;
            this.lbManualTestTargetFile.Text = "...";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(312, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(102, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Selected Target:";
            // 
            // scanProcessBar
            // 
            this.scanProcessBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.scanProcessBar.Enabled = false;
            this.scanProcessBar.Location = new System.Drawing.Point(302, 9);
            this.scanProcessBar.Name = "scanProcessBar";
            this.scanProcessBar.Size = new System.Drawing.Size(477, 17);
            this.scanProcessBar.Step = 1;
            this.scanProcessBar.TabIndex = 20;
            // 
            // cbShowLogExecutionLog
            // 
            this.cbShowLogExecutionLog.AutoSize = true;
            this.cbShowLogExecutionLog.Checked = true;
            this.cbShowLogExecutionLog.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbShowLogExecutionLog.Location = new System.Drawing.Point(146, 9);
            this.cbShowLogExecutionLog.Name = "cbShowLogExecutionLog";
            this.cbShowLogExecutionLog.Size = new System.Drawing.Size(145, 17);
            this.cbShowLogExecutionLog.TabIndex = 19;
            this.cbShowLogExecutionLog.Text = "Show Log Execution Log";
            this.cbShowLogExecutionLog.UseVisualStyleBackColor = true;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(2, 9);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(138, 13);
            this.label10.TabIndex = 18;
            this.label10.Text = "Scanner execution log:";
            // 
            // rtbLogFileForCurrentScan
            // 
            this.rtbLogFileForCurrentScan.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.rtbLogFileForCurrentScan.Location = new System.Drawing.Point(1, 30);
            this.rtbLogFileForCurrentScan.Name = "rtbLogFileForCurrentScan";
            this.rtbLogFileForCurrentScan.Size = new System.Drawing.Size(779, 76);
            this.rtbLogFileForCurrentScan.TabIndex = 5;
            this.rtbLogFileForCurrentScan.Text = "";
            this.rtbLogFileForCurrentScan.WordWrap = false;
            // 
            // rbScannerAppScanDE
            // 
            this.rbScannerAppScanDE.AutoSize = true;
            this.rbScannerAppScanDE.Location = new System.Drawing.Point(179, 19);
            this.rbScannerAppScanDE.Name = "rbScannerAppScanDE";
            this.rbScannerAppScanDE.Size = new System.Drawing.Size(87, 17);
            this.rbScannerAppScanDE.TabIndex = 48;
            this.rbScannerAppScanDE.Text = "AppScan DE";
            this.rbScannerAppScanDE.UseVisualStyleBackColor = true;
            // 
            // ascx_WillItScan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Name = "ascx_WillItScan";
            this.Size = new System.Drawing.Size(1012, 577);
            this.Load += new System.EventHandler(this.ascx_WillItScan_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel1.PerformLayout();
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.Panel2.PerformLayout();
            this.splitContainer2.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.gbNoRulesScanMode_Manual.ResumeLayout(false);
            this.gbNoRulesScanMode_Manual.PerformLayout();
            this.gbNoOutOfTheBoxRules.ResumeLayout(false);
            this.gbNoOutOfTheBoxRules.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ListBox lbTargetFiles;
        private ascx_DropObject ascx_DropObject1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private ascx_Directory adManualTestTempFiles;
        private System.Windows.Forms.Button btCreateCirDumpForSelectedFile;
        private System.Windows.Forms.Label lbManualTestTargetFile;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RichTextBox rtbLogFileForCurrentScan;
        private System.Windows.Forms.Button btCreateAssessmentFiles;
        private System.Windows.Forms.CheckBox cbSourcesAndSinks;
        private System.Windows.Forms.CheckBox cbCallBacksOnEdges_And_ExternalSinks;
        private System.Windows.Forms.CheckBox cbCallBacksOnControlFlowGraphs_And_ExternalSinks;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox cbDoubleClickToDeleteItem;
        private System.Windows.Forms.Label label5;
        private ascx_DropObject ado_AddFilesOrDirectoryToScanBundle;
        private System.Windows.Forms.GroupBox gbNoOutOfTheBoxRules;
        private System.Windows.Forms.CheckBox cbAutoAppendTargetName;
        private System.Windows.Forms.TextBox tbWorkDirectory;
        private System.Windows.Forms.CheckBox cbEnableNoOutOfTheBoxRules;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lnDependenciesStatus;
        private System.Windows.Forms.ListBox lbMissingDependencies;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lbNumberOfRulesInDatabase;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button btScanWithExistingRules;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.LinkLabel llDownloadDemoFile_HacmeBank_Website;
        private System.Windows.Forms.LinkLabel llDownloadDemoFile_HacmeBank_WebServices;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.LinkLabel llDeleteDirectoryContents;
        private System.Windows.Forms.CheckBox cbDeleteDirectoryContentsRecursive;
        private System.Windows.Forms.LinkLabel llDeleteSeletedFile;
        private System.Windows.Forms.LinkLabel llPostCirDumps;
        private System.Windows.Forms.LinkLabel llPreCirDumps;
        private System.Windows.Forms.LinkLabel llRefreshDirectory;
        private System.Windows.Forms.LinkLabel llCreateCirData;
        private System.Windows.Forms.GroupBox gbNoRulesScanMode_Manual;
        private System.Windows.Forms.LinkLabel llRefreshRulesNumber;
        private System.Windows.Forms.LinkLabel llDeleteDatabase;
        private System.Windows.Forms.Button btCreateCirDumpWithExistingRules;
        private System.Windows.Forms.TextBox tbPathToRawCirDataFiles;
        private System.Windows.Forms.Button btCreateO2RulePacks;
        private System.Windows.Forms.LinkLabel llTargetFiles_DeleteSelected;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.RadioButton rbScannerMSCatNet;
        private System.Windows.Forms.RadioButton rbScannerOunceCore;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox cbStoreControlFlowBlockRawDataInsideCirDataFile;
        private System.Windows.Forms.CheckBox cbShowLogExecutionLog;
        private System.Windows.Forms.ProgressBar scanProcessBar;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button btCancelOunceCLIScan;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.RadioButton rbScannerAppScanDE;
    }
}