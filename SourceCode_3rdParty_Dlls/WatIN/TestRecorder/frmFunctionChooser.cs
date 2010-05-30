using System;
using System.Xml;
using System.IO;
using System.Collections.Specialized;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using IfacesEnumsStructsClasses;

namespace DemoApp
{
    public partial class frmFunctionChooser : Form
    {
        string json = "";
        WatinScript wscript = null;
        string browsername = "";
        IHTMLElement currentelement = null;

        public frmFunctionChooser()
        {
            InitializeComponent();
        }

        public void SetWindowProperties(WatinScript watscript, string BrowserName, IHTMLElement element)
        {
            wscript = watscript;
            browsername = BrowserName;
            currentelement = element;
            this.Text = string.Format(Properties.Resources.FunctionExplorer,element.GetType().ToString());

            IHTMLDOMNode node = element as IHTMLDOMNode;
            lbAttributes.Items.Clear();
            lbAttributes.Items.Add(string.Format("tagName={0}",element.tagName), false);

            string strValue = "";
            string strFindMethod = wscript.GetFindMethod(element, ref strValue);

            foreach (IHTMLDOMAttribute attr in node.attributes)
            {
                if (attr.specified)
                {
                    if (strFindMethod.ToLower()==attr.nodeName.ToLower())
                    {
                        lbAttributes.Items.Add(attr.nodeName + "=" + attr.nodeValue, true);
                    }
                    else
                    {
                        lbAttributes.Items.Add(attr.nodeName + "=" + attr.nodeValue, false);
                    }
                }
            }

            lbAttributes.Items.Add("innerHTML="+element.innerHTML, false);
            if (strFindMethod=="innertext")
            {
                lbAttributes.Items.Add("innerText=" + element.innerText, true);
            }
            else
            {
                lbAttributes.Items.Add("innerText=" + element.innerText, false);
            }
            lbAttributes.Items.Add("outerHTML=" + element.outerHTML, false);
            lbAttributes.Items.Add("outerText=" + element.outerText, false);

            json = CreateJSONData(element);
            LoadTabs(element);
        }

        private string CreateJSONData(IHTMLElement element)
        {
            StringBuilder sbJSON = new StringBuilder();

            string strLanguage = System.Enum.GetName(typeof(AppSettings.CodeLanguages), wscript.settings.CodeLanguage);

            sbJSON.Append("{");
            sbJSON.Append("\"CodeLanguage\": \"" + strLanguage + "\",");
            sbJSON.Append("\"tagName\": \"" + element.tagName+"\"");

            IHTMLDOMNode node = element as IHTMLDOMNode;
            foreach (IHTMLDOMAttribute attr in node.attributes)
            {
                if (attr.specified)
                {
                    sbJSON.AppendFormat(",\"{0}\": \"{1}\"", attr.nodeName, System.Uri.EscapeDataString(attr.nodeValue.ToString()));
                }
            }

            if (element.innerText != null)
            {
                sbJSON.Append(",\"innerText\": \"" + System.Uri.EscapeDataString(element.innerText) + "\"");
            }
                           
            sbJSON.Append("}");

            return sbJSON.ToString();
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            PaintTab(tabControl1.SelectedTab);
        }

        private void PaintTab(TabPage tab)
        {
            tab.Controls.Clear();
            tab.Controls.Add(new csExWB.cEXWB());
            csExWB.cEXWB wb = tab.Controls[0] as csExWB.cEXWB;
            FunctionPage page = tab.Tag as FunctionPage;

            wb.Dock = DockStyle.Fill;
            wb.BeforeNavigate2 += new csExWB.BeforeNavigate2EventHandler(this.cEXWB1_BeforeNavigate2);

            string dirpath = "file:///" + wscript.AppDirectory.Replace("\\", "/") + "/Functions/";
            page.PageSource = page.PageSource.Replace("%DIRPATH%/", dirpath);
            wb.LoadHtmlIntoBrowser(page.PageSource + "<div Id=Data style='display:none'>" + json + "</div>", dirpath);
            //wb.LoadHtmlIntoBrowser("page data");

            for (int i = 0; i < page.scNotFoundAssemblies.Count; i++)
            {
                Button nfButton = new Button();
                wb.Controls.Add(nfButton);
                nfButton.Height = 30;
                nfButton.Dock = DockStyle.Top;
                nfButton.TextAlign = ContentAlignment.MiddleLeft;
                nfButton.Text = string.Format("Cannot Find {0}",page.scNotFoundAssemblies[i]);
                nfButton.Tag = page.scNotFoundAssemblies[i];
                nfButton.Click += new EventHandler(nfButton_Click);
            }
        }

