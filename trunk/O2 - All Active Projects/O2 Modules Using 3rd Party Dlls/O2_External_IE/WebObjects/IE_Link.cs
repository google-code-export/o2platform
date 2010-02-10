// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows.Forms;

using mshtml;
using O2.Kernel;
using O2.Kernel.CodeUtils;

namespace O2.External.IE.WebObjects
{
    // add to string extention Methods
    public static class stringEx
    {
        public static bool valid(this string _string)
        {
            if (false == string.IsNullOrEmpty(_string))
                if (_string.Trim() != "")
                    return true;
            return false;
        }

    }


    public interface IO2HtmlLink
    {
        string OuterHtml { get; set; }        
    }

    public interface IO2HtmlFormField
    {
        IO2HtmlForm Form { get; set; }
        string Name { get; set; }
        string Type { get; set; }
        string Value { get; set; }
        bool Enabled { get; set; }  
    }
    
    public interface IO2HtmlForm
    {
        string OuterHtml { get; set; }
        string Action { get; set; }
        string Dir { get; set; }
        string Encoding { get; set; }
        int Length { get; set; }
        string Method { get; set; }
        string Name { get; set; }
        string Target { get; set; }
        string AcceptCharset { get; set; }
        string OnSubmit { get; set; }
        List<IO2HtmlFormField> FormFields { get; set; }
    }

    public class IE_Img
    {
        public string OuterHtml { get; set; }
        public IE_Img(HTMLImgClass image)
        {
            OuterHtml = image.outerHTML;
        }
    }         

    public class IE_Anchor
    {
        public string OuterHtml { get; set; }
        public IE_Anchor(HTMLAnchorElementClass anchor)
        {
            OuterHtml = anchor.outerHTML;
        }
    }

    public class IE_Script
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
                        //var asd = element.prop("name");
                        //PublicDI.log.error("is: {0}", (asd == null) ? "NULL" : "    not null  ");
                        /*var name = element.prop("name").ToString();
                        var type = element.prop("type").ToString();
                        var value = element.prop("value").ToString();
                        FormFields.Add(this.add(element.prop("name").ToString(),
                                                element.prop("type").ToString(),
                                                element.prop("value").ToString(),
                                                true));
                                                //bool.Parse(element.field("isDisabled)").ToString())));                         
                         */ 
                        break;
/*                        var inputElement = (HTMLInputElementClass) element;
                        FormFields.Add(this.add(inputElement.name,
                                                inputElement.type,
                                                inputElement.value,
                                                !inputElement.isDisabled));
                        break;
                    case "HTMLTextAreaElementClass":
                        var textAreaElement = (HTMLTextAreaElementClass)element;
                        FormFields.Add(this.add(textAreaElement.name,
                                                textAreaElement.type,
                                                textAreaElement.value,
                                                !textAreaElement.isDisabled));
                        break;

                    case "HTMLSelectElementClass":
                        var selectElement = (HTMLSelectElementClass)element;
                        FormFields.Add(this.add(selectElement.name,
                                                selectElement.type,
                                                selectElement.value,
                                                !selectElement.isDisabled));
                        break;*/
                        
                            
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

    

    public static class IE_ExtensionMethods
    {
        public static IO2HtmlFormField formField(this IO2HtmlForm form, object data)
        {
            object name = data.prop("name");
            object type = data.prop("type");
            object value = data.prop("value");
            object enabled = data.prop("enabled");
            return new IE_Form_Field
                       {
                           Form = form,
                           Name = (name != null) ? form.ToString() : "",
                           Type = (type != null) ? type.ToString() : "",
                           Value = (value != null) ? value.ToString() : "",
                           Enabled = (enabled != null) ? bool.Parse(enabled.ToString()) : false
                       };
        }

        public static IO2HtmlFormField formField(this IO2HtmlForm form, string name, string type, string value, bool enabled)
        {
            return new IE_Form_Field
                       {
                           Form = form,
                           Name = name,
                           Type = type,
                           Value = value,
                           Enabled = enabled
                       };
        }
    }

    public class IE_Link : IO2HtmlLink
    {
        public string Href { get; set; }
        public string OuterHtml { get; set; }


        //public HtmlLinkIE(HTMLLinkElementClass linkElement)
        public IE_Link(HTMLAnchorElementClass linkElement)         
        {
            Href = linkElement.href;
            OuterHtml = linkElement.outerHTML;
        }

        /*
         */
 
        /*private readonly GeckoElement geckoElement;

        public O2Link(GeckoElement _geckoElement)
        {
            geckoElement = _geckoElement;
            DI.log.debug(geckoElement.ToString());
            foreach (PropertyInfo property in GetType().GetProperties())
            {
                object propertyValue = DI.reflection.getProperty(property.Name, this);
                DI.log.debug("    {0} = {1}", property.Name, propertyValue.ToString());
            }
        }


        public String Id
        {
            get { return geckoElement.GetAttribute("id"); }
            set { geckoElement.SetAttribute("id", value); }
        }

        public String Href
        {
            get { return geckoElement.GetAttribute("href"); }
            set { geckoElement.SetAttribute("href", value); }
        }

        public String Text
        {
            get { return geckoElement.InnerHtml; }
            set { geckoElement.InnerHtml = value; }
        }

        public String Target
        {
            get { return geckoElement.GetAttribute("target"); }
            set { geckoElement.SetAttribute("target", value); }
        }

        //public String OuterHtml
        public String TextContent
        {
            get { return geckoElement.TextContent; }
            set { geckoElement.TextContent = value; }
        }

        public override string ToString()
        {
            return Text ?? "";
        }*/
    }
}