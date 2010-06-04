namespace Amazon.EC2.Model
{
    using System;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://ec2.amazonaws.com/doc/2009-11-30/", IsNullable=false)]
    public class CreateCustomerGatewayRequest
    {
        private decimal? bgpAsnField;
        private string ipAddressField;
        private string typeField;

        public bool IsSetBgpAsn()
        {
            return this.bgpAsnField.HasValue;
        }

        public bool IsSetIpAddress()
        {
            return (this.ipAddressField != null);
        }

        public bool IsSetType()
        {
            return (this.typeField != null);
        }

        public CreateCustomerGatewayRequest WithBgpAsn(decimal bgpAsn)
        {
            this.bgpAsnField = new decimal?(bgpAsn);
            return this;
        }

        public CreateCustomerGatewayRequest WithIpAddress(string ipAddress)
        {
            this.ipAddressField = ipAddress;
            return this;
        }

        public CreateCustomerGatewayRequest WithType(string type)
        {
            this.typeField = type;
            return this;
        }

        [XmlElement(ElementName="BgpAsn")]
        public decimal BgpAsn
        {
            get
            {
                return this.bgpAsnField.GetValueOrDefault();
            }
            set
            {
                this.bgpAsnField = new decimal?(value);
            }
        }

        [XmlElement(ElementName="IpAddress")]
        public string IpAddress
        {
            get
            {
                return this.ipAddressField;
            }
            set
            {
                this.ipAddressField = value;
            }
        }

        [XmlElement(ElementName="Type")]
        public string Type
        {
            get
            {
                return this.typeField;
            }
            set
            {
                this.typeField = value;
            }
        }
    }
}

