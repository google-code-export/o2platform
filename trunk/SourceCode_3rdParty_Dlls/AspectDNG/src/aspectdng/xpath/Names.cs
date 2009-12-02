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

namespace DotNetGuru.AspectDNG.XPath {
    public class Names : XmlNameTable {
        public static readonly Names Instance = new Names();
        private Names() { }

        private StringDictionary m_Collection = new StringDictionary();

        public override string Get(string key) {
            return (string)m_Collection[key];
        }

        public override string Add(string key) {
            string result = m_Collection[key];
            if (result == null) m_Collection[key] = result = key;
            return result;
        }

        public override string Add(char[] tab, int start, int len) {
            return Add(new string(tab, start, len));
        }

        public override string Get(char[] tab, int start, int len) {
            return Get(new string(tab, start, len));
        }
    }
}
