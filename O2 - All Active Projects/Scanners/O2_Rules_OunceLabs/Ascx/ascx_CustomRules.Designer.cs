// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using O2.Kernel.CodeUtils;
using O2.Views.ASCX.CoreControls;

namespace O2.Rules.OunceLabs.Ascx
{
    partial class ascx_CustomRules
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            this.btRefresh = new System.Windows.Forms.Button();
            this.dtDeleteCustomRule = new System.Windows.Forms.Button();
            this.dgvCustomRules = new System.Windows.Forms.DataGridView();
            this.tcRules = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.lbCurrentMethodDbId = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.lbSelectedMethodVulnId = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lbSelectedActionObjectId = new System.Windows.Forms.Label();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.dgvLddb_rec = new System.Windows.Forms.DataGridView();
            this.label4 = new System.Windows.Forms.Label();
            this.dgvLddb_actionobjects = new System.Windows.Forms.DataGridView();
            this.label5 = new System.Windows.Forms.Label();
            this.scActionObjectsAndSelectViews = new System.Windows.Forms.SplitContainer();
            this.gbLddbTables = new System.Windows.Forms.GroupBox();
            this.cbLddb_writes_through_info = new System.Windows.Forms.CheckBox();
            this.cbLddb_stored_writeable_alias_info = new System.Windows.Forms.CheckBox();
            this.cbLddb_ao_options = new System.Windows.Forms.CheckBox();
            this.cbLddb_source_info = new System.Windows.Forms.CheckBox();
            this.cdLddb_validation_descriptor = new System.Windows.Forms.CheckBox();
            this.cbLddb_sink_info = new System.Windows.Forms.CheckBox();
            this.cbLddb_taint_info = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.lbCurrentMethodSignature = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btMarkMethodAsSink = new System.Windows.Forms.Button();
            this.ascx_DropObject1 = new ascx_DropObject();
            this.dgvCustomRulesForMethodSignature = new System.Windows.Forms.DataGridView();
            this.btDeleteSelectedActionObject = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cbEditCustomRules_Type = new System.Windows.Forms.ComboBox();
            this.cbEditCustomRules_Severity = new System.Windows.Forms.ComboBox();
            this.cbEditCustomRules_Signature = new System.Windows.Forms.ComboBox();
            this.cbEditCustomRules_vuln_type = new System.Windows.Forms.ComboBox();
            this.cbEditCustomRules_Trace = new System.Windows.Forms.ComboBox();
            this.label40 = new System.Windows.Forms.Label();
            this.label41 = new System.Windows.Forms.Label();
            this.label37 = new System.Windows.Forms.Label();
            this.label36 = new System.Windows.Forms.Label();
            this.cbEditCustomRules_DbId = new System.Windows.Forms.ComboBox();
            this.label35 = new System.Windows.Forms.Label();
            this.label34 = new System.Windows.Forms.Label();
            this.label32 = new System.Windows.Forms.Label();
            this.label31 = new System.Windows.Forms.Label();
            this.rbEditCustomRule_Sink = new System.Windows.Forms.RadioButton();
            this.label30 = new System.Windows.Forms.Label();
            this.rbEditCustomRule_Source = new System.Windows.Forms.RadioButton();
            this.rbEditCustomRule_ActionObject = new System.Windows.Forms.RadioButton();
            this.btEditCustomRule_markSinkAsTaintPropagator = new System.Windows.Forms.Button();
            this.btDeleteSourceActionObject = new System.Windows.Forms.Button();
            this.tpCurrentRules = new System.Windows.Forms.TabPage();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCustomRules)).BeginInit();
            this.tcRules.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLddb_rec)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLddb_actionobjects)).BeginInit();
            this.scActionObjectsAndSelectViews.Panel1.SuspendLayout();
            this.scActionObjectsAndSelectViews.Panel2.SuspendLayout();
            this.scActionObjectsAndSelectViews.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCustomRulesForMethodSignature)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.tpCurrentRules.SuspendLayout();
            this.SuspendLayout();
            // 
            // btRefresh
            // 
            this.btRefresh.Location = new System.Drawing.Point(6, 3);
            this.btRefresh.Name = "btRefresh";
            this.btRefresh.Size = new System.Drawing.Size(112, 23);
            this.btRefresh.TabIndex = 1;
            this.btRefresh.Text = "Refresh";
            this.btRefresh.UseVisualStyleBackColor = true;
            // 
            // dtDeleteCustomRule
            // 
            this.dtDeleteCustomRule.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.dtDeleteCustomRule.Location = new System.Drawing.Point(805, 3);
            this.dtDeleteCustomRule.Name = "dtDeleteCustomRule";
            this.dtDeleteCustomRule.Size = new System.Drawing.Size(169, 30);
            this.dtDeleteCustomRule.TabIndex = 3;
            this.dtDeleteCustomRule.Text = "Delete Selected Custom Rule";
            this.dtDeleteCustomRule.UseVisualStyleBackColor = true;
            this.dtDeleteCustomRule.Click += new System.EventHandler(this.dtDeleteCustomRule_Click);
            // 
            // dgvCustomRules
            // 
            this.dgvCustomRules.AllowUserToAddRows = false;
            this.dgvCustomRules.AllowUserToDeleteRows = false;
            this.dgvCustomRules.AllowUserToOrderColumns = true;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.Gray;
            this.dgvCustomRules.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvCustomRules.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                                                                                | System.Windows.Forms.AnchorStyles.Left)
                                                                               | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvCustomRules.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.Gray;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvCustomRules.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvCustomRules.Location = new System.Drawing.Point(3, 44);
            this.dgvCustomRules.Name = "dgvCustomRules";
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.Gray;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.White;
            this.dgvCustomRules.RowsDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvCustomRules.Size = new System.Drawing.Size(968, 493);
            this.dgvCustomRules.TabIndex = 9;
            // 
            // tcRules
            // 
            this.tcRules.Controls.Add(this.tabPage1);
            this.tcRules.Controls.Add(this.tpCurrentRules);
            this.tcRules.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcRules.Location = new System.Drawing.Point(0, 0);
            this.tcRules.Name = "tcRules";
            this.tcRules.SelectedIndex = 0;
            this.tcRules.Size = new System.Drawing.Size(1210, 677);
            this.tcRules.TabIndex = 10;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage1.Controls.Add(this.lbCurrentMethodDbId);
            this.tabPage1.Controls.Add(this.label7);
            this.tabPage1.Controls.Add(this.lbSelectedMethodVulnId);
            this.tabPage1.Controls.Add(this.label6);
            this.tabPage1.Controls.Add(this.lbSelectedActionObjectId);
            this.tabPage1.Controls.Add(this.splitContainer2);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.lbCurrentMethodSignature);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.btMarkMethodAsSink);
            this.tabPage1.Controls.Add(this.ascx_DropObject1);
            this.tabPage1.Controls.Add(this.dgvCustomRulesForMethodSignature);
            this.tabPage1.Controls.Add(this.btDeleteSelectedActionObject);
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Controls.Add(this.label32);
            this.tabPage1.Controls.Add(this.label31);
            this.tabPage1.Controls.Add(this.rbEditCustomRule_Sink);
            this.tabPage1.Controls.Add(this.label30);
            this.tabPage1.Controls.Add(this.rbEditCustomRule_Source);
            this.tabPage1.Controls.Add(this.rbEditCustomRule_ActionObject);
            this.tabPage1.Controls.Add(this.btEditCustomRule_markSinkAsTaintPropagator);
            this.tabPage1.Controls.Add(this.btDeleteSourceActionObject);
            this.tabPage1.ForeColor = System.Drawing.Color.Black;
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1202, 651);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "View Rule Details";
            // 
            // lbCurrentMethodDbId
            // 
            this.lbCurrentMethodDbId.AutoSize = true;
            this.lbCurrentMethodDbId.Location = new System.Drawing.Point(198, 33);
            this.lbCurrentMethodDbId.Name = "lbCurrentMethodDbId";
            this.lbCurrentMethodDbId.Size = new System.Drawing.Size(16, 13);
            this.lbCurrentMethodDbId.TabIndex = 67;
            this.lbCurrentMethodDbId.Text = "...";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(24, 33);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(76, 13);
            this.label7.TabIndex = 66;
            this.label7.Text = "Method\'s DbId";
            // 
            // lbSelectedMethodVulnId
            // 
            this.lbSelectedMethodVulnId.AutoSize = true;
            this.lbSelectedMethodVulnId.Location = new System.Drawing.Point(416, 33);
            this.lbSelectedMethodVulnId.Name = "lbSelectedMethodVulnId";
            this.lbSelectedMethodVulnId.Size = new System.Drawing.Size(16, 13);
            this.lbSelectedMethodVulnId.TabIndex = 65;
            this.lbSelectedMethodVulnId.Text = "...";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(280, 33);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(83, 13);
            this.label6.TabIndex = 64;
            this.label6.Text = "Method\'s VulnId";
            // 
            // lbSelectedActionObjectId
            // 
            this.lbSelectedActionObjectId.AutoSize = true;
            this.lbSelectedActionObjectId.Location = new System.Drawing.Point(198, 55);
            this.lbSelectedActionObjectId.Name = "lbSelectedActionObjectId";
            this.lbSelectedActionObjectId.Size = new System.Drawing.Size(16, 13);
            this.lbSelectedActionObjectId.TabIndex = 63;
            this.lbSelectedActionObjectId.Text = "...";
            // 
            // splitContainer2
            // 
            this.splitContainer2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                                                                                 | System.Windows.Forms.AnchorStyles.Left)
                                                                                | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer2.Location = new System.Drawing.Point(19, 108);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.splitContainer1);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.scActionObjectsAndSelectViews);
            this.splitContainer2.Size = new System.Drawing.Size(1177, 453);
            this.splitContainer2.SplitterDistance = 89;
            this.splitContainer2.TabIndex = 62;
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
            this.splitContainer1.Panel1.Controls.Add(this.dgvLddb_rec);
            this.splitContainer1.Panel1.Controls.Add(this.label4);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.dgvLddb_actionobjects);
            this.splitContainer1.Panel2.Controls.Add(this.label5);
            this.splitContainer1.Size = new System.Drawing.Size(1177, 89);
            this.splitContainer1.SplitterDistance = 400;
            this.splitContainer1.TabIndex = 61;
            // 
            // dgvLddb_rec
            // 
            this.dgvLddb_rec.AllowUserToAddRows = false;
            this.dgvLddb_rec.AllowUserToDeleteRows = false;
            this.dgvLddb_rec.AllowUserToOrderColumns = true;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.Black;
            this.dgvLddb_rec.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvLddb_rec.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                                                                             | System.Windows.Forms.AnchorStyles.Left)
                                                                            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvLddb_rec.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvLddb_rec.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.Gray;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvLddb_rec.DefaultCellStyle = dataGridViewCellStyle5;
            this.dgvLddb_rec.Location = new System.Drawing.Point(6, 16);
            this.dgvLddb_rec.Name = "dgvLddb_rec";
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle6.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.Color.White;
            this.dgvLddb_rec.RowsDefaultCellStyle = dataGridViewCellStyle6;
            this.dgvLddb_rec.Size = new System.Drawing.Size(387, 66);
            this.dgvLddb_rec.TabIndex = 63;
            this.dgvLddb_rec.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dgvLddb_rec_DataError);
            this.dgvLddb_rec.ColumnAdded += new System.Windows.Forms.DataGridViewColumnEventHandler(this.dgvLddb_rec_ColumnAdded);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(48, 13);
            this.label4.TabIndex = 59;
            this.label4.Text = "rec";
            // 
            // dgvLddb_actionobjects
            // 
            this.dgvLddb_actionobjects.AllowUserToAddRows = false;
            this.dgvLddb_actionobjects.AllowUserToDeleteRows = false;
            this.dgvLddb_actionobjects.AllowUserToOrderColumns = true;
            dataGridViewCellStyle7.BackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.Color.Gainsboro;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.Color.Black;
            this.dgvLddb_actionobjects.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle7;
            this.dgvLddb_actionobjects.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                                                                                       | System.Windows.Forms.AnchorStyles.Left)
                                                                                      | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvLddb_actionobjects.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvLddb_actionobjects.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.Color.Gray;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvLddb_actionobjects.DefaultCellStyle = dataGridViewCellStyle8;
            this.dgvLddb_actionobjects.Location = new System.Drawing.Point(7, 16);
            this.dgvLddb_actionobjects.Name = "dgvLddb_actionobjects";
            dataGridViewCellStyle9.BackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle9.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.Color.Gainsboro;
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.Color.Black;
            this.dgvLddb_actionobjects.RowsDefaultCellStyle = dataGridViewCellStyle9;
            this.dgvLddb_actionobjects.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvLddb_actionobjects.Size = new System.Drawing.Size(759, 66);
            this.dgvLddb_actionobjects.TabIndex = 64;
            this.dgvLddb_actionobjects.SelectionChanged += new System.EventHandler(this.dgvLddb_actionobjects_SelectionChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(96, 13);
            this.label5.TabIndex = 60;
            this.label5.Text = "actionobjects";
            // 
            // scActionObjectsAndSelectViews
            // 
            this.scActionObjectsAndSelectViews.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.scActionObjectsAndSelectViews.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scActionObjectsAndSelectViews.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.scActionObjectsAndSelectViews.Location = new System.Drawing.Point(0, 0);
            this.scActionObjectsAndSelectViews.Name = "scActionObjectsAndSelectViews";
            // 
            // scActionObjectsAndSelectViews.Panel1
            // 
            this.scActionObjectsAndSelectViews.Panel1.Controls.Add(this.gbLddbTables);
            // 
            // scActionObjectsAndSelectViews.Panel2
            // 
            this.scActionObjectsAndSelectViews.Panel2.Controls.Add(this.cbLddb_writes_through_info);
            this.scActionObjectsAndSelectViews.Panel2.Controls.Add(this.cbLddb_stored_writeable_alias_info);
            this.scActionObjectsAndSelectViews.Panel2.Controls.Add(this.cbLddb_ao_options);
            this.scActionObjectsAndSelectViews.Panel2.Controls.Add(this.cbLddb_source_info);
            this.scActionObjectsAndSelectViews.Panel2.Controls.Add(this.cdLddb_validation_descriptor);
            this.scActionObjectsAndSelectViews.Panel2.Controls.Add(this.cbLddb_sink_info);
            this.scActionObjectsAndSelectViews.Panel2.Controls.Add(this.cbLddb_taint_info);
            this.scActionObjectsAndSelectViews.Size = new System.Drawing.Size(1177, 360);
            this.scActionObjectsAndSelectViews.SplitterDistance = 1085;
            this.scActionObjectsAndSelectViews.TabIndex = 0;
            // 
            // gbLddbTables
            // 
            this.gbLddbTables.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbLddbTables.Location = new System.Drawing.Point(0, 0);
            this.gbLddbTables.Name = "gbLddbTables";
            this.gbLddbTables.Size = new System.Drawing.Size(1081, 356);
            this.gbLddbTables.TabIndex = 58;
            this.gbLddbTables.TabStop = false;
            this.gbLddbTables.Text = "Selected Lddb Tables";
            // 
            // cbLddb_writes_through_info
            // 
            this.cbLddb_writes_through_info.AutoSize = true;
            this.cbLddb_writes_through_info.Checked = true;
            this.cbLddb_writes_through_info.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbLddb_writes_through_info.Location = new System.Drawing.Point(13, 118);
            this.cbLddb_writes_through_info.Name = "cbLddb_writes_through_info";
            this.cbLddb_writes_through_info.Size = new System.Drawing.Size(144, 17);
            this.cbLddb_writes_through_info.TabIndex = 10;
            this.cbLddb_writes_through_info.Text = "writes_through_info";
            this.cbLddb_writes_through_info.UseVisualStyleBackColor = true;
            this.cbLddb_writes_through_info.CheckedChanged += new System.EventHandler(this.cbLddb_writes_through_info_CheckedChanged);
            // 
            // cbLddb_stored_writeable_alias_info
            // 
            this.cbLddb_stored_writeable_alias_info.AutoSize = true;
            this.cbLddb_stored_writeable_alias_info.Checked = true;
            this.cbLddb_stored_writeable_alias_info.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbLddb_stored_writeable_alias_info.Location = new System.Drawing.Point(13, 95);
            this.cbLddb_stored_writeable_alias_info.Name = "cbLddb_stored_writeable_alias_info";
            this.cbLddb_stored_writeable_alias_info.Size = new System.Drawing.Size(179, 17);
            this.cbLddb_stored_writeable_alias_info.TabIndex = 9;
            this.cbLddb_stored_writeable_alias_info.Text = "stored_writeable_alias_info";
            this.cbLddb_stored_writeable_alias_info.UseVisualStyleBackColor = true;
            this.cbLddb_stored_writeable_alias_info.CheckedChanged += new System.EventHandler(this.cbLddb_stored_writeable_alias_info_CheckedChanged);
            // 
            // cbLddb_ao_options
            // 
            this.cbLddb_ao_options.AutoSize = true;
            this.cbLddb_ao_options.Checked = true;
            this.cbLddb_ao_options.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbLddb_ao_options.Location = new System.Drawing.Point(13, 72);
            this.cbLddb_ao_options.Name = "cbLddb_ao_options";
            this.cbLddb_ao_options.Size = new System.Drawing.Size(104, 17);
            this.cbLddb_ao_options.TabIndex = 3;
            this.cbLddb_ao_options.Text = "ao_options";
            this.cbLddb_ao_options.UseVisualStyleBackColor = true;
            this.cbLddb_ao_options.CheckedChanged += new System.EventHandler(this.cbLddb_ao_options_CheckedChanged);
            // 
            // cbLddb_source_info
            // 
            this.cbLddb_source_info.AutoSize = true;
            this.cbLddb_source_info.Checked = true;
            this.cbLddb_source_info.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbLddb_source_info.Location = new System.Drawing.Point(13, 26);
            this.cbLddb_source_info.Name = "cbLddb_source_info";
            this.cbLddb_source_info.Size = new System.Drawing.Size(107, 17);
            this.cbLddb_source_info.TabIndex = 5;
            this.cbLddb_source_info.Text = "source_info";
            this.cbLddb_source_info.UseVisualStyleBackColor = true;
            this.cbLddb_source_info.CheckedChanged += new System.EventHandler(this.cbLddb_source_info_CheckedChanged);
            // 
            // cdLddb_validation_descriptor
            // 
            this.cdLddb_validation_descriptor.AutoSize = true;
            this.cdLddb_validation_descriptor.Checked = true;
            this.cdLddb_validation_descriptor.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cdLddb_validation_descriptor.Location = new System.Drawing.Point(13, 141);
            this.cdLddb_validation_descriptor.Name = "cdLddb_validation_descriptor";
            this.cdLddb_validation_descriptor.Size = new System.Drawing.Size(149, 17);
            this.cdLddb_validation_descriptor.TabIndex = 7;
            this.cdLddb_validation_descriptor.Text = "validation_descriptor";
            this.cdLddb_validation_descriptor.UseVisualStyleBackColor = true;
            this.cdLddb_validation_descriptor.CheckedChanged += new System.EventHandler(this.cdLddb_validation_descriptor_CheckedChanged);
            // 
            // cbLddb_sink_info
            // 
            this.cbLddb_sink_info.AutoSize = true;
            this.cbLddb_sink_info.Checked = true;
            this.cbLddb_sink_info.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbLddb_sink_info.Location = new System.Drawing.Point(13, 49);
            this.cbLddb_sink_info.Name = "cbLddb_sink_info";
            this.cbLddb_sink_info.Size = new System.Drawing.Size(94, 17);
            this.cbLddb_sink_info.TabIndex = 4;
            this.cbLddb_sink_info.Text = "sink_info";
            this.cbLddb_sink_info.UseVisualStyleBackColor = true;
            this.cbLddb_sink_info.CheckedChanged += new System.EventHandler(this.cbLddb_sink_info_CheckedChanged);
            // 
            // cbLddb_taint_info
            // 
            this.cbLddb_taint_info.AutoSize = true;
            this.cbLddb_taint_info.Checked = true;
            this.cbLddb_taint_info.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbLddb_taint_info.Location = new System.Drawing.Point(13, 3);
            this.cbLddb_taint_info.Name = "cbLddb_taint_info";
            this.cbLddb_taint_info.Size = new System.Drawing.Size(95, 17);
            this.cbLddb_taint_info.TabIndex = 6;
            this.cbLddb_taint_info.Text = "taint_info";
            this.cbLddb_taint_info.UseVisualStyleBackColor = true;
            this.cbLddb_taint_info.CheckedChanged += new System.EventHandler(this.cbLddb_taint_info_CheckedChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(22, 55);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(122, 13);
            this.label3.TabIndex = 56;
            this.label3.Text = "Selected ActionObjectId";
            // 
            // lbCurrentMethodSignature
            // 
            this.lbCurrentMethodSignature.AutoSize = true;
            this.lbCurrentMethodSignature.Location = new System.Drawing.Point(198, 13);
            this.lbCurrentMethodSignature.Name = "lbCurrentMethodSignature";
            this.lbCurrentMethodSignature.Size = new System.Drawing.Size(16, 13);
            this.lbCurrentMethodSignature.TabIndex = 53;
            this.lbCurrentMethodSignature.Text = "...";
            this.lbCurrentMethodSignature.TextChanged += new System.EventHandler(this.lbCurrentMethodSignature_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(26, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(166, 13);
            this.label1.TabIndex = 52;
            this.label1.Text = "Showing custom rules for method:";
            // 
            // btMarkMethodAsSink
            // 
            this.btMarkMethodAsSink.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btMarkMethodAsSink.Location = new System.Drawing.Point(587, 567);
            this.btMarkMethodAsSink.Name = "btMarkMethodAsSink";
            this.btMarkMethodAsSink.Size = new System.Drawing.Size(118, 40);
            this.btMarkMethodAsSink.TabIndex = 51;
            this.btMarkMethodAsSink.Text = "Mark Method as Sink";
            this.btMarkMethodAsSink.UseVisualStyleBackColor = true;
            this.btMarkMethodAsSink.Click += new System.EventHandler(this.btMarkMethodAsSink_Click);
            // 
            // ascx_DropObject1
            // 
            this.ascx_DropObject1.AllowDrop = true;
            this.ascx_DropObject1.BackColor = System.Drawing.Color.Maroon;
            this.ascx_DropObject1.ForeColor = System.Drawing.Color.White;
            this.ascx_DropObject1.Location = new System.Drawing.Point(558, 35);
            this.ascx_DropObject1.Name = "ascx_DropObject1";
            this.ascx_DropObject1.Size = new System.Drawing.Size(76, 50);
            this.ascx_DropObject1.TabIndex = 50;
            this.ascx_DropObject1.Load += new System.EventHandler(this.ascx_DropObject1_Load);
            this.ascx_DropObject1.eDnDAction_ObjectDataReceived_Event += new Callbacks.dMethod_Object(this.ascx_DropObject1_eDnDAction_ObjectDataReceived_Event);
            // 
            // dgvCustomRulesForMethodSignature
            // 
            this.dgvCustomRulesForMethodSignature.AllowUserToAddRows = false;
            this.dgvCustomRulesForMethodSignature.AllowUserToDeleteRows = false;
            this.dgvCustomRulesForMethodSignature.AllowUserToOrderColumns = true;
            dataGridViewCellStyle10.BackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle10.SelectionBackColor = System.Drawing.Color.Gray;
            this.dgvCustomRulesForMethodSignature.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle10;
            this.dgvCustomRulesForMethodSignature.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvCustomRulesForMethodSignature.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle11.BackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle11.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle11.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle11.SelectionBackColor = System.Drawing.Color.Gray;
            dataGridViewCellStyle11.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle11.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvCustomRulesForMethodSignature.DefaultCellStyle = dataGridViewCellStyle11;
            this.dgvCustomRulesForMethodSignature.Location = new System.Drawing.Point(874, 13);
            this.dgvCustomRulesForMethodSignature.Name = "dgvCustomRulesForMethodSignature";
            dataGridViewCellStyle12.BackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle12.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle12.SelectionBackColor = System.Drawing.Color.Gray;
            dataGridViewCellStyle12.SelectionForeColor = System.Drawing.Color.White;
            this.dgvCustomRulesForMethodSignature.RowsDefaultCellStyle = dataGridViewCellStyle12;
            this.dgvCustomRulesForMethodSignature.Size = new System.Drawing.Size(322, 68);
            this.dgvCustomRulesForMethodSignature.TabIndex = 49;
            // 
            // btDeleteSelectedActionObject
            // 
            this.btDeleteSelectedActionObject.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btDeleteSelectedActionObject.Location = new System.Drawing.Point(997, 576);
            this.btDeleteSelectedActionObject.Name = "btDeleteSelectedActionObject";
            this.btDeleteSelectedActionObject.Size = new System.Drawing.Size(185, 25);
            this.btDeleteSelectedActionObject.TabIndex = 47;
            this.btDeleteSelectedActionObject.Text = "Delete Selected ActionObject";
            this.btDeleteSelectedActionObject.UseVisualStyleBackColor = true;
            this.btDeleteSelectedActionObject.Click += new System.EventHandler(this.btDeleteSelectedActionObject_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox1.Controls.Add(this.cbEditCustomRules_Type);
            this.groupBox1.Controls.Add(this.cbEditCustomRules_Severity);
            this.groupBox1.Controls.Add(this.cbEditCustomRules_Signature);
            this.groupBox1.Controls.Add(this.cbEditCustomRules_vuln_type);
            this.groupBox1.Controls.Add(this.cbEditCustomRules_Trace);
            this.groupBox1.Controls.Add(this.label40);
            this.groupBox1.Controls.Add(this.label41);
            this.groupBox1.Controls.Add(this.label37);
            this.groupBox1.Controls.Add(this.label36);
            this.groupBox1.Controls.Add(this.cbEditCustomRules_DbId);
            this.groupBox1.Controls.Add(this.label35);
            this.groupBox1.Controls.Add(this.label34);
            this.groupBox1.Location = new System.Drawing.Point(21, 561);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(560, 84);
            this.groupBox1.TabIndex = 46;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Selected Action Object properties";
            // 
            // cbEditCustomRules_Type
            // 
            this.cbEditCustomRules_Type.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cbEditCustomRules_Type.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbEditCustomRules_Type.FormattingEnabled = true;
            this.cbEditCustomRules_Type.Location = new System.Drawing.Point(360, 50);
            this.cbEditCustomRules_Type.Name = "cbEditCustomRules_Type";
            this.cbEditCustomRules_Type.Size = new System.Drawing.Size(61, 21);
            this.cbEditCustomRules_Type.TabIndex = 39;
            // 
            // cbEditCustomRules_Severity
            // 
            this.cbEditCustomRules_Severity.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cbEditCustomRules_Severity.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbEditCustomRules_Severity.FormattingEnabled = true;
            this.cbEditCustomRules_Severity.Location = new System.Drawing.Point(360, 19);
            this.cbEditCustomRules_Severity.Name = "cbEditCustomRules_Severity";
            this.cbEditCustomRules_Severity.Size = new System.Drawing.Size(61, 21);
            this.cbEditCustomRules_Severity.TabIndex = 38;
            // 
            // cbEditCustomRules_Signature
            // 
            this.cbEditCustomRules_Signature.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                                                                                            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbEditCustomRules_Signature.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbEditCustomRules_Signature.FormattingEnabled = true;
            this.cbEditCustomRules_Signature.Location = new System.Drawing.Point(75, 50);
            this.cbEditCustomRules_Signature.Name = "cbEditCustomRules_Signature";
            this.cbEditCustomRules_Signature.Size = new System.Drawing.Size(215, 21);
            this.cbEditCustomRules_Signature.TabIndex = 37;
            // 
            // cbEditCustomRules_vuln_type
            // 
            this.cbEditCustomRules_vuln_type.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                                                                                            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbEditCustomRules_vuln_type.FormattingEnabled = true;
            this.cbEditCustomRules_vuln_type.Location = new System.Drawing.Point(75, 19);
            this.cbEditCustomRules_vuln_type.Name = "cbEditCustomRules_vuln_type";
            this.cbEditCustomRules_vuln_type.Size = new System.Drawing.Size(215, 21);
            this.cbEditCustomRules_vuln_type.TabIndex = 36;
            this.cbEditCustomRules_vuln_type.SelectedIndexChanged += new System.EventHandler(this.cbEditCustomRules_vuln_type_SelectedIndexChanged);
            // 
            // cbEditCustomRules_Trace
            // 
            this.cbEditCustomRules_Trace.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cbEditCustomRules_Trace.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbEditCustomRules_Trace.FormattingEnabled = true;
            this.cbEditCustomRules_Trace.Location = new System.Drawing.Point(475, 19);
            this.cbEditCustomRules_Trace.Name = "cbEditCustomRules_Trace";
            this.cbEditCustomRules_Trace.Size = new System.Drawing.Size(44, 21);
            this.cbEditCustomRules_Trace.TabIndex = 35;
            // 
            // label40
            // 
            this.label40.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label40.AutoSize = true;
            this.label40.Location = new System.Drawing.Point(425, 53);
            this.label40.Name = "label40";
            this.label40.Size = new System.Drawing.Size(36, 13);
            this.label40.TabIndex = 9;
            this.label40.Text = "Db Id:";
            // 
            // label41
            // 
            this.label41.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label41.AutoSize = true;
            this.label41.Location = new System.Drawing.Point(425, 24);
            this.label41.Name = "label41";
            this.label41.Size = new System.Drawing.Size(35, 13);
            this.label41.TabIndex = 8;
            this.label41.Text = "Trace";
            // 
            // label37
            // 
            this.label37.AutoSize = true;
            this.label37.Location = new System.Drawing.Point(11, 53);
            this.label37.Name = "label37";
            this.label37.Size = new System.Drawing.Size(52, 13);
            this.label37.TabIndex = 3;
            this.label37.Text = "Signature";
            // 
            // label36
            // 
            this.label36.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label36.AutoSize = true;
            this.label36.Location = new System.Drawing.Point(309, 53);
            this.label36.Name = "label36";
            this.label36.Size = new System.Drawing.Size(34, 13);
            this.label36.TabIndex = 2;
            this.label36.Text = "Type:";
            // 
            // cbEditCustomRules_DbId
            // 
            this.cbEditCustomRules_DbId.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cbEditCustomRules_DbId.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbEditCustomRules_DbId.FormattingEnabled = true;
            this.cbEditCustomRules_DbId.Location = new System.Drawing.Point(475, 50);
            this.cbEditCustomRules_DbId.Name = "cbEditCustomRules_DbId";
            this.cbEditCustomRules_DbId.Size = new System.Drawing.Size(44, 21);
            this.cbEditCustomRules_DbId.TabIndex = 34;
            // 
            // label35
            // 
            this.label35.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label35.AutoSize = true;
            this.label35.Location = new System.Drawing.Point(310, 24);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(45, 13);
            this.label35.TabIndex = 1;
            this.label35.Text = "Severity";
            // 
            // label34
            // 
            this.label34.AutoSize = true;
            this.label34.Location = new System.Drawing.Point(12, 24);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(58, 13);
            this.label34.TabIndex = 0;
            this.label34.Text = "Vuln Type:";
            // 
            // label32
            // 
            this.label32.AutoSize = true;
            this.label32.Location = new System.Drawing.Point(642, 33);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(68, 13);
            this.label32.TabIndex = 38;
            this.label32.Text = "ActionObject";
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.Location = new System.Drawing.Point(641, 68);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(86, 13);
            this.label31.TabIndex = 10;
            this.label31.Text = "SmartTrace Sink";
            // 
            // rbEditCustomRule_Sink
            // 
            this.rbEditCustomRule_Sink.AutoSize = true;
            this.rbEditCustomRule_Sink.Location = new System.Drawing.Point(753, 68);
            this.rbEditCustomRule_Sink.Name = "rbEditCustomRule_Sink";
            this.rbEditCustomRule_Sink.Size = new System.Drawing.Size(34, 17);
            this.rbEditCustomRule_Sink.TabIndex = 43;
            this.rbEditCustomRule_Sink.Text = "...";
            this.rbEditCustomRule_Sink.UseVisualStyleBackColor = true;
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.Location = new System.Drawing.Point(641, 51);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(102, 13);
            this.label30.TabIndex = 9;
            this.label30.Text = "SmartTrace Source:";
            // 
            // rbEditCustomRule_Source
            // 
            this.rbEditCustomRule_Source.AutoSize = true;
            this.rbEditCustomRule_Source.Location = new System.Drawing.Point(753, 51);
            this.rbEditCustomRule_Source.Name = "rbEditCustomRule_Source";
            this.rbEditCustomRule_Source.Size = new System.Drawing.Size(34, 17);
            this.rbEditCustomRule_Source.TabIndex = 42;
            this.rbEditCustomRule_Source.Text = "...";
            this.rbEditCustomRule_Source.UseVisualStyleBackColor = true;
            // 
            // rbEditCustomRule_ActionObject
            // 
            this.rbEditCustomRule_ActionObject.AutoSize = true;
            this.rbEditCustomRule_ActionObject.Checked = true;
            this.rbEditCustomRule_ActionObject.Location = new System.Drawing.Point(753, 33);
            this.rbEditCustomRule_ActionObject.Name = "rbEditCustomRule_ActionObject";
            this.rbEditCustomRule_ActionObject.Size = new System.Drawing.Size(34, 17);
            this.rbEditCustomRule_ActionObject.TabIndex = 41;
            this.rbEditCustomRule_ActionObject.TabStop = true;
            this.rbEditCustomRule_ActionObject.Text = "...";
            this.rbEditCustomRule_ActionObject.UseVisualStyleBackColor = true;
            // 
            // btEditCustomRule_markSinkAsTaintPropagator
            // 
            this.btEditCustomRule_markSinkAsTaintPropagator.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btEditCustomRule_markSinkAsTaintPropagator.Location = new System.Drawing.Point(880, 597);
            this.btEditCustomRule_markSinkAsTaintPropagator.Name = "btEditCustomRule_markSinkAsTaintPropagator";
            this.btEditCustomRule_markSinkAsTaintPropagator.Size = new System.Drawing.Size(96, 38);
            this.btEditCustomRule_markSinkAsTaintPropagator.TabIndex = 12;
            this.btEditCustomRule_markSinkAsTaintPropagator.Text = "Mark SINK As Taint Propagator";
            this.btEditCustomRule_markSinkAsTaintPropagator.UseVisualStyleBackColor = true;
            this.btEditCustomRule_markSinkAsTaintPropagator.Visible = false;
            this.btEditCustomRule_markSinkAsTaintPropagator.Click += new System.EventHandler(this.btEditCustomRule_markSinkAsTaintPropagator_Click);
            // 
            // btDeleteSourceActionObject
            // 
            this.btDeleteSourceActionObject.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btDeleteSourceActionObject.Location = new System.Drawing.Point(996, 612);
            this.btDeleteSourceActionObject.Name = "btDeleteSourceActionObject";
            this.btDeleteSourceActionObject.Size = new System.Drawing.Size(201, 23);
            this.btDeleteSourceActionObject.TabIndex = 40;
            this.btDeleteSourceActionObject.Text = "Delete Source Action Object ";
            this.btDeleteSourceActionObject.UseVisualStyleBackColor = true;
            this.btDeleteSourceActionObject.Visible = false;
            this.btDeleteSourceActionObject.Click += new System.EventHandler(this.btDeleteSourceActionObject_Click);
            // 
            // tpCurrentRules
            // 
            this.tpCurrentRules.BackColor = System.Drawing.SystemColors.Control;
            this.tpCurrentRules.Controls.Add(this.dgvCustomRules);
            this.tpCurrentRules.Controls.Add(this.btRefresh);
            this.tpCurrentRules.Controls.Add(this.dtDeleteCustomRule);
            this.tpCurrentRules.ForeColor = System.Drawing.Color.Black;
            this.tpCurrentRules.Location = new System.Drawing.Point(4, 22);
            this.tpCurrentRules.Name = "tpCurrentRules";
            this.tpCurrentRules.Padding = new System.Windows.Forms.Padding(3);
            this.tpCurrentRules.Size = new System.Drawing.Size(1202, 651);
            this.tpCurrentRules.TabIndex = 1;
            this.tpCurrentRules.Text = "Current Custom Rules on DB";
            this.tpCurrentRules.Click += new System.EventHandler(this.tpCurrentRules_Click);
            // 
            // ascx_CustomRules
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.tcRules);
            this.ForeColor = System.Drawing.Color.White;
            this.Name = "ascx_CustomRules";
            this.Size = new System.Drawing.Size(1210, 677);
            this.Load += new System.EventHandler(this.ascx_CustomRules_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCustomRules)).EndInit();
            this.tcRules.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvLddb_rec)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLddb_actionobjects)).EndInit();
            this.scActionObjectsAndSelectViews.Panel1.ResumeLayout(false);
            this.scActionObjectsAndSelectViews.Panel2.ResumeLayout(false);
            this.scActionObjectsAndSelectViews.Panel2.PerformLayout();
            this.scActionObjectsAndSelectViews.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCustomRulesForMethodSignature)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tpCurrentRules.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btRefresh;
        private System.Windows.Forms.Button dtDeleteCustomRule;
        private System.Windows.Forms.DataGridView dgvCustomRules;
        private System.Windows.Forms.TabControl tcRules;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tpCurrentRules;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cbEditCustomRules_Type;
        private System.Windows.Forms.ComboBox cbEditCustomRules_Severity;
        private System.Windows.Forms.ComboBox cbEditCustomRules_Signature;
        private System.Windows.Forms.ComboBox cbEditCustomRules_vuln_type;
        private System.Windows.Forms.ComboBox cbEditCustomRules_Trace;
        private System.Windows.Forms.Label label40;
        private System.Windows.Forms.Label label41;
        private System.Windows.Forms.Label label37;
        private System.Windows.Forms.Label label36;
        private System.Windows.Forms.ComboBox cbEditCustomRules_DbId;
        private System.Windows.Forms.Label label35;
        private System.Windows.Forms.Label label34;
        private System.Windows.Forms.Label label32;
        private System.Windows.Forms.Label label31;
        private System.Windows.Forms.RadioButton rbEditCustomRule_Sink;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.RadioButton rbEditCustomRule_Source;
        private System.Windows.Forms.RadioButton rbEditCustomRule_ActionObject;
        private System.Windows.Forms.Button btEditCustomRule_markSinkAsTaintPropagator;
        private System.Windows.Forms.Button btDeleteSourceActionObject;
        private System.Windows.Forms.Button btDeleteSelectedActionObject;
        private System.Windows.Forms.DataGridView dgvCustomRulesForMethodSignature;
        private ascx_DropObject ascx_DropObject1;
        private System.Windows.Forms.Button btMarkMethodAsSink;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lbCurrentMethodSignature;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox gbLddbTables;
        private System.Windows.Forms.CheckBox cdLddb_validation_descriptor;
        private System.Windows.Forms.CheckBox cbLddb_taint_info;
        private System.Windows.Forms.CheckBox cbLddb_source_info;
        private System.Windows.Forms.CheckBox cbLddb_sink_info;
        private System.Windows.Forms.CheckBox cbLddb_ao_options;
        private System.Windows.Forms.CheckBox cbLddb_writes_through_info;
        private System.Windows.Forms.CheckBox cbLddb_stored_writeable_alias_info;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.SplitContainer scActionObjectsAndSelectViews;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lbSelectedActionObjectId;
        private System.Windows.Forms.DataGridView dgvLddb_rec;
        private System.Windows.Forms.DataGridView dgvLddb_actionobjects;
        private System.Windows.Forms.Label lbSelectedMethodVulnId;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lbCurrentMethodDbId;
        private System.Windows.Forms.Label label7;
    }
}
