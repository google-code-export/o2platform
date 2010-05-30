using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using IfacesEnumsStructsClasses;

namespace DemoApp
{
    public partial class frmPopup : Form
    {
        private frmMain m_MainForm = null;
        private frmPopup newfrm = null;
        private WatinScript MainScript = null;
        private IHTMLElement KeyEntryElement = null;
        private bool ShiftKeyDown = false;
        private DateTime LastKeyTime = DateTime.MinValue;
        private bool ControlKeyDown = false;
        public string PopupName = "";
        public bool isSecondaryPopup = false;
        
        public frmPopup()
        {
            InitializeComponent();
        }

        public void AssignBrowserObject(ref object obj)
        {
            obj = this.cEXWB1.WebbrowserObject;
        }

        private void frmPopup_Load(object sender, EventArgs e)
        {
            //this.cEXWB1.RegisterAsBrowser = true;
            this.cEXWB1.WBDragDrop += new csExWB.WBDropEventHandler(cEXWB1_WBDragDrop);
            this.cEXWB1.NavToBlank();
        }

        public void SetURL(frmMain MainForm, WatinScript script, string URL)
        {
            this.MainScript = script;
            this.m_MainForm = MainForm;
            this.cEXWB1.Navigate(URL);
        }

        public IHTMLElement GetActiveElement()
        {
            IHTMLElement element = null;
            try
            {
                element = this.cEXWB1.GetActiveElement();
            }
            catch (Exception ex)
            {
                AllForms.m_frmLog.AppendToLog("Get Active Element Failure\r\n" + ex.ToString());
            }
            return element;
        }

        void cEXWB1_WBDragDrop(object sender, csExWB.WBDropEventArgs e)
        {
            if (e.dataobject.ContainsText())
                AllForms.m_frmLog.AppendToLog("frmPopup_cEXWB1_WBDragDrop\r\n" + e.dataobject.GetText());
            else if (e.dataobject.ContainsFileDropList())
            {
                System.Collections.Specialized.StringCollection files = e.dataobject.GetFileDropList();
                if (files.Count > 1)
                    MessageBox.Show(Properties.Resources.CanNotDoMultiFileDrop);
                else
                {
                    this.cEXWB1.Navigate(files[0]);
                }
            }
        }

