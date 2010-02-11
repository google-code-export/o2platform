// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows.Forms;

using mshtml;
using O2.External.IE.Interfaces;
using O2.Kernel;
using O2.Kernel.CodeUtils;

namespace O2.External.IE.WebObjects
{
    // add to string extention Methods
    

    public class IE_Img : IO2HtmlImg
    {
        public string OuterHtml { get; set; }
        public IE_Img(HTMLImgClass image)
        {
            OuterHtml = image.outerHTML;
        }
    }         

    public class IE_Anchor : IO2HtmlAnchor
    {
        public string OuterHtml { get; set; }
        public IE_Anchor(HTMLAnchorElementClass anchor)
        {
            OuterHtml = anchor.outerHTML;
        }
    }

    public class IE_Script : IO2HtmlScript
    {
        public string CharSet { get; set; }
        public string Event { get; set; }
        public bool Defer { get; set; }
        public string HtmlFor { get; set; }
        public string Src { get; set; }
        public string Text { get; set; }
        public string OuterHtml { get; set; }

        public IE_Script(HTMLScriptElementClass script)
        {
            OuterHtml = script.outerHTML;
            CharSet = ((IHTMLScriptElement2)script).charset;
            Event = ((IHTMLScriptElement) script).@event;
            Defer = ((IHTMLScriptElement)script).defer;
            HtmlFor = ((IHTMLScriptElement)script).htmlFor;
            Src = ((IHTMLScriptElement)script).src;
            Text = ((IHTMLScriptElement)script).text;            
            //Charset = script.charset;            
        }
    }
    public class IE_Form : IO2HtmlForm
    {
    	public string OuterHtml { get; set; }
        public string Action { get; set; }
        public string Dir { get; set; }
        public string Encoding { get; set; }
        public string Id { get; set; }
        public int Length { get; set; }
        public string Method { get; set; }
        public string Name { get; set; }
        public string Target { get; set; }
        public string AcceptCharset { get; set; }
        public string OnSubmit { get; set; }
        public List<IO2HtmlFormField> FormFields { get; set; }

        // use this to get all details from elements
        //public List<HTMLInputElementClass> Elements { get; set; }
        //public List<HTMLSelectElementClass> Elements { get; set; }

        public IE_Form()
        {
            FormFields = new List<IO2HtmlFormField>();
        }
        public IE_Form(HTMLFormElementClass form) : this()
        {
            loadData(form);
            /*Elements = new List<HTMLInputElementClass>();
            foreach (var element in ((IHTMLFormElement)form))
            {                
                Elements.Add((HTMLInputElementClass) element);             
            }
             */
            //PublicDI.log.debug(" --- there are {0} elements loaded", Elements.Count);        
        }
        private void loadData(HTMLFormElementClass form)
        {
            Action = ((IHTMLFormElement)form).action;
            Dir = ((IHTMLFormElement)form).dir;
            Encoding = ((IHTMLFormElement)form).encoding;
            Id = ((IHTMLElement)form).id;
            Length = ((IHTMLFormElement)form).length;
            Method = ((IHTMLFormElement)form).method;
            Name = ((IHTMLFormElement)form).name;
            Target = ((IHTMLFormElement)form).target;
            AcceptCharset = ((IHTMLFormElement2)form).acceptCharset;
            OuterHtml = form.outerHTML;
            if (((IHTMLFormElement)form).onsubmit != null)
                OnSubmit = ((IHTMLFormElement)form).onsubmit.ToString();

            foreach (var element in ((IHTMLFormElement)form))
            {
                switch (element.type().Name)
                {
                    case "HTMLInputElementClass":
                    case "HTMLTextAreaElementClass":
                    case "HTMLSelectElementClass":
                        FormFields.Add(this.formField(element));                        
                        break;                                                    
                    default:
                        PublicDI.log.error("In IE_Form. loadData, unhandled Form type :{0}", element.type().Name);
                        break;
                }
                //PublicDI.log.debug(element.type().Name);
            }
            
        }
        public override string ToString()
        {
            if (Name.valid())
                return Name;
            return Id;
        }
    }

    public class IE_Form_Field : IO2HtmlFormField
    {
        public IO2HtmlForm Form { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Value { get; set; }
        public bool Enabled { get; set; }        
    }        

    public class IE_Link : IO2HtmlLink
    {
        public string Href { get; set; }
        public string InnerText { get; set; }
        public string InnerHtml { get; set; }        
        public string OuterHtml { get; set; }
        public string Target { get; set; }


        //public HtmlLinkIE(HTMLLinkElementClass linkElement)
        public IE_Link(HTMLAnchorElementClass linkElement)         
        {
            Href = linkElement.href;
            OuterHtml = linkElement.outerHTML;
            InnerHtml = linkElement.innerHTML;
            InnerText = linkElement.innerText;
            Target = linkElement.target;
        }    
    }
}