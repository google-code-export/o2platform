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

namespace DotNetGuru.AspectDNG.XPath.NavigatorState {
    // A Variable has 
    // - 2 attributes (Type, Name)
    public class Variable : NavigatorState {
        private Variable() { m_NbAttributes = 2; }
        public static readonly Variable Instance = new Variable();

        private static VariableDefinition Cast(Navigator n) { return (VariableDefinition)n.Current; }

        public override string Name(Navigator n) {
            switch (n.AttributesIndex) {
                case -1: return VariableName;
                case 0: return "Type";
                case 1: return "Name";
                default: return string.Empty;
            }
        }
        public override string Value(Navigator n) {
            switch (n.AttributesIndex) {
                case -1: case 0: return Cast(n).VariableType.ToString();
                case 1: return Cast(n).Name;
                default: return string.Empty;
            }
        }

        public override bool MoveToNext(Navigator n) {
            MethodBody body = ((MethodDefinition)n.Parent).Body;
            return GoToNext(n, body.Variables, body.Instructions);
        }

        public override void Remove(Navigator n) {
            throw new NotImplementedException();
        }
    }
}
