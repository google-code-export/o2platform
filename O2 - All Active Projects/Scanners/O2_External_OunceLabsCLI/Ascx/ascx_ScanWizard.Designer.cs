namespace O2.Scanner.OunceLabsCLI.Ascx
{
    partial class ascx_ScanWizard
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lbScanTargets = new System.Windows.Forms.ListBox();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.brAddCallbacksForAllScanTargets = new System.Windows.Forms.Button();
            this.brAddCallbacksForSelectedScanTarget = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btScanAll = new System.Windows.Forms.Button();
            this.btScanSelected = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btCreateCirForAllScanTargets = new System.Windows.Forms.Button();
            this.btCreateCirForSelectedScanTarget = new System.Windows.Forms.Button();
            this.directoryWithResults = new O2.Views.ASCX.CoreControls.ascx_Directory();
            this.tbTargetFolder = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
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
            this.splitContainer1.Panel1.Controls.Add(this.groupBox1);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            this.splitContainer1.Panel1.Controls.Add(this.lbScanTargets);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(820, 444);
            this.splitContainer1.SplitterDistance = 282;
            this.splitContainer1.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.checkBox1);
            this.groupBox1.Location = new System.Drawing.Point(2, 269);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(276, 170);
            this.groupBox1.TabIndex = 31;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "On-Drop actions (config)";
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Checked = true;
            this.checkBox1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox1.Location = new System.Drawing.Point(7, 20);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(232, 17);
            this.checkBox1.TabIndex = 0;
            this.checkBox1.Text = "On *.paf Drop create 1 scan target per *.ppf";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(0, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 13);
            this.label1.TabIndex = 29;
            this.label1.Text = "Scan Targets";
            // 
            // lbScanTargets
            // 
            this.lbScanTargets.AllowDrop = true;
            this.lbScanTargets.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lbScanTargets.FormattingEnabled = true;
            this.lbScanTargets.Location = new System.Drawing.Point(3, 24);
            this.lbScanTargets.Name = "lbScanTargets";
            this.lbScanTargets.Size = new System.Drawing.Size(272, 290);
            this.lbScanTargets.TabIndex = 30;
            // 
            // splitContainer2
            // 
            this.splitContainer2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.groupBox4);
            this.splitContainer2.Panel1.Controls.Add(this.groupBox3);
            this.splitContainer2.Panel1.Controls.Add(this.groupBox2);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.directoryWithResults);
            this.splitContainer2.Panel2.Controls.Add(this.tbTargetFolder);
            this.splitContainer2.Panel2.Controls.Add(this.label3);
            this.splitContainer2.Panel2.Controls.Add(this.label2);
            this.splitContainer2.Size = new System.Drawing.Size(534, 444);
            this.splitContainer2.SplitterDistance = 215;
            this.splitContainer2.TabIndex = 0;
            // 
            // groupBox4
            // 
            this.groupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox4.Controls.Add(this.brAddCallbacksForAllScanTargets);
            this.groupBox4.Controls.Add(this.brAddCallbacksForSelectedScanTarget);
            this.groupBox4.Location = new System.Drawing.Point(4, 156);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(204, 105);
            this.groupBox4.TabIndex = 2;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Add Callbacks";
            // 
            // brAddCallbacksForAllScanTargets
            // 
            this.brAddCallbacksForAllScanTargets.Location = new System.Drawing.Point(6, 60);
            this.brAddCallbacksForAllScanTargets.Name = "brAddCallbacksForAllScanTargets";
            this.brAddCallbacksForAllScanTargets.Size = new System.Drawing.Size(192, 36);
            this.brAddCallbacksForAllScanTargets.TabIndex = 3;
            this.brAddCallbacksForAllScanTargets.Text = "Add Callbacks for ALL \'Scan Target\'";
            this.brAddCallbacksForAllScanTargets.UseVisualStyleBackColor = true;
            // 
            // brAddCallbacksForSelectedScanTarget
            // 
            this.brAddCallbacksForSelectedScanTarget.Location = new System.Drawing.Point(6, 19);
            this.brAddCallbacksForSelectedScanTarget.Name = "brAddCallbacksForSelectedScanTarget";
            this.brAddCallbacksForSelectedScanTarget.Size = new System.Drawing.Size(192, 35);
            this.brAddCallbacksForSelectedScanTarget.TabIndex = 2;
            this.brAddCallbacksForSelectedScanTarget.Text = "Add Callbacks for Selected \'Scan Target\'";
            this.brAddCallbacksForSelectedScanTarget.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.btScanAll);
            this.groupBox3.Controls.Add(this.btScanSelected);
            this.groupBox3.Location = new System.Drawing.Point(4, 278);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(204, 103);
            this.groupBox3.TabIndex = 1;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Scan Projects";
            // 
            // btScanAll
            // 
            this.btScanAll.Location = new System.Drawing.Point(6, 57);
            this.btScanAll.Name = "btScanAll";
            this.btScanAll.Size = new System.Drawing.Size(192, 34);
            this.btScanAll.TabIndex = 3;
            this.btScanAll.Text = "Scan ALL \'Scan Target\'";
            this.btScanAll.UseVisualStyleBackColor = true;
            // 
            // btScanSelected
            // 
            this.btScanSelected.Location = new System.Drawing.Point(6, 19);
            this.btScanSelected.Name = "btScanSelected";
            this.btScanSelected.Size = new System.Drawing.Size(192, 32);
            this.btScanSelected.TabIndex = 2;
            this.btScanSelected.Text = "Scan Selected \'Scan Target\'";
            this.btScanSelected.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.btCreateCirForAllScanTargets);
            this.groupBox2.Controls.Add(this.btCreateCirForSelectedScanTarget);
            this.groupBox2.Location = new System.Drawing.Point(4, 24);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(204, 114);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Create CIR";
            // 
            // btCreateCirForAllScanTargets
            // 
            this.btCreateCirForAllScanTargets.Location = new System.Drawing.Point(6, 69);
            this.btCreateCirForAllScanTargets.Name = "btCreateCirForAllScanTargets";
            this.btCreateCirForAllScanTargets.Size = new System.Drawing.Size(192, 29);
            this.btCreateCirForAllScanTargets.TabIndex = 1;
            this.btCreateCirForAllScanTargets.Text = "Create CIR for ALL \'Scan Target\'";
            this.btCreateCirForAllScanTargets.UseVisualStyleBackColor = true;
            // 
            // btCreateCirForSelectedScanTarget
            // 
            this.btCreateCirForSelectedScanTarget.Location = new System.Drawing.Point(6, 26);
            this.btCreateCirForSelectedScanTarget.Name = "btCreateCirForSelectedScanTarget";
            this.btCreateCirForSelectedScanTarget.Size = new System.Drawing.Size(192, 37);
            this.btCreateCirForSelectedScanTarget.TabIndex = 0;
            this.btCreateCirForSelectedScanTarget.Text = "Create CIR for Selected \'Scan Target\'";
            this.btCreateCirForSelectedScanTarget.UseVisualStyleBackColor = true;
            // 
            // directoryWithResults
            // 
            this.directoryWithResults._ProcessDroppedObjects = true;
            this.directoryWithResults._ShowFileSize = false;
            this.directoryWithResults._ShowLinkToUpperFolder = true;
            this.directoryWithResults._ViewMode = O2.Views.ASCX.CoreControls.ascx_Directory.ViewMode.Simple;
            this.directoryWithResults._WatchFolder = true;
            this.directoryWithResults.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.directoryWithResults.BackColor = System.Drawing.SystemColors.Control;
            this.directoryWithResults.ForeColor = System.Drawing.Color.Black;
            this.directoryWithResults.Location = new System.Drawing.Point(6, 65);
            this.directoryWithResults.Name = "directoryWithResults";
            this.directoryWithResults.Size = new System.Drawing.Size(302, 372);
            this.directoryWithResults.TabIndex = 33;
            // 
            // tbTargetFolder
            // 
            this.tbTargetFolder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbTargetFolder.Location = new System.Drawing.Point(83, 36);
            this.tbTargetFolder.Name = "tbTargetFolder";
            this.tbTargetFolder.Size = new System.Drawing.Size(225, 20);
            this.tbTargetFolder.TabIndex = 32;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 36);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(73, 13);
            this.label3.TabIndex = 31;
            this.label3.Text = "Target Folder:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(3, 3);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 13);
            this.label2.TabIndex = 30;
            this.label2.Text = "Results";
            // 
            // ascx_ScanWizard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Name = "ascx_ScanWizard";
            this.Size = new System.Drawing.Size(820, 444);
            this.Load += new System.EventHandler(this.ascx_ScanWizard_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.Panel2.PerformLayout();
            this.splitContainer2.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox lbScanTargets;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btScanAll;
        private System.Windows.Forms.Button btScanSelected;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btCreateCirForAllScanTargets;
        private System.Windows.Forms.Button btCreateCirForSelectedScanTarget;
        private O2.Views.ASCX.CoreControls.ascx_Directory directoryWithResults;
        private System.Windows.Forms.TextBox tbTargetFolder;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button brAddCallbacksForAllScanTargets;
        private System.Windows.Forms.Button brAddCallbacksForSelectedScanTarget;
    }
}
