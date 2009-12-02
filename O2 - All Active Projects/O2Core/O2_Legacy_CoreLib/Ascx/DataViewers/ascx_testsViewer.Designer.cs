namespace O2.Legacy.CoreLib.Ascx
{
    partial class ascx_testsViewer
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label1 = new System.Windows.Forms.Label();
            this.lbTestName = new System.Windows.Forms.Label();
            this.lbPassedText = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lbFailedText = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.dgvTests = new System.Windows.Forms.DataGridView();
            this.Tests = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.helpProvider1 = new System.Windows.Forms.HelpProvider();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTests)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(117, 24);
            this.label1.TabIndex = 0;
            this.label1.Text = "Test Name:";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // lbTestName
            // 
            this.lbTestName.AutoSize = true;
            this.lbTestName.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTestName.Location = new System.Drawing.Point(141, 0);
            this.lbTestName.Name = "lbTestName";
            this.lbTestName.Size = new System.Drawing.Size(28, 24);
            this.lbTestName.TabIndex = 1;
            this.lbTestName.Text = "...";
            this.lbTestName.Click += new System.EventHandler(this.label2_Click);
            // 
            // lbPassedText
            // 
            this.lbPassedText.AutoSize = true;
            this.lbPassedText.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbPassedText.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.lbPassedText.Location = new System.Drawing.Point(77, 35);
            this.lbPassedText.Name = "lbPassedText";
            this.lbPassedText.Size = new System.Drawing.Size(24, 20);
            this.lbPassedText.TabIndex = 3;
            this.lbPassedText.Text = "...";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.label3.Location = new System.Drawing.Point(3, 35);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(68, 20);
            this.label3.TabIndex = 2;
            this.label3.Text = "Passed";
            // 
            // lbFailedText
            // 
            this.lbFailedText.AutoSize = true;
            this.lbFailedText.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbFailedText.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lbFailedText.Location = new System.Drawing.Point(216, 35);
            this.lbFailedText.Name = "lbFailedText";
            this.lbFailedText.Size = new System.Drawing.Size(24, 20);
            this.lbFailedText.TabIndex = 5;
            this.lbFailedText.Text = "...";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label5.Location = new System.Drawing.Point(142, 35);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(58, 20);
            this.label5.TabIndex = 4;
            this.label5.Text = "Failed";
            // 
            // dgvTests
            // 
            this.dgvTests.AllowUserToAddRows = false;
            this.dgvTests.AllowUserToDeleteRows = false;
            this.dgvTests.AllowUserToOrderColumns = true;
            this.dgvTests.AllowUserToResizeColumns = false;
            this.dgvTests.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.Gainsboro;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.Black;
            this.dgvTests.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvTests.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvTests.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvTests.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTests.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Tests});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.Gray;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvTests.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvTests.Location = new System.Drawing.Point(8, 58);
            this.dgvTests.Name = "dgvTests";
            this.dgvTests.RowHeadersWidth = 5;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.Transparent;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.White;
            this.dgvTests.RowsDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvTests.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvTests.Size = new System.Drawing.Size(528, 327);
            this.dgvTests.TabIndex = 65;
            // 
            // Tests
            // 
            this.Tests.HeaderText = "Tests";
            this.Tests.Name = "Tests";
            // 
            // ascx_testsViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.dgvTests);
            this.Controls.Add(this.lbFailedText);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.lbPassedText);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lbTestName);
            this.Controls.Add(this.label1);
            this.ForeColor = System.Drawing.Color.Black;
            this.Name = "ascx_testsViewer";
            this.Size = new System.Drawing.Size(543, 392);
            ((System.ComponentModel.ISupportInitialize)(this.dgvTests)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lbTestName;
        private System.Windows.Forms.Label lbPassedText;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lbFailedText;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DataGridView dgvTests;
        private System.Windows.Forms.DataGridViewTextBoxColumn Tests;
        private System.Windows.Forms.HelpProvider helpProvider1;
    }
}
