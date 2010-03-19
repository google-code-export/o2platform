using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;
using System.Data;
using O2.Kernel.ExtensionMethods;
using System.Reflection;
using System.Xml.Schema;

namespace O2.DotNetWrappers.ExtensionMethods
{
    public static class Xml_ExtensionMethods
    {
        #region XML load
        public static XmlReader xmlReader(this string xml)
        {
            var xmlToLoad = xml.fileExists() ? xml.fileContents() : xml;
            XmlReaderSettings xmlReaderSettings = new XmlReaderSettings();
            xmlReaderSettings.XmlResolver = null;
            xmlReaderSettings.ProhibitDtd = false;
            var stringReader = new StringReader(xmlToLoad);
            return XmlReader.Create(stringReader, xmlReaderSettings);
        }

        public static XmlDocument xmlDocument(this string xml)
        {
            try
            {
                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.Load(xml.xmlReader());
                return xmlDocument;
            }
            catch (Exception ex)
            {
                ex.log("in xmlDocument");
                return null;
            }
        }

        public static string xmlDocumentElement(this string xml)
        {
            try
            {
                var xmlDocument = new XmlDocument();
                xmlDocument.Load(xml.xmlReader());
                return xmlDocument.DocumentElement.Name;
            }
            catch (Exception ex)
            {
                ex.info("in xmlDocumentElement");
                return "";
            }
        }

        #endregion

        #region XML format

        public static string xmlFormat(this string xml)
        {
            return xml.xmlFormat(2, ' ');
        }

        public static string xmlFormat(this string xml, int indentation, char indentChar)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(xml.xmlReader());
            var stringWriter = new StringWriter();
            var xmlWriter = new XmlTextWriter(stringWriter);
            xmlWriter.Formatting = Formatting.Indented;
            xmlWriter.Indentation = indentation;
            xmlWriter.IndentChar = indentChar;
            xmlWriter.field("encoding", new UTF8Encoding()); //DC: is there another to set this			
            doc.Save(xmlWriter);
            return stringWriter.str();
        }

        #endregion

        

    }
}
