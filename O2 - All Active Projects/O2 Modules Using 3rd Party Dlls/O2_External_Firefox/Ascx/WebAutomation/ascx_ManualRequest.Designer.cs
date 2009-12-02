// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
namespace O2.External.Firefox.Ascx.WebAutomation
{
    partial class ascx_ManualRequest
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
            this.tbManualRequestContents = new System.Windows.Forms.TextBox();
            this.btOpenManualRequest = new System.Windows.Forms.Button();
            this.tbManualRequestUrl = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // tbManualRequestContents
            // 
            this.tbManualRequestContents.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                                                                                         | System.Windows.Forms.AnchorStyles.Left)
                                                                                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbManualRequestContents.Location = new System.Drawing.Point(3, 29);
            this.tbManualRequestContents.Multiline = true;
            this.tbManualRequestContents.Name = "tbManualRequestContents";
            this.tbManualRequestContents.Size = new System.Drawing.Size(634, 309);
            this.tbManualRequestContents.TabIndex = 10;
            // 
            // btOpenManualRequest
            // 
            this.btOpenManualRequest.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btOpenManualRequest.Location = new System.Drawing.Point(430, 3);
            this.btOpenManualRequest.Name = "btOpenManualRequest";
            this.btOpenManualRequest.Size = new System.Drawing.Size(59, 20);
            this.btOpenManualRequest.TabIndex = 9;
            this.btOpenManualRequest.Text = "open";
            this.btOpenManualRequest.UseVisualStyleBackColor = true;
            this.btOpenManualRequest.Click += new System.EventHandler(this.btOpenManualRequest_Click);
            // 
            // tbManualRequestUrl
            // 
            this.tbManualRequestUrl.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.tbManualRequestUrl.ForeColor = System.Drawing.Color.White;
            this.tbManualRequestUrl.Location = new System.Drawing.Point(0, 3);
            this.tbManualRequestUrl.Name = "tbManualRequestUrl";
            this.tbManualRequestUrl.Size = new System.Drawing.Size(424, 20);
            this.tbManualRequestUrl.TabIndex = 8;
            // 
            // ascx_ManualRequest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tbManualRequestContents);
            this.Controls.Add(this.btOpenManualRequest);
            this.Controls.Add(this.tbManualRequestUrl);
            this.Name = "ascx_ManualRequest";
            this.Size = new System.Drawing.Size(640, 338);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbManualRequestContents;
        private System.Windows.Forms.Button btOpenManualRequest;
        private System.Windows.Forms.TextBox tbManualRequestUrl;
    }
}