        private void LoadTabs(IHTMLElement element)
        {
            // get the list of pages
            List<FunctionPage> pages = wscript.fcnManager.GetApplicableFunctions(element, wscript.settings.CodeLanguage);

            // loop to load each page
            foreach (FunctionPage page in pages)
            {
                // create a new tab
                tabControl1.TabPages.Add(page.Title, page.Title);
                int tabindex = tabControl1.TabPages.IndexOfKey(page.Title);
                TabPage tab = tabControl1.TabPages[tabindex];

                tab.Tag = page;
            }
        }

        void nfButton_Click(object sender, EventArgs e)
        {
            string searchedfile = (sender as Button).Tag.ToString();
            openFileDialog1.Title = string.Format(Properties.Resources.Find,Path.GetFileName(searchedfile));
            openFileDialog1.FileName = Path.GetFileName(searchedfile);
            if (openFileDialog1.ShowDialog()==DialogResult.Cancel)
            {
                return;
            }

            if (Path.GetFileName(openFileDialog1.FileName.ToLower()) == Path.GetFileName(searchedfile.ToLower()))
            {
                FunctionPage page = (sender as Button).Parent.Tag as FunctionPage;
                page.ReplacePath(searchedfile, openFileDialog1.FileName);
                (sender as Button).Visible = false;
            }
        }

        private void cEXWB1_BeforeNavigate2(object sender, csExWB.BeforeNavigate2EventArgs e)
        {
            if (e.postdata==null)
            {
                e.Cancel = true;
                return;
            }

            // get the using statements and code
            byte[] byteData = e.postdata as byte[];
            string strPostData = System.Text.ASCIIEncoding.ASCII.GetString(byteData);
            strPostData = strPostData.Remove(strPostData.Length - 1);
            strPostData = strPostData.Replace("+", " ");

            string[] arrPostData = strPostData.Split("&".ToCharArray());
            NameValueCollection nvcPost = new NameValueCollection();
            for (int i = 0; i < arrPostData.Length; i++)
            {
                string[] pair = arrPostData[i].Split("=".ToCharArray());
                string value = System.Uri.UnescapeDataString(pair[1]);
                nvcPost.Add(pair[0],value);
            }

            /*
            // add using statements
            string[] usings = nvcPost.GetValues("Using");
            if (usings != null)
            {
                for (int i = 0; i < usings.Length; i++)
                {
                    wscript.FunctionUsing.Add(usings[i]);
                }
            }
            */
            
            // find function page to know what assemblies to add
            string[] arrfunctionnames = nvcPost.GetValues("FunctionTitle");
            if (arrfunctionnames != null)
            {
                FunctionPage page = wscript.fcnManager.GetPageFromTitle(arrfunctionnames[0]);
                if (page != null)
                {
                    for (int i = 0; i < page.scAssemblies.Count; i++)
                    {
                        wscript.FunctionAssemblies.Add(page.scAssemblies[i]);
                    }
                }
            }

            // add code, replacing %variable% with the find method
            string varprefix = wscript.DetermineFindMethod(browsername, currentelement, lbAttributes.CheckedItems);
            if (varprefix == "")
            {
                varprefix = wscript.DetermineFindMethod(browsername, currentelement);
                if (varprefix == "")
                {
                    varprefix = Properties.Resources.UnknownItem;
                }
            }

            string[] code = nvcPost.GetValues("Code");
            if (code != null)
            {
                for (int i = 0; i < code.Length; i++)
                {
                    wscript.AddScriptLine(code[i].Replace("%variable%",varprefix));
                }
            }
            
            this.Close();

            e.Cancel = true;
        }

        private void frmFunctionChooser_Shown(object sender, EventArgs e)
        {
            if (tabControl1.TabCount>0)
            {
                PaintTab(tabControl1.TabPages[0]);
            }
        }
    }
}