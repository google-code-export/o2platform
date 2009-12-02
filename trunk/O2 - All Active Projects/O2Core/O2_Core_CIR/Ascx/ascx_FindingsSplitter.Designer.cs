// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
namespace O2.Core.CIR.Ascx
{
    partial class ascx_FindingsSplitter
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ascx_FindingsSplitter));
            this.llCreateFindingsFromCir = new System.Windows.Forms.LinkLabel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.findingsViewer_ToProcess = new O2.Views.ASCX.O2Findings.ascx_FindingsViewer();
            this.label1 = new System.Windows.Forms.Label();
            this.cirDataViewer_ToProcess = new O2.Core.CIR.Ascx.ascx_CirDataViewer();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // llCreateFindingsFromCir
            // 
            this.llCreateFindingsFromCir.AutoSize = true;
            this.llCreateFindingsFromCir.Location = new System.Drawing.Point(225, 0);
            this.llCreateFindingsFromCir.Name = "llCreateFindingsFromCir";
            this.llCreateFindingsFromCir.Size = new System.Drawing.Size(121, 13);
            this.llCreateFindingsFromCir.TabIndex = 5;
            this.llCreateFindingsFromCir.TabStop = true;
            this.llCreateFindingsFromCir.Text = "Create Fidings From CIR";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer1.Location = new System.Drawing.Point(6, 20);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.cirDataViewer_ToProcess);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.findingsViewer_ToProcess);
            this.splitContainer1.Size = new System.Drawing.Size(1013, 234);
            this.splitContainer1.SplitterDistance = 496;
            this.splitContainer1.TabIndex = 4;
            // 
            // findingsViewer_ToProcess
            // 
            this.findingsViewer_ToProcess.Dock = System.Windows.Forms.DockStyle.Fill;
            this.findingsViewer_ToProcess.Location = new System.Drawing.Point(0, 0);
            this.findingsViewer_ToProcess.Name = "findingsViewer_ToProcess";
            this.findingsViewer_ToProcess.Size = new System.Drawing.Size(509, 230);
            this.findingsViewer_ToProcess.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(201, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Resources to analyze  (CirData & Fingings)";
            // 
            // cirDataViewer_ToProcess
            // 
            this.cirDataViewer_ToProcess.cirDataAnalysis = ((O2.Kernel.Interfaces.CIR.ICirDataAnalysis)(resources.GetObject("cirDataViewer_ToProcess.cirDataAnalysis")));
            this.cirDataViewer_ToProcess.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cirDataViewer_ToProcess.Location = new System.Drawing.Point(0, 0);
            this.cirDataViewer_ToProcess.Name = "cirDataViewer_ToProcess";
            this.cirDataViewer_ToProcess.Size = new System.Drawing.Size(492, 230);
            this.cirDataViewer_ToProcess.TabIndex = 3;
            // 
            // ascx_FindingsSplitter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.llCreateFindingsFromCir);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.label1);
            this.Name = "ascx_FindingsSplitter";
            this.Size = new System.Drawing.Size(1022, 407);
            this.Load += new System.EventHandler(this.ascx_FindingsSplitter_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.LinkLabel llCreateFindingsFromCir;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private ascx_CirDataViewer cirDataViewer_ToProcess;
        private O2.Views.ASCX.O2Findings.ascx_FindingsViewer findingsViewer_ToProcess;
        private System.Windows.Forms.Label label1;
    }
}
