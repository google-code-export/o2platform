namespace Amazon.CloudWatch.Model
{
    using System;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://monitoring.amazonaws.com/doc/2009-05-15/", IsNullable=false)]
    public class Dimension
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

        public Dimension WithName(string name)
        {
            this.nameField = name;
            return this;
        }

        public Dimension WithValue(string value)
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

