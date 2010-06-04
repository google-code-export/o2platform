namespace Amazon.SimpleDB.Model
{
    using System;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://sdb.amazonaws.com/doc/2009-04-15/", IsNullable=false)]
    public class Attribute
    {
        private string nameEncodingField;
        private string nameField;
        private string valueEncodingField;
        private string valueField;

        public bool IsSetName()
        {
            return (this.nameField != null);
        }

        public bool IsSetNameEncoding()
        {
            return (this.nameEncodingField != null);
        }

        public bool IsSetValue()
        {
            return (this.valueField != null);
        }

        public bool IsSetValueEncoding()
        {
            return (this.valueEncodingField != null);
        }

        public Amazon.SimpleDB.Model.Attribute WithName(string name)
        {
            this.nameField = name;
            return this;
        }

        public Amazon.SimpleDB.Model.Attribute WithNameEncoding(string nameEncoding)
        {
            this.nameEncodingField = nameEncoding;
            return this;
        }

        public Amazon.SimpleDB.Model.Attribute WithValue(string value)
        {
            this.valueField = value;
            return this;
        }

        public Amazon.SimpleDB.Model.Attribute WithValueEncoding(string valueEncoding)
        {
            this.valueEncodingField = valueEncoding;
            return this;
        }

        [XmlElement(ElementName="Name")]
        public string Name
        {
            get
            {
                return this.nameField;
            }
            set
            {
                this.nameField = value;
            }
        }

        [XmlElement(ElementName="NameEncoding")]
        public string NameEncoding
        {
            get
            {
                return this.nameEncodingField;
            }
            set
            {
                this.nameEncodingField = value;
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

        [XmlElement(ElementName="ValueEncoding")]
        public string ValueEncoding
        {
            get
            {
                return this.valueEncodingField;
            }
            set
            {
                this.valueEncodingField = value;
            }
        }
    }
}

