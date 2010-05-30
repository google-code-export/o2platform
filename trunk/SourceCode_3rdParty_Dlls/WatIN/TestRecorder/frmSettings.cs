using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DemoApp
{
    public partial class frmSettings : Form
    {
        public frmSettings()
        {
            InitializeComponent();
        }

        public void ObjectToGUI(AppSettings settings)
        {
            txtMainBrowserName.Text = settings.BaseIEName;
            txtPopupBrowserName.Text = settings.PopupIEName;
            txtCompilePath.Text = settings.CompilePath;
            numTypeTime.Value = Convert.ToDecimal(settings.TypingTime);
            pnlDOMColor.BackColor = settings.DOMHighlightColor;
            lblSourceFont.Font = settings.ScriptWindowFont;
            chkWarnUnsaved.Checked = settings.WarnWhenUnsaved;
            chkHideDOSWindow.Checked = settings.HideDOSWindow;

            switch (settings.CodeLanguage)
            {
                case AppSettings.CodeLanguages.CSharp: rbCSharp.Checked = true; break;
                case AppSettings.CodeLanguages.VBNet: rbVBNet.Checked = true; break;
                case AppSettings.CodeLanguages.PHP: rbPHP.Checked = true; break;
                case AppSettings.CodeLanguages.Python: rbPython.Checked = true; break;
                case AppSettings.CodeLanguages.Perl: rbPerl.Checked = true; break;
            }
        }

        public void GUIToObject(AppSettings settings)
        {
            settings.BaseIEName = txtMainBrowserName.Text;
            settings.PopupIEName = txtPopupBrowserName.Text;
            settings.CompilePath = txtCompilePath.Text;
            settings.TypingTime = Convert.ToDouble(numTypeTime.Value);
            settings.DOMHighlightColor = pnlDOMColor.BackColor;
            settings.ScriptWindowFont = lblSourceFont.Font;
            settings.WarnWhenUnsaved = chkWarnUnsaved.Checked;
            settings.HideDOSWindow = chkHideDOSWindow.Checked;

            if (rbCSharp.Checked)
            {
                settings.CodeLanguage = AppSettings.CodeLanguages.CSharp;
            }
            else if (rbVBNet.Checked)
            {
                settings.CodeLanguage = AppSettings.CodeLanguages.VBNet;
            }
            else if (rbPHP.Checked)
            {
                settings.CodeLanguage = AppSettings.CodeLanguages.PHP;
            }
            else if (rbPython.Checked)
            {
                settings.CodeLanguage = AppSettings.CodeLanguages.Python;
            }
            else if (rbPerl.Checked)
            {
                settings.CodeLanguage = AppSettings.CodeLanguages.Perl;
            }
        }

        private void btnDOMColor_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog()==DialogResult.Cancel)
            {
                return;
            }
            pnlDOMColor.BackColor = colorDialog1.Color;
        }

        private void btnCodeFont_Click(object sender, EventArgs e)
        {
            if (fontDialog1.ShowDialog()==DialogResult.Cancel)
            {
                return;
            }
            lblSourceFont.Font = fontDialog1.Font;
        }

        private void btnCompilePath_Click(object sender, EventArgs e)
        {
            if (fbCompilePathDialog.ShowDialog()==DialogResult.Cancel)
            {
                return;
            }
            txtCompilePath.Text = fbCompilePathDialog.SelectedPath+"\\";
        }
    }
}