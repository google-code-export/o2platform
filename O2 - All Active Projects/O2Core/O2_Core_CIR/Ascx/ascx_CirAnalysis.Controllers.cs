// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.Collections.Generic;
using System.IO;
using O2.Core.CIR.CirObjects;
using O2.Core.CIR.CirUtils;
using O2.DotNetWrappers.ExtensionMethods;
using O2.Interfaces.CIR;
using O2.Interfaces.Messages;
using O2.Kernel.CodeUtils;

namespace O2.Core.CIR.Ascx
{
	partial class ascx_CirAnalysis
	{
        public ICirDataAnalysis cirDataAnalysis = new CirDataAnalysis();

        void o2MessageQueue_onMessages(IO2Message o2Message)
        {
            if (o2Message is IM_CirAction)
            {
                var cirAction = (IM_CirAction) o2Message;
                switch (cirAction.CirAction)
                {
                    case IM_CirActions.setCirData:                        
                        CirDataAnalysisUtils.addO2CirDataFile(cirDataAnalysis, cirAction.CirData);
                        raiseSetCirDataAnalysisO2Message();
                        //O2Messages.setCirDataAnalysis(cirDataAnalysis);
                        //DI.cirData = cirAction.CirData;
                        //updateCirDataStats();
                        break;
                }
            }
        }

        public void loadFile(String fileToLoad)
        {
            loadO2CirDataFile(fileToLoad);
        }

	    public void loadO2CirDataFile(String sFileToLoad)
        {
            loadO2CirDataFile(sFileToLoad, true, false /*decompileCodeIfNoPdb*/);
        }

        public ICirDataAnalysis getCirDataAnalysisObject()
        {
            return cirDataAnalysis;
        }

        public void loadO2CirDataFile(String sFileToLoad, bool raiseProcessNewDataLoad, bool decompileCodeIfNoPdb)
        {
            try
            {
                if (false == File.Exists(sFileToLoad))
                    DI.log.error("File to load doesnt exists: {0}", sFileToLoad);
                else
                {
                    CirDataAnalysisUtils.loadFileIntoCirDataAnalysisObject(sFileToLoad, cirDataAnalysis, decompileCodeIfNoPdb);
                    
                    if (raiseProcessNewDataLoad)
                        raiseSetCirDataAnalysisO2Message();
                    else
                    {
                        updateCirDataStats();
                    }
                }
            }
            catch (Exception ex)
            {
                DI.log.error("in loadO2CirDataFile: {0}", ex.Message);
            }
        }

	    private void raiseSetCirDataAnalysisO2Message()
        {
            //setO2CirDataAnalysisObject(cirDataAnalysis);
	        CirDataAnalysisUtils.remapIsCalledByXrefs(cirDataAnalysis);             // this is called when everything has been added, so it is the best place to remap the ISCalledBy XRefs
            O2Messages.setCirDataAnalysis(cirDataAnalysis);
            updateCirDataStats();
        }

        public void updateCirDataStats()
        {
            if (this.okThread(delegate { updateCirDataStats(); }))
            {
                laNumberOfClasses.Text = cirDataAnalysis.dCirClass_bySignature.Count + " Classes";
                laNumberOfMethods.Text = cirDataAnalysis.dCirFunction_bySignature.Count + " Functions";
                // show loaded files
                lbLoadedO2CirDataFiles.Items.Clear();
                foreach (String sLoadedO2CirData in cirDataAnalysis.dCirDataFilesLoaded.Keys)
                    lbLoadedO2CirDataFiles.Items.Add(sLoadedO2CirData);
            }
        }

        private void clearLoadedData()
        {
            cirDataAnalysis = new CirDataAnalysis();
            raiseSetCirDataAnalysisO2Message();
        }

        public List<String> getClasses()
        {
            return cirDataAnalysis.CirClasses<string>();
        }
        public List<String> getFunctions()
        {
            return cirDataAnalysis.CirFunctions<string>();
        }

	    /*public void setCirDataObject(ICirData _cirData)
        {
            DI.cirData = _cirData;  
        }*/
	}
}
