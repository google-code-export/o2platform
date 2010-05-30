using System;
using System.IO;
using System.Collections.Specialized;
using System.Collections.Generic;
using System.Text;
using IfacesEnumsStructsClasses;

namespace DemoApp
{
    public class FunctionPage
    {
        public string Filename = "";
        public string Title = "";
        public string PageSource = "";
        public string Languages = "";
        public bool ShowOnAll = false;
        public StringCollection scAssemblies = new StringCollection();
        public StringCollection scNotFoundAssemblies = new StringCollection();
        private NameValueCollection nvAttributes = new NameValueCollection();

        public FunctionPage(string Filename)
        {
            this.Filename = Filename;

            // load the HTML file
            PageSource = File.ReadAllText(Filename);

            // find the handled information
            mshtml.IHTMLDocument2 doc = new mshtml.HTMLDocumentClass();
            doc.write(new object[] { PageSource });
            doc.close();

            foreach (mshtml.IHTMLElement el in (mshtml.IHTMLElementCollection)doc.body.all)
            {
                if (el.tagName.ToLower() == "attributes")
                {
                    // get the handled information
                    string attrname = el.getAttribute("name", 0).ToString();
                    string attrvalue = el.getAttribute("value", 0).ToString();
                    nvAttributes.Add(attrname, attrvalue);

                    if (attrname.ToLower()=="tagname" && attrvalue.ToLower()=="all")
                    {
                        ShowOnAll = true;
                    }
                }
                else if (el.tagName.ToLower() == "assembly")
                {
                    // get the assembly information
                    string path = el.getAttribute("path", 0).ToString();

                    if (File.Exists(path))
                    {
                        scAssemblies.Add(path);
                    }
                    else
                    {
                        scNotFoundAssemblies.Add(path);
                    }
                }
                else if (el.tagName.ToLower().Contains("languages"))
                {
                    Languages = el.getAttribute("value", 0).ToString();
                }
            }

            Title = doc.title;
        }

        public bool IsApplicable(IHTMLElement element, AppSettings.CodeLanguages language)
        {
            if (element == null)
            {
                return false;
            }

            bool result = true;

            // check if this function page applies to this element
            for (int i = 0; i < nvAttributes.Count; i++)
            {
                string name = nvAttributes.Keys[i];
                string[] arrValues = nvAttributes.GetValues(i);
                string value = arrValues.Length == 0 ? "" : arrValues[0];

                if (name == "tagName")
                {
                    if (element.tagName.ToLower() != value.ToLower())
                    {
                        result = false;
                        break;
                    }
                }
                else
                {
                    string strValue = element.getAttribute(name, 0) as string;

                    if (strValue==null && value=="null")
                    {
                        continue;
                    }

                    if (strValue == null)
                    {
                        strValue = "";
                    }

                    if (strValue.ToLower() != value.ToLower())
                    {
                        result = false;
                        break;
                    }
                }
            }

            if (!this.Languages.Contains(language.ToString()))
            {
                result = false;
            }

            return result;
        }

        public void ReplacePath(string OldPath, string NewPath)
        {
            // take out of the not found list
            scNotFoundAssemblies.Remove(OldPath);

            // add to the found list (if found)
            if (File.Exists(NewPath))
            {
                scAssemblies.Add(NewPath);
            }
            else
            {
                scNotFoundAssemblies.Add(NewPath);
            }

            // rewrite the assembly reference list
            PageSource = PageSource.Replace(OldPath, NewPath);

            File.WriteAllText(Filename, PageSource);
        }
    }
}
