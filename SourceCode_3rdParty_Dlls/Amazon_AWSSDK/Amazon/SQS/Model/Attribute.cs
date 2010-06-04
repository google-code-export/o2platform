namespace Amazon.SQS.Model
{
    using System;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://queue.amazonaws.com/doc/2009-02-01/", IsNullable=false)]
    public class Attribute
    {
        private string nameField;
        private string valueField;

        public bool IsSetName()
        {
            return (this.nameField != null);
        }

        public bool IsSetValue()
        {
            return (this.valueField != null);
        }

        public Amazon.SQS.Model.Attribute WithName(string name)
        {
            this.nameField = name;
            return this;
        }

        public Amazon.SQS.Model.Attribute WithValue(string value)
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

