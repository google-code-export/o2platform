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
    public class WatiNCSharp : WatinScript
    {
        private bool DeclaredAlertHandler = false;
        private bool DeclaredConfirmHandler = false;
        private bool DeclaredLogonHandler = false;

        public WatiNCSharp()
        {
            this.LanguageName = "C#";
            this.settings.CodeLanguage = AppSettings.CodeLanguages.CSharp;
        }

        public override void AddGoto(string IEName, string URL)
        {
            if (!URL.StartsWith("http://", StringComparison.OrdinalIgnoreCase))
            {
                URL = URL.Insert(0, "http://");
            }
            AddScriptLine(IEName + ".GoTo(\"" + URL+ "\");");
        }

        public override void AddBack()
        {
            AddScriptLine(settings.BaseIEName + ".Back();");
        }

        public override void AddForward()
        {
            AddScriptLine(settings.BaseIEName + ".Forward();");
        }

        public override void AddRefresh()
        {
            AddScriptLine(settings.BaseIEName + ".Refresh();");
        }

        public override void AddSelectListItem(string IEName, IHTMLElement ActiveElement, bool ByValue)
        {
            string strElement = DetermineFindMethod(IEName, ActiveElement);
            if (ByValue)
            {
                strElement += ".SelectByValue(\"" + ActiveElementAttribute(ActiveElement, "value") + "\");";
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

            string className = ActiveElement.className;

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
                    strElement += ".Checked = false;";
                }
                else
                {
                    strElement += ".Checked = true;";
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
                strElement += ".Click();";
            }

            AddScriptLine(strElement);
        }

        public override void AddAlertHandler(string IEName)
        {
            if (DeclaredAlertHandler)
            {
                AddScriptLine("adhdl = new AlertDialogHandler();");
            }
            else
            {
                AddScriptLine("AlertDialogHandler adhdl = new AlertDialogHandler();");
            }

            AddScriptLine(IEName + ".AddDialogHandler(adhdl);");
            AddScriptLine("adhdl.OKButton.Click();");
        }

        public override void AddConfirmHandler(string IEName, DialogResult DlogResult)
        {
            if (DeclaredConfirmHandler)
            {
                AddScriptLine("cdhdl = new ConfirmDialogHandler();");
            }
            else
            {
                AddScriptLine("ConfirmDialogHandler cdhdl = new ConfirmDialogHandler();");
            }

            AddScriptLine(IEName + ".AddDialogHandler(cdhdl);");

            if (DlogResult == DialogResult.OK)
            {
                AddScriptLine("cdhdl.OKButton.Click();");
            }
            else
            {
                AddScriptLine("cdhdl.CancelButton.Click();");
            }
        }

        public override void AddFileInput(string IEName, IHTMLElement element, string filename)
        {
            string strElement = DetermineFindMethod(IEName, element);
            strElement += ".SetFilename(@\"" + filename + "\");";
            AddScriptLine(strElement);
        }

        public override string AddPopup(string IEName, string URL)
        {
            PopupCounter++;
            AddScriptLine("IE " + IEName + "_" + PopupCounter.ToString() + " = IE.AttachToIE(Find.ByUrl(\"" + URL + "\"));");
            return IEName + "_" + PopupCounter.ToString();
        }

        public override void AddClosePopup(string IEName)
        {
            AddScriptLine(IEName + ".Close();");
        }

        public override void AddLoginDialog(string IEName, string Username, string Password)
        {
            if (DeclaredLogonHandler)
            {
                AddScriptLine("dhdlLogon = new LogonDialogHandler(\"" + Username + "\",\"" + Password + "\");");
            }
            else
            {
                AddScriptLine("LogonDialogHandler dhdlLogon = new LogonDialogHandler(\"" + Username + "\",\"" + Password + "\");");
            }

            AddScriptLine(IEName + ".AddDialogHandler(dhdlLogon);");

            // move the GoTo to the end
            ShiftLineToEnd(IEName + ".GoTo(");
        }


        public override void AddAction(string BrowserName, IHTMLElement element, string Action)
        {
            string strElement = DetermineFindMethod(BrowserName, element);
            strElement += "." + Action;
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

            if (WaitTimerActive && sbCode.Length>0)
            {
                sbCode.Append("System.Threading.Thread.Sleep(new TimeSpan(0, 0, " + GetTimer().ToString() + "));" + Environment.NewLine);
                ClearTimer();
            }

            if (sbCode.Length == 0)
            {
                sbCode.Append("IE " + settings.BaseIEName + " = new IE(\"" + MainBrowser.LocationUrl + "\");" + System.Environment.NewLine);
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
                return Properties.Resources.UnknownItem;
            }

            //IE.item(findmethod).action
            string line = BrowserName + ".";
            string tagtype = "";

            // parentwindow can be the frame, so asking for the parent of the parent
            mshtml.IHTMLDocument2 doc = element.document as mshtml.IHTMLDocument2;
            IHTMLWindow2 winorframe = doc.parentWindow as IHTMLWindow2;
            if (winorframe.parent != null)
            {
                line += GetFramePath(winorframe, ".");
            }

            line += TagToObjectString(element);

            line += "(";

            string strValue = "";
            string strFindMethod = GetFindMethod(element, ref strValue);

            if (tagtype == "radio")
            {
                string radioValue = ActiveElementAttribute(element, "value").ToLower();
                line += "Find.ByName(\"" + strValue + "\") && Find.ByValue(\"" + radioValue;
            }
            else
            {
                switch (strFindMethod)
                {
                    case "id": line += "Find.ById(\"" + strValue; break;
                    case "name": line += "Find.ByName(\"" + strValue; break;
                    case "href": line += "Find.ByUrl(\"" + strValue; break;
                    case "url": line += "Find.ByUrl(\"" + strValue; break;
                    case "src": line += "Find.BySrc(\"" + strValue; break;
                    case "value": line += "Find.ByValue(\"" + strValue; break;
                    case "style": line += "Find.ByStyle(\"" + strValue; break;
                    case "innerText": line += "Find.ByText(\"" + strValue; break;
                    default:

                        strValue = strValue.Replace("\r\n", "\\r\\n");
                        line += "Find.ByCustom(\"" + strFindMethod + "\",\"" + @strValue; break;
                }
            }

            line += "\"))";


            return line;
        }

        public override string DetermineFindMethod(string BrowserName, IHTMLElement element, CheckedListBox.CheckedItemCollection CheckedItems)
        {
            if (element == null)
            {
                return Properties.Resources.UnknownItem;
            }

            //IE.item(findmethod).action
            string line = BrowserName + ".";
            string tagtype = "";

            // parentwindow can be the frame, so asking for the parent of the parent
            mshtml.IHTMLDocument2 doc = element.document as mshtml.IHTMLDocument2;
            IHTMLWindow2 winorframe = doc.parentWindow as IHTMLWindow2;
            if (winorframe.parent != null)
            {
                line += GetFramePath(winorframe, ".");
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
                    case "id": varprefix += " && Find.ById(\"" + arrItem[1] + "\")"; break;
                    case "name": varprefix += " && Find.ByName(\"" + arrItem[1] + "\")"; break;
                    case "href": varprefix += " && Find.ByUrl(\"" + arrItem[1] + "\")"; break;
                    case "url": varprefix += " && Find.ByUrl(\"" + arrItem[1] + "\")"; break;
                    case "src": varprefix += " && Find.BySrc(\"" + arrItem[1] + "\")"; break;
                    case "value": varprefix += " && Find.ByValue(\"" + arrItem[1] + "\")"; break;
                    case "style": varprefix += " && Find.ByStyle(\"" + arrItem[1] + "\")"; break;
                    case "innerText": varprefix += " && Find.ByText(\"" + arrItem[1] + "\")"; break;
                    default: varprefix += " && Find.ByCustom(\"" + arrItem[0] + "\",\"" + arrItem[1] + "\")"; break;
                }
            }
            if (varprefix.Length > 4)
            {
                varprefix = varprefix.Remove(0, 4);
            }
            line += varprefix + ")";


            return line;
        }
    }
}
