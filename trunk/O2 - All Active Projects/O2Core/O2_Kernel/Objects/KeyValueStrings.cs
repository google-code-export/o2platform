using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace O2.Kernel.Objects
{
    // this currently doesn't suport unique Keys    
    public class KeyValueStrings
    {
        public List<KeyValueString> Items { get; set; }

        public KeyValueStrings()
        {
            Items = new List<KeyValueString>();
        }

        public KeyValueStrings add(string key, string value)
        {
            Items.Add(new KeyValueString(key, value));
            return this;
        }
        public int Count
        {
            get { return Items.Count; }
        }


    }

    public class KeyValueString
    {
        [XmlAttribute]
        public string Key { get; set; }
        [XmlAttribute]
        public string Value { get; set; }

        public KeyValueString()
        {
            Key = "";
            Value = "";
        }

        public KeyValueString(string key, string value)
        {
            Key = key;
            Value = value;
        }
    }
}
