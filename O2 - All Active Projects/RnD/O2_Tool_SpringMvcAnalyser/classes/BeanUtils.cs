using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using O2.DotNetWrappers.DotNet;
using O2.DotNetWrappers.O2Misc;
using O2.DotNetWrappers.Windows;


namespace O2.RnD.SpringMVCAnalyzer.classes
{
    internal class BeanUtils
    {
        private static bool bLoadFromO2VarsIfAvailable = true;
        private static String sO2VarToHoldBeansDictionary = "dBeans";

        public static Dictionary<String, XmlNode> getAllBeans_RecursiveSearch(String sWebRoot)
        {
            if (bLoadFromO2VarsIfAvailable && vars.get(sO2VarToHoldBeansDictionary) != null)
                if (((Dictionary<String, XmlNode>) vars.get(sO2VarToHoldBeansDictionary)).Count > 0)
                    return (Dictionary<String, XmlNode>) vars.get(sO2VarToHoldBeansDictionary);
            O2Timer tTimer = new O2Timer("Loading all beans from web root").start();
            var dBeans = new Dictionary<String, XmlNode>();
            var lsXmlFilesInWebRoot = new List<String>();
            Files.getListOfAllFilesFromDirectory(lsXmlFilesInWebRoot, sWebRoot, true, "*.xml", false);
            DI.log.info("{0} xml files found", lsXmlFilesInWebRoot.Count);
            int iFilesProcessed = 0;
            foreach (var sXmlFile in lsXmlFilesInWebRoot)
            {
                try
                {
                    if (Path.GetExtension(sXmlFile).ToLower() == ".xml")
                    {
                        DI.log.info("({0}/{1} Processing xml file: {2}", (iFilesProcessed++), lsXmlFilesInWebRoot.Count,
                                    sXmlFile);
                        var xdXmlDocument = new XmlDocument();
                        String sXmlFileContents = Files.getFileContents(sXmlFile);
                        sXmlFileContents = sXmlFileContents.Replace("<!DOCTYPE", "<!--");
                        sXmlFileContents = sXmlFileContents.Replace(".dtd\">", "-->");

                        xdXmlDocument.LoadXml(sXmlFileContents);
                        xdXmlDocument.XmlResolver = null;
                        XmlNodeList xnlBeans = xdXmlDocument.GetElementsByTagName("bean");

                        foreach (XmlNode xnNode in xnlBeans)
                            if (xnNode.Attributes["id"] != null)
                            {
                                // first add by id
                                if (!dBeans.ContainsKey(xnNode.Attributes["id"].Value))
                                    dBeans.Add(xnNode.Attributes["id"].Value, xnNode);
                            }
                            else if (xnNode.Attributes["name"] != null) // then add by name
                                if (!dBeans.ContainsKey(xnNode.Attributes["name"].Value))
                                    dBeans.Add(xnNode.Attributes["name"].Value, xnNode);
                    }
                }
                catch (Exception ex)
                {
                    DI.log.ex(ex, "in getAllBeans_RecursiveSearch (inside foreach (var sXmlFile in lsXmlFilesInWebRoot)");
                    throw;
                }
            }
            vars.set_(sO2VarToHoldBeansDictionary, dBeans);
            DI.log.info("{0} beans loaded", dBeans.Count);

            tTimer.stop();
            return dBeans;
        }
    }
}