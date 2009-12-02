namespace O2.External.Python.Ascx
{
    partial class ascx_PythonCmdShell
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
            this.rtbShellWindow = new System.Windows.Forms.RichTextBox();
            this.tbCommandToExecute = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tbPathToIronPython = new System.Windows.Forms.TextBox();
            this.btStartIronPython = new System.Windows.Forms.Button();
            this.tbPathToJPython = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btStartJython = new System.Windows.Forms.Button();
            this.btKillProcess = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.lbCurrentEngine = new System.Windows.Forms.Label();
            this.btStartCPython = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.btStartIronPythonInNewWindow = new System.Windows.Forms.Button();
            this.btStartCPythonInNewWindow = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.btStartJythonInNewWindow = new System.Windows.Forms.Button();
            this.tpConfig = new System.Windows.Forms.TabPage();
            this.label6 = new System.Windows.Forms.Label();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.lbCommandHistory = new System.Windows.Forms.ListBox();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tpConfig.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // rtbShellWindow
            // 
            this.rtbShellWindow.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.rtbShellWindow.Location = new System.Drawing.Point(3, 24);
            this.rtbShellWindow.Name = "rtbShellWindow";
            this.rtbShellWindow.Size = new System.Drawing.Size(527, 211);
            this.rtbShellWindow.TabIndex = 0;
            this.rtbShellWindow.Text = "";
            this.rtbShellWindow.TextChanged += new System.EventHandler(this.rtbShellWindow_TextChanged);
            // 
            // tbCommandToExecute
            // 
            this.tbCommandToExecute.Location = new System.Drawing.Point(121, 3);
            this.tbCommandToExecute.Name = "tbCommandToExecute";
            this.tbCommandToExecute.Size = new System.Drawing.Size(409, 20);
            this.tbCommandToExecute.TabIndex = 1;
            this.tbCommandToExecute.TextChanged += new System.EventHandler(this.tbCommandToExecute_TextChanged);
            this.tbCommandToExecute.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbCommandToExecute_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(0, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(112, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Command To Execute";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 8);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(99, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Path To IronPhyton";
            // 
            // tbPathToIronPython
            // 
            this.tbPathToIronPython.Location = new System.Drawing.Point(113, 6);
            this.tbPathToIronPython.Name = "tbPathToIronPython";
            this.tbPathToIronPython.Size = new System.Drawing.Size(324, 20);
            this.tbPathToIronPython.TabIndex = 4;
            // 
            // btStartIronPython
            // 
            this.btStartIronPython.Location = new System.Drawing.Point(115, 11);
            this.btStartIronPython.Name = "btStartIronPython";
            this.btStartIronPython.Size = new System.Drawing.Size(131, 23);
            this.btStartIronPython.TabIndex = 5;
            this.btStartIronPython.Text = "start IronPython (.Net)";
            this.btStartIronPython.UseVisualStyleBackColor = true;
            this.btStartIronPython.Click += new System.EventHandler(this.btStartIronPython_Click);
            // 
            // tbPathToJPython
            // 
            this.tbPathToJPython.Location = new System.Drawing.Point(113, 32);
            this.tbPathToJPython.Name = "tbPathToJPython";
            this.tbPathToJPython.Size = new System.Drawing.Size(324, 20);
            this.tbPathToJPython.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 34);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(86, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Path To JPhyton";
            // 
            // btStartJython
            // 
            this.btStartJython.Location = new System.Drawing.Point(115, 40);
            this.btStartJython.Name = "btStartJython";
            this.btStartJython.Size = new System.Drawing.Size(131, 23);
            this.btStartJython.TabIndex = 8;
            this.btStartJython.Text = "start Jython (Java)";
            this.btStartJython.UseVisualStyleBackColor = true;
            this.btStartJython.Click += new System.EventHandler(this.btStartJPython_Click);
            // 
            // btKillProcess
            // 
            this.btKillProcess.Enabled = false;
            this.btKillProcess.Location = new System.Drawing.Point(252, 12);
            this.btKillProcess.Name = "btKillProcess";
            this.btKillProcess.Size = new System.Drawing.Size(86, 80);
            this.btKillProcess.TabIndex = 9;
            this.btKillProcess.Text = "Stop running  Python Process";
            this.btKillProcess.UseVisualStyleBackColor = true;
            this.btKillProcess.Click += new System.EventHandler(this.btKillProcess_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tpConfig);
            this.tabControl1.Location = new System.Drawing.Point(4, 4);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(518, 141);
            this.tabControl1.TabIndex = 10;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.lbCurrentEngine);
            this.tabPage1.Controls.Add(this.btStartCPython);
            this.tabPage1.Controls.Add(this.label4);
            this.tabPage1.Controls.Add(this.btStartIronPython);
            this.tabPage1.Controls.Add(this.btKillProcess);
            this.tabPage1.Controls.Add(this.btStartJython);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(510, 115);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Interactive O2->Python Shell";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // lbCurrentEngine
            // 
            this.lbCurrentEngine.AutoSize = true;
            this.lbCurrentEngine.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbCurrentEngine.ForeColor = System.Drawing.Color.DarkViolet;
            this.lbCurrentEngine.Location = new System.Drawing.Point(350, 12);
            this.lbCurrentEngine.Name = "lbCurrentEngine";
            this.lbCurrentEngine.Size = new System.Drawing.Size(20, 16);
            this.lbCurrentEngine.TabIndex = 16;
            this.lbCurrentEngine.Text = "...";
            // 
            // btStartCPython
            // 
            this.btStartCPython.Enabled = false;
            this.btStartCPython.Location = new System.Drawing.Point(115, 69);
            this.btStartCPython.Name = "btStartCPython";
            this.btStartCPython.Size = new System.Drawing.Size(131, 23);
            this.btStartCPython.TabIndex = 15;
            this.btStartCPython.Text = "start CPython (C)";
            this.btStartCPython.UseVisualStyleBackColor = true;
            this.btStartCPython.Click += new System.EventHandler(this.btStartCPython_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(5, 12);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(102, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "Open inside O2 GUI";
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.btStartIronPythonInNewWindow);
            this.tabPage3.Controls.Add(this.btStartCPythonInNewWindow);
            this.tabPage3.Controls.Add(this.label5);
            this.tabPage3.Controls.Add(this.btStartJythonInNewWindow);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(482, 115);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "cmd.exe Python Shell";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // btStartIronPythonInNewWindow
            // 
            this.btStartIronPythonInNewWindow.Location = new System.Drawing.Point(173, 11);
            this.btStartIronPythonInNewWindow.Name = "btStartIronPythonInNewWindow";
            this.btStartIronPythonInNewWindow.Size = new System.Drawing.Size(170, 23);
            this.btStartIronPythonInNewWindow.TabIndex = 12;
            this.btStartIronPythonInNewWindow.Text = "start IronPython in new window";
            this.btStartIronPythonInNewWindow.UseVisualStyleBackColor = true;
            this.btStartIronPythonInNewWindow.Click += new System.EventHandler(this.btStartIronPythonInNewWindow_Click);
            // 
            // btStartCPythonInNewWindow
            // 
            this.btStartCPythonInNewWindow.Location = new System.Drawing.Point(173, 71);
            this.btStartCPythonInNewWindow.Name = "btStartCPythonInNewWindow";
            this.btStartCPythonInNewWindow.Size = new System.Drawing.Size(170, 23);
            this.btStartCPythonInNewWindow.TabIndex = 14;
            this.btStartCPythonInNewWindow.Text = "start CPython in new window";
            this.btStartCPythonInNewWindow.UseVisualStyleBackColor = true;
            this.btStartCPythonInNewWindow.Click += new System.EventHandler(this.btStartCPythonInNewWindow_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(5, 13);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(161, 13);
            this.label5.TabIndex = 11;
            this.label5.Text = "Open in standalone cmd window";
            // 
            // btStartJythonInNewWindow
            // 
            this.btStartJythonInNewWindow.Location = new System.Drawing.Point(173, 40);
            this.btStartJythonInNewWindow.Name = "btStartJythonInNewWindow";
            this.btStartJythonInNewWindow.Size = new System.Drawing.Size(170, 23);
            this.btStartJythonInNewWindow.TabIndex = 13;
            this.btStartJythonInNewWindow.Text = "start Jython in new window";
            this.btStartJythonInNewWindow.UseVisualStyleBackColor = true;
            this.btStartJythonInNewWindow.Click += new System.EventHandler(this.btStartJythonInNewWindow_Click);
            // 
            // tpConfig
            // 
            this.tpConfig.Controls.Add(this.tbPathToIronPython);
            this.tpConfig.Controls.Add(this.label2);
            this.tpConfig.Controls.Add(this.tbPathToJPython);
            this.tpConfig.Controls.Add(this.label3);
            this.tpConfig.Location = new System.Drawing.Point(4, 22);
            this.tpConfig.Name = "tpConfig";
            this.tpConfig.Padding = new System.Windows.Forms.Padding(3);
            this.tpConfig.Size = new System.Drawing.Size(482, 115);
            this.tpConfig.TabIndex = 1;
            this.tpConfig.Text = "config";
            this.tpConfig.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(0, 6);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(94, 13);
            this.label6.TabIndex = 11;
            this.label6.Text = "Commands History";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer1.Location = new System.Drawing.Point(3, 151);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            this.splitContainer1.Panel1.Controls.Add(this.tbCommandToExecute);
            this.splitContainer1.Panel1.Controls.Add(this.rtbShellWindow);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.lbCommandHistory);
            this.splitContainer1.Panel2.Controls.Add(this.label6);
            this.splitContainer1.Size = new System.Drawing.Size(696, 242);
            this.splitContainer1.SplitterDistance = 537;
            this.splitContainer1.TabIndex = 12;
            // 
            // lbCommandHistory
            // 
            this.lbCommandHistory.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lbCommandHistory.FormattingEnabled = true;
            this.lbCommandHistory.Location = new System.Drawing.Point(1, 25);
            this.lbCommandHistory.Name = "lbCommandHistory";
            this.lbCommandHistory.Size = new System.Drawing.Size(148, 212);
            this.lbCommandHistory.TabIndex = 12;
            this.lbCommandHistory.SelectedIndexChanged += new System.EventHandler(this.lbCommandHistory_SelectedIndexChanged);
            this.lbCommandHistory.DoubleClick += new System.EventHandler(this.lbCommandHistory_DoubleClick);
            // 
            // ascx_PythonCmdShell
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.tabControl1);
            this.Name = "ascx_PythonCmdShell";
            this.Size = new System.Drawing.Size(702, 396);
            this.Load += new System.EventHandler(this.ascx_PythonCmdShell_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.tpConfig.ResumeLayout(false);
            this.tpConfig.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox rtbShellWindow;
        private System.Windows.Forms.TextBox tbCommandToExecute;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbPathToIronPython;
        private System.Windows.Forms.Button btStartIronPython;
        private System.Windows.Forms.TextBox tbPathToJPython;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btStartJython;
        private System.Windows.Forms.Button btKillProcess;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tpConfig;
        private System.Windows.Forms.Button btStartIronPythonInNewWindow;
        private System.Windows.Forms.Button btStartJythonInNewWindow;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btStartCPythonInNewWindow;
        private System.Windows.Forms.Button btStartCPython;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ListBox lbCommandHistory;
        private System.Windows.Forms.Label lbCurrentEngine;
        private System.Windows.Forms.TabPage tabPage3;
    }
}