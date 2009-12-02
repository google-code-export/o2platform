// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
namespace O2.Rules.OunceLabs.Ascx
{
    partial class ascx_RulePackViewer
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ascx_RulePackViewer));
            this.BottomToolStripPanel = new System.Windows.Forms.ToolStripPanel();
            this.TopToolStripPanel = new System.Windows.Forms.ToolStripPanel();
            this.RightToolStripPanel = new System.Windows.Forms.ToolStripPanel();
            this.LeftToolStripPanel = new System.Windows.Forms.ToolStripPanel();
            this.ContentPanel = new System.Windows.Forms.ToolStripContentPanel();
            this.tsRulePackViewer = new System.Windows.Forms.ToolStrip();
            this.btNewRule = new System.Windows.Forms.ToolStripButton();
            this.btEditSelectedRule = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.cbTypeOfRuleToView = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.tbSignatureFilter = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btImportFromLocalMySqlOunceDatabase = new System.Windows.Forms.ToolStripButton();
            this.btPreferences = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.btSaveCurrentFilter = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.btSaveAllLoadedRules = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.btRefreshView = new System.Windows.Forms.ToolStripButton();
            this.laNumberOfRulesLoaded = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.btRemoveAllLoadedRules = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator9 = new System.Windows.Forms.ToolStripSeparator();
            this.laNotAllRulesShow = new System.Windows.Forms.ToolStripLabel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.functionsViewer = new O2.Views.ASCX.DataViewers.ascx_FunctionsViewer();
            this.scRulesAndMySqlProperties = new System.Windows.Forms.SplitContainer();
            this.rbViewMode_TaggedAndInDb = new System.Windows.Forms.RadioButton();
            this.rbViewMode_OnlyNotInDbAndMapped = new System.Windows.Forms.RadioButton();
            this.llDragSelectedRules = new System.Windows.Forms.LinkLabel();
            this.llDragFilteredRules = new System.Windows.Forms.LinkLabel();
            this.llDragAllLoadedRules = new System.Windows.Forms.LinkLabel();
            this.llChangeRulesTo_ToBeDeleted = new System.Windows.Forms.LinkLabel();
            this.label3 = new System.Windows.Forms.Label();
            this.llChangeRulesTo_DontPropagateTaint = new System.Windows.Forms.LinkLabel();
            this.llChangeRulesTo_TaintPropagator = new System.Windows.Forms.LinkLabel();
            this.llChangeRulesTo_Callback = new System.Windows.Forms.LinkLabel();
            this.linkLabel2 = new System.Windows.Forms.LinkLabel();
            this.llChangeRulesTo_Source = new System.Windows.Forms.LinkLabel();
            this.label5 = new System.Windows.Forms.Label();
            this.rbViewMode_OnlyNotInDb = new System.Windows.Forms.RadioButton();
            this.rbViewMode_OnlyTaggedRules = new System.Windows.Forms.RadioButton();
            this.rbViewMode_AllRules = new System.Windows.Forms.RadioButton();
            this.laLoadingRulePack = new System.Windows.Forms.Label();
            this.laImportingRulesFromLocalMySqlDB = new System.Windows.Forms.Label();
            this.laOnlyShowingRulesFor1Signature = new System.Windows.Forms.Label();
            this.dgvRules = new System.Windows.Forms.DataGridView();
            this.tabControl2 = new System.Windows.Forms.TabControl();
            this.tpLoadAndEditRules = new System.Windows.Forms.TabPage();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.llClearSelectedChangedRules = new System.Windows.Forms.LinkLabel();
            this.btMySqlSync_AddSelectedRulesToDatabase = new System.Windows.Forms.Button();
            this.lvChangedRules = new System.Windows.Forms.ListView();
            this.chRuleType = new System.Windows.Forms.ColumnHeader();
            this.chSignature = new System.Windows.Forms.ColumnHeader();
            this.lbNumberOfChangedRules = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.llClearChangedRules = new System.Windows.Forms.LinkLabel();
            this.label1 = new System.Windows.Forms.Label();
            this.btMySqlSync_DeleteSelectedRulesFromDatabase = new System.Windows.Forms.Button();
            this.llRefreshChangedRules = new System.Windows.Forms.LinkLabel();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btImportFromLocalMySql = new System.Windows.Forms.Button();
            this.cbLanguages = new System.Windows.Forms.ComboBox();
            this.tpConfig = new System.Windows.Forms.TabPage();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.cbShowMySqlPassword = new System.Windows.Forms.CheckBox();
            this.tbMySqlPassword = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.tbMySqlUsername = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.cbOnChangeDeletePreviewRule = new System.Windows.Forms.CheckBox();
            this.cbColorCodeRules = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tbChangeRulesTo_NewVulnName = new System.Windows.Forms.TextBox();
            this.gbMySqlImportPreferences = new System.Windows.Forms.GroupBox();
            this.cbMySqlImport_Callbacks = new System.Windows.Forms.CheckBox();
            this.cbMySqlImport_AnyLow = new System.Windows.Forms.CheckBox();
            this.cbMySqlImport_AnyMedium = new System.Windows.Forms.CheckBox();
            this.cbMySqlImport_DontPropagateTaint = new System.Windows.Forms.CheckBox();
            this.cbMySqlImport_AnyHigh = new System.Windows.Forms.CheckBox();
            this.cbMySqlImport_PropagateTaint = new System.Windows.Forms.CheckBox();
            this.cbMySqlImport_Sinks = new System.Windows.Forms.CheckBox();
            this.cbMySqlImport_Sources = new System.Windows.Forms.CheckBox();
            this.label7 = new System.Windows.Forms.Label();
            this.tpRulesAnalysis = new System.Windows.Forms.TabPage();
            this.label11 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.llShowRulesWithSinksAndPropagateTaint = new System.Windows.Forms.LinkLabel();
            this.tpDroppedData = new System.Windows.Forms.TabPage();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.cbFindingsDrop_KeepRulesLoadedFromDatabase = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cbCirDataDrop_DontMarkAsCallbackOrSinksMethodsWithNoParameters = new System.Windows.Forms.CheckBox();
            this.cbCirDataDrop_DontAddIfThereAreNoCallersAndCallees = new System.Windows.Forms.CheckBox();
            this.cbCirDataDrop_AddExternalMethodsAsSourcesAndSinks = new System.Windows.Forms.CheckBox();
            this.cbCirDataDrop_KeepRulesLoadedFromDatabase = new System.Windows.Forms.CheckBox();
            this.cbCirDataDrop_DontAddIfRuleSignatureAlreadyExists = new System.Windows.Forms.CheckBox();
            this.cbCirDataDrop_AddInternalMethodsWithNoCallersAsCallbacks = new System.Windows.Forms.CheckBox();
            this.label6 = new System.Windows.Forms.Label();
            this.tbCirDataDrop_NewRulesVulnType = new System.Windows.Forms.TextBox();
            this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.tbMySqlIPAddress = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.tbMySqlPort = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.tsRulePackViewer.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.scRulesAndMySqlProperties.Panel1.SuspendLayout();
            this.scRulesAndMySqlProperties.Panel2.SuspendLayout();
            this.scRulesAndMySqlProperties.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRules)).BeginInit();
            this.tabControl2.SuspendLayout();
            this.tpLoadAndEditRules.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.tpConfig.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.gbMySqlImportPreferences.SuspendLayout();
            this.tpRulesAnalysis.SuspendLayout();
            this.tpDroppedData.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.toolStripContainer1.ContentPanel.SuspendLayout();
            this.toolStripContainer1.TopToolStripPanel.SuspendLayout();
            this.toolStripContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // BottomToolStripPanel
            // 
            this.BottomToolStripPanel.Location = new System.Drawing.Point(0, 0);
            this.BottomToolStripPanel.Name = "BottomToolStripPanel";
            this.BottomToolStripPanel.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.BottomToolStripPanel.RowMargin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.BottomToolStripPanel.Size = new System.Drawing.Size(0, 0);
            // 
            // TopToolStripPanel
            // 
            this.TopToolStripPanel.Location = new System.Drawing.Point(0, 0);
            this.TopToolStripPanel.Name = "TopToolStripPanel";
            this.TopToolStripPanel.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.TopToolStripPanel.RowMargin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.TopToolStripPanel.Size = new System.Drawing.Size(0, 0);
            // 
            // RightToolStripPanel
            // 
            this.RightToolStripPanel.Location = new System.Drawing.Point(0, 0);
            this.RightToolStripPanel.Name = "RightToolStripPanel";
            this.RightToolStripPanel.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.RightToolStripPanel.RowMargin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.RightToolStripPanel.Size = new System.Drawing.Size(0, 0);
            // 
            // LeftToolStripPanel
            // 
            this.LeftToolStripPanel.Location = new System.Drawing.Point(0, 0);
            this.LeftToolStripPanel.Name = "LeftToolStripPanel";
            this.LeftToolStripPanel.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.LeftToolStripPanel.RowMargin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.LeftToolStripPanel.Size = new System.Drawing.Size(0, 0);
            // 
            // ContentPanel
            // 
            this.ContentPanel.Size = new System.Drawing.Size(1039, 410);
            // 
            // tsRulePackViewer
            // 
            this.tsRulePackViewer.AllowDrop = true;
            this.tsRulePackViewer.Dock = System.Windows.Forms.DockStyle.None;
            this.tsRulePackViewer.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btNewRule,
            this.btEditSelectedRule,
            this.toolStripSeparator7,
            this.toolStripSeparator8,
            this.toolStripLabel1,
            this.cbTypeOfRuleToView,
            this.toolStripSeparator2,
            this.toolStripLabel2,
            this.tbSignatureFilter,
            this.toolStripSeparator1,
            this.btImportFromLocalMySqlOunceDatabase,
            this.btPreferences,
            this.toolStripSeparator3,
            this.btSaveCurrentFilter,
            this.toolStripSeparator6,
            this.btSaveAllLoadedRules,
            this.toolStripSeparator5,
            this.btRefreshView,
            this.laNumberOfRulesLoaded,
            this.toolStripSeparator4,
            this.btRemoveAllLoadedRules,
            this.toolStripSeparator9,
            this.laNotAllRulesShow});
            this.tsRulePackViewer.Location = new System.Drawing.Point(3, 0);
            this.tsRulePackViewer.Name = "tsRulePackViewer";
            this.tsRulePackViewer.Size = new System.Drawing.Size(866, 25);
            this.tsRulePackViewer.TabIndex = 1;
            this.tsRulePackViewer.Text = "toolStrip1";
            this.tsRulePackViewer.DragEnter += new System.Windows.Forms.DragEventHandler(this.tsRulePackViewer_DragEnter);
            this.tsRulePackViewer.DragDrop += new System.Windows.Forms.DragEventHandler(this.tsRulePackViewer_DragDrop);
            // 
            // btNewRule
            // 
            this.btNewRule.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btNewRule.Image = ((System.Drawing.Image)(resources.GetObject("btNewRule.Image")));
            this.btNewRule.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btNewRule.Name = "btNewRule";
            this.btNewRule.Size = new System.Drawing.Size(23, 22);
            this.btNewRule.Text = "New Rule";
            this.btNewRule.Click += new System.EventHandler(this.btNewRule_Click);
            // 
            // btEditSelectedRule
            // 
            this.btEditSelectedRule.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btEditSelectedRule.Image = ((System.Drawing.Image)(resources.GetObject("btEditSelectedRule.Image")));
            this.btEditSelectedRule.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btEditSelectedRule.Name = "btEditSelectedRule";
            this.btEditSelectedRule.Size = new System.Drawing.Size(23, 22);
            this.btEditSelectedRule.Text = "Edit Selected Rule";
            this.btEditSelectedRule.Click += new System.EventHandler(this.btEditSelectedRule_Click);
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripSeparator8
            // 
            this.toolStripSeparator8.Name = "toolStripSeparator8";
            this.toolStripSeparator8.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(100, 22);
            this.toolStripLabel1.Text = "type of rule to see:";
            // 
            // cbTypeOfRuleToView
            // 
            this.cbTypeOfRuleToView.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbTypeOfRuleToView.Items.AddRange(new object[] {
            "All",
            "Callbacks",
            "Sinks",
            "Lost Sinks",
            "Sources",
            "Propagate Taint",
            "Dont Propagate Taint",
            "",
            "Not Mapped",
            "",
            "Not Sinks",
            "Not Sources",
            "To Be Deleted"});
            this.cbTypeOfRuleToView.Name = "cbTypeOfRuleToView";
            this.cbTypeOfRuleToView.Size = new System.Drawing.Size(121, 25);
            this.cbTypeOfRuleToView.SelectedIndexChanged += new System.EventHandler(this.cbTypeOfRuleToView_SelectedIndexChanged);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(81, 22);
            this.toolStripLabel2.Text = "signature filter:";
            // 
            // tbSignatureFilter
            // 
            this.tbSignatureFilter.Name = "tbSignatureFilter";
            this.tbSignatureFilter.Size = new System.Drawing.Size(100, 25);
            this.tbSignatureFilter.KeyUp += new System.Windows.Forms.KeyEventHandler(this.tbSignatureFilter_KeyUp);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // btImportFromLocalMySqlOunceDatabase
            // 
            this.btImportFromLocalMySqlOunceDatabase.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btImportFromLocalMySqlOunceDatabase.Image = ((System.Drawing.Image)(resources.GetObject("btImportFromLocalMySqlOunceDatabase.Image")));
            this.btImportFromLocalMySqlOunceDatabase.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btImportFromLocalMySqlOunceDatabase.Name = "btImportFromLocalMySqlOunceDatabase";
            this.btImportFromLocalMySqlOunceDatabase.Size = new System.Drawing.Size(23, 22);
            this.btImportFromLocalMySqlOunceDatabase.Text = "Import From Local Ounce MySql Database";
            this.btImportFromLocalMySqlOunceDatabase.Click += new System.EventHandler(this.btImportFromLocalMySqlOunceDatabase_Click);
            // 
            // btPreferences
            // 
            this.btPreferences.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btPreferences.Image = ((System.Drawing.Image)(resources.GetObject("btPreferences.Image")));
            this.btPreferences.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btPreferences.Name = "btPreferences";
            this.btPreferences.Size = new System.Drawing.Size(23, 22);
            this.btPreferences.Text = "Preferences";
            this.btPreferences.Click += new System.EventHandler(this.btPreferences_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // btSaveCurrentFilter
            // 
            this.btSaveCurrentFilter.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btSaveCurrentFilter.Image = ((System.Drawing.Image)(resources.GetObject("btSaveCurrentFilter.Image")));
            this.btSaveCurrentFilter.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btSaveCurrentFilter.Name = "btSaveCurrentFilter";
            this.btSaveCurrentFilter.Size = new System.Drawing.Size(23, 22);
            this.btSaveCurrentFilter.Text = "Save Current Filter";
            this.btSaveCurrentFilter.Click += new System.EventHandler(this.btSaveCurrentFilter_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(6, 25);
            // 
            // btSaveAllLoadedRules
            // 
            this.btSaveAllLoadedRules.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btSaveAllLoadedRules.Image = ((System.Drawing.Image)(resources.GetObject("btSaveAllLoadedRules.Image")));
            this.btSaveAllLoadedRules.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btSaveAllLoadedRules.Name = "btSaveAllLoadedRules";
            this.btSaveAllLoadedRules.Size = new System.Drawing.Size(23, 22);
            this.btSaveAllLoadedRules.Text = "Save all loaded rules";
            this.btSaveAllLoadedRules.Click += new System.EventHandler(this.btSaveAllLoadedRules_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 25);
            // 
            // btRefreshView
            // 
            this.btRefreshView.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btRefreshView.Image = ((System.Drawing.Image)(resources.GetObject("btRefreshView.Image")));
            this.btRefreshView.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btRefreshView.Name = "btRefreshView";
            this.btRefreshView.Size = new System.Drawing.Size(23, 22);
            this.btRefreshView.Text = "Refresh View";
            this.btRefreshView.Click += new System.EventHandler(this.btRefreshView_Click);
            // 
            // laNumberOfRulesLoaded
            // 
            this.laNumberOfRulesLoaded.Name = "laNumberOfRulesLoaded";
            this.laNumberOfRulesLoaded.Size = new System.Drawing.Size(19, 22);
            this.laNumberOfRulesLoaded.Text = "...";
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
            // 
            // btRemoveAllLoadedRules
            // 
            this.btRemoveAllLoadedRules.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btRemoveAllLoadedRules.Image = ((System.Drawing.Image)(resources.GetObject("btRemoveAllLoadedRules.Image")));
            this.btRemoveAllLoadedRules.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btRemoveAllLoadedRules.Name = "btRemoveAllLoadedRules";
            this.btRemoveAllLoadedRules.Size = new System.Drawing.Size(23, 22);
            this.btRemoveAllLoadedRules.Text = "Remove all loaded rules";
            this.btRemoveAllLoadedRules.Click += new System.EventHandler(this.btRemoveAllLoadedRules_Click);
            // 
            // toolStripSeparator9
            // 
            this.toolStripSeparator9.Name = "toolStripSeparator9";
            this.toolStripSeparator9.Size = new System.Drawing.Size(6, 25);
            // 
            // laNotAllRulesShow
            // 
            this.laNotAllRulesShow.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.laNotAllRulesShow.ForeColor = System.Drawing.Color.Red;
            this.laNotAllRulesShow.Name = "laNotAllRulesShow";
            this.laNotAllRulesShow.Size = new System.Drawing.Size(193, 22);
            this.laNotAllRulesShow.Text = "Not all rules shown (max is 7500)";
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
            this.splitContainer1.Panel1.Controls.Add(this.functionsViewer);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.scRulesAndMySqlProperties);
            this.splitContainer1.Size = new System.Drawing.Size(1039, 466);
            this.splitContainer1.SplitterDistance = 201;
            this.splitContainer1.TabIndex = 7;
            // 
            // functionsViewer
            // 
            this.functionsViewer.BackColor = System.Drawing.SystemColors.Control;
            this.functionsViewer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.functionsViewer.ForeColor = System.Drawing.Color.Black;
            this.functionsViewer.Location = new System.Drawing.Point(0, 0);
            this.functionsViewer.Name = "functionsViewer";
            this.functionsViewer.NamespaceDepthValue = 1;
            this.functionsViewer.Size = new System.Drawing.Size(197, 462);
            this.functionsViewer.TabIndex = 0;
            this.functionsViewer._onDoubleClick += new O2.Kernel.CodeUtils.Callbacks.dMethod_Object(this.functionsViewer__onDoubleClick);
            this.functionsViewer._onAfterSelect += new O2.Kernel.CodeUtils.Callbacks.dMethod_Object(this.functionsViewer__onAfterSelect);
            // 
            // scRulesAndMySqlProperties
            // 
            this.scRulesAndMySqlProperties.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.scRulesAndMySqlProperties.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scRulesAndMySqlProperties.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.scRulesAndMySqlProperties.Location = new System.Drawing.Point(0, 0);
            this.scRulesAndMySqlProperties.Name = "scRulesAndMySqlProperties";
            // 
            // scRulesAndMySqlProperties.Panel1
            // 
            this.scRulesAndMySqlProperties.Panel1.Controls.Add(this.rbViewMode_TaggedAndInDb);
            this.scRulesAndMySqlProperties.Panel1.Controls.Add(this.rbViewMode_OnlyNotInDbAndMapped);
            this.scRulesAndMySqlProperties.Panel1.Controls.Add(this.llDragSelectedRules);
            this.scRulesAndMySqlProperties.Panel1.Controls.Add(this.llDragFilteredRules);
            this.scRulesAndMySqlProperties.Panel1.Controls.Add(this.llDragAllLoadedRules);
            this.scRulesAndMySqlProperties.Panel1.Controls.Add(this.llChangeRulesTo_ToBeDeleted);
            this.scRulesAndMySqlProperties.Panel1.Controls.Add(this.label3);
            this.scRulesAndMySqlProperties.Panel1.Controls.Add(this.llChangeRulesTo_DontPropagateTaint);
            this.scRulesAndMySqlProperties.Panel1.Controls.Add(this.llChangeRulesTo_TaintPropagator);
            this.scRulesAndMySqlProperties.Panel1.Controls.Add(this.llChangeRulesTo_Callback);
            this.scRulesAndMySqlProperties.Panel1.Controls.Add(this.linkLabel2);
            this.scRulesAndMySqlProperties.Panel1.Controls.Add(this.llChangeRulesTo_Source);
            this.scRulesAndMySqlProperties.Panel1.Controls.Add(this.label5);
            this.scRulesAndMySqlProperties.Panel1.Controls.Add(this.rbViewMode_OnlyNotInDb);
            this.scRulesAndMySqlProperties.Panel1.Controls.Add(this.rbViewMode_OnlyTaggedRules);
            this.scRulesAndMySqlProperties.Panel1.Controls.Add(this.rbViewMode_AllRules);
            this.scRulesAndMySqlProperties.Panel1.Controls.Add(this.laLoadingRulePack);
            this.scRulesAndMySqlProperties.Panel1.Controls.Add(this.laImportingRulesFromLocalMySqlDB);
            this.scRulesAndMySqlProperties.Panel1.Controls.Add(this.laOnlyShowingRulesFor1Signature);
            this.scRulesAndMySqlProperties.Panel1.Controls.Add(this.dgvRules);
            // 
            // scRulesAndMySqlProperties.Panel2
            // 
            this.scRulesAndMySqlProperties.Panel2.Controls.Add(this.tabControl2);
            this.scRulesAndMySqlProperties.Size = new System.Drawing.Size(834, 466);
            this.scRulesAndMySqlProperties.SplitterDistance = 498;
            this.scRulesAndMySqlProperties.TabIndex = 8;
            this.scRulesAndMySqlProperties.Resize += new System.EventHandler(this.scRulesAndMySqlProperties_Resize);
            // 
            // rbViewMode_TaggedAndInDb
            // 
            this.rbViewMode_TaggedAndInDb.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.rbViewMode_TaggedAndInDb.AutoSize = true;
            this.rbViewMode_TaggedAndInDb.Location = new System.Drawing.Point(469, 425);
            this.rbViewMode_TaggedAndInDb.Name = "rbViewMode_TaggedAndInDb";
            this.rbViewMode_TaggedAndInDb.Size = new System.Drawing.Size(136, 17);
            this.rbViewMode_TaggedAndInDb.TabIndex = 14;
            this.rbViewMode_TaggedAndInDb.Text = "Only Tagged and in DB";
            this.rbViewMode_TaggedAndInDb.UseVisualStyleBackColor = true;
            this.rbViewMode_TaggedAndInDb.CheckedChanged += new System.EventHandler(this.rbViewMode_TaggedAndInDb_CheckedChanged);
            // 
            // rbViewMode_OnlyNotInDbAndMapped
            // 
            this.rbViewMode_OnlyNotInDbAndMapped.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.rbViewMode_OnlyNotInDbAndMapped.AutoSize = true;
            this.rbViewMode_OnlyNotInDbAndMapped.Location = new System.Drawing.Point(318, 425);
            this.rbViewMode_OnlyNotInDbAndMapped.Name = "rbViewMode_OnlyNotInDbAndMapped";
            this.rbViewMode_OnlyNotInDbAndMapped.Size = new System.Drawing.Size(150, 17);
            this.rbViewMode_OnlyNotInDbAndMapped.TabIndex = 26;
            this.rbViewMode_OnlyNotInDbAndMapped.Text = "only NOT in DB && Mapped";
            this.rbViewMode_OnlyNotInDbAndMapped.UseVisualStyleBackColor = true;
            this.rbViewMode_OnlyNotInDbAndMapped.CheckedChanged += new System.EventHandler(this.rbViewMode_OnlyNotInDbAndMapped_CheckedChanged);
            // 
            // llDragSelectedRules
            // 
            this.llDragSelectedRules.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.llDragSelectedRules.AutoSize = true;
            this.llDragSelectedRules.Location = new System.Drawing.Point(395, 445);
            this.llDragSelectedRules.Name = "llDragSelectedRules";
            this.llDragSelectedRules.Size = new System.Drawing.Size(96, 13);
            this.llDragSelectedRules.TabIndex = 9;
            this.llDragSelectedRules.TabStop = true;
            this.llDragSelectedRules.Text = "drag selected rules";
            this.llDragSelectedRules.MouseDown += new System.Windows.Forms.MouseEventHandler(this.llDragSelectedRules_MouseDown);
            // 
            // llDragFilteredRules
            // 
            this.llDragFilteredRules.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.llDragFilteredRules.AutoSize = true;
            this.llDragFilteredRules.Location = new System.Drawing.Point(183, 445);
            this.llDragFilteredRules.Name = "llDragFilteredRules";
            this.llDragFilteredRules.Size = new System.Drawing.Size(87, 13);
            this.llDragFilteredRules.TabIndex = 8;
            this.llDragFilteredRules.TabStop = true;
            this.llDragFilteredRules.Text = "drag filtered rules";
            this.llDragFilteredRules.MouseDown += new System.Windows.Forms.MouseEventHandler(this.llDragFilteredRules_MouseDown);
            // 
            // llDragAllLoadedRules
            // 
            this.llDragAllLoadedRules.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.llDragAllLoadedRules.AutoSize = true;
            this.llDragAllLoadedRules.Location = new System.Drawing.Point(276, 445);
            this.llDragAllLoadedRules.Name = "llDragAllLoadedRules";
            this.llDragAllLoadedRules.Size = new System.Drawing.Size(101, 13);
            this.llDragAllLoadedRules.TabIndex = 7;
            this.llDragAllLoadedRules.TabStop = true;
            this.llDragAllLoadedRules.Text = "drag all loaded rules";
            this.llDragAllLoadedRules.MouseDown += new System.Windows.Forms.MouseEventHandler(this.llDragAllLoadedRules_MouseDown);
            // 
            // llChangeRulesTo_ToBeDeleted
            // 
            this.llChangeRulesTo_ToBeDeleted.AutoSize = true;
            this.llChangeRulesTo_ToBeDeleted.Location = new System.Drawing.Point(462, 4);
            this.llChangeRulesTo_ToBeDeleted.Name = "llChangeRulesTo_ToBeDeleted";
            this.llChangeRulesTo_ToBeDeleted.Size = new System.Drawing.Size(69, 13);
            this.llChangeRulesTo_ToBeDeleted.TabIndex = 24;
            this.llChangeRulesTo_ToBeDeleted.TabStop = true;
            this.llChangeRulesTo_ToBeDeleted.Text = "to be deleted";
            this.toolTip1.SetToolTip(this.llChangeRulesTo_ToBeDeleted, "(will also add ALL (if any) other rules that match this signature)");
            this.llChangeRulesTo_ToBeDeleted.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llChangeRulesTo_ToBeDeleted_LinkClicked);
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(1, 427);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(63, 13);
            this.label3.TabIndex = 13;
            this.label3.Text = "View Mode:";
            // 
            // llChangeRulesTo_DontPropagateTaint
            // 
            this.llChangeRulesTo_DontPropagateTaint.AutoSize = true;
            this.llChangeRulesTo_DontPropagateTaint.Location = new System.Drawing.Point(354, 4);
            this.llChangeRulesTo_DontPropagateTaint.Name = "llChangeRulesTo_DontPropagateTaint";
            this.llChangeRulesTo_DontPropagateTaint.Size = new System.Drawing.Size(102, 13);
            this.llChangeRulesTo_DontPropagateTaint.TabIndex = 19;
            this.llChangeRulesTo_DontPropagateTaint.TabStop = true;
            this.llChangeRulesTo_DontPropagateTaint.Text = "dont propagate taint";
            this.toolTip1.SetToolTip(this.llChangeRulesTo_DontPropagateTaint, "(also deletes other rules for this signature)");
            this.llChangeRulesTo_DontPropagateTaint.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llChangeRulesTo_DontPropagateTaint_LinkClicked);
            // 
            // llChangeRulesTo_TaintPropagator
            // 
            this.llChangeRulesTo_TaintPropagator.AutoSize = true;
            this.llChangeRulesTo_TaintPropagator.Location = new System.Drawing.Point(267, 4);
            this.llChangeRulesTo_TaintPropagator.Name = "llChangeRulesTo_TaintPropagator";
            this.llChangeRulesTo_TaintPropagator.Size = new System.Drawing.Size(81, 13);
            this.llChangeRulesTo_TaintPropagator.TabIndex = 18;
            this.llChangeRulesTo_TaintPropagator.TabStop = true;
            this.llChangeRulesTo_TaintPropagator.Text = "taint propagator";
            this.llChangeRulesTo_TaintPropagator.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llChangeRulesTo_TaintPropagator_LinkClicked);
            // 
            // llChangeRulesTo_Callback
            // 
            this.llChangeRulesTo_Callback.AutoSize = true;
            this.llChangeRulesTo_Callback.Location = new System.Drawing.Point(214, 4);
            this.llChangeRulesTo_Callback.Name = "llChangeRulesTo_Callback";
            this.llChangeRulesTo_Callback.Size = new System.Drawing.Size(47, 13);
            this.llChangeRulesTo_Callback.TabIndex = 20;
            this.llChangeRulesTo_Callback.TabStop = true;
            this.llChangeRulesTo_Callback.Text = "callback";
            this.llChangeRulesTo_Callback.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llChangeRulesTo_Callback_LinkClicked);
            // 
            // linkLabel2
            // 
            this.linkLabel2.AutoSize = true;
            this.linkLabel2.Location = new System.Drawing.Point(182, 4);
            this.linkLabel2.Name = "linkLabel2";
            this.linkLabel2.Size = new System.Drawing.Size(26, 13);
            this.linkLabel2.TabIndex = 17;
            this.linkLabel2.TabStop = true;
            this.linkLabel2.Text = "sink";
            this.linkLabel2.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel2_LinkClicked);
            // 
            // llChangeRulesTo_Source
            // 
            this.llChangeRulesTo_Source.AutoSize = true;
            this.llChangeRulesTo_Source.Location = new System.Drawing.Point(137, 3);
            this.llChangeRulesTo_Source.Name = "llChangeRulesTo_Source";
            this.llChangeRulesTo_Source.Size = new System.Drawing.Size(39, 13);
            this.llChangeRulesTo_Source.TabIndex = 16;
            this.llChangeRulesTo_Source.TabStop = true;
            this.llChangeRulesTo_Source.Text = "source";
            this.llChangeRulesTo_Source.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llChangeRulesTo_Source_LinkClicked);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 4);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(133, 13);
            this.label5.TabIndex = 23;
            this.label5.Text = "Change selected rule(s) to:";
            // 
            // rbViewMode_OnlyNotInDb
            // 
            this.rbViewMode_OnlyNotInDb.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.rbViewMode_OnlyNotInDb.AutoSize = true;
            this.rbViewMode_OnlyNotInDb.Location = new System.Drawing.Point(219, 425);
            this.rbViewMode_OnlyNotInDb.Name = "rbViewMode_OnlyNotInDb";
            this.rbViewMode_OnlyNotInDb.Size = new System.Drawing.Size(99, 17);
            this.rbViewMode_OnlyNotInDb.TabIndex = 12;
            this.rbViewMode_OnlyNotInDb.Text = "only NOT in DB";
            this.rbViewMode_OnlyNotInDb.UseVisualStyleBackColor = true;
            this.rbViewMode_OnlyNotInDb.CheckedChanged += new System.EventHandler(this.rbViewMode_OnlyNotInDb_CheckedChanged);
            // 
            // rbViewMode_OnlyTaggedRules
            // 
            this.rbViewMode_OnlyTaggedRules.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.rbViewMode_OnlyTaggedRules.AutoSize = true;
            this.rbViewMode_OnlyTaggedRules.Location = new System.Drawing.Point(137, 425);
            this.rbViewMode_OnlyTaggedRules.Name = "rbViewMode_OnlyTaggedRules";
            this.rbViewMode_OnlyTaggedRules.Size = new System.Drawing.Size(84, 17);
            this.rbViewMode_OnlyTaggedRules.TabIndex = 11;
            this.rbViewMode_OnlyTaggedRules.Text = "only Tagged";
            this.rbViewMode_OnlyTaggedRules.UseVisualStyleBackColor = true;
            this.rbViewMode_OnlyTaggedRules.CheckedChanged += new System.EventHandler(this.rbViewMode_OnlyTaggedRules_CheckedChanged);
            // 
            // rbViewMode_AllRules
            // 
            this.rbViewMode_AllRules.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.rbViewMode_AllRules.AutoSize = true;
            this.rbViewMode_AllRules.Checked = true;
            this.rbViewMode_AllRules.Location = new System.Drawing.Point(70, 425);
            this.rbViewMode_AllRules.Name = "rbViewMode_AllRules";
            this.rbViewMode_AllRules.Size = new System.Drawing.Size(69, 17);
            this.rbViewMode_AllRules.TabIndex = 10;
            this.rbViewMode_AllRules.TabStop = true;
            this.rbViewMode_AllRules.Text = "ALL rules";
            this.rbViewMode_AllRules.UseVisualStyleBackColor = true;
            this.rbViewMode_AllRules.CheckedChanged += new System.EventHandler(this.rbViewMode_AllRules_CheckedChanged);
            // 
            // laLoadingRulePack
            // 
            this.laLoadingRulePack.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.laLoadingRulePack.AutoSize = true;
            this.laLoadingRulePack.BackColor = System.Drawing.SystemColors.Window;
            this.laLoadingRulePack.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.laLoadingRulePack.ForeColor = System.Drawing.Color.DarkGray;
            this.laLoadingRulePack.Location = new System.Drawing.Point(119, 219);
            this.laLoadingRulePack.Name = "laLoadingRulePack";
            this.laLoadingRulePack.Size = new System.Drawing.Size(333, 42);
            this.laLoadingRulePack.TabIndex = 2;
            this.laLoadingRulePack.Text = "Loading Rule Pack";
            this.laLoadingRulePack.Visible = false;
            // 
            // laImportingRulesFromLocalMySqlDB
            // 
            this.laImportingRulesFromLocalMySqlDB.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.laImportingRulesFromLocalMySqlDB.AutoSize = true;
            this.laImportingRulesFromLocalMySqlDB.BackColor = System.Drawing.SystemColors.Window;
            this.laImportingRulesFromLocalMySqlDB.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.laImportingRulesFromLocalMySqlDB.ForeColor = System.Drawing.Color.DarkGray;
            this.laImportingRulesFromLocalMySqlDB.Location = new System.Drawing.Point(-50, 220);
            this.laImportingRulesFromLocalMySqlDB.Name = "laImportingRulesFromLocalMySqlDB";
            this.laImportingRulesFromLocalMySqlDB.Size = new System.Drawing.Size(608, 42);
            this.laImportingRulesFromLocalMySqlDB.TabIndex = 3;
            this.laImportingRulesFromLocalMySqlDB.Text = "Importing rules from local MySql DB";
            this.laImportingRulesFromLocalMySqlDB.Visible = false;
            // 
            // laOnlyShowingRulesFor1Signature
            // 
            this.laOnlyShowingRulesFor1Signature.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.laOnlyShowingRulesFor1Signature.BackColor = System.Drawing.SystemColors.Window;
            this.laOnlyShowingRulesFor1Signature.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.laOnlyShowingRulesFor1Signature.ForeColor = System.Drawing.Color.SandyBrown;
            this.laOnlyShowingRulesFor1Signature.Location = new System.Drawing.Point(588, 653);
            this.laOnlyShowingRulesFor1Signature.Name = "laOnlyShowingRulesFor1Signature";
            this.laOnlyShowingRulesFor1Signature.Size = new System.Drawing.Size(121, 58);
            this.laOnlyShowingRulesFor1Signature.TabIndex = 6;
            this.laOnlyShowingRulesFor1Signature.Text = "Note: Only showing rules for 1 signature (refresh to restore normal view)";
            this.laOnlyShowingRulesFor1Signature.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.laOnlyShowingRulesFor1Signature.Visible = false;
            // 
            // dgvRules
            // 
            this.dgvRules.AllowDrop = true;
            this.dgvRules.AllowUserToAddRows = false;
            this.dgvRules.AllowUserToOrderColumns = true;
            this.dgvRules.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvRules.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvRules.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dgvRules.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvRules.GridColor = System.Drawing.SystemColors.Window;
            this.dgvRules.Location = new System.Drawing.Point(-2, 25);
            this.dgvRules.Name = "dgvRules";
            this.dgvRules.ReadOnly = true;
            this.dgvRules.RowHeadersWidth = 4;
            this.dgvRules.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvRules.Size = new System.Drawing.Size(497, 397);
            this.dgvRules.TabIndex = 1;
            this.dgvRules.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvRules_CellDoubleClick);
            this.dgvRules.UserDeletedRow += new System.Windows.Forms.DataGridViewRowEventHandler(this.dgvRules_UserDeletedRow);
            this.dgvRules.DragEnter += new System.Windows.Forms.DragEventHandler(this.dgvRules_DragEnter);
            this.dgvRules.Resize += new System.EventHandler(this.dgvRules_Resize);
            this.dgvRules.SelectionChanged += new System.EventHandler(this.dgvRules_SelectionChanged);
            this.dgvRules.DragDrop += new System.Windows.Forms.DragEventHandler(this.dgvRules_DragDrop);
            this.dgvRules.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvRules_CellContentClick);
            // 
            // tabControl2
            // 
            this.tabControl2.Controls.Add(this.tpLoadAndEditRules);
            this.tabControl2.Controls.Add(this.tpConfig);
            this.tabControl2.Controls.Add(this.tpRulesAnalysis);
            this.tabControl2.Controls.Add(this.tpDroppedData);
            this.tabControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl2.Location = new System.Drawing.Point(0, 0);
            this.tabControl2.Name = "tabControl2";
            this.tabControl2.SelectedIndex = 0;
            this.tabControl2.Size = new System.Drawing.Size(328, 462);
            this.tabControl2.TabIndex = 0;
            // 
            // tpLoadAndEditRules
            // 
            this.tpLoadAndEditRules.Controls.Add(this.groupBox4);
            this.tpLoadAndEditRules.Controls.Add(this.groupBox3);
            this.tpLoadAndEditRules.Location = new System.Drawing.Point(4, 22);
            this.tpLoadAndEditRules.Name = "tpLoadAndEditRules";
            this.tpLoadAndEditRules.Padding = new System.Windows.Forms.Padding(3);
            this.tpLoadAndEditRules.Size = new System.Drawing.Size(320, 405);
            this.tpLoadAndEditRules.TabIndex = 0;
            this.tpLoadAndEditRules.Text = "Load and Edit Rules";
            this.tpLoadAndEditRules.UseVisualStyleBackColor = true;
            // 
            // groupBox4
            // 
            this.groupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox4.Controls.Add(this.llClearSelectedChangedRules);
            this.groupBox4.Controls.Add(this.btMySqlSync_AddSelectedRulesToDatabase);
            this.groupBox4.Controls.Add(this.lvChangedRules);
            this.groupBox4.Controls.Add(this.lbNumberOfChangedRules);
            this.groupBox4.Controls.Add(this.label2);
            this.groupBox4.Controls.Add(this.llClearChangedRules);
            this.groupBox4.Controls.Add(this.label1);
            this.groupBox4.Controls.Add(this.btMySqlSync_DeleteSelectedRulesFromDatabase);
            this.groupBox4.Controls.Add(this.llRefreshChangedRules);
            this.groupBox4.Location = new System.Drawing.Point(6, 54);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(311, 347);
            this.groupBox4.TabIndex = 24;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Changed Rules";
            // 
            // llClearSelectedChangedRules
            // 
            this.llClearSelectedChangedRules.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.llClearSelectedChangedRules.AutoSize = true;
            this.llClearSelectedChangedRules.Location = new System.Drawing.Point(145, 278);
            this.llClearSelectedChangedRules.Name = "llClearSelectedChangedRules";
            this.llClearSelectedChangedRules.Size = new System.Drawing.Size(73, 13);
            this.llClearSelectedChangedRules.TabIndex = 22;
            this.llClearSelectedChangedRules.TabStop = true;
            this.llClearSelectedChangedRules.Text = "clear selected";
            this.llClearSelectedChangedRules.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llClearSelectedChangedRules_LinkClicked);
            // 
            // btMySqlSync_AddSelectedRulesToDatabase
            // 
            this.btMySqlSync_AddSelectedRulesToDatabase.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.btMySqlSync_AddSelectedRulesToDatabase.Location = new System.Drawing.Point(6, 19);
            this.btMySqlSync_AddSelectedRulesToDatabase.Name = "btMySqlSync_AddSelectedRulesToDatabase";
            this.btMySqlSync_AddSelectedRulesToDatabase.Size = new System.Drawing.Size(302, 23);
            this.btMySqlSync_AddSelectedRulesToDatabase.TabIndex = 11;
            this.btMySqlSync_AddSelectedRulesToDatabase.Text = "Apply rule(s) changes to Database (MySql)";
            this.btMySqlSync_AddSelectedRulesToDatabase.UseVisualStyleBackColor = true;
            this.btMySqlSync_AddSelectedRulesToDatabase.Click += new System.EventHandler(this.btMySqlSync_AddSelectedRulesToDatabase_Click);
            // 
            // lvChangedRules
            // 
            this.lvChangedRules.AllowDrop = true;
            this.lvChangedRules.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lvChangedRules.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chRuleType,
            this.chSignature});
            this.lvChangedRules.FullRowSelect = true;
            this.lvChangedRules.HideSelection = false;
            this.lvChangedRules.Location = new System.Drawing.Point(6, 48);
            this.lvChangedRules.Name = "lvChangedRules";
            this.lvChangedRules.Size = new System.Drawing.Size(302, 229);
            this.lvChangedRules.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.lvChangedRules.TabIndex = 20;
            this.lvChangedRules.UseCompatibleStateImageBehavior = false;
            this.lvChangedRules.View = System.Windows.Forms.View.Details;
            this.lvChangedRules.Resize += new System.EventHandler(this.lvChangedRules_Resize);
            this.lvChangedRules.DragDrop += new System.Windows.Forms.DragEventHandler(this.lvChangedRules_DragDrop);
            this.lvChangedRules.DragEnter += new System.Windows.Forms.DragEventHandler(this.lvChangedRules_DragEnter);
            // 
            // chRuleType
            // 
            this.chRuleType.Text = "Rule Type";
            this.chRuleType.Width = 70;
            // 
            // chSignature
            // 
            this.chSignature.Text = "Signature";
            this.chSignature.Width = 205;
            // 
            // lbNumberOfChangedRules
            // 
            this.lbNumberOfChangedRules.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lbNumberOfChangedRules.AutoSize = true;
            this.lbNumberOfChangedRules.Location = new System.Drawing.Point(126, 280);
            this.lbNumberOfChangedRules.Name = "lbNumberOfChangedRules";
            this.lbNumberOfChangedRules.Size = new System.Drawing.Size(13, 13);
            this.lbNumberOfChangedRules.TabIndex = 21;
            this.lbNumberOfChangedRules.Text = "0";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 279);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(114, 13);
            this.label2.TabIndex = 17;
            this.label2.Text = "Current changed rules:";
            // 
            // llClearChangedRules
            // 
            this.llClearChangedRules.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.llClearChangedRules.AutoSize = true;
            this.llClearChangedRules.Location = new System.Drawing.Point(221, 278);
            this.llClearChangedRules.Name = "llClearChangedRules";
            this.llClearChangedRules.Size = new System.Drawing.Size(43, 13);
            this.llClearChangedRules.TabIndex = 19;
            this.llClearChangedRules.TabStop = true;
            this.llClearChangedRules.Text = "clear all";
            this.llClearChangedRules.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llClearChangedRules_LinkClicked);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 325);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(262, 13);
            this.label1.TabIndex = 16;
            this.label1.Text = "This will delete all rules that match the rule(s) signature";
            // 
            // btMySqlSync_DeleteSelectedRulesFromDatabase
            // 
            this.btMySqlSync_DeleteSelectedRulesFromDatabase.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btMySqlSync_DeleteSelectedRulesFromDatabase.Location = new System.Drawing.Point(6, 298);
            this.btMySqlSync_DeleteSelectedRulesFromDatabase.Name = "btMySqlSync_DeleteSelectedRulesFromDatabase";
            this.btMySqlSync_DeleteSelectedRulesFromDatabase.Size = new System.Drawing.Size(203, 23);
            this.btMySqlSync_DeleteSelectedRulesFromDatabase.TabIndex = 12;
            this.btMySqlSync_DeleteSelectedRulesFromDatabase.Text = "Delete SIGNATURE(s) From Database";
            this.btMySqlSync_DeleteSelectedRulesFromDatabase.UseVisualStyleBackColor = true;
            this.btMySqlSync_DeleteSelectedRulesFromDatabase.Click += new System.EventHandler(this.btMySqlSync_DeleteSelectedRulesFromDatabase_Click);
            // 
            // llRefreshChangedRules
            // 
            this.llRefreshChangedRules.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.llRefreshChangedRules.AutoSize = true;
            this.llRefreshChangedRules.Location = new System.Drawing.Point(270, 278);
            this.llRefreshChangedRules.Name = "llRefreshChangedRules";
            this.llRefreshChangedRules.Size = new System.Drawing.Size(39, 13);
            this.llRefreshChangedRules.TabIndex = 18;
            this.llRefreshChangedRules.TabStop = true;
            this.llRefreshChangedRules.Text = "refresh";
            this.llRefreshChangedRules.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llRefreshChangedRules_LinkClicked);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btImportFromLocalMySql);
            this.groupBox3.Controls.Add(this.cbLanguages);
            this.groupBox3.Location = new System.Drawing.Point(3, 3);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(314, 45);
            this.groupBox3.TabIndex = 22;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Import rules from MySql";
            // 
            // btImportFromLocalMySql
            // 
            this.btImportFromLocalMySql.Location = new System.Drawing.Point(6, 15);
            this.btImportFromLocalMySql.Name = "btImportFromLocalMySql";
            this.btImportFromLocalMySql.Size = new System.Drawing.Size(187, 23);
            this.btImportFromLocalMySql.TabIndex = 10;
            this.btImportFromLocalMySql.Text = "Load rules for Language ->";
            this.btImportFromLocalMySql.UseVisualStyleBackColor = true;
            this.btImportFromLocalMySql.Click += new System.EventHandler(this.btImportFromLocalMySql_Click);
            // 
            // cbLanguages
            // 
            this.cbLanguages.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbLanguages.FormattingEnabled = true;
            this.cbLanguages.Location = new System.Drawing.Point(199, 17);
            this.cbLanguages.Name = "cbLanguages";
            this.cbLanguages.Size = new System.Drawing.Size(106, 21);
            this.cbLanguages.TabIndex = 8;
            this.cbLanguages.SelectedIndexChanged += new System.EventHandler(this.cbLanguages_SelectedIndexChanged);
            // 
            // tpConfig
            // 
            this.tpConfig.Controls.Add(this.groupBox7);
            this.tpConfig.Controls.Add(this.groupBox6);
            this.tpConfig.Controls.Add(this.groupBox2);
            this.tpConfig.Controls.Add(this.gbMySqlImportPreferences);
            this.tpConfig.Controls.Add(this.label7);
            this.tpConfig.Location = new System.Drawing.Point(4, 22);
            this.tpConfig.Name = "tpConfig";
            this.tpConfig.Padding = new System.Windows.Forms.Padding(3);
            this.tpConfig.Size = new System.Drawing.Size(320, 436);
            this.tpConfig.TabIndex = 1;
            this.tpConfig.Text = "Config";
            this.tpConfig.UseVisualStyleBackColor = true;
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.label15);
            this.groupBox7.Controls.Add(this.tbMySqlPort);
            this.groupBox7.Controls.Add(this.tbMySqlIPAddress);
            this.groupBox7.Controls.Add(this.label14);
            this.groupBox7.Controls.Add(this.cbShowMySqlPassword);
            this.groupBox7.Controls.Add(this.tbMySqlPassword);
            this.groupBox7.Controls.Add(this.label13);
            this.groupBox7.Controls.Add(this.tbMySqlUsername);
            this.groupBox7.Controls.Add(this.label12);
            this.groupBox7.Location = new System.Drawing.Point(7, 289);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(301, 129);
            this.groupBox7.TabIndex = 11;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "MySql Details";
            // 
            // cbShowMySqlPassword
            // 
            this.cbShowMySqlPassword.AutoSize = true;
            this.cbShowMySqlPassword.Location = new System.Drawing.Point(69, 67);
            this.cbShowMySqlPassword.Name = "cbShowMySqlPassword";
            this.cbShowMySqlPassword.Size = new System.Drawing.Size(99, 17);
            this.cbShowMySqlPassword.TabIndex = 26;
            this.cbShowMySqlPassword.Text = "show password";
            this.cbShowMySqlPassword.UseVisualStyleBackColor = true;
            this.cbShowMySqlPassword.CheckedChanged += new System.EventHandler(this.cbShowMySqlPassword_CheckedChanged);
            // 
            // tbMySqlPassword
            // 
            this.tbMySqlPassword.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbMySqlPassword.Location = new System.Drawing.Point(68, 45);
            this.tbMySqlPassword.Name = "tbMySqlPassword";
            this.tbMySqlPassword.Size = new System.Drawing.Size(177, 20);
            this.tbMySqlPassword.TabIndex = 25;
            this.tbMySqlPassword.TextChanged += new System.EventHandler(this.tbMySqlPassword_TextChanged);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(7, 49);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(53, 13);
            this.label13.TabIndex = 24;
            this.label13.Text = "Password";
            // 
            // tbMySqlUsername
            // 
            this.tbMySqlUsername.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbMySqlUsername.Location = new System.Drawing.Point(68, 19);
            this.tbMySqlUsername.Name = "tbMySqlUsername";
            this.tbMySqlUsername.Size = new System.Drawing.Size(177, 20);
            this.tbMySqlUsername.TabIndex = 23;
            this.tbMySqlUsername.TextChanged += new System.EventHandler(this.tbMySqlUsername_TextChanged);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(7, 20);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(55, 13);
            this.label12.TabIndex = 0;
            this.label12.Text = "Username";
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.cbOnChangeDeletePreviewRule);
            this.groupBox6.Controls.Add(this.cbColorCodeRules);
            this.groupBox6.Location = new System.Drawing.Point(7, 9);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(307, 88);
            this.groupBox6.TabIndex = 10;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "GUI Settings";
            // 
            // cbOnChangeDeletePreviewRule
            // 
            this.cbOnChangeDeletePreviewRule.AutoSize = true;
            this.cbOnChangeDeletePreviewRule.Checked = true;
            this.cbOnChangeDeletePreviewRule.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbOnChangeDeletePreviewRule.Location = new System.Drawing.Point(7, 40);
            this.cbOnChangeDeletePreviewRule.Name = "cbOnChangeDeletePreviewRule";
            this.cbOnChangeDeletePreviewRule.Size = new System.Drawing.Size(217, 17);
            this.cbOnChangeDeletePreviewRule.TabIndex = 1;
            this.cbOnChangeDeletePreviewRule.Text = "On Rule Change, Delete Previous Rule?";
            this.cbOnChangeDeletePreviewRule.UseVisualStyleBackColor = true;
            // 
            // cbColorCodeRules
            // 
            this.cbColorCodeRules.AutoSize = true;
            this.cbColorCodeRules.Checked = true;
            this.cbColorCodeRules.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbColorCodeRules.Location = new System.Drawing.Point(7, 20);
            this.cbColorCodeRules.Name = "cbColorCodeRules";
            this.cbColorCodeRules.Size = new System.Drawing.Size(102, 17);
            this.cbColorCodeRules.TabIndex = 0;
            this.cbColorCodeRules.Text = "Color code rules";
            this.cbColorCodeRules.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.tbChangeRulesTo_NewVulnName);
            this.groupBox2.Location = new System.Drawing.Point(7, 238);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(307, 45);
            this.groupBox2.TabIndex = 9;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Settings for \"Change Selected Rule(s)\" ";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(4, 26);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(114, 13);
            this.label4.TabIndex = 21;
            this.label4.Text = "new \'Vuln Type\' name:";
            // 
            // tbChangeRulesTo_NewVulnName
            // 
            this.tbChangeRulesTo_NewVulnName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbChangeRulesTo_NewVulnName.Location = new System.Drawing.Point(124, 21);
            this.tbChangeRulesTo_NewVulnName.Name = "tbChangeRulesTo_NewVulnName";
            this.tbChangeRulesTo_NewVulnName.Size = new System.Drawing.Size(177, 20);
            this.tbChangeRulesTo_NewVulnName.TabIndex = 22;
            this.tbChangeRulesTo_NewVulnName.Text = "O2.Modified.Vulnerability";
            // 
            // gbMySqlImportPreferences
            // 
            this.gbMySqlImportPreferences.Controls.Add(this.cbMySqlImport_Callbacks);
            this.gbMySqlImportPreferences.Controls.Add(this.cbMySqlImport_AnyLow);
            this.gbMySqlImportPreferences.Controls.Add(this.cbMySqlImport_AnyMedium);
            this.gbMySqlImportPreferences.Controls.Add(this.cbMySqlImport_DontPropagateTaint);
            this.gbMySqlImportPreferences.Controls.Add(this.cbMySqlImport_AnyHigh);
            this.gbMySqlImportPreferences.Controls.Add(this.cbMySqlImport_PropagateTaint);
            this.gbMySqlImportPreferences.Controls.Add(this.cbMySqlImport_Sinks);
            this.gbMySqlImportPreferences.Controls.Add(this.cbMySqlImport_Sources);
            this.gbMySqlImportPreferences.Location = new System.Drawing.Point(6, 103);
            this.gbMySqlImportPreferences.Name = "gbMySqlImportPreferences";
            this.gbMySqlImportPreferences.Size = new System.Drawing.Size(308, 129);
            this.gbMySqlImportPreferences.TabIndex = 7;
            this.gbMySqlImportPreferences.TabStop = false;
            this.gbMySqlImportPreferences.Text = "MySql Import Preferences";
            // 
            // cbMySqlImport_Callbacks
            // 
            this.cbMySqlImport_Callbacks.AutoSize = true;
            this.cbMySqlImport_Callbacks.Checked = true;
            this.cbMySqlImport_Callbacks.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbMySqlImport_Callbacks.Location = new System.Drawing.Point(15, 61);
            this.cbMySqlImport_Callbacks.Name = "cbMySqlImport_Callbacks";
            this.cbMySqlImport_Callbacks.Size = new System.Drawing.Size(72, 17);
            this.cbMySqlImport_Callbacks.TabIndex = 9;
            this.cbMySqlImport_Callbacks.Text = "Callbacks";
            this.cbMySqlImport_Callbacks.UseVisualStyleBackColor = true;
            // 
            // cbMySqlImport_AnyLow
            // 
            this.cbMySqlImport_AnyLow.AutoSize = true;
            this.cbMySqlImport_AnyLow.Location = new System.Drawing.Point(148, 61);
            this.cbMySqlImport_AnyLow.Name = "cbMySqlImport_AnyLow";
            this.cbMySqlImport_AnyLow.Size = new System.Drawing.Size(71, 17);
            this.cbMySqlImport_AnyLow.TabIndex = 7;
            this.cbMySqlImport_AnyLow.Text = "\'Any Low\'";
            this.cbMySqlImport_AnyLow.UseVisualStyleBackColor = true;
            // 
            // cbMySqlImport_AnyMedium
            // 
            this.cbMySqlImport_AnyMedium.AutoSize = true;
            this.cbMySqlImport_AnyMedium.Location = new System.Drawing.Point(148, 40);
            this.cbMySqlImport_AnyMedium.Name = "cbMySqlImport_AnyMedium";
            this.cbMySqlImport_AnyMedium.Size = new System.Drawing.Size(88, 17);
            this.cbMySqlImport_AnyMedium.TabIndex = 6;
            this.cbMySqlImport_AnyMedium.Text = "\'Any Medium\'";
            this.cbMySqlImport_AnyMedium.UseVisualStyleBackColor = true;
            // 
            // cbMySqlImport_DontPropagateTaint
            // 
            this.cbMySqlImport_DontPropagateTaint.AutoSize = true;
            this.cbMySqlImport_DontPropagateTaint.Checked = true;
            this.cbMySqlImport_DontPropagateTaint.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbMySqlImport_DontPropagateTaint.Location = new System.Drawing.Point(15, 103);
            this.cbMySqlImport_DontPropagateTaint.Name = "cbMySqlImport_DontPropagateTaint";
            this.cbMySqlImport_DontPropagateTaint.Size = new System.Drawing.Size(128, 17);
            this.cbMySqlImport_DontPropagateTaint.TabIndex = 5;
            this.cbMySqlImport_DontPropagateTaint.Text = "Dont Propagate Taint";
            this.cbMySqlImport_DontPropagateTaint.UseVisualStyleBackColor = true;
            // 
            // cbMySqlImport_AnyHigh
            // 
            this.cbMySqlImport_AnyHigh.AutoSize = true;
            this.cbMySqlImport_AnyHigh.Location = new System.Drawing.Point(148, 19);
            this.cbMySqlImport_AnyHigh.Name = "cbMySqlImport_AnyHigh";
            this.cbMySqlImport_AnyHigh.Size = new System.Drawing.Size(73, 17);
            this.cbMySqlImport_AnyHigh.TabIndex = 4;
            this.cbMySqlImport_AnyHigh.Text = "\'Any High\'";
            this.cbMySqlImport_AnyHigh.UseVisualStyleBackColor = true;
            // 
            // cbMySqlImport_PropagateTaint
            // 
            this.cbMySqlImport_PropagateTaint.AutoSize = true;
            this.cbMySqlImport_PropagateTaint.Checked = true;
            this.cbMySqlImport_PropagateTaint.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbMySqlImport_PropagateTaint.Location = new System.Drawing.Point(15, 82);
            this.cbMySqlImport_PropagateTaint.Name = "cbMySqlImport_PropagateTaint";
            this.cbMySqlImport_PropagateTaint.Size = new System.Drawing.Size(102, 17);
            this.cbMySqlImport_PropagateTaint.TabIndex = 3;
            this.cbMySqlImport_PropagateTaint.Text = "Propagate Taint";
            this.cbMySqlImport_PropagateTaint.UseVisualStyleBackColor = true;
            // 
            // cbMySqlImport_Sinks
            // 
            this.cbMySqlImport_Sinks.AutoSize = true;
            this.cbMySqlImport_Sinks.Checked = true;
            this.cbMySqlImport_Sinks.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbMySqlImport_Sinks.Location = new System.Drawing.Point(15, 40);
            this.cbMySqlImport_Sinks.Name = "cbMySqlImport_Sinks";
            this.cbMySqlImport_Sinks.Size = new System.Drawing.Size(52, 17);
            this.cbMySqlImport_Sinks.TabIndex = 2;
            this.cbMySqlImport_Sinks.Text = "Sinks";
            this.cbMySqlImport_Sinks.UseVisualStyleBackColor = true;
            this.cbMySqlImport_Sinks.CheckedChanged += new System.EventHandler(this.cbMySqlImport_Sinks_CheckedChanged);
            // 
            // cbMySqlImport_Sources
            // 
            this.cbMySqlImport_Sources.AutoSize = true;
            this.cbMySqlImport_Sources.Checked = true;
            this.cbMySqlImport_Sources.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbMySqlImport_Sources.Location = new System.Drawing.Point(15, 19);
            this.cbMySqlImport_Sources.Name = "cbMySqlImport_Sources";
            this.cbMySqlImport_Sources.Size = new System.Drawing.Size(65, 17);
            this.cbMySqlImport_Sources.TabIndex = 1;
            this.cbMySqlImport_Sources.Text = "Sources";
            this.cbMySqlImport_Sources.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(35, 318);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(0, 13);
            this.label7.TabIndex = 0;
            // 
            // tpRulesAnalysis
            // 
            this.tpRulesAnalysis.Controls.Add(this.label11);
            this.tpRulesAnalysis.Controls.Add(this.label9);
            this.tpRulesAnalysis.Controls.Add(this.label8);
            this.tpRulesAnalysis.Controls.Add(this.label10);
            this.tpRulesAnalysis.Controls.Add(this.llShowRulesWithSinksAndPropagateTaint);
            this.tpRulesAnalysis.Location = new System.Drawing.Point(4, 22);
            this.tpRulesAnalysis.Name = "tpRulesAnalysis";
            this.tpRulesAnalysis.Padding = new System.Windows.Forms.Padding(3);
            this.tpRulesAnalysis.Size = new System.Drawing.Size(320, 405);
            this.tpRulesAnalysis.TabIndex = 2;
            this.tpRulesAnalysis.Text = "Rules Analysis";
            this.tpRulesAnalysis.UseVisualStyleBackColor = true;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(13, 137);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(201, 13);
            this.label11.TabIndex = 27;
            this.label11.Text = "(also deletes other rules for this signature)";
            // 
            // label9
            // 
            this.label9.Location = new System.Drawing.Point(3, 62);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(181, 16);
            this.label9.TabIndex = 2;
            this.label9.Text = "Show incompatible rules (TO DO)";
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(3, 8);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(181, 32);
            this.label8.TabIndex = 1;
            this.label8.Text = "The following links perform specific analysis to the currently loaded rules";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(13, 173);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(296, 13);
            this.label10.TabIndex = 25;
            this.label10.Text = "(will also add ALL (if any) other rules that match this signature)";
            // 
            // llShowRulesWithSinksAndPropagateTaint
            // 
            this.llShowRulesWithSinksAndPropagateTaint.AutoSize = true;
            this.llShowRulesWithSinksAndPropagateTaint.Location = new System.Drawing.Point(3, 40);
            this.llShowRulesWithSinksAndPropagateTaint.Name = "llShowRulesWithSinksAndPropagateTaint";
            this.llShowRulesWithSinksAndPropagateTaint.Size = new System.Drawing.Size(229, 13);
            this.llShowRulesWithSinksAndPropagateTaint.TabIndex = 0;
            this.llShowRulesWithSinksAndPropagateTaint.TabStop = true;
            this.llShowRulesWithSinksAndPropagateTaint.Text = "Show rules with both Sink and Propagate Taint";
            this.llShowRulesWithSinksAndPropagateTaint.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llShowRulesWithSinksAndPropagateTaint_LinkClicked);
            // 
            // tpDroppedData
            // 
            this.tpDroppedData.Controls.Add(this.groupBox5);
            this.tpDroppedData.Controls.Add(this.groupBox1);
            this.tpDroppedData.Location = new System.Drawing.Point(4, 22);
            this.tpDroppedData.Name = "tpDroppedData";
            this.tpDroppedData.Padding = new System.Windows.Forms.Padding(3);
            this.tpDroppedData.Size = new System.Drawing.Size(320, 405);
            this.tpDroppedData.TabIndex = 3;
            this.tpDroppedData.Text = "Dropped Data";
            this.tpDroppedData.UseVisualStyleBackColor = true;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.cbFindingsDrop_KeepRulesLoadedFromDatabase);
            this.groupBox5.Location = new System.Drawing.Point(7, 199);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(303, 129);
            this.groupBox5.TabIndex = 9;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Settings for Findings Drop";
            // 
            // cbFindingsDrop_KeepRulesLoadedFromDatabase
            // 
            this.cbFindingsDrop_KeepRulesLoadedFromDatabase.AutoSize = true;
            this.cbFindingsDrop_KeepRulesLoadedFromDatabase.Checked = true;
            this.cbFindingsDrop_KeepRulesLoadedFromDatabase.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbFindingsDrop_KeepRulesLoadedFromDatabase.Location = new System.Drawing.Point(6, 19);
            this.cbFindingsDrop_KeepRulesLoadedFromDatabase.Name = "cbFindingsDrop_KeepRulesLoadedFromDatabase";
            this.cbFindingsDrop_KeepRulesLoadedFromDatabase.Size = new System.Drawing.Size(195, 17);
            this.cbFindingsDrop_KeepRulesLoadedFromDatabase.TabIndex = 26;
            this.cbFindingsDrop_KeepRulesLoadedFromDatabase.Text = "Keep Rules Loaded From Database";
            this.cbFindingsDrop_KeepRulesLoadedFromDatabase.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cbCirDataDrop_DontMarkAsCallbackOrSinksMethodsWithNoParameters);
            this.groupBox1.Controls.Add(this.cbCirDataDrop_DontAddIfThereAreNoCallersAndCallees);
            this.groupBox1.Controls.Add(this.cbCirDataDrop_AddExternalMethodsAsSourcesAndSinks);
            this.groupBox1.Controls.Add(this.cbCirDataDrop_KeepRulesLoadedFromDatabase);
            this.groupBox1.Controls.Add(this.cbCirDataDrop_DontAddIfRuleSignatureAlreadyExists);
            this.groupBox1.Controls.Add(this.cbCirDataDrop_AddInternalMethodsWithNoCallersAsCallbacks);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.tbCirDataDrop_NewRulesVulnType);
            this.groupBox1.Location = new System.Drawing.Point(3, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(307, 186);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Settings for CirData Drops";
            // 
            // cbCirDataDrop_DontMarkAsCallbackOrSinksMethodsWithNoParameters
            // 
            this.cbCirDataDrop_DontMarkAsCallbackOrSinksMethodsWithNoParameters.AutoSize = true;
            this.cbCirDataDrop_DontMarkAsCallbackOrSinksMethodsWithNoParameters.Checked = true;
            this.cbCirDataDrop_DontMarkAsCallbackOrSinksMethodsWithNoParameters.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbCirDataDrop_DontMarkAsCallbackOrSinksMethodsWithNoParameters.Location = new System.Drawing.Point(6, 57);
            this.cbCirDataDrop_DontMarkAsCallbackOrSinksMethodsWithNoParameters.Name = "cbCirDataDrop_DontMarkAsCallbackOrSinksMethodsWithNoParameters";
            this.cbCirDataDrop_DontMarkAsCallbackOrSinksMethodsWithNoParameters.Size = new System.Drawing.Size(297, 17);
            this.cbCirDataDrop_DontMarkAsCallbackOrSinksMethodsWithNoParameters.TabIndex = 29;
            this.cbCirDataDrop_DontMarkAsCallbackOrSinksMethodsWithNoParameters.Text = "Don\'t mark as Callback or Sinks methods with NO params";
            this.cbCirDataDrop_DontMarkAsCallbackOrSinksMethodsWithNoParameters.UseVisualStyleBackColor = true;
            // 
            // cbCirDataDrop_DontAddIfThereAreNoCallersAndCallees
            // 
            this.cbCirDataDrop_DontAddIfThereAreNoCallersAndCallees.AutoSize = true;
            this.cbCirDataDrop_DontAddIfThereAreNoCallersAndCallees.Checked = true;
            this.cbCirDataDrop_DontAddIfThereAreNoCallersAndCallees.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbCirDataDrop_DontAddIfThereAreNoCallersAndCallees.Location = new System.Drawing.Point(6, 159);
            this.cbCirDataDrop_DontAddIfThereAreNoCallersAndCallees.Name = "cbCirDataDrop_DontAddIfThereAreNoCallersAndCallees";
            this.cbCirDataDrop_DontAddIfThereAreNoCallersAndCallees.Size = new System.Drawing.Size(230, 17);
            this.cbCirDataDrop_DontAddIfThereAreNoCallersAndCallees.TabIndex = 28;
            this.cbCirDataDrop_DontAddIfThereAreNoCallersAndCallees.Text = "Don\'t add if there are no callers and callees";
            this.cbCirDataDrop_DontAddIfThereAreNoCallersAndCallees.UseVisualStyleBackColor = true;
            // 
            // cbCirDataDrop_AddExternalMethodsAsSourcesAndSinks
            // 
            this.cbCirDataDrop_AddExternalMethodsAsSourcesAndSinks.AutoSize = true;
            this.cbCirDataDrop_AddExternalMethodsAsSourcesAndSinks.Checked = true;
            this.cbCirDataDrop_AddExternalMethodsAsSourcesAndSinks.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbCirDataDrop_AddExternalMethodsAsSourcesAndSinks.Location = new System.Drawing.Point(6, 19);
            this.cbCirDataDrop_AddExternalMethodsAsSourcesAndSinks.Name = "cbCirDataDrop_AddExternalMethodsAsSourcesAndSinks";
            this.cbCirDataDrop_AddExternalMethodsAsSourcesAndSinks.Size = new System.Drawing.Size(234, 17);
            this.cbCirDataDrop_AddExternalMethodsAsSourcesAndSinks.TabIndex = 26;
            this.cbCirDataDrop_AddExternalMethodsAsSourcesAndSinks.Text = "Add external methods as Sources and Sinks";
            this.cbCirDataDrop_AddExternalMethodsAsSourcesAndSinks.UseVisualStyleBackColor = true;
            // 
            // cbCirDataDrop_KeepRulesLoadedFromDatabase
            // 
            this.cbCirDataDrop_KeepRulesLoadedFromDatabase.AutoSize = true;
            this.cbCirDataDrop_KeepRulesLoadedFromDatabase.Checked = true;
            this.cbCirDataDrop_KeepRulesLoadedFromDatabase.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbCirDataDrop_KeepRulesLoadedFromDatabase.Location = new System.Drawing.Point(6, 121);
            this.cbCirDataDrop_KeepRulesLoadedFromDatabase.Name = "cbCirDataDrop_KeepRulesLoadedFromDatabase";
            this.cbCirDataDrop_KeepRulesLoadedFromDatabase.Size = new System.Drawing.Size(195, 17);
            this.cbCirDataDrop_KeepRulesLoadedFromDatabase.TabIndex = 25;
            this.cbCirDataDrop_KeepRulesLoadedFromDatabase.Text = "Keep Rules Loaded From Database";
            this.cbCirDataDrop_KeepRulesLoadedFromDatabase.UseVisualStyleBackColor = true;
            // 
            // cbCirDataDrop_DontAddIfRuleSignatureAlreadyExists
            // 
            this.cbCirDataDrop_DontAddIfRuleSignatureAlreadyExists.AutoSize = true;
            this.cbCirDataDrop_DontAddIfRuleSignatureAlreadyExists.Checked = true;
            this.cbCirDataDrop_DontAddIfRuleSignatureAlreadyExists.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbCirDataDrop_DontAddIfRuleSignatureAlreadyExists.Location = new System.Drawing.Point(6, 140);
            this.cbCirDataDrop_DontAddIfRuleSignatureAlreadyExists.Name = "cbCirDataDrop_DontAddIfRuleSignatureAlreadyExists";
            this.cbCirDataDrop_DontAddIfRuleSignatureAlreadyExists.Size = new System.Drawing.Size(262, 17);
            this.cbCirDataDrop_DontAddIfRuleSignatureAlreadyExists.TabIndex = 0;
            this.cbCirDataDrop_DontAddIfRuleSignatureAlreadyExists.Text = "Don\'t add if rule with same signature already exists";
            this.cbCirDataDrop_DontAddIfRuleSignatureAlreadyExists.UseVisualStyleBackColor = true;
            // 
            // cbCirDataDrop_AddInternalMethodsWithNoCallersAsCallbacks
            // 
            this.cbCirDataDrop_AddInternalMethodsWithNoCallersAsCallbacks.AutoSize = true;
            this.cbCirDataDrop_AddInternalMethodsWithNoCallersAsCallbacks.Checked = true;
            this.cbCirDataDrop_AddInternalMethodsWithNoCallersAsCallbacks.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbCirDataDrop_AddInternalMethodsWithNoCallersAsCallbacks.Location = new System.Drawing.Point(6, 38);
            this.cbCirDataDrop_AddInternalMethodsWithNoCallersAsCallbacks.Name = "cbCirDataDrop_AddInternalMethodsWithNoCallersAsCallbacks";
            this.cbCirDataDrop_AddInternalMethodsWithNoCallersAsCallbacks.Size = new System.Drawing.Size(258, 17);
            this.cbCirDataDrop_AddInternalMethodsWithNoCallersAsCallbacks.TabIndex = 27;
            this.cbCirDataDrop_AddInternalMethodsWithNoCallersAsCallbacks.Text = "Add internal methods with no callers as Callbacks";
            this.cbCirDataDrop_AddInternalMethodsWithNoCallersAsCallbacks.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(3, 98);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(173, 13);
            this.label6.TabIndex = 23;
            this.label6.Text = "Mark new rules with the Vuln Type:";
            // 
            // tbCirDataDrop_NewRulesVulnType
            // 
            this.tbCirDataDrop_NewRulesVulnType.Location = new System.Drawing.Point(179, 95);
            this.tbCirDataDrop_NewRulesVulnType.Name = "tbCirDataDrop_NewRulesVulnType";
            this.tbCirDataDrop_NewRulesVulnType.Size = new System.Drawing.Size(85, 20);
            this.tbCirDataDrop_NewRulesVulnType.TabIndex = 24;
            this.tbCirDataDrop_NewRulesVulnType.Text = "Not Mapped";
            // 
            // toolStripContainer1
            // 
            // 
            // toolStripContainer1.ContentPanel
            // 
            this.toolStripContainer1.ContentPanel.Controls.Add(this.splitContainer1);
            this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(1039, 466);
            this.toolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStripContainer1.Location = new System.Drawing.Point(0, 0);
            this.toolStripContainer1.Name = "toolStripContainer1";
            this.toolStripContainer1.Size = new System.Drawing.Size(1039, 491);
            this.toolStripContainer1.TabIndex = 7;
            this.toolStripContainer1.Text = "toolStripContainer1";
            // 
            // toolStripContainer1.TopToolStripPanel
            // 
            this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.tsRulePackViewer);
            // 
            // tbMySqlIPAddress
            // 
            this.tbMySqlIPAddress.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbMySqlIPAddress.Location = new System.Drawing.Point(68, 88);
            this.tbMySqlIPAddress.Name = "tbMySqlIPAddress";
            this.tbMySqlIPAddress.Size = new System.Drawing.Size(90, 20);
            this.tbMySqlIPAddress.TabIndex = 28;
            this.tbMySqlIPAddress.TextChanged += new System.EventHandler(this.tbMySqlIPAddress_TextChanged);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(7, 92);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(47, 13);
            this.label14.TabIndex = 27;
            this.label14.Text = "IP / Port";
            // 
            // tbMySqlPort
            // 
            this.tbMySqlPort.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbMySqlPort.Location = new System.Drawing.Point(176, 88);
            this.tbMySqlPort.Name = "tbMySqlPort";
            this.tbMySqlPort.Size = new System.Drawing.Size(69, 20);
            this.tbMySqlPort.TabIndex = 29;
            this.tbMySqlPort.TextChanged += new System.EventHandler(this.tbMySqlPort_TextChanged);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(164, 91);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(10, 13);
            this.label15.TabIndex = 30;
            this.label15.Text = ":";
            // 
            // ascx_RulePackViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.toolStripContainer1);
            this.Name = "ascx_RulePackViewer";
            this.Size = new System.Drawing.Size(1039, 491);
            this.Load += new System.EventHandler(this.ascx_RulePackViewer_Load);
            this.tsRulePackViewer.ResumeLayout(false);
            this.tsRulePackViewer.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.scRulesAndMySqlProperties.Panel1.ResumeLayout(false);
            this.scRulesAndMySqlProperties.Panel1.PerformLayout();
            this.scRulesAndMySqlProperties.Panel2.ResumeLayout(false);
            this.scRulesAndMySqlProperties.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvRules)).EndInit();
            this.tabControl2.ResumeLayout(false);
            this.tpLoadAndEditRules.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.tpConfig.ResumeLayout(false);
            this.tpConfig.PerformLayout();
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.gbMySqlImportPreferences.ResumeLayout(false);
            this.gbMySqlImportPreferences.PerformLayout();
            this.tpRulesAnalysis.ResumeLayout(false);
            this.tpRulesAnalysis.PerformLayout();
            this.tpDroppedData.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.toolStripContainer1.ContentPanel.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.PerformLayout();
            this.toolStripContainer1.ResumeLayout(false);
            this.toolStripContainer1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolStripPanel BottomToolStripPanel;
        private System.Windows.Forms.ToolStripPanel TopToolStripPanel;
        private System.Windows.Forms.ToolStripPanel RightToolStripPanel;
        private System.Windows.Forms.ToolStripPanel LeftToolStripPanel;
        private System.Windows.Forms.ToolStripContentPanel ContentPanel;
        private System.Windows.Forms.ToolStrip tsRulePackViewer;
        private System.Windows.Forms.ToolStripButton btEditSelectedRule;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.ToolStripButton btRemoveAllLoadedRules;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripComboBox cbTypeOfRuleToView;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private System.Windows.Forms.ToolStripTextBox tbSignatureFilter;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton btImportFromLocalMySqlOunceDatabase;
        private System.Windows.Forms.ToolStripButton btPreferences;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton btSaveCurrentFilter;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripButton btSaveAllLoadedRules;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripButton btRefreshView;
        private System.Windows.Forms.ToolStripLabel laNumberOfRulesLoaded;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripLabel laNotAllRulesShow;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private O2.Views.ASCX.DataViewers.ascx_FunctionsViewer functionsViewer;
        private System.Windows.Forms.SplitContainer scRulesAndMySqlProperties;
        private System.Windows.Forms.LinkLabel llDragSelectedRules;
        private System.Windows.Forms.LinkLabel llDragFilteredRules;
        private System.Windows.Forms.LinkLabel llDragAllLoadedRules;
        private System.Windows.Forms.LinkLabel llChangeRulesTo_Source;
        private System.Windows.Forms.LinkLabel llChangeRulesTo_TaintPropagator;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.LinkLabel llChangeRulesTo_Callback;
        private System.Windows.Forms.LinkLabel linkLabel2;
        private System.Windows.Forms.RadioButton rbViewMode_TaggedAndInDb;
        private System.Windows.Forms.LinkLabel llChangeRulesTo_DontPropagateTaint;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RadioButton rbViewMode_OnlyNotInDb;
        private System.Windows.Forms.RadioButton rbViewMode_OnlyTaggedRules;
        private System.Windows.Forms.RadioButton rbViewMode_AllRules;
        private System.Windows.Forms.Label laLoadingRulePack;
        private System.Windows.Forms.Label laImportingRulesFromLocalMySqlDB;
        private System.Windows.Forms.Label laOnlyShowingRulesFor1Signature;
        private System.Windows.Forms.DataGridView dgvRules;
        private System.Windows.Forms.TabControl tabControl2;
        private System.Windows.Forms.TabPage tpLoadAndEditRules;
        private System.Windows.Forms.ListView lvChangedRules;
        private System.Windows.Forms.ColumnHeader chRuleType;
        private System.Windows.Forms.ColumnHeader chSignature;
        private System.Windows.Forms.Label lbNumberOfChangedRules;
        private System.Windows.Forms.Button btImportFromLocalMySql;
        private System.Windows.Forms.ComboBox cbLanguages;
        private System.Windows.Forms.LinkLabel llClearChangedRules;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.LinkLabel llRefreshChangedRules;
        private System.Windows.Forms.Button btMySqlSync_AddSelectedRulesToDatabase;
        private System.Windows.Forms.Button btMySqlSync_DeleteSelectedRulesFromDatabase;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabPage tpConfig;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbChangeRulesTo_NewVulnName;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox cbCirDataDrop_DontAddIfThereAreNoCallersAndCallees;
        private System.Windows.Forms.CheckBox cbCirDataDrop_AddExternalMethodsAsSourcesAndSinks;
        private System.Windows.Forms.CheckBox cbCirDataDrop_KeepRulesLoadedFromDatabase;
        private System.Windows.Forms.CheckBox cbCirDataDrop_DontAddIfRuleSignatureAlreadyExists;
        private System.Windows.Forms.CheckBox cbCirDataDrop_AddInternalMethodsWithNoCallersAsCallbacks;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox tbCirDataDrop_NewRulesVulnType;
        private System.Windows.Forms.GroupBox gbMySqlImportPreferences;
        private System.Windows.Forms.CheckBox cbMySqlImport_Callbacks;
        private System.Windows.Forms.CheckBox cbMySqlImport_AnyLow;
        private System.Windows.Forms.CheckBox cbMySqlImport_AnyMedium;
        private System.Windows.Forms.CheckBox cbMySqlImport_DontPropagateTaint;
        private System.Windows.Forms.CheckBox cbMySqlImport_AnyHigh;
        private System.Windows.Forms.CheckBox cbMySqlImport_PropagateTaint;
        private System.Windows.Forms.CheckBox cbMySqlImport_Sinks;
        private System.Windows.Forms.CheckBox cbMySqlImport_Sources;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TabPage tpRulesAnalysis;
        private System.Windows.Forms.LinkLabel llShowRulesWithSinksAndPropagateTaint;
        private System.Windows.Forms.ToolStripContainer toolStripContainer1;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.LinkLabel llChangeRulesTo_ToBeDeleted;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.RadioButton rbViewMode_OnlyNotInDbAndMapped;
        private System.Windows.Forms.CheckBox cbCirDataDrop_DontMarkAsCallbackOrSinksMethodsWithNoParameters;
        private System.Windows.Forms.TabPage tpDroppedData;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.CheckBox cbFindingsDrop_KeepRulesLoadedFromDatabase;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.CheckBox cbColorCodeRules;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.CheckBox cbOnChangeDeletePreviewRule;
        private System.Windows.Forms.ToolStripButton btNewRule;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator9;
        private System.Windows.Forms.LinkLabel llClearSelectedChangedRules;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.TextBox tbMySqlUsername;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.CheckBox cbShowMySqlPassword;
        private System.Windows.Forms.TextBox tbMySqlPassword;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox tbMySqlIPAddress;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox tbMySqlPort;


    }
}
