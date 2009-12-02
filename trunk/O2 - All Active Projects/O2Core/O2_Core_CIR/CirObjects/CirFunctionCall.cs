using System;
using Mono.Cecil.Cil;
using O2.Kernel.Interfaces.CIR;
using O2.Kernel.Interfaces.CIR;

namespace O2.Core.CIR.CirObjects
{
    [Serializable]
    class CirFunctionCall : ICirFunctionCall
    {
        public ICirFunction cirFunction { get; set; }
        public int lineNumber { get; set; }
        public int endLine { get; set; }
        public int startColumn { get; set; }
        public int endColumn { get; set; }        
        public string fileName { get; set; }
        public int sequenceNumber { get; set; }
        public string sourceCodeText { get; set; }

        public CirFunctionCall()
        {
            cirFunction = null;
            lineNumber = -1;            // -1 means there is no source code references
            endLine = -1;
            startColumn = -1;
            endColumn = -1;
            sequenceNumber = -1;
            sourceCodeText = "";
        }

        public CirFunctionCall(ICirFunction _cirFunction, SequencePoint sequencePoint)
            : this(_cirFunction)
        {            
            if (sequencePoint!=null)
            {
                fileName = sequencePoint.Document.Url;
                lineNumber = sequencePoint.StartLine;
                endLine = sequencePoint.EndLine;
                startColumn = sequencePoint.StartColumn;
                endColumn = sequencePoint.EndColumn;                
            }
            else
            {
                
            }
        }

        public CirFunctionCall(ICirFunction _cirFunction) : this()
        {
            cirFunction = _cirFunction;
        }

        public CirFunctionCall(ICirFunction _cirFunction, string _fileName, string _lineNumber) : this(_cirFunction)
        {
            try
            {
                lineNumber = Int32.Parse(_lineNumber);  // we can't use properties on TryParse                
                fileName = _fileName;
            }
            catch (Exception ex )
            {
                DI.log.error("in CirFunctionCall: {0}", ex.Message);                
            }            
        }

        public CirFunctionCall(ICirFunction _cirFunction, string _fileName, int _lineNumber)
            : this(_cirFunction)
        {
            lineNumber = _lineNumber;
            fileName = _fileName;
        }

        public CirFunctionCall(ICirFunction _cirFunction, string _fileName, int _lineNumber, int _sequenceNumber)
            : this(_cirFunction)            
        {
            lineNumber = _lineNumber;
            fileName = _fileName;
            sequenceNumber = _sequenceNumber;
        }

    }
}