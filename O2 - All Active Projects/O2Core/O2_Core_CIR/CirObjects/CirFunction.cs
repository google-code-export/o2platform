// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.Collections.Generic;
using O2.Core.CIR.Xsd;
using O2.DotNetWrappers.Filters;
using O2.Interfaces.CIR;
using O2.Interfaces.O2Findings;

namespace O2.Core.CIR.CirObjects
{
    [Serializable]
    public class CirFunction : ICirFunction ,ICirTraces
    {
        // from ICirTraces
        public List<IO2Finding> IsSink { get; set; }
        public List<IO2Finding> IsSource { get; set; }

        // from ICirFunction
        public bool HasControlFlowGraph { get; set;}
     
        public bool OnlyShowFunctionNameInToString { get; set; }
        public ICirClass ParentClass { get; set; }
        public string ParentClassFullName { get; set; }
        public string ParentClassName { get; set; }
        public string Module { get; set; }        

        public List<ICirFunctionParameter> FunctionParameters { get; set; }
        public string ReturnType { get; set; }
        public string FunctionSignature { get; set; }
        public string FunctionNameAndParameters { get; set; }
        public string ClassNameFunctionNameAndParameters { get; set; }
        public string FunctionName { get; set; }
        public String SymbolDef { get; set; }

        public string File { get; set; }
        public string FileLine { get; set; }

        public List<ICirAttribute> FunctionAttributes { get; set; }
        public List<ICirAttribute> FunctionParameterAttributes { get; set; }

        public List<ICirFunction> FunctionsCalledUniqueList { get; set; }
        public List<ICirFunctionCall> FunctionsCalled { get; set; }
        public List<ICirFunctionCall> FunctionIsCalledBy { get; set; }

        public Dictionary<String, ICirSsaVariable> dSsaVariables { get; set; }
        public Dictionary<String, ICirFunctionVariable> dVariables { get; set; }
        public List<ControlFlowGraphBasicBlock> lcfgBasicBlocks { get; set; }
        public List<string> UsedTypes { get; set; }




        public string CecilSignature { get; set; }
        public string ReflectionSignature { get; set; }
        public string O2MDbgSignature { get; set; }

        public bool IsTainted { get; set; }

        public bool IsPrivate { get; set; }
        public bool IsStatic { get; set; }        
        public bool IsConstructor { get; set; }
        public bool IsUnmanaged { get; set; }
        public bool IsUnmanagedExport { get; set; }
        public bool IsVirtual { get; set; }
        public bool IsSetter { get; set; }
        public bool IsGetter { get; set; }
        public bool IsRuntime { get; set; }
        public bool IsPublic { get; set; }
        public bool IsPInvokeImpl { get; set; }
        public bool IsNative { get; set; }
        public bool IsManaged { get; set; }
        public bool IsInternalCall { get; set; }
        public bool IsIL { get; set; }
        public bool IsAbstract { get; set; }
        public bool HasSecurity { get; set; }
        public bool HasBody { get; set; }

        public bool HasBeenProcessedByCirFactory { get; set; }


        public CirFunction()
        {
            HasControlFlowGraph = false;
            //IsPrivate = false;                
            //IsStatic = false;
            OnlyShowFunctionNameInToString = false;
            FunctionParameters = new List<ICirFunctionParameter>();
            FunctionsCalledUniqueList = new List<ICirFunction>();
            FunctionsCalled = new List<ICirFunctionCall>();
            UsedTypes = new List<String>();
            FunctionIsCalledBy = new List<ICirFunctionCall>();
            dSsaVariables = new Dictionary<string, ICirSsaVariable>();
            dVariables = new Dictionary<string, ICirFunctionVariable>();
            lcfgBasicBlocks = new List<ControlFlowGraphBasicBlock>();
            FunctionAttributes = new List<ICirAttribute>(); 
            FunctionParameterAttributes = new List<ICirAttribute>();
            ReturnType = "";
            FunctionSignature = "";
            FunctionNameAndParameters = "";
        }
        public CirFunction(string functionSignature) : this()
        {
            FunctionSignature = functionSignature;
            var filteredSignature = new FilteredSignature(FunctionSignature);
            FunctionName = filteredSignature.sFunctionName;
            FunctionNameAndParameters = filteredSignature.sFunctionNameAndParams;
            ClassNameFunctionNameAndParameters = filteredSignature.sFunctionClass + "." + filteredSignature.sFunctionNameAndParams;
            ReturnType = filteredSignature.sReturnClass;
        }

        public override string ToString()
        {
            if (false == OnlyShowFunctionNameInToString)
                return FunctionSignature;

            String sStringToFilter = FunctionSignature;
            const bool bShowParameters = true;
            const bool bShowReturnClass = true;
            const bool bShowNamespace = true;
            const int iNamespaceDepth = 1;
            return FilteredSignature.filterSignature(sStringToFilter, bShowParameters, bShowReturnClass, bShowNamespace,
                                                     iNamespaceDepth);        
        }


        public ICirFunction addCalledFunction(string calledFunctionSignature)        
        {
            return addCalledFunction(calledFunctionSignature, "" , -1);
        }

        public ICirFunction addCalledFunction(string calledFunctionSignature, string file, int lineNumber)
        {
            ICirFunction calledCirFunction = null;
            foreach (var existingCirFunctionCallReference in FunctionsCalledUniqueList)
                if (calledFunctionSignature == existingCirFunctionCallReference.FunctionSignature)
                    calledCirFunction = existingCirFunctionCallReference;
            if (calledCirFunction == null)      // means we have not mapped a function with this signature before
            {
                calledCirFunction = new CirFunction(calledFunctionSignature);
                calledCirFunction.File = file;
                FunctionsCalledUniqueList.Add(calledCirFunction);
            }
            FunctionsCalled.Add(new CirFunctionCall(calledCirFunction, calledCirFunction.File, lineNumber));
            return calledCirFunction;
        }
    }
}
