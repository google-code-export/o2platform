//---------------------------------------------------------------------
//  This file is part of the CLR Managed Debugger (mdbg) Sample.
// 
//  Copyright (C) Microsoft Corporation.  All rights reserved.
//---------------------------------------------------------------------

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using O2.Debugger.Mdbg.Debugging.MdbgEngine;
using O2.Debugger.Mdbg.Debugging.MdbgEngine;
using O2.Debugger.Mdbg.Debugging.MdbgEngine;

namespace O2.Debugger.Mdbg.Tools.Mdbg.Extension
{
    /// <summary>
    /// Summary description for Form1.
    /// </summary>
    public class AboutForm : Form
    {
        private Button closeBtn;

        /// <summary>
        /// Required designer variable.
        /// </summary>
        private Container components;

        private Label label1;
        private Label labelVersion;
        private RichTextBox richTextBox1;

        public AboutForm()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();

            Type tGui = typeof (AboutForm);
            Type tEngine = typeof (MDbgEngine);
            // Set version info
            labelVersion.Text =
                "Version:" +
                "Gui=" + tGui.Assembly.GetName().Version +
                ",  MdbgEng=" + tEngine.Assembly.GetName().Version;
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        //#region Windows Form Designer generated code
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            label1 = new Label();
            closeBtn = new Button();
            richTextBox1 = new RichTextBox();
            labelVersion = new Label();
            SuspendLayout();
// 
// label1
// 
            label1.Location = new Point(16, 8);
            label1.Name = "label1";
            label1.Size = new Size(328, 24);
            label1.TabIndex = 0;
            label1.Text = "GUI: Simple Extension for MDbg debugger";
// 
// closeBtn
// 
            closeBtn.Anchor = (((AnchorStyles.Bottom | AnchorStyles.Right)));
            closeBtn.DialogResult = DialogResult.Cancel;
            closeBtn.Location = new Point(292, 255);
            closeBtn.Name = "closeBtn";
            closeBtn.Size = new Size(80, 23);
            closeBtn.TabIndex = 1;
            closeBtn.Text = "Close";
            closeBtn.Click += closeBtn_Click;
// 
// richTextBox1
// 
            richTextBox1.Anchor = (((((AnchorStyles.Top | AnchorStyles.Bottom)
                                      | AnchorStyles.Left)
                                     | AnchorStyles.Right)));
            richTextBox1.Location = new Point(8, 53);
            richTextBox1.Name = "richTextBox1";
            richTextBox1.ReadOnly = true;
            richTextBox1.Size = new Size(364, 195);
            richTextBox1.TabIndex = 2;
            richTextBox1.Text =
                @"Defined Hotkeys:
F9  - Set / Remove Breakpoint at line
F10 - Step Over
F11 - Step Into
Shift+F11 - Step Out

Visit the MDbg forum (http://forums.microsoft.com/MSDN/ShowForum.aspx?ForumID=868&SiteID=1) for MDbg issues or further questions about writing your own .NET diagnostic tools.
";
            richTextBox1.LinkClicked += richTextBox1_LinkClicked;
            richTextBox1.TextChanged += richTextBox1_TextChanged;
// 
// labelVersion
// 
            labelVersion.AutoSize = true;
            labelVersion.Location = new Point(16, 32);
            labelVersion.Name = "labelVersion";
            labelVersion.Size = new Size(43, 14);
            labelVersion.TabIndex = 3;
            labelVersion.Text = "Version";
// 
// AboutForm
// 
            AcceptButton = closeBtn;
            AutoScaleBaseSize = new Size(5, 13);
            CancelButton = closeBtn;
            ClientSize = new Size(384, 290);
            Controls.Add(labelVersion);
            Controls.Add(richTextBox1);
            Controls.Add(closeBtn);
            Controls.Add(label1);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "AboutForm";
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "About";
            ResumeLayout(false);
            PerformLayout();
        }

        //#endregion

        private void closeBtn_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
        }

        // Launch the target of the link
        private void richTextBox1_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            string target = e.LinkText;

            try
            {
                // More general start.
                Process.Start(e.LinkText);

                return;
            }
            catch (Exception)
            {
                // Swallow exception.
            }

            // Try again.
            // This is silly. Start() appears to look at the extension.
            // But URLs are based off the prefix (http) and may have any random extension.
            // So try again and explicitly use IE.
            try
            {
                Process.Start("IExplore.exe", target);
                return;
            }
            catch (Exception)
            {
                // Swallow exception.
            }
        }
    }
}