using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;
using System.Data;
using O2.Kernel.ExtensionMethods;

namespace O2.DotNetWrappers.ExtensionMethods
{
    public static class Xml_ExtensionMethods
    {
        public static string formatXml(this string xmlFileOrText)
        {
            return (xmlFileOrText.isFile()) ? xmlFileOrText.formatXmlFile() : xmlFileOrText.formatXmlText();
        }

        public static string formatXmlFile(this string xmlFile)
        {
            return xmlFile.contents().formatXmlText();
        }

        public static string formatXmlText(this string xmlText)
        {
            return xmlText.formatXmlText(2, ' ');
        }

        public static string formatXmlText(this string xmlText, int indentation, char indentChar)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xmlText);
            var stringWriter = new StringWriter();
            var xmlWriter = new XmlTextWriter(stringWriter);
            xmlWriter.Formatting = Formatting.Indented;
            xmlWriter.Indentation = indentation;
            xmlWriter.IndentChar = indentChar;
            xmlWriter.field("encoding", new UTF8Encoding()); //DC: is there another to set this			
            doc.Save(xmlWriter);
            return stringWriter.str();
        }

        public static string createXSDfromXmlFile(this string xmlFile)
        {
            try
            {
                var dataSet = new DataSet();
                dataSet.ReadXml(xmlFile);
                var stringWriter = new StringWriter();
                XmlTextWriter xmlTextWriter = new XmlTextWriter(stringWriter);
                xmlTextWriter.Formatting = Formatting.Indented;
                xmlTextWriter.field("encoding", new UTF8Encoding());	//DC: is there another to set this
                //xmlTextWriter.WriteStartDocument();
                dataSet.WriteXmlSchema(xmlTextWriter);                
                xmlTextWriter.Close();
                stringWriter.Close();
                return stringWriter.ToString();
            }
            catch (Exception ex)
            {
                ex.log("in createXSDfromXmlFile");
                return "";
            }
        }
    }
}
