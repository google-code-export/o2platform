// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using O2.Legacy.OunceV6.TraceViewer;
using O2.Views.ASCX.CoreControls;

namespace O2.Tool.SearchAssessmentRun.Ascx
{
    partial class ascx_SearchAssessmentRun
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ascx_SearchAssessmentRun));
            this.dgvSearchCriteria = new System.Windows.Forms.DataGridView();
            this.sName = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.sValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NegativeSearch = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.scTopRight = new System.Windows.Forms.SplitContainer();
            this.llSearchCriteria_ClearSearchCriteria = new System.Windows.Forms.LinkLabel();
            this.llSearchCriteria_DeleteSelectedRow = new System.Windows.Forms.LinkLabel();
            this.cbSearchTypeNegative = new System.Windows.Forms.CheckBox();
            this.btAddSearchCriteria = new System.Windows.Forms.Button();
            this.tbSearchTextToAdd = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.lbAvailableSearchType = new System.Windows.Forms.ListBox();
            this.cbSearchOnFindingsWithNoTraces = new System.Windows.Forms.CheckBox();
            this.btExecuteSearch = new System.Windows.Forms.Button();
            this.lbPreviousSearches = new System.Windows.Forms.ListBox();
            this.label7 = new System.Windows.Forms.Label();
            this.scSearchResults = new System.Windows.Forms.SplitContainer();
            this.ascx_FindingsSearchViewer1 = new ascx_FindingsSearchViewer();
            this.ascx_TraceViewer1 = new ascx_TraceViewer();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.wbInfo = new System.Windows.Forms.WebBrowser();
            this.scTopLevelOne = new System.Windows.Forms.SplitContainer();
            this.scHost = new System.Windows.Forms.SplitContainer();
            this.scTop = new System.Windows.Forms.SplitContainer();
            this.cbUnLoadAssessmentFilesOnDoubleClick = new System.Windows.Forms.CheckBox();
            this.lbAssessmentFilesThatNeedSourceCodePathFixing = new System.Windows.Forms.ListBox();
            this.lbFixSourceCodeFilereferences = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lbLoadedAssessmentFiles = new System.Windows.Forms.ListBox();
            this.ascx_DropObject1 = new O2.Views.ASCX.CoreControls.ascx_DropObject();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.scBottom = new System.Windows.Forms.SplitContainer();
            this.lbNumberOfSearchResults = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.lbRemoveFindingsFromSearces = new System.Windows.Forms.ListBox();
            this.ado_RemoveFindingsFromSearches = new O2.Views.ASCX.CoreControls.ascx_DropObject();
            this.cbRemoveFindingsFromAssessmentsFromView = new System.Windows.Forms.CheckBox();
            this.label14 = new System.Windows.Forms.Label();
            this.scCreateCustomAssessmentFiles = new System.Windows.Forms.SplitContainer();
            this.tv_CreateSavedAssessment_PerFindingsType = new System.Windows.Forms.TreeView();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btCreateAssessmentRun_WithSelectedFindingsType = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cbCreateFileWithAllTraces = new System.Windows.Forms.CheckBox();
            this.cbCreateFileWithUniqueTraces = new System.Windows.Forms.CheckBox();
            this.cbDropDuplicateSmartTraces = new System.Windows.Forms.CheckBox();
            this.cbIgnoreRootCallInvocation = new System.Windows.Forms.CheckBox();
            this.btCreateAssessmentRunWithSearchResults = new System.Windows.Forms.Button();
            this.btCreateCustomAssessmentFiles_LoadData = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.tbSavedAssessment_FolderName = new System.Windows.Forms.TextBox();
            this.tbSavedAssessmentFileName = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.llDownloadDemoFile_FromTempDirectory = new System.Windows.Forms.LinkLabel();
            this.llDownloadDemoFile_WebGoat = new System.Windows.Forms.LinkLabel();
            this.llDownloadDemoFile_HacmeBank_Website = new System.Windows.Forms.LinkLabel();
            this.llDownloadDemoFile_HacmeBank_WebServices = new System.Windows.Forms.LinkLabel();
            this.ascx_svpSearchAssessmentRun = new O2.Views.ASCX.CoreControls.ascx_SelectVisiblePanels();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSearchCriteria)).BeginInit();
            this.scTopRight.Panel1.SuspendLayout();
            this.scTopRight.Panel2.SuspendLayout();
            this.scTopRight.SuspendLayout();
            this.scSearchResults.Panel1.SuspendLayout();
            this.scSearchResults.Panel2.SuspendLayout();
            this.scSearchResults.SuspendLayout();
            this.scTopLevelOne.Panel1.SuspendLayout();
            this.scTopLevelOne.Panel2.SuspendLayout();
            this.scTopLevelOne.SuspendLayout();
            this.scHost.Panel1.SuspendLayout();
            this.scHost.Panel2.SuspendLayout();
            this.scHost.SuspendLayout();
            this.scTop.Panel1.SuspendLayout();
            this.scTop.Panel2.SuspendLayout();
            this.scTop.SuspendLayout();
            this.scBottom.Panel1.SuspendLayout();
            this.scBottom.Panel2.SuspendLayout();
            this.scBottom.SuspendLayout();
            this.scCreateCustomAssessmentFiles.Panel1.SuspendLayout();
            this.scCreateCustomAssessmentFiles.Panel2.SuspendLayout();
            this.scCreateCustomAssessmentFiles.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvSearchCriteria
            // 
            this.dgvSearchCriteria.AllowUserToOrderColumns = true;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.Silver;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.Gainsboro;
            this.dgvSearchCriteria.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvSearchCriteria.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                                                                                   | System.Windows.Forms.AnchorStyles.Left)
                                                                                  | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvSearchCriteria.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSearchCriteria.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
                                                                                                      this.sName,
                                                                                                      this.sValue,
                                                                                                      this.NegativeSearch});
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.Gray;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.Gainsboro;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.GradientInactiveCaption;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvSearchCriteria.DefaultCellStyle = dataGridViewCellStyle5;
            this.dgvSearchCriteria.Location = new System.Drawing.Point(127, 22);
            this.dgvSearchCriteria.Name = "dgvSearchCriteria";
            this.dgvSearchCriteria.RowHeadersWidth = 20;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.DarkGray;
            dataGridViewCellStyle6.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.Gainsboro;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.Color.Black;
            this.dgvSearchCriteria.RowsDefaultCellStyle = dataGridViewCellStyle6;
            this.dgvSearchCriteria.Size = new System.Drawing.Size(273, 135);
            this.dgvSearchCriteria.TabIndex = 14;
            this.dgvSearchCriteria.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.dgvSearchCriteria_RowsAdded);
            this.dgvSearchCriteria.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.dgvSearchCriteria_KeyPress);
            // 
            // sName
            // 
            this.sName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.sName.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.ComboBox;
            this.sName.HeaderText = "Search Type";
            this.sName.Name = "sName";
            this.sName.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.sName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.sName.Width = 93;
            // 
            // sValue
            // 
            this.sValue.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.sValue.HeaderText = "Search Text";
            this.sValue.Name = "sValue";
            // 
            // NegativeSearch
            // 
            this.NegativeSearch.HeaderText = "!";
            this.NegativeSearch.Name = "NegativeSearch";
            this.NegativeSearch.Width = 20;
            // 
            // scTopRight
            // 
            this.scTopRight.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                                                                            | System.Windows.Forms.AnchorStyles.Left)
                                                                           | System.Windows.Forms.AnchorStyles.Right)));
            this.scTopRight.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.scTopRight.Location = new System.Drawing.Point(-2, 22);
            this.scTopRight.Name = "scTopRight";
            // 
            // scTopRight.Panel1
            // 
            this.scTopRight.Panel1.Controls.Add(this.llSearchCriteria_ClearSearchCriteria);
            this.scTopRight.Panel1.Controls.Add(this.llSearchCriteria_DeleteSelectedRow);
            this.scTopRight.Panel1.Controls.Add(this.cbSearchTypeNegative);
            this.scTopRight.Panel1.Controls.Add(this.btAddSearchCriteria);
            this.scTopRight.Panel1.Controls.Add(this.tbSearchTextToAdd);
            this.scTopRight.Panel1.Controls.Add(this.label12);
            this.scTopRight.Panel1.Controls.Add(this.label8);
            this.scTopRight.Panel1.Controls.Add(this.lbAvailableSearchType);
            this.scTopRight.Panel1.Controls.Add(this.cbSearchOnFindingsWithNoTraces);
            this.scTopRight.Panel1.Controls.Add(this.dgvSearchCriteria);
            this.scTopRight.Panel1.Controls.Add(this.btExecuteSearch);
            // 
            // scTopRight.Panel2
            // 
            this.scTopRight.Panel2.Controls.Add(this.lbPreviousSearches);
            this.scTopRight.Panel2.Controls.Add(this.label7);
            this.scTopRight.Size = new System.Drawing.Size(602, 183);
            this.scTopRight.SplitterDistance = 484;
            this.scTopRight.TabIndex = 21;
            // 
            // llSearchCriteria_ClearSearchCriteria
            // 
            this.llSearchCriteria_ClearSearchCriteria.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.llSearchCriteria_ClearSearchCriteria.AutoSize = true;
            this.llSearchCriteria_ClearSearchCriteria.Location = new System.Drawing.Point(251, 162);
            this.llSearchCriteria_ClearSearchCriteria.Name = "llSearchCriteria_ClearSearchCriteria";
            this.llSearchCriteria_ClearSearchCriteria.Size = new System.Drawing.Size(103, 13);
            this.llSearchCriteria_ClearSearchCriteria.TabIndex = 25;
            this.llSearchCriteria_ClearSearchCriteria.TabStop = true;
            this.llSearchCriteria_ClearSearchCriteria.Text = "Clear Search Criteria";
            this.llSearchCriteria_ClearSearchCriteria.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llSearchCriteria_ClearSearchCriteria_LinkClicked);
            // 
            // llSearchCriteria_DeleteSelectedRow
            // 
            this.llSearchCriteria_DeleteSelectedRow.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.llSearchCriteria_DeleteSelectedRow.AutoSize = true;
            this.llSearchCriteria_DeleteSelectedRow.Location = new System.Drawing.Point(127, 162);
            this.llSearchCriteria_DeleteSelectedRow.Name = "llSearchCriteria_DeleteSelectedRow";
            this.llSearchCriteria_DeleteSelectedRow.Size = new System.Drawing.Size(108, 13);
            this.llSearchCriteria_DeleteSelectedRow.TabIndex = 24;
            this.llSearchCriteria_DeleteSelectedRow.TabStop = true;
            this.llSearchCriteria_DeleteSelectedRow.Text = "Delete Selected Row";
            this.llSearchCriteria_DeleteSelectedRow.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llSearchCriteria_DeleteSelectedRow_LinkClicked);
            // 
            // cbSearchTypeNegative
            // 
            this.cbSearchTypeNegative.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cbSearchTypeNegative.AutoSize = true;
            this.cbSearchTypeNegative.Location = new System.Drawing.Point(5, 75);
            this.cbSearchTypeNegative.Name = "cbSearchTypeNegative";
            this.cbSearchTypeNegative.Size = new System.Drawing.Size(110, 17);
            this.cbSearchTypeNegative.TabIndex = 23;
            this.cbSearchTypeNegative.Text = "Negative search?";
            this.cbSearchTypeNegative.UseVisualStyleBackColor = true;
            // 
            // btAddSearchCriteria
            // 
            this.btAddSearchCriteria.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btAddSearchCriteria.Location = new System.Drawing.Point(5, 133);
            this.btAddSearchCriteria.Name = "btAddSearchCriteria";
            this.btAddSearchCriteria.Size = new System.Drawing.Size(115, 23);
            this.btAddSearchCriteria.TabIndex = 22;
            this.btAddSearchCriteria.Text = "3) Add ";
            this.btAddSearchCriteria.UseVisualStyleBackColor = true;
            this.btAddSearchCriteria.Click += new System.EventHandler(this.btAddSearchCriteria_Click);
            // 
            // tbSearchTextToAdd
            // 
            this.tbSearchTextToAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.tbSearchTextToAdd.Location = new System.Drawing.Point(6, 110);
            this.tbSearchTextToAdd.Name = "tbSearchTextToAdd";
            this.tbSearchTextToAdd.Size = new System.Drawing.Size(114, 20);
            this.tbSearchTextToAdd.TabIndex = 21;
            this.tbSearchTextToAdd.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbSearchTextToAdd_KeyDown);
            // 
            // label12
            // 
            this.label12.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(3, 95);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(109, 13);
            this.label12.TabIndex = 20;
            this.label12.Text = "2) Enter Text (RegEx)";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(1, 6);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(82, 13);
            this.label8.TabIndex = 19;
            this.label8.Text = "1) Choose Type";
            // 
            // lbAvailableSearchType
            // 
            this.lbAvailableSearchType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                                                                                      | System.Windows.Forms.AnchorStyles.Left)));
            this.lbAvailableSearchType.FormattingEnabled = true;
            this.lbAvailableSearchType.Location = new System.Drawing.Point(5, 22);
            this.lbAvailableSearchType.Name = "lbAvailableSearchType";
            this.lbAvailableSearchType.ScrollAlwaysVisible = true;
            this.lbAvailableSearchType.Size = new System.Drawing.Size(115, 43);
            this.lbAvailableSearchType.TabIndex = 18;
            // 
            // cbSearchOnFindingsWithNoTraces
            // 
            this.cbSearchOnFindingsWithNoTraces.AutoSize = true;
            this.cbSearchOnFindingsWithNoTraces.Location = new System.Drawing.Point(128, 5);
            this.cbSearchOnFindingsWithNoTraces.Name = "cbSearchOnFindingsWithNoTraces";
            this.cbSearchOnFindingsWithNoTraces.Size = new System.Drawing.Size(190, 17);
            this.cbSearchOnFindingsWithNoTraces.TabIndex = 17;
            this.cbSearchOnFindingsWithNoTraces.Text = "Search on Findings with NO traces";
            this.cbSearchOnFindingsWithNoTraces.UseVisualStyleBackColor = true;
            // 
            // btExecuteSearch
            // 
            this.btExecuteSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                                                                                | System.Windows.Forms.AnchorStyles.Right)));
            this.btExecuteSearch.Location = new System.Drawing.Point(406, 22);
            this.btExecuteSearch.Name = "btExecuteSearch";
            this.btExecuteSearch.Size = new System.Drawing.Size(75, 135);
            this.btExecuteSearch.TabIndex = 16;
            this.btExecuteSearch.Text = "4) Search";
            this.btExecuteSearch.UseVisualStyleBackColor = true;
            this.btExecuteSearch.Click += new System.EventHandler(this.btExecuteSearch_Click);
            // 
            // lbPreviousSearches
            // 
            this.lbPreviousSearches.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                                                                                    | System.Windows.Forms.AnchorStyles.Left)
                                                                                   | System.Windows.Forms.AnchorStyles.Right)));
            this.lbPreviousSearches.FormattingEnabled = true;
            this.lbPreviousSearches.Location = new System.Drawing.Point(2, 22);
            this.lbPreviousSearches.Name = "lbPreviousSearches";
            this.lbPreviousSearches.Size = new System.Drawing.Size(106, 134);
            this.lbPreviousSearches.TabIndex = 21;
            this.lbPreviousSearches.SelectedIndexChanged += new System.EventHandler(this.lbPreviousSearches_SelectedIndexChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(3, 6);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(76, 13);
            this.label7.TabIndex = 20;
            this.label7.Text = "Search History";
            // 
            // scSearchResults
            // 
            this.scSearchResults.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                                                                                 | System.Windows.Forms.AnchorStyles.Left)
                                                                                | System.Windows.Forms.AnchorStyles.Right)));
            this.scSearchResults.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.scSearchResults.Location = new System.Drawing.Point(-2, 22);
            this.scSearchResults.Name = "scSearchResults";
            // 
            // scSearchResults.Panel1
            // 
            this.scSearchResults.Panel1.Controls.Add(this.ascx_FindingsSearchViewer1);
            // 
            // scSearchResults.Panel2
            // 
            this.scSearchResults.Panel2.Controls.Add(this.ascx_TraceViewer1);
            this.scSearchResults.Size = new System.Drawing.Size(631, 317);
            this.scSearchResults.SplitterDistance = 388;
            this.scSearchResults.TabIndex = 0;
            // 
            // ascx_FindingsSearchViewer1
            // 
            this.ascx_FindingsSearchViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ascx_FindingsSearchViewer1.Location = new System.Drawing.Point(0, 0);
            this.ascx_FindingsSearchViewer1.Name = "ascx_FindingsSearchViewer1";
            this.ascx_FindingsSearchViewer1.Size = new System.Drawing.Size(384, 313);
            this.ascx_FindingsSearchViewer1.TabIndex = 0;
            // 
            // ascx_TraceViewer1
            // 
            this.ascx_TraceViewer1.BackColor = System.Drawing.SystemColors.Control;
            this.ascx_TraceViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ascx_TraceViewer1.ForeColor = System.Drawing.Color.Black;
            this.ascx_TraceViewer1.Location = new System.Drawing.Point(0, 0);
            this.ascx_TraceViewer1.Name = "ascx_TraceViewer1";
            this.ascx_TraceViewer1.Size = new System.Drawing.Size(235, 313);
            this.ascx_TraceViewer1.TabIndex = 0;
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "Findings_SmartAudit_NoResults.ico");
            this.imageList1.Images.SetKeyName(1, "SmartTrace.ico");
            // 
            // wbInfo
            // 
            this.wbInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.wbInfo.Location = new System.Drawing.Point(0, 0);
            this.wbInfo.MinimumSize = new System.Drawing.Size(20, 20);
            this.wbInfo.Name = "wbInfo";
            this.wbInfo.Size = new System.Drawing.Size(1113, 60);
            this.wbInfo.TabIndex = 16;
            // 
            // scTopLevelOne
            // 
            this.scTopLevelOne.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.scTopLevelOne.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scTopLevelOne.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.scTopLevelOne.Location = new System.Drawing.Point(0, 0);
            this.scTopLevelOne.Name = "scTopLevelOne";
            this.scTopLevelOne.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // scTopLevelOne.Panel1
            // 
            this.scTopLevelOne.Panel1.Controls.Add(this.wbInfo);
            // 
            // scTopLevelOne.Panel2
            // 
            this.scTopLevelOne.Panel2.Controls.Add(this.scHost);
            this.scTopLevelOne.Panel2.Controls.Add(this.groupBox1);
            this.scTopLevelOne.Panel2.Controls.Add(this.ascx_svpSearchAssessmentRun);
            this.scTopLevelOne.Size = new System.Drawing.Size(1117, 669);
            this.scTopLevelOne.SplitterDistance = 64;
            this.scTopLevelOne.TabIndex = 17;
            // 
            // scHost
            // 
            this.scHost.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                                                                        | System.Windows.Forms.AnchorStyles.Left)
                                                                       | System.Windows.Forms.AnchorStyles.Right)));
            this.scHost.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.scHost.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.scHost.Location = new System.Drawing.Point(3, 38);
            this.scHost.Name = "scHost";
            this.scHost.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // scHost.Panel1
            // 
            this.scHost.Panel1.Controls.Add(this.scTop);
            // 
            // scHost.Panel2
            // 
            this.scHost.Panel2.Controls.Add(this.scBottom);
            this.scHost.Size = new System.Drawing.Size(1107, 556);
            this.scHost.SplitterDistance = 209;
            this.scHost.TabIndex = 17;
            this.scHost.Enter += new System.EventHandler(this.scHost_Enter);
            // 
            // scTop
            // 
            this.scTop.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.scTop.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scTop.Location = new System.Drawing.Point(0, 0);
            this.scTop.Name = "scTop";
            // 
            // scTop.Panel1
            // 
            this.scTop.Panel1.Controls.Add(this.cbUnLoadAssessmentFilesOnDoubleClick);
            this.scTop.Panel1.Controls.Add(this.lbAssessmentFilesThatNeedSourceCodePathFixing);
            this.scTop.Panel1.Controls.Add(this.lbFixSourceCodeFilereferences);
            this.scTop.Panel1.Controls.Add(this.label2);
            this.scTop.Panel1.Controls.Add(this.lbLoadedAssessmentFiles);
            this.scTop.Panel1.Controls.Add(this.ascx_DropObject1);
            this.scTop.Panel1.Controls.Add(this.label3);
            this.scTop.Panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.scTop_Panel1_Paint);
            // 
            // scTop.Panel2
            // 
            this.scTop.Panel2.Controls.Add(this.label1);
            this.scTop.Panel2.Controls.Add(this.scTopRight);
            this.scTop.Size = new System.Drawing.Size(1107, 209);
            this.scTop.SplitterDistance = 501;
            this.scTop.TabIndex = 0;
            // 
            // cbUnLoadAssessmentFilesOnDoubleClick
            // 
            this.cbUnLoadAssessmentFilesOnDoubleClick.AutoSize = true;
            this.cbUnLoadAssessmentFilesOnDoubleClick.Checked = true;
            this.cbUnLoadAssessmentFilesOnDoubleClick.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbUnLoadAssessmentFilesOnDoubleClick.Location = new System.Drawing.Point(243, 2);
            this.cbUnLoadAssessmentFilesOnDoubleClick.Name = "cbUnLoadAssessmentFilesOnDoubleClick";
            this.cbUnLoadAssessmentFilesOnDoubleClick.Size = new System.Drawing.Size(154, 17);
            this.cbUnLoadAssessmentFilesOnDoubleClick.TabIndex = 45;
            this.cbUnLoadAssessmentFilesOnDoubleClick.Text = "Unload file on Double Click";
            this.cbUnLoadAssessmentFilesOnDoubleClick.UseVisualStyleBackColor = true;
            // 
            // lbAssessmentFilesThatNeedSourceCodePathFixing
            // 
            this.lbAssessmentFilesThatNeedSourceCodePathFixing.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                                                                                                              | System.Windows.Forms.AnchorStyles.Right)));
            this.lbAssessmentFilesThatNeedSourceCodePathFixing.FormattingEnabled = true;
            this.lbAssessmentFilesThatNeedSourceCodePathFixing.Location = new System.Drawing.Point(221, 154);
            this.lbAssessmentFilesThatNeedSourceCodePathFixing.Name = "lbAssessmentFilesThatNeedSourceCodePathFixing";
            this.lbAssessmentFilesThatNeedSourceCodePathFixing.Size = new System.Drawing.Size(156, 43);
            this.lbAssessmentFilesThatNeedSourceCodePathFixing.TabIndex = 44;
            this.lbAssessmentFilesThatNeedSourceCodePathFixing.Visible = false;
            this.lbAssessmentFilesThatNeedSourceCodePathFixing.DoubleClick += new System.EventHandler(this.lbAssessmentFilesThatNeedSourceCodePathFixing_DoubleClick);
            // 
            // lbFixSourceCodeFilereferences
            // 
            this.lbFixSourceCodeFilereferences.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lbFixSourceCodeFilereferences.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbFixSourceCodeFilereferences.ForeColor = System.Drawing.Color.Red;
            this.lbFixSourceCodeFilereferences.Location = new System.Drawing.Point(5, 152);
            this.lbFixSourceCodeFilereferences.Name = "lbFixSourceCodeFilereferences";
            this.lbFixSourceCodeFilereferences.Size = new System.Drawing.Size(219, 50);
            this.lbFixSourceCodeFilereferences.TabIndex = 43;
            this.lbFixSourceCodeFilereferences.Text = "Warning these (on the right)  loaded assessments have broken references to Files " +
                                                      "(double click to fix them):";
            this.lbFixSourceCodeFilereferences.Visible = false;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.Location = new System.Drawing.Point(387, 151);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(111, 55);
            this.label2.TabIndex = 41;
            this.label2.Text = "Note: You can also drop a folder to load all assessment files from that folder ";
            // 
            // lbLoadedAssessmentFiles
            // 
            this.lbLoadedAssessmentFiles.AllowDrop = true;
            this.lbLoadedAssessmentFiles.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                                                                                         | System.Windows.Forms.AnchorStyles.Left)
                                                                                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lbLoadedAssessmentFiles.FormattingEnabled = true;
            this.lbLoadedAssessmentFiles.Location = new System.Drawing.Point(6, 22);
            this.lbLoadedAssessmentFiles.Name = "lbLoadedAssessmentFiles";
            this.lbLoadedAssessmentFiles.Size = new System.Drawing.Size(371, 121);
            this.lbLoadedAssessmentFiles.TabIndex = 32;
            this.lbLoadedAssessmentFiles.DragDrop += new System.Windows.Forms.DragEventHandler(this.lbLoadedAssessmentFiles_DragDrop);
            this.lbLoadedAssessmentFiles.DoubleClick += new System.EventHandler(this.lbTargetSavedAssessmentFiles_DoubleClick);
            this.lbLoadedAssessmentFiles.DragEnter += new System.Windows.Forms.DragEventHandler(this.lbLoadedAssessmentFiles_DragEnter);
            // 
            // ascx_DropObject1
            // 
            this.ascx_DropObject1.AllowDrop = true;
            this.ascx_DropObject1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                                                                                 | System.Windows.Forms.AnchorStyles.Right)));
            this.ascx_DropObject1.BackColor = System.Drawing.Color.Maroon;
            this.ascx_DropObject1.ForeColor = System.Drawing.Color.White;
            this.ascx_DropObject1.Location = new System.Drawing.Point(387, 22);
            this.ascx_DropObject1.Name = "ascx_DropObject1";
            this.ascx_DropObject1.Size = new System.Drawing.Size(111, 126);
            this.ascx_DropObject1.TabIndex = 30;
            this.ascx_DropObject1.Text = "Drop Content Here!!";
            this.ascx_DropObject1.eDnDAction_ObjectDataReceived_Event += new O2.Kernel.CodeUtils.Callbacks.dMethod_Object(this.ascx_DropObject1_eDnDAction_ObjectDataReceived_Event);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(1, 3);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(149, 13);
            this.label3.TabIndex = 31;
            this.label3.Text = "Loaded Assessment Files";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(3, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(91, 13);
            this.label1.TabIndex = 22;
            this.label1.Text = "Search Criteria";
            // 
            // scBottom
            // 
            this.scBottom.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.scBottom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scBottom.Location = new System.Drawing.Point(0, 0);
            this.scBottom.Name = "scBottom";
            // 
            // scBottom.Panel1
            // 
            this.scBottom.Panel1.Controls.Add(this.lbNumberOfSearchResults);
            this.scBottom.Panel1.Controls.Add(this.label13);
            this.scBottom.Panel1.Controls.Add(this.scSearchResults);
            // 
            // scBottom.Panel2
            // 
            this.scBottom.Panel2.Controls.Add(this.lbRemoveFindingsFromSearces);
            this.scBottom.Panel2.Controls.Add(this.ado_RemoveFindingsFromSearches);
            this.scBottom.Panel2.Controls.Add(this.cbRemoveFindingsFromAssessmentsFromView);
            this.scBottom.Panel2.Controls.Add(this.label14);
            this.scBottom.Panel2.Controls.Add(this.scCreateCustomAssessmentFiles);
            this.scBottom.Panel2.Controls.Add(this.label11);
            this.scBottom.Size = new System.Drawing.Size(1107, 343);
            this.scBottom.SplitterDistance = 633;
            this.scBottom.TabIndex = 0;
            // 
            // lbNumberOfSearchResults
            // 
            this.lbNumberOfSearchResults.AutoSize = true;
            this.lbNumberOfSearchResults.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.lbNumberOfSearchResults.Location = new System.Drawing.Point(97, 6);
            this.lbNumberOfSearchResults.Name = "lbNumberOfSearchResults";
            this.lbNumberOfSearchResults.Size = new System.Drawing.Size(16, 13);
            this.lbNumberOfSearchResults.TabIndex = 35;
            this.lbNumberOfSearchResults.Text = "...";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(1, 6);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(101, 13);
            this.label13.TabIndex = 34;
            this.label13.Text = "Search Results: ";
            // 
            // lbRemoveFindingsFromSearces
            // 
            this.lbRemoveFindingsFromSearces.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                                                                                            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbRemoveFindingsFromSearces.Enabled = false;
            this.lbRemoveFindingsFromSearces.FormattingEnabled = true;
            this.lbRemoveFindingsFromSearces.Location = new System.Drawing.Point(232, 308);
            this.lbRemoveFindingsFromSearces.Name = "lbRemoveFindingsFromSearces";
            this.lbRemoveFindingsFromSearces.Size = new System.Drawing.Size(228, 30);
            this.lbRemoveFindingsFromSearces.TabIndex = 47;
            // 
            // ado_RemoveFindingsFromSearches
            // 
            this.ado_RemoveFindingsFromSearches.AllowDrop = true;
            this.ado_RemoveFindingsFromSearches.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ado_RemoveFindingsFromSearches.BackColor = System.Drawing.Color.Maroon;
            this.ado_RemoveFindingsFromSearches.Enabled = false;
            this.ado_RemoveFindingsFromSearches.ForeColor = System.Drawing.Color.White;
            this.ado_RemoveFindingsFromSearches.Location = new System.Drawing.Point(3, 308);
            this.ado_RemoveFindingsFromSearches.Name = "ado_RemoveFindingsFromSearches";
            this.ado_RemoveFindingsFromSearches.Size = new System.Drawing.Size(181, 28);
            this.ado_RemoveFindingsFromSearches.TabIndex = 46;
            this.ado_RemoveFindingsFromSearches.Text = "Drop Content Here!!";
            this.ado_RemoveFindingsFromSearches.eDnDAction_ObjectDataReceived_Event += new O2.Kernel.CodeUtils.Callbacks.dMethod_Object(this.ado_RemoveFindingsFromSearches_eDnDAction_ObjectDataReceived_Event);
            // 
            // cbRemoveFindingsFromAssessmentsFromView
            // 
            this.cbRemoveFindingsFromAssessmentsFromView.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cbRemoveFindingsFromAssessmentsFromView.AutoSize = true;
            this.cbRemoveFindingsFromAssessmentsFromView.Enabled = false;
            this.cbRemoveFindingsFromAssessmentsFromView.Location = new System.Drawing.Point(232, 289);
            this.cbRemoveFindingsFromAssessmentsFromView.Name = "cbRemoveFindingsFromAssessmentsFromView";
            this.cbRemoveFindingsFromAssessmentsFromView.Size = new System.Drawing.Size(294, 17);
            this.cbRemoveFindingsFromAssessmentsFromView.TabIndex = 35;
            this.cbRemoveFindingsFromAssessmentsFromView.Text = "Remove Findings from assessments below from searches";
            this.cbRemoveFindingsFromAssessmentsFromView.UseVisualStyleBackColor = true;
            // 
            // label14
            // 
            this.label14.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label14.AutoSize = true;
            this.label14.Enabled = false;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(-3, 289);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(189, 13);
            this.label14.TabIndex = 34;
            this.label14.Text = "Remove Findings from Searches";
            // 
            // scCreateCustomAssessmentFiles
            // 
            this.scCreateCustomAssessmentFiles.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                                                                                               | System.Windows.Forms.AnchorStyles.Left)
                                                                                              | System.Windows.Forms.AnchorStyles.Right)));
            this.scCreateCustomAssessmentFiles.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.scCreateCustomAssessmentFiles.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.scCreateCustomAssessmentFiles.Location = new System.Drawing.Point(-2, 22);
            this.scCreateCustomAssessmentFiles.Name = "scCreateCustomAssessmentFiles";
            // 
            // scCreateCustomAssessmentFiles.Panel1
            // 
            this.scCreateCustomAssessmentFiles.Panel1.Controls.Add(this.tv_CreateSavedAssessment_PerFindingsType);
            // 
            // scCreateCustomAssessmentFiles.Panel2
            // 
            this.scCreateCustomAssessmentFiles.Panel2.Controls.Add(this.groupBox3);
            this.scCreateCustomAssessmentFiles.Panel2.Controls.Add(this.groupBox2);
            this.scCreateCustomAssessmentFiles.Panel2.Controls.Add(this.btCreateCustomAssessmentFiles_LoadData);
            this.scCreateCustomAssessmentFiles.Panel2.Controls.Add(this.label10);
            this.scCreateCustomAssessmentFiles.Panel2.Controls.Add(this.tbSavedAssessment_FolderName);
            this.scCreateCustomAssessmentFiles.Panel2.Controls.Add(this.tbSavedAssessmentFileName);
            this.scCreateCustomAssessmentFiles.Panel2.Controls.Add(this.label9);
            this.scCreateCustomAssessmentFiles.Size = new System.Drawing.Size(464, 264);
            this.scCreateCustomAssessmentFiles.SplitterDistance = 39;
            this.scCreateCustomAssessmentFiles.TabIndex = 32;
            // 
            // tv_CreateSavedAssessment_PerFindingsType
            // 
            this.tv_CreateSavedAssessment_PerFindingsType.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tv_CreateSavedAssessment_PerFindingsType.Location = new System.Drawing.Point(0, 0);
            this.tv_CreateSavedAssessment_PerFindingsType.Name = "tv_CreateSavedAssessment_PerFindingsType";
            this.tv_CreateSavedAssessment_PerFindingsType.Size = new System.Drawing.Size(35, 260);
            this.tv_CreateSavedAssessment_PerFindingsType.TabIndex = 29;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btCreateAssessmentRun_WithSelectedFindingsType);
            this.groupBox3.Location = new System.Drawing.Point(4, 114);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(108, 96);
            this.groupBox3.TabIndex = 39;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Create n Files";
            // 
            // btCreateAssessmentRun_WithSelectedFindingsType
            // 
            this.btCreateAssessmentRun_WithSelectedFindingsType.Location = new System.Drawing.Point(7, 19);
            this.btCreateAssessmentRun_WithSelectedFindingsType.Name = "btCreateAssessmentRun_WithSelectedFindingsType";
            this.btCreateAssessmentRun_WithSelectedFindingsType.Size = new System.Drawing.Size(95, 66);
            this.btCreateAssessmentRun_WithSelectedFindingsType.TabIndex = 30;
            this.btCreateAssessmentRun_WithSelectedFindingsType.Text = "Create Assessment Run file with Selected Findings Types";
            this.btCreateAssessmentRun_WithSelectedFindingsType.UseVisualStyleBackColor = true;
            this.btCreateAssessmentRun_WithSelectedFindingsType.Click += new System.EventHandler(this.btCreateAssessmentRun_WithSelectedFindingsType_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.cbCreateFileWithAllTraces);
            this.groupBox2.Controls.Add(this.cbCreateFileWithUniqueTraces);
            this.groupBox2.Controls.Add(this.cbDropDuplicateSmartTraces);
            this.groupBox2.Controls.Add(this.cbIgnoreRootCallInvocation);
            this.groupBox2.Controls.Add(this.btCreateAssessmentRunWithSearchResults);
            this.groupBox2.Location = new System.Drawing.Point(123, 113);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(285, 97);
            this.groupBox2.TabIndex = 38;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Create 1 file with ALL filtered findings";
            // 
            // cbCreateFileWithAllTraces
            // 
            this.cbCreateFileWithAllTraces.AutoSize = true;
            this.cbCreateFileWithAllTraces.Location = new System.Drawing.Point(7, 33);
            this.cbCreateFileWithAllTraces.Name = "cbCreateFileWithAllTraces";
            this.cbCreateFileWithAllTraces.Size = new System.Drawing.Size(159, 17);
            this.cbCreateFileWithAllTraces.TabIndex = 35;
            this.cbCreateFileWithAllTraces.Text = "Create File With ALL Traces";
            this.cbCreateFileWithAllTraces.UseVisualStyleBackColor = true;
            this.cbCreateFileWithAllTraces.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // cbCreateFileWithUniqueTraces
            // 
            this.cbCreateFileWithUniqueTraces.AutoSize = true;
            this.cbCreateFileWithUniqueTraces.Checked = true;
            this.cbCreateFileWithUniqueTraces.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbCreateFileWithUniqueTraces.Location = new System.Drawing.Point(7, 16);
            this.cbCreateFileWithUniqueTraces.Name = "cbCreateFileWithUniqueTraces";
            this.cbCreateFileWithUniqueTraces.Size = new System.Drawing.Size(174, 17);
            this.cbCreateFileWithUniqueTraces.TabIndex = 32;
            this.cbCreateFileWithUniqueTraces.Text = "Create File With Unique Traces";
            this.cbCreateFileWithUniqueTraces.UseVisualStyleBackColor = true;
            this.cbCreateFileWithUniqueTraces.CheckedChanged += new System.EventHandler(this.cbCreateFileWithUniqueTraces_CheckedChanged);
            // 
            // cbDropDuplicateSmartTraces
            // 
            this.cbDropDuplicateSmartTraces.AutoSize = true;
            this.cbDropDuplicateSmartTraces.Checked = true;
            this.cbDropDuplicateSmartTraces.CheckState = System.Windows.Forms.CheckState.Checked;
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
            this.cbIgnoreRootCallInvocation.CheckedChanged += new System.EventHandler(this.cbIgnoreRootCallInvocation_CheckedChanged);
            // 
            // btCreateAssessmentRunWithSearchResults
            // 
            this.btCreateAssessmentRunWithSearchResults.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btCreateAssessmentRunWithSearchResults.Location = new System.Drawing.Point(182, 16);
            this.btCreateAssessmentRunWithSearchResults.Name = "btCreateAssessmentRunWithSearchResults";
            this.btCreateAssessmentRunWithSearchResults.Size = new System.Drawing.Size(97, 70);
            this.btCreateAssessmentRunWithSearchResults.TabIndex = 0;
            this.btCreateAssessmentRunWithSearchResults.Text = "Create Assessment Run file with ALL Search Results";
            this.btCreateAssessmentRunWithSearchResults.UseVisualStyleBackColor = true;
            this.btCreateAssessmentRunWithSearchResults.Click += new System.EventHandler(this.btCreateAssessmentRunWithSearchResults_Click);
            // 
            // btCreateCustomAssessmentFiles_LoadData
            // 
            this.btCreateCustomAssessmentFiles_LoadData.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                                                                                                       | System.Windows.Forms.AnchorStyles.Right)));
            this.btCreateCustomAssessmentFiles_LoadData.Location = new System.Drawing.Point(4, 4);
            this.btCreateCustomAssessmentFiles_LoadData.Name = "btCreateCustomAssessmentFiles_LoadData";
            this.btCreateCustomAssessmentFiles_LoadData.Size = new System.Drawing.Size(404, 31);
            this.btCreateCustomAssessmentFiles_LoadData.TabIndex = 31;
            this.btCreateCustomAssessmentFiles_LoadData.Text = "Load Data";
            this.btCreateCustomAssessmentFiles_LoadData.UseVisualStyleBackColor = true;
            this.btCreateCustomAssessmentFiles_LoadData.Click += new System.EventHandler(this.btCreateCustomAssessmentFiles_LoadData_Click);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(-2, 38);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(70, 13);
            this.label10.TabIndex = 27;
            this.label10.Text = "Target Folder";
            // 
            // tbSavedAssessment_FolderName
            // 
            this.tbSavedAssessment_FolderName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                                                                                             | System.Windows.Forms.AnchorStyles.Right)));
            this.tbSavedAssessment_FolderName.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.tbSavedAssessment_FolderName.Location = new System.Drawing.Point(3, 54);
            this.tbSavedAssessment_FolderName.Name = "tbSavedAssessment_FolderName";
            this.tbSavedAssessment_FolderName.Size = new System.Drawing.Size(405, 20);
            this.tbSavedAssessment_FolderName.TabIndex = 28;
            // 
            // tbSavedAssessmentFileName
            // 
            this.tbSavedAssessmentFileName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                                                                                          | System.Windows.Forms.AnchorStyles.Right)));
            this.tbSavedAssessmentFileName.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.tbSavedAssessmentFileName.Location = new System.Drawing.Point(3, 87);
            this.tbSavedAssessmentFileName.Name = "tbSavedAssessmentFileName";
            this.tbSavedAssessmentFileName.Size = new System.Drawing.Size(405, 20);
            this.tbSavedAssessmentFileName.TabIndex = 26;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(-2, 73);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(54, 13);
            this.label9.TabIndex = 25;
            this.label9.Text = "File Name";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(3, 6);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(189, 13);
            this.label11.TabIndex = 33;
            this.label11.Text = "Create Custom Assessment Files";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.llDownloadDemoFile_FromTempDirectory);
            this.groupBox1.Controls.Add(this.llDownloadDemoFile_WebGoat);
            this.groupBox1.Controls.Add(this.llDownloadDemoFile_HacmeBank_Website);
            this.groupBox1.Controls.Add(this.llDownloadDemoFile_HacmeBank_WebServices);
            this.groupBox1.Location = new System.Drawing.Point(579, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(531, 35);
            this.groupBox1.TabIndex = 40;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Load demo data:";
            // 
            // llDownloadDemoFile_FromTempDirectory
            // 
            this.llDownloadDemoFile_FromTempDirectory.AutoSize = true;
            this.llDownloadDemoFile_FromTempDirectory.Location = new System.Drawing.Point(425, 16);
            this.llDownloadDemoFile_FromTempDirectory.Name = "llDownloadDemoFile_FromTempDirectory";
            this.llDownloadDemoFile_FromTempDirectory.Size = new System.Drawing.Size(102, 13);
            this.llDownloadDemoFile_FromTempDirectory.TabIndex = 3;
            this.llDownloadDemoFile_FromTempDirectory.TabStop = true;
            this.llDownloadDemoFile_FromTempDirectory.Text = "from Temp Directory";
            this.llDownloadDemoFile_FromTempDirectory.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llDownloadDemoFile_FromTempDirectory_LinkClicked);
            // 
            // llDownloadDemoFile_WebGoat
            // 
            this.llDownloadDemoFile_WebGoat.AutoSize = true;
            this.llDownloadDemoFile_WebGoat.Location = new System.Drawing.Point(334, 16);
            this.llDownloadDemoFile_WebGoat.Name = "llDownloadDemoFile_WebGoat";
            this.llDownloadDemoFile_WebGoat.Size = new System.Drawing.Size(85, 13);
            this.llDownloadDemoFile_WebGoat.TabIndex = 2;
            this.llDownloadDemoFile_WebGoat.TabStop = true;
            this.llDownloadDemoFile_WebGoat.Text = "Web Goat (java)";
            this.llDownloadDemoFile_WebGoat.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llDownloadDemoFile_WebGoat_LinkClicked);
            // 
            // llDownloadDemoFile_HacmeBank_Website
            // 
            this.llDownloadDemoFile_HacmeBank_Website.AutoSize = true;
            this.llDownloadDemoFile_HacmeBank_Website.Location = new System.Drawing.Point(182, 16);
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
            // ascx_svpSearchAssessmentRun
            // 
            this.ascx_svpSearchAssessmentRun.BackColor = System.Drawing.SystemColors.Control;
            this.ascx_svpSearchAssessmentRun.ForeColor = System.Drawing.Color.Black;
            this.ascx_svpSearchAssessmentRun.Location = new System.Drawing.Point(3, -4);
            this.ascx_svpSearchAssessmentRun.Name = "ascx_svpSearchAssessmentRun";
            this.ascx_svpSearchAssessmentRun.Size = new System.Drawing.Size(570, 40);
            this.ascx_svpSearchAssessmentRun.TabIndex = 16;
            // 
            // ascx_SearchAssessmentRun
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.scTopLevelOne);
            this.ForeColor = System.Drawing.Color.Black;
            this.Name = "ascx_SearchAssessmentRun";
            this.Size = new System.Drawing.Size(1117, 669);
            this.Load += new System.EventHandler(this.ascx_SearchAssessmentRun_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSearchCriteria)).EndInit();
            this.scTopRight.Panel1.ResumeLayout(false);
            this.scTopRight.Panel1.PerformLayout();
            this.scTopRight.Panel2.ResumeLayout(false);
            this.scTopRight.Panel2.PerformLayout();
            this.scTopRight.ResumeLayout(false);
            this.scSearchResults.Panel1.ResumeLayout(false);
            this.scSearchResults.Panel2.ResumeLayout(false);
            this.scSearchResults.ResumeLayout(false);
            this.scTopLevelOne.Panel1.ResumeLayout(false);
            this.scTopLevelOne.Panel2.ResumeLayout(false);
            this.scTopLevelOne.ResumeLayout(false);
            this.scHost.Panel1.ResumeLayout(false);
            this.scHost.Panel2.ResumeLayout(false);
            this.scHost.ResumeLayout(false);
            this.scTop.Panel1.ResumeLayout(false);
            this.scTop.Panel1.PerformLayout();
            this.scTop.Panel2.ResumeLayout(false);
            this.scTop.Panel2.PerformLayout();
            this.scTop.ResumeLayout(false);
            this.scBottom.Panel1.ResumeLayout(false);
            this.scBottom.Panel1.PerformLayout();
            this.scBottom.Panel2.ResumeLayout(false);
            this.scBottom.Panel2.PerformLayout();
            this.scBottom.ResumeLayout(false);
            this.scCreateCustomAssessmentFiles.Panel1.ResumeLayout(false);
            this.scCreateCustomAssessmentFiles.Panel2.ResumeLayout(false);
            this.scCreateCustomAssessmentFiles.Panel2.PerformLayout();
            this.scCreateCustomAssessmentFiles.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvSearchCriteria;
        private System.Windows.Forms.SplitContainer scSearchResults;
        private System.Windows.Forms.Button btExecuteSearch;
        private System.Windows.Forms.SplitContainer scTopRight;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ListBox lbPreviousSearches;
        private System.Windows.Forms.CheckBox cbSearchOnFindingsWithNoTraces;
        private ascx_TraceViewer ascx_TraceViewer1;
        private System.Windows.Forms.WebBrowser wbInfo;
        private System.Windows.Forms.SplitContainer scTopLevelOne;
        private ascx_SelectVisiblePanels ascx_svpSearchAssessmentRun;
        private System.Windows.Forms.SplitContainer scHost;
        private System.Windows.Forms.SplitContainer scTop;
        private System.Windows.Forms.SplitContainer scBottom;
        private System.Windows.Forms.ListBox lbLoadedAssessmentFiles;
        private ascx_DropObject ascx_DropObject1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.SplitContainer scCreateCustomAssessmentFiles;
        private System.Windows.Forms.TreeView tv_CreateSavedAssessment_PerFindingsType;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button btCreateAssessmentRunWithSearchResults;
        private System.Windows.Forms.Button btCreateAssessmentRun_WithSelectedFindingsType;
        private System.Windows.Forms.TextBox tbSavedAssessment_FolderName;
        private System.Windows.Forms.TextBox tbSavedAssessmentFileName;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.LinkLabel llDownloadDemoFile_WebGoat;
        private System.Windows.Forms.LinkLabel llDownloadDemoFile_HacmeBank_Website;
        private System.Windows.Forms.LinkLabel llDownloadDemoFile_HacmeBank_WebServices;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lbFixSourceCodeFilereferences;
        private System.Windows.Forms.ListBox lbAssessmentFilesThatNeedSourceCodePathFixing;
        private System.Windows.Forms.CheckBox cbUnLoadAssessmentFilesOnDoubleClick;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ListBox lbAvailableSearchType;
        private System.Windows.Forms.Button btAddSearchCriteria;
        private System.Windows.Forms.TextBox tbSearchTextToAdd;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.CheckBox cbRemoveFindingsFromAssessmentsFromView;
        private System.Windows.Forms.Label label14;
        private ascx_DropObject ado_RemoveFindingsFromSearches;
        private System.Windows.Forms.ListBox lbRemoveFindingsFromSearces;
        private System.Windows.Forms.CheckBox cbSearchTypeNegative;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.LinkLabel llDownloadDemoFile_FromTempDirectory;
        private System.Windows.Forms.DataGridViewComboBoxColumn sName;
        private System.Windows.Forms.DataGridViewTextBoxColumn sValue;
        private System.Windows.Forms.DataGridViewCheckBoxColumn NegativeSearch;
        private System.Windows.Forms.LinkLabel llSearchCriteria_ClearSearchCriteria;
        private System.Windows.Forms.LinkLabel llSearchCriteria_DeleteSelectedRow;
        private System.Windows.Forms.Label lbNumberOfSearchResults;
        private System.Windows.Forms.ImageList imageList1;
        private ascx_FindingsSearchViewer ascx_FindingsSearchViewer1;
        private System.Windows.Forms.Button btCreateCustomAssessmentFiles_LoadData;
        private System.Windows.Forms.CheckBox cbCreateFileWithUniqueTraces;
        private System.Windows.Forms.CheckBox cbIgnoreRootCallInvocation;
        private System.Windows.Forms.CheckBox cbDropDuplicateSmartTraces;
        private System.Windows.Forms.CheckBox cbCreateFileWithAllTraces;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
    }
}