        private void frmPopup_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (e.CloseReason == CloseReason.UserClosing)
                {
                    MainScript.AddClosePopup(PopupName);
                    if (this.cEXWB1 != null)
                        this.cEXWB1.NavToBlank();
                    timerActiveElement.Enabled = false;

                    if (!isSecondaryPopup)
                    {
                        e.Cancel = true;
                        this.Hide();
                    }
                }
            }
            catch
            {
                // dump it
            }
        }

        /// <summary>
        /// Sets the DL flags of the browser to be as restrictive as possible
        /// </summary>
        public void SetupAsSandBox()
        {
            this.cEXWB1.WBDOCDOWNLOADCTLFLAG = (int)(
                DOCDOWNLOADCTLFLAG.NO_SCRIPTS |
                DOCDOWNLOADCTLFLAG.NO_JAVA |
                DOCDOWNLOADCTLFLAG.NOFRAMES |
                DOCDOWNLOADCTLFLAG.NO_DLACTIVEXCTLS |
                DOCDOWNLOADCTLFLAG.NO_RUNACTIVEXCTLS |
                DOCDOWNLOADCTLFLAG.PRAGMA_NO_CACHE |
                DOCDOWNLOADCTLFLAG.SILENT |
                DOCDOWNLOADCTLFLAG.NO_BEHAVIORS |
                DOCDOWNLOADCTLFLAG.NO_CLIENTPULL);
        }

        private void cEXWB1_StatusTextChange(object sender, csExWB.StatusTextChangeEventArgs e)
        {
            lblStatus.Text = e.text;
        }

        private void cEXWB1_TitleChange(object sender, csExWB.TitleChangeEventArgs e)
        {
            this.Text = e.title;
        }

        private void cEXWB1_WBDocHostShowUIShowMessage(object sender, csExWB.DocHostShowUIShowMessageEventArgs e)
        {
            AllForms.m_frmLog.AppendToLog("frmPopup_cEXWB1_WBDocHostShowUIShowMessage\r\n" + e.text);
            e.handled = true;

            // simple alert dialog
            if (e.type == 48)
            {
                MessageBox.Show(e.text, e.caption);
                MainScript.AddAlertHandler(PopupName);
            }
            // confirm dialog
            else if (e.type == 33)
            {
                DialogResult result = MessageBox.Show(e.text, e.caption, MessageBoxButtons.OKCancel);
                MainScript.AddConfirmHandler(PopupName, result);
            }
        }

        private void cEXWB1_WindowClosing(object sender, csExWB.WindowClosingEventArgs e)
        {
            this.cEXWB1.NavToBlank();
            this.Hide();
        }

        private void cEXWB1_WindowSetHeight(object sender, csExWB.WindowSetHeightEventArgs e)
        {
            if ((!this.Visible) || (this.WindowState == FormWindowState.Maximized) ||
                (this.WindowState == FormWindowState.Minimized) || (e.height < 0))
                return;
            this.Height = e.height;
        }

        private void cEXWB1_WindowSetWidth(object sender, csExWB.WindowSetWidthEventArgs e)
        {
            if ((!this.Visible) || (this.WindowState == FormWindowState.Maximized) ||
                (this.WindowState == FormWindowState.Minimized) || (e.width < 0))
                return;
            this.Width = e.width;
        }

        private void cEXWB1_ScriptError(object sender, csExWB.ScriptErrorEventArgs e)
        {
            AllForms.m_frmLog.AppendToLog("cEXWB1_ScriptError");
            e.continueScripts = true;
        }

        private void cEXWB1_NavigateError(object sender, csExWB.NavigateErrorEventArgs e)
        {
            AllForms.m_frmLog.AppendToLog("frmPopup_cEXWB1_NavigateError");
            e.Cancel = true;
        }

        private void cEXWB1_NewWindow2(object sender, csExWB.NewWindow2EventArgs e)
        {
            csExWB.cEXWB pWB = (csExWB.cEXWB)sender;
            if ((pWB != null) && (!pWB.RegisterAsBrowser))
                pWB.RegisterAsBrowser = true;
            newfrm.AssignBrowserObject(ref e.browser);
        }

        private void cEXWB1_NewWindow3(object sender, csExWB.NewWindow3EventArgs e)
        {
            csExWB.cEXWB pWB = (csExWB.cEXWB)sender;
            if ((pWB != null) && (!pWB.RegisterAsBrowser))
                pWB.RegisterAsBrowser = true;
            newfrm.AssignBrowserObject(ref e.browser);
        }

        private void cEXWB1_WBEvaluteNewWindow(object sender, csExWB.EvaluateNewWindowEventArgs e)
        {
            newfrm = new frmPopup();
            newfrm.isSecondaryPopup = true;
            newfrm.Show();
            newfrm.PopupName = MainScript.AddPopup(this.PopupName, e.url);
            newfrm.SetURL(m_MainForm, MainScript, e.url);
            newfrm.BringToFront();
        }

        private void cEXWB1_BeforeNavigate2(object sender, csExWB.BeforeNavigate2EventArgs e)
        {
            AllForms.m_frmLog.AppendToLog("frmPopup_BeforeNavigate2\r\n" + e.url);
        }

        private void cEXWB1_WindowSetLeft(object sender, csExWB.WindowSetLeftEventArgs e)
        {
            if ((!this.Visible) || (this.WindowState == FormWindowState.Maximized) ||
                (this.WindowState == FormWindowState.Minimized) || (e.left < 0))
                return;
            this.Left = e.left;
        }

        private void cEXWB1_WindowSetTop(object sender, csExWB.WindowSetTopEventArgs e)
        {
            if ((!this.Visible) || (this.WindowState == FormWindowState.Maximized) ||
                (this.WindowState == FormWindowState.Minimized) || (e.top < 0))
                return;
            this.Top = e.top;
        }

        private void cEXWB1_WBSecurityProblem(object sender, csExWB.SecurityProblemEventArgs e)
        {
            AllForms.m_frmLog.AppendToLog("cEXWB1_WBSecurityProblem");
        }

        private void timerActiveElement_Tick(object sender, EventArgs e)
        {
            try
            {
                //if (LastKeyTime < DateTime.Now.AddMilliseconds(-1 * MainScript.settings.TypingTime) && sbKeys.Length > 0)
                if (LastKeyTime < DateTime.Now.AddMilliseconds(-1 * MainScript.settings.TypingTime) && MainScript.sbKeys.Length > 0)
                {
                    MainScript.AddTyping(PopupName, cEXWB1.GetActiveElement());
                }
            }
            catch (Exception ex)
            {
                AllForms.m_frmLog.AppendToLog("Timer Active Failure\r\n" + ex.ToString());
            }
        }
        
        private void cEXWB1_WBKeyUp(object sender, csExWB.WBKeyUpEventArgs e)
        {
            if (e.keycode == Keys.ShiftKey)
            {
                ShiftKeyDown = false;
            }
            else if (e.keycode == Keys.ControlKey)
            {
                ControlKeyDown = false;
            }
        }

        private void cEXWB1_WBKeyDown(object sender, csExWB.WBKeyDownEventArgs e)
        {
            try
            {
                KeyEntryElement = cEXWB1.GetActiveElement();
            }
            catch (Exception ex)
            {
                AllForms.m_frmLog.AppendToLog("WBKeyDown Failure\r\n" + ex.ToString());
                return;
            }


            if (e.keycode == Keys.ControlKey)
            {
                ControlKeyDown = true;
            }
            else if (e.keycode == Keys.ShiftKey)
            {
                ShiftKeyDown = true;
            }
            else
            {
                MainScript.AddKeys(ShiftKeyDown, e.keycode);
                LastKeyTime = DateTime.Now;
            }
        }

        private void frmPopup_VisibleChanged(object sender, EventArgs e)
        {
            timerActiveElement.Enabled = true;
        }

        private void cEXWB1_WBAuthenticate(object sender, csExWB.AuthenticateEventArgs e)
        {
            if (m_MainForm.m_frmAuth.ShowDialogInternal(this) == DialogResult.OK)
            {
                //Default value of handled is false
                e.handled = true;
                //To pass a doamin as in a NTLM authentication scheme,
                //use this format: Username = Domain\username
                e.username = m_MainForm.m_frmAuth.m_Username;
                e.password = m_MainForm.m_frmAuth.m_Password;

                MainScript.AddLoginDialog(PopupName, e.username, e.password);
            }
        }

        private void cEXWB1_WBContextMenu(object sender, csExWB.ContextMenuEventArgs e)
        {
            try
            {
                IHTMLElement pelem = cEXWB1.GetActiveElement();
                frmPropertyExplorer frm = new frmPropertyExplorer();
                frm.SetWindowProperties(MainScript, PopupName, pelem);
                frm.Location = e.pt;
                frm.ShowInTaskbar = false;
                frm.ShowDialog();
                e.displaydefault = false;
            }
            catch (Exception ex)
            {
                AllForms.m_frmLog.AppendToLog("WBContext Failure\r\n" + ex.ToString());
            }

        }
    }
}