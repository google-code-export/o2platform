namespace O2.Cmd.SpringMvc.Ascx
{
    partial class ascx_JoinControllersWithFindings
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
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.findingsViewerWith_ScanResults = new O2.Views.ASCX.O2Findings.ascx_FindingsViewer();
            this.label2 = new System.Windows.Forms.Label();
            this.splitContainer_withTempFileAndJoinTraces = new System.Windows.Forms.SplitContainer();
            this.findingsViewerWith_JoinResults = new O2.Views.ASCX.O2Findings.ascx_FindingsViewer();
            this.label1 = new System.Windows.Forms.Label();
            this.btLoadTestData = new System.Windows.Forms.Button();
            this.btGenerateJspTraces = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.springMvcMappings = new O2.Cmd.SpringMvc.Ascx.ascx_SpringMvcMappings();
            this.sourceCodeEditor_withController = new O2.External.SharpDevelop.Ascx.ascx_SourceCodeEditor();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.splitContainer_withTempFileAndJoinTraces.Panel1.SuspendLayout();
            this.splitContainer_withTempFileAndJoinTraces.Panel2.SuspendLayout();
            this.splitContainer_withTempFileAndJoinTraces.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer1.Location = new System.Drawing.Point(0, 47);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.springMvcMappings);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(903, 464);
            this.splitContainer1.SplitterDistance = 256;
            this.splitContainer1.TabIndex = 0;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.findingsViewerWith_ScanResults);
            this.splitContainer2.Panel1.Controls.Add(this.label2);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.splitContainer_withTempFileAndJoinTraces);
            this.splitContainer2.Size = new System.Drawing.Size(636, 457);
            this.splitContainer2.SplitterDistance = 219;
            this.splitContainer2.TabIndex = 0;
            // 
            // findingsViewerWith_ScanResults
            // 
            this.findingsViewerWith_ScanResults.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.findingsViewerWith_ScanResults.Location = new System.Drawing.Point(3, 35);
            this.findingsViewerWith_ScanResults.Name = "findingsViewerWith_ScanResults";
            this.findingsViewerWith_ScanResults.Size = new System.Drawing.Size(626, 177);
            this.findingsViewerWith_ScanResults.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 7);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(175, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Source Findings (from Ounce Scan)";
            // 
            // splitContainer_withTempFileAndJoinTraces
            // 
            this.splitContainer_withTempFileAndJoinTraces.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer_withTempFileAndJoinTraces.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer_withTempFileAndJoinTraces.Location = new System.Drawing.Point(0, 0);
            this.splitContainer_withTempFileAndJoinTraces.Name = "splitContainer_withTempFileAndJoinTraces";
            // 
            // splitContainer_withTempFileAndJoinTraces.Panel1
            // 
            this.splitContainer_withTempFileAndJoinTraces.Panel1.Controls.Add(this.sourceCodeEditor_withController);
            // 
            // splitContainer_withTempFileAndJoinTraces.Panel2
            // 
            this.splitContainer_withTempFileAndJoinTraces.Panel2.Controls.Add(this.findingsViewerWith_JoinResults);
            this.splitContainer_withTempFileAndJoinTraces.Size = new System.Drawing.Size(636, 234);
            this.splitContainer_withTempFileAndJoinTraces.SplitterDistance = 299;
            this.splitContainer_withTempFileAndJoinTraces.TabIndex = 0;
            // 
            // findingsViewerWith_JoinResults
            // 
            this.findingsViewerWith_JoinResults.Dock = System.Windows.Forms.DockStyle.Fill;
            this.findingsViewerWith_JoinResults.Location = new System.Drawing.Point(0, 0);
            this.findingsViewerWith_JoinResults.Name = "findingsViewerWith_JoinResults";
            this.findingsViewerWith_JoinResults.Size = new System.Drawing.Size(329, 230);
            this.findingsViewerWith_JoinResults.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(130, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Map controllers to findings";
            // 
            // btLoadTestData
            // 
            this.btLoadTestData.Location = new System.Drawing.Point(172, 7);
            this.btLoadTestData.Name = "btLoadTestData";
            this.btLoadTestData.Size = new System.Drawing.Size(113, 23);
            this.btLoadTestData.TabIndex = 2;
            this.btLoadTestData.Text = "Load Test Data";
            this.btLoadTestData.UseVisualStyleBackColor = true;
            this.btLoadTestData.Click += new System.EventHandler(this.btLoadTestData_Click);
            // 
            // btGenerateJspTraces
            // 
            this.btGenerateJspTraces.Location = new System.Drawing.Point(299, 7);
            this.btGenerateJspTraces.Name = "btGenerateJspTraces";
            this.btGenerateJspTraces.Size = new System.Drawing.Size(168, 23);
            this.btGenerateJspTraces.TabIndex = 3;
            this.btGenerateJspTraces.Text = "Generate Jsp Traces";
            this.btGenerateJspTraces.UseVisualStyleBackColor = true;
            this.btGenerateJspTraces.Click += new System.EventHandler(this.btGenerateJspTraces_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(539, 12);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(113, 23);
            this.button2.TabIndex = 4;
            this.button2.Text = "Load Test Data";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // springMvcMappings
            // 
            this.springMvcMappings.Dock = System.Windows.Forms.DockStyle.Fill;
            this.springMvcMappings.Location = new System.Drawing.Point(0, 0);
            this.springMvcMappings.Name = "springMvcMappings";
            this.springMvcMappings.Size = new System.Drawing.Size(252, 460);
            this.springMvcMappings.TabIndex = 0;
            this.springMvcMappings._onTreeViewSelect += new O2.DotNetWrappers.DotNet.O2Thread.FuncVoidT1<System.Windows.Forms.TreeView>(this.springMvcMappings__onTreeViewSelect);
            // 
            // sourceCodeEditor_withController
            // 
            this.sourceCodeEditor_withController.AllowDrop = true;
            this.sourceCodeEditor_withController.BackColor = System.Drawing.SystemColors.Control;
            this.sourceCodeEditor_withController.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sourceCodeEditor_withController.ForeColor = System.Drawing.Color.Black;
            this.sourceCodeEditor_withController.Location = new System.Drawing.Point(0, 0);
            this.sourceCodeEditor_withController.Name = "sourceCodeEditor_withController";
            this.sourceCodeEditor_withController.Size = new System.Drawing.Size(295, 230);
            this.sourceCodeEditor_withController.TabIndex = 0;
            // 
            // ascx_JoinControllersWithFindings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.button2);
            this.Controls.Add(this.btGenerateJspTraces);
            this.Controls.Add(this.btLoadTestData);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.splitContainer1);
            this.Name = "ascx_JoinControllersWithFindings";
            this.Size = new System.Drawing.Size(906, 514);
            this.Load += new System.EventHandler(this.ascx_JoinControllersWithFindings_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel1.PerformLayout();
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.ResumeLayout(false);
            this.splitContainer_withTempFileAndJoinTraces.Panel1.ResumeLayout(false);
            this.splitContainer_withTempFileAndJoinTraces.Panel2.ResumeLayout(false);
            this.splitContainer_withTempFileAndJoinTraces.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private ascx_SpringMvcMappings springMvcMappings;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private O2.Views.ASCX.O2Findings.ascx_FindingsViewer findingsViewerWith_ScanResults;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btLoadTestData;
        private System.Windows.Forms.Button btGenerateJspTraces;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.SplitContainer splitContainer_withTempFileAndJoinTraces;
        private O2.Views.ASCX.O2Findings.ascx_FindingsViewer findingsViewerWith_JoinResults;
        private O2.External.SharpDevelop.Ascx.ascx_SourceCodeEditor sourceCodeEditor_withController;
    }
}
