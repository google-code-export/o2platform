namespace Amazon.SimpleDB.Model
{
    using System;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://sdb.amazonaws.com/doc/2009-04-15/", IsNullable=false)]
    public class ReplaceableAttribute
    {
        private string nameField;
        private bool? replaceField;
        private string valueField;

        public bool IsSetName()
        {
            return (this.nameField != null);
        }

        public bool IsSetReplace()
        {
            return this.replaceField.HasValue;
        }

        public bool IsSetValue()
        {
            return (this.valueField != null);
        }

        public ReplaceableAttribute WithName(string name)
        {
            this.nameField = name;
            return this;
        }

        public ReplaceableAttribute WithReplace(bool replace)
        {
            this.replaceField = new bool?(replace);
            return this;
        }

        public ReplaceableAttribute WithValue(string value)
        {
            this.valueField = value;
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

        [XmlElement(ElementName="Replace")]
        public bool Replace
        {
            get
            {
                return this.replaceField.GetValueOrDefault();
            }
            set
            {
                this.replaceField = new bool?(value);
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

