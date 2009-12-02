using System;
using System.Collections.Generic;
using System.Xml;

namespace O2.RnD.SpringMVCAnalyzer.classes
{
    public class SpringController
    {
        public Dictionary<String, ActionMappingEntry> dEntries = new Dictionary<String, ActionMappingEntry>();
        public Dictionary<String, Property> dProperties = new Dictionary<String, Property>();
        public String sClass;
        public String sId;
        public String sInnerXml = "";
        public String sParent;

        public SpringController(String sId, XmlNode xnData)
        {
            this.sId = sId;
            if (xnData.Attributes["class"] != null)
                sClass = xnData.Attributes["class"].Value;
            if (xnData.Attributes["parent"] != null)
                sParent = xnData.Attributes["parent"].Value;
            sInnerXml = xnData.InnerXml;

            dProperties = Property.getProperties(xnData);

            if (dProperties.ContainsKey("actionMappings"))
                dEntries = ActionMappingEntry.getEntries(dProperties["actionMappings"]);
        }

        public void ShowDetails()
        {
            DI.log.info("  Bean:  {0}", sId);
            if (sClass != null)
                DI.log.debug("  Class:  {0}", sClass);
            if (sParent != null)
                DI.log.debug("  Parent:  {0}", sParent);
            foreach (String sName in dProperties.Keys)
                DI.log.debug("  Property:  {0}", dProperties[sName]);
            foreach (String sName in dEntries.Keys)
            {
                DI.log.debug("  Entry:  {0}", dEntries[sName]);
                foreach (String sPropertyName in dEntries[sName].dProperties.Keys)
                    DI.log.debug("    {0} = {1}", sPropertyName, dEntries[sName].dProperties[sPropertyName].sValue);
            }
        }

        /*
		
        //dProperties = mapObjectToXmlElemmentAndAttributes(xnData,"property", "name","ref");
		
        //dEntries = mapObjectToXmlElemmentAndAttributes(xnData,"entry", "key","value-ref");
            //dEntries = mapObjectToXmlElemmentAndAttributeAndInnerText(xnData,"entry", "key");			
		
        public static Dictionary<String,String> mapObjectToXmlElemmentAndAttributeAndInnerText(XmlNode xnData, String sElementName, String sAttributeA)
        {
        Dictionary<String,String> dProperties = new Dictionary<String,String>();		
        XmlElement xeUrlMapping = (XmlElement)xnData;
        foreach(XmlNode xnXmlNode in xeUrlMapping.GetElementsByTagName(sElementName))		
            if (xnXmlNode.Attributes[sAttributeA] != null && xnXmlNode.InnerText != null)			
                dProperties.Add(xnXmlNode.Attributes[sAttributeA].Value, xnXmlNode.InnerText);	
        return dProperties;
        }
		
        public static Dictionary<String,String> mapObjectToXmlElemmentAndAttributes(XmlNode xnData, String sElementName, String sAttributeA, String sAttributeB)
        {
            Dictionary<String,String> dProperties = new Dictionary<String,String>();		
            XmlElement xeUrlMapping = (XmlElement)xnData;
            foreach(XmlNode xnXmlNode in xeUrlMapping.GetElementsByTagName(sElementName))		
                if (xnXmlNode.Attributes[sAttributeA] != null && xnXmlNode.Attributes[sAttributeB] != null)			
                    dProperties.Add(xnXmlNode.Attributes[sAttributeA].Value, xnXmlNode.Attributes[sAttributeB].Value);	
            return dProperties;
        }
        */
    }
}