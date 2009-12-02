namespace O2.Legacy.OunceV6.JoinTraces
{
    partial class ascx_JoinDotNetWebServices
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
            this.lbCirFileLoaded = new System.Windows.Forms.Label();
            this.lbTargetSavedAssessmentFiles = new System.Windows.Forms.ListBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btCreateTraces = new System.Windows.Forms.Button();
            this.findingsViewerfor_JoinnedTraces = new O2.Views.ASCX.O2Findings.ascx_FindingsViewer();
            this.cbMakeLostSinksIntoSinks = new System.Windows.Forms.CheckBox();
            this.lbCreatedAssessmentFile = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
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
            this.splitContainer1.Panel1.Controls.Add(this.lbCreatedAssessmentFile);
            this.splitContainer1.Panel1.Controls.Add(this.cbMakeLostSinksIntoSinks);
            this.splitContainer1.Panel1.Controls.Add(this.label12);
            this.splitContainer1.Panel1.Controls.Add(this.btCreateTraces);
            this.splitContainer1.Panel1.Controls.Add(this.label2);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            this.splitContainer1.Panel1.Controls.Add(this.lbCirFileLoaded);
            this.splitContainer1.Panel1.Controls.Add(this.lbTargetSavedAssessmentFiles);
            this.splitContainer1.Panel1.Controls.Add(this.label8);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.findingsViewerfor_JoinnedTraces);
            this.splitContainer1.Size = new System.Drawing.Size(708, 469);
            this.splitContainer1.SplitterDistance = 263;
            this.splitContainer1.TabIndex = 0;
            // 
            // lbCirFileLoaded
            // 
            this.lbCirFileLoaded.AutoSize = true;
            this.lbCirFileLoaded.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbCirFileLoaded.Location = new System.Drawing.Point(107, 169);
            this.lbCirFileLoaded.Name = "lbCirFileLoaded";
            this.lbCirFileLoaded.Size = new System.Drawing.Size(15, 13);
            this.lbCirFileLoaded.TabIndex = 33;
            this.lbCirFileLoaded.Text = "..";
            // 
            // lbTargetSavedAssessmentFiles
            // 
            this.lbTargetSavedAssessmentFiles.AllowDrop = true;
            this.lbTargetSavedAssessmentFiles.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lbTargetSavedAssessmentFiles.FormattingEnabled = true;
            this.lbTargetSavedAssessmentFiles.Location = new System.Drawing.Point(6, 71);
            this.lbTargetSavedAssessmentFiles.Name = "lbTargetSavedAssessmentFiles";
            this.lbTargetSavedAssessmentFiles.Size = new System.Drawing.Size(250, 95);
            this.lbTargetSavedAssessmentFiles.TabIndex = 31;
            this.lbTargetSavedAssessmentFiles.DragDrop += new System.Windows.Forms.DragEventHandler(this.lbTargetSavedAssessmentFiles_DragDrop);
            this.lbTargetSavedAssessmentFiles.DragEnter += new System.Windows.Forms.DragEventHandler(this.lbTargetSavedAssessmentFiles_DragEnter);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(3, 169);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(98, 13);
            this.label8.TabIndex = 32;
            this.label8.Text = "CirData fiile loaded:";
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(3, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(253, 47);
            this.label1.TabIndex = 34;
            this.label1.Text = "STEP 1: Drop on the textbox below the CirData of the WebServices layer and the Oz" +
                "amst files with traces";
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(1, 192);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(242, 26);
            this.label2.TabIndex = 35;
            this.label2.Text = "STEP 2: Click on the button below";
            // 
            // btCreateTraces
            // 
            this.btCreateTraces.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.btCreateTraces.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btCreateTraces.Location = new System.Drawing.Point(3, 221);
            this.btCreateTraces.Name = "btCreateTraces";
            this.btCreateTraces.Size = new System.Drawing.Size(253, 75);
            this.btCreateTraces.TabIndex = 36;
            this.btCreateTraces.Text = "Step 2: Create Assessment File With ALL Traces (and glued .NET Web Services)";
            this.btCreateTraces.UseVisualStyleBackColor = true;
            this.btCreateTraces.Click += new System.EventHandler(this.btCreateTraces_Click);
            // 
            // findingsViewerfor_JoinnedTraces
            // 
            this.findingsViewerfor_JoinnedTraces.Dock = System.Windows.Forms.DockStyle.Fill;
            this.findingsViewerfor_JoinnedTraces.Location = new System.Drawing.Point(0, 0);
            this.findingsViewerfor_JoinnedTraces.Name = "findingsViewerfor_JoinnedTraces";
            this.findingsViewerfor_JoinnedTraces.Size = new System.Drawing.Size(437, 465);
            this.findingsViewerfor_JoinnedTraces.TabIndex = 1;
            // 
            // cbMakeLostSinksIntoSinks
            // 
            this.cbMakeLostSinksIntoSinks.AutoSize = true;
            this.cbMakeLostSinksIntoSinks.Checked = true;
            this.cbMakeLostSinksIntoSinks.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbMakeLostSinksIntoSinks.Location = new System.Drawing.Point(6, 302);
            this.cbMakeLostSinksIntoSinks.Name = "cbMakeLostSinksIntoSinks";
            this.cbMakeLostSinksIntoSinks.Size = new System.Drawing.Size(151, 17);
            this.cbMakeLostSinksIntoSinks.TabIndex = 37;
            this.cbMakeLostSinksIntoSinks.Text = "Make LostSinks into Sinks";
            this.cbMakeLostSinksIntoSinks.UseVisualStyleBackColor = true;
            // 
            // lbCreatedAssessmentFile
            // 
            this.lbCreatedAssessmentFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lbCreatedAssessmentFile.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbCreatedAssessmentFile.ForeColor = System.Drawing.Color.Red;
            this.lbCreatedAssessmentFile.Location = new System.Drawing.Point(3, 377);
            this.lbCreatedAssessmentFile.Name = "lbCreatedAssessmentFile";
            this.lbCreatedAssessmentFile.Size = new System.Drawing.Size(253, 65);
            this.lbCreatedAssessmentFile.TabIndex = 40;
            this.lbCreatedAssessmentFile.Text = "Created Saved Assessment File:";
            this.lbCreatedAssessmentFile.Visible = false;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(3, 362);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(189, 13);
            this.label12.TabIndex = 39;
            this.label12.Text = "Created Saved Assessment File:";
            // 
            // ascx_JoinDotNetWebServices
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Name = "ascx_JoinDotNetWebServices";
            this.Size = new System.Drawing.Size(708, 469);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lbCirFileLoaded;
        private System.Windows.Forms.ListBox lbTargetSavedAssessmentFiles;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button btCreateTraces;
        private O2.Views.ASCX.O2Findings.ascx_FindingsViewer findingsViewerfor_JoinnedTraces;
        private System.Windows.Forms.CheckBox cbMakeLostSinksIntoSinks;
        private System.Windows.Forms.Label lbCreatedAssessmentFile;
        private System.Windows.Forms.Label label12;
    }
}
