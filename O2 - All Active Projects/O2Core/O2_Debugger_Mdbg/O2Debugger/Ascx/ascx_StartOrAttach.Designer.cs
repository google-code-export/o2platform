namespace O2.Debugger.Mdbg.O2Debugger.Ascx
{
    partial class ascx_StartOrAttach
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
            this.lvExecutablesInO2Dirs = new System.Windows.Forms.ListView();
            this.label13 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.tbExecutableToStartAndDebug = new System.Windows.Forms.TextBox();
            this.btStartProcess = new System.Windows.Forms.Button();
            this.label12 = new System.Windows.Forms.Label();
            this.btAttachToSelectedProcess = new System.Windows.Forms.Button();
            this.lvManagedProcesses = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.llRefreshListOfAvailableManagedProcesses = new System.Windows.Forms.LinkLabel();
            this.tcStartOrAttach = new System.Windows.Forms.TabControl();
            this.tpStartProcess = new System.Windows.Forms.TabPage();
            this.tpAttachIntoProcess = new System.Windows.Forms.TabPage();
            this.tcStartOrAttach.SuspendLayout();
            this.tpStartProcess.SuspendLayout();
            this.tpAttachIntoProcess.SuspendLayout();
            this.SuspendLayout();
            // 
            // lvExecutablesInO2Dirs
            // 
            this.lvExecutablesInO2Dirs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                                                                                       | System.Windows.Forms.AnchorStyles.Left)
                                                                                      | System.Windows.Forms.AnchorStyles.Right)));
            this.lvExecutablesInO2Dirs.Location = new System.Drawing.Point(6, 102);
            this.lvExecutablesInO2Dirs.Name = "lvExecutablesInO2Dirs";
            this.lvExecutablesInO2Dirs.Size = new System.Drawing.Size(263, 365);
            this.lvExecutablesInO2Dirs.TabIndex = 6;
            this.lvExecutablesInO2Dirs.UseCompatibleStateImageBehavior = false;
            this.lvExecutablesInO2Dirs.View = System.Windows.Forms.View.List;
            this.lvExecutablesInO2Dirs.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lvExecutablesInO2Dirs_MouseDoubleClick);
            this.lvExecutablesInO2Dirs.SelectedIndexChanged += new System.EventHandler(this.lvExecutablesInO2Dirs_SelectedIndexChanged);
            // 
            // label13
            // 
            this.label13.Location = new System.Drawing.Point(3, 71);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(157, 39);
            this.label13.TabIndex = 5;
            this.label13.Text = "Available Process in current O2 executable and temp folders";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(86, 47);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(258, 13);
            this.label11.TabIndex = 4;
            this.label11.Text = "(you can drop an EXE file into the text box to debug it";
            this.label11.Visible = false;
            // 
            // tbExecutableToStartAndDebug
            // 
            this.tbExecutableToStartAndDebug.AllowDrop = true;
            this.tbExecutableToStartAndDebug.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                                                                                            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbExecutableToStartAndDebug.Location = new System.Drawing.Point(89, 24);
            this.tbExecutableToStartAndDebug.Name = "tbExecutableToStartAndDebug";
            this.tbExecutableToStartAndDebug.Size = new System.Drawing.Size(90, 20);
            this.tbExecutableToStartAndDebug.TabIndex = 3;
            this.tbExecutableToStartAndDebug.DragDrop += new System.Windows.Forms.DragEventHandler(this.tbExecutableToStartAndDebug_DragDrop);
            this.tbExecutableToStartAndDebug.DragEnter += new System.Windows.Forms.DragEventHandler(this.tbExecutableToStartAndDebug_DragEnter);
            // 
            // btStartProcess
            // 
            this.btStartProcess.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btStartProcess.Location = new System.Drawing.Point(185, 22);
            this.btStartProcess.Name = "btStartProcess";
            this.btStartProcess.Size = new System.Drawing.Size(87, 23);
            this.btStartProcess.TabIndex = 2;
            this.btStartProcess.Text = "Start Process";
            this.btStartProcess.UseVisualStyleBackColor = true;
            this.btStartProcess.Click += new System.EventHandler(this.btStartProcess_Click);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(3, 27);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(80, 13);
            this.label12.TabIndex = 0;
            this.label12.Text = "Process to start";
            // 
            // btAttachToSelectedProcess
            // 
            this.btAttachToSelectedProcess.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btAttachToSelectedProcess.Location = new System.Drawing.Point(73, 7);
            this.btAttachToSelectedProcess.Name = "btAttachToSelectedProcess";
            this.btAttachToSelectedProcess.Size = new System.Drawing.Size(135, 54);
            this.btAttachToSelectedProcess.TabIndex = 36;
            this.btAttachToSelectedProcess.Text = "Attach to selected Managed Process";
            this.btAttachToSelectedProcess.UseVisualStyleBackColor = true;
            this.btAttachToSelectedProcess.Click += new System.EventHandler(this.btAttachToSelectedProcess_Click);
            // 
            // lvManagedProcesses
            // 
            this.lvManagedProcesses.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                                                                                    | System.Windows.Forms.AnchorStyles.Left)
                                                                                   | System.Windows.Forms.AnchorStyles.Right)));
            this.lvManagedProcesses.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
                                                                                                 this.columnHeader1,
                                                                                                 this.columnHeader2});
            this.lvManagedProcesses.FullRowSelect = true;
            this.lvManagedProcesses.Location = new System.Drawing.Point(6, 96);
            this.lvManagedProcesses.Name = "lvManagedProcesses";
            this.lvManagedProcesses.Size = new System.Drawing.Size(263, 377);
            this.lvManagedProcesses.TabIndex = 35;
            this.lvManagedProcesses.UseCompatibleStateImageBehavior = false;
            this.lvManagedProcesses.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Process ID";
            this.columnHeader1.Width = 89;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Process Name";
            this.columnHeader2.Width = 412;
            // 
            // llRefreshListOfAvailableManagedProcesses
            // 
            this.llRefreshListOfAvailableManagedProcesses.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.llRefreshListOfAvailableManagedProcesses.AutoSize = true;
            this.llRefreshListOfAvailableManagedProcesses.Location = new System.Drawing.Point(27, 70);
            this.llRefreshListOfAvailableManagedProcesses.Name = "llRefreshListOfAvailableManagedProcesses";
            this.llRefreshListOfAvailableManagedProcesses.Size = new System.Drawing.Size(217, 13);
            this.llRefreshListOfAvailableManagedProcesses.TabIndex = 34;
            this.llRefreshListOfAvailableManagedProcesses.TabStop = true;
            this.llRefreshListOfAvailableManagedProcesses.Text = "Refresh list of Available Managed Processes";
            this.llRefreshListOfAvailableManagedProcesses.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llRefreshListOfAvailableManagedProcesses_LinkClicked);
            // 
            // tcStartOrAttach
            // 
            this.tcStartOrAttach.Controls.Add(this.tpStartProcess);
            this.tcStartOrAttach.Controls.Add(this.tpAttachIntoProcess);
            this.tcStartOrAttach.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcStartOrAttach.Location = new System.Drawing.Point(0, 0);
            this.tcStartOrAttach.Name = "tcStartOrAttach";
            this.tcStartOrAttach.SelectedIndex = 0;
            this.tcStartOrAttach.Size = new System.Drawing.Size(283, 499);
            this.tcStartOrAttach.TabIndex = 41;
            // 
            // tpStartProcess
            // 
            this.tpStartProcess.Controls.Add(this.lvExecutablesInO2Dirs);
            this.tpStartProcess.Controls.Add(this.label12);
            this.tpStartProcess.Controls.Add(this.label13);
            this.tpStartProcess.Controls.Add(this.btStartProcess);
            this.tpStartProcess.Controls.Add(this.label11);
            this.tpStartProcess.Controls.Add(this.tbExecutableToStartAndDebug);
            this.tpStartProcess.Location = new System.Drawing.Point(4, 22);
            this.tpStartProcess.Name = "tpStartProcess";
            this.tpStartProcess.Padding = new System.Windows.Forms.Padding(3);
            this.tpStartProcess.Size = new System.Drawing.Size(275, 473);
            this.tpStartProcess.TabIndex = 0;
            this.tpStartProcess.Text = "Start Process";
            this.tpStartProcess.UseVisualStyleBackColor = true;
            this.tpStartProcess.Enter += new System.EventHandler(this.tpStartProcess_Enter);
            // 
            // tpAttachIntoProcess
            // 
            this.tpAttachIntoProcess.Controls.Add(this.btAttachToSelectedProcess);
            this.tpAttachIntoProcess.Controls.Add(this.lvManagedProcesses);
            this.tpAttachIntoProcess.Controls.Add(this.llRefreshListOfAvailableManagedProcesses);
            this.tpAttachIntoProcess.Location = new System.Drawing.Point(4, 22);
            this.tpAttachIntoProcess.Name = "tpAttachIntoProcess";
            this.tpAttachIntoProcess.Padding = new System.Windows.Forms.Padding(3);
            this.tpAttachIntoProcess.Size = new System.Drawing.Size(275, 473);
            this.tpAttachIntoProcess.TabIndex = 1;
            this.tpAttachIntoProcess.Text = "Attach Into Process";
            this.tpAttachIntoProcess.UseVisualStyleBackColor = true;
            this.tpAttachIntoProcess.Enter += new System.EventHandler(this.tpAttachIntoProcess_Enter);
            // 
            // ascx_StartOrAttach
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tcStartOrAttach);
            this.Name = "ascx_StartOrAttach";
            this.Size = new System.Drawing.Size(283, 499);
            this.tcStartOrAttach.ResumeLayout(false);
            this.tpStartProcess.ResumeLayout(false);
            this.tpStartProcess.PerformLayout();
            this.tpAttachIntoProcess.ResumeLayout(false);
            this.tpAttachIntoProcess.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView lvExecutablesInO2Dirs;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox tbExecutableToStartAndDebug;
        private System.Windows.Forms.Button btStartProcess;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Button btAttachToSelectedProcess;
        private System.Windows.Forms.ListView lvManagedProcesses;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.LinkLabel llRefreshListOfAvailableManagedProcesses;
        private System.Windows.Forms.TabControl tcStartOrAttach;
        private System.Windows.Forms.TabPage tpStartProcess;
        private System.Windows.Forms.TabPage tpAttachIntoProcess;
    }
}