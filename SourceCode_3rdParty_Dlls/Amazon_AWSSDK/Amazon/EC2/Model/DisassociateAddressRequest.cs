namespace Amazon.EC2.Model
{
    using System;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://ec2.amazonaws.com/doc/2009-11-30/", IsNullable=false)]
    public class DisassociateAddressRequest
    {
        private string publicIpField;

        public bool IsSetPublicIp()
        {
            return (this.publicIpField != null);
        }

        public DisassociateAddressRequest WithPublicIp(string publicIp)
        {
            this.publicIpField = publicIp;
            return this;
        }

        [XmlElement(ElementName="PublicIp")]
        public string PublicIp
        {
            get
            {
                return this.publicIpField;
            }
            set
            {
                this.publicIpField = value;
            }
        }
    }
}

