using O2.External.SharpDevelop.Ascx;
using O2.Kernel.CodeUtils;
using O2.Legacy.OunceV6.GLEEGraphWiz.Ascx;
using O2.Views.ASCX.CoreControls;

namespace O2.Tool.ViewAssessmentRun.Ascx
{
    partial class ascx_ViewAssessmentRun
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ascx_ViewAssessmentRun));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lbLoadTime = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.lbNumberOfAssessmentFiles = new System.Windows.Forms.Label();
            this.lbNumberOfLostSinks = new System.Windows.Forms.Label();
            this.lbNumberOfSmartTraces = new System.Windows.Forms.Label();
            this.lbNumberOfFindings = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.lbAssessmentFileLoaded = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.btViewByFiles = new System.Windows.Forms.Button();
            this.btViewByVulnerabilityType = new System.Windows.Forms.Button();
            this.btFilterByActionObject = new System.Windows.Forms.Button();
            this.tvFindingsList = new System.Windows.Forms.TreeView();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.lbFilter = new System.Windows.Forms.Label();
            this.dgvFindingData = new System.Windows.Forms.DataGridView();
            this.sName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label22 = new System.Windows.Forms.Label();
            this.cbActionObject_NameFilter = new System.Windows.Forms.ComboBox();
            this.lbActionObjectFilter = new System.Windows.Forms.Label();
            this.lbBrowseAssessment_NumberOfItemsLoadedIn_ByActionObject = new System.Windows.Forms.Label();
            this.lnActionObjectNodes = new System.Windows.Forms.Label();
            this.btCollapseAll = new System.Windows.Forms.Button();
            this.ilSolutionsProjecstFiles = new System.Windows.Forms.ImageList(this.components);
            this.btExpandAll = new System.Windows.Forms.Button();
            this.scHostsTracesRulesGlee = new System.Windows.Forms.SplitContainer();
            this.scTraceDetailAndSourceCode = new System.Windows.Forms.SplitContainer();
            this.rbSmartTraceFilter_MethodName = new System.Windows.Forms.RadioButton();
            this.tvSmartTrace = new System.Windows.Forms.TreeView();
            this.rbSmartTraceFilter_SourceCode = new System.Windows.Forms.RadioButton();
            this.rbSmartTraceFilter_Context = new System.Windows.Forms.RadioButton();
            this.sceSourceCodeEditor = new O2.External.SharpDevelop.Ascx.ascx_SourceCodeEditor();
            this.scCustomRulesGlee = new System.Windows.Forms.SplitContainer();
            this.btGLEE_PlotAllTracesInInAnewWindow = new System.Windows.Forms.Button();
            this.aGLEE = new O2.Legacy.OunceV6.GLEEGraphWiz.Ascx.ascx_Glee();
            this.cbBrowseLoadedAssessmentFile_IgnoreRootCallInvocation = new System.Windows.Forms.CheckBox();
            this.cbBrowseLoadedAssessmentFile_DropDuplicateSmartTraces = new System.Windows.Forms.CheckBox();
            this.rbFindingsFilter_SmartTraces_Unique_Lost_Sinks = new System.Windows.Forms.RadioButton();
            this.rbFindingsFilter_SmartTraces_Lost_Sinks = new System.Windows.Forms.RadioButton();
            this.rbFindingsFilter_NoTraces = new System.Windows.Forms.RadioButton();
            this.rbFindingsFilter_SmartTraces = new System.Windows.Forms.RadioButton();
            this.rbFindingsFilter_AllFindings = new System.Windows.Forms.RadioButton();
            this.btFilterByCustomScript = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.btFilterBySelectedFilter = new System.Windows.Forms.Button();
            this.scTopLevelContainer_StatsAndRest = new System.Windows.Forms.SplitContainer();
            this.gbVisibleControls = new System.Windows.Forms.GroupBox();
            this.cbVisibleControls_CustomRules = new System.Windows.Forms.CheckBox();
            this.cbVisibleControls_SourceCode = new System.Windows.Forms.CheckBox();
            this.cbVisibleControls_StatsAndFilters = new System.Windows.Forms.CheckBox();
            this.cbVisibleControls_GraphStats = new System.Windows.Forms.CheckBox();
            this.cbVisibleControls_TraceDetails = new System.Windows.Forms.CheckBox();
            this.dndDropArea = new O2.Views.ASCX.CoreControls.ascx_DropObject();
            this.cbAutoTryToResolveSourceCodePaths = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFindingData)).BeginInit();
            this.scHostsTracesRulesGlee.Panel1.SuspendLayout();
            this.scHostsTracesRulesGlee.Panel2.SuspendLayout();
            this.scHostsTracesRulesGlee.SuspendLayout();
            this.scTraceDetailAndSourceCode.Panel1.SuspendLayout();
            this.scTraceDetailAndSourceCode.Panel2.SuspendLayout();
            this.scTraceDetailAndSourceCode.SuspendLayout();
            this.scCustomRulesGlee.Panel2.SuspendLayout();
            this.scCustomRulesGlee.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.scTopLevelContainer_StatsAndRest.Panel1.SuspendLayout();
            this.scTopLevelContainer_StatsAndRest.Panel2.SuspendLayout();
            this.scTopLevelContainer_StatsAndRest.SuspendLayout();
            this.gbVisibleControls.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.lbLoadTime);
            this.groupBox1.Controls.Add(this.label24);
            this.groupBox1.Controls.Add(this.lbNumberOfAssessmentFiles);
            this.groupBox1.Controls.Add(this.lbNumberOfLostSinks);
            this.groupBox1.Controls.Add(this.lbNumberOfSmartTraces);
            this.groupBox1.Controls.Add(this.lbNumberOfFindings);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Location = new System.Drawing.Point(2, 47);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(273, 112);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "AssessmentRun Stats";
            // 
            // lbLoadTime
            // 
            this.lbLoadTime.AutoSize = true;
            this.lbLoadTime.Location = new System.Drawing.Point(25, 31);
            this.lbLoadTime.Name = "lbLoadTime";
            this.lbLoadTime.Size = new System.Drawing.Size(10, 13);
            this.lbLoadTime.TabIndex = 50;
            this.lbLoadTime.Text = ".";
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(6, 18);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(102, 13);
            this.label24.TabIndex = 49;
            this.label24.Text = "Load time (seconds)";
            // 
            // lbNumberOfAssessmentFiles
            // 
            this.lbNumberOfAssessmentFiles.AutoSize = true;
            this.lbNumberOfAssessmentFiles.Location = new System.Drawing.Point(85, 46);
            this.lbNumberOfAssessmentFiles.Name = "lbNumberOfAssessmentFiles";
            this.lbNumberOfAssessmentFiles.Size = new System.Drawing.Size(13, 13);
            this.lbNumberOfAssessmentFiles.TabIndex = 46;
            this.lbNumberOfAssessmentFiles.Text = "0";
            // 
            // lbNumberOfLostSinks
            // 
            this.lbNumberOfLostSinks.AutoSize = true;
            this.lbNumberOfLostSinks.Location = new System.Drawing.Point(85, 91);
            this.lbNumberOfLostSinks.Name = "lbNumberOfLostSinks";
            this.lbNumberOfLostSinks.Size = new System.Drawing.Size(13, 13);
            this.lbNumberOfLostSinks.TabIndex = 45;
            this.lbNumberOfLostSinks.Text = "0";
            // 
            // lbNumberOfSmartTraces
            // 
            this.lbNumberOfSmartTraces.AutoSize = true;
            this.lbNumberOfSmartTraces.Location = new System.Drawing.Point(85, 76);
            this.lbNumberOfSmartTraces.Name = "lbNumberOfSmartTraces";
            this.lbNumberOfSmartTraces.Size = new System.Drawing.Size(13, 13);
            this.lbNumberOfSmartTraces.TabIndex = 44;
            this.lbNumberOfSmartTraces.Text = "0";
            // 
            // lbNumberOfFindings
            // 
            this.lbNumberOfFindings.AutoSize = true;
            this.lbNumberOfFindings.Location = new System.Drawing.Point(85, 61);
            this.lbNumberOfFindings.Name = "lbNumberOfFindings";
            this.lbNumberOfFindings.Size = new System.Drawing.Size(13, 13);
            this.lbNumberOfFindings.TabIndex = 43;
            this.lbNumberOfFindings.Text = "0";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(6, 46);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(38, 13);
            this.label11.TabIndex = 42;
            this.label11.Text = "# Files";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(6, 91);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(66, 13);
            this.label10.TabIndex = 41;
            this.label10.Text = "# Lost Sinks";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(6, 76);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(80, 13);
            this.label9.TabIndex = 40;
            this.label9.Text = "# Smart Traces";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 61);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(56, 13);
            this.label8.TabIndex = 39;
            this.label8.Text = "# Findings";
            // 
            // lbAssessmentFileLoaded
            // 
            this.lbAssessmentFileLoaded.AutoSize = true;
            this.lbAssessmentFileLoaded.Location = new System.Drawing.Point(784, 22);
            this.lbAssessmentFileLoaded.Name = "lbAssessmentFileLoaded";
            this.lbAssessmentFileLoaded.Size = new System.Drawing.Size(16, 13);
            this.lbAssessmentFileLoaded.TabIndex = 48;
            this.lbAssessmentFileLoaded.Text = "...";
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(784, 9);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(124, 13);
            this.label23.TabIndex = 47;
            this.label23.Text = "Assessment File Loaded:";
            // 
            // btViewByFiles
            // 
            this.btViewByFiles.Location = new System.Drawing.Point(2, 121);
            this.btViewByFiles.Name = "btViewByFiles";
            this.btViewByFiles.Size = new System.Drawing.Size(161, 23);
            this.btViewByFiles.TabIndex = 49;
            this.btViewByFiles.Text = "Files";
            this.btViewByFiles.UseVisualStyleBackColor = true;
            this.btViewByFiles.Click += new System.EventHandler(this.btViewByFiles_Click);
            // 
            // btViewByVulnerabilityType
            // 
            this.btViewByVulnerabilityType.Location = new System.Drawing.Point(2, 85);
            this.btViewByVulnerabilityType.Name = "btViewByVulnerabilityType";
            this.btViewByVulnerabilityType.Size = new System.Drawing.Size(161, 23);
            this.btViewByVulnerabilityType.TabIndex = 50;
            this.btViewByVulnerabilityType.Text = "Vulnerability Type";
            this.btViewByVulnerabilityType.UseVisualStyleBackColor = true;
            this.btViewByVulnerabilityType.Click += new System.EventHandler(this.btViewByVulnerabilityType_Click);
            // 
            // btFilterByActionObject
            // 
            this.btFilterByActionObject.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.btFilterByActionObject.Location = new System.Drawing.Point(2, 47);
            this.btFilterByActionObject.Name = "btFilterByActionObject";
            this.btFilterByActionObject.Size = new System.Drawing.Size(161, 23);
            this.btFilterByActionObject.TabIndex = 51;
            this.btFilterByActionObject.Text = "Action Object";
            this.btFilterByActionObject.UseVisualStyleBackColor = true;
            this.btFilterByActionObject.Click += new System.EventHandler(this.btFilterByActionObject_Click);
            // 
            // tvFindingsList
            // 
            this.tvFindingsList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tvFindingsList.Location = new System.Drawing.Point(3, 26);
            this.tvFindingsList.Name = "tvFindingsList";
            this.tvFindingsList.Size = new System.Drawing.Size(326, 528);
            this.tvFindingsList.TabIndex = 52;
            this.tvFindingsList.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvFindingsList_AfterSelect);
            // 
            // splitContainer1
            // 
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.cbAutoTryToResolveSourceCodePaths);
            this.splitContainer1.Panel1.Controls.Add(this.lbFilter);
            this.splitContainer1.Panel1.Controls.Add(this.tvFindingsList);
            this.splitContainer1.Panel1.Controls.Add(this.dgvFindingData);
            this.splitContainer1.Panel1.Controls.Add(this.label22);
            this.splitContainer1.Panel1.Controls.Add(this.cbActionObject_NameFilter);
            this.splitContainer1.Panel1.Controls.Add(this.lbActionObjectFilter);
            this.splitContainer1.Panel1.Controls.Add(this.lbBrowseAssessment_NumberOfItemsLoadedIn_ByActionObject);
            this.splitContainer1.Panel1.Controls.Add(this.lnActionObjectNodes);
            this.splitContainer1.Panel1.Controls.Add(this.btCollapseAll);
            this.splitContainer1.Panel1.Controls.Add(this.btExpandAll);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.scHostsTracesRulesGlee);
            this.splitContainer1.Size = new System.Drawing.Size(1185, 706);
            this.splitContainer1.SplitterDistance = 336;
            this.splitContainer1.TabIndex = 53;
            // 
            // lbFilter
            // 
            this.lbFilter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lbFilter.AutoSize = true;
            this.lbFilter.Location = new System.Drawing.Point(3, 558);
            this.lbFilter.Name = "lbFilter";
            this.lbFilter.Size = new System.Drawing.Size(16, 13);
            this.lbFilter.TabIndex = 53;
            this.lbFilter.Text = "...";
            // 
            // dgvFindingData
            // 
            this.dgvFindingData.AllowUserToAddRows = false;
            this.dgvFindingData.AllowUserToDeleteRows = false;
            this.dgvFindingData.AllowUserToOrderColumns = true;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.Silver;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.Black;
            this.dgvFindingData.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvFindingData.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvFindingData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvFindingData.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.sName,
            this.sValue});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.LightGray;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvFindingData.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvFindingData.Location = new System.Drawing.Point(3, 576);
            this.dgvFindingData.Name = "dgvFindingData";
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.LightGray;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.Black;
            this.dgvFindingData.RowsDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvFindingData.Size = new System.Drawing.Size(326, 100);
            this.dgvFindingData.TabIndex = 13;
            this.dgvFindingData.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvFindingData_CellContentClick);
            // 
            // sName
            // 
            this.sName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.sName.HeaderText = "Name";
            this.sName.Name = "sName";
            this.sName.Width = 60;
            // 
            // sValue
            // 
            this.sValue.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.sValue.HeaderText = "Value";
            this.sValue.Name = "sValue";
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(3, 318);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(76, 13);
            this.label22.TabIndex = 11;
            this.label22.Text = "Finding Details";
            // 
            // cbActionObject_NameFilter
            // 
            this.cbActionObject_NameFilter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbActionObject_NameFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbActionObject_NameFilter.FormattingEnabled = true;
            this.cbActionObject_NameFilter.Items.AddRange(new object[] {
            "vuln_type",
            "caller_name",
            "lost_sink",
            "source",
            "known_sink",
            "source_code"});
            this.cbActionObject_NameFilter.Location = new System.Drawing.Point(252, 3);
            this.cbActionObject_NameFilter.Name = "cbActionObject_NameFilter";
            this.cbActionObject_NameFilter.Size = new System.Drawing.Size(77, 21);
            this.cbActionObject_NameFilter.TabIndex = 56;
            this.cbActionObject_NameFilter.SelectedIndexChanged += new System.EventHandler(this.cbActionObject_NameFilter_SelectedIndexChanged);
            // 
            // lbActionObjectFilter
            // 
            this.lbActionObjectFilter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lbActionObjectFilter.AutoSize = true;
            this.lbActionObjectFilter.Location = new System.Drawing.Point(217, 9);
            this.lbActionObjectFilter.Name = "lbActionObjectFilter";
            this.lbActionObjectFilter.Size = new System.Drawing.Size(29, 13);
            this.lbActionObjectFilter.TabIndex = 59;
            this.lbActionObjectFilter.Text = "Filter";
            // 
            // lbBrowseAssessment_NumberOfItemsLoadedIn_ByActionObject
            // 
            this.lbBrowseAssessment_NumberOfItemsLoadedIn_ByActionObject.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lbBrowseAssessment_NumberOfItemsLoadedIn_ByActionObject.AutoSize = true;
            this.lbBrowseAssessment_NumberOfItemsLoadedIn_ByActionObject.Location = new System.Drawing.Point(272, 436);
            this.lbBrowseAssessment_NumberOfItemsLoadedIn_ByActionObject.Name = "lbBrowseAssessment_NumberOfItemsLoadedIn_ByActionObject";
            this.lbBrowseAssessment_NumberOfItemsLoadedIn_ByActionObject.Size = new System.Drawing.Size(13, 13);
            this.lbBrowseAssessment_NumberOfItemsLoadedIn_ByActionObject.TabIndex = 57;
            this.lbBrowseAssessment_NumberOfItemsLoadedIn_ByActionObject.Text = "0";
            // 
            // lnActionObjectNodes
            // 
            this.lnActionObjectNodes.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lnActionObjectNodes.AutoSize = true;
            this.lnActionObjectNodes.Location = new System.Drawing.Point(291, 558);
            this.lnActionObjectNodes.Name = "lnActionObjectNodes";
            this.lnActionObjectNodes.Size = new System.Drawing.Size(38, 13);
            this.lnActionObjectNodes.TabIndex = 58;
            this.lnActionObjectNodes.Text = "Nodes";
            // 
            // btCollapseAll
            // 
            this.btCollapseAll.ImageIndex = 6;
            this.btCollapseAll.ImageList = this.ilSolutionsProjecstFiles;
            this.btCollapseAll.Location = new System.Drawing.Point(3, 1);
            this.btCollapseAll.Name = "btCollapseAll";
            this.btCollapseAll.Size = new System.Drawing.Size(40, 23);
            this.btCollapseAll.TabIndex = 60;
            this.btCollapseAll.UseVisualStyleBackColor = true;
            this.btCollapseAll.Visible = false;
            this.btCollapseAll.Click += new System.EventHandler(this.btCollapseAll_Click);
            // 
            // ilSolutionsProjecstFiles
            // 
            this.ilSolutionsProjecstFiles.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ilSolutionsProjecstFiles.ImageStream")));
            this.ilSolutionsProjecstFiles.TransparentColor = System.Drawing.Color.Transparent;
            this.ilSolutionsProjecstFiles.Images.SetKeyName(0, "Explorer_allApplications.ico");
            this.ilSolutionsProjecstFiles.Images.SetKeyName(1, "Explorer_Application.ico");
            this.ilSolutionsProjecstFiles.Images.SetKeyName(2, "Explorer_Project.ico");
            this.ilSolutionsProjecstFiles.Images.SetKeyName(3, "Explorer_Folder.ico");
            this.ilSolutionsProjecstFiles.Images.SetKeyName(4, "Explorer_File.ico");
            this.ilSolutionsProjecstFiles.Images.SetKeyName(5, "ExpandAll.ico");
            this.ilSolutionsProjecstFiles.Images.SetKeyName(6, "CollapseAll.ico");
            this.ilSolutionsProjecstFiles.Images.SetKeyName(7, "PatternLibrary_Delete.ico");
            // 
            // btExpandAll
            // 
            this.btExpandAll.ImageIndex = 5;
            this.btExpandAll.ImageList = this.ilSolutionsProjecstFiles;
            this.btExpandAll.Location = new System.Drawing.Point(3, 2);
            this.btExpandAll.Name = "btExpandAll";
            this.btExpandAll.Size = new System.Drawing.Size(40, 23);
            this.btExpandAll.TabIndex = 61;
            this.btExpandAll.UseVisualStyleBackColor = true;
            this.btExpandAll.Click += new System.EventHandler(this.btExpandAll_Click);
            // 
            // scHostsTracesRulesGlee
            // 
            this.scHostsTracesRulesGlee.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scHostsTracesRulesGlee.Location = new System.Drawing.Point(0, 0);
            this.scHostsTracesRulesGlee.Name = "scHostsTracesRulesGlee";
            this.scHostsTracesRulesGlee.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // scHostsTracesRulesGlee.Panel1
            // 
            this.scHostsTracesRulesGlee.Panel1.Controls.Add(this.scTraceDetailAndSourceCode);
            // 
            // scHostsTracesRulesGlee.Panel2
            // 
            this.scHostsTracesRulesGlee.Panel2.Controls.Add(this.scCustomRulesGlee);
            this.scHostsTracesRulesGlee.Size = new System.Drawing.Size(841, 702);
            this.scHostsTracesRulesGlee.SplitterDistance = 321;
            this.scHostsTracesRulesGlee.TabIndex = 18;
            // 
            // scTraceDetailAndSourceCode
            // 
            this.scTraceDetailAndSourceCode.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.scTraceDetailAndSourceCode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scTraceDetailAndSourceCode.Location = new System.Drawing.Point(0, 0);
            this.scTraceDetailAndSourceCode.Name = "scTraceDetailAndSourceCode";
            // 
            // scTraceDetailAndSourceCode.Panel1
            // 
            this.scTraceDetailAndSourceCode.Panel1.Controls.Add(this.rbSmartTraceFilter_MethodName);
            this.scTraceDetailAndSourceCode.Panel1.Controls.Add(this.tvSmartTrace);
            this.scTraceDetailAndSourceCode.Panel1.Controls.Add(this.rbSmartTraceFilter_SourceCode);
            this.scTraceDetailAndSourceCode.Panel1.Controls.Add(this.rbSmartTraceFilter_Context);
            // 
            // scTraceDetailAndSourceCode.Panel2
            // 
            this.scTraceDetailAndSourceCode.Panel2.BackColor = System.Drawing.Color.White;
            this.scTraceDetailAndSourceCode.Panel2.Controls.Add(this.sceSourceCodeEditor);
            this.scTraceDetailAndSourceCode.Size = new System.Drawing.Size(841, 321);
            this.scTraceDetailAndSourceCode.SplitterDistance = 511;
            this.scTraceDetailAndSourceCode.TabIndex = 19;
            // 
            // rbSmartTraceFilter_MethodName
            // 
            this.rbSmartTraceFilter_MethodName.AutoSize = true;
            this.rbSmartTraceFilter_MethodName.Checked = true;
            this.rbSmartTraceFilter_MethodName.Location = new System.Drawing.Point(3, 3);
            this.rbSmartTraceFilter_MethodName.Name = "rbSmartTraceFilter_MethodName";
            this.rbSmartTraceFilter_MethodName.Size = new System.Drawing.Size(92, 17);
            this.rbSmartTraceFilter_MethodName.TabIndex = 14;
            this.rbSmartTraceFilter_MethodName.TabStop = true;
            this.rbSmartTraceFilter_MethodName.Text = "Method Name";
            this.rbSmartTraceFilter_MethodName.UseVisualStyleBackColor = true;
            this.rbSmartTraceFilter_MethodName.CheckedChanged += new System.EventHandler(this.rbSmartTraceFilter_MethodName_CheckedChanged);
            // 
            // tvSmartTrace
            // 
            this.tvSmartTrace.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tvSmartTrace.Location = new System.Drawing.Point(3, 28);
            this.tvSmartTrace.Name = "tvSmartTrace";
            this.tvSmartTrace.Size = new System.Drawing.Size(501, 286);
            this.tvSmartTrace.TabIndex = 12;
            this.tvSmartTrace.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvSmartTrace_AfterSelect);
            // 
            // rbSmartTraceFilter_SourceCode
            // 
            this.rbSmartTraceFilter_SourceCode.AutoSize = true;
            this.rbSmartTraceFilter_SourceCode.Location = new System.Drawing.Point(168, 3);
            this.rbSmartTraceFilter_SourceCode.Name = "rbSmartTraceFilter_SourceCode";
            this.rbSmartTraceFilter_SourceCode.Size = new System.Drawing.Size(87, 17);
            this.rbSmartTraceFilter_SourceCode.TabIndex = 17;
            this.rbSmartTraceFilter_SourceCode.Text = "Source Code";
            this.rbSmartTraceFilter_SourceCode.UseVisualStyleBackColor = true;
            this.rbSmartTraceFilter_SourceCode.CheckedChanged += new System.EventHandler(this.rbSmartTraceFilter_SourceCode_CheckedChanged);
            // 
            // rbSmartTraceFilter_Context
            // 
            this.rbSmartTraceFilter_Context.AutoSize = true;
            this.rbSmartTraceFilter_Context.Location = new System.Drawing.Point(101, 3);
            this.rbSmartTraceFilter_Context.Name = "rbSmartTraceFilter_Context";
            this.rbSmartTraceFilter_Context.Size = new System.Drawing.Size(61, 17);
            this.rbSmartTraceFilter_Context.TabIndex = 15;
            this.rbSmartTraceFilter_Context.Text = "Context";
            this.rbSmartTraceFilter_Context.UseVisualStyleBackColor = true;
            this.rbSmartTraceFilter_Context.CheckedChanged += new System.EventHandler(this.rbSmartTraceFilter_Context_CheckedChanged);
            // 
            // sceSourceCodeEditor
            // 
            this.sceSourceCodeEditor.AllowDrop = true;
            this.sceSourceCodeEditor.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.sceSourceCodeEditor.BackColor = System.Drawing.SystemColors.Control;
            this.sceSourceCodeEditor.ForeColor = System.Drawing.Color.Black;
            this.sceSourceCodeEditor.Location = new System.Drawing.Point(0, -54);
            this.sceSourceCodeEditor.Name = "sceSourceCodeEditor";
            this.sceSourceCodeEditor.Size = new System.Drawing.Size(324, 373);
            this.sceSourceCodeEditor.TabIndex = 17;
            // 
            // scCustomRulesGlee
            // 
            this.scCustomRulesGlee.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.scCustomRulesGlee.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scCustomRulesGlee.Location = new System.Drawing.Point(0, 0);
            this.scCustomRulesGlee.Name = "scCustomRulesGlee";
            this.scCustomRulesGlee.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // scCustomRulesGlee.Panel2
            // 
            this.scCustomRulesGlee.Panel2.Controls.Add(this.btGLEE_PlotAllTracesInInAnewWindow);
            this.scCustomRulesGlee.Panel2.Controls.Add(this.aGLEE);
            this.scCustomRulesGlee.Size = new System.Drawing.Size(841, 377);
            this.scCustomRulesGlee.SplitterDistance = 63;
            this.scCustomRulesGlee.TabIndex = 0;
            // 
            // btGLEE_PlotAllTracesInInAnewWindow
            // 
            this.btGLEE_PlotAllTracesInInAnewWindow.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btGLEE_PlotAllTracesInInAnewWindow.Location = new System.Drawing.Point(3, 279);
            this.btGLEE_PlotAllTracesInInAnewWindow.Name = "btGLEE_PlotAllTracesInInAnewWindow";
            this.btGLEE_PlotAllTracesInInAnewWindow.Size = new System.Drawing.Size(239, 24);
            this.btGLEE_PlotAllTracesInInAnewWindow.TabIndex = 66;
            this.btGLEE_PlotAllTracesInInAnewWindow.Text = "Show Graph of all Traces on Finding List";
            this.btGLEE_PlotAllTracesInInAnewWindow.UseVisualStyleBackColor = true;
            this.btGLEE_PlotAllTracesInInAnewWindow.Click += new System.EventHandler(this.btGLEE_PlotAllTracesInInAnewWindow_Click);
            // 
            // aGLEE
            // 
            this.aGLEE.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.aGLEE.BackColor = System.Drawing.SystemColors.Control;
            this.aGLEE.ForeColor = System.Drawing.Color.Black;
            this.aGLEE.Location = new System.Drawing.Point(-3, -2);
            this.aGLEE.Name = "aGLEE";
            this.aGLEE.Size = new System.Drawing.Size(840, 281);
            this.aGLEE.TabIndex = 88;
            // 
            // cbBrowseLoadedAssessmentFile_IgnoreRootCallInvocation
            // 
            this.cbBrowseLoadedAssessmentFile_IgnoreRootCallInvocation.AutoSize = true;
            this.cbBrowseLoadedAssessmentFile_IgnoreRootCallInvocation.Checked = true;
            this.cbBrowseLoadedAssessmentFile_IgnoreRootCallInvocation.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbBrowseLoadedAssessmentFile_IgnoreRootCallInvocation.Location = new System.Drawing.Point(3, 26);
            this.cbBrowseLoadedAssessmentFile_IgnoreRootCallInvocation.Name = "cbBrowseLoadedAssessmentFile_IgnoreRootCallInvocation";
            this.cbBrowseLoadedAssessmentFile_IgnoreRootCallInvocation.Size = new System.Drawing.Size(175, 17);
            this.cbBrowseLoadedAssessmentFile_IgnoreRootCallInvocation.TabIndex = 55;
            this.cbBrowseLoadedAssessmentFile_IgnoreRootCallInvocation.Text = "Ignore Root Call Invocation (IR)";
            this.cbBrowseLoadedAssessmentFile_IgnoreRootCallInvocation.UseVisualStyleBackColor = true;
            // 
            // cbBrowseLoadedAssessmentFile_DropDuplicateSmartTraces
            // 
            this.cbBrowseLoadedAssessmentFile_DropDuplicateSmartTraces.AutoSize = true;
            this.cbBrowseLoadedAssessmentFile_DropDuplicateSmartTraces.Checked = true;
            this.cbBrowseLoadedAssessmentFile_DropDuplicateSmartTraces.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbBrowseLoadedAssessmentFile_DropDuplicateSmartTraces.Location = new System.Drawing.Point(3, 3);
            this.cbBrowseLoadedAssessmentFile_DropDuplicateSmartTraces.Name = "cbBrowseLoadedAssessmentFile_DropDuplicateSmartTraces";
            this.cbBrowseLoadedAssessmentFile_DropDuplicateSmartTraces.Size = new System.Drawing.Size(185, 17);
            this.cbBrowseLoadedAssessmentFile_DropDuplicateSmartTraces.TabIndex = 54;
            this.cbBrowseLoadedAssessmentFile_DropDuplicateSmartTraces.Text = "Drop Duplicate SmartTraces (ND)";
            this.cbBrowseLoadedAssessmentFile_DropDuplicateSmartTraces.UseVisualStyleBackColor = true;
            // 
            // rbFindingsFilter_SmartTraces_Unique_Lost_Sinks
            // 
            this.rbFindingsFilter_SmartTraces_Unique_Lost_Sinks.AutoSize = true;
            this.rbFindingsFilter_SmartTraces_Unique_Lost_Sinks.Location = new System.Drawing.Point(7, 62);
            this.rbFindingsFilter_SmartTraces_Unique_Lost_Sinks.Name = "rbFindingsFilter_SmartTraces_Unique_Lost_Sinks";
            this.rbFindingsFilter_SmartTraces_Unique_Lost_Sinks.Size = new System.Drawing.Size(186, 17);
            this.rbFindingsFilter_SmartTraces_Unique_Lost_Sinks.TabIndex = 64;
            this.rbFindingsFilter_SmartTraces_Unique_Lost_Sinks.Text = "Smart Traces - UNIQUE Lost Sink";
            this.rbFindingsFilter_SmartTraces_Unique_Lost_Sinks.UseVisualStyleBackColor = true;
            this.rbFindingsFilter_SmartTraces_Unique_Lost_Sinks.CheckedChanged += new System.EventHandler(this.rbFindingsFilter_SmartTraces_Unique_Lost_Sinks_CheckedChanged);
            // 
            // rbFindingsFilter_SmartTraces_Lost_Sinks
            // 
            this.rbFindingsFilter_SmartTraces_Lost_Sinks.AutoSize = true;
            this.rbFindingsFilter_SmartTraces_Lost_Sinks.Location = new System.Drawing.Point(7, 39);
            this.rbFindingsFilter_SmartTraces_Lost_Sinks.Name = "rbFindingsFilter_SmartTraces_Lost_Sinks";
            this.rbFindingsFilter_SmartTraces_Lost_Sinks.Size = new System.Drawing.Size(141, 17);
            this.rbFindingsFilter_SmartTraces_Lost_Sinks.TabIndex = 63;
            this.rbFindingsFilter_SmartTraces_Lost_Sinks.Text = "Smart Traces - Lost Sink";
            this.rbFindingsFilter_SmartTraces_Lost_Sinks.UseVisualStyleBackColor = true;
            this.rbFindingsFilter_SmartTraces_Lost_Sinks.CheckedChanged += new System.EventHandler(this.rbFindingsFilter_SmartTraces_Lost_Sinks_CheckedChanged);
            // 
            // rbFindingsFilter_NoTraces
            // 
            this.rbFindingsFilter_NoTraces.AutoSize = true;
            this.rbFindingsFilter_NoTraces.Location = new System.Drawing.Point(7, 83);
            this.rbFindingsFilter_NoTraces.Name = "rbFindingsFilter_NoTraces";
            this.rbFindingsFilter_NoTraces.Size = new System.Drawing.Size(105, 17);
            this.rbFindingsFilter_NoTraces.TabIndex = 62;
            this.rbFindingsFilter_NoTraces.Text = "No Smart Traces";
            this.rbFindingsFilter_NoTraces.UseVisualStyleBackColor = true;
            this.rbFindingsFilter_NoTraces.CheckedChanged += new System.EventHandler(this.rbFindingsFilter_NoTraces_CheckedChanged);
            // 
            // rbFindingsFilter_SmartTraces
            // 
            this.rbFindingsFilter_SmartTraces.AutoSize = true;
            this.rbFindingsFilter_SmartTraces.Checked = true;
            this.rbFindingsFilter_SmartTraces.Location = new System.Drawing.Point(96, 16);
            this.rbFindingsFilter_SmartTraces.Name = "rbFindingsFilter_SmartTraces";
            this.rbFindingsFilter_SmartTraces.Size = new System.Drawing.Size(88, 17);
            this.rbFindingsFilter_SmartTraces.TabIndex = 61;
            this.rbFindingsFilter_SmartTraces.TabStop = true;
            this.rbFindingsFilter_SmartTraces.Text = "Smart Traces";
            this.rbFindingsFilter_SmartTraces.UseVisualStyleBackColor = true;
            this.rbFindingsFilter_SmartTraces.CheckedChanged += new System.EventHandler(this.rbFindingsFilter_SmartTraces_CheckedChanged);
            // 
            // rbFindingsFilter_AllFindings
            // 
            this.rbFindingsFilter_AllFindings.AutoSize = true;
            this.rbFindingsFilter_AllFindings.Location = new System.Drawing.Point(6, 16);
            this.rbFindingsFilter_AllFindings.Name = "rbFindingsFilter_AllFindings";
            this.rbFindingsFilter_AllFindings.Size = new System.Drawing.Size(78, 17);
            this.rbFindingsFilter_AllFindings.TabIndex = 60;
            this.rbFindingsFilter_AllFindings.Text = "All Findings";
            this.rbFindingsFilter_AllFindings.UseVisualStyleBackColor = true;
            this.rbFindingsFilter_AllFindings.CheckedChanged += new System.EventHandler(this.rbFindingsFilter_AllFindings_CheckedChanged);
            // 
            // btFilterByCustomScript
            // 
            this.btFilterByCustomScript.Location = new System.Drawing.Point(3, 155);
            this.btFilterByCustomScript.Name = "btFilterByCustomScript";
            this.btFilterByCustomScript.Size = new System.Drawing.Size(161, 23);
            this.btFilterByCustomScript.TabIndex = 65;
            this.btFilterByCustomScript.Text = "Custom Script";
            this.btFilterByCustomScript.UseVisualStyleBackColor = true;
            this.btFilterByCustomScript.Click += new System.EventHandler(this.btFilterByCustomScript_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.rbFindingsFilter_SmartTraces_Lost_Sinks);
            this.groupBox2.Controls.Add(this.rbFindingsFilter_AllFindings);
            this.groupBox2.Controls.Add(this.rbFindingsFilter_SmartTraces_Unique_Lost_Sinks);
            this.groupBox2.Controls.Add(this.rbFindingsFilter_SmartTraces);
            this.groupBox2.Controls.Add(this.rbFindingsFilter_NoTraces);
            this.groupBox2.Location = new System.Drawing.Point(4, 166);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(271, 110);
            this.groupBox2.TabIndex = 67;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Global Filters";
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(4, 286);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(271, 413);
            this.tabControl1.TabIndex = 68;
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage2.Controls.Add(this.btFilterBySelectedFilter);
            this.tabPage2.Controls.Add(this.btFilterByActionObject);
            this.tabPage2.Controls.Add(this.btViewByVulnerabilityType);
            this.tabPage2.Controls.Add(this.btFilterByCustomScript);
            this.tabPage2.Controls.Add(this.btViewByFiles);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(263, 387);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Generic Filters";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // btFilterBySelectedFilter
            // 
            this.btFilterBySelectedFilter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.btFilterBySelectedFilter.Location = new System.Drawing.Point(2, 10);
            this.btFilterBySelectedFilter.Name = "btFilterBySelectedFilter";
            this.btFilterBySelectedFilter.Size = new System.Drawing.Size(161, 23);
            this.btFilterBySelectedFilter.TabIndex = 66;
            this.btFilterBySelectedFilter.Text = "Selected Filter";
            this.btFilterBySelectedFilter.UseVisualStyleBackColor = true;
            this.btFilterBySelectedFilter.Click += new System.EventHandler(this.btFilterBySelectedFilter_Click);
            // 
            // scTopLevelContainer_StatsAndRest
            // 
            this.scTopLevelContainer_StatsAndRest.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.scTopLevelContainer_StatsAndRest.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.scTopLevelContainer_StatsAndRest.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.scTopLevelContainer_StatsAndRest.Location = new System.Drawing.Point(3, 44);
            this.scTopLevelContainer_StatsAndRest.Name = "scTopLevelContainer_StatsAndRest";
            // 
            // scTopLevelContainer_StatsAndRest.Panel1
            // 
            this.scTopLevelContainer_StatsAndRest.Panel1.Controls.Add(this.cbBrowseLoadedAssessmentFile_DropDuplicateSmartTraces);
            this.scTopLevelContainer_StatsAndRest.Panel1.Controls.Add(this.tabControl1);
            this.scTopLevelContainer_StatsAndRest.Panel1.Controls.Add(this.groupBox1);
            this.scTopLevelContainer_StatsAndRest.Panel1.Controls.Add(this.groupBox2);
            this.scTopLevelContainer_StatsAndRest.Panel1.Controls.Add(this.cbBrowseLoadedAssessmentFile_IgnoreRootCallInvocation);
            // 
            // scTopLevelContainer_StatsAndRest.Panel2
            // 
            this.scTopLevelContainer_StatsAndRest.Panel2.Controls.Add(this.splitContainer1);
            this.scTopLevelContainer_StatsAndRest.Size = new System.Drawing.Size(1471, 706);
            this.scTopLevelContainer_StatsAndRest.SplitterDistance = 282;
            this.scTopLevelContainer_StatsAndRest.TabIndex = 69;
            // 
            // gbVisibleControls
            // 
            this.gbVisibleControls.Controls.Add(this.cbVisibleControls_CustomRules);
            this.gbVisibleControls.Controls.Add(this.cbVisibleControls_SourceCode);
            this.gbVisibleControls.Controls.Add(this.cbVisibleControls_StatsAndFilters);
            this.gbVisibleControls.Controls.Add(this.cbVisibleControls_GraphStats);
            this.gbVisibleControls.Controls.Add(this.cbVisibleControls_TraceDetails);
            this.gbVisibleControls.ForeColor = System.Drawing.Color.Gray;
            this.gbVisibleControls.Location = new System.Drawing.Point(189, 5);
            this.gbVisibleControls.Name = "gbVisibleControls";
            this.gbVisibleControls.Size = new System.Drawing.Size(571, 35);
            this.gbVisibleControls.TabIndex = 99;
            this.gbVisibleControls.TabStop = false;
            this.gbVisibleControls.Text = "ViewAssessmentRun Select Visible Controls";
            // 
            // cbVisibleControls_CustomRules
            // 
            this.cbVisibleControls_CustomRules.AutoSize = true;
            this.cbVisibleControls_CustomRules.Location = new System.Drawing.Point(388, 13);
            this.cbVisibleControls_CustomRules.Name = "cbVisibleControls_CustomRules";
            this.cbVisibleControls_CustomRules.Size = new System.Drawing.Size(91, 17);
            this.cbVisibleControls_CustomRules.TabIndex = 98;
            this.cbVisibleControls_CustomRules.Text = "Custom Rules";
            this.cbVisibleControls_CustomRules.UseVisualStyleBackColor = true;
            this.cbVisibleControls_CustomRules.CheckedChanged += new System.EventHandler(this.cbVisibleControls_CustomRules_CheckedChanged);
            // 
            // cbVisibleControls_SourceCode
            // 
            this.cbVisibleControls_SourceCode.AutoSize = true;
            this.cbVisibleControls_SourceCode.Location = new System.Drawing.Point(212, 14);
            this.cbVisibleControls_SourceCode.Name = "cbVisibleControls_SourceCode";
            this.cbVisibleControls_SourceCode.Size = new System.Drawing.Size(88, 17);
            this.cbVisibleControls_SourceCode.TabIndex = 97;
            this.cbVisibleControls_SourceCode.Text = "Source Code";
            this.cbVisibleControls_SourceCode.UseVisualStyleBackColor = true;
            this.cbVisibleControls_SourceCode.CheckedChanged += new System.EventHandler(this.cbVisibleControls_SourceCode_CheckedChanged);
            // 
            // cbVisibleControls_StatsAndFilters
            // 
            this.cbVisibleControls_StatsAndFilters.AutoSize = true;
            this.cbVisibleControls_StatsAndFilters.FlatAppearance.CheckedBackColor = System.Drawing.Color.Red;
            this.cbVisibleControls_StatsAndFilters.Location = new System.Drawing.Point(12, 14);
            this.cbVisibleControls_StatsAndFilters.Name = "cbVisibleControls_StatsAndFilters";
            this.cbVisibleControls_StatsAndFilters.Size = new System.Drawing.Size(101, 17);
            this.cbVisibleControls_StatsAndFilters.TabIndex = 95;
            this.cbVisibleControls_StatsAndFilters.Text = "Stats and Filters";
            this.cbVisibleControls_StatsAndFilters.UseVisualStyleBackColor = true;
            this.cbVisibleControls_StatsAndFilters.CheckedChanged += new System.EventHandler(this.cbVisibleControls_StatsAndFilters_CheckedChanged);
            // 
            // cbVisibleControls_GraphStats
            // 
            this.cbVisibleControls_GraphStats.AutoSize = true;
            this.cbVisibleControls_GraphStats.Checked = true;
            this.cbVisibleControls_GraphStats.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbVisibleControls_GraphStats.Location = new System.Drawing.Point(299, 14);
            this.cbVisibleControls_GraphStats.Name = "cbVisibleControls_GraphStats";
            this.cbVisibleControls_GraphStats.Size = new System.Drawing.Size(86, 17);
            this.cbVisibleControls_GraphStats.TabIndex = 96;
            this.cbVisibleControls_GraphStats.Text = "Graph (Glee)";
            this.cbVisibleControls_GraphStats.UseVisualStyleBackColor = true;
            this.cbVisibleControls_GraphStats.CheckedChanged += new System.EventHandler(this.cbVisibleControls_GraphStats_CheckedChanged);
            // 
            // cbVisibleControls_TraceDetails
            // 
            this.cbVisibleControls_TraceDetails.AutoSize = true;
            this.cbVisibleControls_TraceDetails.Checked = true;
            this.cbVisibleControls_TraceDetails.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbVisibleControls_TraceDetails.Location = new System.Drawing.Point(120, 14);
            this.cbVisibleControls_TraceDetails.Name = "cbVisibleControls_TraceDetails";
            this.cbVisibleControls_TraceDetails.Size = new System.Drawing.Size(89, 17);
            this.cbVisibleControls_TraceDetails.TabIndex = 94;
            this.cbVisibleControls_TraceDetails.Text = "Trace Details";
            this.cbVisibleControls_TraceDetails.UseVisualStyleBackColor = true;
            this.cbVisibleControls_TraceDetails.CheckedChanged += new System.EventHandler(this.cbVisibleControls_TraceDetails_CheckedChanged);
            // 
            // dndDropArea
            // 
            this.dndDropArea.AllowDrop = true;
            this.dndDropArea.BackColor = System.Drawing.Color.Maroon;
            this.dndDropArea.ForeColor = System.Drawing.Color.White;
            this.dndDropArea.Location = new System.Drawing.Point(14, 7);
            this.dndDropArea.Name = "dndDropArea";
            this.dndDropArea.Size = new System.Drawing.Size(169, 31);
            this.dndDropArea.TabIndex = 0;
            this.dndDropArea.Text = "Drop Content Here!!";
            this.dndDropArea.Load += new System.EventHandler(this.dndDropArea_Load);
            this.dndDropArea.eDnDAction_ObjectDataReceived_Event += new O2.Kernel.CodeUtils.Callbacks.dMethod_Object(this.dndDropArea_eDnDAction_ObjectDataReceived_Event);
            // 
            // cbAutoTryToResolveSourceCodePaths
            // 
            this.cbAutoTryToResolveSourceCodePaths.AutoSize = true;
            this.cbAutoTryToResolveSourceCodePaths.Location = new System.Drawing.Point(3, 682);
            this.cbAutoTryToResolveSourceCodePaths.Name = "cbAutoTryToResolveSourceCodePaths";
            this.cbAutoTryToResolveSourceCodePaths.Size = new System.Drawing.Size(180, 17);
            this.cbAutoTryToResolveSourceCodePaths.TabIndex = 62;
            this.cbAutoTryToResolveSourceCodePaths.Text = "Auto try to resolve file references";
            this.cbAutoTryToResolveSourceCodePaths.UseVisualStyleBackColor = true;
            this.cbAutoTryToResolveSourceCodePaths.CheckedChanged += new System.EventHandler(this.cbAutoTryToResolveSourceCodePaths_CheckedChanged);
            // 
            // ascx_ViewAssessmentRun
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.gbVisibleControls);
            this.Controls.Add(this.scTopLevelContainer_StatsAndRest);
            this.Controls.Add(this.dndDropArea);
            this.Controls.Add(this.lbAssessmentFileLoaded);
            this.Controls.Add(this.label23);
            this.ForeColor = System.Drawing.Color.Black;
            this.Name = "ascx_ViewAssessmentRun";
            this.Size = new System.Drawing.Size(1477, 753);
            this.Load += new System.EventHandler(this.ascx_ViewAssessmentRun_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvFindingData)).EndInit();
            this.scHostsTracesRulesGlee.Panel1.ResumeLayout(false);
            this.scHostsTracesRulesGlee.Panel2.ResumeLayout(false);
            this.scHostsTracesRulesGlee.ResumeLayout(false);
            this.scTraceDetailAndSourceCode.Panel1.ResumeLayout(false);
            this.scTraceDetailAndSourceCode.Panel1.PerformLayout();
            this.scTraceDetailAndSourceCode.Panel2.ResumeLayout(false);
            this.scTraceDetailAndSourceCode.ResumeLayout(false);
            this.scCustomRulesGlee.Panel2.ResumeLayout(false);
            this.scCustomRulesGlee.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.scTopLevelContainer_StatsAndRest.Panel1.ResumeLayout(false);
            this.scTopLevelContainer_StatsAndRest.Panel1.PerformLayout();
            this.scTopLevelContainer_StatsAndRest.Panel2.ResumeLayout(false);
            this.scTopLevelContainer_StatsAndRest.ResumeLayout(false);
            this.gbVisibleControls.ResumeLayout(false);
            this.gbVisibleControls.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ascx_DropObject dndDropArea;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lbLoadTime;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Label lbNumberOfAssessmentFiles;
        private System.Windows.Forms.Label lbNumberOfLostSinks;
        private System.Windows.Forms.Label lbNumberOfSmartTraces;
        private System.Windows.Forms.Label lbNumberOfFindings;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label lbAssessmentFileLoaded;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Button btViewByFiles;
        private System.Windows.Forms.Button btViewByVulnerabilityType;
        private System.Windows.Forms.Button btFilterByActionObject;
        private System.Windows.Forms.TreeView tvFindingsList;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Label lbFilter;
        private System.Windows.Forms.CheckBox cbBrowseLoadedAssessmentFile_IgnoreRootCallInvocation;
        private System.Windows.Forms.CheckBox cbBrowseLoadedAssessmentFile_DropDuplicateSmartTraces;
        private System.Windows.Forms.Label lbActionObjectFilter;
        private System.Windows.Forms.Label lnActionObjectNodes;
        private System.Windows.Forms.Label lbBrowseAssessment_NumberOfItemsLoadedIn_ByActionObject;
        private System.Windows.Forms.ComboBox cbActionObject_NameFilter;
        private System.Windows.Forms.RadioButton rbFindingsFilter_SmartTraces_Unique_Lost_Sinks;
        private System.Windows.Forms.RadioButton rbFindingsFilter_SmartTraces_Lost_Sinks;
        private System.Windows.Forms.RadioButton rbFindingsFilter_NoTraces;
        private System.Windows.Forms.RadioButton rbFindingsFilter_SmartTraces;
        private System.Windows.Forms.RadioButton rbFindingsFilter_AllFindings;
        private System.Windows.Forms.RadioButton rbSmartTraceFilter_Context;
        private System.Windows.Forms.RadioButton rbSmartTraceFilter_MethodName;
        private System.Windows.Forms.DataGridView dgvFindingData;
        private System.Windows.Forms.DataGridViewTextBoxColumn sName;
        private System.Windows.Forms.DataGridViewTextBoxColumn sValue;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.TreeView tvSmartTrace;
        private System.Windows.Forms.RadioButton rbSmartTraceFilter_SourceCode;
        private System.Windows.Forms.SplitContainer scHostsTracesRulesGlee;
        private System.Windows.Forms.Button btFilterByCustomScript;
        private System.Windows.Forms.SplitContainer scTraceDetailAndSourceCode;
        private System.Windows.Forms.Button btGLEE_PlotAllTracesInInAnewWindow;        
        private ascx_SourceCodeEditor sceSourceCodeEditor;
        private System.Windows.Forms.Button btCollapseAll;
        private System.Windows.Forms.ImageList ilSolutionsProjecstFiles;
        private System.Windows.Forms.Button btExpandAll;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.SplitContainer scTopLevelContainer_StatsAndRest;
        private ascx_Glee aGLEE;
        private System.Windows.Forms.SplitContainer scCustomRulesGlee;
        private System.Windows.Forms.Button btFilterBySelectedFilter;        
        private System.Windows.Forms.GroupBox gbVisibleControls;
        private System.Windows.Forms.CheckBox cbVisibleControls_StatsAndFilters;
        private System.Windows.Forms.CheckBox cbVisibleControls_GraphStats;
        private System.Windows.Forms.CheckBox cbVisibleControls_TraceDetails;
        private System.Windows.Forms.CheckBox cbVisibleControls_SourceCode;
        private System.Windows.Forms.CheckBox cbVisibleControls_CustomRules;
        private System.Windows.Forms.CheckBox cbAutoTryToResolveSourceCodePaths;

  
    }
}