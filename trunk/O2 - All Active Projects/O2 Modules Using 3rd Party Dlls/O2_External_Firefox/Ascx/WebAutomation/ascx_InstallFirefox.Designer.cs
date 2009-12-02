// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
namespace O2.External.Firefox.Ascx.WebAutomation
{
    partial class ascx_InstallFirefox
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
            this.tbPathToFirefoxDirectory = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.wbInstallFirefox = new System.Windows.Forms.WebBrowser();
            this.btAfterInstall = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label1.Location = new System.Drawing.Point(5, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(387, 57);
            this.label1.TabIndex = 0;
            this.label1.Text = "Firefox doesn\'t seem to be installed on this computer. Please install it from the" +
                               " link below, or provide a path to its installation";
            // 
            // tbPathToFirefoxDirectory
            // 
            this.tbPathToFirefoxDirectory.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                                                                                         | System.Windows.Forms.AnchorStyles.Right)));
            this.tbPathToFirefoxDirectory.Location = new System.Drawing.Point(157, 68);
            this.tbPathToFirefoxDirectory.Name = "tbPathToFirefoxDirectory";
            this.tbPathToFirefoxDirectory.Size = new System.Drawing.Size(352, 20);
            this.tbPathToFirefoxDirectory.TabIndex = 51;
            this.tbPathToFirefoxDirectory.TextChanged += new System.EventHandler(this.tbPathToFirefoxDirectory_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(5, 71);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(142, 13);
            this.label2.TabIndex = 50;
            this.label2.Text = "Path to Firefox Installation dir";
            // 
            // wbInstallFirefox
            // 
            this.wbInstallFirefox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                                                                                  | System.Windows.Forms.AnchorStyles.Left)
                                                                                 | System.Windows.Forms.AnchorStyles.Right)));
            this.wbInstallFirefox.Location = new System.Drawing.Point(3, 101);
            this.wbInstallFirefox.MinimumSize = new System.Drawing.Size(20, 20);
            this.wbInstallFirefox.Name = "wbInstallFirefox";
            this.wbInstallFirefox.Size = new System.Drawing.Size(506, 335);
            this.wbInstallFirefox.TabIndex = 52;
            // 
            // btAfterInstall
            // 
            this.btAfterInstall.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btAfterInstall.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btAfterInstall.Location = new System.Drawing.Point(515, 101);
            this.btAfterInstall.Name = "btAfterInstall";
            this.btAfterInstall.Size = new System.Drawing.Size(96, 79);
            this.btAfterInstall.TabIndex = 53;
            this.btAfterInstall.Text = "After Install Click Here";
            this.btAfterInstall.UseVisualStyleBackColor = true;
            this.btAfterInstall.Click += new System.EventHandler(this.btAfterInstall_Click);
            // 
            // ascx_InstallFirefox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btAfterInstall);
            this.Controls.Add(this.wbInstallFirefox);
            this.Controls.Add(this.tbPathToFirefoxDirectory);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "ascx_InstallFirefox";
            this.Size = new System.Drawing.Size(614, 439);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbPathToFirefoxDirectory;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.WebBrowser wbInstallFirefox;
        private System.Windows.Forms.Button btAfterInstall;
    }
}
