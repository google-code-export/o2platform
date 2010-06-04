namespace Amazon.EC2.Model
{
    using System;
    using System.Collections.Generic;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://ec2.amazonaws.com/doc/2009-11-30/", IsNullable=false)]
    public class DescribeKeyPairsRequest
    {
        private List<string> keyNameField;

        public bool IsSetKeyName()
        {
            return (this.KeyName.Count > 0);
        }

        public DescribeKeyPairsRequest WithKeyName(params string[] list)
        {
            foreach (string str in list)
            {
                this.KeyName.Add(str);
            }
            return this;
        }

        [XmlElement(ElementName="KeyName")]
        public List<string> KeyName
        {
            get
            {
                if (this.keyNameField == null)
                {
                    this.keyNameField = new List<string>();
                }
                return this.keyNameField;
            }
            set
            {
                this.keyNameField = value;
            }
        }
    }
}

