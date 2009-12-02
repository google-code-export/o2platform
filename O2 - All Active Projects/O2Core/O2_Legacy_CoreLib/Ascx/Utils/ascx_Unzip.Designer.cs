// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using O2.Views.ASCX.CoreControls;

namespace O2.Legacy.CoreLib.Ascx.Utils
{
    partial class ascx_Unzip
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
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.tasksHostControl = new System.Windows.Forms.FlowLayoutPanel();
            this.directoryWithUnzipedFiles = new ascx_Directory();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.SuspendLayout();
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
            this.splitContainer2.Panel1.Controls.Add(this.directoryWithUnzipedFiles);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.tasksHostControl);
            this.splitContainer2.Size = new System.Drawing.Size(321, 301);
            this.splitContainer2.SplitterDistance = 229;
            this.splitContainer2.TabIndex = 0;
            // 
            // tasksHostControl
            // 
            this.tasksHostControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tasksHostControl.Location = new System.Drawing.Point(0, 0);
            this.tasksHostControl.Name = "tasksHostControl";
            this.tasksHostControl.Size = new System.Drawing.Size(317, 64);
            this.tasksHostControl.TabIndex = 0;
            // 
            // directoryWithUnzipedFiles
            //             
            this.directoryWithUnzipedFiles._ProcessDroppedObjects = true;
            this.directoryWithUnzipedFiles._ShowFileSize = false;
            this.directoryWithUnzipedFiles._ShowLinkToUpperFolder = true;
            this.directoryWithUnzipedFiles._ViewMode = ascx_Directory.ViewMode.Simple_With_LocationBar;
            this.directoryWithUnzipedFiles.BackColor = System.Drawing.SystemColors.Control;
            this.directoryWithUnzipedFiles.Dock = System.Windows.Forms.DockStyle.Fill;
            this.directoryWithUnzipedFiles.ForeColor = System.Drawing.Color.Black;
            this.directoryWithUnzipedFiles.Location = new System.Drawing.Point(0, 0);
            this.directoryWithUnzipedFiles.Name = "directoryWithUnzipedFiles";
            this.directoryWithUnzipedFiles.Size = new System.Drawing.Size(317, 225);
            this.directoryWithUnzipedFiles.TabIndex = 0;
            this.directoryWithUnzipedFiles.Load += new System.EventHandler(this.directoryWithUnzipedFiles_Load);
            // 
            // ascx_Unzip
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer2);
            this.Name = "ascx_Unzip";
            this.Size = new System.Drawing.Size(321, 301);
            this.Load += new System.EventHandler(this.ascx_Unzip_Load);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer2;
        private ascx_Directory directoryWithUnzipedFiles;
        private System.Windows.Forms.FlowLayoutPanel tasksHostControl;
    }
}
