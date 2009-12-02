using O2.Views.ASCX.CoreControls;

namespace O2.Rnd.Tool.ScanQueue.Ascx
{
    partial class ascx_ScanQueue
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
            this.cbStartFileWatch_DropQueue = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.Test = new System.Windows.Forms.Button();
            this.btProcessScanQueue = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.directoryScanResults = new ascx_Directory();
            this.directoryScanQueue = new ascx_Directory();
            this.directoryDropQueue = new ascx_Directory();
            this.btProcessDropQueue = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Scan Queue";
            // 
            // cbStartFileWatch_DropQueue
            // 
            this.cbStartFileWatch_DropQueue.AutoSize = true;
            this.cbStartFileWatch_DropQueue.Location = new System.Drawing.Point(23, 37);
            this.cbStartFileWatch_DropQueue.Name = "cbStartFileWatch_DropQueue";
            this.cbStartFileWatch_DropQueue.Size = new System.Drawing.Size(80, 17);
            this.cbStartFileWatch_DropQueue.TabIndex = 1;
            this.cbStartFileWatch_DropQueue.Text = "checkBox1";
            this.cbStartFileWatch_DropQueue.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(188, 37);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "files to scan";
            // 
            // Test
            // 
            this.Test.Location = new System.Drawing.Point(191, 4);
            this.Test.Name = "Test";
            this.Test.Size = new System.Drawing.Size(75, 23);
            this.Test.TabIndex = 6;
            this.Test.Text = "Test";
            this.Test.UseVisualStyleBackColor = true;
            this.Test.Click += new System.EventHandler(this.Test_Click);
            // 
            // btProcessScanQueue
            // 
            this.btProcessScanQueue.Location = new System.Drawing.Point(321, 90);
            this.btProcessScanQueue.Name = "btProcessScanQueue";
            this.btProcessScanQueue.Size = new System.Drawing.Size(163, 23);
            this.btProcessScanQueue.TabIndex = 9;
            this.btProcessScanQueue.Text = "Process Scan Queue";
            this.btProcessScanQueue.UseVisualStyleBackColor = true;
            this.btProcessScanQueue.Click += new System.EventHandler(this.btProcessScanQueue_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(20, 74);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(62, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "DropQueue";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(330, 74);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(64, 13);
            this.label4.TabIndex = 11;
            this.label4.Text = "ScanQueue";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(596, 74);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(77, 13);
            this.label5.TabIndex = 12;
            this.label5.Text = "Results Queue";
            // 
            // directoryScanResults
            // 
            this.directoryScanResults._ProcessDroppedObjects = true;
            this.directoryScanResults._ShowFileSize = false;
            this.directoryScanResults._ShowLinkToUpperFolder = true;
            this.directoryScanResults._ViewMode = ascx_Directory.ViewMode.Advanced;
            this.directoryScanResults._WatchFolder = false;
            this.directoryScanResults.BackColor = System.Drawing.SystemColors.Control;
            this.directoryScanResults.ForeColor = System.Drawing.Color.Black;
            this.directoryScanResults.Location = new System.Drawing.Point(589, 119);
            this.directoryScanResults.Name = "directoryScanResults";
            this.directoryScanResults.Size = new System.Drawing.Size(194, 248);
            this.directoryScanResults.TabIndex = 8;
            // 
            // directoryScanQueue
            // 
            this.directoryScanQueue._ProcessDroppedObjects = true;
            this.directoryScanQueue._ShowFileSize = false;
            this.directoryScanQueue._ShowLinkToUpperFolder = true;
            this.directoryScanQueue._ViewMode = ascx_Directory.ViewMode.Advanced;
            this.directoryScanQueue._WatchFolder = false;
            this.directoryScanQueue.BackColor = System.Drawing.SystemColors.Control;
            this.directoryScanQueue.ForeColor = System.Drawing.Color.Black;
            this.directoryScanQueue.Location = new System.Drawing.Point(321, 119);
            this.directoryScanQueue.Name = "directoryScanQueue";
            this.directoryScanQueue.Size = new System.Drawing.Size(188, 248);
            this.directoryScanQueue.TabIndex = 7;
            // 
            // directoryDropQueue
            // 
            this.directoryDropQueue._ProcessDroppedObjects = true;
            this.directoryDropQueue._ShowFileSize = false;
            this.directoryDropQueue._ShowLinkToUpperFolder = true;
            this.directoryDropQueue._ViewMode = ascx_Directory.ViewMode.Advanced;
            this.directoryDropQueue._WatchFolder = false;
            this.directoryDropQueue.BackColor = System.Drawing.SystemColors.Control;
            this.directoryDropQueue.ForeColor = System.Drawing.Color.Black;
            this.directoryDropQueue.Location = new System.Drawing.Point(17, 119);
            this.directoryDropQueue.Name = "directoryDropQueue";
            this.directoryDropQueue.Size = new System.Drawing.Size(180, 248);
            this.directoryDropQueue.TabIndex = 5;
            // 
            // btProcessDropQueue
            // 
            this.btProcessDropQueue.Location = new System.Drawing.Point(17, 90);
            this.btProcessDropQueue.Name = "btProcessDropQueue";
            this.btProcessDropQueue.Size = new System.Drawing.Size(163, 23);
            this.btProcessDropQueue.TabIndex = 13;
            this.btProcessDropQueue.Text = "Process Drop Queue";
            this.btProcessDropQueue.UseVisualStyleBackColor = true;
            this.btProcessDropQueue.Click += new System.EventHandler(this.btProcessDropQueue_Click);
            // 
            // ascx_ScanQueue
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btProcessDropQueue);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btProcessScanQueue);
            this.Controls.Add(this.directoryScanResults);
            this.Controls.Add(this.directoryScanQueue);
            this.Controls.Add(this.Test);
            this.Controls.Add(this.directoryDropQueue);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cbStartFileWatch_DropQueue);
            this.Controls.Add(this.label1);
            this.Name = "ascx_ScanQueue";
            this.Size = new System.Drawing.Size(834, 388);
            this.Load += new System.EventHandler(this.ascx_ScanQueue_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox cbStartFileWatch_DropQueue;
        private System.Windows.Forms.Label label2;
        private ascx_Directory directoryDropQueue;
        private System.Windows.Forms.Button Test;
        private ascx_Directory directoryScanQueue;
        private ascx_Directory directoryScanResults;
        private System.Windows.Forms.Button btProcessScanQueue;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btProcessDropQueue;
    }
}