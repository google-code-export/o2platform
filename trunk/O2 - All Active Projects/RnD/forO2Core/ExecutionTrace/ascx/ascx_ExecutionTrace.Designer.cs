// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
namespace O2.Rnd.ExecutionTrace.ascx
{
    partial class ascx_ExecutionTrace
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
            this.tbDirectoryToMonitor = new System.Windows.Forms.TextBox();
            this.DirectoryToMonitor = new System.Windows.Forms.Label();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.btDeleteLogFileFromDirectory = new System.Windows.Forms.Button();
            this.lbFilesInDirectory = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cbShowTrace = new System.Windows.Forms.CheckBox();
            this.lbMethodsToFilter = new System.Windows.Forms.ListBox();
            this.cbFilterTraceMethods = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tbFileContents = new System.Windows.Forms.TextBox();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.wbFileContents = new System.Windows.Forms.WebBrowser();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tbDirectoryToMonitor
            // 
            this.tbDirectoryToMonitor.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                                                                                     | System.Windows.Forms.AnchorStyles.Right)));
            this.tbDirectoryToMonitor.Location = new System.Drawing.Point(125, 9);
            this.tbDirectoryToMonitor.Name = "tbDirectoryToMonitor";
            this.tbDirectoryToMonitor.Size = new System.Drawing.Size(513, 20);
            this.tbDirectoryToMonitor.TabIndex = 2;
            this.tbDirectoryToMonitor.TextChanged += new System.EventHandler(this.tbDirectoryToMonitor_TextChanged);
            // 
            // DirectoryToMonitor
            // 
            this.DirectoryToMonitor.AutoSize = true;
            this.DirectoryToMonitor.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DirectoryToMonitor.Location = new System.Drawing.Point(3, 11);
            this.DirectoryToMonitor.Name = "DirectoryToMonitor";
            this.DirectoryToMonitor.Size = new System.Drawing.Size(100, 13);
            this.DirectoryToMonitor.TabIndex = 3;
            this.DirectoryToMonitor.Text = "Execution Trace";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                                                                                 | System.Windows.Forms.AnchorStyles.Left)
                                                                                | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer1.Location = new System.Drawing.Point(6, 46);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.btDeleteLogFileFromDirectory);
            this.splitContainer1.Panel1.Controls.Add(this.lbFilesInDirectory);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Panel2.Controls.Add(this.cbShowTrace);
            this.splitContainer1.Panel2.Controls.Add(this.lbMethodsToFilter);
            this.splitContainer1.Panel2.Controls.Add(this.cbFilterTraceMethods);
            this.splitContainer1.Panel2.Controls.Add(this.label2);
            this.splitContainer1.Size = new System.Drawing.Size(637, 454);
            this.splitContainer1.SplitterDistance = 344;
            this.splitContainer1.TabIndex = 4;
            // 
            // btDeleteLogFileFromDirectory
            // 
            this.btDeleteLogFileFromDirectory.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btDeleteLogFileFromDirectory.Location = new System.Drawing.Point(6, 424);
            this.btDeleteLogFileFromDirectory.Name = "btDeleteLogFileFromDirectory";
            this.btDeleteLogFileFromDirectory.Size = new System.Drawing.Size(175, 23);
            this.btDeleteLogFileFromDirectory.TabIndex = 6;
            this.btDeleteLogFileFromDirectory.Text = "Delete Log Files from Directory";
            this.btDeleteLogFileFromDirectory.UseVisualStyleBackColor = true;
            this.btDeleteLogFileFromDirectory.Click += new System.EventHandler(this.btDeleteLogFileFromDirectory_Click);
            // 
            // lbFilesInDirectory
            // 
            this.lbFilesInDirectory.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                                                                                    | System.Windows.Forms.AnchorStyles.Left)
                                                                                   | System.Windows.Forms.AnchorStyles.Right)));
            this.lbFilesInDirectory.FormattingEnabled = true;
            this.lbFilesInDirectory.Location = new System.Drawing.Point(4, 27);
            this.lbFilesInDirectory.Name = "lbFilesInDirectory";
            this.lbFilesInDirectory.ScrollAlwaysVisible = true;
            this.lbFilesInDirectory.Size = new System.Drawing.Size(333, 394);
            this.lbFilesInDirectory.TabIndex = 5;
            this.lbFilesInDirectory.SelectedIndexChanged += new System.EventHandler(this.lbFilesInDirectory_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(3, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(33, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Files";
            // 
            // cbShowTrace
            // 
            this.cbShowTrace.AutoSize = true;
            this.cbShowTrace.Checked = true;
            this.cbShowTrace.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbShowTrace.Location = new System.Drawing.Point(90, 5);
            this.cbShowTrace.Name = "cbShowTrace";
            this.cbShowTrace.Size = new System.Drawing.Size(84, 17);
            this.cbShowTrace.TabIndex = 8;
            this.cbShowTrace.Text = "Show Trace";
            this.cbShowTrace.UseVisualStyleBackColor = true;
            // 
            // lbMethodsToFilter
            // 
            this.lbMethodsToFilter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                                                                                  | System.Windows.Forms.AnchorStyles.Right)));
            this.lbMethodsToFilter.FormattingEnabled = true;
            this.lbMethodsToFilter.Location = new System.Drawing.Point(3, 378);
            this.lbMethodsToFilter.Name = "lbMethodsToFilter";
            this.lbMethodsToFilter.ScrollAlwaysVisible = true;
            this.lbMethodsToFilter.Size = new System.Drawing.Size(279, 69);
            this.lbMethodsToFilter.TabIndex = 7;
            // 
            // cbFilterTraceMethods
            // 
            this.cbFilterTraceMethods.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cbFilterTraceMethods.AutoSize = true;
            this.cbFilterTraceMethods.Checked = true;
            this.cbFilterTraceMethods.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbFilterTraceMethods.Location = new System.Drawing.Point(4, 353);
            this.cbFilterTraceMethods.Name = "cbFilterTraceMethods";
            this.cbFilterTraceMethods.Size = new System.Drawing.Size(123, 17);
            this.cbFilterTraceMethods.TabIndex = 7;
            this.cbFilterTraceMethods.Text = "Filter Trace Methods";
            this.cbFilterTraceMethods.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(3, 6);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(81, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "File Contents";
            // 
            // tbFileContents
            // 
            this.tbFileContents.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbFileContents.Location = new System.Drawing.Point(0, 0);
            this.tbFileContents.Multiline = true;
            this.tbFileContents.Name = "tbFileContents";
            this.tbFileContents.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tbFileContents.Size = new System.Drawing.Size(272, 167);
            this.tbFileContents.TabIndex = 6;
            this.tbFileContents.WordWrap = false;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                                                                                 | System.Windows.Forms.AnchorStyles.Left)
                                                                                | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer2.Location = new System.Drawing.Point(6, 28);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.tbFileContents);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.wbFileContents);
            this.splitContainer2.Size = new System.Drawing.Size(276, 319);
            this.splitContainer2.SplitterDistance = 171;
            this.splitContainer2.TabIndex = 9;
            // 
            // wbFileContents
            // 
            this.wbFileContents.Dock = System.Windows.Forms.DockStyle.Fill;
            this.wbFileContents.Location = new System.Drawing.Point(0, 0);
            this.wbFileContents.MinimumSize = new System.Drawing.Size(20, 20);
            this.wbFileContents.Name = "wbFileContents";
            this.wbFileContents.Size = new System.Drawing.Size(272, 140);
            this.wbFileContents.TabIndex = 0;
            // 
            // ascx_ExecutionTrace
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.DirectoryToMonitor);
            this.Controls.Add(this.tbDirectoryToMonitor);
            this.Name = "ascx_ExecutionTrace";
            this.Size = new System.Drawing.Size(646, 503);
            this.Load += new System.EventHandler(this.ascx_ExecutionTrace_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel1.PerformLayout();
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbDirectoryToMonitor;
        private System.Windows.Forms.Label DirectoryToMonitor;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox lbFilesInDirectory;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btDeleteLogFileFromDirectory;
        private System.Windows.Forms.ListBox lbMethodsToFilter;
        private System.Windows.Forms.CheckBox cbFilterTraceMethods;
        private System.Windows.Forms.CheckBox cbShowTrace;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.TextBox tbFileContents;
        private System.Windows.Forms.WebBrowser wbFileContents;
    }
}
