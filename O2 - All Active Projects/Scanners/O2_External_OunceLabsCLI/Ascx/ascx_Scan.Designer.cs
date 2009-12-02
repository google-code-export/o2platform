using O2.Kernel.CodeUtils;
using O2.Views.ASCX.CoreControls;

namespace O2.Scanner.OunceLabsCLI.Ascx
{
    partial class ascx_Scan
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
            this.lbStatus = new System.Windows.Forms.Label();
            this.lbScanLog = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lbScanTargets = new System.Windows.Forms.ListBox();
            this.btScan = new System.Windows.Forms.Button();
            this.llClearScanTargets = new System.Windows.Forms.LinkLabel();
            this.llDeleteAddFilesInScanResultsFolder = new System.Windows.Forms.LinkLabel();
            this.targetDirectory = new O2.Views.ASCX.CoreControls.ascx_Directory();
            this.adoScanDropArea = new O2.Views.ASCX.CoreControls.ascx_DropObject();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.btCreateCir = new System.Windows.Forms.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.llScanResults_seeAllFiles = new System.Windows.Forms.LinkLabel();
            this.llScanResults_seeCirData = new System.Windows.Forms.LinkLabel();
            this.llScanResults_seeOzasmt = new System.Windows.Forms.LinkLabel();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.cbStoreControlFlowBlockRawDataInsideCirDataFile = new System.Windows.Forms.CheckBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.cbAutoScanOnFileDrop = new System.Windows.Forms.CheckBox();
            this.cbOnFolderDropSearchRecursively = new System.Windows.Forms.CheckBox();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbStatus
            // 
            this.lbStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lbStatus.AutoSize = true;
            this.lbStatus.Location = new System.Drawing.Point(6, 292);
            this.lbStatus.Name = "lbStatus";
            this.lbStatus.Size = new System.Drawing.Size(52, 13);
            this.lbStatus.TabIndex = 25;
            this.lbStatus.Text = "Scan Idle";
            // 
            // lbScanLog
            // 
            this.lbScanLog.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lbScanLog.Location = new System.Drawing.Point(6, 308);
            this.lbScanLog.Name = "lbScanLog";
            this.lbScanLog.Size = new System.Drawing.Size(256, 31);
            this.lbScanLog.TabIndex = 26;
            this.lbScanLog.Text = "....";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(3, 64);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 13);
            this.label1.TabIndex = 27;
            this.label1.Text = "Scan Targets";
            // 
            // lbScanTargets
            // 
            this.lbScanTargets.AllowDrop = true;
            this.lbScanTargets.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lbScanTargets.FormattingEnabled = true;
            this.lbScanTargets.Location = new System.Drawing.Point(6, 85);
            this.lbScanTargets.Name = "lbScanTargets";
            this.lbScanTargets.Size = new System.Drawing.Size(259, 173);
            this.lbScanTargets.TabIndex = 28;
            this.lbScanTargets.DragDrop += new System.Windows.Forms.DragEventHandler(this.lbScanTargets_DragDrop);
            this.lbScanTargets.DragEnter += new System.Windows.Forms.DragEventHandler(this.lbScanTargets_DragEnter);
            // 
            // btScan
            // 
            this.btScan.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.btScan.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btScan.Location = new System.Drawing.Point(117, 264);
            this.btScan.Name = "btScan";
            this.btScan.Size = new System.Drawing.Size(145, 25);
            this.btScan.TabIndex = 30;
            this.btScan.Text = "scan";
            this.btScan.UseVisualStyleBackColor = true;
            this.btScan.Click += new System.EventHandler(this.btScan_Click);
            // 
            // llClearScanTargets
            // 
            this.llClearScanTargets.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.llClearScanTargets.AutoSize = true;
            this.llClearScanTargets.Location = new System.Drawing.Point(236, 64);
            this.llClearScanTargets.Name = "llClearScanTargets";
            this.llClearScanTargets.Size = new System.Drawing.Size(30, 13);
            this.llClearScanTargets.TabIndex = 31;
            this.llClearScanTargets.TabStop = true;
            this.llClearScanTargets.Text = "clear";
            this.llClearScanTargets.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llClearScanTargets_LinkClicked);
            // 
            // llDeleteAddFilesInScanResultsFolder
            // 
            this.llDeleteAddFilesInScanResultsFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.llDeleteAddFilesInScanResultsFolder.AutoSize = true;
            this.llDeleteAddFilesInScanResultsFolder.Location = new System.Drawing.Point(189, 321);
            this.llDeleteAddFilesInScanResultsFolder.Name = "llDeleteAddFilesInScanResultsFolder";
            this.llDeleteAddFilesInScanResultsFolder.Size = new System.Drawing.Size(70, 13);
            this.llDeleteAddFilesInScanResultsFolder.TabIndex = 33;
            this.llDeleteAddFilesInScanResultsFolder.TabStop = true;
            this.llDeleteAddFilesInScanResultsFolder.Text = "delete all files";
            this.llDeleteAddFilesInScanResultsFolder.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llDeleteAddFilesInScanResultsFolder_LinkClicked);
            // 
            // targetDirectory
            // 
            this.targetDirectory._ProcessDroppedObjects = false;
            this.targetDirectory._ShowFileSize = false;
            this.targetDirectory._ShowLinkToUpperFolder = true;
            this.targetDirectory._ViewMode = O2.Views.ASCX.CoreControls.ascx_Directory.ViewMode.Simple_With_LocationBar;
            this.targetDirectory._WatchFolder = true;
            this.targetDirectory.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.targetDirectory.BackColor = System.Drawing.SystemColors.Control;
            this.targetDirectory.ForeColor = System.Drawing.Color.Black;
            this.targetDirectory.Location = new System.Drawing.Point(0, 25);
            this.targetDirectory.Name = "targetDirectory";
            this.targetDirectory.Size = new System.Drawing.Size(265, 293);
            this.targetDirectory.TabIndex = 24;
            // 
            // adoScanDropArea
            // 
            this.adoScanDropArea.AllowDrop = true;
            this.adoScanDropArea.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.adoScanDropArea.BackColor = System.Drawing.Color.Maroon;
            this.adoScanDropArea.ForeColor = System.Drawing.Color.White;
            this.adoScanDropArea.Location = new System.Drawing.Point(3, 3);
            this.adoScanDropArea.Name = "adoScanDropArea";
            this.adoScanDropArea.Size = new System.Drawing.Size(262, 52);
            this.adoScanDropArea.TabIndex = 11;
            this.adoScanDropArea.Text = "Drop Content Here!!";
            this.adoScanDropArea.eDnDAction_ObjectDataReceived_Event += new O2.Kernel.CodeUtils.Callbacks.dMethod_Object(this.adoScanDropArea_eDnDAction_ObjectDataReceived_Event);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(276, 368);
            this.tabControl1.TabIndex = 35;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.btCreateCir);
            this.tabPage1.Controls.Add(this.llClearScanTargets);
            this.tabPage1.Controls.Add(this.lbStatus);
            this.tabPage1.Controls.Add(this.lbScanLog);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.adoScanDropArea);
            this.tabPage1.Controls.Add(this.btScan);
            this.tabPage1.Controls.Add(this.lbScanTargets);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(268, 342);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Load Targets & Run Scans";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // btCreateCir
            // 
            this.btCreateCir.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btCreateCir.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btCreateCir.Location = new System.Drawing.Point(6, 264);
            this.btCreateCir.Name = "btCreateCir";
            this.btCreateCir.Size = new System.Drawing.Size(87, 25);
            this.btCreateCir.TabIndex = 32;
            this.btCreateCir.Text = "create CIR";
            this.btCreateCir.UseVisualStyleBackColor = true;
            this.btCreateCir.Click += new System.EventHandler(this.btCreateCir_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.llScanResults_seeAllFiles);
            this.tabPage2.Controls.Add(this.llScanResults_seeCirData);
            this.tabPage2.Controls.Add(this.llScanResults_seeOzasmt);
            this.tabPage2.Controls.Add(this.targetDirectory);
            this.tabPage2.Controls.Add(this.llDeleteAddFilesInScanResultsFolder);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(268, 342);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Scan Results";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // llScanResults_seeAllFiles
            // 
            this.llScanResults_seeAllFiles.AutoSize = true;
            this.llScanResults_seeAllFiles.Location = new System.Drawing.Point(181, 5);
            this.llScanResults_seeAllFiles.Name = "llScanResults_seeAllFiles";
            this.llScanResults_seeAllFiles.Size = new System.Drawing.Size(58, 13);
            this.llScanResults_seeAllFiles.TabIndex = 36;
            this.llScanResults_seeAllFiles.TabStop = true;
            this.llScanResults_seeAllFiles.Text = "see all files";
            this.llScanResults_seeAllFiles.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llScanResults_seeAllFiles_LinkClicked);
            // 
            // llScanResults_seeCirData
            // 
            this.llScanResults_seeCirData.AutoSize = true;
            this.llScanResults_seeCirData.Location = new System.Drawing.Point(94, 5);
            this.llScanResults_seeCirData.Name = "llScanResults_seeCirData";
            this.llScanResults_seeCirData.Size = new System.Drawing.Size(69, 13);
            this.llScanResults_seeCirData.TabIndex = 35;
            this.llScanResults_seeCirData.TabStop = true;
            this.llScanResults_seeCirData.Text = "see *.CirData";
            this.llScanResults_seeCirData.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llScanResults_seeCirData_LinkClicked);
            // 
            // llScanResults_seeOzasmt
            // 
            this.llScanResults_seeOzasmt.AutoSize = true;
            this.llScanResults_seeOzasmt.Location = new System.Drawing.Point(6, 5);
            this.llScanResults_seeOzasmt.Name = "llScanResults_seeOzasmt";
            this.llScanResults_seeOzasmt.Size = new System.Drawing.Size(67, 13);
            this.llScanResults_seeOzasmt.TabIndex = 34;
            this.llScanResults_seeOzasmt.TabStop = true;
            this.llScanResults_seeOzasmt.Text = "see *.ozasmt";
            this.llScanResults_seeOzasmt.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llScanResults_seeOzasmt_LinkClicked);
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.cbStoreControlFlowBlockRawDataInsideCirDataFile);
            this.tabPage3.Controls.Add(this.checkBox1);
            this.tabPage3.Controls.Add(this.cbAutoScanOnFileDrop);
            this.tabPage3.Controls.Add(this.cbOnFolderDropSearchRecursively);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(268, 342);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Config";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // cbStoreControlFlowBlockRawDataInsideCirDataFile
            // 
            this.cbStoreControlFlowBlockRawDataInsideCirDataFile.AutoSize = true;
            this.cbStoreControlFlowBlockRawDataInsideCirDataFile.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbStoreControlFlowBlockRawDataInsideCirDataFile.Location = new System.Drawing.Point(6, 75);
            this.cbStoreControlFlowBlockRawDataInsideCirDataFile.Name = "cbStoreControlFlowBlockRawDataInsideCirDataFile";
            this.cbStoreControlFlowBlockRawDataInsideCirDataFile.Size = new System.Drawing.Size(268, 17);
            this.cbStoreControlFlowBlockRawDataInsideCirDataFile.TabIndex = 36;
            this.cbStoreControlFlowBlockRawDataInsideCirDataFile.Text = "on CIR creation, store Control Flow Block Raw Data";
            this.cbStoreControlFlowBlockRawDataInsideCirDataFile.UseVisualStyleBackColor = true;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Checked = true;
            this.checkBox1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.checkBox1.ForeColor = System.Drawing.Color.Black;
            this.checkBox1.Location = new System.Drawing.Point(6, 52);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(185, 17);
            this.checkBox1.TabIndex = 35;
            this.checkBox1.Text = "Raise \'new scan completed\' event";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // cbAutoScanOnFileDrop
            // 
            this.cbAutoScanOnFileDrop.AutoSize = true;
            this.cbAutoScanOnFileDrop.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbAutoScanOnFileDrop.ForeColor = System.Drawing.Color.Black;
            this.cbAutoScanOnFileDrop.Location = new System.Drawing.Point(6, 29);
            this.cbAutoScanOnFileDrop.Name = "cbAutoScanOnFileDrop";
            this.cbAutoScanOnFileDrop.Size = new System.Drawing.Size(126, 17);
            this.cbAutoScanOnFileDrop.TabIndex = 34;
            this.cbAutoScanOnFileDrop.Text = "Auto scan on file drop";
            this.cbAutoScanOnFileDrop.UseVisualStyleBackColor = true;
            // 
            // cbOnFolderDropSearchRecursively
            // 
            this.cbOnFolderDropSearchRecursively.AutoSize = true;
            this.cbOnFolderDropSearchRecursively.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbOnFolderDropSearchRecursively.ForeColor = System.Drawing.Color.Black;
            this.cbOnFolderDropSearchRecursively.Location = new System.Drawing.Point(6, 6);
            this.cbOnFolderDropSearchRecursively.Name = "cbOnFolderDropSearchRecursively";
            this.cbOnFolderDropSearchRecursively.Size = new System.Drawing.Size(188, 17);
            this.cbOnFolderDropSearchRecursively.TabIndex = 33;
            this.cbOnFolderDropSearchRecursively.Text = "On Folder Drop search Recursively";
            this.cbOnFolderDropSearchRecursively.UseVisualStyleBackColor = true;
            this.cbOnFolderDropSearchRecursively.CheckedChanged += new System.EventHandler(this.cbOnFolderDropSearchRecursively_CheckedChanged);
            // 
            // ascx_Scan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.tabControl1);
            this.ForeColor = System.Drawing.Color.Black;
            this.Name = "ascx_Scan";
            this.Size = new System.Drawing.Size(276, 368);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private ascx_DropObject adoScanDropArea;
        private ascx_Directory targetDirectory;
        private System.Windows.Forms.Label lbStatus;
        private System.Windows.Forms.Label lbScanLog;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox lbScanTargets;
        private System.Windows.Forms.Button btScan;
        private System.Windows.Forms.LinkLabel llClearScanTargets;
        private System.Windows.Forms.LinkLabel llDeleteAddFilesInScanResultsFolder;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.CheckBox cbOnFolderDropSearchRecursively;
        private System.Windows.Forms.CheckBox cbAutoScanOnFileDrop;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Button btCreateCir;
        private System.Windows.Forms.LinkLabel llScanResults_seeAllFiles;
        private System.Windows.Forms.LinkLabel llScanResults_seeCirData;
        private System.Windows.Forms.LinkLabel llScanResults_seeOzasmt;
        private System.Windows.Forms.CheckBox cbStoreControlFlowBlockRawDataInsideCirDataFile;
    }
}