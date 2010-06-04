namespace Amazon.EC2.Model
{
    using System;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://ec2.amazonaws.com/doc/2009-11-30/", IsNullable=false)]
    public class CreateKeyPairRequest
    {
        private string keyNameField;

        public bool IsSetKeyName()
        {
            return (this.keyNameField != null);
        }

        public CreateKeyPairRequest WithKeyName(string keyName)
        {
            this.keyNameField = keyName;
            return this;
        }

        [XmlElement(ElementName="KeyName")]
        public string KeyName
        {
            get
            {
                return this.keyNameField;
            }
            set
            {
                this.keyNameField = value;
            }
        }
    }
}

