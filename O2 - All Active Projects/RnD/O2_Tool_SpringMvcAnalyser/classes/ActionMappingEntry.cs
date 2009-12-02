// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.Collections.Generic;
using System.Xml;

namespace O2.RnD.SpringMVCAnalyzer.classes
{
    public class ActionMappingEntry
    {
        public Dictionary<String, Property> dProperties = new Dictionary<String, Property>();
        public String sClass;
        public String sKey;
        public String sParent;
        public String sValue_Ref;
        //		public List<Property> lProperties = new List<Property>();

        public ActionMappingEntry(String sKey)
        {
            this.sKey = sKey;
        }

        public static Dictionary<String, ActionMappingEntry> getEntries(Property pProperty)
        {
            var dEntries = new Dictionary<String, ActionMappingEntry>();
            if (pProperty.sName == "actionMappings" && pProperty.xnlChildNodes != null)
            {
                foreach (XmlNode xnChildNode_map in pProperty.xnlChildNodes)
                    if (xnChildNode_map.Name == "map" && xnChildNode_map.ChildNodes != null)
                        foreach (XmlNode xnChildNode_entry in xnChildNode_map.ChildNodes)
                            if (xnChildNode_entry.Name == "entry")
                                if (xnChildNode_entry.Attributes["key"] != null)
                                {
                                    var eEntry = new ActionMappingEntry(xnChildNode_entry.Attributes["key"].Value);
                                    if (xnChildNode_entry["bean"] != null)
                                    {
                                        if (xnChildNode_entry["bean"].Attributes["class"] != null)
                                            eEntry.sClass = xnChildNode_entry["bean"].Attributes["class"].Value;
                                        if (xnChildNode_entry["bean"].Attributes["parent"] != null)
                                            eEntry.sParent = xnChildNode_entry["bean"].Attributes["parent"].Value;

                                        // if (xnChildNode_entry["bean"]
                                        eEntry.dProperties = Property.getProperties(xnChildNode_entry["bean"]);
                                    }
                                    if (xnChildNode_entry.Attributes["value-ref"] != null)
                                        eEntry.sValue_Ref = xnChildNode_entry.Attributes["value-ref"].Value;
                                    dEntries.Add(eEntry.sKey, eEntry);
                                }
                //eEntry.sKey = "cc" + .Count.ToString();
            }
            /*
            XmlElement xeXmlElement = (XmlElement)xnXmlDataToProcess;
            foreach(XmlNode xnXmlNode in xeXmlElement.GetElementsByTagName("property"))
            {
                if (xnXmlNode	
            }*/

            return dEntries;
        }

        public override String ToString()
        {
            return sKey + "      " +
                   ((sParent == null) ? "" : "parent:" + sParent) + "  " +
                   ((sClass == null) ? "" : "class:" + sClass) + "  " +
                   ((sValue_Ref == null) ? "" : "value-ref:" + sValue_Ref) + "  ";
            //	((lProperties.Count ==0) ? "" : lProperties.Count.ToString()) + " properties";
        }
    }
}
