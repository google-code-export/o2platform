using O2.Core.CIR.CirObjects;
using O2.Core.CIR.CirObjects;
using O2.Core.CIR.CirObjects;

namespace O2.Core.CIR.CirUtils
{
    public class CirCopy
    {
        public static CirClass copy(CirClass cirClass)
        {
            var newCirClass = new CirClass
                                  {
                                      SymbolDef = cirClass.SymbolDef,
                                      Signature = cirClass.Signature,
                                      bClassHasMethodsWithControlFlowGraphs =
                                          cirClass.bClassHasMethodsWithControlFlowGraphs,
                                      ///TODO
                                      /// need to change this so that newCirClass gets a new copy of these dictionaries
                                      dSuperClasses = cirClass.dSuperClasses,
                                      dIsSuperClassedBy = cirClass.dIsSuperClassedBy,
                                      dField_Class = cirClass.dField_Class,
                                      dField_Member = cirClass.dField_Member,
                                      lcmIsUsedByMethod_Argument = cirClass.lcmIsUsedByMethod_Argument,
                                      lcmIsUsedByMethod_ReturnType = cirClass.lcmIsUsedByMethod_ReturnType,
                                      lcmIsUsedByMethod_InternalVariable = cirClass.lcmIsUsedByMethod_InternalVariable
                                  };
            return newCirClass;
        }
    }
}