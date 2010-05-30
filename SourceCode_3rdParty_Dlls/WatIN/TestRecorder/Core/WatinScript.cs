using System;
using System.Timers;
using System.Collections;
using System.Collections.Specialized;
using System.Collections.Generic;
using System.Text;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml;
using IfacesEnumsStructsClasses;
using System.Drawing;
using System.Runtime.InteropServices;
using Microsoft.Win32;

namespace DemoApp
{
    // settings
    

    public class WatinScript
    {
        public StringBuilder sbCode = new StringBuilder();
        public StringBuilder sbKeys = new StringBuilder();
        private DateTime LastKeyTime = DateTime.MinValue;
        private string FileIEName = "";
        private IHTMLElement FileActiveElement;
        private System.Timers.Timer timerFileDialog = null;
        private bool blnFileDialogFound = false;
        public RichTextBox rtbTarget = null;
        public csExWB.cEXWB MainBrowser = null;
        public ListBox lbTestList = null;
        public bool UnsavedScript = false;
        public int PopupCounter = 0;
        public bool Recording = false;
        public string TestName = "WatiNTest";
        public NameValueCollection RecordedTests = new NameValueCollection();
        public AppSettings settings = null;
        public FunctionManager fcnManager = null;
        public StringCollection FunctionAssemblies = new StringCollection();
        public StringCollection FunctionUsing = new StringCollection();
        public Templates TemplateFiles;
        public DateTime WaitTimer;
        public bool WaitTimerActive = false;
        public string LanguageName = "";

        // generic constructor
        public WatinScript()
        {
            settings = new AppSettings(System.IO.Path.Combine(AppDirectory, "settings.xml"));
            TemplateFiles = new Templates(AppDirectory+"\\Templates\\");
        }

        public void ClearTimer()
        {
            WaitTimer = DateTime.Now;
        }

        public int GetTimer()
        {
            TimeSpan span = DateTime.Now.Subtract(WaitTimer);
            WaitTimer = DateTime.Now;
            return Convert.ToInt32(Math.Round(span.TotalSeconds));
        }

        /// <summary>
        /// Property for the executable filename, useful for when testing using TestDriven
        /// </summary>
        public string ExecutableFilename
        {
            get
            {
                if (settings.CompilePath == null)
                {
                    settings.CompilePath = Path.GetDirectoryName(Application.ExecutablePath)+"\\";
                    if (settings.CompilePath.Contains("TestDriven"))
                    {
                        settings.CompilePath = @"C:\Development\TestRecorder\bin\Debug\";
                    }
                }

                return Path.Combine(settings.CompilePath, TestName + ".exe");
            }
        }

        /// <summary>
        /// Property for the application directory, instead of TestDriven
        /// </summary>
        public string AppDirectory
        {
            get
            {
                string directory = Path.GetDirectoryName(Application.ExecutablePath);
                if (directory.Contains("TestDriven"))
                {
                    directory = @"C:\Development\TestRecorder\bin\Debug\";
                }
                return directory;
            }
        }

        /// <summary>
        /// Retieves the index of the test given a name
        /// </summary>
        /// <param name="NameOfTest">name to search for (case-sensitive)</param>
        /// <returns>index of the test or -1 if not found</returns>
        public int GetTestIndex(string NameOfTest)
        {
            int result = -1;
            for (int i = 0; i < RecordedTests.Count; i++)
            {
                if (RecordedTests.GetKey(i)==NameOfTest)
                {
                    result = i;
                    break;
                }
            }
            return result;
        }

        #region Script Save/Load/Prepare

