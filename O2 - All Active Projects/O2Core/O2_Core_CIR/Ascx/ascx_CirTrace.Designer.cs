namespace O2.Core.CIR.Ascx
{
    partial class ascx_CirTrace
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
            this.cirTreeView = new System.Windows.Forms.TreeView();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.traceTreeView = new O2.Views.ASCX.O2Findings.ascx_TraceTreeView();
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
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.cirTreeView);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.label2);
            this.splitContainer1.Panel2.Controls.Add(this.traceTreeView);
            this.splitContainer1.Size = new System.Drawing.Size(609, 387);
            this.splitContainer1.SplitterDistance = 283;
            this.splitContainer1.TabIndex = 0;
            // 
            // cirTreeView
            // 
            this.cirTreeView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cirTreeView.Location = new System.Drawing.Point(4, 18);
            this.cirTreeView.Name = "cirTreeView";
            this.cirTreeView.Size = new System.Drawing.Size(272, 362);
            this.cirTreeView.TabIndex = 2;
            this.cirTreeView.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(this.cirTreeView_BeforeExpand);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 2);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(96, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Cir View (Call Flow)";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 1);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(115, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Trace  View (Call Flow)";
            // 
            // traceTreeView
            // 
            this.traceTreeView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.traceTreeView.bMoveTraces = true;
            this.traceTreeView.Location = new System.Drawing.Point(6, 18);
            this.traceTreeView.Name = "traceTreeView";
            this.traceTreeView.o2Finding = null;
            this.traceTreeView.o2Trace = null;
            this.traceTreeView.selectedNode = null;
            this.traceTreeView.selectedNodeTag = null;
            this.traceTreeView.Size = new System.Drawing.Size(309, 362);
            this.traceTreeView.TabIndex = 0;
            // 
            // ascx_CirTrace
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Name = "ascx_CirTrace";
            this.Size = new System.Drawing.Size(609, 387);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TreeView cirTreeView;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private O2.Views.ASCX.O2Findings.ascx_TraceTreeView traceTreeView;
    }
}
