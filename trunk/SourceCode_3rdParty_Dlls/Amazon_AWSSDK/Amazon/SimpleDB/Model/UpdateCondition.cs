namespace Amazon.SimpleDB.Model
{
    using System;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://sdb.amazonaws.com/doc/2009-04-15/", IsNullable=false)]
    public class UpdateCondition
    {
        private bool? existsField;
        private string nameField;
        private string valueField;

        public bool IsSetExists()
        {
            return this.existsField.HasValue;
        }

        public bool IsSetName()
        {
            return (this.nameField != null);
        }

        public bool IsSetValue()
        {
            return (this.valueField != null);
        }

        public UpdateCondition WithExists(bool exists)
        {
            this.existsField = new bool?(exists);
            return this;
        }

        public UpdateCondition WithName(string name)
        {
            this.nameField = name;
            return this;
        }

        public UpdateCondition WithValue(string value)
        {
            this.valueField = value;
            return this;
        }

        [XmlElement(ElementName="Exists")]
        public bool Exists
        {
            get
            {
                return this.existsField.GetValueOrDefault();
            }
            set
            {
                this.existsField = new bool?(value);
            }
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

