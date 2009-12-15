// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
namespace O2.Debugger.Mdbg.O2Debugger.Ascx
{
    partial class ascx_StackTraceAndThreads
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
            this.label3 = new System.Windows.Forms.Label();
            this.tvThreadsAndStackTrace = new System.Windows.Forms.TreeView();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tvExecutionArchive = new System.Windows.Forms.TreeView();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(0, 5);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(129, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Threads and Stack Trace";
            // 
            // tvThreadsAndStackTrace
            // 
            this.tvThreadsAndStackTrace.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvThreadsAndStackTrace.Location = new System.Drawing.Point(0, 0);
            this.tvThreadsAndStackTrace.Name = "tvThreadsAndStackTrace";
            this.tvThreadsAndStackTrace.Size = new System.Drawing.Size(298, 379);
            this.tvThreadsAndStackTrace.TabIndex = 8;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer1.Location = new System.Drawing.Point(4, 22);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.tvExecutionArchive);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tvThreadsAndStackTrace);
            this.splitContainer1.Size = new System.Drawing.Size(458, 383);
            this.splitContainer1.SplitterDistance = 152;
            this.splitContainer1.TabIndex = 10;
            // 
            // tvExecutionArchive
            // 
            this.tvExecutionArchive.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvExecutionArchive.Location = new System.Drawing.Point(0, 0);
            this.tvExecutionArchive.Name = "tvExecutionArchive";
            this.tvExecutionArchive.Size = new System.Drawing.Size(148, 379);
            this.tvExecutionArchive.TabIndex = 9;
            this.tvExecutionArchive.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvExecutionArchive_AfterSelect);
            // 
            // ascx_StackTraceAndThreads
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.label3);
            this.Name = "ascx_StackTraceAndThreads";
            this.Size = new System.Drawing.Size(465, 408);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TreeView tvThreadsAndStackTrace;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TreeView tvExecutionArchive;
    }
}
