namespace Amazon.EC2.Model
{
    using System;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://ec2.amazonaws.com/doc/2009-11-30/", IsNullable=false)]
    public class CustomerGateway
    {
        private string bgpAsnField;
        private string customerGatewayIdField;
        private string customerGatewayStateField;
        private string ipAddressField;
        private string typeField;

        public bool IsSetBgpAsn()
        {
            return (this.bgpAsnField != null);
        }

        public bool IsSetCustomerGatewayId()
        {
            return (this.customerGatewayIdField != null);
        }

        public bool IsSetCustomerGatewayState()
        {
            return (this.customerGatewayStateField != null);
        }

        public bool IsSetIpAddress()
        {
            return (this.ipAddressField != null);
        }

        public bool IsSetType()
        {
            return (this.typeField != null);
        }

        public CustomerGateway WithBgpAsn(string bgpAsn)
        {
            this.bgpAsnField = bgpAsn;
            return this;
        }

        public CustomerGateway WithCustomerGatewayId(string customerGatewayId)
        {
            this.customerGatewayIdField = customerGatewayId;
            return this;
        }

        public CustomerGateway WithCustomerGatewayState(string customerGatewayState)
        {
            this.customerGatewayStateField = customerGatewayState;
            return this;
        }

        public CustomerGateway WithIpAddress(string ipAddress)
        {
            this.ipAddressField = ipAddress;
            return this;
        }

        public CustomerGateway WithType(string type)
        {
            this.typeField = type;
            return this;
        }

        [XmlElement(ElementName="BgpAsn")]
        public string BgpAsn
        {
            get
            {
                return this.bgpAsnField;
            }
            set
            {
                this.bgpAsnField = value;
            }
        }

        [XmlElement(ElementName="CustomerGatewayId")]
        public string CustomerGatewayId
        {
            get
            {
                return this.customerGatewayIdField;
            }
            set
            {
                this.customerGatewayIdField = value;
            }
        }

        [XmlElement(ElementName="CustomerGatewayState")]
        public string CustomerGatewayState
        {
            get
            {
                return this.customerGatewayStateField;
            }
            set
            {
                this.customerGatewayStateField = value;
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

