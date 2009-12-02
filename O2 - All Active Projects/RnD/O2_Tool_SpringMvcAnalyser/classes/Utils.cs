using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Xml;
using O2.Core.CIR.CirObjects;
using O2.Core.CIR.CirUtils;
using O2.DotNetWrappers.Windows;
using O2.ImportExport.OunceLabs.Ozasmt_OunceV6;
using O2.Legacy.OunceV6.SavedAssessmentFile.classes;

namespace O2.RnD.SpringMVCAnalyzer.classes
{
    internal class Utils
    {
        private static readonly CirData fadCirData = new CirData();
        private static bool bExpandAllAfterDataIsLoaded = true;
        private static List<String> lsClassesToFindInBeanList_Bad;
        private static List<String> lsClassesToFindInBeanList_Good;
        private static String sO2CirDataFile = @"c:\cirdumps\jpetstore.CirData";

        private static string sOzasmt_FindingContent_SetPath =
            @"C:\Documents and Settings\rberg\Desktop\jpetStore_Spring  8-26-08 1130AM.ozasmt";

        private static String sWebPath = @"C:\projects\jpetStore_Spring\WebContent\";
        private static String sWebRoot = @"C:\projects\jpetStore_Spring\WebContent\WEB-INF";

        /// <summary>
        /// NOT COMPLETED
        /// </summary>
        public static void test_SpringForm_MapToJsps()
        {
            List<SpringMappingItem> lsmiSpringMappingItems = getListOfSpringMappingItems();
            Dictionary<String, List<String>> dJspUsedFields = getDictionaryWithJSPsUsedFields();
            foreach (SpringMappingItem smiSpringMappingItem in lsmiSpringMappingItems)
            {
                if (dJspUsedFields.ContainsKey(smiSpringMappingItem.sJsp))
                {
                    DI.log.debug("On " + smiSpringMappingItem.sJsp); // + "   " + smiSpringMappingItem.ToString());	
                    DI.log.debug("Fields used in JSP ");
                    var sJspFields = new List<String>(dJspUsedFields[smiSpringMappingItem.sJsp]);
                    sJspFields.Sort();
                    foreach (String sFieldUsed in sJspFields)
                        DI.log.info("   {0} ", sFieldUsed);
                    DI.log.debug("Command class: {0}", smiSpringMappingItem.sCommandClass);
                    foreach (
                        CirFunction cfCirFunction in
                            fadCirData.dClasses_bySignature[smiSpringMappingItem.sCommandClass].dFunctions.Values)
                        if (cfCirFunction.FunctionSignature.IndexOf(".set") > -1)
                            DI.log.error("     .. {0}",
                                                  cfCirFunction.FunctionSignature.Replace(
                                                      smiSpringMappingItem.sCommandClass + ".set", ""));


                    //fadCirData.dClasses_bySignature[sClassSignature]
                }
            }
        }

        /// <summary>
        /// NOT COMPLETED
        /// </summary>
        public static Dictionary<String, List<String>> getDictionaryWithJSPsUsedFields()
        {
            var dMappedFields = new Dictionary<String, List<String>>();
  /*          O2AssessmentData_OunceV6 fadAssessmentData = null;
            Analysis.loadAssessmentFile(sOzasmt_FindingContent_SetPath, true, ref fadAssessmentData);
            Analysis.populateDictionariesWithXrefsToLoadedAssessment(Analysis.FindingFilter.AllFindings, false, false,
                                                                     fadAssessmentData);
            var fFindingsInFile = new List<AssessmentAssessmentFileFinding>();
            
            foreach (AssessmentAssessmentFileFinding fFinding in fadAssessmentData.dFindings.Keys)
            {
                AssessmentAssessmentFile fFile = fadAssessmentData.dFindings[fFinding];
                if (false == dMappedFields.ContainsKey(fFile.filename))
                    dMappedFields.Add(fFile.filename, new List<String>());
                dMappedFields[fFile.filename].Add(extractPathValue(fFinding.context));
            }*/
            return dMappedFields;
        }

