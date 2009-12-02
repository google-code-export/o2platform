// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.Collections.Generic;
using O2.Core.CIR.CirUtils;
using O2.Kernel.Interfaces.CIR;

namespace O2.Core.CIR.CirObjects
{
    [Serializable]
    public class CirData // object with loaded Cir Data (after some analisis and xrefs
        : ICirData
    {        
        public bool bStoreControlFlowBlockRawDataInsideCirDataFile { get; set;}
        // this makes the file much bigger and we only need this if we are going to manually analyze this file

        public bool bVerbose { get; set;}

        public Dictionary<string, ICirClass> dClasses_bySignature  { get; set;}
        public Dictionary<string, ICirClass> dClasses_bySymbolDef { get; set; }

        public Dictionary<string, ICirFunction> dFunctions_bySignature { get; set; }                
        //public Dictionary<string, ICirFunction> dFunctions_bySymbolDef { get; set; }
        public Dictionary<string, ICirFunction> dTemp_Functions_bySymbolDef { get; set; }
        
        public Dictionary<string, string> dSymbols { get; set; }
        public List<String> lFiles { get; set; }
        public string sDbId { get; set; }

        public CirData()
        {            
            init();
        }

        public void init()
        {
            dClasses_bySignature = new Dictionary<string, ICirClass>();
            dClasses_bySymbolDef = new Dictionary<string, ICirClass>();
            dFunctions_bySignature = new Dictionary<string, ICirFunction>();
            //dFunctions_bySymbolDef = new Dictionary<string, ICirFunction>();
            dTemp_Functions_bySymbolDef = new Dictionary<string, ICirFunction>();
            lFiles = new List<string>();
            dSymbols = new Dictionary<string, string>();
            bStoreControlFlowBlockRawDataInsideCirDataFile = false;
            sDbId = "";
        }

        public ICirClass getClass(string cirClassToFind)
        {
            return getClass(cirClassToFind, true, true);
        }

        public ICirClass getClass(string cirClassToFind, bool exactMatch, bool createOnNotMatch)
        {
            // try to find a directy match
            if (dClasses_bySignature.ContainsKey(cirClassToFind))
                return dClasses_bySignature[cirClassToFind];
            if (exactMatch == false)
            {
                // try to find a directy just on the FullName (since the Signature contains the Module name
                foreach (ICirClass cirClass in dClasses_bySignature.Values)
                    if (cirClass.FullName == cirClassToFind)
                        return cirClass;


                // try to find a directy just on the name
                foreach (ICirClass cirClass in dClasses_bySignature.Values)
                    if (cirClass.Name == cirClassToFind)
                        return cirClass;
            }
            if (createOnNotMatch)
                return addClass(cirClassToFind);

            return null;
        }

        public ICirClass addClass(string newClassSignature)
        {
            if (newClassSignature == null || dClasses_bySignature.ContainsKey(newClassSignature))
                return null;
            //return dClasses_bySignature[classSignature];

            var cirClass = new CirClass(newClassSignature);
            
            dClasses_bySignature.Add(newClassSignature, cirClass);
            return cirClass;
        }

        /// <summary>
        /// use this to make sure all main dictionaires are syncronized
        /// </summary>
        public void remapXRefs()
        {
            var tempCirDataAnalysis = new CirDataAnalysis(this);            
            foreach (var cirClass in dClasses_bySignature.Values)
                foreach (var cirFunction in cirClass.dFunctions.Values)
                    if (false == dClasses_bySignature.ContainsKey(cirFunction.FunctionSignature))
                        dFunctions_bySignature.Add(cirFunction.FunctionSignature, cirFunction);

            CirDataAnalysisUtils.remapIsCalledByXrefs(tempCirDataAnalysis);
        }        
    }
}
