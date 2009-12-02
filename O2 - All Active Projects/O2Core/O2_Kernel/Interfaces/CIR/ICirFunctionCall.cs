using System;


namespace O2.Kernel.Interfaces.CIR
{
    public interface ICirFunctionCall
    {
        ICirFunction cirFunction { get; set; }
        int lineNumber { get; set; }
        string fileName { get; set; }
        int sequenceNumber { get; set; }
        String sourceCodeText { get; set; }
    }
}
