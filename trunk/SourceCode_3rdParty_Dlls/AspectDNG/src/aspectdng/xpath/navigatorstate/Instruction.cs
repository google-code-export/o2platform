/* 
	Author 	: Thomas GIL (DotNetGuru.org)
	Date 	: 17 september 2006
	License : Public Domain
*/
using System;
using System.Collections;
using System.Xml;
using System.Xml.XPath;
using DotNetGuru.AspectDNG.XPath;
using Mono.Cecil;
using Mono.Cecil.Cil;
using InstructionDefinition = Mono.Cecil.Cil.Instruction;

namespace DotNetGuru.AspectDNG.XPath.NavigatorState {
    // A Instruction has 
    // - 2 attributes (OpCode, Operand)
    public class Instruction : NavigatorState {
        private Instruction() { m_NbAttributes = 2; }
        public static readonly Instruction Instance = new Instruction();

        private static InstructionDefinition Cast(Navigator n) { return (InstructionDefinition)n.Current; }

        public override string Name(Navigator n) {
            InstructionDefinition instruction = Cast(n);
            string name;
            if (instruction.OpCode == OpCodes.Call || instruction.OpCode == OpCodes.Callvirt ||
                instruction.OpCode == OpCodes.Newobj || instruction.OpCode == OpCodes.Calli)
                name = CallName;
            //else if (instruction.OpCode == OpCodes.Ldfld)
            //    name = LdFldName;
            //else if (instruction.OpCode == OpCodes.Stfld)
            //    name = StFldName;
            else if (instruction.OpCode == OpCodes.Ldfld || instruction.OpCode == OpCodes.Ldsfld)
                name = LdFldName;
            else if (instruction.OpCode == OpCodes.Stfld || instruction.OpCode == OpCodes.Stsfld)
                name = StFldName;
            else
                name = InstructionName;
            switch (n.AttributesIndex) {
                case -1: return name;
                case 0: return "OpCode";
                case 1: return "Operand";
                default: return string.Empty;
            }
        }
        public override string Value(Navigator n) {
            switch (n.AttributesIndex) {
                case 0: return Cast(n).OpCode.Name;
                case -1:case 1: return Cast(n).Operand != null ? Cast(n).Operand.ToString() : string.Empty;
                default: return string.Empty;
            }
        }

        public override bool MoveToNext(Navigator n) {
            bool result = false;
            InstructionDefinition instruction = Cast(n);
            if (instruction.Next != null) {
                n.Current = instruction.Next;
                result = true;
            }
            return result;
        }

        public override void Remove(Navigator n) {
            throw new NotImplementedException();
        }
    }
}