        public static List<SpringMappingItem> getListOfSpringMappingItems()
        {
/*
            Dictionary<String, String> dTilesDefinitions = getDictionaryWithTilesDefinitions(Path.Combine(sWebRoot, "tiles-view-definitions.xml"));

            fadCirData = load.loadSerializedO2CirDataObject(sO2CirDataFile);
                SpringBeans sbSpringBeans = new SpringBeans(beansUtils.getAllBeans_RecursiveSearch(sWebRoot));

            List<SpringMappingItem> lsmiSpringMappingItems = new List<SpringMappingItem>();
            foreach (String sBeanName in sbSpringBeans.dBeans.Keys)
            //String sBeanName	= "userController";
            {
                XmlNode xnBean = sbSpringBeans.getBean(sBeanName);
                SpringController spSpringControler = new SpringController(sBeanName, xnBean);

                foreach (String sName in spSpringControler.dEntries.Keys)
                {
                    SpringMappingItem smSpringMapping = new SpringMappingItem();
                    smSpringMapping.sBean = sBeanName;
                    smSpringMapping.sClass = spSpringControler.sClass;
                    smSpringMapping.sKey = sName;
                    if (spSpringControler.dEntries[sName].dProperties.ContainsKey("commandClass"))
                        smSpringMapping.sCommandClass = spSpringControler.dEntries[sName].dProperties["commandClass"].sValue;
                    if (spSpringControler.dEntries[sName].dProperties.ContainsKey("formView"))
                        smSpringMapping.sFormView = spSpringControler.dEntries[sName].dProperties["formView"].sValue;
                    if (spSpringControler.dEntries[sName].dProperties.ContainsKey("commandName"))
                        smSpringMapping.sCommandName = spSpringControler.dEntries[sName].dProperties["commandName"].sValue;
                    //	 DI.log.info(smSpringMapping.ToString());
                    if (smSpringMapping.sFormView != "" && smSpringMapping.sCommandClass != "")
                    {
                        if (dTilesDefinitions.ContainsKey(smSpringMapping.sFormView))
                            smSpringMapping.sJsp = (sWebPath + dTilesDefinitions[smSpringMapping.sFormView]).Replace(@"/", @"\"); ;

                        lsmiSpringMappingItems.Add(smSpringMapping);

                    }
                }
            }
            return lsmiSpringMappingItems;
 */
            return null;
        }


        public static Dictionary<String, String> getDictionaryWithTilesDefinitions(String sTilesXmlFile)
        {
            var dTilesDefinitions = new Dictionary<String, String>();
            var xdTilesDefinition = new XmlDocument();
            String sXmlFileContents = Files.getFileContents(sTilesXmlFile);
            sXmlFileContents = sXmlFileContents.Replace("<!DOCTYPE", "<!--");
            sXmlFileContents = sXmlFileContents.Replace(".dtd\">", "-->");

            xdTilesDefinition.LoadXml(sXmlFileContents);

            foreach (XmlNode xdDefinition in xdTilesDefinition.GetElementsByTagName("definition"))
            {
                if (xdDefinition.ChildNodes != null)
                    foreach (XmlNode xnXmlNode in xdDefinition.ChildNodes)
                        if (xnXmlNode.Attributes != null && xnXmlNode.Attributes["name"] != null &&
                            xnXmlNode.Attributes["name"].Value == "content")
                            dTilesDefinitions.Add(xdDefinition.Attributes["name"].Value,
                                                  xnXmlNode.Attributes["value"].Value);
            }
            return dTilesDefinitions;
        }

        public static String extractPathValue(String sContext)
        {
            int iIndexOfFirstQuote = sContext.IndexOf("\"");
            int iIndexOfLastQuote = sContext.LastIndexOf("\"");
            if (iIndexOfFirstQuote > -1 && iIndexOfLastQuote > -1)
                return sContext.Substring(iIndexOfFirstQuote + 1, iIndexOfLastQuote - iIndexOfFirstQuote - 1);
            return sContext;
        }


