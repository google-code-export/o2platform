namespace O2.Rules.OunceLabs.Ascx
{
    partial class ascx_ApplyRulesToFindings
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
            this.label1 = new System.Windows.Forms.Label();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cbAddFindingsWithNoMatches = new System.Windows.Forms.CheckBox();
            this.btMapFirstSourcesThenSinksToAllTraces = new System.Windows.Forms.Button();
            this.btMapSourcesToAllTraces = new System.Windows.Forms.Button();
            this.btCreateAllPartialTraces = new System.Windows.Forms.Button();
            this.btFilter_MapSinksToAllTraces = new System.Windows.Forms.Button();
            this.btFilter_BasicSinksMapping = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.rulePackViewer = new O2.Rules.OunceLabs.Ascx.ascx_RulePackViewer();
            this.findingsViewerSourceFindings = new O2.Views.ASCX.O2Findings.ascx_FindingsViewer();
            this.findingsViewerMappedFindings = new O2.Views.ASCX.O2Findings.ascx_FindingsViewer();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.rulePackViewer);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(916, 609);
            this.splitContainer1.SplitterDistance = 334;
            this.splitContainer1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(-2, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(118, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Loaded O2 Rule Packs";
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
            this.splitContainer2.Panel1.Controls.Add(this.findingsViewerSourceFindings);
            this.splitContainer2.Panel1.Controls.Add(this.label2);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.groupBox1);
            this.splitContainer2.Panel2.Controls.Add(this.findingsViewerMappedFindings);
            this.splitContainer2.Panel2.Controls.Add(this.label4);
            this.splitContainer2.Panel2.Controls.Add(this.label3);
            this.splitContainer2.Size = new System.Drawing.Size(916, 271);
            this.splitContainer2.SplitterDistance = 385;
            this.splitContainer2.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Source Findings";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cbAddFindingsWithNoMatches);
            this.groupBox1.Controls.Add(this.btMapFirstSourcesThenSinksToAllTraces);
            this.groupBox1.Controls.Add(this.btMapSourcesToAllTraces);
            this.groupBox1.Controls.Add(this.btCreateAllPartialTraces);
            this.groupBox1.Controls.Add(this.btFilter_MapSinksToAllTraces);
            this.groupBox1.Controls.Add(this.btFilter_BasicSinksMapping);
            this.groupBox1.Location = new System.Drawing.Point(4, 9);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(124, 378);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Rules Filters";
            // 
            // cbAddFindingsWithNoMatches
            // 
            this.cbAddFindingsWithNoMatches.CheckAlign = System.Drawing.ContentAlignment.TopLeft;
            this.cbAddFindingsWithNoMatches.Location = new System.Drawing.Point(7, 212);
            this.cbAddFindingsWithNoMatches.Name = "cbAddFindingsWithNoMatches";
            this.cbAddFindingsWithNoMatches.Size = new System.Drawing.Size(104, 48);
            this.cbAddFindingsWithNoMatches.TabIndex = 10;
            this.cbAddFindingsWithNoMatches.Text = "Add Findings With No Matches";
            this.cbAddFindingsWithNoMatches.UseVisualStyleBackColor = true;
            // 
            // btMapFirstSourcesThenSinksToAllTraces
            // 
            this.btMapFirstSourcesThenSinksToAllTraces.Location = new System.Drawing.Point(6, 19);
            this.btMapFirstSourcesThenSinksToAllTraces.Name = "btMapFirstSourcesThenSinksToAllTraces";
            this.btMapFirstSourcesThenSinksToAllTraces.Size = new System.Drawing.Size(112, 60);
            this.btMapFirstSourcesThenSinksToAllTraces.TabIndex = 9;
            this.btMapFirstSourcesThenSinksToAllTraces.Text = "Map First Sources then Sinks to All Traces";
            this.btMapFirstSourcesThenSinksToAllTraces.UseVisualStyleBackColor = true;
            this.btMapFirstSourcesThenSinksToAllTraces.Click += new System.EventHandler(this.btMapFirstSourcesThenSinksToAllTraces_Click);
            // 
            // btMapSourcesToAllTraces
            // 
            this.btMapSourcesToAllTraces.Location = new System.Drawing.Point(6, 159);
            this.btMapSourcesToAllTraces.Name = "btMapSourcesToAllTraces";
            this.btMapSourcesToAllTraces.Size = new System.Drawing.Size(112, 48);
            this.btMapSourcesToAllTraces.TabIndex = 8;
            this.btMapSourcesToAllTraces.Text = "Map Sources to All Traces";
            this.btMapSourcesToAllTraces.UseVisualStyleBackColor = true;
            this.btMapSourcesToAllTraces.Click += new System.EventHandler(this.btMapSourcesToAllTraces_Click);
            // 
            // btCreateAllPartialTraces
            // 
            this.btCreateAllPartialTraces.Location = new System.Drawing.Point(6, 324);
            this.btCreateAllPartialTraces.Name = "btCreateAllPartialTraces";
            this.btCreateAllPartialTraces.Size = new System.Drawing.Size(112, 48);
            this.btCreateAllPartialTraces.TabIndex = 7;
            this.btCreateAllPartialTraces.Text = "Create All Partial Traces";
            this.btCreateAllPartialTraces.UseVisualStyleBackColor = true;
            this.btCreateAllPartialTraces.Click += new System.EventHandler(this.btCreateAllPartialTraces_Click);
            // 
            // btFilter_MapSinksToAllTraces
            // 
            this.btFilter_MapSinksToAllTraces.Location = new System.Drawing.Point(6, 103);
            this.btFilter_MapSinksToAllTraces.Name = "btFilter_MapSinksToAllTraces";
            this.btFilter_MapSinksToAllTraces.Size = new System.Drawing.Size(112, 48);
            this.btFilter_MapSinksToAllTraces.TabIndex = 6;
            this.btFilter_MapSinksToAllTraces.Text = "Map Sinks to All Traces";
            this.btFilter_MapSinksToAllTraces.UseVisualStyleBackColor = true;
            this.btFilter_MapSinksToAllTraces.Click += new System.EventHandler(this.btFilter_MapSinksToAllTraces_Click);
            // 
            // btFilter_BasicSinksMapping
            // 
            this.btFilter_BasicSinksMapping.Location = new System.Drawing.Point(6, 270);
            this.btFilter_BasicSinksMapping.Name = "btFilter_BasicSinksMapping";
            this.btFilter_BasicSinksMapping.Size = new System.Drawing.Size(112, 48);
            this.btFilter_BasicSinksMapping.TabIndex = 5;
            this.btFilter_BasicSinksMapping.Text = "Basic Sinks Mapping";
            this.btFilter_BasicSinksMapping.UseVisualStyleBackColor = true;
            this.btFilter_BasicSinksMapping.Click += new System.EventHandler(this.btFilter_BasicSinksMapping_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(244, 166);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(35, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "label4";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(131, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(141, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Findings after filter execution";
            // 
            // rulePackViewer
            // 
            this.rulePackViewer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.rulePackViewer.Location = new System.Drawing.Point(1, 21);
            this.rulePackViewer.Name = "rulePackViewer";
            this.rulePackViewer.Size = new System.Drawing.Size(908, 309);
            this.rulePackViewer.TabIndex = 1;
            // 
            // findingsViewerSourceFindings
            // 
            this.findingsViewerSourceFindings.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.findingsViewerSourceFindings.Location = new System.Drawing.Point(4, 26);
            this.findingsViewerSourceFindings.Name = "findingsViewerSourceFindings";
            this.findingsViewerSourceFindings.Size = new System.Drawing.Size(374, 238);
            this.findingsViewerSourceFindings.TabIndex = 2;
            this.findingsViewerSourceFindings._onFolderSelectEvent += new O2.DotNetWrappers.DotNet.O2Thread.FuncVoidT1<string>(this.findingsViewerSourceFindings__onFolderSelectEvent);
            this.findingsViewerSourceFindings._onFindingSelected += new O2.DotNetWrappers.DotNet.O2Thread.FuncVoidT1<O2.Kernel.Interfaces.O2Findings.IO2Finding>(this.findingsViewerSourceFindings__onFindingSelected);
            this.findingsViewerSourceFindings._onTraceSelected += new O2.DotNetWrappers.DotNet.O2Thread.FuncVoidT1<O2.Kernel.Interfaces.O2Findings.IO2Trace>(this.findingsViewerSourceFindings__onTraceSelected);
            // 
            // findingsViewerMappedFindings
            // 
            this.findingsViewerMappedFindings.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.findingsViewerMappedFindings.Location = new System.Drawing.Point(134, 26);
            this.findingsViewerMappedFindings.Name = "findingsViewerMappedFindings";
            this.findingsViewerMappedFindings.Size = new System.Drawing.Size(383, 237);
            this.findingsViewerMappedFindings.TabIndex = 4;
            this.findingsViewerMappedFindings._onFindingSelected += new O2.DotNetWrappers.DotNet.O2Thread.FuncVoidT1<O2.Kernel.Interfaces.O2Findings.IO2Finding>(this.findingsViewerMappedFindings__onFindingSelected);
            this.findingsViewerMappedFindings._onTraceSelected += new O2.DotNetWrappers.DotNet.O2Thread.FuncVoidT1<O2.Kernel.Interfaces.O2Findings.IO2Trace>(this.findingsViewerMappedFindings__onTraceSelected);
            // 
            // ascx_ApplyRulesToFindings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Name = "ascx_ApplyRulesToFindings";
            this.Size = new System.Drawing.Size(916, 609);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel1.PerformLayout();
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.Panel2.PerformLayout();
            this.splitContainer2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private ascx_RulePackViewer rulePackViewer;
        private O2.Views.ASCX.O2Findings.ascx_FindingsViewer findingsViewerSourceFindings;
        private O2.Views.ASCX.O2Findings.ascx_FindingsViewer findingsViewerMappedFindings;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btFilter_BasicSinksMapping;
        private System.Windows.Forms.Button btFilter_MapSinksToAllTraces;
        private System.Windows.Forms.Button btCreateAllPartialTraces;
        private System.Windows.Forms.Button btMapSourcesToAllTraces;
        private System.Windows.Forms.Button btMapFirstSourcesThenSinksToAllTraces;
        private System.Windows.Forms.CheckBox cbAddFindingsWithNoMatches;
    }
}