        /// <summary>
        /// Saves the test script using the "native" format
        /// Only the native format can be loaded
        /// </summary>
        /// <param name="Filename">Filename to save to</param>
        public void SaveScript(string Filename)
        {
            if (System.IO.File.Exists(Filename))
            {
                System.IO.File.Delete(Filename);
            }

            try
            {
                Settings scriptfile = new Settings(Filename);
                StringBuilder sbTestNames = new StringBuilder();
                for (int i = 0; i < RecordedTests.Count; i++)
                {
                    sbTestNames.AppendLine(RecordedTests.GetKey(i));
                    scriptfile.PutSetting(RecordedTests.GetKey(i), RecordedTests[i]);
                }
                string strAssemblies = Template.JoinList(FunctionAssemblies);

                scriptfile.PutSetting("Tests", sbTestNames.ToString());
                scriptfile.PutSetting("CodeLanguage", settings.CodeLanguage.ToString());
                scriptfile.PutSetting("Assemblies", strAssemblies);

                UnsavedScript = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Properties.Resources.SaveErrorNotSaved, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Saves the test script using the template file indicated
        /// </summary>
        /// <param name="Filename">Filename to save to</param>
        /// <param name="TemplateFile">Template to apply</param>
        public void SaveScript(string Filename, Template TemplateFile)
        {
            try
            {
                if (System.IO.File.Exists(Filename))
                {
                    System.IO.File.Delete(Filename);
                }

                string code = TemplateFile.PrepareScript(RecordedTests);
                System.IO.File.WriteAllText(Filename, code);
                UnsavedScript = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Properties.Resources.SaveErrorNotSaved, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Loads code from a file, must be in native format
        /// </summary>
        /// <param name="Filename">Filename to load from</param>
        public void LoadScript(string Filename)
        {
            if (!System.IO.File.Exists(Filename))
            {
                MessageBox.Show(string.Format(Properties.Resources.FileDoesNotExist, Filename), Properties.Resources.FileError, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (UnsavedScript)
            {
                if (MessageBox.Show(Properties.Resources.EraseAndContinueLoading, Properties.Resources.Confirmation,MessageBoxButtons.YesNo)==DialogResult.No)
                {
                    return;
                }
            }

            try
            {
                Settings scriptfile = new Settings(Filename);

                string scriptlang = scriptfile.GetSetting("CodeLanguage","CSharp");
                if (scriptlang != settings.CodeLanguage.ToString())
                {
                    MessageBox.Show(Properties.Resources.LoadedCodeInDifferentLang,Properties.Resources.CodeLanguage,MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                string[] arrTestNames = scriptfile.GetSetting("Tests", "").Split(Environment.NewLine.ToCharArray());

                RecordedTests.Clear();

                for (int i = 0; i < arrTestNames.Length; i++)
                {
                    if (arrTestNames[i].Trim()=="")
                    {
                        continue;
                    }
                    RecordedTests.Add(arrTestNames[i], scriptfile.GetSetting(arrTestNames[i], ""));
                }

                string[] arrAssemblies = scriptfile.GetSetting("Assemblies", "").Split(Environment.NewLine.ToCharArray());
                for (int i = 0; i < arrAssemblies.Length; i++)
                {
                    if (arrAssemblies[i].Trim() != "")
                    {
                        FunctionAssemblies.Add(arrAssemblies[i]);
                    }
                }

                UnsavedScript = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Load Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        #endregion

        /// <summary>
        /// Uses Win32 calls to find whether the foreground window is a file dialog
        /// </summary>
        /// <returns>true if it is a file dialog</returns>
        public bool FileDialogFound()
        {
            IntPtr win = GetForegroundWindow();
            long lstyle = GetWindowStyle(win);

            if (lstyle.ToString("X") == "96CC20C4" || lstyle.ToString("X") == "96CC02C4")
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Sets a timer to watch for a file dialog box
        /// </summary>
        /// <param name="IEName">IE window title to monitor</param>
        /// <param name="ActiveElement">Element to check after dialog is found</param>
        public void WatchFileUploadBox(string IEName, IHTMLElement ActiveElement)
        {
            blnFileDialogFound = false;
            FileActiveElement = ActiveElement;
            FileIEName = IEName;
            timerFileDialog = new System.Timers.Timer(1000);
            timerFileDialog.Elapsed += new ElapsedEventHandler(timerFileDialog_Elapsed);
            timerFileDialog.Enabled = true;
        }

        /// <summary>
        /// Timer event for the file dialog
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void timerFileDialog_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (!blnFileDialogFound)
            {
                if (FileDialogFound())
                {
                    blnFileDialogFound = true;
                }
                else
                {
                    return;
                }
            }

            if (FileDialogFound())
            {
                return;
            }

            timerFileDialog.Enabled = false;
            string filename = ActiveElementAttribute(FileActiveElement, "value");
            if (filename == "")
            {
                return;
            }

            AddFileInput(FileIEName, FileActiveElement, filename);
            blnFileDialogFound = false;
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct RECT
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct WINDOWINFO
        {
            public uint cbSize;
            public RECT rcWindow;
            public RECT rcClient;
            public uint dwStyle;
            public uint dwExStyle;
            public uint dwWindowStatus;
            public uint cxWindowBorders;
            public uint cyWindowBorders;
            public ushort atomWindowType;
            public ushort wCreatorVersion;
        }

        [DllImport("user32.dll", SetLastError = true)]
        internal static extern bool GetWindowInfo(IntPtr hwnd, ref WINDOWINFO pwi);

        [DllImport("user32.dll", SetLastError = true)]
        internal static extern IntPtr GetForegroundWindow();

        internal static Int64 GetWindowStyle(IntPtr hwnd)
        {
            WINDOWINFO info = new WINDOWINFO();
            info.cbSize = (uint)Marshal.SizeOf(info);
            GetWindowInfo(hwnd, ref info);

            return Convert.ToInt64(info.dwStyle);
        }

        /// <summary>
        /// Determines the find method using the settings' find pattern
        /// </summary>
        /// <param name="element">element to search for</param>
        /// <param name="value"></param>
        /// <returns></returns>
        public string GetFindMethod(IHTMLElement element, ref string value)
        {
            string[] arrFindMethod = settings.FindPattern.Split(",".ToCharArray());
            for (int i = 0; i < arrFindMethod.Length; i++)
            {
                string method = arrFindMethod[i].Trim();
                value = ActiveElementAttribute(element, method);
                if (value != "")
                {
                    return method;
                }
            }
            return "";
        }

        delegate void CodeChangeDelegate(string newcode);
        delegate string CodeRetrieveDelegate();

        public void ChangeCode(string newcode)
        {
            if (rtbTarget.InvokeRequired)
            {
                CodeChangeDelegate method = new CodeChangeDelegate(ChangeCode);
                rtbTarget.Invoke(method, new object[] { newcode });
                return;
            }
            rtbTarget.Text = newcode;
        }

        public string RetrieveCode()
        {
            if (rtbTarget.InvokeRequired)
            {
                CodeRetrieveDelegate method = new CodeRetrieveDelegate(RetrieveCode);
                return rtbTarget.Invoke(method).ToString();
            }
            return rtbTarget.Text;
        }

        public void AddKeys(bool Shifted, Keys keycode)
        {
            string strKey = keycode.ToString();
            switch (keycode)
            {
                case Keys.Space: strKey = " "; break;
                case Keys.Enter: strKey = "{enter}"; break;
                case Keys.Tab: strKey = "{tab}"; break;
                case Keys.Up: strKey = "{up}"; break;
                case Keys.Down: strKey = "{down}"; break;
                case Keys.Left: strKey = "{left}"; break;
                case Keys.Right: strKey = "{right}"; break;
                case Keys.Back: strKey = "{back}"; break;
            }

            if (Shifted && Regex.IsMatch(strKey, @"\AD\d\z"))
            {
                strKey = Convert.ToChar(keycode).ToString();
                switch (strKey)
                {
                    case "1": strKey = "!"; break;
                    case "2": strKey = "@"; break;
                    case "3": strKey = "#"; break;
                    case "4": strKey = "$"; break;
                    case "5": strKey = "%"; break;
                    case "6": strKey = "^"; break;
                    case "7": strKey = "&"; break;
                    case "8": strKey = "*"; break;
                    case "9": strKey = "("; break;
                    case "0": strKey = ")"; break;
                }
            }
            else if (!Shifted && Regex.IsMatch(strKey, @"\AD\d\z"))
            {
                strKey = Convert.ToChar(keycode).ToString();
            }
            else if (Regex.IsMatch(strKey, @"\ANumPad\d\z"))
            {
                strKey = strKey.Replace("NumPad", "");
            }
            else if (!Shifted && Regex.IsMatch(strKey, @"\AOem\w+\z"))
            {
                switch (strKey)
                {
                    case "Oemtilde": strKey = "`"; break;
                    case "OemMinus": strKey = "-"; break;
                    case "Oemplus": strKey = "="; break;
                    case "OemOpenBrackets": strKey = "["; break;
                    case "Oem6": strKey = "]"; break;
                    case "Oem1": strKey = ";"; break;
                    case "Oem7": strKey = "'"; break;
                    case "Oemcomma": strKey = ","; break;
                    case "OemPeriod": strKey = "."; break;
                    case "OemQuestion": strKey = "/"; break;
                    case "Oem5": strKey = @"\"; break;
                }
            }
            else if (Shifted && Regex.IsMatch(strKey, @"\AOem\w+\z"))
            {
                switch (strKey)
                {
                    case "Oemtilde": strKey = "~"; break;
                    case "OemMinus": strKey = "_"; break;
                    case "Oemplus": strKey = "+"; break;
                    case "OemOpenBrackets": strKey = "{"; break;
                    case "Oem6": strKey = "}"; break;
                    case "Oem1": strKey = ":"; break;
                    case "Oem7": strKey = "\\\""; break;
                    case "Oemcomma": strKey = "<"; break;
                    case "OemPeriod": strKey = ">"; break;
                    case "OemQuestion": strKey = "?"; break;
                    case "Oem5": strKey = "|"; break;
                }
            }

            if (Shifted)
            {
                sbKeys.Append(strKey);
            }
            else
            {
                sbKeys.Append(strKey.ToLower());
            }
        }

        public string ActiveElementAttribute(IHTMLElement element, string AttributeName)
        {
            if (element == null)
            {
                return "";
            }

            string strValue = element.getAttribute(AttributeName, 0) as string;
            if (strValue == null)
            {
                strValue = "";
            }
            return strValue;
        }


        #region Add Line To Script
        
        public virtual void AddGoto(string IEName, string URL)
        {
            //AddScriptLine(IEName+".GoTo(\"" + URL + "\");");
        }

        public virtual void AddBack()
        {
            //AddScriptLine(settings.BaseIEName+".Back();");
        }

        public virtual void AddForward()
        {
            //AddScriptLine(settings.BaseIEName + ".Forward();");
        }

        public virtual void AddRefresh()
        {
            //AddScriptLine(settings.BaseIEName + ".Refresh();");
        }

        public virtual void AddSelectListItem(string IEName, IHTMLElement ActiveElement, bool ByValue)
        {
            /*
            string strElement = DetermineFindMethod(IEName, ActiveElement);
            if (ByValue)
            {
                strElement += ".SelectByValue(\"" + ActiveElementAttribute(ActiveElement,"value") + "\");";
            }
            else
            {
                mshtml.IHTMLSelectElement sel = ActiveElement as mshtml.IHTMLSelectElement;
                for (int i = 0; i < sel.length; i++)
                {
                    mshtml.IHTMLOptionElement op = sel.item(i, i) as mshtml.IHTMLOptionElement;
                    if (op.selected)
                    {
                        strElement += ".SelectByText(\"" + op.text + "\");";
                        break;
                    }
                }                
            }
            
            AddScriptLine(strElement);
             */
        }

        public virtual void AddTyping(string IEName, IHTMLElement ActiveElement)
        {
            /*
            AddAction(IEName, ActiveElement, "TypeText(\"" + sbKeys.ToString() + "\");");
            sbKeys.Length = 0;
             */
        }

        public virtual void AddClick(string IEName, IHTMLElement ActiveElement)
        {
            /*
            string strElement = DetermineFindMethod(IEName, ActiveElement);
            if (strElement=="")
            {
                return;
            }

            string tagtype = ActiveElementAttribute(ActiveElement, "type").ToLower();
            if (tagtype=="file")
            {
                // start a timer checking for the open dialog
                WatchFileUploadBox(IEName, ActiveElement);
                return;
            }

            if (ActiveElement.tagName.ToLower() == "input" && (tagtype == "radio" || tagtype == "checkbox"))
            {
                if (ActiveElement.outerHTML.ToLower().Contains("checked"))
                {
                    strElement += ".Checked = false;";
                }
                else
                {
                    strElement += ".Checked = true;";
                }
            }
            else
            {
                strElement += ".Click();";
            }
            
            AddScriptLine(strElement);
             */
        }
        
        public virtual void AddAlertHandler(string IEName)
        {
            /*
            if (DeclaredAlertHandler)
            {
                AddScriptLine("adhdl = new AlertDialogHandler();");
            }
            else
            {
                AddScriptLine("AlertDialogHandler adhdl = new AlertDialogHandler();");
            }
            
            AddScriptLine(IEName+".AddDialogHandler(adhdl);");
            AddScriptLine("adhdl.OKButton.Click();");
             */
        }

        public virtual void AddConfirmHandler(string IEName, DialogResult DlogResult)
        {
            /*
            if (DeclaredConfirmHandler)
            {
                AddScriptLine("cdhdl = new ConfirmDialogHandler();");
            }
            else
            {
                AddScriptLine("ConfirmDialogHandler cdhdl = new ConfirmDialogHandler();");
            }
            
            AddScriptLine(IEName+".AddDialogHandler(cdhdl);");

            if (DlogResult==DialogResult.OK)
            {
                AddScriptLine("cdhdl.OKButton.Click();");
            }
            else
            {
                AddScriptLine("cdhdl.CancelButton.Click();");
            }
             */
        }

        public virtual void AddFileInput(string IEName, IHTMLElement element, string filename)
        {
            /*
            string strElement = DetermineFindMethod(IEName, element);
            strElement += ".SetFilename(@\"" + filename + "\");";
            AddScriptLine(strElement);
             */
        }

        public virtual string AddPopup(string IEName, string URL)
        {
            /*
            PopupCounter++;
            AddScriptLine("IE "+IEName+"_"+PopupCounter.ToString()+" = IE.AttachToBrowser(Find.ByUrl(\"" + URL + "\"));");
            return IEName + PopupCounter.ToString();
             */
            return "";
        }

        public virtual void AddClosePopup(string IEName)
        {
            //AddScriptLine(IEName+".Close();");
        }

        public virtual void AddLoginDialog(string IEName, string Username, string Password)
        {
            /*
            if (DeclaredLogonHandler)
            {
                AddScriptLine("dhdlLogon = LogonDialogHandler(\"" + Username + "\",\"" + Password + "\");");
            }
            else
            {
                AddScriptLine("LogonDialogHandler dhdlLogon = LogonDialogHandler(\"" + Username + "\",\"" + Password + "\");");
            }
            
            AddScriptLine(IEName+".AddDialogHandler(dhdlLogon);");
             */
        }

        public virtual void AddAction(string BrowserName, IHTMLElement element, string Action)
        {
            /*
            string strElement = DetermineFindMethod(BrowserName, element);
            strElement += "." + Action;
            AddScriptLine(strElement);
             */
        }

        public virtual void AddScriptLine(string Line)
        {
        }

        public virtual string DetermineFindMethod(string BrowserName, IHTMLElement element)
        {
            return "";
        }

        public virtual string DetermineFindMethod(string BrowserName, IHTMLElement element, CheckedListBox.CheckedItemCollection CheckedItems)
        {
            return "";
        }

        public string TagToObjectString(IHTMLElement element)
        {
            string line = "";
            string tagtype = "";
            if (element.tagName.ToLower() == "input")
            {
                tagtype = ActiveElementAttribute(element, "type").ToLower();
                switch (tagtype)
                {
                    case "button": line += "Button"; break;
                    case "submit": line += "Button"; break;
                    case "reset": line += "Button"; break;
                    case "radio": line += "RadioButton"; break;
                    case "checkbox": line += "CheckBox"; break;
                    case "file": line += "FileUpload"; break;
                    case "image": line += "Button"; break;
                    default: 
                        // Not sure if we need the TextField. It seems redundant since after the click comes the typing.
                        
                        line += "TextField"; 
                        break;
                }
            }
            else
            {
                switch (element.tagName.ToLower())
                {
                    case "select": line += "SelectList"; break;
                    case "span": line += "Span"; break;
                    case "div": line += "Div"; break;
                    case "a": line += "Link"; break;
                    case "img": line += "Image"; break;
                    case "form": line += "Form"; break;
                    case "frame": line += "Frame"; break;
                    case "p": line += "Para"; break;
                    case "table": line += "Table"; break;
                    case "tbody": line += "TableBody"; break;
                    case "tr": line += "TableRow"; break;
                    case "td": line += "TableCell"; break;
                    case "textarea": line += "TextField"; break;
                    case "body": return "";
                    default: return "// Unknown Element: " + element.tagName;
                }
            }

            return line;
        }

        public string GetFramePath(IHTMLWindow2 parentwindow, string separator)
        {
            string namepath = "";
            if (parentwindow.parent.name != null)
            {
                namepath = GetFramePath(parentwindow.parent, separator);
            }
            return namepath + "Frame(\"" + parentwindow.name + "\")"+separator;
        }

        public void ShiftLineToEnd(string strCommand)
        {
            if (rtbTarget == null)
            {
                return;
            }

            for (int i = rtbTarget.Lines.Length-1; i > 0; i--)
            {
                if (rtbTarget.Lines[i].StartsWith(strCommand))
                {
                    string oldline = rtbTarget.Lines[i];
                    sbCode.Length = 0;
                    sbCode.Append(RetrieveCode());
                    string[] arrCode = sbCode.ToString().Split(System.Environment.NewLine.ToCharArray());
                    sbCode.Length = 0;
                    for (int j = 0; j < arrCode.Length; j++)
                    {
                        if (j==i)
                        {
                            continue;
                        }
                        else if (arrCode.Length-1==j && arrCode[j]=="")
                        {
                            continue;
                        }
                        sbCode.AppendLine(arrCode[j]);
                    }
                   
                    sbCode.AppendLine(oldline);
                    ChangeCode(sbCode.ToString());
                    break;
                }
            }
        }
        

#endregion

        #region CompileScript

        public virtual string CompileScript(Template TemplateObj, bool RunScript)
        {
            return CompileScript("", TemplateObj, RunScript);
        }

        private bool AssemblyAlreadyInList(string Filename, StringCollection AssemblyList)
        {
            for (int i = 0; i < AssemblyList.Count; i++)
            {
                string nameonly = Path.GetFileName(AssemblyList[i]).ToLower();
                if (Path.GetFileName(Filename).ToLower()==nameonly)
                {
                    return true;
                }
            }
            return false;
        }

        public virtual string CompileScript(string ScriptCode, Template TemplateObj, bool RunScript)
        {
            if (RecordedTests.Count==0)
            {
                return Properties.Resources.NoTestsToCompile;
            }

            if (!TemplateObj.CanCompile)
            {
                	return Properties.Resources.TargetTemplateIsSetNotToCompile;
            }

            if (!Template.AllFilesExistInList(TemplateObj.ReferencedAssemblies) || !Template.AllFilesExistInList(TemplateObj.IncludedFiles))
            {
                frmLocateResource frm = new frmLocateResource();
                frm.ShowResourceList(TemplateObj, FunctionAssemblies, true);

                // make sure all items can be found
                if (!Template.AllFilesExistInList(TemplateObj.ReferencedAssemblies) || !Template.AllFilesExistInList(TemplateObj.IncludedFiles))
                {
                    return Properties.Resources.NecessaryCodeFilesCouldNotBeFound;
                }
            }

            if (!settings.CompilePath.EndsWith(@"\"))
            {
                settings.CompilePath = Path.GetDirectoryName(settings.CompilePath)+"\\";
            }
                        
            if (!Directory.Exists(settings.CompilePath))
            {
                try
                {
                    Directory.CreateDirectory(settings.CompilePath);
                }
                catch (Exception ex)
                {
                    return string.Format(Properties.Resources.CompilePathCouldNotBeCreated,settings.CompilePath);
                }                
            }

            StringBuilder sbErrors = new StringBuilder();

            CompilerParameters cps = new CompilerParameters();
            cps.OutputAssembly = ExecutableFilename;
            cps.GenerateExecutable = true;
            cps.IncludeDebugInformation = true;

            if (!TemplateObj.CanRun)
            {
                cps.OutputAssembly = System.IO.Path.ChangeExtension(cps.OutputAssembly, ".dll");
                cps.GenerateExecutable = false;
            }

            StringCollection scAssemblies = TemplateObj.GetAssemblyList();
            for (int i = 0; i < scAssemblies.Count; i++)
            {
                cps.ReferencedAssemblies.Add(scAssemblies[i]);
            }

            // add assemblies from function explorer
            for (int i = 0; i < FunctionAssemblies.Count; i++)
            {
                if (!AssemblyAlreadyInList(FunctionAssemblies[i], cps.ReferencedAssemblies))
                {
                    cps.ReferencedAssemblies.Add(FunctionAssemblies[i]);
                }
            }

            string[] sourcefiles = new string[TemplateObj.IncludedFiles.Count + 1];
            for (int i = 0; i < TemplateObj.IncludedFiles.Count; i++)
            {
                if (TemplateObj.IncludedFiles[i].Trim()=="")
                {
                    continue;
                }
                sourcefiles[i] = System.IO.File.ReadAllText(TemplateObj.IncludedFiles[i]);
            }

            if (ScriptCode.Trim() != "")
            {
                NameValueCollection nvTest = new NameValueCollection();
                nvTest.Add("CurrentTest", ScriptCode);
                sourcefiles[sourcefiles.Length - 1] = TemplateObj.PrepareScript(nvTest);
            }
            else
            {
                sourcefiles[sourcefiles.Length - 1] = TemplateObj.PrepareScript(this.RecordedTests);
            }
            
            
            // Compile the source code
            CompilerResults cr = null;
            try
            {
                if (TemplateObj.CodeLanguage==AppSettings.CodeLanguages.CSharp)
                {
                    Microsoft.CSharp.CSharpCodeProvider codeprovider = new Microsoft.CSharp.CSharpCodeProvider();
                    cr = codeprovider.CompileAssemblyFromSource(cps, sourcefiles);
                }
                else if (TemplateObj.CodeLanguage == AppSettings.CodeLanguages.VBNet)
                {
                    Microsoft.VisualBasic.VBCodeProvider codeprovider = new Microsoft.VisualBasic.VBCodeProvider();
                    cr = codeprovider.CompileAssemblyFromSource(cps, sourcefiles);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format(Properties.Resources.CompilerError,ex.Message));
            }
            
            // Check for errors
            if (cr.Errors.Count > 0)
            {
                // Has errors so display them
                foreach (CompilerError ce in cr.Errors)
                {
                    sbErrors.AppendFormat(Properties.Resources.ErrorLineListing+System.Environment.NewLine, ce.ErrorNumber, ce.Line, ce.Column, ce.ErrorText, (ce.IsWarning) ? "Warning" : "Error");
                }
                System.Diagnostics.Debug.WriteLine(sbErrors.ToString());
                return sbErrors.ToString();
            }

            // copy imported assemblies (not in .NET main)
            string NetPath = RuntimeEnvironment.GetRuntimeDirectory();
            for (int i = 0; i < scAssemblies.Count; i++)
            {
                if (scAssemblies[i] == null || scAssemblies[i].Trim() == "")
                {
                    continue;
                }
                if (!File.Exists(scAssemblies[i]))
                {
                    return string.Format(Properties.Resources.CompileSuccessfulButCantFind,scAssemblies[i]);
                }

                if (Path.GetDirectoryName(NetPath) != Path.GetDirectoryName(scAssemblies[i]))
                {
                    System.IO.File.Copy(scAssemblies[i], Path.Combine(settings.CompilePath, Path.GetFileName(scAssemblies[i])), true);
                }
                
            }

            if (RunScript && TemplateObj.CanRun)
            {
                string scriptrun = RunScriptOutput(TemplateObj.StartupApplication, cps.OutputAssembly);
                if (scriptrun != "")
                {
                    return scriptrun;
                }
            }

            return "";
        }

        public virtual string RunScriptOutput(string StartupApplication, string Filename)
        {
            try
            {
                System.Diagnostics.ProcessStartInfo info = null;
                if (StartupApplication.Trim()=="")
                {
                    info = new System.Diagnostics.ProcessStartInfo(Filename);
                }
                else
                {
                    info = new System.Diagnostics.ProcessStartInfo(StartupApplication);
                    info.Arguments = Filename;
                }
                
                if (settings.HideDOSWindow)
                {
                    info.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                }
                System.Diagnostics.Process.Start(info);
            }
            catch (Exception e)
            {
                return e.Message;
            }

            return "";
        }

        #endregion
    }
}
