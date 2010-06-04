namespace Amazon.ElasticMapReduce.Model
{
    using System;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://elasticmapreduce.amazonaws.com/doc/2009-03-31", IsNullable=false)]
    public class KeyValue
    {
        private string keyField;
        private string valueField;

        public bool IsSetKey()
        {
            return (this.keyField != null);
        }

        public bool IsSetValue()
        {
            return (this.valueField != null);
        }

        public KeyValue WithKey(string key)
        {
            this.keyField = key;
            return this;
        }

        public KeyValue WithValue(string value)
        {
            this.valueField = value;
            return this;
        }

        [XmlElement(ElementName="Key")]
        public string Key
        {
            get
            {
                return this.keyField;
            }
            set
            {
                this.keyField = value;
            }
        }

        [XmlElement(ElementName="Value")]
        public string Value
        {
            get
            {
                return this.valueField;
            }
            set
            {
                this.valueField = value;
            }
        }
    }
}

