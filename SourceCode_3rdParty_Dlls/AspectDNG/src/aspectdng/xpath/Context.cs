/* 
	Author 	: Thomas GIL (DotNetGuru.org)
	Date 	: 17 september 2006
	License : Public Domain
*/
using System;
using System.Collections;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Xsl;

using DotNetGuru.AspectDNG.Util;

namespace DotNetGuru.AspectDNG.XPath {
	public class Context : XsltContext {
		public static readonly Context Instance = new Context();

		private Context() : base(new NameTable()) {}

		public override int CompareDocument(string baseUri, string nextbaseUri) { return 0; }
		public override bool PreserveWhitespace(XPathNavigator node) { return false; }
		public override IXsltContextVariable ResolveVariable(string prefix, string name) { return null; }
		public override bool Whitespace { get { return false; } }
		public override string LookupNamespace(string prefix) { return string.Empty; }

        private static IXsltContextFunction m_MatchFct = new MatchRegExpFunction();
        private static IXsltContextFunction m_CanCastTo = new CanCastToFunction();
        public override IXsltContextFunction ResolveFunction(string prefix, string name, XPathResultType[] ArgTypes) {
            switch (name) {
                case "match":
                    return m_MatchFct;
                case "canCastTo":
                    return m_CanCastTo;
                default:
                    throw new NotSupportedException();
            }
		}
	}

	// match() has two signatures:
	// - match('expression')
	// - match(context, 'expression')
    class MatchRegExpFunction : IXsltContextFunction {
        public XPathResultType[] ArgTypes { get { return null; } }
        public XPathResultType ReturnType { get { return XPathResultType.Boolean; } }
        public int Minargs { get { return 1; } }
        public int Maxargs { get { return 2; } }

        public object Invoke(XsltContext xsltContext, object[] args, XPathNavigator docContext) {
            bool result = false;
            try {
                if (args.Length == 2) {
                    XPathNodeIterator it = (XPathNodeIterator)args[0];
                    if (it.MoveNext()) {
                        string pseudoRegexp = (string)args[1];
                        result = SimpleRegex.IsMatch(it.Current, pseudoRegexp);
                    }
                } else {
                    string pseudoRegexp = (string)args[0];
                    result = SimpleRegex.IsMatch(docContext, pseudoRegexp);
                }
            } catch (Exception e) {
                Console.WriteLine("Regular Expression function failed : \n" + e);
            }
            return result;
        }
    }

    // canCastTo() has two signatures:
    // - canCastTo('Fully.Qualified.Type')
    // - canCastTo(context, 'Fully.Qualified.Type')
    // this function can only be applied to contexts that contain type information (type reference or type definition)
    class CanCastToFunction : IXsltContextFunction {
        public XPathResultType[] ArgTypes { get { return null; } }
        public XPathResultType ReturnType { get { return XPathResultType.Boolean; } }
        public int Minargs { get { return 1; } }
        public int Maxargs { get { return 2; } }

        public object Invoke(XsltContext xsltContext, object[] args, XPathNavigator docContext) {
            bool result = false;
            try {
                Navigator nav = null;
                string toTypeName = null;

                if (args.Length == 2) {
                    XPathNodeIterator it = (XPathNodeIterator)args[0];
                    if (it.MoveNext()) {
                        toTypeName = (string)args[1];
                        nav = (Navigator)it.Current;
                    }
                } else {
                    toTypeName = (string)args[0];
                    nav = (Navigator)docContext;
                }

                if (nav.Current is Mono.Cecil.TypeReference) {
                    Mono.Cecil.TypeReference reference = (Mono.Cecil.TypeReference)nav.Current;
                    Type fromType = Type.GetType(reference.FullName);
                    Type toType = Type.GetType(toTypeName);

                    if (fromType != null && toType != null)
                        result = toType.IsAssignableFrom(fromType);
                }

            } catch (Exception e) {
                Console.WriteLine("CanCastTo function failed : \n" + e);
            }
            return result;
        }
    }
}
