using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using O2.Kernel.Interfaces.CIR;

namespace O2.Core.CIR.CirObjects
{
    [Serializable]
    public class CirFunctionParameter : ICirFunctionParameter
    {
        public string ParameterName { get; set; }
        public string ParameterType { get; set; }
        public string Constant { get; set; }
        public bool HasConstant { get; set; }
        public bool HasDefault { get; set; }
        public string Method { get; set; }
        public bool IsTainted { get; set; }
    }
}
