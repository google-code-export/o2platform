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
    // A Attribute has 
    // - 1 attribute (Constructor)
    public class Attribute : NavigatorState {
        private Attribute() { m_NbAttributes = 1; }
        public static readonly Attribute Instance = new Attribute();

        private static CustomAttribute Cast(Navigator n) { return (CustomAttribute)n.Current; }

        public override string Name(Navigator n) {
            switch (n.AttributesIndex) {
                case -1: return AttributeName;
                case 0: return "Constructor";
                default: return string.Empty;
            }
        }
        public override string Value(Navigator n) {
            switch (n.AttributesIndex) {
                case -1:
                case 0: return Cast(n).Constructor.ToString();
                default: return string.Empty;
            }
        }

        public override bool MoveToNext(Navigator n) {
            bool result = false;
            object parent = n.Parent;
            if (parent is FieldDefinition) { // Next attribute in field 
                FieldDefinition field = (FieldDefinition)parent;
                result = GoToNext(n, field.CustomAttributes);
            } else if (parent is PropertyDefinition) { // Next attribute in property
                PropertyDefinition property = (PropertyDefinition)parent;
                result = GoToNext(n, property.CustomAttributes);
            } else if (parent is AssemblyDefinition) { // Next attribute in assembly or first type
                AssemblyDefinition assembly = (AssemblyDefinition)parent;
                result = GoToNext(n, assembly.CustomAttributes, assembly.MainModule.Types);
            } else if (parent is TypeDefinition) {// Next attribute in type or first field / property / constructor / method
                TypeDefinition type = (TypeDefinition)parent;
                result = GoToNext(n, type.CustomAttributes, type.Fields, type.Properties, type.Constructors, type.Methods);
            } else if (parent is MethodDefinition) { // Next attribute in operation or first variable / instruction
                MethodDefinition operation = (MethodDefinition)parent;
                result = (operation.Body == null) ? GoToNext(n, operation.CustomAttributes) :
                    GoToNext(n, operation.CustomAttributes, operation.Body.Variables, operation.Body.Instructions);
            } else throw new NotSupportedException("this kind of parent should not occur here");
            return result;
        }

        public override void Remove(Navigator n) {
            ((ICustomAttributeProvider) n.Parent).CustomAttributes.Remove(Cast(n));
        }
    }
}
