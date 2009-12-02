namespace O2.Debugger.Mdbg.O2Debugger.Ascx
{
    partial class ascx_FindingsCreator
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
            this.label2 = new System.Windows.Forms.Label();
            this.llMakeTraceIntoFindings = new System.Windows.Forms.LinkLabel();
            this.findingsViewer = new O2.Views.ASCX.O2Findings.ascx_FindingsViewer();
            this.traceViewer = new O2.Views.ASCX.O2Findings.ascx_TraceTreeView();
            this.llClearTrace = new System.Windows.Forms.LinkLabel();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
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
            this.splitContainer1.Panel1.Controls.Add(this.findingsViewer);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            this.splitContainer1.Panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.splitContainer1_Panel1_Paint);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.llClearTrace);
            this.splitContainer1.Panel2.Controls.Add(this.traceViewer);
            this.splitContainer1.Panel2.Controls.Add(this.llMakeTraceIntoFindings);
            this.splitContainer1.Panel2.Controls.Add(this.label2);
            this.splitContainer1.Size = new System.Drawing.Size(405, 495);
            this.splitContainer1.SplitterDistance = 267;
            this.splitContainer1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(1, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(86, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Findings Created";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(130, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Dynamic trace Generation";
            // 
            // llMakeTraceIntoFindings
            // 
            this.llMakeTraceIntoFindings.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.llMakeTraceIntoFindings.AutoSize = true;
            this.llMakeTraceIntoFindings.Location = new System.Drawing.Point(141, 199);
            this.llMakeTraceIntoFindings.Name = "llMakeTraceIntoFindings";
            this.llMakeTraceIntoFindings.Size = new System.Drawing.Size(122, 13);
            this.llMakeTraceIntoFindings.TabIndex = 2;
            this.llMakeTraceIntoFindings.TabStop = true;
            this.llMakeTraceIntoFindings.Text = "Make Trace into Finding";
            this.llMakeTraceIntoFindings.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llMakeTraceIntoFindings_LinkClicked);
            // 
            // findingsViewer
            // 
            this.findingsViewer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.findingsViewer.Location = new System.Drawing.Point(6, 16);
            this.findingsViewer.Name = "findingsViewer";
            this.findingsViewer.Size = new System.Drawing.Size(392, 244);
            this.findingsViewer.TabIndex = 1;
            // 
            // traceViewer
            // 
            this.traceViewer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.traceViewer.bMoveTraces = true;
            this.traceViewer.Location = new System.Drawing.Point(6, 21);
            this.traceViewer.Name = "traceViewer";
            this.traceViewer.o2Finding = null;
            this.traceViewer.o2Trace = null;
            this.traceViewer.selectedNode = null;
            this.traceViewer.selectedNodeTag = null;
            this.traceViewer.Size = new System.Drawing.Size(392, 174);
            this.traceViewer.TabIndex = 3;
            // 
            // llClearTrace
            // 
            this.llClearTrace.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.llClearTrace.AutoSize = true;
            this.llClearTrace.Location = new System.Drawing.Point(336, 2);
            this.llClearTrace.Name = "llClearTrace";
            this.llClearTrace.Size = new System.Drawing.Size(62, 13);
            this.llClearTrace.TabIndex = 4;
            this.llClearTrace.TabStop = true;
            this.llClearTrace.Text = "Clear Trace";
            // 
            // ascx_FindingsCreator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Name = "ascx_FindingsCreator";
            this.Size = new System.Drawing.Size(405, 495);
            this.Load += new System.EventHandler(this.ascx_FindingsCreator_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private O2.Views.ASCX.O2Findings.ascx_FindingsViewer findingsViewer;
        private O2.Views.ASCX.O2Findings.ascx_TraceTreeView traceViewer;
        private System.Windows.Forms.LinkLabel llMakeTraceIntoFindings;
        private System.Windows.Forms.LinkLabel llClearTrace;
    }
}
