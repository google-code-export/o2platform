namespace Amazon.EC2.Model
{
    using System;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://ec2.amazonaws.com/doc/2009-11-30/", IsNullable=false)]
    public class InstanceState
    {
        private decimal? codeField;
        private string nameField;

        public bool IsSetCode()
        {
            return this.codeField.HasValue;
        }

        public bool IsSetName()
        {
            return (this.nameField != null);
        }

        public InstanceState WithCode(decimal code)
        {
            this.codeField = new decimal?(code);
            return this;
        }

        public InstanceState WithName(string name)
        {
            this.nameField = name;
            return this;
        }

        [XmlElement(ElementName="Code")]
        public decimal Code
        {
            get
            {
                return this.codeField.GetValueOrDefault();
            }
            set
            {
                this.codeField = new decimal?(value);
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
    }
}