        public static void findSpringOnSubmitMethods(List<String> lsSpringMvcClasses)
        {
            lsClassesToFindInBeanList_Bad = new List<String>();
            lsClassesToFindInBeanList_Good = new List<String>();
            foreach (String sClassSignature in lsSpringMvcClasses)
            {
                foreach (CirFunction cfCirFunction in fadCirData.dClasses_bySignature[sClassSignature].dFunctions.Values
                    )
                {
                    if (cfCirFunction.FunctionSignature.IndexOf("onSubmit") > -1)
                        if (false == lsClassesToFindInBeanList_Bad.Contains(sClassSignature))
                        {
                            lsClassesToFindInBeanList_Bad.Add(sClassSignature);
                            DI.log.info("Bad: {0}", sClassSignature);
                        }

                    if (cfCirFunction.FunctionSignature.IndexOf("initBinder") > -1)
                        foreach (String sCalledFunction in ViewHelpers.getCirFunctionStringList(cfCirFunction.FunctionsCalledUniqueList))
                            if (sCalledFunction.IndexOf("setAllowedFields") > -1)
                            {
                                lsClassesToFindInBeanList_Good.Add(sClassSignature);
                                DI.log.debug("init binder -> setAllowedFields: " + sCalledFunction);
                            }
                }
                //if (false == lsClassesToFindInBeanList_Bad.Contains(sClassSignature))
                //	 DI.log.info("Skipping: {0}", sClassSignature);
            }
        }


        public static void showMappingsInTreeView(SpringBeans sbSpringBeans)
        {
            throw new Exception("showMappingsInTreeView not converted");
            /*
            Messages.sendMessage("close SpringXmlViewer");
            Messages.sendMessage("open EmptyControl,SpringXmlViewer");
            ascx_EmptyControl aecHostControl = (ascx_EmptyControl)Messages.sendMessage("SpringXmlViewer.ascx_EmptyControl");
            TreeView tvTreeView = new TreeView();
            tvTreeView.Sorted = true;
            tvTreeView.Dock = System.Windows.Forms.DockStyle.Fill;
            aecHostControl.Controls.Add(tvTreeView);

            addUrlMappingsToTreeView(tvTreeView, sbSpringBeans);
            if (bExpandAllAfterDataIsLoaded)
                tvTreeView.ExpandAll();
            //xdXmlDocument.Load(@"E:\2ndCodeDrop\AfterCompile\SourceCodeZips\web_war\WEB-INF\spring-web-beans.xml");

            //atvAscxTreeView.viewObject(tvTreeView);
            */
        }

        public static void addUrlMappingsToTreeView(TreeView tvTargetTreeView, SpringBeans sbSpringBeans)
        {
            int iItemsToShow = 50;
            foreach (String sUrlMapping in sbSpringBeans.dUrlMappings.Keys)
            {
                TreeNode tnTreeNode = tvTargetTreeView.Nodes.Add(sUrlMapping);
                addBeanInfoToTreeNode(tnTreeNode, sbSpringBeans.dUrlMappings[sUrlMapping], "urlMapping", sbSpringBeans);
                if (iItemsToShow-- == 0)
                    return;
            }
        }

        public static void colorCodeTreeNode(TreeNode tnTreeNode, String sBeanName, String sBeanType)
        {
            // default colors
            /*	if (sBeanType == "action")
                    tnTreeNode.ForeColor = Color.Blue;
                if (sBeanType == "class")
                    tnTreeNode.ForeColor = Color.Gray;
                if (sBeanType == "value-ref")
                    tnTreeNode.ForeColor = Color.Orange;
                if (sBeanType == "parent")
                    tnTreeNode.ForeColor = Color.Green;*/
            tnTreeNode.ForeColor = Color.Gray;
            if (sBeanType == "urlMapping")
                tnTreeNode.ForeColor = Color.Brown;

            // now do searches
            var lSearchStrings = new List<String>();
            lSearchStrings.Add("SearchController");
            lSearchStrings.Add("memberReportController");
            //lSearchStrings.Add("AuthoriseAction");

            //	foreach(String sSearchItems in lSearchStrings)
            foreach (String sSearchItems in lsClassesToFindInBeanList_Bad)
                if (sBeanName.IndexOf(sSearchItems) > -1)
                {
                    tnTreeNode.ForeColor = Color.Red;
                    tnTreeNode.NodeFont = new Font("Microsoft Sans Serif", 9F, FontStyle.Bold, GraphicsUnit.Point, ((0)));
                    tnTreeNode.Text += "           ";
                }

            foreach (String sSearchItems in lsClassesToFindInBeanList_Good)
                if (sBeanName.IndexOf(sSearchItems) > -1)
                {
                    tnTreeNode.ForeColor = Color.Green;
                    tnTreeNode.NodeFont = new Font("Microsoft Sans Serif", 9F, FontStyle.Bold, GraphicsUnit.Point, ((0)));
                    tnTreeNode.Text += "           ";
                }
        }

