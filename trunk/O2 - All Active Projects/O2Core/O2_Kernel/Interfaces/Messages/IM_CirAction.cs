using O2.Kernel.Interfaces.CIR;

namespace O2.Kernel.Interfaces.Messages
{
    public enum IM_CirActions 
    {
        setCirData,
        setCirDataAnalysis
        //newData        
    }
    public interface IM_CirAction : IO2Message
    {
        IM_CirActions CirAction { get; set; }
        ICirData CirData { get; set; }
        ICirDataAnalysis CirDataAnalysis { get; set; }
    }
}
