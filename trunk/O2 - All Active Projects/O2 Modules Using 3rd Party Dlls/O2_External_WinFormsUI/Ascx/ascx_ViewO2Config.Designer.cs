namespace O2.External.WinFormsUI.Ascx
{
    partial class ascx_ViewO2Config
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lbContentsBreakMainO2ConfigSchema = new System.Windows.Forms.Label();
            this.tbMainO2ConfigFile = new System.Windows.Forms.TextBox();
            this.cbManuallyEditO2ConfigFile = new System.Windows.Forms.CheckBox();
            this.llRefreshMainO2ConfigFiles = new System.Windows.Forms.LinkLabel();
            this.webBrowserMainO2ConfigFile = new System.Windows.Forms.WebBrowser();
            this.btRestoreToDefaultValues = new System.Windows.Forms.Button();
            this.btSaveMainO2ConfigFile = new System.Windows.Forms.Button();
            this.lbLocationOfLocalConfigFile = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lbContentsBreakLocalO2ConfigSchema = new System.Windows.Forms.Label();
            this.btFindLocalO2ConfigFile = new System.Windows.Forms.Button();
            this.btCreateLocalO2ConfigFile = new System.Windows.Forms.Button();
            this.tbLocationOfLocalO2ConfigFile = new System.Windows.Forms.TextBox();
            this.btSaveLocalConfigFile = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.tbLocalO2ConfigFile = new System.Windows.Forms.TextBox();
            this.tcO2ConfigFiles = new System.Windows.Forms.TabControl();
            this.tpO2ConfigFilesInDisk = new System.Windows.Forms.TabPage();
            this.tpCurrentO2ConfigValuesInMemory = new System.Windows.Forms.TabPage();
            this.lbDependencyInjectionTestValue = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.llRefreshMemoryViewOfO2ConfigFiles = new System.Windows.Forms.LinkLabel();
            this.webBrowser_O2ConfigFilesInMemory = new System.Windows.Forms.WebBrowser();
            this.label3 = new System.Windows.Forms.Label();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tcO2ConfigFiles.SuspendLayout();
            this.tpO2ConfigFilesInDisk.SuspendLayout();
            this.tpCurrentO2ConfigValuesInMemory.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(3, 3);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.groupBox1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.groupBox2);
            this.splitContainer1.Size = new System.Drawing.Size(990, 556);
            this.splitContainer1.SplitterDistance = 485;
            this.splitContainer1.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lbContentsBreakMainO2ConfigSchema);
            this.groupBox1.Controls.Add(this.tbMainO2ConfigFile);
            this.groupBox1.Controls.Add(this.cbManuallyEditO2ConfigFile);
            this.groupBox1.Controls.Add(this.llRefreshMainO2ConfigFiles);
            this.groupBox1.Controls.Add(this.webBrowserMainO2ConfigFile);
            this.groupBox1.Controls.Add(this.btRestoreToDefaultValues);
            this.groupBox1.Controls.Add(this.btSaveMainO2ConfigFile);
            this.groupBox1.Controls.Add(this.lbLocationOfLocalConfigFile);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(485, 556);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Main O2 Config File (in O2 Execution Dir)";
            // 
            // lbContentsBreakMainO2ConfigSchema
            // 
            this.lbContentsBreakMainO2ConfigSchema.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lbContentsBreakMainO2ConfigSchema.AutoSize = true;
            this.lbContentsBreakMainO2ConfigSchema.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbContentsBreakMainO2ConfigSchema.ForeColor = System.Drawing.Color.Red;
            this.lbContentsBreakMainO2ConfigSchema.Location = new System.Drawing.Point(3, 529);
            this.lbContentsBreakMainO2ConfigSchema.Name = "lbContentsBreakMainO2ConfigSchema";
            this.lbContentsBreakMainO2ConfigSchema.Size = new System.Drawing.Size(203, 13);
            this.lbContentsBreakMainO2ConfigSchema.TabIndex = 9;
            this.lbContentsBreakMainO2ConfigSchema.Text = "Contents Break O2 Config Schema";
            this.lbContentsBreakMainO2ConfigSchema.Visible = false;
            // 
            // tbMainO2ConfigFile
            // 
            this.tbMainO2ConfigFile.AcceptsReturn = true;
            this.tbMainO2ConfigFile.AcceptsTab = true;
            this.tbMainO2ConfigFile.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbMainO2ConfigFile.Location = new System.Drawing.Point(3, 71);
            this.tbMainO2ConfigFile.Multiline = true;
            this.tbMainO2ConfigFile.Name = "tbMainO2ConfigFile";
            this.tbMainO2ConfigFile.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tbMainO2ConfigFile.Size = new System.Drawing.Size(476, 430);
            this.tbMainO2ConfigFile.TabIndex = 8;
            this.tbMainO2ConfigFile.Visible = false;
            this.tbMainO2ConfigFile.WordWrap = false;
            this.tbMainO2ConfigFile.TextChanged += new System.EventHandler(this.tbMainO2ConfigFile_TextChanged);
            // 
            // cbManuallyEditO2ConfigFile
            // 
            this.cbManuallyEditO2ConfigFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cbManuallyEditO2ConfigFile.AutoSize = true;
            this.cbManuallyEditO2ConfigFile.Location = new System.Drawing.Point(4, 507);
            this.cbManuallyEditO2ConfigFile.Name = "cbManuallyEditO2ConfigFile";
            this.cbManuallyEditO2ConfigFile.Size = new System.Drawing.Size(182, 17);
            this.cbManuallyEditO2ConfigFile.TabIndex = 7;
            this.cbManuallyEditO2ConfigFile.Text = "Manually edit main O2 Config File";
            this.cbManuallyEditO2ConfigFile.UseVisualStyleBackColor = true;
            this.cbManuallyEditO2ConfigFile.CheckedChanged += new System.EventHandler(this.cbManuallyEditO2ConfigFile_CheckedChanged);
            // 
            // llRefreshMainO2ConfigFiles
            // 
            this.llRefreshMainO2ConfigFiles.AutoSize = true;
            this.llRefreshMainO2ConfigFiles.Location = new System.Drawing.Point(1, 52);
            this.llRefreshMainO2ConfigFiles.Name = "llRefreshMainO2ConfigFiles";
            this.llRefreshMainO2ConfigFiles.Size = new System.Drawing.Size(39, 13);
            this.llRefreshMainO2ConfigFiles.TabIndex = 6;
            this.llRefreshMainO2ConfigFiles.TabStop = true;
            this.llRefreshMainO2ConfigFiles.Text = "refresh";
            this.llRefreshMainO2ConfigFiles.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llRefreshMainO2ConfigFiles_LinkClicked);
            // 
            // webBrowserMainO2ConfigFile
            // 
            this.webBrowserMainO2ConfigFile.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.webBrowserMainO2ConfigFile.Location = new System.Drawing.Point(4, 71);
            this.webBrowserMainO2ConfigFile.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowserMainO2ConfigFile.Name = "webBrowserMainO2ConfigFile";
            this.webBrowserMainO2ConfigFile.Size = new System.Drawing.Size(475, 430);
            this.webBrowserMainO2ConfigFile.TabIndex = 5;
            // 
            // btRestoreToDefaultValues
            // 
            this.btRestoreToDefaultValues.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btRestoreToDefaultValues.Location = new System.Drawing.Point(336, 42);
            this.btRestoreToDefaultValues.Name = "btRestoreToDefaultValues";
            this.btRestoreToDefaultValues.Size = new System.Drawing.Size(143, 23);
            this.btRestoreToDefaultValues.TabIndex = 4;
            this.btRestoreToDefaultValues.Text = "Restore to default values";
            this.btRestoreToDefaultValues.UseVisualStyleBackColor = true;
            this.btRestoreToDefaultValues.Click += new System.EventHandler(this.btRestoreToDefaultValues_Click);
            // 
            // btSaveMainO2ConfigFile
            // 
            this.btSaveMainO2ConfigFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btSaveMainO2ConfigFile.Location = new System.Drawing.Point(336, 507);
            this.btSaveMainO2ConfigFile.Name = "btSaveMainO2ConfigFile";
            this.btSaveMainO2ConfigFile.Size = new System.Drawing.Size(143, 40);
            this.btSaveMainO2ConfigFile.TabIndex = 3;
            this.btSaveMainO2ConfigFile.Text = "Save main O2 Config File";
            this.btSaveMainO2ConfigFile.UseVisualStyleBackColor = true;
            this.btSaveMainO2ConfigFile.Visible = false;
            this.btSaveMainO2ConfigFile.Click += new System.EventHandler(this.btSaveMainO2ConfigFile_Click);
            // 
            // lbLocationOfLocalConfigFile
            // 
            this.lbLocationOfLocalConfigFile.AutoSize = true;
            this.lbLocationOfLocalConfigFile.Location = new System.Drawing.Point(60, 20);
            this.lbLocationOfLocalConfigFile.Name = "lbLocationOfLocalConfigFile";
            this.lbLocationOfLocalConfigFile.Size = new System.Drawing.Size(16, 13);
            this.lbLocationOfLocalConfigFile.TabIndex = 1;
            this.lbLocationOfLocalConfigFile.Text = "...";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "location:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lbContentsBreakLocalO2ConfigSchema);
            this.groupBox2.Controls.Add(this.btFindLocalO2ConfigFile);
            this.groupBox2.Controls.Add(this.btCreateLocalO2ConfigFile);
            this.groupBox2.Controls.Add(this.tbLocationOfLocalO2ConfigFile);
            this.groupBox2.Controls.Add(this.btSaveLocalConfigFile);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.tbLocalO2ConfigFile);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(501, 556);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "LOCAL O2 Config file (containing user perferences)";
            // 
            // lbContentsBreakLocalO2ConfigSchema
            // 
            this.lbContentsBreakLocalO2ConfigSchema.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lbContentsBreakLocalO2ConfigSchema.AutoSize = true;
            this.lbContentsBreakLocalO2ConfigSchema.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbContentsBreakLocalO2ConfigSchema.ForeColor = System.Drawing.Color.Red;
            this.lbContentsBreakLocalO2ConfigSchema.Location = new System.Drawing.Point(9, 528);
            this.lbContentsBreakLocalO2ConfigSchema.Name = "lbContentsBreakLocalO2ConfigSchema";
            this.lbContentsBreakLocalO2ConfigSchema.Size = new System.Drawing.Size(203, 13);
            this.lbContentsBreakLocalO2ConfigSchema.TabIndex = 8;
            this.lbContentsBreakLocalO2ConfigSchema.Text = "Contents Break O2 Config Schema";
            this.lbContentsBreakLocalO2ConfigSchema.Visible = false;
            // 
            // btFindLocalO2ConfigFile
            // 
            this.btFindLocalO2ConfigFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btFindLocalO2ConfigFile.Location = new System.Drawing.Point(464, 15);
            this.btFindLocalO2ConfigFile.Name = "btFindLocalO2ConfigFile";
            this.btFindLocalO2ConfigFile.Size = new System.Drawing.Size(31, 23);
            this.btFindLocalO2ConfigFile.TabIndex = 7;
            this.btFindLocalO2ConfigFile.Text = "...";
            this.btFindLocalO2ConfigFile.UseVisualStyleBackColor = true;
            // 
            // btCreateLocalO2ConfigFile
            // 
            this.btCreateLocalO2ConfigFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btCreateLocalO2ConfigFile.Location = new System.Drawing.Point(321, 42);
            this.btCreateLocalO2ConfigFile.Name = "btCreateLocalO2ConfigFile";
            this.btCreateLocalO2ConfigFile.Size = new System.Drawing.Size(174, 23);
            this.btCreateLocalO2ConfigFile.TabIndex = 5;
            this.btCreateLocalO2ConfigFile.Text = "Create / Set local O2 Config File";
            this.btCreateLocalO2ConfigFile.UseVisualStyleBackColor = true;
            this.btCreateLocalO2ConfigFile.Click += new System.EventHandler(this.btCreateLocalO2ConfigFile_Click);
            // 
            // tbLocationOfLocalO2ConfigFile
            // 
            this.tbLocationOfLocalO2ConfigFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbLocationOfLocalO2ConfigFile.Location = new System.Drawing.Point(60, 17);
            this.tbLocationOfLocalO2ConfigFile.Name = "tbLocationOfLocalO2ConfigFile";
            this.tbLocationOfLocalO2ConfigFile.Size = new System.Drawing.Size(398, 20);
            this.tbLocationOfLocalO2ConfigFile.TabIndex = 6;
            // 
            // btSaveLocalConfigFile
            // 
            this.btSaveLocalConfigFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btSaveLocalConfigFile.Enabled = false;
            this.btSaveLocalConfigFile.Location = new System.Drawing.Point(352, 507);
            this.btSaveLocalConfigFile.Name = "btSaveLocalConfigFile";
            this.btSaveLocalConfigFile.Size = new System.Drawing.Size(143, 40);
            this.btSaveLocalConfigFile.TabIndex = 5;
            this.btSaveLocalConfigFile.Text = "Save local O2 Config File";
            this.btSaveLocalConfigFile.UseVisualStyleBackColor = true;
            this.btSaveLocalConfigFile.Click += new System.EventHandler(this.btSaveLocalConfigFile_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "location:";
            // 
            // tbLocalO2ConfigFile
            // 
            this.tbLocalO2ConfigFile.AcceptsReturn = true;
            this.tbLocalO2ConfigFile.AcceptsTab = true;
            this.tbLocalO2ConfigFile.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbLocalO2ConfigFile.Location = new System.Drawing.Point(6, 71);
            this.tbLocalO2ConfigFile.Multiline = true;
            this.tbLocalO2ConfigFile.Name = "tbLocalO2ConfigFile";
            this.tbLocalO2ConfigFile.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tbLocalO2ConfigFile.Size = new System.Drawing.Size(489, 430);
            this.tbLocalO2ConfigFile.TabIndex = 4;
            this.tbLocalO2ConfigFile.WordWrap = false;
            this.tbLocalO2ConfigFile.TextChanged += new System.EventHandler(this.tbLocalO2ConfigFile_TextChanged);
            // 
            // tcO2ConfigFiles
            // 
            this.tcO2ConfigFiles.Controls.Add(this.tpO2ConfigFilesInDisk);
            this.tcO2ConfigFiles.Controls.Add(this.tpCurrentO2ConfigValuesInMemory);
            this.tcO2ConfigFiles.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcO2ConfigFiles.Location = new System.Drawing.Point(0, 0);
            this.tcO2ConfigFiles.Name = "tcO2ConfigFiles";
            this.tcO2ConfigFiles.SelectedIndex = 0;
            this.tcO2ConfigFiles.Size = new System.Drawing.Size(1004, 588);
            this.tcO2ConfigFiles.TabIndex = 1;
            this.tcO2ConfigFiles.SelectedIndexChanged += new System.EventHandler(this.tcO2ConfigFiles_SelectedIndexChanged);
            // 
            // tpO2ConfigFilesInDisk
            // 
            this.tpO2ConfigFilesInDisk.Controls.Add(this.splitContainer1);
            this.tpO2ConfigFilesInDisk.Location = new System.Drawing.Point(4, 22);
            this.tpO2ConfigFilesInDisk.Name = "tpO2ConfigFilesInDisk";
            this.tpO2ConfigFilesInDisk.Padding = new System.Windows.Forms.Padding(3);
            this.tpO2ConfigFilesInDisk.Size = new System.Drawing.Size(996, 562);
            this.tpO2ConfigFilesInDisk.TabIndex = 0;
            this.tpO2ConfigFilesInDisk.Text = "O2 Config Files (in Disk)";
            this.tpO2ConfigFilesInDisk.UseVisualStyleBackColor = true;
            // 
            // tpCurrentO2ConfigValuesInMemory
            // 
            this.tpCurrentO2ConfigValuesInMemory.Controls.Add(this.lbDependencyInjectionTestValue);
            this.tpCurrentO2ConfigValuesInMemory.Controls.Add(this.label4);
            this.tpCurrentO2ConfigValuesInMemory.Controls.Add(this.llRefreshMemoryViewOfO2ConfigFiles);
            this.tpCurrentO2ConfigValuesInMemory.Controls.Add(this.webBrowser_O2ConfigFilesInMemory);
            this.tpCurrentO2ConfigValuesInMemory.Controls.Add(this.label3);
            this.tpCurrentO2ConfigValuesInMemory.Location = new System.Drawing.Point(4, 22);
            this.tpCurrentO2ConfigValuesInMemory.Name = "tpCurrentO2ConfigValuesInMemory";
            this.tpCurrentO2ConfigValuesInMemory.Padding = new System.Windows.Forms.Padding(3);
            this.tpCurrentO2ConfigValuesInMemory.Size = new System.Drawing.Size(996, 562);
            this.tpCurrentO2ConfigValuesInMemory.TabIndex = 1;
            this.tpCurrentO2ConfigValuesInMemory.Text = "Current O2 Config values (in Memory)";
            this.tpCurrentO2ConfigValuesInMemory.UseVisualStyleBackColor = true;
            // 
            // lbDependencyInjectionTestValue
            // 
            this.lbDependencyInjectionTestValue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lbDependencyInjectionTestValue.AutoSize = true;
            this.lbDependencyInjectionTestValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbDependencyInjectionTestValue.Location = new System.Drawing.Point(416, 542);
            this.lbDependencyInjectionTestValue.Name = "lbDependencyInjectionTestValue";
            this.lbDependencyInjectionTestValue.Size = new System.Drawing.Size(19, 13);
            this.lbDependencyInjectionTestValue.TabIndex = 4;
            this.lbDependencyInjectionTestValue.Text = "...";
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 542);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(396, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Dependency Injection test (i.e. contents of: KO2Config.dependencyInjectionTest): " +
                "";
            // 
            // llRefreshMemoryViewOfO2ConfigFiles
            // 
            this.llRefreshMemoryViewOfO2ConfigFiles.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.llRefreshMemoryViewOfO2ConfigFiles.AutoSize = true;
            this.llRefreshMemoryViewOfO2ConfigFiles.Location = new System.Drawing.Point(951, 7);
            this.llRefreshMemoryViewOfO2ConfigFiles.Name = "llRefreshMemoryViewOfO2ConfigFiles";
            this.llRefreshMemoryViewOfO2ConfigFiles.Size = new System.Drawing.Size(39, 13);
            this.llRefreshMemoryViewOfO2ConfigFiles.TabIndex = 2;
            this.llRefreshMemoryViewOfO2ConfigFiles.TabStop = true;
            this.llRefreshMemoryViewOfO2ConfigFiles.Text = "refresh";
            this.llRefreshMemoryViewOfO2ConfigFiles.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llRefreshMemoryViewOfO2ConfigFiles_LinkClicked);
            // 
            // webBrowser_O2ConfigFilesInMemory
            // 
            this.webBrowser_O2ConfigFilesInMemory.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.webBrowser_O2ConfigFilesInMemory.Location = new System.Drawing.Point(7, 24);
            this.webBrowser_O2ConfigFilesInMemory.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser_O2ConfigFilesInMemory.Name = "webBrowser_O2ConfigFilesInMemory";
            this.webBrowser_O2ConfigFilesInMemory.Size = new System.Drawing.Size(983, 515);
            this.webBrowser_O2ConfigFilesInMemory.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(4, 7);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(513, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "This represents (a serialized view of) the current O2 Config values in memory (af" +
                "ter merging of all config files)";
            // 
            // ascx_ViewO2Config
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tcO2ConfigFiles);
            this.Name = "ascx_ViewO2Config";
            this.Size = new System.Drawing.Size(1004, 588);
            this.Load += new System.EventHandler(this.ascx_ViewO2Config_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.tcO2ConfigFiles.ResumeLayout(false);
            this.tpO2ConfigFilesInDisk.ResumeLayout(false);
            this.tpCurrentO2ConfigValuesInMemory.ResumeLayout(false);
            this.tpCurrentO2ConfigValuesInMemory.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btSaveMainO2ConfigFile;
        private System.Windows.Forms.Label lbLocationOfLocalConfigFile;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btSaveLocalConfigFile;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbLocalO2ConfigFile;
        private System.Windows.Forms.Button btRestoreToDefaultValues;
        private System.Windows.Forms.Button btFindLocalO2ConfigFile;
        private System.Windows.Forms.Button btCreateLocalO2ConfigFile;
        private System.Windows.Forms.TextBox tbLocationOfLocalO2ConfigFile;
        private System.Windows.Forms.WebBrowser webBrowserMainO2ConfigFile;
        private System.Windows.Forms.LinkLabel llRefreshMainO2ConfigFiles;
        private System.Windows.Forms.CheckBox cbManuallyEditO2ConfigFile;
        private System.Windows.Forms.TextBox tbMainO2ConfigFile;
        private System.Windows.Forms.TabControl tcO2ConfigFiles;
        private System.Windows.Forms.TabPage tpO2ConfigFilesInDisk;
        private System.Windows.Forms.TabPage tpCurrentO2ConfigValuesInMemory;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.LinkLabel llRefreshMemoryViewOfO2ConfigFiles;
        private System.Windows.Forms.WebBrowser webBrowser_O2ConfigFilesInMemory;
        private System.Windows.Forms.Label lbContentsBreakLocalO2ConfigSchema;
        private System.Windows.Forms.Label lbContentsBreakMainO2ConfigSchema;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lbDependencyInjectionTestValue;
    }
}
