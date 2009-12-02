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
    public class Assembly : NavigatorState {
        private Assembly() { m_NbAttributes = 2;  }
        public static readonly Assembly Instance = new Assembly();

        private static AssemblyDefinition Cast(Navigator n) { return (AssemblyDefinition) n.Current; }

        public override string Name(Navigator n) {
            switch (n.AttributesIndex) {
                case -1: return AssemblyName;
                case 0: return "Name";
                case 1: return "FullName";
                default: return string.Empty;
            }
        }
        public override string Value(Navigator n) {
            switch (n.AttributesIndex) {
                case -1: case 0: return Cast(n).Name.Name;
                case 1: return Cast(n).Name.FullName;
                default: return string.Empty;
            }
        }

        public override bool MoveToNext(Navigator n) { return false; } // No next sibling

        public override bool MoveToFirstChild(Navigator n) {
            return GoToFirstChild(n, Cast(n).CustomAttributes, Cast(n).MainModule.Types);
        }

        public override void Remove(Navigator n) {
            throw new NotSupportedException("Cannot remove the assembly itself");
        }
    }
}
