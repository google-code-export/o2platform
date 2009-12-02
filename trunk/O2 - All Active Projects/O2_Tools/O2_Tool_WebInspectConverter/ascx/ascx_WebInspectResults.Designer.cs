// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using O2.Views.ASCX.DataViewers;

namespace O2.Tool.WebInspectConverter.ascx
{
    partial class ascx_WebInspectResults
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
            this.lbLoadedFiles = new System.Windows.Forms.ListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.llClearResults = new System.Windows.Forms.LinkLabel();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.tableListWithWebInspectFindings = new ascx_TableList();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                                                                                 | System.Windows.Forms.AnchorStyles.Left)
                                                                                | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer1.Location = new System.Drawing.Point(3, 28);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.lbLoadedFiles);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.label2);
            this.splitContainer1.Panel2.Controls.Add(this.tableListWithWebInspectFindings);
            this.splitContainer1.Size = new System.Drawing.Size(625, 203);
            this.splitContainer1.SplitterDistance = 208;
            this.splitContainer1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Loaded Files";
            // 
            // lbLoadedFiles
            // 
            this.lbLoadedFiles.AllowDrop = true;
            this.lbLoadedFiles.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                                                                               | System.Windows.Forms.AnchorStyles.Left)
                                                                              | System.Windows.Forms.AnchorStyles.Right)));
            this.lbLoadedFiles.FormattingEnabled = true;
            this.lbLoadedFiles.Location = new System.Drawing.Point(7, 21);
            this.lbLoadedFiles.Name = "lbLoadedFiles";
            this.lbLoadedFiles.Size = new System.Drawing.Size(194, 173);
            this.lbLoadedFiles.TabIndex = 1;
            this.lbLoadedFiles.DragDrop += new System.Windows.Forms.DragEventHandler(this.lbLoadedFiles_DragDrop);
            this.lbLoadedFiles.DragEnter += new System.Windows.Forms.DragEventHandler(this.lbLoadedFiles_DragEnter);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 5);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(110, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Web Inspect Findings";
            // 
            // llClearResults
            // 
            this.llClearResults.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.llClearResults.AutoSize = true;
            this.llClearResults.Location = new System.Drawing.Point(565, 12);
            this.llClearResults.Name = "llClearResults";
            this.llClearResults.Size = new System.Drawing.Size(63, 13);
            this.llClearResults.TabIndex = 1;
            this.llClearResults.TabStop = true;
            this.llClearResults.Text = "clear results";
            this.llClearResults.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llClearResults_LinkClicked);
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Location = new System.Drawing.Point(9, 12);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(259, 13);
            this.linkLabel1.TabIndex = 2;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "WEB INSPECT results (Drag into Ozasmt transformer)";
            this.linkLabel1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.linkLabel1_MouseDown);
            // 
            // tableListWithWebInspectFindings
            // 
            this.tableListWithWebInspectFindings.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                                                                                                 | System.Windows.Forms.AnchorStyles.Left)
                                                                                                | System.Windows.Forms.AnchorStyles.Right)));
            this.tableListWithWebInspectFindings.Location = new System.Drawing.Point(3, 21);
            this.tableListWithWebInspectFindings.Name = "tableListWithWebInspectFindings";
            this.tableListWithWebInspectFindings.Size = new System.Drawing.Size(403, 175);
            this.tableListWithWebInspectFindings.TabIndex = 0;
            // 
            // ascx_WebInspectResults
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.llClearResults);
            this.Controls.Add(this.splitContainer1);
            this.Name = "ascx_WebInspectResults";
            this.Size = new System.Drawing.Size(631, 234);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox lbLoadedFiles;
        private System.Windows.Forms.Label label2;
        private ascx_TableList tableListWithWebInspectFindings;
        private System.Windows.Forms.LinkLabel llClearResults;
        private System.Windows.Forms.LinkLabel linkLabel1;
    }
}
