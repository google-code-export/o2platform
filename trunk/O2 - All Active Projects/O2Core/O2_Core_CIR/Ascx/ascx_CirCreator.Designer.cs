// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
namespace O2.Core.CIR.Ascx
{
    partial class ascx_CirCreator
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
            this.label1 = new System.Windows.Forms.Label();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.llDeleteFilesInCirCreationQueue = new System.Windows.Forms.LinkLabel();
            this.label2 = new System.Windows.Forms.Label();
            this.llDeleteFilesInCreatedCirFilesDirectory = new System.Windows.Forms.LinkLabel();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.laCreateCirForDotNetFrameworkAssemblies = new System.Windows.Forms.LinkLabel();
            this.llCreateCirForAllO2Modules = new System.Windows.Forms.LinkLabel();
            this.llCreateCirForCurrentO2Module = new System.Windows.Forms.LinkLabel();
            this.llHideTaskControlHost = new System.Windows.Forms.LinkLabel();
            this.taskHostControl = new System.Windows.Forms.FlowLayoutPanel();
            this.scMainGuiAndTasksHost = new System.Windows.Forms.SplitContainer();
            this.btCreateCirForSelectedFile = new System.Windows.Forms.Button();
            this.btProcessQueue = new System.Windows.Forms.Button();
            this.directory_CirCreationQueue = new O2.Views.ASCX.CoreControls.ascx_Directory();
            this.directory_CreatedCirFiles = new O2.Views.ASCX.CoreControls.ascx_Directory();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.scMainGuiAndTasksHost.Panel1.SuspendLayout();
            this.scMainGuiAndTasksHost.Panel2.SuspendLayout();
            this.scMainGuiAndTasksHost.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(4, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(233, 34);
            this.label1.TabIndex = 0;
            this.label1.Text = "note: only .NET assemblies are supported in the current version of O2CirCreator";
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
            this.splitContainer1.Panel1.Controls.Add(this.llDeleteFilesInCirCreationQueue);
            this.splitContainer1.Panel1.Controls.Add(this.directory_CirCreationQueue);
            this.splitContainer1.Panel1.Controls.Add(this.label2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.llDeleteFilesInCreatedCirFilesDirectory);
            this.splitContainer1.Panel2.Controls.Add(this.directory_CreatedCirFiles);
            this.splitContainer1.Panel2.Controls.Add(this.label3);
            this.splitContainer1.Size = new System.Drawing.Size(492, 170);
            this.splitContainer1.SplitterDistance = 244;
            this.splitContainer1.TabIndex = 1;
            // 
            // llDeleteFilesInCirCreationQueue
            // 
            this.llDeleteFilesInCirCreationQueue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.llDeleteFilesInCirCreationQueue.AutoSize = true;
            this.llDeleteFilesInCirCreationQueue.Location = new System.Drawing.Point(175, 4);
            this.llDeleteFilesInCirCreationQueue.Name = "llDeleteFilesInCirCreationQueue";
            this.llDeleteFilesInCirCreationQueue.Size = new System.Drawing.Size(62, 13);
            this.llDeleteFilesInCirCreationQueue.TabIndex = 2;
            this.llDeleteFilesInCirCreationQueue.TabStop = true;
            this.llDeleteFilesInCirCreationQueue.Text = "Delete Files";
            this.llDeleteFilesInCirCreationQueue.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llDeleteFilesInCirCreationQueue_LinkClicked);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(2, 4);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(96, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Cir Creation Queue";
            // 
            // llDeleteFilesInCreatedCirFilesDirectory
            // 
            this.llDeleteFilesInCreatedCirFilesDirectory.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.llDeleteFilesInCreatedCirFilesDirectory.AutoSize = true;
            this.llDeleteFilesInCreatedCirFilesDirectory.Location = new System.Drawing.Point(175, 5);
            this.llDeleteFilesInCreatedCirFilesDirectory.Name = "llDeleteFilesInCreatedCirFilesDirectory";
            this.llDeleteFilesInCreatedCirFilesDirectory.Size = new System.Drawing.Size(62, 13);
            this.llDeleteFilesInCreatedCirFilesDirectory.TabIndex = 3;
            this.llDeleteFilesInCreatedCirFilesDirectory.TabStop = true;
            this.llDeleteFilesInCreatedCirFilesDirectory.Text = "Delete Files";
            this.llDeleteFilesInCreatedCirFilesDirectory.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llDeleteFilesInCreatedCirFilesDirectory_LinkClicked);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(1, 4);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(83, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "Created Cir Files";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.laCreateCirForDotNetFrameworkAssemblies);
            this.groupBox1.Controls.Add(this.llCreateCirForAllO2Modules);
            this.groupBox1.Controls.Add(this.llCreateCirForCurrentO2Module);
            this.groupBox1.Location = new System.Drawing.Point(7, 279);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(492, 57);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Test Files";
            // 
            // laCreateCirForDotNetFrameworkAssemblies
            // 
            this.laCreateCirForDotNetFrameworkAssemblies.AutoSize = true;
            this.laCreateCirForDotNetFrameworkAssemblies.Enabled = false;
            this.laCreateCirForDotNetFrameworkAssemblies.Location = new System.Drawing.Point(7, 38);
            this.laCreateCirForDotNetFrameworkAssemblies.Name = "laCreateCirForDotNetFrameworkAssemblies";
            this.laCreateCirForDotNetFrameworkAssemblies.Size = new System.Drawing.Size(155, 13);
            this.laCreateCirForDotNetFrameworkAssemblies.TabIndex = 2;
            this.laCreateCirForDotNetFrameworkAssemblies.TabStop = true;
            this.laCreateCirForDotNetFrameworkAssemblies.Text = ".Net 2.0 Framework Assemblies";
            // 
            // llCreateCirForAllO2Modules
            // 
            this.llCreateCirForAllO2Modules.AutoSize = true;
            this.llCreateCirForAllO2Modules.Enabled = false;
            this.llCreateCirForAllO2Modules.Location = new System.Drawing.Point(124, 18);
            this.llCreateCirForAllO2Modules.Name = "llCreateCirForAllO2Modules";
            this.llCreateCirForAllO2Modules.Size = new System.Drawing.Size(125, 13);
            this.llCreateCirForAllO2Modules.TabIndex = 1;
            this.llCreateCirForAllO2Modules.TabStop = true;
            this.llCreateCirForAllO2Modules.Text = "All loaded O2 Assemblies";
            // 
            // llCreateCirForCurrentO2Module
            // 
            this.llCreateCirForCurrentO2Module.AutoSize = true;
            this.llCreateCirForCurrentO2Module.Location = new System.Drawing.Point(7, 18);
            this.llCreateCirForCurrentO2Module.Name = "llCreateCirForCurrentO2Module";
            this.llCreateCirForCurrentO2Module.Size = new System.Drawing.Size(82, 13);
            this.llCreateCirForCurrentO2Module.TabIndex = 0;
            this.llCreateCirForCurrentO2Module.TabStop = true;
            this.llCreateCirForCurrentO2Module.Text = "This O2 Module";
            this.llCreateCirForCurrentO2Module.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llCreateCirForCurrentO2Module_LinkClicked);
            // 
            // llHideTaskControlHost
            // 
            this.llHideTaskControlHost.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.llHideTaskControlHost.AutoSize = true;
            this.llHideTaskControlHost.Location = new System.Drawing.Point(456, 3);
            this.llHideTaskControlHost.Name = "llHideTaskControlHost";
            this.llHideTaskControlHost.Size = new System.Drawing.Size(29, 13);
            this.llHideTaskControlHost.TabIndex = 35;
            this.llHideTaskControlHost.TabStop = true;
            this.llHideTaskControlHost.Text = "Hide";
            // 
            // taskHostControl
            // 
            this.taskHostControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.taskHostControl.Location = new System.Drawing.Point(3, 3);
            this.taskHostControl.Name = "taskHostControl";
            this.taskHostControl.Size = new System.Drawing.Size(482, 48);
            this.taskHostControl.TabIndex = 34;
            // 
            // scMainGuiAndTasksHost
            // 
            this.scMainGuiAndTasksHost.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.scMainGuiAndTasksHost.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.scMainGuiAndTasksHost.Location = new System.Drawing.Point(7, 41);
            this.scMainGuiAndTasksHost.Name = "scMainGuiAndTasksHost";
            this.scMainGuiAndTasksHost.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // scMainGuiAndTasksHost.Panel1
            // 
            this.scMainGuiAndTasksHost.Panel1.Controls.Add(this.splitContainer1);
            // 
            // scMainGuiAndTasksHost.Panel2
            // 
            this.scMainGuiAndTasksHost.Panel2.Controls.Add(this.llHideTaskControlHost);
            this.scMainGuiAndTasksHost.Panel2.Controls.Add(this.taskHostControl);
            this.scMainGuiAndTasksHost.Size = new System.Drawing.Size(492, 232);
            this.scMainGuiAndTasksHost.SplitterDistance = 170;
            this.scMainGuiAndTasksHost.TabIndex = 36;
            // 
            // btCreateCirForSelectedFile
            // 
            this.btCreateCirForSelectedFile.Enabled = false;
            this.btCreateCirForSelectedFile.Location = new System.Drawing.Point(346, 3);
            this.btCreateCirForSelectedFile.Name = "btCreateCirForSelectedFile";
            this.btCreateCirForSelectedFile.Size = new System.Drawing.Size(148, 23);
            this.btCreateCirForSelectedFile.TabIndex = 37;
            this.btCreateCirForSelectedFile.Text = "Create Cir For Selected File";
            this.btCreateCirForSelectedFile.UseVisualStyleBackColor = true;
            this.btCreateCirForSelectedFile.Click += new System.EventHandler(this.btCreateCirForSelectedFile_Click);
            // 
            // btProcessQueue
            // 
            this.btProcessQueue.Enabled = false;
            this.btProcessQueue.Location = new System.Drawing.Point(230, 4);
            this.btProcessQueue.Name = "btProcessQueue";
            this.btProcessQueue.Size = new System.Drawing.Size(111, 23);
            this.btProcessQueue.TabIndex = 38;
            this.btProcessQueue.Text = "Process Queue";
            this.btProcessQueue.UseVisualStyleBackColor = true;
            this.btProcessQueue.Click += new System.EventHandler(this.btProcessQueue_Click);
            // 
            // directory_CirCreationQueue
            // 
            this.directory_CirCreationQueue._ProcessDroppedObjects = true;
            this.directory_CirCreationQueue._ShowFileSize = false;
            this.directory_CirCreationQueue._ShowLinkToUpperFolder = true;
            this.directory_CirCreationQueue._ViewMode = O2.Views.ASCX.CoreControls.ascx_Directory.ViewMode.Simple_With_LocationBar;
            this.directory_CirCreationQueue._WatchFolder = true;
            this.directory_CirCreationQueue.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.directory_CirCreationQueue.BackColor = System.Drawing.SystemColors.Control;
            this.directory_CirCreationQueue.ForeColor = System.Drawing.Color.Black;
            this.directory_CirCreationQueue.Location = new System.Drawing.Point(2, 21);
            this.directory_CirCreationQueue.Name = "directory_CirCreationQueue";
            this.directory_CirCreationQueue.Size = new System.Drawing.Size(235, 142);
            this.directory_CirCreationQueue.TabIndex = 1;
            this.directory_CirCreationQueue._onTreeViewDrop += new O2.Kernel.CodeUtils.Callbacks.dMethod_String(this.directory_CirCreationQueue__onTreeViewDrop);
            this.directory_CirCreationQueue._onDirectoryClick += new O2.Kernel.CodeUtils.Callbacks.dMethod_String(this.directory_CirCreationQueue__onDirectoryClick);
            this.directory_CirCreationQueue._onFileWatchEvent += new O2.DotNetWrappers.Windows.FolderWatcher.CallbackOnFolderWatchEvent(this.directory_CirCreationQueue__onFileWatchEvent);
            // 
            // directory_CreatedCirFiles
            // 
            this.directory_CreatedCirFiles._ProcessDroppedObjects = true;
            this.directory_CreatedCirFiles._ShowFileSize = false;
            this.directory_CreatedCirFiles._ShowLinkToUpperFolder = true;
            this.directory_CreatedCirFiles._ViewMode = O2.Views.ASCX.CoreControls.ascx_Directory.ViewMode.Simple_With_LocationBar;
            this.directory_CreatedCirFiles._WatchFolder = true;
            this.directory_CreatedCirFiles.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.directory_CreatedCirFiles.BackColor = System.Drawing.SystemColors.Control;
            this.directory_CreatedCirFiles.ForeColor = System.Drawing.Color.Black;
            this.directory_CreatedCirFiles.Location = new System.Drawing.Point(4, 21);
            this.directory_CreatedCirFiles.Name = "directory_CreatedCirFiles";
            this.directory_CreatedCirFiles.Size = new System.Drawing.Size(233, 144);
            this.directory_CreatedCirFiles.TabIndex = 2;
            // 
            // ascx_CirCreator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btProcessQueue);
            this.Controls.Add(this.btCreateCirForSelectedFile);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.scMainGuiAndTasksHost);
            this.Controls.Add(this.label1);
            this.Name = "ascx_CirCreator";
            this.Size = new System.Drawing.Size(502, 339);
            this.Load += new System.EventHandler(this.ascx_CirCreator_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            this.splitContainer1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.scMainGuiAndTasksHost.Panel1.ResumeLayout(false);
            this.scMainGuiAndTasksHost.Panel2.ResumeLayout(false);
            this.scMainGuiAndTasksHost.Panel2.PerformLayout();
            this.scMainGuiAndTasksHost.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.LinkLabel laCreateCirForDotNetFrameworkAssemblies;
        private System.Windows.Forms.LinkLabel llCreateCirForAllO2Modules;
        private System.Windows.Forms.LinkLabel llCreateCirForCurrentO2Module;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.LinkLabel llHideTaskControlHost;
        private System.Windows.Forms.FlowLayoutPanel taskHostControl;
        private System.Windows.Forms.SplitContainer scMainGuiAndTasksHost;
        private O2.Views.ASCX.CoreControls.ascx_Directory directory_CirCreationQueue;
        private O2.Views.ASCX.CoreControls.ascx_Directory directory_CreatedCirFiles;
        private System.Windows.Forms.LinkLabel llDeleteFilesInCirCreationQueue;
        private System.Windows.Forms.Button btCreateCirForSelectedFile;
        private System.Windows.Forms.LinkLabel llDeleteFilesInCreatedCirFilesDirectory;
        private System.Windows.Forms.Button btProcessQueue;
    }
}
