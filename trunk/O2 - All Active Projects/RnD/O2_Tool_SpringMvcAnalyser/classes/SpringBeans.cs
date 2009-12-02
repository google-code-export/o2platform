// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.Collections.Generic;
using System.Xml;

namespace O2.RnD.SpringMVCAnalyzer.classes
{
    public class SpringBeans
    {
        public Dictionary<String, XmlNode> dBeans;
        public Dictionary<String, SpringMappingItem> dSpringMappingItems = new Dictionary<String, SpringMappingItem>();
        public Dictionary<String, String> dUrlMappings; // <key, bean_name>
        //public Dictionary<String,SpringController> dControllers;

        public SpringBeans(Dictionary<String, XmlNode> dBeans)
        {
            this.dBeans = dBeans;
            resolveUrlMappings();
            resolveSpringMappingItem();
        }

        public void resolveUrlMappings()
        {
            if (dBeans.ContainsKey("urlMapping"))
            {
                dUrlMappings = new Dictionary<String, String>();
                var xeUrlMapping = (XmlElement) dBeans["urlMapping"];
                foreach (XmlNode xnXmlNode in xeUrlMapping.GetElementsByTagName("prop"))
                    dUrlMappings.Add(xnXmlNode.Attributes["key"].Value,
                                     xnXmlNode.InnerXml.Replace(Environment.NewLine, "").Trim());
                DI.log.debug("ChildNodes: {0}", dBeans["urlMapping"].ChildNodes.Count);
            }
        }

        public void resolveSpringMappingItem()
        {
            //String sTilesFile = "tiles-view-definitions.xml";
            //Dictionary<String, String> dTilesDefinitions = getDictionaryWithTilesDefinitions(Path.Combine(sWebRoot, sTilesFile));

            // fadCirData = load.loadSerializedO2CirDataObject(sO2CirDataFile);
            //SpringBeans sbSpringBeans = new SpringBeans(beansUtils.getAllBeans_RecursiveSearch(sWebRoot));

            foreach (String sBeanName in dBeans.Keys)
                //String sBeanName	= "userController";
            {
                XmlNode xnBean = getBean(sBeanName);
                var spSpringControler = new SpringController(sBeanName, xnBean);

                var smSpringMapping = new SpringMappingItem();
                smSpringMapping.sInnerXml = xnBean.InnerXml;
                if (spSpringControler.dEntries.Count == 0)
                {
                    smSpringMapping.sBean = spSpringControler.sId;
                    smSpringMapping.sClass = spSpringControler.sClass;
                }
                else
                    foreach (String sName in spSpringControler.dEntries.Keys)
                    {
                        smSpringMapping.sBean = sBeanName;
                        smSpringMapping.sClass = spSpringControler.sClass;
                        smSpringMapping.sKey = sName;
                        if (spSpringControler.dEntries[sName].dProperties.ContainsKey("commandClass"))
                            smSpringMapping.sCommandClass =
                                spSpringControler.dEntries[sName].dProperties["commandClass"].sValue;
                        if (spSpringControler.dEntries[sName].dProperties.ContainsKey("formView"))
                            smSpringMapping.sFormView = spSpringControler.dEntries[sName].dProperties["formView"].sValue;
                        if (spSpringControler.dEntries[sName].dProperties.ContainsKey("commandName"))
                            smSpringMapping.sCommandName =
                                spSpringControler.dEntries[sName].dProperties["commandName"].sValue;
                        //	 DI.log.info(smSpringMapping.ToString());
                        //if (smSpringMapping.sFormView != "" && smSpringMapping.sCommandClass != "")
                        //{
                        //   if (dTilesDefinitions.ContainsKey(smSpringMapping.sFormView))
                        //       smSpringMapping.sJsp = (sWebPath + dTilesDefinitions[smSpringMapping.sFormView]).Replace(@"/", @"\"); ;


                        //}
                    }

                dSpringMappingItems.Add(smSpringMapping.sBean, smSpringMapping);
            }
        }

        public XmlNode getBean(String sBeanName)
        {
            if (false == dBeans.ContainsKey(sBeanName))
            {
                DI.log.error("Bean does not exist : {0}", sBeanName);
                return null;
            }
            else
                return dBeans[sBeanName];
        }

        public void ShowBeanDetails(String sBeanName)
        {
            XmlNode xnBean = getBean(sBeanName);
            if (null != xnBean)
            {
                DI.log.debug("Showing details for bean:{0}", sBeanName);
            }
        }

        public void ShowControllerDetails(String sBeanName)
        {
            DI.log.info("ShowControllerDetails: {0}", sBeanName);
            XmlNode xnBean = getBean(sBeanName);
            if (null != xnBean)
            {
                var spSpringControler = new SpringController(sBeanName, xnBean);
                spSpringControler.ShowDetails();
            }
        }
    }
}
