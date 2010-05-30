namespace DemoApp
{
    partial class frmSettings
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSettings));
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.fontDialog1 = new System.Windows.Forms.FontDialog();
            this.openNUnitDialog = new System.Windows.Forms.OpenFileDialog();
            this.fbCompilePathDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.pageGeneral = new System.Windows.Forms.TabPage();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lblSourceFont = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.chkWarnUnsaved = new System.Windows.Forms.CheckBox();
            this.label7 = new System.Windows.Forms.Label();
            this.btnCodeFont = new System.Windows.Forms.Button();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.label6 = new System.Windows.Forms.Label();
            this.btnDOMColor = new System.Windows.Forms.Button();
            this.pnlDOMColor = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.numTypeTime = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.txtPopupBrowserName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtMainBrowserName = new System.Windows.Forms.TextBox();
            this.pageCompiler = new System.Windows.Forms.TabPage();
            this.rbPython = new System.Windows.Forms.RadioButton();
            this.rbPHP = new System.Windows.Forms.RadioButton();
            this.rbVBNet = new System.Windows.Forms.RadioButton();
            this.rbCSharp = new System.Windows.Forms.RadioButton();
            this.label4 = new System.Windows.Forms.Label();
            this.chkHideDOSWindow = new System.Windows.Forms.CheckBox();
            this.btnCompilePath = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.txtCompilePath = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnDone = new System.Windows.Forms.Button();
            this.openAssemblyDialog = new System.Windows.Forms.OpenFileDialog();
            this.openMBUnitDialog = new System.Windows.Forms.OpenFileDialog();
            this.openVS05Dialog = new System.Windows.Forms.OpenFileDialog();
            this.rbPerl = new System.Windows.Forms.RadioButton();
            this.tabControl1.SuspendLayout();
            this.pageGeneral.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numTypeTime)).BeginInit();
            this.pageCompiler.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // openNUnitDialog
            // 
            this.openNUnitDialog.FileName = "NUnit.Framework.dll";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.pageGeneral);
            this.tabControl1.Controls.Add(this.pageCompiler);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(561, 409);
            this.tabControl1.TabIndex = 24;
            // 
            // pageGeneral
            // 
            this.pageGeneral.Controls.Add(this.panel2);
            this.pageGeneral.Controls.Add(this.label10);
            this.pageGeneral.Controls.Add(this.label9);
            this.pageGeneral.Controls.Add(this.chkWarnUnsaved);
            this.pageGeneral.Controls.Add(this.label7);
            this.pageGeneral.Controls.Add(this.btnCodeFont);
            this.pageGeneral.Controls.Add(this.label6);
            this.pageGeneral.Controls.Add(this.btnDOMColor);
            this.pageGeneral.Controls.Add(this.pnlDOMColor);
            this.pageGeneral.Controls.Add(this.label5);
            this.pageGeneral.Controls.Add(this.numTypeTime);
            this.pageGeneral.Controls.Add(this.label2);
            this.pageGeneral.Controls.Add(this.txtPopupBrowserName);
            this.pageGeneral.Controls.Add(this.label1);
            this.pageGeneral.Controls.Add(this.txtMainBrowserName);
            this.pageGeneral.Location = new System.Drawing.Point(4, 22);
            this.pageGeneral.Name = "pageGeneral";
            this.pageGeneral.Padding = new System.Windows.Forms.Padding(3);
            this.pageGeneral.Size = new System.Drawing.Size(553, 383);
            this.pageGeneral.TabIndex = 0;
            this.pageGeneral.Text = "General";
            this.pageGeneral.UseVisualStyleBackColor = true;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.lblSourceFont);
            this.panel2.Location = new System.Drawing.Point(122, 150);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(144, 28);
            this.panel2.TabIndex = 35;
            // 
            // lblSourceFont
            // 
            this.lblSourceFont.AutoSize = true;
            this.lblSourceFont.Location = new System.Drawing.Point(3, 10);
            this.lblSourceFont.Name = "lblSourceFont";
            this.lblSourceFont.Size = new System.Drawing.Size(93, 13);
            this.lblSourceFont.TabIndex = 32;
            this.lblSourceFont.Text = "Source Code Font";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(232, 40);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(203, 13);
            this.label10.TabIndex = 34;
            this.label10.Text = "Variable name to use for a popup browser";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(232, 14);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(215, 13);
            this.label9.TabIndex = 33;
            this.label9.Text = "Variable name to use for the primary browser";
            // 
            // chkWarnUnsaved
            // 
            this.chkWarnUnsaved.AutoSize = true;
            this.chkWarnUnsaved.Location = new System.Drawing.Point(123, 216);
            this.chkWarnUnsaved.Name = "chkWarnUnsaved";
            this.chkWarnUnsaved.Size = new System.Drawing.Size(268, 17);
            this.chkWarnUnsaved.TabIndex = 5;
            this.chkWarnUnsaved.Text = "Warn when code is unsaved and window is closing";
            this.chkWarnUnsaved.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(229, 72);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(271, 13);
            this.label7.TabIndex = 31;
            this.label7.Text = "Milliseconds of time to wait after last key to record typing";
            // 
            // btnCodeFont
            // 
            this.btnCodeFont.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCodeFont.ImageIndex = 6;
            this.btnCodeFont.ImageList = this.imageList1;
            this.btnCodeFont.Location = new System.Drawing.Point(272, 155);
            this.btnCodeFont.Name = "btnCodeFont";
            this.btnCodeFont.Size = new System.Drawing.Size(148, 23);
            this.btnCodeFont.TabIndex = 4;
            this.btnCodeFont.Text = "Change Source Font...";
            this.btnCodeFont.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnCodeFont.UseVisualStyleBackColor = true;
            this.btnCodeFont.Click += new System.EventHandler(this.btnCodeFont_Click);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Fuchsia;
            this.imageList1.Images.SetKeyName(0, "Tick.bmp");
            this.imageList1.Images.SetKeyName(1, "Delete.bmp");
            this.imageList1.Images.SetKeyName(2, "Plus.bmp");
            this.imageList1.Images.SetKeyName(3, "Minus.bmp");
            this.imageList1.Images.SetKeyName(4, "Folder.bmp");
            this.imageList1.Images.SetKeyName(5, "Colour Scheme.bmp");
            this.imageList1.Images.SetKeyName(6, "Format-Font.bmp");
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(5, 119);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(75, 13);
            this.label6.TabIndex = 28;
            this.label6.Text = "Highlight Color";
            // 
            // btnDOMColor
            // 
            this.btnDOMColor.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDOMColor.ImageIndex = 5;
            this.btnDOMColor.ImageList = this.imageList1;
            this.btnDOMColor.Location = new System.Drawing.Point(272, 109);
            this.btnDOMColor.Name = "btnDOMColor";
            this.btnDOMColor.Size = new System.Drawing.Size(148, 23);
            this.btnDOMColor.TabIndex = 3;
            this.btnDOMColor.Text = "Change Color...";
            this.btnDOMColor.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnDOMColor.UseVisualStyleBackColor = true;
            this.btnDOMColor.Click += new System.EventHandler(this.btnDOMColor_Click);
            // 
            // pnlDOMColor
            // 
            this.pnlDOMColor.BackColor = System.Drawing.Color.Yellow;
            this.pnlDOMColor.Location = new System.Drawing.Point(122, 109);
            this.pnlDOMColor.Name = "pnlDOMColor";
            this.pnlDOMColor.Size = new System.Drawing.Size(100, 23);
            this.pnlDOMColor.TabIndex = 26;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 77);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(112, 13);
            this.label5.TabIndex = 25;
            this.label5.Text = "Maximum Typing Time";
            // 
            // numTypeTime
            // 
            this.numTypeTime.Increment = new decimal(new int[] {
            250,
            0,
            0,
            0});
            this.numTypeTime.Location = new System.Drawing.Point(123, 70);
            this.numTypeTime.Maximum = new decimal(new int[] {
            3000,
            0,
            0,
            0});
            this.numTypeTime.Minimum = new decimal(new int[] {
            250,
            0,
            0,
            0});
            this.numTypeTime.Name = "numTypeTime";
            this.numTypeTime.Size = new System.Drawing.Size(100, 20);
            this.numTypeTime.TabIndex = 2;
            this.numTypeTime.Value = new decimal(new int[] {
            250,
            0,
            0,
            0});
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 40);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(110, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Popup Browser Name";
            // 
            // txtPopupBrowserName
            // 
            this.txtPopupBrowserName.Location = new System.Drawing.Point(123, 37);
            this.txtPopupBrowserName.Name = "txtPopupBrowserName";
            this.txtPopupBrowserName.Size = new System.Drawing.Size(100, 20);
            this.txtPopupBrowserName.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(102, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Main Browser Name";
            // 
            // txtMainBrowserName
            // 
            this.txtMainBrowserName.Location = new System.Drawing.Point(123, 11);
            this.txtMainBrowserName.Name = "txtMainBrowserName";
            this.txtMainBrowserName.Size = new System.Drawing.Size(100, 20);
            this.txtMainBrowserName.TabIndex = 0;
            // 
            // pageCompiler
            // 
            this.pageCompiler.Controls.Add(this.rbPerl);
            this.pageCompiler.Controls.Add(this.rbPython);
            this.pageCompiler.Controls.Add(this.rbPHP);
            this.pageCompiler.Controls.Add(this.rbVBNet);
            this.pageCompiler.Controls.Add(this.rbCSharp);
            this.pageCompiler.Controls.Add(this.label4);
            this.pageCompiler.Controls.Add(this.chkHideDOSWindow);
            this.pageCompiler.Controls.Add(this.btnCompilePath);
            this.pageCompiler.Controls.Add(this.label3);
            this.pageCompiler.Controls.Add(this.txtCompilePath);
            this.pageCompiler.Location = new System.Drawing.Point(4, 22);
            this.pageCompiler.Name = "pageCompiler";
            this.pageCompiler.Padding = new System.Windows.Forms.Padding(3);
            this.pageCompiler.Size = new System.Drawing.Size(553, 383);
            this.pageCompiler.TabIndex = 1;
            this.pageCompiler.Text = "Language";
            this.pageCompiler.UseVisualStyleBackColor = true;
            // 
            // rbPython
            // 
            this.rbPython.AutoSize = true;
            this.rbPython.Location = new System.Drawing.Point(90, 168);
            this.rbPython.Name = "rbPython";
            this.rbPython.Size = new System.Drawing.Size(58, 17);
            this.rbPython.TabIndex = 41;
            this.rbPython.TabStop = true;
            this.rbPython.Text = "Python";
            this.rbPython.UseVisualStyleBackColor = true;
            // 
            // rbPHP
            // 
            this.rbPHP.AutoSize = true;
            this.rbPHP.Location = new System.Drawing.Point(90, 145);
            this.rbPHP.Name = "rbPHP";
            this.rbPHP.Size = new System.Drawing.Size(47, 17);
            this.rbPHP.TabIndex = 40;
            this.rbPHP.TabStop = true;
            this.rbPHP.Text = "PHP";
            this.rbPHP.UseVisualStyleBackColor = true;
            // 
            // rbVBNet
            // 
            this.rbVBNet.AutoSize = true;
            this.rbVBNet.Location = new System.Drawing.Point(90, 122);
            this.rbVBNet.Name = "rbVBNet";
            this.rbVBNet.Size = new System.Drawing.Size(64, 17);
            this.rbVBNet.TabIndex = 39;
            this.rbVBNet.TabStop = true;
            this.rbVBNet.Text = "VB.NET";
            this.rbVBNet.UseVisualStyleBackColor = true;
            // 
            // rbCSharp
            // 
            this.rbCSharp.AutoSize = true;
            this.rbCSharp.Checked = true;
            this.rbCSharp.Location = new System.Drawing.Point(90, 99);
            this.rbCSharp.Name = "rbCSharp";
            this.rbCSharp.Size = new System.Drawing.Size(39, 17);
            this.rbCSharp.TabIndex = 38;
            this.rbCSharp.TabStop = true;
            this.rbCSharp.Text = "C#";
            this.rbCSharp.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(11, 101);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(55, 13);
            this.label4.TabIndex = 37;
            this.label4.Text = "Language";
            // 
            // chkHideDOSWindow
            // 
            this.chkHideDOSWindow.AutoSize = true;
            this.chkHideDOSWindow.Location = new System.Drawing.Point(14, 16);
            this.chkHideDOSWindow.Name = "chkHideDOSWindow";
            this.chkHideDOSWindow.Size = new System.Drawing.Size(113, 17);
            this.chkHideDOSWindow.TabIndex = 36;
            this.chkHideDOSWindow.Text = "Hide Run Window";
            this.chkHideDOSWindow.UseVisualStyleBackColor = true;
            // 
            // btnCompilePath
            // 
            this.btnCompilePath.ImageIndex = 4;
            this.btnCompilePath.ImageList = this.imageList1;
            this.btnCompilePath.Location = new System.Drawing.Point(510, 47);
            this.btnCompilePath.Name = "btnCompilePath";
            this.btnCompilePath.Size = new System.Drawing.Size(26, 23);
            this.btnCompilePath.TabIndex = 8;
            this.btnCompilePath.Text = "...";
            this.btnCompilePath.UseVisualStyleBackColor = true;
            this.btnCompilePath.Click += new System.EventHandler(this.btnCompilePath_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(11, 52);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(69, 13);
            this.label3.TabIndex = 22;
            this.label3.Text = "Compile Path";
            // 
            // txtCompilePath
            // 
            this.txtCompilePath.Location = new System.Drawing.Point(90, 49);
            this.txtCompilePath.Name = "txtCompilePath";
            this.txtCompilePath.ReadOnly = true;
            this.txtCompilePath.Size = new System.Drawing.Size(414, 20);
            this.txtCompilePath.TabIndex = 21;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnCancel);
            this.panel1.Controls.Add(this.btnDone);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 355);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(561, 54);
            this.panel1.TabIndex = 25;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCancel.ImageIndex = 1;
            this.btnCancel.ImageList = this.imageList1;
            this.btnCancel.Location = new System.Drawing.Point(116, 16);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 7;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnDone
            // 
            this.btnDone.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnDone.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDone.ImageIndex = 0;
            this.btnDone.ImageList = this.imageList1;
            this.btnDone.Location = new System.Drawing.Point(9, 16);
            this.btnDone.Name = "btnDone";
            this.btnDone.Size = new System.Drawing.Size(75, 23);
            this.btnDone.TabIndex = 6;
            this.btnDone.Text = "Done";
            this.btnDone.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnDone.UseVisualStyleBackColor = true;
            // 
            // openAssemblyDialog
            // 
            this.openAssemblyDialog.DefaultExt = "dll";
            this.openAssemblyDialog.Filter = ".Net Assembly|*.dll";
            // 
            // openMBUnitDialog
            // 
            this.openMBUnitDialog.FileName = "MbUnit.Framework.dll";
            // 
            // openVS05Dialog
            // 
            this.openVS05Dialog.FileName = "Microsoft.VisualStudio.QualityTools.UnitTestFramework.dll";
            // 
            // rbPerl
            // 
            this.rbPerl.AutoSize = true;
            this.rbPerl.Location = new System.Drawing.Point(90, 191);
            this.rbPerl.Name = "rbPerl";
            this.rbPerl.Size = new System.Drawing.Size(43, 17);
            this.rbPerl.TabIndex = 42;
            this.rbPerl.TabStop = true;
            this.rbPerl.Text = "Perl";
            this.rbPerl.UseVisualStyleBackColor = true;
            // 
            // frmSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(561, 409);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.tabControl1);
            this.Name = "frmSettings";
            this.Text = "Settings";
            this.tabControl1.ResumeLayout(false);
            this.pageGeneral.ResumeLayout(false);
            this.pageGeneral.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numTypeTime)).EndInit();
            this.pageCompiler.ResumeLayout(false);
            this.pageCompiler.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.FontDialog fontDialog1;
        private System.Windows.Forms.OpenFileDialog openNUnitDialog;
        private System.Windows.Forms.FolderBrowserDialog fbCompilePathDialog;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage pageGeneral;
        private System.Windows.Forms.CheckBox chkWarnUnsaved;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btnCodeFont;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnDOMColor;
        private System.Windows.Forms.Panel pnlDOMColor;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown numTypeTime;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtPopupBrowserName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtMainBrowserName;
        private System.Windows.Forms.TabPage pageCompiler;
        private System.Windows.Forms.Button btnCompilePath;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtCompilePath;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnDone;
        private System.Windows.Forms.OpenFileDialog openAssemblyDialog;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label lblSourceFont;
        private System.Windows.Forms.CheckBox chkHideDOSWindow;
        private System.Windows.Forms.OpenFileDialog openMBUnitDialog;
        private System.Windows.Forms.OpenFileDialog openVS05Dialog;
        private System.Windows.Forms.RadioButton rbVBNet;
        private System.Windows.Forms.RadioButton rbCSharp;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.RadioButton rbPHP;
        private System.Windows.Forms.RadioButton rbPython;
        private System.Windows.Forms.RadioButton rbPerl;
    }
}