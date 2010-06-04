namespace Amazon.EC2.Model
{
    using System;
    using System.Collections.Generic;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://ec2.amazonaws.com/doc/2009-11-30/", IsNullable=false)]
    public class Filter
    {
        private string nameField;
        private List<string> valueField;

        public bool IsSetName()
        {
            return (this.nameField != null);
        }

        public bool IsSetValue()
        {
            return (this.Value.Count > 0);
        }

        public Amazon.EC2.Model.Filter WithName(string name)
        {
            this.nameField = name;
            return this;
        }

        public Amazon.EC2.Model.Filter WithValue(params string[] list)
        {
            foreach (string str in list)
            {
                this.Value.Add(str);
            }
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

