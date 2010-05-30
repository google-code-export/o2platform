using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using IfacesEnumsStructsClasses;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml;
using System.Drawing;
using System.Runtime.InteropServices;
using Microsoft.Win32;
using System.CodeDom;
using System.CodeDom.Compiler;

namespace DemoApp
{
    public class WatiNPerl : WatinScript
    {
        public WatiNPerl()
        {
            this.LanguageName = "Perl";
            this.settings.CodeLanguage = AppSettings.CodeLanguages.Perl;
        }

        public override void AddGoto(string IEName, string URL)
        {
            if (!URL.StartsWith("http://", StringComparison.OrdinalIgnoreCase))
            {
                URL = URL.Insert(0, "http://");
            }
            AddScriptLine("$" + IEName + "->GoTo(\"" + URL + "\");");
        }

        public override void AddBack()
        {
            AddScriptLine("$" + settings.BaseIEName + "->Back();");
        }

        public override void AddForward()
        {
            AddScriptLine("$" + settings.BaseIEName + "->Forward();");
        }

        public override void AddRefresh()
        {
            AddScriptLine("$" + settings.BaseIEName + "->Refresh();");
        }

        public override void AddSelectListItem(string IEName, IHTMLElement ActiveElement, bool ByValue)
        {
            string strElement = DetermineFindMethod(IEName, ActiveElement);
            if (ByValue)
            {
                strElement += "->SelectByValue(\"" + ActiveElementAttribute(ActiveElement, "value") + "\");";
            }
            else
            {
                mshtml.IHTMLSelectElement sel = ActiveElement as mshtml.IHTMLSelectElement;
                for (int i = 0; i < sel.length; i++)
                {
                    mshtml.IHTMLOptionElement op = sel.item(i, i) as mshtml.IHTMLOptionElement;
                    if (op.selected)
                    {
                        strElement += "->SelectByText(\"" + op.text + "\");";
                        break;
                    }
                }
            }

            AddScriptLine(strElement);
        }

        public override void AddTyping(string IEName, IHTMLElement ActiveElement)
        {
            if (sbKeys.Length == 0) return;
            if (ActiveElement.tagName.ToLower() == "td") return;
            if (ActiveElement.tagName.ToLower() == "table") return;

            AddAction(IEName, ActiveElement, "TypeText(\"" + sbKeys.ToString() + "\");");
            sbKeys.Length = 0;
        }

        public override void AddClick(string IEName, IHTMLElement ActiveElement)
        {
            // only certain activeelements get recorded...
            if (ActiveElement.tagName.ToLower() == "td") return;
            if (ActiveElement.tagName.ToLower() == "table") return;
            if (ActiveElement.tagName.ToLower() == "div") return;

            string strElement = DetermineFindMethod(IEName, ActiveElement);
            if (strElement == "")
            {
                return;
            }

            string tagtype = ActiveElementAttribute(ActiveElement, "type").ToLower();
            if (tagtype == "file")
            {
                // start a timer checking for the open dialog
                WatchFileUploadBox(IEName, ActiveElement);
                return;
            }

            if (ActiveElement.tagName.ToLower() == "input" && (tagtype == "radio" || tagtype == "checkbox"))
            {
                if (ActiveElement.outerHTML.ToLower().Contains("checked"))
                {
                    strElement += "->Checked = false;";
                }
                else
                {
                    strElement += "->Checked = true;";
                }
            }
            else
            {
                // Freddy Guime:
                // Clicking on Inputs is redundant when it comes to text inputs

                if (strElement.ToLower().Contains("textfield"))
                {
                    // no strElement is done for clicks
                    return;
                }
                strElement += "->Click();";
            }

            AddScriptLine(strElement);
        }

        public override void AddAlertHandler(string IEName)
        {
            AddScriptLine("$adhdl = $" + IEName + "->AddHandlerAlertDialog();");
            AddScriptLine("$adhdl->ClickOK();");
        }

        public override void AddConfirmHandler(string IEName, DialogResult DlogResult)
        {
            AddScriptLine("$" + IEName + "->AddHandlerConfirmDialog();");

            if (DlogResult == DialogResult.OK)
            {
                AddScriptLine("$cdhdl->ClickOK();");
            }
            else
            {
                AddScriptLine("$cdhdl->ClickCancel();");
            }
        }

