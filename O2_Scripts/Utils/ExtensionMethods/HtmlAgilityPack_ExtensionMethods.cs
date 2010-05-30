using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using O2.Kernel.ExtensionMethods;
using O2.DotNetWrappers.ExtensionMethods;
using O2.External.SharpDevelop.Ascx;
using O2.External.SharpDevelop.ExtensionMethods;
using System.IO;
using System.Xml;


namespace O2.XRules.Database.ExtensionMethods
{
    public static class HtmlAgilityPack_ExtensionMethods
    {
        #region HtmlAgilityPack.HtmlDocument

        public static HtmlAgilityPack.HtmlDocument htmlDocument(this string htmlCode)
        {
            var htmlDocument = new HtmlAgilityPack.HtmlDocument();
            htmlDocument.LoadHtml(htmlCode);
            return htmlDocument;
        }

        public static string html(this HtmlAgilityPack.HtmlDocument htmlDocument)
        {
            return htmlDocument.DocumentNode.OuterHtml;
        }


        public static List<HtmlAgilityPack.HtmlNode> select(this HtmlAgilityPack.HtmlDocument htmlDocument, string query)
        {
            return htmlDocument.DocumentNode.SelectNodes(query).toList<HtmlAgilityPack.HtmlNode>();
        }

        public static List<HtmlAgilityPack.HtmlNode> links(this HtmlAgilityPack.HtmlDocument htmlDocument)
        {
            return htmlDocument.select("//a");
        }

        #endregion
        
        #region HtmlAgilityPack.HtmlNode

        public static List<string> html(this List<HtmlAgilityPack.HtmlNode> htmlNodes)
        {
            return htmlNodes.outerHtml();
        }

        public static List<string> outerHtml(this List<HtmlAgilityPack.HtmlNode> htmlNodes)
        {
            var outerHtml = new List<string>();
            foreach (var htmlNode in htmlNodes)
                outerHtml.add(htmlNode.outerHtml());
            return outerHtml;
        }

        public static string html(this HtmlAgilityPack.HtmlNode htmlNode)
        {
            return htmlNode.outerHtml();
        }

        public static string outerHtml(this HtmlAgilityPack.HtmlNode htmlNode)
        {
            return htmlNode.OuterHtml;
        }

        public static string innerHtml(this HtmlAgilityPack.HtmlNode htmlNode)
        {
            return htmlNode.InnerHtml;
        }

        public static List<string> innerHtml(this List<HtmlAgilityPack.HtmlNode> htmlNodes)
        {
            var outerHtml = new List<string>();
            foreach (var htmlNode in htmlNodes)
                outerHtml.add(htmlNode.innerHtml());
            return outerHtml;
        }

        public static string value(this HtmlAgilityPack.HtmlNode htmlNode)
        {
            return htmlNode.innerHtml();
        }

        public static List<string> values(this List<HtmlAgilityPack.HtmlNode> htmlNodes)
        {
            return htmlNodes.innerHtml();
        }

        public static List<HtmlAgilityPack.HtmlAttribute> attributes(this List<HtmlAgilityPack.HtmlNode> htmlNodes)
        {
            return htmlNodes.attributes("");
        }

        public static List<HtmlAgilityPack.HtmlAttribute> attributes(this List<HtmlAgilityPack.HtmlNode> htmlNodes, string attributeName)
        {
            var allAttributes = new List<HtmlAgilityPack.HtmlAttribute>();
            foreach (var htmlNode in htmlNodes)
                allAttributes.add(htmlNode.attributes(attributeName));
            return allAttributes;
        }

        public static List<HtmlAgilityPack.HtmlAttribute> attributes(this HtmlAgilityPack.HtmlNode htmlNode)
        {
            return htmlNode.attributes("");
        }
        public static List<HtmlAgilityPack.HtmlAttribute> attributes(this HtmlAgilityPack.HtmlNode htmlNode, string attributeName)
        {
            var attributes = new List<HtmlAgilityPack.HtmlAttribute>();
            foreach (var htmlAttribute in htmlNode.Attributes)
                if (attributeName.valid().isFalse() || htmlAttribute.Name == attributeName)
                    attributes.add(htmlAttribute);
            return attributes;
        }

