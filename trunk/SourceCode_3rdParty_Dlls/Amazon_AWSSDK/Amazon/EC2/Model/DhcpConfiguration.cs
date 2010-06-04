namespace Amazon.EC2.Model
{
    using System;
    using System.Collections.Generic;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://ec2.amazonaws.com/doc/2009-11-30/", IsNullable=false)]
    public class DhcpConfiguration
    {
        private string keyField;
        private List<string> valueField;

        public bool IsSetKey()
        {
            return (this.keyField != null);
        }

        public bool IsSetValue()
        {
            return (this.Value.Count > 0);
        }

        public DhcpConfiguration WithKey(string key)
        {
            this.keyField = key;
            return this;
        }

        public DhcpConfiguration WithValue(params string[] list)
        {
            foreach (string str in list)
            {
                this.Value.Add(str);
            }
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
        public List<string> Value
        {
            get
            {
                if (this.valueField == null)
                {
                    this.valueField = new List<string>();
                }
                return this.valueField;
            }
            set
            {
                this.valueField = value;
            }
        }
    }
}

