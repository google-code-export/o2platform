namespace O2.Rules.OunceLabs.Ascx
{
    partial class ascx_RuleEditor
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ascx_RuleEditor));
            this.cbRuleType = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.laNumberOfRulesLoaded = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.btSaveRuleChanges = new System.Windows.Forms.ToolStripButton();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.btSaveChangesToAllRules = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btSetRuleTypeAsNotASink = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.laUnsavedChanges = new System.Windows.Forms.ToolStripLabel();
            this.laDataSaved = new System.Windows.Forms.ToolStripLabel();
            this.label2 = new System.Windows.Forms.Label();
            this.tbSignature = new System.Windows.Forms.TextBox();
            this.cbSeverity = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.tbVulnerabilityType = new System.Windows.Forms.TextBox();
            this.tbParam = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tbReturn = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.tbFromParam = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.tbToParam = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.tbComment = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.tbDbId = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // cbRuleType
            // 
            this.cbRuleType.DropDownHeight = 300;
            this.cbRuleType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbRuleType.FormattingEnabled = true;
            this.cbRuleType.IntegralHeight = false;
            this.cbRuleType.Location = new System.Drawing.Point(68, 83);
            this.cbRuleType.Name = "cbRuleType";
            this.cbRuleType.Size = new System.Drawing.Size(121, 21);
            this.cbRuleType.TabIndex = 2;
            this.cbRuleType.SelectedIndexChanged += new System.EventHandler(this.controlUnsavedChanges);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 86);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Rule Type:";
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.laNumberOfRulesLoaded,
            this.toolStripSeparator1,
            this.toolStripLabel1,
            this.btSaveRuleChanges,
            this.toolStripLabel2,
            this.btSaveChangesToAllRules,
            this.toolStripSeparator2,
            this.btSetRuleTypeAsNotASink,
            this.toolStripSeparator4,
            this.laUnsavedChanges,
            this.laDataSaved});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(628, 25);
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // laNumberOfRulesLoaded
            // 
            this.laNumberOfRulesLoaded.Name = "laNumberOfRulesLoaded";
            this.laNumberOfRulesLoaded.Size = new System.Drawing.Size(74, 22);
            this.laNumberOfRulesLoaded.Text = "0 rules loaded";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(55, 22);
            this.toolStripLabel1.Text = "save rule:";
            // 
            // btSaveRuleChanges
            // 
            this.btSaveRuleChanges.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btSaveRuleChanges.Image = ((System.Drawing.Image)(resources.GetObject("btSaveRuleChanges.Image")));
            this.btSaveRuleChanges.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btSaveRuleChanges.Name = "btSaveRuleChanges";
            this.btSaveRuleChanges.Size = new System.Drawing.Size(23, 22);
            this.btSaveRuleChanges.Text = "Save Rule Changes";
            this.btSaveRuleChanges.Click += new System.EventHandler(this.btSaveRuleChanges_Click);
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(60, 22);
            this.toolStripLabel2.Text = "save rules:";
            // 
            // btSaveChangesToAllRules
            // 
            this.btSaveChangesToAllRules.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btSaveChangesToAllRules.Image = ((System.Drawing.Image)(resources.GetObject("btSaveChangesToAllRules.Image")));
            this.btSaveChangesToAllRules.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btSaveChangesToAllRules.Name = "btSaveChangesToAllRules";
            this.btSaveChangesToAllRules.Size = new System.Drawing.Size(23, 22);
            this.btSaveChangesToAllRules.Text = "Save changes to all rules";
            this.btSaveChangesToAllRules.Click += new System.EventHandler(this.btSaveChangesToAllRules_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // btSetRuleTypeAsNotASink
            // 
            this.btSetRuleTypeAsNotASink.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btSetRuleTypeAsNotASink.Image = ((System.Drawing.Image)(resources.GetObject("btSetRuleTypeAsNotASink.Image")));
            this.btSetRuleTypeAsNotASink.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btSetRuleTypeAsNotASink.Name = "btSetRuleTypeAsNotASink";
            this.btSetRuleTypeAsNotASink.Size = new System.Drawing.Size(23, 22);
            this.btSetRuleTypeAsNotASink.Text = "Set Rule as NOT a Sink";
            this.btSetRuleTypeAsNotASink.Click += new System.EventHandler(this.btSetRuleTypeAsNotASink_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
            // 
            // laUnsavedChanges
            // 
            this.laUnsavedChanges.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.laUnsavedChanges.ForeColor = System.Drawing.Color.Red;
            this.laUnsavedChanges.Name = "laUnsavedChanges";
            this.laUnsavedChanges.Size = new System.Drawing.Size(107, 22);
            this.laUnsavedChanges.Text = "Unsaved Changes";
            this.laUnsavedChanges.Visible = false;
            // 
            // laDataSaved
            // 
            this.laDataSaved.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.laDataSaved.ForeColor = System.Drawing.Color.Green;
            this.laDataSaved.Name = "laDataSaved";
            this.laDataSaved.Size = new System.Drawing.Size(72, 22);
            this.laDataSaved.Text = "Data Saved";
            this.laDataSaved.Visible = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(4, 35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Signature:";
            // 
            // tbSignature
            // 
            this.tbSignature.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbSignature.Location = new System.Drawing.Point(68, 32);
            this.tbSignature.Name = "tbSignature";
            this.tbSignature.Size = new System.Drawing.Size(557, 20);
            this.tbSignature.TabIndex = 0;
            this.tbSignature.TextChanged += new System.EventHandler(this.controlUnsavedChanges);
            // 
            // cbSeverity
            // 
            this.cbSeverity.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSeverity.FormattingEnabled = true;
            this.cbSeverity.Location = new System.Drawing.Point(243, 83);
            this.cbSeverity.Name = "cbSeverity";
            this.cbSeverity.Size = new System.Drawing.Size(88, 21);
            this.cbSeverity.TabIndex = 3;
            this.cbSeverity.SelectedIndexChanged += new System.EventHandler(this.controlUnsavedChanges);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(192, 86);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(45, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Severity";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(4, 60);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(58, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Vuln Type:";
            // 
            // tbVulnerabilityType
            // 
            this.tbVulnerabilityType.Location = new System.Drawing.Point(68, 57);
            this.tbVulnerabilityType.Name = "tbVulnerabilityType";
            this.tbVulnerabilityType.Size = new System.Drawing.Size(263, 20);
            this.tbVulnerabilityType.TabIndex = 1;
            this.tbVulnerabilityType.TextChanged += new System.EventHandler(this.controlUnsavedChanges);
            // 
            // tbParam
            // 
            this.tbParam.Location = new System.Drawing.Point(451, 80);
            this.tbParam.Name = "tbParam";
            this.tbParam.Size = new System.Drawing.Size(32, 20);
            this.tbParam.TabIndex = 10;
            this.tbParam.TextChanged += new System.EventHandler(this.controlUnsavedChanges);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(409, 83);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(40, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "Param:";
            // 
            // tbReturn
            // 
            this.tbReturn.Location = new System.Drawing.Point(451, 58);
            this.tbReturn.Name = "tbReturn";
            this.tbReturn.Size = new System.Drawing.Size(32, 20);
            this.tbReturn.TabIndex = 12;
            this.tbReturn.TextChanged += new System.EventHandler(this.controlUnsavedChanges);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(407, 60);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(42, 13);
            this.label6.TabIndex = 11;
            this.label6.Text = "Return:";
            // 
            // tbFromParam
            // 
            this.tbFromParam.Location = new System.Drawing.Point(547, 58);
            this.tbFromParam.Name = "tbFromParam";
            this.tbFromParam.Size = new System.Drawing.Size(27, 20);
            this.tbFromParam.TabIndex = 14;
            this.tbFromParam.TextChanged += new System.EventHandler(this.controlUnsavedChanges);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(483, 61);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(63, 13);
            this.label7.TabIndex = 13;
            this.label7.Text = "From Param";
            // 
            // tbToParam
            // 
            this.tbToParam.Location = new System.Drawing.Point(547, 84);
            this.tbToParam.Name = "tbToParam";
            this.tbToParam.Size = new System.Drawing.Size(27, 20);
            this.tbToParam.TabIndex = 16;
            this.tbToParam.TextChanged += new System.EventHandler(this.controlUnsavedChanges);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(493, 87);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(53, 13);
            this.label8.TabIndex = 15;
            this.label8.Text = "To Param";
            // 
            // tbComment
            // 
            this.tbComment.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbComment.Location = new System.Drawing.Point(68, 110);
            this.tbComment.Multiline = true;
            this.tbComment.Name = "tbComment";
            this.tbComment.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tbComment.Size = new System.Drawing.Size(557, 53);
            this.tbComment.TabIndex = 18;
            this.tbComment.TextChanged += new System.EventHandler(this.controlUnsavedChanges);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(8, 113);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(54, 13);
            this.label9.TabIndex = 17;
            this.label9.Text = "Comment:";
            // 
            // tbDbId
            // 
            this.tbDbId.Location = new System.Drawing.Point(374, 58);
            this.tbDbId.Name = "tbDbId";
            this.tbDbId.Size = new System.Drawing.Size(32, 20);
            this.tbDbId.TabIndex = 20;
            this.tbDbId.TextChanged += new System.EventHandler(this.controlUnsavedChanges);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(337, 61);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(38, 13);
            this.label10.TabIndex = 19;
            this.label10.Text = "Db ID:";
            this.label10.Click += new System.EventHandler(this.label10_Click);
            // 
            // ascx_RuleEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tbDbId);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.tbComment);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.tbToParam);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.tbFromParam);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.tbReturn);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.tbParam);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.tbVulnerabilityType);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cbSeverity);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tbSignature);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.cbRuleType);
            this.Controls.Add(this.label1);
            this.Name = "ascx_RuleEditor";
            this.Size = new System.Drawing.Size(628, 166);
            this.Load += new System.EventHandler(this.ascx_RuleEditor_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbRuleType;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btSaveRuleChanges;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbSignature;
        private System.Windows.Forms.ComboBox cbSeverity;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbVulnerabilityType;
        private System.Windows.Forms.TextBox tbParam;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tbReturn;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox tbFromParam;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox tbToParam;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox tbComment;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox tbDbId;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ToolStripLabel laNumberOfRulesLoaded;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private System.Windows.Forms.ToolStripButton btSaveChangesToAllRules;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripLabel laDataSaved;
        private System.Windows.Forms.ToolStripLabel laUnsavedChanges;
        private System.Windows.Forms.ToolStripButton btSetRuleTypeAsNotASink;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
    }
}