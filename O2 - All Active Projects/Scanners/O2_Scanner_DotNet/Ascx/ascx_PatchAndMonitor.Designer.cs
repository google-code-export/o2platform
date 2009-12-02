namespace O2.Scanner.DotNet.Ascx
{
    partial class ascx_PatchAndMonitor
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ascx_PatchAndMonitor));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.sourceCodeEditor = new O2.External.SharpDevelop.Ascx.ascx_SourceCodeEditor();
            this.cirDataViewer = new O2.Core.CIR.Ascx.ascx_CirDataViewer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.llHookedAssembly = new System.Windows.Forms.LinkLabel();
            this.llTestCodeAssembly = new System.Windows.Forms.LinkLabel();
            this.btInstallHookIntoNewAssembly = new System.Windows.Forms.Button();
            this.ascx_LogViewer1 = new O2.External.WinFormsUI.Ascx.ascx_LogViewer();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
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
            this.splitContainer1.Panel1.Controls.Add(this.splitContainer3);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(670, 370);
            this.splitContainer1.SplitterDistance = 328;
            this.splitContainer1.TabIndex = 0;
            // 
            // splitContainer3
            // 
            this.splitContainer3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer3.Location = new System.Drawing.Point(0, 0);
            this.splitContainer3.Name = "splitContainer3";
            this.splitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.sourceCodeEditor);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.cirDataViewer);
            this.splitContainer3.Size = new System.Drawing.Size(328, 370);
            this.splitContainer3.SplitterDistance = 184;
            this.splitContainer3.TabIndex = 1;
            // 
            // sourceCodeEditor
            // 
            this.sourceCodeEditor.AllowDrop = true;
            this.sourceCodeEditor.BackColor = System.Drawing.SystemColors.Control;
            this.sourceCodeEditor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sourceCodeEditor.ForeColor = System.Drawing.Color.Black;
            this.sourceCodeEditor.Location = new System.Drawing.Point(0, 0);
            this.sourceCodeEditor.Name = "sourceCodeEditor";
            this.sourceCodeEditor.Size = new System.Drawing.Size(324, 180);
            this.sourceCodeEditor.TabIndex = 0;
            // 
            // cirDataViewer
            // 
            this.cirDataViewer.cirDataAnalysis = ((O2.Kernel.Interfaces.CIR.ICirDataAnalysis)(resources.GetObject("cirDataViewer.cirDataAnalysis")));
            this.cirDataViewer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cirDataViewer.Location = new System.Drawing.Point(0, 0);
            this.cirDataViewer.Name = "cirDataViewer";
            this.cirDataViewer.Size = new System.Drawing.Size(324, 178);
            this.cirDataViewer.TabIndex = 0;
            // 
            // splitContainer2
            // 
            this.splitContainer2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.llHookedAssembly);
            this.splitContainer2.Panel1.Controls.Add(this.llTestCodeAssembly);
            this.splitContainer2.Panel1.Controls.Add(this.btInstallHookIntoNewAssembly);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.ascx_LogViewer1);
            this.splitContainer2.Size = new System.Drawing.Size(338, 370);
            this.splitContainer2.SplitterDistance = 181;
            this.splitContainer2.TabIndex = 0;
            // 
            // llHookedAssembly
            // 
            this.llHookedAssembly.AutoSize = true;
            this.llHookedAssembly.Enabled = false;
            this.llHookedAssembly.Location = new System.Drawing.Point(4, 72);
            this.llHookedAssembly.Name = "llHookedAssembly";
            this.llHookedAssembly.Size = new System.Drawing.Size(92, 13);
            this.llHookedAssembly.TabIndex = 2;
            this.llHookedAssembly.TabStop = true;
            this.llHookedAssembly.Text = "Hooked Assembly";
            this.llHookedAssembly.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llHookedAssembly_LinkClicked);
            this.llHookedAssembly.MouseDown += new System.Windows.Forms.MouseEventHandler(this.llHookedAssembly_MouseDown);
            // 
            // llTestCodeAssembly
            // 
            this.llTestCodeAssembly.AutoSize = true;
            this.llTestCodeAssembly.Location = new System.Drawing.Point(4, 43);
            this.llTestCodeAssembly.Name = "llTestCodeAssembly";
            this.llTestCodeAssembly.Size = new System.Drawing.Size(103, 13);
            this.llTestCodeAssembly.TabIndex = 1;
            this.llTestCodeAssembly.TabStop = true;
            this.llTestCodeAssembly.Text = "Test Code Assembly";
            this.llTestCodeAssembly.MouseDown += new System.Windows.Forms.MouseEventHandler(this.llTestCodeAssembly_MouseDown);
            // 
            // btInstallHookIntoNewAssembly
            // 
            this.btInstallHookIntoNewAssembly.Location = new System.Drawing.Point(3, 3);
            this.btInstallHookIntoNewAssembly.Name = "btInstallHookIntoNewAssembly";
            this.btInstallHookIntoNewAssembly.Size = new System.Drawing.Size(172, 23);
            this.btInstallHookIntoNewAssembly.TabIndex = 0;
            this.btInstallHookIntoNewAssembly.Text = "Insert Hooks Into New Assembly";
            this.btInstallHookIntoNewAssembly.UseVisualStyleBackColor = true;
            this.btInstallHookIntoNewAssembly.Click += new System.EventHandler(this.btInstallHookIntoNewAssembly_Click);
            // 
            // ascx_LogViewer1
            // 
            this.ascx_LogViewer1.BackColor = System.Drawing.Color.Gainsboro;
            this.ascx_LogViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ascx_LogViewer1.Location = new System.Drawing.Point(0, 0);
            this.ascx_LogViewer1.Name = "ascx_LogViewer1";
            this.ascx_LogViewer1.Size = new System.Drawing.Size(334, 181);
            this.ascx_LogViewer1.TabIndex = 0;
            // 
            // ascx_PatchAndMonitor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Name = "ascx_PatchAndMonitor";
            this.Size = new System.Drawing.Size(670, 370);
            this.Load += new System.EventHandler(this.ascx_PatchAndMonitor_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel2.ResumeLayout(false);
            this.splitContainer3.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel1.PerformLayout();
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private O2.Core.CIR.Ascx.ascx_CirDataViewer cirDataViewer;
        private O2.External.SharpDevelop.Ascx.ascx_SourceCodeEditor sourceCodeEditor;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private System.Windows.Forms.Button btInstallHookIntoNewAssembly;
        private System.Windows.Forms.LinkLabel llHookedAssembly;
        private System.Windows.Forms.LinkLabel llTestCodeAssembly;
        private O2.External.WinFormsUI.Ascx.ascx_LogViewer ascx_LogViewer1;
    }
}
