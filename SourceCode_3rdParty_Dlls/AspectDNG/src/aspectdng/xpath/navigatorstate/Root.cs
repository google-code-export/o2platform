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

namespace DotNetGuru.AspectDNG.XPath.NavigatorState {
    public class Root : NavigatorState {
        private Root() { }
        public static readonly Root Instance = new Root();

        public override string Name(Navigator n) { return RootName; }
        public override string Value(Navigator n) { return string.Empty; }

        public override bool MoveToFirstChild(Navigator n) {
            return GoToFirstChild(n, n.Root);
        }

        public override void Remove(Navigator n) {
            throw new NotSupportedException("Cannot remove the root of an XPath tree");
        }
    }
}
