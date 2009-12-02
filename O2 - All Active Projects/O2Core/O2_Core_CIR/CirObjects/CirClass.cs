using System;
using System.Collections.Generic;
using O2.Core.CIR.CirUtils;
using O2.Core.CIR.CirUtils;
using O2.Core.CIR.CirUtils;
using O2.DotNetWrappers.Filters;
using O2.Kernel.Interfaces.CIR;

namespace O2.Core.CIR.CirObjects
{
    [Serializable]
    public class CirClass : ICirClass
    {
        public bool bClassHasMethodsWithControlFlowGraphs { get; set;}
        // keeping track of this helps to speed up the GUI display componebts
        
        public List<ICirAttribute> ClassAttributes { get; set; }        

        public Dictionary<string, ICirFieldClass> dField_Class { get; set; }
        public Dictionary<string, ICirFieldMember> dField_Member { get; set; }
        

        public Dictionary<string, ICirFunction> dFunctions { get; set; }

        // contains  list of classes that superclass the current one (Implement, Extend)
        public Dictionary<string, ICirClass> dIsSuperClassedBy { get; set; }

        // contains list of classes that this class superclass (Implement , Extend)
        public Dictionary<string, ICirClass> dSuperClasses { get; set; }
                
        public List<ICirFunction> lcmIsUsedByMethod_Argument { get; set; }
        public List<ICirFunction> lcmIsUsedByMethod_InternalVariable { get; set; }
        public List<ICirFunction> lcmIsUsedByMethod_ReturnType { get; set; }
               
        public string Signature { get; set; }
        public bool IsAbstract { get; set; }
        public int IsAnonymous { get; set; }
        public bool IsClass { get; set; }
        public bool IsEnum { get; set; }
        public bool IsInterface { get; set; }
        public bool IsImport { get; set; }
        public bool IsNotPublic { get; set; }
        public bool IsPublic { get; set; }
        public bool HasSecurity { get; set; }

        public string Name { get; set; }
        public string FullName { get; set; }
        public string Namespace { get; set; }
        public string Module { get; set; }

        public string SymbolDef { get; set; }

        public string File { get; set; }
        public string FileLine { get; set; }

        public bool HasBeenProcessedByCirFactory { get; set; }

        public CirClass()
        {
            ClassAttributes = new List<ICirAttribute>();
            dField_Class = new Dictionary<string, ICirFieldClass>();
            dField_Member = new Dictionary<string, ICirFieldMember>();
            dFunctions = new Dictionary<string, ICirFunction>();

            dIsSuperClassedBy = new Dictionary<string, ICirClass>();
            dSuperClasses = new Dictionary<string, ICirClass>();

            lcmIsUsedByMethod_Argument = new List<ICirFunction>();
            lcmIsUsedByMethod_InternalVariable = new List<ICirFunction>();
            lcmIsUsedByMethod_ReturnType = new List<ICirFunction>();
            Name = "";
            Signature = "";
            bClassHasMethodsWithControlFlowGraphs = false;
            SymbolDef = "";
        }

        public CirClass(string signature) : this()
        {
            Signature = signature;
            FullName = signature;
            int lastDot = signature.LastIndexOf('.');
            if (lastDot > -1)
            {
                Name = signature.Substring(lastDot + 1);
                Namespace = signature.Substring(0, lastDot);
            }
        }

        public override string ToString()
        {
            String sStringToFilter = Signature;
            const bool bShowParameters = true;
            const bool bShowReturnClass = false;
            const bool bShowNamespace = false;
            const int iNamespaceDepth = 1;
            return FilteredSignature.filterSignature(sStringToFilter, bShowParameters, bShowReturnClass, bShowNamespace,
                                                     iNamespaceDepth);
        }

        public ICirClass clone_SimpleMode()
        {
            return CirCopy.copy(this);
        }

        public ICirFunction getFunction(string functionSignature)
        {                        
            // try to find a direct match (this means that functionSignature contain the complete signature
            if (dFunctions.ContainsKey(functionSignature))
            {
                return dFunctions["functionSignature"];
            }
            // if not lets try to find just based on the function name and its parameters
            foreach (ICirFunction cirFunction in dFunctions.Values)
                if (cirFunction.FunctionNameAndParameters == functionSignature)
                    return cirFunction;

            // if the previous one didn't work then just look for a name match (_note that only the first match will be returned)
            foreach (ICirFunction cirFunction in dFunctions.Values)
                if (cirFunction.FunctionName == functionSignature)
                    return cirFunction;

            // and if nothing works just return null
            return null;
        }

        public ICirFunction addFunction(string newFunctionSignature)
        {
            if (newFunctionSignature == null || dFunctions.ContainsKey(newFunctionSignature))
                return null;

            var cirFunction = new CirFunction(newFunctionSignature);            
            dFunctions.Add(newFunctionSignature, cirFunction);
            return cirFunction;
        }
    }
}