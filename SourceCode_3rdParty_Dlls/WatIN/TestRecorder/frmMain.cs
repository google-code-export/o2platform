using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Web;
using IfacesEnumsStructsClasses;
using System.IO;
using System.Reflection;
using System.Resources;
using System.Globalization;
using WatiN.Core;

namespace DemoApp
{

    /// <summary>
    /// A simple multi tab simulated webbrowser control which
    /// demonstrates the usage of csEXWB control with some extras.
    /// 
    /// The demo pretty much covers all the basics and most of the advanced
    /// functionality that the control offers. It also includes a complete DOM
    /// viewer, cache and cookie explorers, thumb navigation,
    /// document information viewwer, loading and displaying favorites in dynamic
    /// menu, Popup, authentication and find handlers,
    /// a functional HTML editor, ....
    /// 
    /// A bit of details:
    /// In the frmMain, each browser has a corresponding toolstripbutton
    /// acting as a tab and a menu item for tab switching which displays
    /// number of open tabs as well. In the frmThumbs, each browser has a
    /// corresponding label, and picturebox.
    /// 
    /// The name of all the webbrowser corresponding controls are identical!
    /// All use the webbrowser instance Name.
    /// Any new browser can be deleted but the first one which was placed
    /// on the form in design time. Kept for making things a bit easier!
    /// 
    /// The rest is a matter of synchronizing addition, removal, and switching
    /// among webbrowser instances. Simple enough.
    /// 
    /// All the images, except for progres_spinner.gif, used in this project 
    /// are coming from IeToolbar.bmp image strip. They are in 
    /// SetupImages() method into a static imagelist
    /// which then can be shared by all project forms and controls.
    /// </summary>
    public partial class frmMain : System.Windows.Forms.Form
    {
        #region Local Variables

        private const string m_AboutBlank = "about:blank";
        private const string m_Blank = "Blank";
        private csExWB.cEXWB m_CurWB = null; //Current WB
        private int m_iCurTab = 0; //Current Tab index
        private int m_iCurMenu = 0; //Current WB count menu
        private int m_iCountWB = 1; //WB Count
        private const int m_MaxTextLen = 15; //Maimum len of text displayed in tabs,...
        private ToolStripButton m_tsBtnFirstTab = null; //for reference
        //For reference when rClicked on a toolstripbutton
        private ToolStripButton m_tsBtnctxMnu = null; 

        //To capture file download name in FileDownload event
        private string m_Status = string.Empty;

        //Images for statusbar, ....
        private System.Drawing.Image m_imgLock = null;
        private System.Drawing.Image m_imgUnLock = null;
        private System.Drawing.Image m_imgAniProgress = null;

        //Forms
        private frmPopup m_frmPopup = new frmPopup();
        private frmFind m_frmFind = new frmFind();
        private frmCacheCookie m_frmCacheCookie = new frmCacheCookie();
        private frmDocInfo m_frmDocInfo = new frmDocInfo();
        public frmAuthenticate m_frmAuth = new frmAuthenticate();

        public WatinScript wscript = new WatinScript();
        private Microsoft.Win32.MouseHook hook = new Microsoft.Win32.MouseHook();
        private IHTMLElement KeyEntryElement = null;
        private bool ControlKeyDown = false;
        private bool ShiftKeyDown = false;
        private DateTime LastKeyTime = DateTime.MinValue;
        public WatiN.Core.IE watinie = null;
        private IHTMLElement lastelement = null;
        private string OriginalColor = "white";
        private IHTMLElement textActiveElement = null;

        public ResourceManager resmgr = new ResourceManager("frmMain",Assembly.GetExecutingAssembly());

        //Use for Element tree
        TreeNode m_RootNode;
        TreeNode m_ButtonRootNode;
        TreeNode m_CheckBoxRootNode;
        TreeNode m_FrameRootNode;
        TreeNode m_HtmlDialogRootNode;
        TreeNode m_ImageRootNode;
        TreeNode m_LabelRootNode;
        TreeNode m_LinkRootNode;
        TreeNode m_RadioButtonRootNode;
        TreeNode m_SelectListRootNode;
        TreeNode m_TableCellRootNode;
        TreeNode m_TableRowRootNode;
        TreeNode m_TableRootNode;
        TreeNode m_TextFieldRootNode;
        System.Collections.ArrayList arrSelectItems = new System.Collections.ArrayList();
        

        #endregion

        #region Form Events

        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            CultureInfo ci = new CultureInfo("en-US");
            System.Threading.Thread.CurrentThread.CurrentUICulture = ci;

            Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(Application_ThreadException);

            try
            {                
                wscript.settings.LoadSettings(txtCode);
                switch (wscript.settings.CodeLanguage)
                {
                    case AppSettings.CodeLanguages.CSharp: wscript = new WatiNCSharp(); break;
                    case AppSettings.CodeLanguages.VBNet: wscript = new WatiNVBNet(); break;
                    case AppSettings.CodeLanguages.PHP: wscript = new WatiNPHP(); break;
                    case AppSettings.CodeLanguages.Python: wscript = new WatiNPython(); break;
                    case AppSettings.CodeLanguages.Perl: wscript = new WatiNPerl(); break;
                }
                wscript.settings.LoadSettings(txtCode);

                tabControl.TabPages[0].Text = string.Format(Properties.Resources.Test_Source_Tab,wscript.LanguageName);

                wscript.fcnManager = new FunctionManager(Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "Functions"));
                wscript.rtbTarget = txtCode;
                wscript.MainBrowser = cEXWB1;

                if (wscript.settings.ScriptWindowFont==null)
                {
                    wscript.settings.ScriptWindowFont = txtCode.Font;
                }

                // load up the templates
                tsbRunSelected.DropDownItems.Clear();
                tsbRun.DropDownItems.Clear();
                tsbResources.DropDownItems.Clear();
                List<Template> tlist = wscript.TemplateFiles.GetListForRun(wscript.settings.CodeLanguage);
                foreach (Template tfile in tlist)
                {
                    tsbRunSelected.DropDownItems.Add(tfile.Name);
                    tsbRun.DropDownItems.Add(tfile.Name);
                }
                ShowAsDefault(tsbRun.DropDownItems, wscript.settings.DefaultRunTemplate);
                ShowAsDefault(tsbRunSelected.DropDownItems, wscript.settings.DefaultRunTemplate);
                if (tlist.Count == 1)
                {
                    wscript.settings.DefaultRunTemplate = tlist[0].Name;
                }
                else if (tlist.Count==0)
                {
                    tsbRun.Enabled = false;
                }

                tsbCompile.DropDownItems.Clear();
                tlist = wscript.TemplateFiles.GetListForCompile(wscript.settings.CodeLanguage);
                foreach (Template tfile in tlist)
                {
                    tsbCompile.DropDownItems.Add(tfile.Name);
                }
                ShowAsDefault(tsbCompile.DropDownItems, wscript.settings.DefaultCompileTemplate);
                if (tlist.Count==1)
                {
                    wscript.settings.DefaultCompileTemplate = tlist[0].Name;
                }
                else if (tlist.Count == 0)
                {
                    tsbCompile.Enabled = false;
                }

                tlist = wscript.TemplateFiles.GetListForLanguage(wscript.settings.CodeLanguage);
                foreach (Template tfile in tlist)
                {
                    tsbResources.DropDownItems.Add(tfile.Name);
                }

                hook.Install();
                
                hook.MouseUp += new Microsoft.Win32.MouseHookEventHandler(hook_MouseUp);

                SetupImages();

                cEXWB1.RegisterAsBrowser = true;
                
                
                //Add first tab
                string sname = cEXWB1.Name;
                m_tsBtnFirstTab = new ToolStripButton(sname, m_imgUnLock);
                m_tsBtnFirstTab.Name = sname;
                m_tsBtnFirstTab.Text = m_Blank;
                m_tsBtnFirstTab.ToolTipText = m_AboutBlank;
                m_tsBtnFirstTab.Checked = true;
                m_tsBtnFirstTab.MouseUp += new MouseEventHandler(this.tsWBTabs_ToolStripButtonCtxMenuHandler);
                tsWBTabs.Items.Add((ToolStripItem)m_tsBtnFirstTab);
                //Take note of current WB and first toolstripbutton index
                m_CurWB = cEXWB1;
                m_iCurTab = tsWBTabs.Items.Count - 1;
                //Add menu
                ToolStripMenuItem menu = new ToolStripMenuItem(m_Blank, m_imgUnLock);
                menu.Name = sname;
                menu.Checked = true;

                m_frmFind.Icon = AllForms.BitmapToIcon(30);
                //form find callback
                m_frmFind.FindInPageEvent += new FindInPage(m_frmFind_FindInPageEvent);
                

