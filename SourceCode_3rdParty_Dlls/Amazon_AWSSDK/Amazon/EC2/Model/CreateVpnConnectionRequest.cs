namespace Amazon.EC2.Model
{
    using System;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://ec2.amazonaws.com/doc/2009-11-30/", IsNullable=false)]
    public class CreateVpnConnectionRequest
    {
        private string customerGatewayIdField;
        private string typeField;
        private string vpnGatewayIdField;

        public bool IsSetCustomerGatewayId()
        {
            return (this.customerGatewayIdField != null);
        }

        public bool IsSetType()
        {
            return (this.typeField != null);
        }

        public bool IsSetVpnGatewayId()
        {
            return (this.vpnGatewayIdField != null);
        }

        public CreateVpnConnectionRequest WithCustomerGatewayId(string customerGatewayId)
        {
            this.customerGatewayIdField = customerGatewayId;
            return this;
        }

        public CreateVpnConnectionRequest WithType(string type)
        {
            this.typeField = type;
            return this;
        }

        public CreateVpnConnectionRequest WithVpnGatewayId(string vpnGatewayId)
        {
            this.vpnGatewayIdField = vpnGatewayId;
            return this;
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

        [XmlElement(ElementName="VpnGatewayId")]
        public string VpnGatewayId
        {
            get
            {
                return this.vpnGatewayIdField;
            }
            set
            {
                this.vpnGatewayIdField = value;
            }
        }
    }
}

