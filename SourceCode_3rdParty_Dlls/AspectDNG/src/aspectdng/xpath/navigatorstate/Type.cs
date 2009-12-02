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
    // A Type has 
    // - 4 attributes (Namespace, Name, FullName, BaseType)
    // - 4 kinds of possible children (Field, Property, Constructor, Method)
    public class Type : NavigatorState {
        private Type() { m_NbAttributes = 5; }
        public static readonly Type Instance = new Type();

        private static TypeDefinition Cast(Navigator n) { return (TypeDefinition)n.Current; }

        public override string Name(Navigator n) {
            switch (n.AttributesIndex) {
                case -1: return TypeName;
                case 0: return "Namespace";
                case 1: return "Name";
                case 2: return "FullName";
                case 3: return "BaseType";
                case 4: return "Attributes";
                default: return string.Empty;
            }
        }
        public override string Value(Navigator n) {
            switch (n.AttributesIndex) {
                case -1: return n.Current.ToString();
                case 0: return Cast(n).Namespace;
                case 1: return Cast(n).Name;
                case 2: return Cast(n).FullName;
                case 3: return Cast(n).BaseType != null ? Cast(n).BaseType.ToString() : string.Empty;
                case 4: return Cast(n).Attributes.ToString();
                default: return string.Empty;
            }
        }

        public override bool MoveToNext(Navigator n) {
            return GoToNext(n, Cast(n).Module.Types);
        }

        public override bool MoveToFirstChild(Navigator n) {
            return GoToFirstChild(n, Cast(n).CustomAttributes, Cast(n).Fields, Cast(n).Properties, Cast(n).Constructors, Cast(n).Methods);
        }

        public override void Remove(Navigator n) {
            TypeDefinition type = (TypeDefinition)n.Current;
            TypeDefinition declaringType = (TypeDefinition)type.DeclaringType;

            if (declaringType != null) 
                declaringType.NestedTypes.Remove(type);
            else
                type.Module.Types.Remove(type);
        }
    }
}
