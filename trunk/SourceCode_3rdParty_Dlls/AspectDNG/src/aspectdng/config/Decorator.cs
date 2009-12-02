/* 
	Author 	: Thomas GIL (DotNetGuru.org)
	Date 	: 17 september 2006
	License : Public Domain
*/
using System;
using System.Collections;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Schema;
using System.Reflection;

namespace DotNetGuru.AspectDNG.Config {
	public class Decorator {
		private static XmlSchema Schema;

		static Decorator(){
			Schema = XmlSchema.Read(new XmlTextReader
                (Assembly.GetExecutingAssembly().GetManifestResourceStream("AspectDNG.xsd")), null);
		}

        private static XmlValidatingReader GetXmlConfigReader(string path) {
            XmlValidatingReader reader = new XmlValidatingReader(new XmlTextReader(path));
            reader.Schemas.Add(Schema);
            return reader;
        }

        private XPathNavigator m_Navigator;

		public Decorator(XPathNavigator nav) { m_Navigator = nav; }
		public Decorator(XPathDocument doc) : this(doc.CreateNavigator()){ }
		public Decorator(XmlReader reader) : this(new XPathDocument(reader)) { }
		public Decorator(string path) : this(GetXmlConfigReader(path)){}

		public void GoTo(string xpath){
			XPathNodeIterator it = m_Navigator.Select(xpath);
			if (it.MoveNext())
				m_Navigator = it.Current;
		}

		public string this[string xpath]{
			get{
				string result = null;
				XPathNodeIterator it = m_Navigator.Select(xpath);
				if (it.MoveNext())
					result = it.Current.Value;
				return result;
			}
		}

		public string[] GetStrings(string xpath){
			string[] result = null;
			XPathNodeIterator it = m_Navigator.Select(xpath);
			result = new string[it.Count];
			int i=0;
			while (it.MoveNext())
				result[i++] = it.Current.Value;
			return result;
		}

		public IList GetDecorators(string xpath){
			IList result = new ArrayList();
			XPathNodeIterator it = m_Navigator.Select(xpath);
			while (it.MoveNext())
				result.Add(new Decorator(it.Current.Clone()));
			return result;
		}

        public string Name { get { return m_Navigator.Name; } }
    }
}
