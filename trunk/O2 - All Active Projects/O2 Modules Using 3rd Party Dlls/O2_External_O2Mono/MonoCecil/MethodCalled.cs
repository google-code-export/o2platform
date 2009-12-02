using Mono.Cecil;
using Mono.Cecil.Cil;

namespace O2.External.O2Mono.MonoCecil
{
    public class MethodCalled
    {
        public IMemberReference memberReference { get; set;}
        public SequencePoint sequencePoint { get; set; }

        public MethodCalled(IMemberReference _memberReference, SequencePoint _sequencePoint)
        {
            memberReference = _memberReference;
            sequencePoint = _sequencePoint;
        }
    }
}