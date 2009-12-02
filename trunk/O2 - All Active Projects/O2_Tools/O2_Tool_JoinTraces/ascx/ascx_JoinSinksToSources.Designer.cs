namespace O2.Tool.JoinTraces.ascx
{
    partial class ascx_JoinSinksToSources
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
            this.scTopLevel = new System.Windows.Forms.SplitContainer();
            this.sc = new System.Windows.Forms.SplitContainer();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.findingsViewer_Sinks = new O2.Views.ASCX.O2Findings.ascx_FindingsViewer();
            this.findingsViewer_Sources = new O2.Views.ASCX.O2Findings.ascx_FindingsViewer();
            this.findingsViewer_JoinnedTraces = new O2.Views.ASCX.O2Findings.ascx_FindingsViewer();
            this.btCalculateJoinnedTraces = new System.Windows.Forms.Button();
            this.scTopLevel.Panel1.SuspendLayout();
            this.scTopLevel.Panel2.SuspendLayout();
            this.scTopLevel.SuspendLayout();
            this.sc.Panel1.SuspendLayout();
            this.sc.Panel2.SuspendLayout();
            this.sc.SuspendLayout();
            this.SuspendLayout();
            // 
            // scTopLevel
            // 
            this.scTopLevel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.scTopLevel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.scTopLevel.Location = new System.Drawing.Point(3, 34);
            this.scTopLevel.Name = "scTopLevel";
            this.scTopLevel.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // scTopLevel.Panel1
            // 
            this.scTopLevel.Panel1.Controls.Add(this.sc);
            // 
            // scTopLevel.Panel2
            // 
            this.scTopLevel.Panel2.Controls.Add(this.btCalculateJoinnedTraces);
            this.scTopLevel.Panel2.Controls.Add(this.findingsViewer_JoinnedTraces);
            this.scTopLevel.Panel2.Controls.Add(this.label4);
            this.scTopLevel.Size = new System.Drawing.Size(1185, 626);
            this.scTopLevel.SplitterDistance = 320;
            this.scTopLevel.TabIndex = 0;
            // 
            // sc
            // 
            this.sc.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.sc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sc.Location = new System.Drawing.Point(0, 0);
            this.sc.Name = "sc";
            // 
            // sc.Panel1
            // 
            this.sc.Panel1.Controls.Add(this.findingsViewer_Sinks);
            this.sc.Panel1.Controls.Add(this.label2);
            // 
            // sc.Panel2
            // 
            this.sc.Panel2.Controls.Add(this.findingsViewer_Sources);
            this.sc.Panel2.Controls.Add(this.label3);
            this.sc.Size = new System.Drawing.Size(1185, 320);
            this.sc.SplitterDistance = 564;
            this.sc.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(474, 31);
            this.label1.TabIndex = 1;
            this.label1.Text = "This module will join traces using a very simple formula: Sinks on the Left that " +
                "Match Sources on the Right will generate 1 new trace";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(4, 4);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(134, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Findings to get SINKS from";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 4);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(154, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "Findings to get SOURCES from";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 15);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(80, 13);
            this.label4.TabIndex = 1;
            this.label4.Text = "Joinned Traces";
            // 
            // findingsViewer_Sinks
            // 
            this.findingsViewer_Sinks.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.findingsViewer_Sinks.Location = new System.Drawing.Point(7, 21);
            this.findingsViewer_Sinks.Name = "findingsViewer_Sinks";
            this.findingsViewer_Sinks.Size = new System.Drawing.Size(550, 292);
            this.findingsViewer_Sinks.TabIndex = 1;
            // 
            // findingsViewer_Sources
            // 
            this.findingsViewer_Sources.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.findingsViewer_Sources.Location = new System.Drawing.Point(6, 21);
            this.findingsViewer_Sources.Name = "findingsViewer_Sources";
            this.findingsViewer_Sources.Size = new System.Drawing.Size(606, 292);
            this.findingsViewer_Sources.TabIndex = 2;
            // 
            // findingsViewer_JoinnedTraces
            // 
            this.findingsViewer_JoinnedTraces.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.findingsViewer_JoinnedTraces.Location = new System.Drawing.Point(7, 52);
            this.findingsViewer_JoinnedTraces.Name = "findingsViewer_JoinnedTraces";
            this.findingsViewer_JoinnedTraces.Size = new System.Drawing.Size(1171, 243);
            this.findingsViewer_JoinnedTraces.TabIndex = 2;
            // 
            // btCalculateJoinnedTraces
            // 
            this.btCalculateJoinnedTraces.Location = new System.Drawing.Point(101, 10);
            this.btCalculateJoinnedTraces.Name = "btCalculateJoinnedTraces";
            this.btCalculateJoinnedTraces.Size = new System.Drawing.Size(164, 23);
            this.btCalculateJoinnedTraces.TabIndex = 3;
            this.btCalculateJoinnedTraces.Text = "Calculate Joinned Traces";
            this.btCalculateJoinnedTraces.UseVisualStyleBackColor = true;
            this.btCalculateJoinnedTraces.Click += new System.EventHandler(this.btCalculateJoinnedTraces_Click);
            // 
            // ascx_JoinSinksToSources
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label1);
            this.Controls.Add(this.scTopLevel);
            this.Name = "ascx_JoinSinksToSources";
            this.Size = new System.Drawing.Size(1191, 660);
            this.Load += new System.EventHandler(this.ascx_JoinSinksToSources_Load);
            this.scTopLevel.Panel1.ResumeLayout(false);
            this.scTopLevel.Panel2.ResumeLayout(false);
            this.scTopLevel.Panel2.PerformLayout();
            this.scTopLevel.ResumeLayout(false);
            this.sc.Panel1.ResumeLayout(false);
            this.sc.Panel1.PerformLayout();
            this.sc.Panel2.ResumeLayout(false);
            this.sc.Panel2.PerformLayout();
            this.sc.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer scTopLevel;
        private System.Windows.Forms.SplitContainer sc;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label1;
        private O2.Views.ASCX.O2Findings.ascx_FindingsViewer findingsViewer_Sinks;
        private O2.Views.ASCX.O2Findings.ascx_FindingsViewer findingsViewer_Sources;
        private O2.Views.ASCX.O2Findings.ascx_FindingsViewer findingsViewer_JoinnedTraces;
        private System.Windows.Forms.Button btCalculateJoinnedTraces;
    }
}
