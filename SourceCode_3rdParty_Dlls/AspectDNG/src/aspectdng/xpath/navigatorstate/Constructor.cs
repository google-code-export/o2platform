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

namespace DotNetGuru.AspectDNG.XPath.NavigatorState {
    // A Constructor has 
    // - 3 attributes (Signature, Name, IsStatic)
    public class Constructor : NavigatorState {
        private Constructor() { m_NbAttributes = 4; }
        public static readonly Constructor Instance = new Constructor();

        private static MethodDefinition Cast(Navigator n) { return (MethodDefinition)n.Current; }

        public override string Name(Navigator n) {
            switch (n.AttributesIndex) {
                case -1: return ConstructorName;
                case 0: return "Signature";
                case 1: return "Name";
                case 2: return "IsStatic";
                case 3: return "Attributes";
                default: return string.Empty;
            }
        }
        public override string Value(Navigator n) {
            switch (n.AttributesIndex) {
                case -1: case 0: return Cast(n).ToString();
                case 1: return Cast(n).Name;
                case 2: return Cast(n).IsStatic.ToString();
                case 3: return Cast(n).Attributes.ToString();
                default: return string.Empty;
            }
        }

        public override bool MoveToNext(Navigator n) {
            TypeDefinition type = Cast(n).DeclaringType as TypeDefinition;
            return GoToNext(n, type.Constructors, type.Methods);
        }

        public override bool MoveToFirstChild(Navigator n) {
            return Cast(n).Body == null ? GoToFirstChild(n, Cast(n).CustomAttributes) 
                : GoToFirstChild(n, Cast(n).CustomAttributes, Cast(n).Body.Variables, Cast(n).Body.Instructions);
        }

        public override void Remove(Navigator n) {
            ((TypeDefinition)n.Parent).Constructors.Remove(Cast(n));
        }
    }
}
