using O2.Kernel.Interfaces.CIR;
using O2.Kernel.Interfaces.Messages;
using O2.Kernel.InterfacesBaseImpl;

namespace O2.Kernel.InterfacesBaseImpl
{
    class KM_CirAction : KO2Message, IM_CirAction
    {
        public IM_CirActions CirAction { get; set; }
        public ICirData CirData { get; set; }
        public ICirDataAnalysis CirDataAnalysis { get; set; }

        public static KM_CirAction setCirData(ICirData cirData)
        {
            var kmCirAction = new KM_CirAction
                                  {
                                      CirAction = IM_CirActions.setCirData,
                                      CirData = cirData
                                  };
            return kmCirAction;
        }

        public static KM_CirAction setCirDataAnalysis(ICirDataAnalysis cirDataAnalysis)
        {
            var kmCirAction = new KM_CirAction
            {
                CirAction = IM_CirActions.setCirDataAnalysis,
                CirDataAnalysis = cirDataAnalysis
            };
            return kmCirAction;
        }
    }
}