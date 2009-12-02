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
    // A Method has 
    // - 5 attributes (Signature, Name, ReturnType, IsStatic, IsAbstract)
    public class Method : NavigatorState {
        private Method() { m_NbAttributes = 6; }
        public static readonly Method Instance = new Method();

        private static MethodDefinition Cast(Navigator n) { return (MethodDefinition)n.Current; }

        public override string Name(Navigator n) {
            switch (n.AttributesIndex) {
                case -1: return MethodName;
                case 0: return "Signature";
                case 1: return "Name";
                case 2: return "ReturnType";
                case 3: return "IsStatic";
                case 4: return "IsAbstract";
                case 5: return "Attributes";
                default: return string.Empty;
            }
        }
        public override string Value(Navigator n) {
            switch (n.AttributesIndex) {
                case -1: case 0: return Cast(n).ToString();
                case 1: return Cast(n).Name;
                case 2: return Cast(n).ReturnType.ReturnType.FullName;
                case 3: return Cast(n).IsStatic.ToString();
                case 4: return Cast(n).IsAbstract.ToString();
                case 5: return Cast(n).Attributes.ToString();
                default: return string.Empty;
            }
        }

        public override bool MoveToNext(Navigator n) {
            TypeDefinition type = Cast(n).DeclaringType as TypeDefinition;
            return GoToNext(n, type.Methods);
        }

        public override bool MoveToFirstChild(Navigator n) {
            return Cast(n).Body == null ? GoToFirstChild(n, Cast(n).CustomAttributes)
                : GoToFirstChild(n, Cast(n).CustomAttributes, Cast(n).Body.Variables, Cast(n).Body.Instructions);
        }

        public override void Remove(Navigator n) {
            ((TypeDefinition)n.Parent).Methods.Remove(Cast(n));
        }
    }
}
