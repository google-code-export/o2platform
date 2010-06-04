namespace Amazon.EC2.Model
{
    using System;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://ec2.amazonaws.com/doc/2009-11-30/", IsNullable=false)]
    public class CreateKeyPairResult
    {
        private Amazon.EC2.Model.KeyPair keyPairField;

        public bool IsSetKeyPair()
        {
            return (this.keyPairField != null);
        }

        public CreateKeyPairResult WithKeyPair(Amazon.EC2.Model.KeyPair keyPair)
        {
            this.keyPairField = keyPair;
            return this;
        }

        [XmlElement(ElementName="KeyPair")]
        public Amazon.EC2.Model.KeyPair KeyPair
        {
            get
            {
                return this.keyPairField;
            }
            set
            {
                this.keyPairField = value;
            }
        }
    }
}