                //Start watching favorites folder
                fswFavorites.Path = Environment.GetFolderPath(Environment.SpecialFolder.Favorites);

                
                //Load favorites
                LoadFavoriteMenuItems();
            }
            catch (Exception ee)
            {
                MessageBox.Show(string.Format("frmMain_Load Failed\r\n{0}",ee.ToString()));
            }
        }


        void ShowAsDefault(ToolStripItemCollection items, string DefaultItem)
        {
            if (DefaultItem=="")
            {
                return;
            }
            foreach (ToolStripItem item in items)
            {
                if (item.Text==DefaultItem)
                {
                    Font newfont = new Font(item.Font, FontStyle.Bold);
                    item.Font = newfont;
                }
            }
        }

        void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            frmException frm = new frmException();
            frm.lblError.Text = e.Exception.Message;
            frm.rtbStack.Text = e.Exception.StackTrace;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                EmailException email = new EmailException();
                string strAddress = frm.txtEmail.Text.Trim();
                if (!System.Text.RegularExpressions.Regex.IsMatch(strAddress.ToUpper(), @"^[A-Z0-9._%-]+@[A-Z0-9.-]+\.[A-Z]{2,4}$"))
                {
                    strAddress = "";
                }
                email.SendMail(e.Exception, strAddress, frm.txtComments.Text, frm.chkCopy.Checked, false);
            }
        }

        void ShowPropertyWindow(IHTMLElement pelem, Point pt)
        {
            if (pelem==null)
            {
                return;
            }

            frmFunctionChooser frm = new frmFunctionChooser();
            frm.SetWindowProperties(wscript, wscript.settings.BaseIEName, pelem);

            if (pt.X + frm.Width > Screen.PrimaryScreen.WorkingArea.Width)
            {
                pt.X = Screen.PrimaryScreen.WorkingArea.Width - frm.Width;
            }
            if (pt.Y + frm.Height > Screen.PrimaryScreen.WorkingArea.Height)
            {
                pt.Y = Screen.PrimaryScreen.WorkingArea.Height - frm.Height;
            }

            frm.Location = pt;

            frm.ShowDialog();
            wscript.ClearTimer();
        }

        void hook_MouseUp(object sender, Microsoft.Win32.MouseHookEventArgs e)
        {
            if (e.Control != null && e.Control.Name == "cEXWB1" && e.Button == MouseButtons.Right)
            {
                IHTMLElement pelem = cEXWB1.GetActiveElement();
                if (pelem != null && pelem.tagName.ToLower() == "select")
                {
                    ShowPropertyWindow(pelem, new Point(e.X, e.Y));
                }
                return;
            }
            else if (e.Button == MouseButtons.Right)
            {
                return;
            }
            
            if (e.Control != null && e.Control.Name == "cEXWB1" && e.Control.Parent != null && e.Control.Parent.Name == "frmMain" && cEXWB1.GetActiveElement() != null)
            {
                // Freddy Guime
                // We are doing this differently now.
                // The issue being that clicks capture will not relate to the correct
                // activeelement if a "change" on the activeelement has been made.

                // 1) Figure out if there was an "active" element before.

                
                // 2) Make Click record the "TextActiveElement" 
                WriteKeys();
                textActiveElement = cEXWB1.GetActiveElement();
                wscript.AddClick(wscript.settings.BaseIEName, textActiveElement);
            }
            else if (e.Control != null && e.Control.Name == "cEXWB1" && e.Control.Parent != null && e.Control.Parent.Name == "frmPopup" && m_frmPopup.GetActiveElement() != null)
            {
                wscript.AddClick((e.Control.Parent as frmPopup).PopupName, m_frmPopup.GetActiveElement());
            }
        }
        
        /// <summary>
        /// This function is called by the timer to figure out what has been "added"
        /// in text. Try to change this to realtime on certain keys.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timerActiveElement_Tick(object sender, EventArgs e)
        {
            if (LastKeyTime < DateTime.Now.AddMilliseconds(-1 * wscript.settings.TypingTime) && wscript.sbKeys.Length > 0)
            {
                wscript.AddTyping(wscript.settings.BaseIEName, cEXWB1.GetActiveElement());
            }
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (wscript.UnsavedScript && wscript.settings.WarnWhenUnsaved)
                {
                    if (MessageBox.Show(Properties.Resources.UnsavedScript, Properties.Resources.UnsavedScript_title, MessageBoxButtons.YesNo) == DialogResult.No)
                    {
                        e.Cancel = true;
                    }
                }
            }
            catch
            {
                // just close
            }
        }

        private void frmMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            hook.Uninstall();

            m_CurWB = null;
            if (m_imgLock != null)
            {
                m_imgLock.Dispose();
                m_imgLock = null;
            }
            if (m_imgUnLock != null)
            {
                m_imgUnLock.Dispose();
                m_imgUnLock = null;
            }
            if (m_imgAniProgress != null)
            {
                m_imgAniProgress.Dispose();
                m_imgAniProgress = null;
            }
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            try
            {
                if (this.WindowState == FormWindowState.Minimized)
                    return;
            }
            finally
            {
            }
        } 
        #endregion

        #region Local methods

        public csExWB.cEXWB CurrentBrowserControl
        {
            get
            {
                return m_CurWB;
            }
        }

        private bool CheckWBPointer()
        {
            return (m_CurWB == null) ? false : true;
        }

        private csExWB.cEXWB FindBrowser(string name)
        {
            try
            {
                foreach (Control ctl in Controls)
                {
                    if (ctl.Name == name)
                    {
                        if (ctl is csExWB.cEXWB)
                            return (csExWB.cEXWB)ctl;
                    }
                }
            }
            catch (Exception)
            {
            }
            return null;
        }

        private ToolStripButton FindTab(string name)
        {
            try
            {
                foreach (ToolStripItem item in tsWBTabs.Items)
                {
                    if (item.Name == name)
                    {
                        if (item is ToolStripButton)
                            return (ToolStripButton)item;
                    }
                }
            }
            catch (Exception)
            {
            }
            return null;
        }

        private bool AddNewBrowser(string TabText, string TabTooltip, string Url, bool BringToFront)
        {
            //Copy flags
            int iDochostUIFlag = (int)(DOCHOSTUIFLAG.NO3DBORDER |
                        DOCHOSTUIFLAG.FLAT_SCROLLBAR | DOCHOSTUIFLAG.THEME);
            int iDocDlCltFlag = (int)(DOCDOWNLOADCTLFLAG.DLIMAGES |
                        DOCDOWNLOADCTLFLAG.BGSOUNDS | DOCDOWNLOADCTLFLAG.VIDEOS);

            if (m_CurWB != null)
            {
                iDochostUIFlag = m_CurWB.WBDOCHOSTUIFLAG;
                iDocDlCltFlag = m_CurWB.WBDOCDOWNLOADCTLFLAG;
            }

            csExWB.cEXWB pWB = null;
            
            int i = m_iCountWB + 1;
            string sname = "cEXWB" + i.ToString();

            try
            {
                ToolStripButton btn = new ToolStripButton(sname, m_imgUnLock);
                btn.Name = sname;
                if (TabText.Length > 0)
                    btn.Text = TabText;
                if (TabTooltip.Length > 0)
                    btn.ToolTipText = TabTooltip;
                btn.AutoToolTip = true;
                btn.MouseUp += new MouseEventHandler(this.tsWBTabs_ToolStripButtonCtxMenuHandler);
                tsWBTabs.Items.Add(btn);

                //Create and setup browser
                pWB = new csExWB.cEXWB();
                pWB.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
                pWB.Name = sname;
                pWB.Location = cEXWB1.Location;
                pWB.Size = cEXWB1.Size;
                pWB.RegisterAsBrowser = true;
                pWB.WBDOCDOWNLOADCTLFLAG = iDocDlCltFlag;
                pWB.WBDOCHOSTUIFLAG = iDochostUIFlag;

                //Add events, using the same eventhandlers for all browsers
                pWB.TitleChange += new csExWB.TitleChangeEventHandler(this.cEXWB1_TitleChange);
                pWB.StatusTextChange += new csExWB.StatusTextChangeEventHandler(this.cEXWB1_StatusTextChange);
                pWB.CommandStateChange += new csExWB.CommandStateChangeEventHandler(this.cEXWB1_CommandStateChange);
                pWB.WBKeyDown += new csExWB.WBKeyDownEventHandler(this.cEXWB1_WBKeyDown);
                pWB.WBEvaluteNewWindow += new csExWB.EvaluateNewWindowEventHandler(this.cEXWB1_WBEvaluteNewWindow);
                pWB.BeforeNavigate2 += new csExWB.BeforeNavigate2EventHandler(this.cEXWB1_BeforeNavigate2);
                pWB.ProgressChange += new csExWB.ProgressChangeEventHandler(this.cEXWB1_ProgressChange);
                pWB.NavigateComplete2 += new csExWB.NavigateComplete2EventHandler(this.cEXWB1_NavigateComplete2);
                pWB.HTMLEvent += new csExWB.HTMLEventHandler(this.cEXWB1_HTMLEvent);
                pWB.DownloadBegin += new csExWB.DownloadBeginEventHandler(this.cEXWB1_DownloadBegin);
                pWB.ScriptError += new csExWB.ScriptErrorEventHandler(this.cEXWB1_ScriptError);
                pWB.DownloadComplete += new csExWB.DownloadCompleteEventHandler(this.cEXWB1_DownloadComplete);
                pWB.StatusTextChange += new csExWB.StatusTextChangeEventHandler(this.cEXWB1_StatusTextChange);
                pWB.DocumentCompleteEX += new csExWB.DocumentCompleteExEventHandler(this.cEXWB1_DocumentCompleteEX);
                pWB.WBDragDrop += new csExWB.WBDropEventHandler(cEXWB1_WBDragDrop);
                pWB.SetSecureLockIcon += new csExWB.SetSecureLockIconEventHandler(this.cEXWB1_SetSecureLockIcon);
                pWB.NavigateError += new csExWB.NavigateErrorEventHandler(this.cEXWB1_NavigateError);
                pWB.WBSecurityProblem += new csExWB.SecurityProblemEventHandler(this.cEXWB1_WBSecurityProblem);
                pWB.NewWindow2 += new csExWB.NewWindow2EventHandler(this.cEXWB1_NewWindow2);
                pWB.DocumentComplete += new csExWB.DocumentCompleteEventHandler(this.cEXWB1_DocumentComplete);
                pWB.NewWindow3 += new csExWB.NewWindow3EventHandler(this.cEXWB1_NewWindow3);
                pWB.WBKeyUp += new csExWB.WBKeyUpEventHandler(this.cEXWB1_WBKeyUp);
                pWB.WindowClosing += new csExWB.WindowClosingEventHandler(this.cEXWB1_WindowClosing);
                pWB.WBContextMenu += new csExWB.ContextMenuEventHandler(this.cEXWB1_WBContextMenu);
                pWB.WBDocHostShowUIShowMessage += new csExWB.DocHostShowUIShowMessageEventHandler(this.cEXWB1_WBDocHostShowUIShowMessage);
                pWB.FileDownload += new csExWB.FileDownloadEventHandler(this.cEXWB1_FileDownload);
                pWB.WBAuthenticate += new csExWB.AuthenticateEventHandler(this.cEXWB1_WBAuthenticate);
                pWB.GotFocus += new EventHandler(pWB_GotFocus);

                //Add to controls collection
                this.Controls.Add(pWB);

                ToolStripMenuItem menu = new ToolStripMenuItem(btn.Text, btn.Image);
                menu.Name = sname;

                if (BringToFront)
                {
                    //Uncheck last tab
                    ((ToolStripButton)tsWBTabs.Items[m_iCurTab]).Checked = false;
                    btn.Checked = true;

                    //Adjust current browser pointer
                    m_CurWB = pWB;
                    //Adjust current tab index
                    m_iCurTab = tsWBTabs.Items.Count - 1;
                    //Reset and hide progressbar
                    tsProgress.Value = 0;
                    tsProgress.Maximum = 0;
                    tsProgress.Visible = false;
                    //Bring to front
                    pWB.BringToFront();
                }
                //Increase count
                m_iCountWB++;
                tsBtnOpenWBs.Text = m_iCountWB.ToString() + " open tab(s)";


                if (Url.Length > 0)
                    pWB.Navigate(Url);
            }
            catch (Exception ee)
            {
                AllForms.m_frmLog.AppendToLog("AddNewBrowser\r\n" + ee.ToString());
                return false;
            }

            return true;
        }

        void pWB_GotFocus(object sender, EventArgs e)
        {
            string ssss = "aa";

            //throw new Exception("The method or operation is not implemented.");
        }

        /// <summary>
        /// Removes an inactive browser without switching to another
        /// </summary>
        /// <param name="name"></param>
        /// <param name="RemoveMenu">true, removes corresponding menu item</param>
        /// <returns></returns>
        private bool RemoveBrowser2(string name, bool RemoveMenu)
        {
            try
            {
                //Do not remove the first browser          
                if ((m_iCountWB == 1) || (name == m_tsBtnFirstTab.Name))
                    return false;

                csExWB.cEXWB pWB = FindBrowser(name);
                Controls.Remove(pWB);
                pWB.Dispose();
                pWB = null;

                ToolStripButton btn = FindTab(name);
                tsWBTabs.Items.Remove((ToolStripItem)btn);
                btn.Dispose();
                btn = null;

                m_iCountWB--;
                tsBtnOpenWBs.Text = m_iCountWB.ToString() + " open tab(s)";
            }
            catch (Exception ee)
            {
                AllForms.m_frmLog.AppendToLog("RemoveBrowser2\r\n" + ee.ToString());
            }
            return true;
        }

        /// <summary>
        /// Removes the current browser and switches to the one before it
        /// if one is available, else the first one is selected
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        private bool RemoveBrowser(string name)
        {
            bool bRet = false;

            //Do not remove the first browser          
            if ((m_iCountWB == 1) || (name == m_tsBtnFirstTab.Name))
                return bRet;

            tsProgress.Value = 0;
            tsProgress.Maximum = 0;
            tsProgress.Visible = false;

            ToolStripButton btn = FindTab(name);
            ToolStripButton nexttab = null;
            try
            {
                //find the first available btn before this one and switch
                foreach (ToolStripItem item in tsWBTabs.Items)
                {
                    if (item.Name == btn.Name)
                    {
                        break;
                    }
                    if (item is ToolStripButton)
                        nexttab = (ToolStripButton)item;
                }
            }
            catch (Exception ERemoveBrowser)
            {
                AllForms.m_frmLog.AppendToLog("RemoveBrowser\r\n" + ERemoveBrowser.ToString());
            }

            try
            {
                tsWBTabs.Items.Remove((ToolStripItem)btn);
                btn.Dispose();
                btn = null;
            }
            catch (Exception ERemoveBrowser1)
            {
                AllForms.m_frmLog.AppendToLog("RemoveBrowser1\r\n" + ERemoveBrowser1.ToString());
            }

            try
            {
                csExWB.cEXWB pWB = FindBrowser(name);
                Controls.Remove(pWB);
                pWB.Dispose();
                pWB = null;
            }
            catch (Exception ERemoveBrowser2)
            {
                AllForms.m_frmLog.AppendToLog("RemoveBrowser2\r\n" + ERemoveBrowser2.ToString());
            }

            ToolStripMenuItem nextmenu = null;

            try
            {
                if (nexttab == null)
                {
                    m_CurWB = cEXWB1;
                    m_iCurTab = tsWBTabs.Items.IndexOf((ToolStripItem)m_tsBtnFirstTab);
                    m_iCurMenu = 0;
                    nexttab = m_tsBtnFirstTab;
                }
                else
                {
                    m_CurWB = FindBrowser(nexttab.Name);
                    m_iCurTab = tsWBTabs.Items.IndexOf((ToolStripItem)nexttab);
                }

                this.Text = m_CurWB.GetTitle(true);
                if (this.Text.Length == 0)
                    this.Text = m_Blank;
                this.comboURL.Text = nexttab.ToolTipText;
                nexttab.Checked = true;
                nextmenu.Checked = true;
                m_CurWB.BringToFront();

                m_CurWB.SetFocus();

            }
            catch (Exception ERemoveBrowser4)
            {
                AllForms.m_frmLog.AppendToLog("RemoveBrowser4\r\n" + ERemoveBrowser4.ToString());
            }

            m_iCountWB--;
            tsBtnOpenWBs.Text = m_iCountWB.ToString() + " open tab(s)";

            return bRet;
        }

        private void SwitchTabs(string name, ToolStripButton btn, bool UpdateThumb)
        {
            try
            {
                csExWB.cEXWB pWB = FindBrowser(name);
                if (pWB == null)
                    return;

                //Uncheck last one
                if (m_iCountWB > 1)
                    ((ToolStripButton)tsWBTabs.Items[m_iCurTab]).Checked = false;
                m_iCurTab = tsWBTabs.Items.IndexOf(btn);

                m_CurWB = pWB;
                tsBtnBack.Enabled = m_CurWB.CanGoBack;
                tsBtnForward.Enabled = m_CurWB.CanGoForward;
                m_CurWB.BringToFront();
                m_CurWB.SetFocus();
                this.Text = m_CurWB.GetTitle(true);
                if (this.Text.Length == 0)
                    this.Text = m_Blank;
                if (btn != null)
                {
                    btn.Checked = true;
                    btn.Text = this.Text;
                    if (btn.Text.Length > m_MaxTextLen)
                        btn.Text = btn.Text.Substring(0, m_MaxTextLen) + "...";
                    btn.ToolTipText = HttpUtility.UrlDecode(m_CurWB.LocationUrl);
                    this.comboURL.Text = btn.ToolTipText;
                }

                //Reset and hide progressbar
                //If page is in the process of loading then the progressbar
                //will be adjusted
                tsProgress.Value = 0;
                tsProgress.Maximum = 0;
                tsProgress.Visible = false;

                //update SecureLockIcon state
                UpdateSecureLockIcon(m_CurWB.SecureLockIcon);
            }
            catch (Exception ee)
            {
                AllForms.m_frmLog.AppendToLog("SwitchingTabs\r\n" + ee.ToString());
            }

        }

        private void UpdateSecureLockIcon(SecureLockIconConstants slic)
        {
            if (slic == SecureLockIconConstants.secureLockIconUnsecure)
            {
                tsSecurity.Image = m_imgUnLock;
                this.tsSecurity.Text = "Not Secure";
            }
            else if (slic == SecureLockIconConstants.secureLockIcon128Bit)
            {
                tsSecurity.Image = m_imgLock;
                this.tsSecurity.Text = "128 Bit";
            }
            else if (slic == SecureLockIconConstants.secureLockIcon40Bit)
            {
                tsSecurity.Image = m_imgLock;
                this.tsSecurity.Text = "40 Bit";
            }
            else if (slic == SecureLockIconConstants.secureLockIcon56Bit)
            {
                tsSecurity.Image = m_imgLock;
                this.tsSecurity.Text = "56 Bit";
            }
            else if (slic == SecureLockIconConstants.secureLockIconFortezza)
            {
                tsSecurity.Image = m_imgLock;
                this.tsSecurity.Text = "Fortezza";
            }
            else if (slic == SecureLockIconConstants.secureLockIconMixed)
            {
                tsSecurity.Image = m_imgUnLock;
                this.tsSecurity.Text = "Mixed";
            }
            else if (slic == SecureLockIconConstants.secureLockIconUnknownBits)
            {
                tsSecurity.Image = m_imgUnLock;
                this.tsSecurity.Text = "UnknownBits";
            }
        }

        /// <summary>
        /// Loads all images from a image strip into a
        /// static imagelist which in turn can 
        /// be used bey any form or control 
        /// capable of using images
        /// </summary>
        private void SetupImages()
        {
            try
            {
                //string[] str = this.GetType().Assembly.GetManifestResourceNames();
                //foreach (string s in str)
                //{
                //    System.Diagnostics.Debug.Print(s);
                //}
                //DemoApp.Properties.Resources.resources
                //DemoApp.frmPopup.resources
                //DemoApp.frmMain.resources
                //DemoApp.Resources.IeToolbar.bmp
                //....
                System.IO.Stream file1 =
                    this.GetType().Assembly.GetManifestResourceStream("DemoApp.Resources.progress_spinner.gif");
                m_imgAniProgress = System.Drawing.Image.FromStream(file1);

                System.IO.Stream file =
                    this.GetType().Assembly.GetManifestResourceStream("DemoApp.Resources.IeToolbar.bmp");
                var img = System.Drawing.Image.FromStream(file);

                AllForms.m_imgListMain.TransparentColor = Color.FromArgb(192, 192, 192);
                AllForms.m_imgListMain.Images.AddStrip(img);

                tsBtnBack.Image = AllForms.m_imgListMain.Images[0];
                tsBtnForward.Image = AllForms.m_imgListMain.Images[1];
                tsBtnStop.Image = AllForms.m_imgListMain.Images[2];
                tsBtnRefresh.Image = AllForms.m_imgListMain.Images[4];
                tsSplitBtnSearch.Image = AllForms.m_imgListMain.Images[30];
                tsBtnGo.Image = AllForms.m_imgListMain.Images[10];
                tsChkBtnGo.Image = AllForms.m_imgListMain.Images[18];

                tsBtnOpenWBs.Image = AllForms.m_imgListMain.Images[12];
                tsBtnAddWB.Image = AllForms.m_imgListMain.Images[16];
                tsChkBtnAddWB.Image = AllForms.m_imgListMain.Images[18];
                tsBtnRemoveWB.Image = AllForms.m_imgListMain.Images[39];
                tsBtnRemoveAllWBs.Image = AllForms.m_imgListMain.Images[40];

                m_imgLock = AllForms.m_imgListMain.Images[13];
                m_imgUnLock = AllForms.m_imgListMain.Images[32]; //normall ie

                tsLinksLblText.Image = AllForms.m_imgListMain.Images[20];

                tsFileMnuNew.Image = AllForms.m_imgListMain.Images[19];
                tsFileMnuOpen.Image = AllForms.m_imgListMain.Images[43];
                tsFileMnuSave.Image = AllForms.m_imgListMain.Images[21];
                tsFileMnuSaveDocument.Image = AllForms.m_imgListMain.Images[44];
                tsFileMnuSaveDocumentImage.Image = AllForms.m_imgListMain.Images[45];
                tsEditMnuCut.Image = AllForms.m_imgListMain.Images[23];
                tsEditMnuCopy.Image = AllForms.m_imgListMain.Images[24];
                tsEditMnuPaste.Image = AllForms.m_imgListMain.Images[25];
                tsEditMnuSelectAll.Image = AllForms.m_imgListMain.Images[28];
                tsEditMnuFindInPage.Image = AllForms.m_imgListMain.Images[30];
                tsFileMnuPrintPreview.Image = AllForms.m_imgListMain.Images[7];
                tsFileMnuPrint.Image = AllForms.m_imgListMain.Images[8];
                tsFileMnuExit.Image = AllForms.m_imgListMain.Images[37];

                tsHelpMnuHelpAbout.Image = AllForms.m_imgListMain.Images[33];
                tsHelpMnuHelpContents.Image = AllForms.m_imgListMain.Images[9];

                this.Icon = AllForms.BitmapToIcon(41);
            }
            catch (Exception ee)
            {
                AllForms.m_frmLog.AppendToLog("\r\nError=" + ee.ToString());
            }
        }

        public void NavToUrl(string sUrl)
        {
            if (!CheckWBPointer())
                return;
            try
            {
                m_CurWB.Navigate(sUrl);
                wscript.AddGoto(wscript.settings.BaseIEName, sUrl);
            }
            catch (Exception ee)
            {
                AllForms.m_frmLog.AppendToLog("NavToUrl\r\n" + ee.ToString());
            }
        }

        #endregion

        #region Event handlers

        private void ToolStripViewMenuClickHandler(object sender, EventArgs e)
        {
            /*
            try
            {
                if ((sender == tsViewMnuLogs) && (!AllForms.m_frmLog.Visible))
                    AllForms.m_frmLog.Show(this);
                else if ((sender == tsViewMnuBrowserThumbs) && (!m_frmThumbs.Visible))
                    m_frmThumbs.Show(this);
            }
            catch (Exception ee)
            {
                AllForms.m_frmLog.AppendToLog("ToolStripViewMenuClickHandler\r\n" + ee.ToString());
            }
             */
        }

        private void tsWBTabs_ToolStripButtonCtxMenuHandler(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                try
                {
                    //Do not show for the first btn,
                    Point ptC = new Point(Cursor.Position.X, Cursor.Position.Y);
                    Point pt = tsWBTabs.PointToClient(ptC);
                    m_tsBtnctxMnu = (ToolStripButton)tsWBTabs.GetItemAt(pt);
                    if ((m_tsBtnctxMnu != null) && (m_tsBtnctxMnu.Name != m_tsBtnFirstTab.Name))
                    {
                        tsMnuCloasAllWBs.Enabled = (m_iCountWB > 1) ? true : false;
                        ctxMnuCloseWB.Show(tsWBTabs.PointToScreen(pt));
                    }
                }
                catch (Exception ee)
                {
                    AllForms.m_frmLog.AppendToLog("TabContextMenuHandler\r\n" + ee.ToString());
                }
            }
        }

        private void comboSearch_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;

                if (comboSearch.Text.Length == 0)
                    return;

                string str = string.Empty;


                str = comboSearch.Text.Replace(" ", "+");
                str = string.Format(Properties.Resources.SearchURL, str);
                NavToUrl(str);
            }
        }

        private void GoSearchToolStripButtonClickHandler(object sender, EventArgs e)
        {
            if (!CheckWBPointer())
                return;

            try
            {
                if (sender == this.tsSplitBtnSearch)
                {
                    if (comboSearch.Text.Length == 0)
                        return;

                    string str = string.Empty;
                    str = comboSearch.Text.Replace(" ", "+");
                    str = string.Format(Properties.Resources.SearchURL, str);
                    NavToUrl(str);
                }
                else if (sender == this.tsBtnGo)
                {
                    if (tsChkBtnGo.Checked) //Open in a new background browser
                    {
                        AddNewBrowser(m_Blank, m_AboutBlank, comboURL.Text, false);
                    }
                    else
                        NavToUrl(comboURL.Text);
                }
                else if (sender == this.tsBtnBack)
                {
                    if (m_CurWB.CanGoBack)
                    {
                        wscript.AddBack();
                        m_CurWB.GoBack();
                    }

                }
                else if (sender == this.tsBtnForward)
                {
                    if (m_CurWB.CanGoForward)
                    {
                        wscript.AddForward();
                        m_CurWB.GoForward();
                    }
                }
                else if (sender == this.tsBtnRefresh)
                {
                    wscript.AddRefresh();
                    m_CurWB.Refresh();
                }

                else if (sender == this.tsBtnStop)
                    m_CurWB.Stop();
            }
            catch (Exception ee)
            {
                AllForms.m_frmLog.AppendToLog("GoSearchToolStripButtonClickHandler\r\n" + ee.ToString());
            }
        }

        private void ToolStripHelpMenuClickHandler(object sender, EventArgs e)
        {
            try
            {
                if (sender == tsHelpMnuHelpAbout)
                {
                    frmAbout about = new frmAbout();
                    about.ShowDialog(this);
                    about.Dispose();
                }
                else if (sender == tsHelpMnuHelpContents)
                {
                    System.Diagnostics.Process.Start(Properties.Resources.RecorderHelpURL);
                }
            }
            catch (Exception ee)
            {
                AllForms.m_frmLog.AppendToLog("ToolStripHelpMenuClickHandler\r\n" + ee.ToString());
            }
        }

        private void ToolStripToolsMenuClickHandler(object sender, EventArgs e)
        {
            if (!CheckWBPointer())
                return;

            /*
            if (sender == tsToolsMnuDocumentDOM)
            {
                try
                {
                    this.Cursor = Cursors.WaitCursor;
                    //load and display DOM, passing Document object
                    
                    m_frmDOM.watinie = watinie;
                    m_frmDOM.LoadDOM(m_CurWB.WebbrowserObject.Document);


                    if (!m_frmDOM.Visible)
                        m_frmDOM.Show(this);
                    else
                        m_frmDOM.BringToFront();
                    this.Cursor = Cursors.Default;
                }
                catch (Exception eee)
                {
                    this.Cursor = Cursors.Default;
                    AllForms.m_frmLog.AppendToLog("tsToolsMnuDocumentDOM\r\n" + eee.ToString());
                }
                return;
            }
            else 
             */ 
            if (sender == tsToolsMnuDocumentInfo)
            {
                try
                {
                    this.Cursor = Cursors.WaitCursor;
                    m_frmDocInfo.LoadDocumentInfo(this.m_CurWB);
                    if (!m_frmDocInfo.Visible)
                        m_frmDocInfo.Show(this);
                    else
                        m_frmDocInfo.BringToFront();
                    this.Cursor = Cursors.Default;
                }
                catch (Exception eeee)
                {
                    this.Cursor = Cursors.Default;
                    AllForms.m_frmLog.AppendToLog("tsToolsMnuDocumentInfo\r\n" + eeee.ToString());
                }
                return;
            }
                /*
            else if (sender == tsToolsMnuActivateDocumentOnClickEvent)
            {
                if (m_CurWB != null)
                {
                    int[] dispids = { (int)HTMLEventDispIds.ID_ONCLICK };
                    m_CurWB.ActivateHTMLEvents(HTMLEventType.HTMLDocumentEvent, dispids);
                    if (!AllForms.m_frmInput.Visible)
                        AllForms.m_frmInput.Show(this);
                }
                return;
            }
            else if (sender == tsToolsMnuSimpleHTMLEditor)
            {
                if (!m_frmHTMLEditor.Visible)
                    m_frmHTMLEditor.Show(this);
                return;
            }
            */

            #region Cache Cookie History
            
            bool bshowform = true;
            int iCount = 0; //Number of cookies or cache entries deleted
            try
            {
                if (sender == tsToolsMnuClearHistory)
                {
                    if (!AllForms.AskForConfirmation(Properties.Resources.RemoveAllHistory, this))
                        return;
                    m_CurWB.ClearHistory();
                }
                else if (sender == tsToolsMnuCookieViewAll)
                {
                    this.Cursor = Cursors.WaitCursor;
                    iCount = m_frmCacheCookie.LoadListViewItems(AllForms.COOKIE);
                    this.Cursor = Cursors.Default;
                }
                else if (sender == tsToolsMnuCookieViewCurrentSite)
                {
                    string url = m_CurWB.LocationUrl;
                    if (url.Length > 0)
                    {
                        this.Cursor = Cursors.WaitCursor;
                        iCount = m_frmCacheCookie.LoadListViewItems(
                            AllForms.SetupCookieCachePattern(m_CurWB.LocationUrl, AllForms.COOKIE));
                        this.Cursor = Cursors.Default;
                    }
                }
                else if (sender == tsToolsMnuCacheViewAll)
                {
                    this.Cursor = Cursors.WaitCursor;
                    iCount = m_frmCacheCookie.LoadListViewItems(AllForms.VISITED);
                    this.Cursor = Cursors.Default;
                }
                else if (sender == tsToolsMnuCacheViewCurrentSite)
                {
                    string url = m_CurWB.LocationUrl;
                    if (url.Length > 0)
                    {
                        //Visited:.*\.example\.com
                        this.Cursor = Cursors.WaitCursor;
                        iCount = m_frmCacheCookie.LoadListViewItems(
                            AllForms.SetupCookieCachePattern(m_CurWB.LocationUrl, AllForms.VISITED));
                        this.Cursor = Cursors.Default;
                    }
                }
                else if (sender == tsToolsMnuCookieEmptyAll)
                {
                    if (!AllForms.AskForConfirmation(Properties.Resources.RemoveAllCookies, this))
                        return;
                    this.Cursor = Cursors.WaitCursor;
                    iCount = m_frmCacheCookie.ClearAllCookies(string.Empty);
                    bshowform = false;
                    this.Cursor = Cursors.Default;
                    MessageBox.Show(this, string.Format(Properties.Resources.DeletedCookies,iCount),
                        Properties.Resources.Information, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (sender == tsToolsMnuCookieEmptyCurrentSite)
                {
                    if (!AllForms.AskForConfirmation(string.Format(Properties.Resources.RemoveCookiesFromPath, m_CurWB.LocationUrl), this))
                        return;
                    this.Cursor = Cursors.WaitCursor;
                    iCount = m_frmCacheCookie.ClearAllCookies(m_CurWB.LocationUrl);
                    bshowform = false;
                    this.Cursor = Cursors.Default;
                    MessageBox.Show(this, string.Format( Properties.Resources.DeletedCookiesFromPath,iCount.ToString(),m_CurWB.LocationUrl),
                        Properties.Resources.Information, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (sender == tsToolsMnuCacheEmptyAll)
                {
                    if (!AllForms.AskForConfirmation(Properties.Resources.RemoveAllCacheEntries, this))
                        return;
                    this.Cursor = Cursors.WaitCursor;
                    iCount = m_frmCacheCookie.ClearAllCache(string.Empty);
                    bshowform = false;
                    this.Cursor = Cursors.Default;
                    MessageBox.Show(this, string.Format(Properties.Resources.DeletedCacheEntries,iCount),
                        Properties.Resources.Information, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (sender == tsToolsMnuCacheEmptyCurrentSite)
                {
                    if (!AllForms.AskForConfirmation(string.Format(Properties.Resources.RemoveCacheEntriesFromPath, m_CurWB.LocationUrl), this))
                        return;
                    this.Cursor = Cursors.WaitCursor;
                    iCount = m_frmCacheCookie.ClearAllCache(m_CurWB.LocationUrl);
                    bshowform = false;
                    this.Cursor = Cursors.Default;
                    MessageBox.Show(this, string.Format(Properties.Resources.DeletedCacheEntriesFromPath,iCount,m_CurWB.LocationUrl),
                        Properties.Resources.Information, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                if (bshowform)
                {
                    if (iCount > 0)
                    {
                        if (!m_frmCacheCookie.Visible)
                            m_frmCacheCookie.Show(this);
                    }
                    else
                        MessageBox.Show(this, Properties.Resources.NoItemsFound, Properties.Resources.Information, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ee)
            {
                this.Cursor = Cursors.Default;
                AllForms.m_frmLog.AppendToLog("ToolStripToolsMenuClickHandler\r\n" + ee.ToString());
            }

            #endregion

        }

        /// <summary>
        /// File menu items click handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolStripFileMenuClickHandler(object sender, EventArgs e)
        {
            try
            {
                if (sender == this.tsFileMnuBackgroundBlankPage)
                {
                    AddNewBrowser(m_Blank, m_AboutBlank, string.Empty, false);
                }
                else if (sender == tsFileMnuBackgroundFromAddress)
                {
                    AddNewBrowser(m_Blank, m_AboutBlank, comboURL.Text, false);
                }
                else if (sender == tsFileMnuForegroundBlankPage)
                {
                    AddNewBrowser(m_Blank, m_AboutBlank, string.Empty, true);
                }
                else if (sender == tsFileMnuForegroundFromAddress)
                {
                    AddNewBrowser(m_Blank, m_AboutBlank, comboURL.Text, true);
                }
                else if (sender == tsFileMnuPrint)
                {
                    m_CurWB.Print();
                }
                else if (sender == tsFileMnuPrintPreview)
                {
                    m_CurWB.PrintPreview();
                }
                else if (sender == tsFileMnuSaveDocument)
                {
                    m_CurWB.SaveAs();
                }
                else if (sender == tsFileMnuSaveDocumentImage)
                {
                    ////gif format produces some of the smallest sizes
                    if (AllForms.ShowStaticSaveDialogForImage(this) == DialogResult.OK)
                    {
                        System.Drawing.Imaging.ImageFormat format = System.Drawing.Imaging.ImageFormat.Bmp;
                        string ext = ".bmp";
                        switch (AllForms.m_dlgSave.FilterIndex)
                        {
                            case 1:
                                break;
                            case 2:
                                format = System.Drawing.Imaging.ImageFormat.Gif;
                                ext = ".gif";
                                break;
                            case 3:
                                format = System.Drawing.Imaging.ImageFormat.Jpeg;
                                ext = ".jpeg";
                                break;
                            case 4:
                                format = System.Drawing.Imaging.ImageFormat.Png;
                                ext = ".png";
                                break;
                            case 5:
                                format = System.Drawing.Imaging.ImageFormat.Wmf;
                                ext = ".wmf";
                                break;
                            case 6:
                                format = System.Drawing.Imaging.ImageFormat.Tiff;
                                ext = ".tiff";
                                break;
                            case 7:
                                format = System.Drawing.Imaging.ImageFormat.Emf;
                                ext = ".emf";
                                break;
                            default:
                                break;
                        }
                        if (string.IsNullOrEmpty(Path.GetExtension(AllForms.m_dlgSave.FileName)))
                            AllForms.m_dlgSave.FileName += ext;

                        m_CurWB.SaveBrowserImage(AllForms.m_dlgSave.FileName,
                            System.Drawing.Imaging.PixelFormat.Format24bppRgb, format);
                    }
                }
                else if (sender == tsFileMnuOpen)
                {
                    if (AllForms.ShowStaticOpenDialog(this, AllForms.DLG_HTMLS_FILTER, 
                        string.Empty, "C:",true) == DialogResult.OK)
                        m_CurWB.Navigate(AllForms.m_dlgOpen.FileName);
                }
                else if (sender == tsFileMnuExit)
                {
                    Application.Exit();
                }
            }
            catch (Exception ee)
            {
                AllForms.m_frmLog.AppendToLog("File Menu\r\n" + ee.ToString());
            }
        }

        /// <summary>
        /// Edit menu items click handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolStripEditMenuClickHandler(object sender, EventArgs e)
        {
            try
            {
                if (!CheckWBPointer())
                    return;
                if (sender == tsEditMnuSelectAll)
                    m_CurWB.SelectAll();
                else if (sender == tsEditMnuCopy)
                    m_CurWB.Copy();
                else if (sender == tsEditMnuCut)
                    m_CurWB.Cut();
                else if (sender == tsEditMnuPaste)
                    m_CurWB.Paste();
                else if (sender == tsEditMnuFindInPage)
                {
                    m_frmFind.Show(this);
                }
            }
            catch (Exception ee)
            {
                AllForms.m_frmLog.AppendToLog("ToolStripEditMenuClickHandler\r\n" + ee.ToString());
            }
        }

        /// <summary>
        /// URL combo click handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboURL_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Return)
                {
                    e.Handled = true;
                    NavToUrl(comboURL.Text);
                }
                else if (e.Control && e.KeyCode == Keys.V)
                {
                    e.Handled = true;
                    comboURL.Text = Clipboard.GetText();
                    NavToUrl(Clipboard.GetText());
                    cEXWB1.Focus();
                }
                else if (e.Control && e.KeyCode == Keys.C)
                {
                    Clipboard.SetText(comboURL.Text);
                }
            }
            catch (Exception eex)
            {
                MessageBox.Show(eex.ToString(), "comboUrl_KeyUp");
            }
        }

        /// <summary>
        /// Handles click event of the drop down menu items of the
        /// toolstrip utton responsible to display number of open browsers
        /// and to offer a quick menu to select a browser
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsBtnOpenWBs_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            try
            {
                SwitchTabs(e.ClickedItem.Name, FindTab(e.ClickedItem.Name), true);
            }
            catch (Exception ee)
            {
                AllForms.m_frmLog.AppendToLog("tsBtnOpenWBs_DropDownItemClicked\r\n" + ee.ToString());
            }
        }

        /// <summary>
        /// Handles toolstripbutton (tabs) click events
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsWBTabs_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            try
            {
                if (e.ClickedItem.Name == tsBtnOpenWBs.Name)
                    return;

                ToolStripButton btn = (ToolStripButton)e.ClickedItem;
                if (e.ClickedItem.Name == tsBtnAddWB.Name)
                {
                    if (tsChkBtnAddWB.Checked)
                    {
                        AddNewBrowser(m_Blank, m_AboutBlank, "", false);
                    }
                    else
                        AddNewBrowser(m_Blank, m_AboutBlank, "", true);
                }
                else if (e.ClickedItem.Name == tsBtnRemoveWB.Name)
                {
                    RemoveBrowser(((ToolStripButton)tsWBTabs.Items[m_iCurTab]).Name);
                }
                else if (e.ClickedItem.Name == tsBtnRemoveAllWBs.Name)
                {
                    tsMnuCloseAllWBs_Click(this, EventArgs.Empty);
                }
                else
                    SwitchTabs(e.ClickedItem.Name, btn, true);
            }
            catch (Exception ee)
            {
                AllForms.m_frmLog.AppendToLog("tsWBTabs_ItemClicked\r\n" + ee.ToString());
            }
        }

        /// <summary>
        /// Handles close menu click event to remove a browser
        /// May not be the current browser in front
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsMnuCloseWB_Click(object sender, EventArgs e)
        {
            try
            {
                if (m_tsBtnctxMnu == null)
                    return;
                //Is this the current one
                if (m_tsBtnctxMnu.Name == m_CurWB.Name)
                {
                    RemoveBrowser(m_tsBtnctxMnu.Name);
                }
                else
                {
                    RemoveBrowser2(m_tsBtnctxMnu.Name, true);
                }
                m_tsBtnctxMnu = null;
            }
            catch (Exception ee)
            {
                AllForms.m_frmLog.AppendToLog("tsMnuCloseWB_Click\r\n" + ee.ToString());
            }
        }

        /// <summary>
        /// Close all browsers except the first one from design time
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsMnuCloseAllWBs_Click(object sender, EventArgs e)
        {
            if (m_iCountWB == 1)
                return;
            try
            {

                m_CurWB = cEXWB1;
                m_CurWB.BringToFront();
                m_CurWB.SetFocus();

                m_tsBtnFirstTab.Checked = true;
                m_iCurTab = tsWBTabs.Items.IndexOf((ToolStripItem)m_tsBtnFirstTab);

                string text = m_CurWB.GetTitle(true);
                if (text.Length == 0)
                    text = m_Blank;
                ToolStripMenuItem menu = new ToolStripMenuItem(text, tsBtnOpenWBs.Image);
                menu.Name = m_tsBtnFirstTab.Name;
                menu.Checked = true;

                tsBtnOpenWBs.Text = m_iCountWB.ToString() + " open tab(s)";
            }
            catch (Exception ee)
            {
                AllForms.m_frmLog.AppendToLog("tsMnuCloasAllWBs_Click\r\n" + ee.ToString());
            }
        }

        /// <summary>
        /// Update enable state of Edit menu items before displaying them
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsMnuEdit_DropDownOpening(object sender, EventArgs e)
        {
            try
            {
                tsEditMnuSelectAll.Enabled = m_CurWB.IsCommandEnabled("SelectAll");
                tsEditMnuCopy.Enabled = m_CurWB.IsCommandEnabled("Copy");
                tsEditMnuCut.Enabled = m_CurWB.IsCommandEnabled("Cut");
                tsEditMnuPaste.Enabled = m_CurWB.IsCommandEnabled("Paste");
            }
            catch (Exception ee)
            {
                AllForms.m_frmLog.AppendToLog("tsMnuEdit_DropDownOpening\r\n" + ee.ToString());
            }
        }

        /// <summary>
        /// Handles click event of frmThumbs pictureboxs
        /// Simulates a tab switch
        /// </summary>
        /// <param name="BrowserName"></param>
        void m_frmThumbs_ThumbImageClickEvent(string BrowserName)
        {
            try
            {
                SwitchTabs(BrowserName, FindTab(BrowserName), false);
            }
            catch (Exception ee)
            {
                AllForms.m_frmLog.AppendToLog("m_frmThumbs_ThumbImageClickEvent\r\n" + ee.ToString());
            }
        }

        /// <summary>
        /// Call back to intercept find requests from frmFind
        /// </summary>
        /// <param name="FindPhrase"></param>
        /// <param name="MatchWholeWord"></param>
        /// <param name="MatchCase"></param>
        /// <param name="Downward"></param>
        /// <param name="HighlightAll"></param>
        /// <param name="sColor"></param>
        void m_frmFind_FindInPageEvent(string FindPhrase, bool MatchWholeWord, bool MatchCase, bool Downward, bool HighlightAll, string sColor)
        {
            try
            {
                if (HighlightAll)
                {
                    int found = m_CurWB.FindAndHightAllInPage(FindPhrase, MatchWholeWord, MatchCase, sColor, "black");
                    MessageBox.Show(this, string.Format(Properties.Resources.FoundMatches, found), Properties.Resources.FindInPage, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    if (m_CurWB.FindInPage(FindPhrase, Downward, MatchWholeWord, MatchCase, true) == false)
                        MessageBox.Show(this, string.Format(Properties.Resources.NoMoreMatchesFound,FindPhrase), Properties.Resources.FindInPage, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ee)
            {
                AllForms.m_frmLog.AppendToLog("frmMain_m_frmFind_FindInPageEvent\r\n" + ee.ToString());
            }
        }
        
        #endregion

   #region Favorites Handling

        //Use a FileSystemWatcher to determine whether to reload favorites upon
        //dropping down or not.
        //To be more effeicent, I would have, in case of
        //Create, insert a new menu item in the appropriate index
        //delete, remove the menu item
        //renamed, modify the text
        //changed, modify text and/or url
        private bool m_FavNeedReload = false;

        private void fswFavorites_Created(object sender, FileSystemEventArgs e)
        {
            m_FavNeedReload = true;
            //e.ChangeType.ToString();
            //e.FullPath;
            //e.Name;
        }

        private void fswFavorites_Deleted(object sender, FileSystemEventArgs e)
        {
            m_FavNeedReload = true;
            //try
            //{
            //    //If a link then we remove it
            //    ToolStripItem itema = null;
            //    foreach (ToolStripItem item in tsLinks.Items)
            //    {
            //        if (item.Name == e.Name)
            //        {
            //            itema = item;
            //            break;
            //        }
            //    }
            //    if (itema != null)
            //        tsLinks.Items.Remove(itema);
            //}
            //catch (Exception ee)
            //{
            //    AllForms.m_frmLog.AppendToLog("fswFavorites_Deleted\r\n" + ee.ToString());
            //}
        }

        private void fswFavorites_Renamed(object sender, RenamedEventArgs e)
        {
            m_FavNeedReload = true;
        }

        private void fswFavorites_Changed(object sender, FileSystemEventArgs e)
        {
            m_FavNeedReload = true;
        }

        private void LoadFavoriteMenuItems()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                DirectoryInfo objDir = new DirectoryInfo(fswFavorites.Path);
                //Recurse, starting from main dir
                LoadFavoriteMenuItems(objDir, tsFavoritesMnu);
                this.Cursor = Cursors.Default;
            }
            catch (Exception ee)
            {
                this.Cursor = Cursors.Default;
                AllForms.m_frmLog.AppendToLog("LoadFavoriteMenuItems\r\n" + ee.ToString());
            }
        }

        /// <summary>
        /// Recursive function
        /// </summary>
        /// <param name="objDir"></param>
        private void LoadFavoriteMenuItems(DirectoryInfo objDir, ToolStripMenuItem menuitem)
        {
            try
            {
                string strName = string.Empty;
                string strUrl = string.Empty;
                
                DirectoryInfo[] dirs = objDir.GetDirectories();
                foreach (DirectoryInfo dir in dirs)
                {
                    
                    ToolStripMenuItem diritem = new ToolStripMenuItem(dir.Name, tsFileMnuOpen.Image);
                    menuitem.DropDownItems.Add((ToolStripItem)diritem);
                    LoadFavoriteMenuItems(dir, diritem);
                }

                bool addlinks = (objDir.Name.Equals("links", 
                    StringComparison.CurrentCultureIgnoreCase)) ? true : false;
                FileInfo[] urls = objDir.GetFiles("*.url");
                foreach (FileInfo url in urls)
                {
                    strName = Path.GetFileNameWithoutExtension(url.Name);
                    strUrl = m_CurWB.ResolveInternetShortCut(url.FullName);
                    //load up quick links
                    if (addlinks)
                    {
                        ToolStripButton btn = new ToolStripButton(strName, m_imgUnLock);
                        btn.Tag = strUrl;
                        btn.Click += new EventHandler(ToolStripLinksButtonClickHandler);
                        tsLinks.Items.Add((ToolStripItem)btn);
                    }
                    ToolStripMenuItem item = new ToolStripMenuItem(strName, m_imgUnLock);
                    item.Tag = strUrl;
                    item.Click += new EventHandler(ToolStripFavoritesMenuClickHandler);
                    menuitem.DropDownItems.Add((ToolStripItem)item);
                }
            }
            catch (Exception ee)
            {
                AllForms.m_frmLog.AppendToLog("LoadFavoriteMenuItems\r\n" + ee.ToString());
            }
        }

        void ToolStripLinksButtonClickHandler(object sender, EventArgs e)
        {
            try
            {
                ToolStripItem item = (ToolStripItem)sender;
                if (item.Tag != null)
                    this.NavToUrl(item.Tag.ToString());
            }
            catch (Exception ee)
            {
                AllForms.m_frmLog.AppendToLog("ToolStripLinksButtonClickHandler\r\n" + ee.ToString());
            }
        }

        private void tsFavoritesMnu_DropDownOpening(object sender, EventArgs e)
        {
            if (!m_FavNeedReload)
                return;
            m_FavNeedReload = false;
            try
            {
                //Reload favorites
                if (tsFavoritesMnu.DropDownItems.Count > 3)
                {
                    //Remove from back to front except the original items
                    for (int i = tsFavoritesMnu.DropDownItems.Count - 1; i > 2; i--)
                    {
                        if ((tsFavoritesMnu.DropDownItems[i] != tsFavoritesMnuAddToFavorites) &&
                            (tsFavoritesMnu.DropDownItems[i] != tsFavoritesMnuOrganizeFavorites) &&
                            (tsFavoritesMnu.DropDownItems[i] != tsFavoritesMnuSeparator))
                        {
                            tsFavoritesMnu.DropDownItems.Remove(tsFavoritesMnu.DropDownItems[i]);
                        }
                    }
                    for (int i = tsLinks.Items.Count - 1; i > 0; i--)
                    {
                        tsLinks.Items.Remove(tsLinks.Items[i]);
                    }
                }
                //Load favorites
                LoadFavoriteMenuItems();
            }
            catch (Exception ee)
            {
                AllForms.m_frmLog.AppendToLog("tsFavoritesMnu_DropDownOpening\r\n" + ee.ToString());
            }
        }

        private void ToolStripFavoritesMenuClickHandler(object sender, EventArgs e)
        {
            try
            {
                if (sender == tsFavoritesMnuAddToFavorites)
                {
                    m_CurWB.AddToFavorites();
                }
                else if (sender == tsFavoritesMnuOrganizeFavorites)
                {
                    m_CurWB.OrganizeFavorites();
                }
                ToolStripItem item = (ToolStripItem)sender;
                if (item.Tag != null)
                    this.NavToUrl(item.Tag.ToString());
            }
            catch (Exception ee)
            {
                AllForms.m_frmLog.AppendToLog("ToolStripFavoritesMenuClickHandler\r\n" + ee.ToString());
            }
        }

        #endregion

   #region TextSize
        
        /// <summary>
        /// Sets browser text size based on given parameter 0-4
        /// Adjust the chack state of textsize menu items
        /// </summary>
        /// <param name="iLevel">Text size level 0-4</param>
        private void SetZoomLevel(int iLevel)
        {
            tsViewMnuTextSizeLargest.Checked = false;
            tsViewMnuTextSizeLarger.Checked = false;
            tsViewMnuTextSizeMedium.Checked = false;
            tsViewMnuTextSizeSmaller.Checked = false;
            tsViewMnuTextSizeSmallest.Checked = false;

            switch (iLevel)
            {
                case 0:
                    tsViewMnuTextSizeLargest.Checked = true;
                    if (m_CurWB != null)
                        m_CurWB.TextSize = TextSizeWB.Largest;
                    break;
                case 1:
                    tsViewMnuTextSizeLarger.Checked = true;
                    if (m_CurWB != null)
                        m_CurWB.TextSize = TextSizeWB.Larger;
                    break;
                case 2:
                    tsViewMnuTextSizeMedium.Checked = true;
                    if (m_CurWB != null)
                        m_CurWB.TextSize = TextSizeWB.Medium;
                    break;
                case 3:
                    tsViewMnuTextSizeSmaller.Checked = true;
                    if (m_CurWB != null)
                        m_CurWB.TextSize = TextSizeWB.Smaller;
                    break;
                case 4:
                    tsViewMnuTextSizeSmallest.Checked = true;
                    if (m_CurWB != null)
                        m_CurWB.TextSize = TextSizeWB.Smallest;
                    break;
            }
        }

        /// <summary>
        /// Hanldes textsize menu item clicks
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsMnuTextSizeClickHandler(object sender, EventArgs e)
        {
            if (sender == tsViewMnuTextSizeLargest)
                SetZoomLevel(0);
            else if (sender == tsViewMnuTextSizeLarger)
                SetZoomLevel(1);
            else if (sender == tsViewMnuTextSizeMedium)
                SetZoomLevel(2);
            else if (sender == tsViewMnuTextSizeSmaller)
                SetZoomLevel(3);
            else if (sender == tsViewMnuTextSizeSmallest)
                SetZoomLevel(4);
        }

        /// <summary>
        /// Updates the check state of the text size menu items before displaying them
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsMnuTextSize_DropDownOpening(object sender, EventArgs e)
        {
            tsViewMnuTextSizeLargest.Checked = false;
            tsViewMnuTextSizeLarger.Checked = false;
            tsViewMnuTextSizeMedium.Checked = false;
            tsViewMnuTextSizeSmaller.Checked = false;
            tsViewMnuTextSizeSmallest.Checked = false;
            if (cEXWB1.TextSize == TextSizeWB.Largest)
                tsViewMnuTextSizeLargest.Checked = true;
            else if (cEXWB1.TextSize == TextSizeWB.Larger)
                tsViewMnuTextSizeLarger.Checked = true;
            else if (cEXWB1.TextSize == TextSizeWB.Medium)
                tsViewMnuTextSizeMedium.Checked = true;
            else if (cEXWB1.TextSize == TextSizeWB.Smaller)
                tsViewMnuTextSizeSmaller.Checked = true;
            else if (cEXWB1.TextSize == TextSizeWB.Smallest)
                tsViewMnuTextSizeSmallest.Checked = true;
        } 

        #endregion

        #region WebBrowser Events

        private void cEXWB1_TitleChange(object sender, csExWB.TitleChangeEventArgs e)
        {
            if (sender != m_CurWB)
                return;

            this.Text = Application.ProductName + " - " + e.title;
        }

        private void cEXWB1_StatusTextChange(object sender, csExWB.StatusTextChangeEventArgs e)
        {
            if (sender != m_CurWB)
                return;
            if (e.text.Length > 0) m_Status = e.text;
            tsStatus.Text = e.text;
        }

        private void cEXWB1_BeforeNavigate2(object sender, csExWB.BeforeNavigate2EventArgs e)
        {
            /*
            //If toplevel then adjust the associated icon in the thumbs form
            try
            {
                if( (m_CurWB != null) && (m_CurWB == sender) && (e.istoplevel) )
                {
                    m_frmThumbs.SetLabelIcon(m_CurWB.Name, m_imgAniProgress);
                }
            }
            catch (Exception ee)
            {
                if(m_CurWB != null)
                    AllForms.m_frmLog.AppendToLog(m_CurWB.Name + "_BeforeNavigate2\r\n" + ee.ToString());
                else
                    AllForms.m_frmLog.AppendToLog("cEXWB1_BeforeNavigate2\r\n" + ee.ToString());
            }
            */
        }

        private void cEXWB1_CommandStateChange(object sender, csExWB.CommandStateChangeEventArgs e)
        {
            if (sender != m_CurWB)
                return;
            try
            {
                if (e.command == CommandStateChangeConstants.CSC_NAVIGATEBACK)
                    tsBtnBack.Enabled = e.enable;
                else if (e.command == CommandStateChangeConstants.CSC_NAVIGATEFORWARD)
                    tsBtnForward.Enabled = e.enable;
            }
            catch (Exception ee)
            {
                if(m_CurWB != null)
                    AllForms.m_frmLog.AppendToLog(m_CurWB.Name + "_CommandStateChange\r\n" + ee.ToString());
                else
                    AllForms.m_frmLog.AppendToLog("cEXWB1_CommandStateChange\r\n" + ee.ToString());
            }
        }

        private bool m_error = false;
        private void cEXWB1_DocumentComplete(object sender, csExWB.DocumentCompleteEventArgs e)
        {
            AllForms.m_frmLog.AppendToLog("cEXWBxx_DocumentComplete\r\n");
            
            try
            {
                
                if (e.istoplevel)
                {
                    
                    csExWB.cEXWB pWB = (csExWB.cEXWB)sender;
                    ToolStripButton btn = FindTab(pWB.Name);

                    
                    btn.Text = pWB.GetTitle(true);
                    if (btn.Text.Length == 0)
                    {
                        btn.Text = m_Blank;
                    }
                    else if (btn.Text.Length > m_MaxTextLen)
                        btn.Text = btn.Text.Substring(0, m_MaxTextLen) + "...";

                    btn.ToolTipText = HttpUtility.UrlDecode(e.url);
                    

                    // Web page is complete, so load the WatiN, DOM, and Source
                    txtHTMLSource.Text = pWB.GetSource(e.browser as IWebBrowser2);
                    WatiN.Core.IE.Settings.AutoStartDialogWatcher = false;
                    if (watinie == null)                   
                    {
                        watinie = WatiN.Core.IE.AttachToIE(WatiN.Core.Find.ByUrl(pWB.LocationUrl));
                    }
                    lastelement = null;
                    LoadDOM(m_CurWB.WebbrowserObject.Document);
                    LoadWatinTree(watinie);
                    
                    m_error = false;

                    if (sender == m_CurWB)
                    {
                        this.comboURL.Text = btn.ToolTipText;
                        pWB.SetFocus();
                    }
                    
                }
                
            }
            catch (Exception ee)
            {
                if (m_CurWB != null)
                    AllForms.m_frmLog.AppendToLog(m_CurWB.Name + "_DocumentComplete\r\n" + ee.ToString());
                else
                    AllForms.m_frmLog.AppendToLog("cEXWBxx_DocumentComplete\r\n" + ee.ToString());
            }             
        }

        private void cEXWB1_DocumentCompleteEX(object sender, csExWB.DocumentCompleteExEventArgs e)
        {
            AllForms.m_frmLog.AppendToLog("cEXWBxx_DocumentCompleteEX\r\n");
            //Activate this event, if you need to process the source HTML before
            //any scripts have been executed.
        }

        //Handling HTMLDocument and HTMLWindow events
        private void cEXWB1_HTMLEvent(object sender, csExWB.HTMLEventArgs e)
        {
            try
            {
                //Window events are not cancellable
                if (e.m_EventType == HTMLEventType.HTMLWindowEvent)
                {
                    if (e.m_EventDispId == HTMLEventDispIds.ID_ONLOAD)
                    {
                        csExWB.cEXWB pWB = (csExWB.cEXWB)sender;
                        if (pWB != null)
                            AllForms.m_frmLog.AppendToLog(pWB.Name + "_ONLOAD fired.");
                        else
                            AllForms.m_frmLog.AppendToLog("UNKNOWN_ONLOAD fired.");
                    }
                }
                else if (e.m_EventType == HTMLEventType.HTMLDocumentEvent)
                {
                    if (e.m_EventDispId == HTMLEventDispIds.ID_ONCLICK)
                    {
                        //Cancellable
                        if ( (e.m_pEvtObj != null) &&
                            (e.m_pEvtObj.SrcElement != null) && 
                            (!string.IsNullOrEmpty(e.m_pEvtObj.SrcElement.tagName)) )
                        {
                            if (e.m_pEvtObj.SrcElement.tagName == "A")
                            {
                                IHTMLAnchorElement anchor = (IHTMLAnchorElement)e.m_pEvtObj.SrcElement;
                                if (anchor != null)
                                    AllForms.m_frmLog.AppendToLog("A Link Clicked:\r\n" + anchor.href);
                                else
                                    AllForms.m_frmLog.AppendToLog("Unable to retrevie clicked link html anchor");
                            }
                            else
                                AllForms.m_frmLog.AppendToLog("An HTML element was clicked - tagname:\r\n" + e.m_pEvtObj.SrcElement.tagName);
                        }
                    }
                }
            }
            catch (Exception ee)
            {
                if (m_CurWB != null)
                    AllForms.m_frmLog.AppendToLog(m_CurWB.Name + "_HTMLEvent\r\n" + ee.ToString());
                else
                    AllForms.m_frmLog.AppendToLog("cEXWB1_HTMLEvent\r\n" + ee.ToString());
            }

        }

        private void cEXWB1_NavigateComplete2(object sender, csExWB.NavigateComplete2EventArgs e)
        {
            AllForms.m_frmLog.AppendToLog("cEXWBxx_NavigateComplete2 "+e.url+"\r\n");
        }

        private void cEXWB1_NavigateError(object sender, csExWB.NavigateErrorEventArgs e)
        {
            if (m_CurWB != null)
            {
                AllForms.m_frmLog.AppendToLog(m_CurWB.Name +
                    "_NavigateError\r\nURL\r\n" + HttpUtility.UrlDecode(e.url) +
                    "\r\nStatus Code\r\n" + e.statuscode.ToString());
            }
            else
            {
                AllForms.m_frmLog.AppendToLog("cEXWBxx_NavigateError\r\nURL\r\n" + HttpUtility.UrlDecode(e.url) +
                    "\r\nStatus Code\r\n" + e.statuscode.ToString());

            }
            //Any nav errors, cancel it
            e.Cancel = true;
        }

        private void AssignPopup(ref object obj)
        {
            if (m_CurWB != null)
            {
                if (!m_CurWB.RegisterAsBrowser)
                    m_CurWB.RegisterAsBrowser = true;
                obj = m_CurWB.WebbrowserObject;
            }
        }

        private void cEXWB1_WBEvaluteNewWindow(object sender, csExWB.EvaluateNewWindowEventArgs e)
        {
            // modeless/modal flags=38
            if ((e.flags & NWMF.HTMLDIALOG)==NWMF.HTMLDIALOG)
            {
                if (MessageBox.Show(Properties.Resources.showDialogBug, Properties.Resources.KnownBug, MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                {
                    e.Cancel = true;
                }
            }
            else
            {
                m_frmPopup.Show();
                m_frmPopup.PopupName = wscript.AddPopup(wscript.settings.PopupIEName, e.url);
                m_frmPopup.SetURL(this, wscript, e.url);
            }
        }

        private void cEXWB1_NewWindow2(object sender, csExWB.NewWindow2EventArgs e)
        {
            if (!m_frmPopup.Visible)
            {
                m_frmPopup.Show(this);
            }

            csExWB.cEXWB pWB = (csExWB.cEXWB)sender;
            if ((pWB != null) && (!pWB.RegisterAsBrowser))
            {
                pWB.RegisterAsBrowser = true;
            }

            m_frmPopup.AssignBrowserObject(ref e.browser);
        }

        private void cEXWB1_NewWindow3(object sender, csExWB.NewWindow3EventArgs e)
        {
            if (!m_frmPopup.Visible)
            {
                m_frmPopup.Show(this);
            }

            csExWB.cEXWB pWB = (csExWB.cEXWB)sender;
            if ((pWB != null) && (!pWB.RegisterAsBrowser))
            {
                pWB.RegisterAsBrowser = true;
            }

            m_frmPopup.AssignBrowserObject(ref e.browser);
        }

        private void cEXWB1_ProgressChange(object sender, csExWB.ProgressChangeEventArgs e)
        {
            if (sender != m_CurWB)
                return;

            if ((e.progress == -1) || (e.progressmax == e.progress))
            {
                tsProgress.Value = 0; // 100;
                tsProgress.Maximum = 0;
                return;
            }
            if ((e.progressmax > 0) && (e.progress > 0) && (e.progress < e.progressmax))
            {
                tsProgress.Maximum = e.progressmax;
                tsProgress.Value = e.progress; //* 100) / tsProgress.Maximum;
            }
        }

        private void cEXWB1_ScriptError(object sender, csExWB.ScriptErrorEventArgs e)
        {
            string wbname = (m_CurWB != null) ? m_CurWB.Name : "cEXWBxx";
            AllForms.m_frmLog.AppendToLog(wbname + "_ScriptError - Continuing to run scripts");
            AllForms.m_frmLog.AppendToLog("Error Message" + e.errorMessage + "\r\nLine Number" + e.lineNumber.ToString());
        }

        private void cEXWB1_SetSecureLockIcon(object sender, csExWB.SetSecureLockIconEventArgs e)
        {
            if (sender != m_CurWB)
                return;

            UpdateSecureLockIcon(e.securelockicon);
        }

        private void cEXWB1_WBContextMenu(object sender, csExWB.ContextMenuEventArgs e)
        {
            if (wscript.Recording)
            {
                e.displaydefault = false;
                IHTMLElement pelem = cEXWB1.GetActiveElement();
                ShowPropertyWindow(pelem, e.pt);
            }
        }

        private void cEXWB1_WBDocHostShowUIShowMessage(object sender, csExWB.DocHostShowUIShowMessageEventArgs e)
        {
            string wbname = (m_CurWB != null) ? m_CurWB.Name : "cEXWBxx";
            AllForms.m_frmLog.AppendToLog(wbname + "_WBDocHostShowUIShowMessage - Text\r\n" + e.text);

            
            // simple alert dialog
            if (e.type==48)
            {
                MessageBox.Show(e.text, e.caption);
                wscript.AddAlertHandler(wscript.settings.BaseIEName);
            }
            // confirm dialog
            else if (e.type == 33)
            {
                DialogResult result = MessageBox.Show(e.text, e.caption, MessageBoxButtons.OKCancel);
                wscript.AddConfirmHandler(wscript.settings.BaseIEName, result);
                e.result = Convert.ToInt32(result);
                
            }
            e.handled = true;

        }

        private void cEXWB1_WBSecurityProblem(object sender, csExWB.SecurityProblemEventArgs e)
        {
            //if (e.problem != WinInetErrors.HTTP_REDIRECT_NEEDS_CONFIRMATION)
            //{
            //    e.handled = true;
            //    e.retvalue = Hresults.E_ABORT;
            //}
            
            string wbname = (m_CurWB != null) ? m_CurWB.Name : "cEXWBxx";
            AllForms.m_frmLog.AppendToLog(wbname + "_WBSecurityProblem - Wininet Problem=" + e.problem.ToString());
        }

        private void cEXWB1_WindowClosing(object sender, csExWB.WindowClosingEventArgs e)
        {
            //Ask, or read from users options what to do. For now
            e.Cancel = true;
            AllForms.m_frmLog.AppendToLog("frmMain_cEXWB1_WindowClosing");
        }

        private void cEXWB1_WBKeyUp(object sender, csExWB.WBKeyUpEventArgs e)
        {
            if (e.keycode==Keys.ShiftKey)
            {
                ShiftKeyDown = false;
            }
            else if (e.keycode == Keys.ControlKey)
            {
                ControlKeyDown = false;
            }
            // update the current element line.
//            KeyEntryElement = cEXWB1.GetActiveElement();
            
            
        }

        private void WriteKeys()
        {
            if (cEXWB1.GetActiveElement() != textActiveElement)
            {
                if (textActiveElement == null) return;
                // This means we're done with a cycle for TextActive keys.
                // Write it out
                try
                {
                    bool result = textActiveElement.isTextEdit;
                } catch (System.UnauthorizedAccessException e)
                {
                    // on this case, it means that the element has been navigated "away"
                    // so let's move on
                    textActiveElement = null;
                    return;
                } catch (System.Exception e){
                    throw (e);
                }
                
                wscript.AddTyping(wscript.settings.BaseIEName, textActiveElement);
            }


        }

        private void cEXWB1_WBKeyDown(object sender, csExWB.WBKeyDownEventArgs e)
        {
            WriteKeys();


            textActiveElement = cEXWB1.GetActiveElement();


            KeyEntryElement = cEXWB1.GetActiveElement();
           
            if (e.keycode==Keys.ControlKey)
            {
                ControlKeyDown = true;
            }
            else if (e.keycode == Keys.ShiftKey)
            {
                ShiftKeyDown = true;
            }
            else
            {
                if (e.keycode != Keys.Tab)
                {

                    wscript.AddKeys(ShiftKeyDown, e.keycode);
                    LastKeyTime = DateTime.Now;
                }
            }            
            
            //Consume keys here, if needed
            try
            {
                if (e.virtualkey == Keys.ControlKey)
                {
                    switch (e.keycode)
                    {
                        case Keys.F:
                            m_frmFind.Show(this);
                            e.handled = true;
                            break;
                        case Keys.N:
                            AddNewBrowser(m_Blank, m_AboutBlank, string.Empty, true);
                            e.handled = true;
                            break;
                        case Keys.O:
                            AddNewBrowser(m_Blank, m_AboutBlank, string.Empty, true);
                            e.handled = true;
                            break;
                    }
                }
            }
            catch (Exception eex)
            {
                if (m_CurWB != null)
                    AllForms.m_frmLog.AppendToLog(m_CurWB.Name + "_WBKeyDown\r\n" + eex.ToString());
                else
                    AllForms.m_frmLog.AppendToLog("cEXWBxx_WBKeyDown\r\n" + eex.ToString());            
            }
        }

        private void cEXWB1_DownloadBegin(object sender)
        {
            if (sender != m_CurWB)
                return;
            tsProgress.Visible = true;
        }

        private void cEXWB1_DownloadComplete(object sender)
        {
            if (sender != m_CurWB)
                return;
            tsProgress.Value = 0;
            tsProgress.Maximum = 0;
            tsProgress.Visible = false;
        }

        private void cEXWB1_RefreshBegin(object sender)
        {
            /*
            try
            {
                csExWB.cEXWB pWB = (csExWB.cEXWB)sender;
                if( (m_CurWB != null) && (m_CurWB == pWB) )
                {
                    m_frmThumbs.SetLabelIcon(m_CurWB.Name, m_imgAniProgress);
                }
            }
            catch (Exception eex)
            {
                if (m_CurWB != null)
                    AllForms.m_frmLog.AppendToLog(m_CurWB.Name + "_RefreshBegin\r\n" + eex.ToString());
                else
                    AllForms.m_frmLog.AppendToLog("cEXWBxx_RefreshBegin\r\n" + eex.ToString());
            }
            */
        }

        private void cEXWB1_RefreshEnd(object sender)
        {
            //
        }

        private void cEXWB1_FileDownload(object sender, csExWB.FileDownloadEventArgs e)
        {
            //Here is the easiest way to find out the download file name
            //m_Status is set in StatusTextChange event handler
            //After the user has clicked a downloadable link, we get a 
            //BeforeNavigate2 and then at least two calls to StatusTextChange
            //One containing a text such as below
            //Start downloading from site: http://www.codeproject.com/cs/media/cameraviewer/cv_demo.zip
            //and one that sends an empty string to clear the status text.
            //After the status calls, we should get this event fired.
            //AllForms.m_frmLog.AppendToLog("cEXWBxx_FileDownload\r\n" + m_Status);

            //Here you can cancel the download and take over.
            //e.Cancel = true;
        }
        
        private void cEXWB1_WBAuthenticate(object sender, csExWB.AuthenticateEventArgs e)
        {
            if (m_frmAuth.ShowDialogInternal(this) == DialogResult.OK)
            {
                //Default value of handled is false
                e.handled = true;
                //To pass a doamin as in a NTLM authentication scheme,
                //use this format: Username = Domain\username
                e.username = m_frmAuth.m_Username;
                e.password = m_frmAuth.m_Password;

                wscript.AddLoginDialog(wscript.settings.BaseIEName, e.username, e.password);

                //Default value of retValue is 0 or S_OK
            }
        }
     
        private void cEXWB1_WBDragDrop(object sender, csExWB.WBDropEventArgs e)
        {
            if (e.dataobject == null)
                return;
            if (e.dataobject.ContainsText())
                AllForms.m_frmLog.AppendToLog("cEXWB1_WBDragDrop\r\n" + e.dataobject.GetText());
            else if (e.dataobject.ContainsFileDropList())
            {
                System.Collections.Specialized.StringCollection files = e.dataobject.GetFileDropList();
                if (files.Count > 1)
                    MessageBox.Show(Properties.Resources.CanNotDoMultiFileDrop);
                else
                {
                    if(m_CurWB != null)
                        m_CurWB.Navigate(files[0]);
                }
            }
            else
            {
                //Example of how to catch a dragdrop of a ListViewItem from frmCacheCookie form
                object obj = e.dataobject.GetData("WindowsForms10PersistentObject");
                if (obj != null)
                {
                    if (obj is ListViewItem)
                    {
                        ListViewItem ctl = (ListViewItem)obj;
                        AllForms.m_frmLog.AppendToLog("cEXWB1_WBDragDrop\r\n" + ctl.Text);
                    }
                }
            }
            //To get the available formats
            //string[] formats = obja.GetFormats();
            //foreach (string str in formats)
            //{
            //    Debug.Print("\r\n" + str);
            //}
        }

        #endregion


        #region DOM Handler

        private const string TEXTNODE = "#text";
        private const string COMMENTNODE = "#comment";
        private const string FRAMENODE = "FRAME";
        private const string BASENODE = "BASE";
        private const string BODYNODE = "BODY";
        private const string VALUESEPERATOR = " \"";
        private const string VALUESEPERATOR1 = "\"";

        /// <summary>
        /// Starting point to walk the DOM
        /// </summary>
        /// <param name="DocumentObject">Webbrowser.Document object</param>
        public void LoadDOM(object DocumentObject)
        {
            treeDOM.Nodes.Clear();
            arrSelectItems.Clear();

            if (DocumentObject == null)
            {
                return;
            }

            try
            {
                IHTMLDocument3 doc3 = DocumentObject as IHTMLDocument3;
                IHTMLDOMNode domnode = (IHTMLDOMNode)doc3.documentElement;
                //Start walking
                if (domnode != null)
                    parseNodes(domnode, null);
                if (treeDOM.Nodes.Count > 0)
                    treeDOM.Nodes[0].Expand();
            }
            catch (Exception ex)
            {
                AllForms.m_frmLog.AppendToLog("Load DOM failed: " + ex.Message);
            }
        }

        /// <summary>
        /// Recursive method to walk the DOM, acounts for frames
        /// </summary>
        /// <param name="nd">Parent DOM node to walk</param>
        /// <param name="node">Parent tree node to populate</param>
        /// <returns></returns>
        private TreeNode parseNodes(IHTMLDOMNode nd, TreeNode node)
        {
            if (nd == null)
            {
                return null;
            }
            string str = nd.nodeName;
            TreeNode nextnode = null;

            //Add a new node to tree
            if (node != null)
            {
                nextnode = node.Nodes.Add(str);
                nextnode.Tag = nd as IHTMLElement;

                IHTMLElement _elem = nd as IHTMLElement;
                if (str.ToLower() == "select")
                {
                    mshtml.HTMLSelectElementClass sel = _elem as mshtml.HTMLSelectElementClass;
                    if (arrSelectItems.IndexOf(sel.GetHashCode()) == -1)
                    {
                        arrSelectItems.Add(sel.GetHashCode());
                        sel.HTMLSelectElementEvents2_Event_onchange += new mshtml.HTMLSelectElementEvents2_onchangeEventHandler(sel_HTMLSelectElementEvents2_Event_onchange);
                    }
                }
            }

            else
            {
                nextnode = treeDOM.Nodes.Add(str);
                nextnode.Tag = nd as IHTMLElement;
            }

            //For each child, get children collection
            //And continue walking up and down the DOM
            try
            {
                //Frame?
                if (str == FRAMENODE)
                {
                    //Get the nd.IWebBrowser2.IHTMLDocument3.documentelement and recurse
                    IWebBrowser2 wb = (IWebBrowser2)nd;
                    IHTMLDocument3 doc3 = (IHTMLDocument3)wb.Document;

                    IHTMLDOMNode tempnode = (IHTMLDOMNode)doc3.documentElement;
                    //get the comments for this node, if any
                    IHTMLDOMChildrenCollection framends = (IHTMLDOMChildrenCollection)doc3.childNodes;
                    foreach (IHTMLDOMNode tmpnd in framends)
                    {
                        str = tmpnd.nodeName;
                        if (COMMENTNODE == str)
                        {
                            if (tmpnd.nodeValue != null)
                                str += VALUESEPERATOR + tmpnd.nodeValue.ToString() + VALUESEPERATOR1;
                            if (nextnode != null)
                            {
                                TreeNode newnode = nextnode.Nodes.Add(str);
                                newnode.Tag = tmpnd as IHTMLElement;
                            }

                        }
                    }
                    //parse document
                    parseNodes(tempnode, nextnode);
                    return nextnode;
                }

                //Get the DOM collection
                string strdom = string.Empty;
                IHTMLDOMChildrenCollection nds = (IHTMLDOMChildrenCollection)nd.childNodes;
                foreach (IHTMLDOMNode childnd in nds)
                {
                    strdom = childnd.nodeName;
                    //Attempt to extract text and comments
                    if ((COMMENTNODE == strdom) || (TEXTNODE == strdom))
                    {
                        if (childnd.nodeValue != null)
                            strdom += VALUESEPERATOR + childnd.nodeValue.ToString() + VALUESEPERATOR1;
                        //Add a new node to tree
                        if (nextnode != null)
                        {
                            TreeNode newnode = nextnode.Nodes.Add(strdom);
                            newnode.Tag = childnd as IHTMLElement;
                        }
                    }
                    else
                    {
                        if ((BODYNODE == strdom) &&
                            (str == BASENODE))
                        {
                            //In MSDN, one of the inner FRAMEs BASE element
                            //contains the BODY element???
                            //Do nothing
                        }
                        else
                        {
                            parseNodes(childnd, nextnode);
                        }
                    }
                }

            }
            catch (System.InvalidCastException icee)
            {
                Console.Write("\r\n InvalidCastException =" +
                    icee.ToString() + "\r\nName =" +
                    str + " \r\n");
            }
            catch (Exception) //Anything else throw it
            {
                throw;
            }
            return nextnode;
        }

        private void sel_HTMLSelectElementEvents2_Event_onchange(mshtml.IHTMLEventObj pEvtObj)
        {
            if (pEvtObj.srcElement.getAttribute("value", 0)==null)
            {
                wscript.AddSelectListItem(wscript.settings.BaseIEName, (pEvtObj.srcElement as IHTMLElement), false);
            }
            else
            {
                wscript.AddSelectListItem(wscript.settings.BaseIEName, (pEvtObj.srcElement as IHTMLElement), true);
            }
            
        }

        private void saveTreeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (treeDOM.Nodes.Count == 0)
                    return;

                if (AllForms.ShowStaticSaveDialogForXML(this) == DialogResult.OK)
                {
                    TreeViewSerializer savetree = new TreeViewSerializer();
                    savetree.SerializeTreeView(treeDOM, AllForms.m_dlgSave.FileName);
                }
            }
            catch (Exception ee)
            {
                AllForms.m_frmLog.AppendToLog("frmDOM::saveTreeToolStripMenuItem_Click\r\n" + ee.ToString());
            }
        }

        private WatiN.Core.Element FindElement(IHTMLElement element)
        {
            WatiN.Core.Element result = null;
            if (watinie==null)
            {
                return result;
            }
            for (int i = 0; i < watinie.Elements.Length; i++)
            {
                if (watinie.Elements[i].HTMLElement == element)
                {
                    result = watinie.Elements[i];
                    break;
                }
            }
            return result;
        }

        private string GetNodeAttribute(IHTMLElement element, string Attribute)
        {
            if (element==null)
            {
                return "";
            }

            string result = "";
            try
            {
                Object objResult = element.getAttribute(Attribute, 0);
                if (objResult!=null)
                {
                    result = objResult.ToString();
                }
            }
            catch 
            {
                // do nothing
            }

            return result;
        }

        private void HighlightElement(IHTMLElement element)
        {
            if (lastelement != null)
            {
                lastelement.style.setAttribute("backgroundColor", OriginalColor, 0);
            }

            if (element == null)
            {
                return;
            }

            lastelement = element;
            Object _objColor = lastelement.style.getAttribute("backgroundColor", 0);
            if (_objColor != null)
            {
                OriginalColor = _objColor.ToString();
            }
            else
            {
                OriginalColor = "";
            }
            lastelement.style.setAttribute("backgroundColor", wscript.settings.DOMHighlightColor.ToKnownColor().ToString(), 0);
        }

        private void treeDOM_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            IHTMLElement _element = null;
            try
            {
                _element = e.Node.Tag as IHTMLElement;
                HighlightElement(_element);
            }
            catch (Exception ex)
            {
                AllForms.m_frmLog.AppendToLog("DOM Node Mouse Click Failed: " + ex.Message);
                tsStatus.Text = Properties.Resources.DOMErrorHighlighting;
                return;
            }
            
            if (e.Button == MouseButtons.Right)
            {
                frmPropertyExplorer frm = new frmPropertyExplorer();
                frm.Show();
                frm.SetWindowProperties(wscript, wscript.settings.BaseIEName, _element);
                return;
            }
        }
        #endregion


        #region ScriptCodeInterface
        
        private void txtCode_TextChanged(object sender, EventArgs e)
        {
            wscript.UnsavedScript = true;
        }

        private void menuSettings_Click(object sender, EventArgs e)
        {
            frmSettings frm = new frmSettings();
            AppSettings.CodeLanguages _language = wscript.settings.CodeLanguage;
            frm.ObjectToGUI(wscript.settings);
            if (frm.ShowDialog()==DialogResult.OK)
            {
                frm.GUIToObject(wscript.settings);
                wscript.settings.SaveSettings(txtCode);
                if (_language != wscript.settings.CodeLanguage)
                {
                    if (MessageBox.Show(Properties.Resources.LanguageChange,Properties.Resources.ChangeLanguage,MessageBoxButtons.YesNo, MessageBoxIcon.Information)==DialogResult.Yes)
                    {
                        Application.Restart();
                    }
                }
                txtCode.Font = wscript.settings.ScriptWindowFont;
            }
            frm.Dispose();
        }

        private void GenericTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.C)
            {
                (sender as TextBox).Copy();
            }
            else if (e.Control && e.KeyCode == Keys.X)
            {
                (sender as TextBox).Cut();
            }
            else if (e.Control && e.KeyCode == Keys.V)
            {
                (sender as TextBox).Paste();
            }
            else if (e.Control && e.KeyCode == Keys.Z)
            {
                (sender as TextBox).Undo();
            }
            else if (e.Control && e.KeyCode == Keys.A)
            {
                (sender as TextBox).SelectAll();
            }
        }

        private void GenericRichText_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.C)
            {
                (sender as RichTextBox).Copy();
            }
            else if (e.Control && e.KeyCode == Keys.X)
            {
                (sender as RichTextBox).Cut();
            }
            else if (e.Control && e.KeyCode == Keys.V)
            {
                (sender as RichTextBox).Paste();
            }
            else if (e.Control && e.KeyCode == Keys.Z)
            {
                (sender as RichTextBox).Undo();
            }
            else if (e.Control && e.KeyCode == Keys.A)
            {
                (sender as RichTextBox).SelectAll();
            }
        }

        private void tsbLoad_Click(object sender, EventArgs e)
        {
            if (openCodeDialog.ShowDialog() == DialogResult.Cancel)
            {
                return;
            }

            if (wscript.Recording)
            {
                tsbStop_Click(null, e);
            }

            wscript.LoadScript(openCodeDialog.FileName);
            lbTests.Items.Clear();
            txtCode.Text = "";
            for (int i = 0; i < wscript.RecordedTests.Count; i++)
            {
                lbTests.Items.Add(wscript.RecordedTests.GetKey(i));
            }
            if (wscript.RecordedTests.Count>0)
            {
                lbTests.SelectedIndex = 0;
                txtCode.Text = wscript.RecordedTests[0];
            }
        }

        void UnboldToolstripItems(ToolStripItemCollection items)
        {
            foreach (ToolStripItem item in items)
            {
                Font newfont = new Font(item.Font, FontStyle.Regular);
                item.Font = newfont;
            }
        }

        private void tsbSave_Click(object sender, EventArgs e)
        {
            if (txtCode.Text.Trim().Length == 0 && wscript.RecordedTests.Count==0)
            {
                return;
            }

            if (wscript.Recording)
            {
                tsbStop_Click(null, e);
            }

            saveCodeDialog.Filter = wscript.TemplateFiles.GetSaveFilter(wscript.settings.CodeLanguage);

            if (saveCodeDialog.ShowDialog() == DialogResult.Cancel)
            {
                return;
            }

            // get the template
            if (saveCodeDialog.FilterIndex == 1)
            {
                wscript.SaveScript(saveCodeDialog.FileName);
            }
            else
            {
                Template tfile = wscript.TemplateFiles.GetTemplateByIndex(wscript.settings.CodeLanguage, saveCodeDialog.FilterIndex - 2);
                wscript.SaveScript(saveCodeDialog.FileName, tfile);
            }
        }

        private void tsbRecord_Click(object sender, EventArgs e)
        {
            if (txtTestName.Text.Trim() == "")
            {
                MessageBox.Show(Properties.Resources.TestsMustHaveAName, Properties.Resources.TestName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (!System.Text.RegularExpressions.Regex.IsMatch(txtTestName.Text, @"\A[a-zA-Z0-9_]+\z"))
            {
                MessageBox.Show(Properties.Resources.TestNameCanContainOnlyAZAnd09, Properties.Resources.TestName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // if the name is not in the list, clear
            if (wscript.GetTestIndex(txtTestName.Text) == -1)
            {
                txtCode.Text = "";
            }



            tsbRecord.Enabled = false;
            tsbStop.Enabled = true;
            txtTestName.Enabled = false;
            wscript.TestName = txtTestName.Text;
            wscript.Recording = true;
            wscript.ClearTimer();
        }

        private void tsbStop_Click(object sender, EventArgs e)
        {
            tsbRecord.Enabled = true;
            tsbStop.Enabled = false;
            txtTestName.Enabled = true;
            wscript.Recording = false;

            // write to a name-value pair
            if (txtCode.Text != "")
            {
                int index = wscript.GetTestIndex(txtTestName.Text);
                if (index != -1)
                {
                    wscript.RecordedTests.Remove(txtTestName.Text);
                }
                else
                {
                    lbTests.Items.Add(txtTestName.Text);
                }
                wscript.RecordedTests.Add(txtTestName.Text, txtCode.Text);
                lbTests.SelectedIndex = wscript.GetTestIndex(txtTestName.Text);
            }
        }

        private void tsbDeleteTest_Click(object sender, EventArgs e)
        {
            if (lbTests.SelectedIndex == -1)
            {
                return;
            }

            try
            {
                wscript.RecordedTests.Remove(lbTests.SelectedItem.ToString());
                lbTests.Items.RemoveAt(lbTests.SelectedIndex);
            }
            catch
            {
                //
            }
        }

        private void tsbRunSelected_ButtonClick(object sender, EventArgs e)
        {
            if (wscript.Recording)
            {
                tsbStop_Click(null, e);
            }

            tsbRun.DropDown.Close();
            Application.DoEvents();

            if (wscript.settings.DefaultRunTemplate == "")
            {
                MessageBox.Show(Properties.Resources.NoDefaultTemplateSelected, Properties.Resources.NoDefault, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            Template tfile = wscript.TemplateFiles.GetTemplateByName(wscript.settings.DefaultRunTemplate);

            tsStatus.Text = Properties.Resources.CompilingScript;
            string result = wscript.CompileScript(txtCode.Text, tfile, true);

            if (result != "")
            {
                tsbRun.DropDown.Close();
                Application.DoEvents();
                MessageBox.Show(string.Format(Properties.Resources.ErrorsCompilingYourScript,result), Properties.Resources.CompilerErrors, MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            else
            {
                tsStatus.Text = Properties.Resources.CompilingCompletedWithNoErrorsRunningNow;
            }
        }

        private void tsbRunSelected_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (wscript.Recording)
            {
                tsbStop_Click(null, e);
            }

            tsbRun.DropDown.Close();
            Application.DoEvents();

            Template tfile = wscript.TemplateFiles.GetTemplateByName(wscript.settings.DefaultRunTemplate);

            // make it the default
            UnboldToolstripItems(tsbRunSelected.DropDownItems);
            Font newfont = new Font(e.ClickedItem.Font, FontStyle.Bold);
            e.ClickedItem.Font = newfont;
            wscript.settings.DefaultRunTemplate = e.ClickedItem.Text;
            wscript.settings.SaveSettings(txtCode);

            tsStatus.Text = Properties.Resources.CompilingScript;
            string result = wscript.CompileScript(txtCode.Text, tfile, true);

            if (result != "")
            {
                MessageBox.Show(string.Format(Properties.Resources.ErrorsCompilingYourScript, result), Properties.Resources.CompilerErrors, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }
            else
            {
                tsStatus.Text = Properties.Resources.CompilingCompletedWithNoErrorsRunningNow;
            }
        }

        private void tsbRun_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (wscript.Recording)
            {
                tsbStop_Click(null, e);
            }

            Template tfile = wscript.TemplateFiles.GetTemplateByName(e.ClickedItem.Text);

            // make it the default
            UnboldToolstripItems(tsbRun.DropDownItems);
            Font newfont = new Font(e.ClickedItem.Font, FontStyle.Bold);
            e.ClickedItem.Font = newfont;
            wscript.settings.DefaultRunTemplate = e.ClickedItem.Text;
            wscript.settings.SaveSettings(txtCode);

            tsStatus.Text = Properties.Resources.CompilingScript;
            string result = wscript.CompileScript(tfile, true);

            if (result != "")
            {
                tsbRun.DropDown.Close();
                Application.DoEvents();
                MessageBox.Show(string.Format(Properties.Resources.ErrorsCompilingYourScript, result), Properties.Resources.CompilerErrors, MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            else
            {
                tsStatus.Text = Properties.Resources.CompilingCompletedWithNoErrorsRunningNow;
            }
        }

        private void tsbRun_ButtonClick(object sender, EventArgs e)
        {
            if (wscript.Recording)
            {
                tsbStop_Click(null, e);
            }

            tsbRun.DropDown.Close();
            Application.DoEvents();

            if (wscript.settings.DefaultRunTemplate == "")
            {
                MessageBox.Show(Properties.Resources.NoDefaultTemplateSelected, Properties.Resources.NoDefault, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            Template tfile = wscript.TemplateFiles.GetTemplateByName(wscript.settings.DefaultRunTemplate);

            tsStatus.Text = Properties.Resources.CompilingScript;
            string result = wscript.CompileScript(tfile, true);

            if (result != "")
            {
                MessageBox.Show(string.Format(Properties.Resources.ErrorsCompilingYourScript, result), Properties.Resources.CompilerErrors, MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            else
            {
                tsStatus.Text = Properties.Resources.CompilingCompletedWithNoErrorsRunningNow;
            }
        }

        private void tsbCompile_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (wscript.Recording)
            {
                tsbStop_Click(null, e);
            }

            Template tfile = wscript.TemplateFiles.GetTemplateByName(e.ClickedItem.Text);

            // make it the default
            UnboldToolstripItems(tsbCompile.DropDownItems);
            Font newfont = new Font(e.ClickedItem.Font, FontStyle.Bold);
            e.ClickedItem.Font = newfont;
            wscript.settings.DefaultCompileTemplate = e.ClickedItem.Text;
            wscript.settings.SaveSettings(txtCode);

            tsStatus.Text = Properties.Resources.CompilingScript;
            string result = wscript.CompileScript(tfile, false);

            if (result != "")
            {
                tsbCompile.DropDown.Close();
                Application.DoEvents();
                MessageBox.Show(string.Format(Properties.Resources.ErrorsCompilingYourScript, result), Properties.Resources.CompilerErrors, MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            else
            {
                tsStatus.Text = Properties.Resources.CompilingCompletedWithNoErrors;
            }
        }

        private void tsbCompile_ButtonClick(object sender, EventArgs e)
        {
            if (wscript.Recording)
            {
                tsbStop_Click(null, e);
            }

            tsbCompile.DropDown.Close();
            Application.DoEvents();

            if (wscript.settings.DefaultCompileTemplate == "")
            {
                MessageBox.Show(Properties.Resources.NoDefaultTemplateSelected, Properties.Resources.NoDefault, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            Template tfile = wscript.TemplateFiles.GetTemplateByName(wscript.settings.DefaultCompileTemplate);

            tsStatus.Text = Properties.Resources.CompilingScript;
            string result = wscript.CompileScript(tfile, false);

            if (result != "")
            {
                MessageBox.Show(string.Format(Properties.Resources.ErrorsCompilingYourScript, result), Properties.Resources.CompilerErrors, MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            else
            {
                tsStatus.Text = Properties.Resources.CompilingCompletedWithNoErrors;
            }
        }

        private void tsbResources_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            tsbResources.DropDown.Close();
            Template tfile = wscript.TemplateFiles.GetTemplateByName(e.ClickedItem.Text);
            frmLocateResource frm = new frmLocateResource();
            frm.ShowResourceList(tfile, wscript.FunctionAssemblies, false);
        }

        private void lbTests_SelectedIndexChanged(object sender, EventArgs e)
        {
            // if the test name is different, stop recording, save the current (if not blank) and load the new
            if (lbTests.SelectedIndex == -1)
            {
                return;
            }

            if (lbTests.SelectedItem.ToString() != wscript.TestName)
            {
                if (wscript.Recording)
                {
                    tsbStop_Click(null, e);
                }
            }

            txtCode.Text = wscript.RecordedTests[lbTests.SelectedItem.ToString()].ToString();
            txtTestName.Text = lbTests.SelectedItem.ToString();
        }

        private void txtTestName_TextChanged(object sender, EventArgs e)
        {
            txtTestName.Text = System.Text.RegularExpressions.Regex.Replace(txtTestName.Text, @"[^a-zA-Z0-9_]+", "");
            txtTestName.SelectionStart = txtTestName.Text.Length;
        }

        private void watiNHomePageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NavToUrl("http://watin.sourceforge.net");
        }

#endregion


        #region WatinTree

        /// <summary>
        /// Load the elements tree
        /// </summary>
        /// <param name="internetExplorer">Internet Explorer instance, we are attached to</param>
        private void LoadWatinTree(WatiN.Core.IE internetExplorer)
        {
            if (internetExplorer != null)
            {
                treeWatin.Nodes.Clear();
                m_RootNode = new TreeNode("Web Page");
                treeWatin.Nodes.Add(m_RootNode);
                WatiN.Core.FrameCollection frames = watinie.Frames;
                if (frames.Length > 0)
                {
                    foreach (WatiN.Core.Frame frame in frames)
                    {
                        AddFrameElements(frame);
                    }
                }
                else
                    AddIEElements();
                m_RootNode.Expand();
            }
        }
        /// <summary>
        /// Add an new element node to a parent treenode
        /// </summary>
        /// <param name="parentNode">The parent node</param>
        /// <param name="element">The element</param>
        public void AddElementToNode(TreeNode parentNode, WatiN.Core.Element element)
        {
            TreeNode newNode = new TreeNode();
            string elementName = element.GetAttributeValue("name");
            string elementText = element.GetAttributeValue("text");
            string elementValue = element.GetAttributeValue("value");
            string elementType = element.GetAttributeValue("type");
            string tagName = element.TagName;

            bool isRadioButton = WatiN.Core.ElementTag.IsValidElement(element.HTMLElement, WatiN.Core.RadioButton.ElementTags);
            bool isImage = WatiN.Core.ElementTag.IsValidElement(element.HTMLElement, WatiN.Core.Image.ElementTags);
           
            if ((elementName != null) || (isRadioButton))
            {
                newNode.ForeColor = Color.Green;
                if (isRadioButton)
                {
                    newNode.Text = element.NextSibling.Text;
                }
                else
                    newNode.Text = element.Text + "Name: '" + elementName + "'";
            }
            else
            {
                if (elementName != null)
                {
                    newNode.ForeColor = Color.Orange;
                    newNode.Text = element.Text + ", Text: '" + elementText + "'";
                }
                else
                {
                    if (elementValue != null)
                    {
                        newNode.ForeColor = Color.DarkOrange;
                        newNode.Text = element.Text + ", Value: '" + elementValue + "'";
                    }
                    else if (element.Text != null)
                    {
                        newNode.ForeColor = Color.Orange;
                        newNode.Text = element.Text + "(Only text)";
                    }
                    else if ((element.TagName != null) && (isImage))
                    {
                        newNode.ForeColor = Color.Orange;
                        newNode.Text = element.GetAttributeValue("src") + "(Image)";
                    }
                    else
                    {
                        newNode.ForeColor = Color.Red;
                        newNode.Text = element.Text + ", (No name, value or text!)";
                    }
                }
            }

            if (((newNode.Text != null) && (newNode.Text.Length > 0)) || (isRadioButton) || (isImage))
            {
                newNode.Tag = element.HTMLElement;
                parentNode.Nodes.Add(newNode);
            }
        }

        /// <summary>
        /// Add all elements to a specific frame
        /// </summary>
        /// <param name="frame">The frame containing all elements to add</param>
        private void AddFrameElements(WatiN.Core.Frame frame)
        {
            AddFrameElements(frame, m_RootNode);
        }

        /// <summary>
        /// Add all elements to a specific frame
        /// </summary>
        /// <param name="frame">The frame containing all elements to add</param>
        private void AddFrameElements(WatiN.Core.Frame frame, TreeNode ParentNode)
        {
            TreeNode frameNode = new TreeNode("Frame");

            if (frame.Name.Length > 0)
            {
                frameNode.Text = "Frame Name: "+frame.Name;
            }
            else if (frame.Title.Length > 0)
            {
                frameNode.Text = "Frame Title: " + frame.Title;
            }
            else if (frame.Id.Length > 0)
            {
                frameNode.Text = "Frame Id: " + frame.Id ;
            }
            else if (frame.Url.Length>0)
            {
                frameNode.Text = "Frame URL: " + frame.Url;
            }
            
            ParentNode.Nodes.Add(frameNode);
            CreateControlTypeNodes(frameNode);

            AddElementsToNode(m_ButtonRootNode, frame.Buttons);
            AddElementsToNode(m_CheckBoxRootNode, frame.CheckBoxes);

            foreach (WatiN.Core.Frame childframe in frame.Frames)
                AddFrameElements(childframe, m_FrameRootNode);

            //WatiN.Core.HtmlDialogCollection htmlDialogs = watinie.HtmlDialogs;
            AddElementsToNode(m_ImageRootNode, frame.Images);
            AddElementsToNode(m_LabelRootNode, frame.Labels);
            AddElementsToNode(m_LinkRootNode, frame.Links);
            AddElementsToNode(m_RadioButtonRootNode, frame.RadioButtons);
            AddElementsToNode(m_SelectListRootNode, frame.SelectLists);
            AddElementsToNode(m_TableCellRootNode, frame.TableCells);
            AddElementsToNode(m_TableRowRootNode, frame.TableRows);
            AddElementsToNode(m_TableRootNode, frame.Tables);
            AddElementsToNode(m_TextFieldRootNode, frame.TextFields);
        }

        /// <summary>
        /// Add all elements from the root of IE
        /// </summary>
        private void AddIEElements()
        {
            TreeNode frameNode = new TreeNode(watinie.Title);
            frameNode.Text = "No name";
            if (watinie.Title.Length > 0)
                frameNode.Text = watinie.Title;
            m_RootNode.Nodes.Add(frameNode);
            CreateControlTypeNodes(frameNode);

            AddElementsToNode(m_ButtonRootNode, watinie.Buttons);
            AddElementsToNode(m_CheckBoxRootNode, watinie.CheckBoxes);
            AddElementsToNode(m_ImageRootNode, watinie.Images);
            AddElementsToNode(m_LabelRootNode, watinie.Labels);
            AddElementsToNode(m_LinkRootNode, watinie.Links);
            AddElementsToNode(m_RadioButtonRootNode, watinie.RadioButtons);
            AddElementsToNode(m_SelectListRootNode, watinie.SelectLists);
            AddElementsToNode(m_TableCellRootNode, watinie.TableCells);
            AddElementsToNode(m_TableRowRootNode, watinie.TableRows);
            AddElementsToNode(m_TableRootNode, watinie.Tables);
            AddElementsToNode(m_TextFieldRootNode, watinie.TextFields);
            //WatiN.Core.HtmlDialogCollection htmlDialogs = watinie.HtmlDialogs;
        }
        /// <summary>
        /// Add a collection of elements to a parent treenode
        /// </summary>
        /// <param name="parentNode">The parent node</param>
        /// <param name="elements">The collection of elements to add</param>
        private void AddElementsToNode<T>(TreeNode parentNode, WatiN.Core.BaseElementCollection<T> elements)
            where T : Element
        {
            foreach (WatiN.Core.Element element in elements)
                AddElementToNode(parentNode, element);
        }

        /// <summary>
        /// Add a controltype node to the parent node
        /// </summary>
        /// <param name="parentNode">The parent node</param>
        /// <param name="controlTypeNode">The node that was created as root controltype node</param>
        /// <param name="text">The controltype text</param>
        public void AddControlTypeNode(TreeNode parentNode, out TreeNode controlTypeNode, string text)
        {
            controlTypeNode = new TreeNode();
            controlTypeNode.Text = text;
            parentNode.Nodes.Add(controlTypeNode);
        }
        /// <summary>
        /// Creates all the controltype nodes
        /// </summary>
        /// <param name="parentNode">The parent node to attach all the controltype nodes to</param>
        private void CreateControlTypeNodes(TreeNode parentNode)
        {
            AddControlTypeNode(parentNode, out m_ButtonRootNode, "Buttons");
            AddControlTypeNode(parentNode, out m_CheckBoxRootNode, "Checkboxes");
            AddControlTypeNode(parentNode, out m_FrameRootNode, "Frames");
            AddControlTypeNode(parentNode, out m_HtmlDialogRootNode, "HTML Dialogs");
            AddControlTypeNode(parentNode, out m_ImageRootNode, "Images");
            AddControlTypeNode(parentNode, out m_LabelRootNode, "Labels");
            AddControlTypeNode(parentNode, out m_LinkRootNode, "Links");
            AddControlTypeNode(parentNode, out m_RadioButtonRootNode, "Radio Buttons");
            AddControlTypeNode(parentNode, out m_SelectListRootNode, "Select Lists");
            AddControlTypeNode(parentNode, out m_TableCellRootNode, "Table Cells");
            AddControlTypeNode(parentNode, out m_TableRowRootNode, "Table Rows");
            AddControlTypeNode(parentNode, out m_TableRootNode, "Tables");
            AddControlTypeNode(parentNode, out m_TextFieldRootNode, "Text Fields");
        }

        private void treeWatin_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                Point clickedPoint = new Point(e.X, e.Y);
                TreeNode selectedNode = treeWatin.GetNodeAt(clickedPoint);
                if (selectedNode == null) return;
                Point screenPoint = treeWatin.PointToScreen(clickedPoint);
                Point clickPoint = this.PointToClient(screenPoint);
                IHTMLElement element = (IHTMLElement)selectedNode.Tag;
                if (element != null)
                {
                    try
                    {
                        HighlightElement(element);
                        ShowPropertyWindow(element, clickPoint);
                    }
                    catch (Exception ex)
                    {
                        AllForms.m_frmLog.AppendToLog("treeWatin_AfterSelect Error: " + ex.Message);
                    }
                }
            }
        }

        private void treeWatin_Click(object sender, EventArgs e)
        {
            treeWatin.SelectedNode = treeWatin.GetNodeAt(treeWatin.PointToClient(Cursor.Position));
        }

        private void treeWatin_AfterSelect(object sender, TreeViewEventArgs e)
        {
            IHTMLElement element = null;
            if (e.Node.Tag != null)
            {
                try
                {
                    element = (IHTMLElement)e.Node.Tag;
                    HighlightElement(element);
                }
                catch (Exception ex)
                {
                    AllForms.m_frmLog.AppendToLog("treeWatin_AfterSelect Error: " + ex.Message);
                }
            }
        }

        #endregion 

        private void watiNTestRecorderHomePageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NavToUrl("http://watintestrecord.sourceforge.net/");
        }

        private void tsbTimer_Click(object sender, EventArgs e)
        {
            tsbTimer.Checked = !tsbTimer.Checked;
            wscript.WaitTimerActive = tsbTimer.Checked;
            wscript.ClearTimer();
        }

        private void feedbackToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowFeedback();
        }

        private void ShowFeedback()
        {
            frmFeedback frm = new frmFeedback();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                EmailException mail = new EmailException();
                mail.SendMail(frm.txtComments.Text, "Comments", frm.txtEmail.Text, false, false);
            }
            wscript.settings.SaveSettings(txtCode);
        }

        private void timerFeedback_Tick(object sender, EventArgs e)
        {
            timerFeedback.Enabled = false;
            if (wscript.settings.RunCount>0 && wscript.settings.RunCount % 5 == 0)
            {
                ShowFeedback();
            }
        }

    }

    #region AllForms class
    
    /// <summary>
    /// Simple class to encapsulate global forms, controls, consts, and methods
    /// </summary>
    public sealed class AllForms
    {
        public const string COOKIE = "Cookie:";
        public const string VISITED = "Visited:";
        public const string DLG_HTMLS_FILTER = "Html files (*.html)|*.html|Htm files (*.htm)|*.htm|Text files (*.txt)|*.txt|All files (*.*)|*.*";
        public const string DLG_XMLS_FILTER = "XML files (*.xml)|*.xml";
        public const string DLG_TEXTFILES_FILTER  = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
        public const string DLG_IMAGES_FILTER = "Bmp files (*.bmp)|*.bmp" +
                        "|Gif files (*.gif)|*.gif" +
                        "|Jpeg files (*.Jpeg)|*.Jpeg" +
                        "|Png files (*.png)|*.png" +
                        "|Wmf files (*.wmf)|*.wmf" +
                        "|Tiff files (*.tiff)|*.tiff" +
                        "|Emf files (*.emf)|*.emf";

        public static frmLog m_frmLog = new frmLog();
        public static frmDynamicConfirm m_frmDynamicConfirm = new frmDynamicConfirm();
        public static ImageList m_imgListMain = new ImageList();
        public static SaveFileDialog m_dlgSave = new SaveFileDialog();
        public static OpenFileDialog m_dlgOpen = new OpenFileDialog();

        public static Icon BitmapToIcon(System.Drawing.Image orgImage)
        {
            Icon icoRet = null;
            if(orgImage == null)
                return icoRet;
            Bitmap bmp = new Bitmap(orgImage);
            icoRet = Icon.FromHandle(bmp.GetHicon());
            bmp.Dispose();
            return icoRet;
        }

        //Using m_imgListMain static member
        public static Icon BitmapToIcon(int orgImage)
        {
            Icon icoRet = null;
            if (AllForms.m_imgListMain.Images.Count > 0)
            {
                Bitmap bmp = new Bitmap(AllForms.m_imgListMain.Images[orgImage]);
                icoRet = Icon.FromHandle(bmp.GetHicon());
                bmp.Dispose();
            }
            return icoRet;
        }

        //replace = visited: or cookie:
        public static string SetupCookieCachePattern(string pattern, string replace)
        {
            const string COOKIECACHEPATTERN = ".*";
            const string DOT = ".";
            const string BACKSLASHDOT = "\\.";
            
            string url = pattern;
            if (url.Length > 0)
            {
                Uri curUri = new Uri(url);
                url = curUri.Host;
                //Replace "." with "\\."
                url = url.Replace(DOT, BACKSLASHDOT);
                url = replace + COOKIECACHEPATTERN + url;

                //www.google.com
                //visited:.*www\\.google\\.com

                //login.live.com
                //cookie:.*login\\.live\\.com

            }
            return url;
        }

        /// <summary>
        /// A little shortcut when asking for yes or no type of confirmation
        /// </summary>
        /// <param name="Msg"></param>
        /// <param name="Win"></param>
        /// <returns></returns>
        public static bool AskForConfirmation(string Msg, IWin32Window Win)
        {
            DialogResult result = MessageBox.Show(Win, Msg, Properties.Resources.ConfirmationRequested, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            return (result == DialogResult.Yes) ? true : false;
        }

        /// <summary>
        /// To display a save dialog from any form within this project
        /// </summary>
        /// <param name="defaultext">If empty, "txt" is used.</param>
        /// <param name="filename">Name or Name.ext</param>
        /// <param name="filter">If empty, Textual filter is used.</param>
        /// <param name="title">if empty, "Save File" is used.</param>
        /// <param name="initialdir">If empty, current directory is used.</param>
        /// <returns></returns>
        public static DialogResult ShowStaticSaveDialog(IWin32Window Win, 
            string defaultext, string filename,
            string filter, string title, string initialdir)
        {
            if(string.IsNullOrEmpty(defaultext))
                AllForms.m_dlgSave.DefaultExt = "txt";
            else
                AllForms.m_dlgSave.DefaultExt = defaultext;

            if(string.IsNullOrEmpty(filename))
                AllForms.m_dlgSave.FileName = "FileName";
            else
                AllForms.m_dlgSave.FileName = filename;

            if(string.IsNullOrEmpty(filter))
                AllForms.m_dlgSave.Filter = DLG_TEXTFILES_FILTER;
            else
                AllForms.m_dlgSave.Filter = filter;

            if(string.IsNullOrEmpty(title))
                AllForms.m_dlgSave.Title = "Save File";
            else
                AllForms.m_dlgSave.Title = title;

            if(string.IsNullOrEmpty(initialdir))
                AllForms.m_dlgSave.InitialDirectory = Environment.CurrentDirectory;
            else
                AllForms.m_dlgSave.InitialDirectory = initialdir;

            return AllForms.m_dlgSave.ShowDialog(Win);
        }

        public static DialogResult ShowStaticSaveDialogForText(IWin32Window Win)
        {
            return ShowStaticSaveDialog(Win, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty);
        }

        public static DialogResult ShowStaticSaveDialogForImage(IWin32Window Win)
        {
            return ShowStaticSaveDialog(Win, "bmp", "ImageFileName" , DLG_IMAGES_FILTER, "Save Image", string.Empty);
        }

        public static DialogResult ShowStaticSaveDialogForXML(IWin32Window Win)
        {
            return ShowStaticSaveDialog(Win, "xml", "XMLFileName" , DLG_XMLS_FILTER, "Save XML", string.Empty);
        }

        public static DialogResult ShowStaticOpenDialog(IWin32Window Win,
            string filter, string title, string initialdir, bool showreadonly)
        {
            AllForms.m_dlgOpen.Filter = filter;
            
            if (string.IsNullOrEmpty(title))
                AllForms.m_dlgOpen.Title = "Open File";
            else
                AllForms.m_dlgOpen.Title = title;

            if (string.IsNullOrEmpty(initialdir))
                AllForms.m_dlgOpen.InitialDirectory = Environment.CurrentDirectory;
            else
                AllForms.m_dlgOpen.InitialDirectory = initialdir;
            
            AllForms.m_dlgOpen.ShowReadOnly = showreadonly;
            
            return AllForms.m_dlgOpen.ShowDialog(Win);
        }

        public static void LoadWebColors(ComboBox combo)
        {
            Array knownColors = Enum.GetValues(typeof(System.Drawing.KnownColor));
            //First add an empty color
            combo.Items.Add(Color.Empty);
            //Then the rest
            foreach (KnownColor k in knownColors)
            {
                Color c = Color.FromKnownColor(k);

                if (!c.IsSystemColor && (c.A > 0))
                {
                    combo.Items.Add(c);
                }
            }
            //Select default
            combo.SelectedIndex = 0;
        }

    }

    //Simple text for InvokeScript
    //if (tsBtn.Tag == null) //First click load the script into browser
    //{
    //    tsBtn.Tag = 1;
    //    string html = "<HTML><HEAD><SCRIPT language=\"Jscript\">function InvokeMethod (str){ myDiv.innerHTML = \"<font size=8>\" + str + \"</font>\";}</Script></Head><Body><H2>Call from App via InvokeScript</h2><div id=\"myDiv\" style=\"font-size: 100%; vertical-align: middle; width: 100%; direction: ltr; font-family: Fantasy, Sans-Serif, Serif; height: 200px; text-align: center\"></div></Body></HTML>";
    //    m_CurWB.LoadHtmlIntoBrowser(html, "http://www.dummy.com");
    //}
    //else //Second click invoke script
    //{
    //    tsBtn.Tag = null;
    //    m_CurWB.InvokeScript("InvokeMethod", new object[] { "Nice !" });
    //}

    #endregion


}