        #endregion

        #region  HtmlAgilityPack.HtmlAttribute

        public static List<string> names(this List<HtmlAgilityPack.HtmlAttribute> htmlAttributes)
        {
            var names = new List<string>();
            foreach (var htmlAttribute in htmlAttributes)
                if (names.Contains(htmlAttribute.Name).isFalse())
                    names.add(htmlAttribute.Name);
            return names;
        }

        public static List<string> values(this List<HtmlAgilityPack.HtmlAttribute> htmlAttributes)
        {
            var values = new List<string>();
            foreach (var htmlAttribute in htmlAttributes)
                values.add(htmlAttribute.Value);
            return values;
        }

        #endregion

        #region TreeView mappings

        public static TreeView add_Node(this TreeView treeView, HtmlAgilityPack.HtmlDocument htmlDocument)
        {
            return treeView.add_Node(htmlDocument.DocumentNode);
        }

        public static TreeView add_Node(this TreeView treeView, HtmlAgilityPack.HtmlNode htmlNode)
        {
            treeView.rootNode().add_Node(htmlNode);
            return treeView;
        }

        public static TreeNode add_Node(this TreeNode treeNode, HtmlAgilityPack.HtmlNode htmlNode)
        {
            return treeNode.add_Node(htmlNode.Name, htmlNode, true);
        }

        public static TreeView add_Nodes(this TreeView treeView, List<HtmlAgilityPack.HtmlNode> htmlNodes)
        {
            treeView.rootNode().add_Nodes(htmlNodes);
            return treeView;
        }

        public static TreeNode add_Nodes(this TreeNode treeNode, List<HtmlAgilityPack.HtmlNode> htmlNodes)
        {
            foreach (var htmlNode in htmlNodes)
                treeNode.add_Node(htmlNode);
            return treeNode;
        }

        #endregion        

        #region ascx_SourceCodeViewer mappings

        public static ascx_SourceCodeViewer showHtmlNodeLocation(this ascx_SourceCodeViewer codeViewer, HtmlAgilityPack.HtmlNode htmlNode)
        {
            codeViewer.editor().showHtmlNodeLocation(htmlNode);
            return codeViewer;
        }

        public static ascx_SourceCodeEditor showHtmlNodeLocation(this ascx_SourceCodeEditor codeEditor, HtmlAgilityPack.HtmlNode htmlNode)
        {

            var startLine = htmlNode.Line;
            var startColumn = htmlNode.LinePosition;

            var endLine = startLine;
            var endColumn = startColumn;

            if (htmlNode.NextSibling != null)
            {
                endLine = htmlNode.NextSibling.Line;
                endColumn = htmlNode.NextSibling.LinePosition;
            }
            else
                endColumn += htmlNode.html().size();
            "selecting CodeEditor location: {0}:{1} -> {2}:{3}".info(startLine, startColumn, endLine, endColumn);
            codeEditor.clearMarkers();
            codeEditor.selectTextWithColor(startLine, startColumn, endLine, endColumn);
            codeEditor.caret_Line(startLine);
            codeEditor.refresh();

            return codeEditor;
        }

        #endregion

        #region string mappings

        public static string htmlToXml(this string htmlCode)
        {
        	return htmlCode.htmlToXml(true);
        }
        
        public static string htmlToXml(this string htmlCode, bool xmlFormat)
        {
        	try
        	{
        		var stringWriter = new StringWriter();
				var xmlWriter = XmlWriter.Create(stringWriter); 				
				xmlWriter.Flush();
				var htmlDocument = htmlCode.htmlDocument();
				
				htmlDocument.Save(xmlWriter);				
				if (xmlFormat)
					return stringWriter.str().xmlFormat();
				return stringWriter.str();
        	}
        	catch(Exception ex)
        	{
        		ex.log("in string.htmlToXml");
        		return ex.Message;
        	}
        }

        #endregion
    }
}
