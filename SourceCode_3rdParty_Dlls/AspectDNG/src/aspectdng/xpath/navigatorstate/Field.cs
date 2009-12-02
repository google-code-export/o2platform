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
    // A Field has 
    // - 4 attributes (Signature, Type, Name, IsStatic)
    public class Field : NavigatorState {
        private Field() { m_NbAttributes = 5; }
        public static readonly Field Instance = new Field();

        private static FieldDefinition Cast(Navigator n) { return (FieldDefinition)n.Current; }

        public override string Name(Navigator n) {
            switch (n.AttributesIndex) {
                case -1: return FieldName;
                case 0: return "Signature";
                case 1: return "Type";
                case 2: return "Name";
                case 3: return "IsStatic";
                case 4: return "Attributes";
                default: return string.Empty;
            }
        }
        public override string Value(Navigator n) {
            switch (n.AttributesIndex) {
                case -1: case 0: return Cast(n).ToString();
                case 1: return Cast(n).FieldType.ToString();
                case 2: return Cast(n).Name;
                case 3: return Cast(n).IsStatic.ToString();
                case 4: return Cast(n).Attributes.ToString();
                default: return string.Empty;
            }
        }

        public override bool MoveToNext(Navigator n) {
            TypeDefinition type = Cast(n).DeclaringType as TypeDefinition;
            return GoToNext(n, type.Fields, type.Properties, type.Constructors, type.Methods);
        }

        public override bool MoveToFirstChild(Navigator n) {
            return GoToFirstChild(n, Cast(n).CustomAttributes);
        }

        public override void Remove(Navigator n) {
            ((TypeDefinition)n.Parent).Fields.Remove(Cast(n));
        }
    }
}
