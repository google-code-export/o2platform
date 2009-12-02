/* 
	Author 	: Thomas GIL (DotNetGuru.org)
	Date 	: 17 september 2006
	License : Public Domain
*/
using System;
using System.Collections;
using System.Collections.Specialized;
using System.Xml;
using System.Xml.XPath;
using DotNetGuru.AspectDNG.XPath;
using Mono.Cecil;
using Mono.Cecil.Cil;

namespace DotNetGuru.AspectDNG.XPath {

    // This custom navigator can only work on "Adatpers"
    // It only presents XML Elements and XML Attributes

    public class Navigator : XPathNavigator {
        private const int Depth = 5;

        public object Root; // Cecil root of this object graph. This may be any Cecil type

        public int AttributesIndex;

        private int m_Index;
        private int[] m_ChildrenIndex = new int[Depth];
        private object[] m_Parents = new object[Depth];

        public int Size { get { return m_Index; } }
        public void Push(int i, object newParent) {
            m_ChildrenIndex[++m_Index] = i;
            m_Parents[m_Index] = newParent;
        }
        public bool Pop() { 
            bool result = m_Index > 0;
            if (result)
                --m_Index;
            return result;
        }
        public int ChildIndex { get { return m_ChildrenIndex[m_Index]; } }
        public int IncrementChildIndex() { return ++m_ChildrenIndex[m_Index]; }
        public void ResetChildIndex() { m_ChildrenIndex[m_Index] = 0; }
        public object Parent { get { return m_Parents[m_Index - 1]; } }
        public object Current { 
            get { return m_Parents[m_Index]; }
            set { m_Parents[m_Index] = value; }
        }

        public NavigatorState.NavigatorState State {
            get {
                NavigatorState.NavigatorState result = null;
                object newParent = m_Parents[m_Index];
                if (newParent == null) result = NavigatorState.Root.Instance;
                else if (newParent is Instruction) result = NavigatorState.Instruction.Instance;
                else if (newParent is VariableDefinition) result = NavigatorState.Variable.Instance;
                else if (newParent is CustomAttribute) result = NavigatorState.Attribute.Instance;
                else if (newParent is TypeDefinition) result = NavigatorState.Type.Instance;
                else if (newParent is FieldDefinition) result = NavigatorState.Field.Instance;
                else if (newParent is PropertyDefinition) result = NavigatorState.Property.Instance;
                else if (newParent is MethodDefinition) {
                    if (((MethodDefinition)newParent).IsConstructor) result = NavigatorState.Constructor.Instance;
                    else return result = NavigatorState.Method.Instance;
                } else if (newParent is AssemblyDefinition) result = NavigatorState.Assembly.Instance;
                else throw new NotSupportedException("this kind of element isn't supported : " + newParent.GetType());
                return result;
            }
        }

        public Navigator(object rootCecilObject) {
            Root = rootCecilObject;
            MoveToRoot();
        }
        private Navigator() { }

        public override void MoveToRoot() {
            m_Index = 0;
            AttributesIndex = -1;
        }

        public override XPathNavigator Clone() {
            Navigator newNav = new Navigator();
            newNav.MoveTo(this);
            return newNav;
        }

        public override bool MoveTo(XPathNavigator other) {
            Navigator otherNav = (Navigator)other;
            Root = otherNav.Root;
            AttributesIndex = otherNav.AttributesIndex;

            m_Index = otherNav.m_Index;
            for (int i = 0; i <= m_Index; i++) {
                m_ChildrenIndex[i] = otherNav.m_ChildrenIndex[i];
                m_Parents[i] = otherNav.m_Parents[i];
            }
            return true;
        }

        public override XPathNodeType NodeType {
            get {
                return AttributesIndex > -1 ? XPathNodeType.Attribute :
                    Size == 0 ? XPathNodeType.Root :
                    XPathNodeType.Element;
            }
        }

        public override string BaseURI { get { return string.Empty; } }
        public override string LocalName { get { return Name; } }
        public override XmlNameTable NameTable { get { return Names.Instance; } }
        public override string XmlLang { get { return string.Empty; } }
        public override string GetNamespace(string localName) { return string.Empty; }
        public override string NamespaceURI { get { return string.Empty; } }
        public override string Prefix { get { return string.Empty; } }
        public override bool IsEmptyElement { get { return !HasChildren; } }

        public override bool IsDescendant(XPathNavigator nav) {
            if (nav == null) return false;
            
            Navigator otherNav = (Navigator)nav;
            if (otherNav.Size < Size) return false;

            Navigator navClone = (Navigator)nav.Clone();
            while (true) {
                if (IsSamePosition(navClone)) return true;
                if (!navClone.MoveToParent()) break;
            }
            return false;
        }

        public override bool IsSamePosition(XPathNavigator other) {
            if (other == null) return false;
            Navigator otherNav = (Navigator)other;
            return Current == otherNav.Current && // Same element
                AttributesIndex == otherNav.AttributesIndex; // Same attribute
        }

        public override bool MoveToFirstNamespace(XPathNamespaceScope scope) { return false; }
        public override bool MoveToId(string id) { return false; }
        public override bool MoveToNamespace(string name) { return false; }
        public override bool MoveToNextNamespace(XPathNamespaceScope scope) { return false; }

        public override bool MoveToParent() {
            bool result = false;
            if (AttributesIndex > -1) {
                AttributesIndex = -1;
                result = true;
            } else result = Pop();
            return result;
        }

        /************* Delegated methods ********************/

        // Those method aren't required to run xpath & xslt queries
        public override string GetAttribute(string localName, string namespaceURI) { throw new NotSupportedException(); }
        public override bool MoveToAttribute(string localName, string namespaceURI) { throw new NotSupportedException(); }
        public override bool MoveToFirst() { throw new NotSupportedException(); }
        public override bool MoveToPrevious() { throw new NotSupportedException(); }

        // Context info
        public override bool HasAttributes { get { return true; } }
        public override bool HasChildren { get { return true; } }

        public override string Name { get { return State.Name(this); } }
        public override string Value { get { return State.Value(this); } }

        // Attributes
        public override bool MoveToFirstAttribute() { return State.MoveToFirstAttribute(this); }
        public override bool MoveToNextAttribute() { return State.MoveToNextAttribute(this); }

        // Elements
        public override bool MoveToFirstChild() {
            if (AttributesIndex > -1) return false; // Attributes don't have children
            return State.MoveToFirstChild(this);
        }

        public override bool MoveToNext() { return State.MoveToNext(this); }

        // Selection methods (inherited, but we add some extra xpath methods)
        public override XPathNodeIterator Select(string xpath) {
            XPathExpression xpathExpression = Compile(xpath);
            xpathExpression.SetContext(Context.Instance);
            return base.Select(xpathExpression);
        }

        private HybridDictionary m_Cache = new HybridDictionary();
        public ICollection SelectList(string xpath) {
            ArrayList result = (ArrayList)m_Cache[xpath];
            if (result == null) {
                XPathNodeIterator iterator = Select(xpath);
                m_Cache[xpath] = result = new ArrayList();
                while (iterator.MoveNext()) 
                    result.Add(iterator.Current.Clone());
            }
            return result;
        }

        // Cecil Added value
        public void Remove() { State.Remove(this); }
    }
}
