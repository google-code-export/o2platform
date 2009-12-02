// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)

using O2.Views.ASCX.O2Findings;

namespace O2.Tool.FilterAssessmentFiles.Ascx
{
    partial class ascx_ViewAssessmentFile
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
            this.ascx_FindingsViewer1 = new ascx_FindingsViewer();
            this.ascx_FindingEditor1 = new ascx_FindingEditor();
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
            this.splitContainer1.Panel1.Controls.Add(this.ascx_FindingsViewer1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.ascx_FindingEditor1);
            this.splitContainer1.Size = new System.Drawing.Size(802, 483);
            this.splitContainer1.SplitterDistance = 325;
            this.splitContainer1.TabIndex = 0;
            // 
            // ascx_FindingsViewer1
            // 
            this.ascx_FindingsViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ascx_FindingsViewer1.Location = new System.Drawing.Point(0, 0);
            this.ascx_FindingsViewer1.Name = "ascx_FindingsViewer1";
            this.ascx_FindingsViewer1.Size = new System.Drawing.Size(321, 479);
            this.ascx_FindingsViewer1.TabIndex = 0;
            // 
            // ascx_FindingEditor1
            // 
            this.ascx_FindingEditor1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ascx_FindingEditor1.Location = new System.Drawing.Point(0, 0);
            this.ascx_FindingEditor1.Name = "ascx_FindingEditor1";
            this.ascx_FindingEditor1.Size = new System.Drawing.Size(469, 479);
            this.ascx_FindingEditor1.TabIndex = 0;
            // 
            // ascx_ViewAssessmentFile
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Name = "ascx_ViewAssessmentFile";
            this.Size = new System.Drawing.Size(802, 483);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private ascx_FindingsViewer ascx_FindingsViewer1;
        private ascx_FindingEditor ascx_FindingEditor1;
    }
}
