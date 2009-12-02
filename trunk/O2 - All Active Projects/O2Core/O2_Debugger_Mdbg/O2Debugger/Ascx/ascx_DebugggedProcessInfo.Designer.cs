namespace O2.Debugger.Mdbg.O2Debugger
{
    partial class ascx_DebugggedProcessInfo
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
            this.lbCurrentThreads = new System.Windows.Forms.ListBox();
            this.lbCurrentModules = new System.Windows.Forms.ListBox();
            this.lbCurrentAppDomains = new System.Windows.Forms.ListBox();
            this.lbLoadedAssemblies = new System.Windows.Forms.ListBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.llRefreshDebuggedProcessInformation = new System.Windows.Forms.LinkLabel();
            this.SuspendLayout();
            // 
            // lbCurrentThreads
            // 
            this.lbCurrentThreads.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lbCurrentThreads.FormattingEnabled = true;
            this.lbCurrentThreads.Location = new System.Drawing.Point(315, 216);
            this.lbCurrentThreads.Name = "lbCurrentThreads";
            this.lbCurrentThreads.Size = new System.Drawing.Size(132, 43);
            this.lbCurrentThreads.Sorted = true;
            this.lbCurrentThreads.TabIndex = 30;
            // 
            // lbCurrentModules
            // 
            this.lbCurrentModules.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lbCurrentModules.FormattingEnabled = true;
            this.lbCurrentModules.Location = new System.Drawing.Point(3, 19);
            this.lbCurrentModules.Name = "lbCurrentModules";
            this.lbCurrentModules.Size = new System.Drawing.Size(444, 173);
            this.lbCurrentModules.Sorted = true;
            this.lbCurrentModules.TabIndex = 32;
            this.lbCurrentModules.SelectedIndexChanged += new System.EventHandler(this.lbCurrentModules_SelectedIndexChanged);
            // 
            // lbCurrentAppDomains
            // 
            this.lbCurrentAppDomains.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lbCurrentAppDomains.FormattingEnabled = true;
            this.lbCurrentAppDomains.Location = new System.Drawing.Point(6, 216);
            this.lbCurrentAppDomains.Name = "lbCurrentAppDomains";
            this.lbCurrentAppDomains.Size = new System.Drawing.Size(148, 43);
            this.lbCurrentAppDomains.Sorted = true;
            this.lbCurrentAppDomains.TabIndex = 28;
            // 
            // lbLoadedAssemblies
            // 
            this.lbLoadedAssemblies.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lbLoadedAssemblies.FormattingEnabled = true;
            this.lbLoadedAssemblies.Location = new System.Drawing.Point(160, 216);
            this.lbLoadedAssemblies.Name = "lbLoadedAssemblies";
            this.lbLoadedAssemblies.Size = new System.Drawing.Size(135, 43);
            this.lbLoadedAssemblies.Sorted = true;
            this.lbLoadedAssemblies.TabIndex = 31;
            this.lbLoadedAssemblies.SelectedIndexChanged += new System.EventHandler(this.lbLoadedAssemblies_SelectedIndexChanged);
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(312, 199);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(83, 13);
            this.label8.TabIndex = 33;
            this.label8.Text = "Current Threads";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(3, 3);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(84, 13);
            this.label7.TabIndex = 35;
            this.label7.Text = "Current Modules";
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(157, 199);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(98, 13);
            this.label4.TabIndex = 34;
            this.label4.Text = "Loaded Assemblies";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 199);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(104, 13);
            this.label3.TabIndex = 29;
            this.label3.Text = "Current AppDomains";
            // 
            // llRefreshDebuggedProcessInformation
            // 
            this.llRefreshDebuggedProcessInformation.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.llRefreshDebuggedProcessInformation.AutoSize = true;
            this.llRefreshDebuggedProcessInformation.Location = new System.Drawing.Point(380, 3);
            this.llRefreshDebuggedProcessInformation.Name = "llRefreshDebuggedProcessInformation";
            this.llRefreshDebuggedProcessInformation.Size = new System.Drawing.Size(70, 13);
            this.llRefreshDebuggedProcessInformation.TabIndex = 36;
            this.llRefreshDebuggedProcessInformation.TabStop = true;
            this.llRefreshDebuggedProcessInformation.Text = "Refresh Data";
            this.llRefreshDebuggedProcessInformation.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llRefreshDebuggedProcessInformation_LinkClicked);
            // 
            // ascx_DebugggedProcessInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.llRefreshDebuggedProcessInformation);
            this.Controls.Add(this.lbCurrentThreads);
            this.Controls.Add(this.lbCurrentModules);
            this.Controls.Add(this.lbCurrentAppDomains);
            this.Controls.Add(this.lbLoadedAssemblies);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Name = "ascx_DebugggedProcessInfo";
            this.Size = new System.Drawing.Size(450, 262);
            this.Enter += new System.EventHandler(this.ascx_DebugggedProcessInfo_Enter);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox lbCurrentThreads;
        private System.Windows.Forms.ListBox lbCurrentModules;
        private System.Windows.Forms.ListBox lbCurrentAppDomains;
        private System.Windows.Forms.ListBox lbLoadedAssemblies;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.LinkLabel llRefreshDebuggedProcessInformation;
    }
}