        public override void AddFileInput(string IEName, IHTMLElement element, string filename)
        {
            string strElement = DetermineFindMethod(IEName, element);
            strElement += "->SetFilename('" + filename + "');";
            AddScriptLine(strElement);
        }

        public override string AddPopup(string IEName, string URL)
        {
            PopupCounter++;
            AddScriptLine("$" + IEName + "_" + PopupCounter.ToString() + " = $Interface->AttachByUrl(\"" + URL + "\");");
            return IEName + "_" + PopupCounter.ToString();
        }

        public override void AddClosePopup(string IEName)
        {
            AddScriptLine("$" + IEName + "->Close();");
        }

        public override void AddLoginDialog(string IEName, string Username, string Password)
        {
            AddScriptLine("$" + IEName + "->AddHandlerLogin(\"" + Username + "\",\"" + Password + "\");");
            // move the GoTo to the end
            ShiftLineToEnd("$" + IEName + "->GoTo(");
        }


        public override void AddAction(string BrowserName, IHTMLElement element, string Action)
        {
            string strElement = DetermineFindMethod(BrowserName, element);
            strElement += "->" + Action;
            AddScriptLine(strElement);
        }

        public override void AddScriptLine(string Line)
        {
            if (!Recording)
            {
                return;
            }

            UnsavedScript = true;
            sbCode.Length = 0;
            sbCode.Append(RetrieveCode());

            if (WaitTimerActive && sbCode.Length > 0)
            {
                sbCode.Append("Sleep(" + GetTimer().ToString() + ");" + Environment.NewLine);
                ClearTimer();
            }

            if (sbCode.Length == 0)
            {
                sbCode.Append("Win32::OLE->Initialize(Win32::OLE::COINIT_APARTMENTTHREADED);"+Environment.NewLine);
                sbCode.Append("$Interface = Win32::OLE->new('WatiN.COMInterface') or die 'Cannot start WatiN COM interface';" + Environment.NewLine);
                sbCode.Append("$" + settings.BaseIEName + " = $Interface->CreateIE(\"" + MainBrowser.LocationUrl + "\");" + System.Environment.NewLine);
            }

            sbCode.Append(Line + System.Environment.NewLine);

            // send to registered target
            if (rtbTarget != null)
            {
                ChangeCode(sbCode.ToString());
            }
        }

        public override string DetermineFindMethod(string BrowserName, IHTMLElement element)
        {
            if (element == null)
            {
                return "# UNKNOWN ELEMENT ";
            }

            //IE.item(findmethod).action
            string line = "$" + BrowserName + "->";
            string tagtype = "";

            // parentwindow can be the frame, so asking for the parent of the parent
            mshtml.IHTMLDocument2 doc = element.document as mshtml.IHTMLDocument2;
            IHTMLWindow2 winorframe = doc.parentWindow as IHTMLWindow2;
            if (winorframe.parent != null)
            {
                line += GetFramePath(winorframe, "->");
            }

            line += TagToObjectString(element);

            line += "(";

            string strValue = "";
            string strFindMethod = GetFindMethod(element, ref strValue);

            if (tagtype == "radio")
            {
                string radioValue = ActiveElementAttribute(element, "value").ToLower();
                line += "FindByName(\"" + strValue + "\") && FindByValue(\"" + radioValue;
            }
            else
            {
                switch (strFindMethod)
                {
                    case "id": line += "$Interface->FindById(\"" + strValue; break;
                    case "name": line += "$Interface->FindByName(\"" + strValue; break;
                    case "href": line += "$Interface->FindByUrl(\"" + strValue; break;
                    case "url": line += "$Interface->FindByUrl(\"" + strValue; break;
                    case "src": line += "$Interface->FindBySrc(\"" + strValue; break;
                    case "value": line += "$Interface->FindByValue(\"" + strValue; break;
                    case "style": line += "$Interface->FindByStyle(\"" + strValue; break;
                    case "innerText": line += "$Interface->FindByText(\"" + strValue; break;
                    default: line += "$Interface->FindByCustom(\"" + strFindMethod + "\",\"" + strValue; break;
                }
            }

            line += "\"))";


            return line;
        }