        public static void addBeanInfoToTreeNode(TreeNode tnTreeNode, String sBeanName, String sBeanType,
                                                 SpringBeans sbSpringBeans)
        {
            if (sbSpringBeans.dBeans.ContainsKey(sBeanName))
            {
                var spSpringControler = new SpringController(sBeanName, sbSpringBeans.dBeans[sBeanName]);
                //	spSpringControler.ShowDetails();

                var tnNewTreeNode = new TreeNode(sBeanType + "  :  " + sBeanName);
                colorCodeTreeNode(tnNewTreeNode, sBeanName, sBeanType);
                tnNewTreeNode.Tag = spSpringControler;
                tnTreeNode.Nodes.Add(tnNewTreeNode);

                // show class
                if (spSpringControler.sClass != null)
                {
                    colorCodeTreeNode(tnNewTreeNode.Nodes.Add("class:" + spSpringControler.sClass),
                                      spSpringControler.sClass, "class");
                }
                // show parent
                if (spSpringControler.sParent != null)
                    addBeanInfoToTreeNode(tnNewTreeNode, spSpringControler.sParent, "parent", sbSpringBeans);
                // show actionMappings property
                foreach (String sName in spSpringControler.dProperties.Keys)
                    if (sName == "actionMappings")
                    {
                        Dictionary<String, ActionMappingEntry> dEntries =
                            ActionMappingEntry.getEntries(spSpringControler.dProperties["actionMappings"]);
                        // show entries
                        foreach (String sEntry in dEntries.Keys)
                        {
                            TreeNode tnEntry;
                            if (dEntries[sEntry].sClass != null)
                                //tnEntry.Nodes.Add("class:" + dEntries[sEntry].sClass);
                                tnEntry = new TreeNode("action: " + sEntry + " , class:" + dEntries[sEntry].sClass);
                            else
                                tnEntry = new TreeNode("action: " + sEntry);
                            colorCodeTreeNode(tnEntry, tnEntry.Text, "action");
                            // show entry.class

                            // show entry.value-ref
                            if (dEntries[sEntry].sValue_Ref != null)
                            {
                                //TreeNode tnValueRef = tnEntry.Nodes.Add("value-ref:" + dEntries[sEntry].sValue_Ref);
                                addBeanInfoToTreeNode(tnEntry, dEntries[sEntry].sValue_Ref, "value-ref", sbSpringBeans);
                            }
                            // show entry.parent
                            if (dEntries[sEntry].sParent != null)
                            {
                                addBeanInfoToTreeNode(tnEntry, dEntries[sEntry].sParent, "parent", sbSpringBeans);
                            }

                            //tnNewTreeNode.Nodes.Add("entry: " + sEntry);
                            tnNewTreeNode.Nodes.Add(tnEntry);
                        }
                    }
                //tnNewTreeNode.Nodes.Add(String.Format("property: {0} = {1}" , sName, spSpringControler.dProperties[sName]));
                // DI.log.debug("  Property:  {0}", dProperties[sName]);			
                // show entries
                //	foreach(String sName in dEntries.Keys)
                //		 DI.log.debug("  Entry:  {0}",dEntries[sName]);
            }
            else
            {
                tnTreeNode.Nodes.Add(String.Format("ERROR: COULD NOT RESOLVE BEAN: {0} : {1}", sBeanType, sBeanName));
            }
        }

        public static void listUrlMappings(Dictionary<String, XmlNode> dBeans)
        {
        }

        public static void listBeans(SpringBeans sbSpringBeans)
        {
            foreach (String sBeanName in sbSpringBeans.dBeans.Keys)
                DI.log.info(sBeanName);
        }
    }
}