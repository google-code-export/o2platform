namespace Amazon.EC2.Model
{
    using System;
    using System.Collections.Generic;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://ec2.amazonaws.com/doc/2009-11-30/", IsNullable=false)]
    public class DescribeKeyPairsResult
    {
        private List<Amazon.EC2.Model.KeyPair> keyPairField;

        public bool IsSetKeyPair()
        {
            return (this.KeyPair.Count > 0);
        }

        public DescribeKeyPairsResult WithKeyPair(params Amazon.EC2.Model.KeyPair[] list)
        {
            foreach (Amazon.EC2.Model.KeyPair pair in list)
            {
                this.KeyPair.Add(pair);
            }
            return this;
        }

        [XmlElement(ElementName="KeyPair")]
        public List<Amazon.EC2.Model.KeyPair> KeyPair
        {
            get
            {
                if (this.keyPairField == null)
                {
                    this.keyPairField = new List<Amazon.EC2.Model.KeyPair>();
                }
                return this.keyPairField;
            }
            set
            {
                this.keyPairField = value;
            }
        }
    }
}

