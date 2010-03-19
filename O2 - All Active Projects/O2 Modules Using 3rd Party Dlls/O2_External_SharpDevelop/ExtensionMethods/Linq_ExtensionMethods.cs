using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Xml.Schema.Linq;
using System.Xml;
using O2.Kernel.ExtensionMethods;
using System.IO;
using System.Windows.Forms;

namespace O2.DotNetWrappers.ExtensionMethods
{
    public static class Linq_ExtensionMethods
    {

        #region Linq XML

        public static XDocument xDocument(this string xml)
        {
            var xmlToLoad = xml.fileExists() ? xml.fileContents() : xml;
            XmlReaderSettings xmlReaderSettings = new XmlReaderSettings();
            xmlReaderSettings.XmlResolver = null;
            xmlReaderSettings.ProhibitDtd = false;
            using (StringReader stringReader = new StringReader(xmlToLoad))
            using (XmlReader xmlReader = XmlReader.Create(stringReader, xmlReaderSettings))
                return XDocument.Load(xmlReader);

        }

        public static XElement xRoot(this string xml)
        {
            var xDocument = xml.xDocument();
            if (xDocument != null)
                return xDocument.Root;
            return null;
        }

        public static IEnumerable<XElement> allNodes(this XElement xElement)
        {
            return xElement.DescendantsAndSelf();
        }

        public static string name(this XElement xElement)
        {
            return xElement.Name.str();
        }

        public static bool hasDataForChildTreeNodes(this XElement xElement)
        {
            return xElement.Nodes().Any() ||
                   xElement.Attributes().Any() ||
                   (xElement.Nodes().Any() && xElement.Value.valid());
        }

        public static XElement xElement(this XTypedElement xTypedElement)
        {
            return (XElement)xTypedElement.prop("Untyped");
        }

        public static string xElementName(this XTypedElement xTypedElement)
        {
            return xTypedElement.xElement().Name.str();
        }
        #endregion

        #region Controls - TreeView

        public static TreeNode add_Node(this TreeView treeView, XElement xElement)
        {
            return treeView.add_Node(xElement.name(), xElement, xElement.hasDataForChildTreeNodes());
        }

        public static TreeNode add_Node(this TreeNode treeNode, XElement xElement)
        {
            return treeNode.add_Node(xElement.name(), xElement, xElement.hasDataForChildTreeNodes());
        }

        public static TreeNode add_Node(this TreeNode treeNode, XAttribute xAttribute)
        {
            return treeNode.add_Node("{0}: {1}".format(xAttribute.Name, xAttribute.Value));
        }

        public static string getNormalizedValue(this XElement xElement)
        {
            var value = xElement.Value;
            if (value.valid())
                xElement.Nodes().forEach<XElement>(
                    (element) => value = value.replace(element.Value, ""));
            return value.trim();
        }

        public static TreeView autoExpandXElementData(this TreeView treeView)
        {
            //var onBeforeExpand = "onBeforeExpand"
            if (treeView.hasEventHandler("BeforeExpand"))  	// don't add if there is already an onBeforeExpand event already mapped        	        		
                return treeView;
            treeView.beforeExpand<XElement>(
                (xElement) =>
                {
                    treeView.current().clear();
                    xElement.Nodes().forEach<XElement>(
                            (element) => treeView.current().add_Node(element));

                    xElement.Attributes().forEach<XAttribute>(
                            (attribute) => treeView.current().add_Node(attribute));

                    var value = xElement.getNormalizedValue();
                    if (value.valid())
                        treeView.current().add_Node("value: {0}".format(value));
                });
            return treeView;
        }

        public static TreeView xmlShow(this TreeView treeView, string xml)
        {
            return treeView.showXml(xml);
        }

        public static TreeView showXml(this TreeView treeView, object dataToLoad)
        {
            try
            {
                XElement xElement = null;
                if (dataToLoad is string)
                    xElement = ((string)dataToLoad).xRoot();
                else if (dataToLoad is XTypedElement)
                    xElement = ((XTypedElement)dataToLoad).xElement();

                if (xElement != null)
                {
                    treeView.clear();
                    treeView.autoExpandXElementData();
                    treeView.add_Node(xElement);
                    treeView.expand();
                }
            }
            catch (Exception ex)
            {
                ex.log(ex.Message);
            }
            return treeView;
        }        
        

        #endregion
    }
}