        public override string DetermineFindMethod(string BrowserName, IHTMLElement element, CheckedListBox.CheckedItemCollection CheckedItems)
        {
            if (element == null)
            {
                return "# UNKNOWN ELEMENT ";
            }

            //IE.item(findmethod).action
            string line = "$" + BrowserName + "->";

            // parentwindow can be the frame, so asking for the parent of the parent
            mshtml.IHTMLDocument2 doc = element.document as mshtml.IHTMLDocument2;
            IHTMLWindow2 winorframe = doc.parentWindow as IHTMLWindow2;
            if (winorframe.parent != null)
            {
                line += GetFramePath(winorframe, "->");
            }

            line += TagToObjectString(element);

            line += "(";

            string strValue = "";
            string strFindMethod = GetFindMethod(element, ref strValue);

            string varprefix = "";
            for (int i = 0; i < CheckedItems.Count; i++)
            {
                string[] arrItem = CheckedItems[i].ToString().Split("=".ToCharArray());
                switch (arrItem[0].ToLower())
                {
                    case "id": varprefix += " && $Interface->FindById(\"" + arrItem[1] + "\")"; break;
                    case "name": varprefix += " && $Interface->FindByName(\"" + arrItem[1] + "\")"; break;
                    case "href": varprefix += " && $Interface->FindByUrl(\"" + arrItem[1] + "\")"; break;
                    case "url": varprefix += " && $Interface->FindByUrl(\"" + arrItem[1] + "\")"; break;
                    case "src": varprefix += " && $Interface->FindBySrc(\"" + arrItem[1] + "\")"; break;
                    case "value": varprefix += " && $Interface->FindByValue(\"" + arrItem[1] + "\")"; break;
                    case "style": varprefix += " && $Interface->FindByStyle(\"" + arrItem[1] + "\")"; break;
                    case "innerText": varprefix += " && $Interface->FindByText(\"" + arrItem[1] + "\")"; break;
                    default: varprefix += " && $Interface->FindByCustom(\"" + arrItem[0] + "\",\"" + arrItem[1] + "\")"; break;
                }
            }
            if (varprefix.Length > 4)
            {
                varprefix = varprefix.Remove(0, 4);
            }
            line += varprefix + ")";


            return line;
        }

        public override string CompileScript(string ScriptCode, Template TemplateObj, bool RunScript)
        {
            if (RecordedTests.Count == 0)
            {
                return Properties.Resources.NoTestsToRun;
            }

            if (!Template.AllFilesExistInList(TemplateObj.IncludedFiles))
            {
                frmLocateResource frm = new frmLocateResource();
                frm.ShowResourceList(TemplateObj, FunctionAssemblies, true);

                // make sure all items can be found
                if (!Template.AllFilesExistInList(TemplateObj.IncludedFiles))
                {
                    return Properties.Resources.NecessaryCodeFilesCouldNotBeFound;
                }
            }

            if (!File.Exists(TemplateObj.StartupApplication))
            {
                frmLocateResource frm = new frmLocateResource();
                if (TemplateObj.StartupApplication.Trim() == "")
                {
                    TemplateObj.StartupApplication = @"c:\Perl\bin\perl.exe";
                }
                frm.ShowResourceList(TemplateObj, FunctionAssemblies, true);
            }

            if (!settings.CompilePath.EndsWith(@"\"))
            {
                settings.CompilePath = Path.GetDirectoryName(settings.CompilePath) + "\\";
            }

            if (!Directory.Exists(settings.CompilePath))
            {
                try
                {
                    Directory.CreateDirectory(settings.CompilePath);
                }
                catch (Exception ex)
                {
                    return string.Format(Properties.Resources.CompilePathCouldNotBeCreated, settings.CompilePath);
                }
            }

            // just save it to the compilation directory
            string Filename = System.IO.Path.ChangeExtension(ExecutableFilename, ".pl");
            SaveScript(Filename, TemplateObj);
            string runoutput = RunScriptOutput(TemplateObj.StartupApplication.Trim(), Filename.Trim());
            return runoutput;
        }
    }
}
