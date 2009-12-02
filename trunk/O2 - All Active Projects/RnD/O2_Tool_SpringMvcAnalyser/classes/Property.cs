using System;
using System.Collections.Generic;
using System.Xml;

namespace O2.RnD.SpringMVCAnalyzer.classes
{
    public class Property
    {
        public String sName;

        public String sRef;
        public String sValue;
        public XmlNodeList xnlChildNodes;

        public Property(String sName)
        {
            this.sName = sName;
        }

        public static Dictionary<String, Property> getProperties(XmlNode xnXmlDataToProcess)
        {
            var dProperties = new Dictionary<String, Property>();
            //XmlElement xeXmlElement = (XmlElement)xnXmlDataToProcess;
            foreach (XmlNode xnChildNode in xnXmlDataToProcess.ChildNodes)
            {
                if (xnChildNode.Name == "property" && xnChildNode.Attributes["name"] != null)
                {
                    var pProperty = new Property(xnChildNode.Attributes["name"].Value);
                    if (xnChildNode.Attributes["value"] != null)
                        pProperty.sValue = xnChildNode.Attributes["value"].Value;
                    if (xnChildNode.Attributes["ref"] != null)
                        pProperty.sRef = xnChildNode.Attributes["ref"].Value;
                    if (xnChildNode.ChildNodes != null)
                    {
                        pProperty.xnlChildNodes = xnChildNode.ChildNodes;
                        if (pProperty.sValue == null)
                            pProperty.sValue =
                                xnChildNode.InnerText.Replace(Environment.NewLine, "").Replace("\t", "").Trim();
                    }
                    dProperties.Add(pProperty.sName, pProperty);
                }
            }
            return dProperties;
        }

        public override String ToString()
        {
            return sName + "      " +
                   ((sRef == null) ? "" : "ref:" + sRef) + "  " +
                   ((sValue == null) ? "" : "value:" + sValue) + "  "; // + 				
            //((xnlChildNodes == null) ? "" : xnlChildNodes.Count.ToString()) + " child nodes";
        }
    }
}