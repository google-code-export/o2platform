/* 
	Author 	: Thomas GIL (DotNetGuru.org)
	Date 	: 17 september 2006
	License : Public Domain
*/
using System;
using System.Collections;
using System.Reflection;
using System.Xml;
using System.Xml.XPath;
using DotNetGuru.AspectDNG.XPath;
using Mono.Cecil;
using Mono.Cecil.Cil;

namespace DotNetGuru.AspectDNG.XPath.NavigatorState {
    // This custom navigator can only work on Cecil Objects
    // It only presents XML Elements and XML Attributes
    public abstract class NavigatorState {
        protected static readonly string RootName;
        protected static readonly string AssemblyName;
        protected static readonly string TypeName;
        protected static readonly string AttributeName;
        protected static readonly string FieldName;
        protected static readonly string PropertyName;
        protected static readonly string ConstructorName;
        protected static readonly string MethodName;
        protected static readonly string VariableName;
        protected static readonly string InstructionName;
        protected static readonly string CallName;
        protected static readonly string LdFldName;
        protected static readonly string StFldName;

        static NavigatorState() {
            RootName = Names.Instance.Add("/");
            AssemblyName = Names.Instance.Add("Assembly");
            TypeName = Names.Instance.Add("Type");
            AttributeName = Names.Instance.Add("Attribute");
            FieldName = Names.Instance.Add("Field");
            PropertyName = Names.Instance.Add("Property");
            ConstructorName = Names.Instance.Add("Constructor");
            MethodName = Names.Instance.Add("Method");
            VariableName = Names.Instance.Add("Variable");
            InstructionName = Names.Instance.Add("Instruction");
            CallName = Names.Instance.Add("Call");
            LdFldName = Names.Instance.Add("LdFld");
            StFldName = Names.Instance.Add("StFld");
        }

        protected int m_NbAttributes;

        // Navigate across attributes
        public bool MoveToFirstAttribute(Navigator n) {
            bool result = m_NbAttributes > 0;
            if (result) 
                n.AttributesIndex = 0;
            return result;
        }
        public bool MoveToNextAttribute(Navigator n) {
            bool result = n.AttributesIndex < m_NbAttributes - 1;
            if (result)
                ++n.AttributesIndex;
            return result;
        }

        // Navigate across elements
        public abstract string Name(Navigator n);
        public abstract string Value(Navigator n);

        public virtual bool MoveToFirstChild(Navigator n) { return false; }
        public virtual bool MoveToNext(Navigator n) { return false; }


        protected bool GoToNext(Navigator n, IIndexedCollection siblings, params IIndexedCollection[] notSiblingsList) {
            bool result = false;

            if (n.ChildIndex < siblings.Count - 1) {
                n.Current = siblings[n.IncrementChildIndex()];
                result = true;
            } else foreach (IIndexedCollection notSiblings in notSiblingsList)
                    if (notSiblings.Count > 0) {
                        n.Current = notSiblings[0];
                        n.ResetChildIndex();
                        result = true;
                        break;
                    }
            return result;
        }

        protected bool GoToFirstChild(Navigator n, object current) {
            n.AttributesIndex = -1;
            n.Push(0, current);
            return true;
        }

        protected bool GoToFirstChild(Navigator n, params IIndexedCollection[] collections) {
            foreach (IIndexedCollection collection in collections)  
                if (collection.Count > 0)
                    return GoToFirstChild(n, collection[0]);
            return false;
        }

        public abstract void Remove(Navigator n);
    }
}
