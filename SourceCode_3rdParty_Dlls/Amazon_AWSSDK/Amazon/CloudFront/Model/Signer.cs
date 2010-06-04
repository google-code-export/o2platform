namespace Amazon.CloudFront.Model
{
    using System;
    using System.Collections.Generic;
    using System.Xml.Serialization;

    [Serializable]
    public class Signer
    {
        private string awsAccountNumber;
        private List<string> keypairId;
        private bool self;

        internal bool IsSetAwsAccountNumber()
        {
            return !string.IsNullOrEmpty(this.AwsAccountNumber);
        }

        internal bool IsSetKeypairId()
        {
            return ((this.KeyPairId != null) && (this.KeyPairId.Count > 0));
        }

        [XmlElement(ElementName="AwsAccountNumber")]
        public string AwsAccountNumber
        {
            get
            {
                return this.awsAccountNumber;
            }
            set
            {
                this.awsAccountNumber = value;
            }
        }

        [XmlElement(ElementName="KeyPairId")]
        public List<string> KeyPairId
        {
            get
            {
                if (this.keypairId == null)
                {
                    this.keypairId = new List<string>();
                }
                return this.keypairId;
            }
        }

        [XmlElement(ElementName="Self")]
        public bool Self
        {
            get
            {
                return this.self;
            }
            set
            {
                this.self = value;
            }
        }
    }
}

