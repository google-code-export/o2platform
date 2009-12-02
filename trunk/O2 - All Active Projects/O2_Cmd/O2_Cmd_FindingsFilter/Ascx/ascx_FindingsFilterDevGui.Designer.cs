using O2.External.SharpDevelop.Ascx;

namespace O2.Cmd.FindingsFilter.Ascx
{
    partial class ascx_FindingsFilterDevGui
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
            this.sourceCodeEditor = new ascx_SourceCodeEditor();
            this.findingsFilter = new O2.Cmd.FindingsFilter.Ascx.ascx_FindingsFilter();
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
            this.splitContainer1.Panel1.Controls.Add(this.sourceCodeEditor);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.findingsFilter);
            this.splitContainer1.Size = new System.Drawing.Size(832, 475);
            this.splitContainer1.SplitterDistance = 355;
            this.splitContainer1.TabIndex = 0;
            // 
            // sourceCodeEditor
            // 
            this.sourceCodeEditor.AllowDrop = true;
            this.sourceCodeEditor.BackColor = System.Drawing.SystemColors.Control;
            this.sourceCodeEditor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sourceCodeEditor.ForeColor = System.Drawing.Color.Black;
            this.sourceCodeEditor.Location = new System.Drawing.Point(0, 0);
            this.sourceCodeEditor.Name = "sourceCodeEditor";
            this.sourceCodeEditor.Size = new System.Drawing.Size(351, 471);
            this.sourceCodeEditor.TabIndex = 0;
            // 
            // findingsFilter
            // 
            this.findingsFilter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.findingsFilter.Location = new System.Drawing.Point(0, 0);
            this.findingsFilter.Name = "findingsFilter";
            this.findingsFilter.Size = new System.Drawing.Size(469, 471);
            this.findingsFilter.TabIndex = 0;
            // 
            // ascx_FindingsFilterDevGui
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Name = "ascx_FindingsFilterDevGui";
            this.Size = new System.Drawing.Size(832, 475);
            this.Load += new System.EventHandler(this.ascx_FindingsFilterDevGui_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private ascx_SourceCodeEditor sourceCodeEditor;
        private ascx_FindingsFilter findingsFilter;
    }
}
