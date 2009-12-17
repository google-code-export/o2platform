// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
namespace O2.Core.CIR.Ascx.DotNet
{
    partial class ascx_GAC_Browser
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tpGacContents = new System.Windows.Forms.TabPage();
            this.label5 = new System.Windows.Forms.Label();
            this.tbGacAssemblyFilter = new System.Windows.Forms.TextBox();
            this.tvListOfGacAssemblies = new System.Windows.Forms.TreeView();
            this.tpConfig = new System.Windows.Forms.TabPage();
            this.gbBackupGAC = new System.Windows.Forms.GroupBox();
            this.llBackUpGAC = new System.Windows.Forms.LinkLabel();
            this.directory_ToBackupGAC = new O2.Views.ASCX.CoreControls.ascx_Directory();
            this.tabControl1.SuspendLayout();
            this.tpGacContents.SuspendLayout();
            this.tpConfig.SuspendLayout();
            this.gbBackupGAC.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Alignment = System.Windows.Forms.TabAlignment.Bottom;
            this.tabControl1.Controls.Add(this.tpGacContents);
            this.tabControl1.Controls.Add(this.tpConfig);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(241, 318);
            this.tabControl1.TabIndex = 0;
            // 
            // tpGacContents
            // 
            this.tpGacContents.Controls.Add(this.label5);
            this.tpGacContents.Controls.Add(this.tbGacAssemblyFilter);
            this.tpGacContents.Controls.Add(this.tvListOfGacAssemblies);
            this.tpGacContents.Location = new System.Drawing.Point(4, 4);
            this.tpGacContents.Name = "tpGacContents";
            this.tpGacContents.Padding = new System.Windows.Forms.Padding(3);
            this.tpGacContents.Size = new System.Drawing.Size(233, 292);
            this.tpGacContents.TabIndex = 0;
            this.tpGacContents.Text = "GAC Contents";
            this.tpGacContents.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 6);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(29, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "Filter";
            this.label5.Click += new System.EventHandler(this.label5_Click);
            // 
            // tbGacAssemblyFilter
            // 
            this.tbGacAssemblyFilter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbGacAssemblyFilter.Location = new System.Drawing.Point(33, 3);
            this.tbGacAssemblyFilter.Name = "tbGacAssemblyFilter";
            this.tbGacAssemblyFilter.Size = new System.Drawing.Size(201, 20);
            this.tbGacAssemblyFilter.TabIndex = 1;
            this.tbGacAssemblyFilter.TextChanged += new System.EventHandler(this.tbGacAssemblyFilter_TextChanged);
            this.tbGacAssemblyFilter.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbGacAssemblyFilter_KeyDown);
            // 
            // tvListOfGacAssemblies
            // 
            this.tvListOfGacAssemblies.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tvListOfGacAssemblies.Location = new System.Drawing.Point(0, 23);
            this.tvListOfGacAssemblies.Name = "tvListOfGacAssemblies";
            this.tvListOfGacAssemblies.Size = new System.Drawing.Size(234, 263);
            this.tvListOfGacAssemblies.TabIndex = 0;
            this.tvListOfGacAssemblies.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvListOfGacAssemblies_AfterSelect);
            // 
            // tpConfig
            // 
            this.tpConfig.Controls.Add(this.gbBackupGAC);
            this.tpConfig.Location = new System.Drawing.Point(4, 4);
            this.tpConfig.Name = "tpConfig";
            this.tpConfig.Padding = new System.Windows.Forms.Padding(3);
            this.tpConfig.Size = new System.Drawing.Size(233, 292);
            this.tpConfig.TabIndex = 1;
            this.tpConfig.Text = "config";
            this.tpConfig.UseVisualStyleBackColor = true;
            // 
            // gbBackupGAC
            // 
            this.gbBackupGAC.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gbBackupGAC.Controls.Add(this.llBackUpGAC);
            this.gbBackupGAC.Controls.Add(this.directory_ToBackupGAC);
            this.gbBackupGAC.Location = new System.Drawing.Point(7, 7);
            this.gbBackupGAC.Name = "gbBackupGAC";
            this.gbBackupGAC.Size = new System.Drawing.Size(220, 175);
            this.gbBackupGAC.TabIndex = 0;
            this.gbBackupGAC.TabStop = false;
            this.gbBackupGAC.Text = "Backup GAC Dlls";
            // 
            // llBackUpGAC
            // 
            this.llBackUpGAC.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.llBackUpGAC.AutoSize = true;
            this.llBackUpGAC.Location = new System.Drawing.Point(50, 155);
            this.llBackUpGAC.Name = "llBackUpGAC";
            this.llBackUpGAC.Size = new System.Drawing.Size(127, 13);
            this.llBackUpGAC.TabIndex = 2;
            this.llBackUpGAC.TabStop = true;
            this.llBackUpGAC.Text = "Backup GAC (into zip file)";
            this.llBackUpGAC.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llBackUpGAC_LinkClicked);
            // 
            // directory_ToBackupGAC
            // 
            this.directory_ToBackupGAC._FileFilter = "*.*";
            this.directory_ToBackupGAC._HandleDrop = true;
            this.directory_ToBackupGAC._HideFiles = false;
            this.directory_ToBackupGAC._ProcessDroppedObjects = true;
            this.directory_ToBackupGAC._ShowFileContentsOnTopTip = false;
            this.directory_ToBackupGAC._ShowFileSize = false;
            this.directory_ToBackupGAC._ShowLinkToUpperFolder = true;
            this.directory_ToBackupGAC._ViewMode = O2.Views.ASCX.CoreControls.ascx_Directory.ViewMode.Simple_With_LocationBar;
            this.directory_ToBackupGAC._WatchFolder = false;
            this.directory_ToBackupGAC.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.directory_ToBackupGAC.BackColor = System.Drawing.SystemColors.Control;
            this.directory_ToBackupGAC.ForeColor = System.Drawing.Color.Black;
            this.directory_ToBackupGAC.Location = new System.Drawing.Point(7, 20);
            this.directory_ToBackupGAC.Name = "directory_ToBackupGAC";
            this.directory_ToBackupGAC.Size = new System.Drawing.Size(207, 136);
            this.directory_ToBackupGAC.TabIndex = 1;
            // 
            // ascx_GAC_Browser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabControl1);
            this.Name = "ascx_GAC_Browser";
            this.Size = new System.Drawing.Size(241, 318);
            this.tabControl1.ResumeLayout(false);
            this.tpGacContents.ResumeLayout(false);
            this.tpGacContents.PerformLayout();
            this.tpConfig.ResumeLayout(false);
            this.gbBackupGAC.ResumeLayout(false);
            this.gbBackupGAC.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tpGacContents;
        private System.Windows.Forms.TabPage tpConfig;
        private System.Windows.Forms.GroupBox gbBackupGAC;
        private O2.Views.ASCX.CoreControls.ascx_Directory directory_ToBackupGAC;
        private System.Windows.Forms.LinkLabel llBackUpGAC;
        private System.Windows.Forms.TreeView tvListOfGacAssemblies;
        private System.Windows.Forms.TextBox tbGacAssemblyFilter;
        private System.Windows.Forms.Label label5;
    }
}
