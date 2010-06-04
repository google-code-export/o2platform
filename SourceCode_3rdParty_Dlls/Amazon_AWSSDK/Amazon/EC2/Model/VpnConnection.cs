namespace Amazon.EC2.Model
{
    using System;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://ec2.amazonaws.com/doc/2009-11-30/", IsNullable=false)]
    public class VpnConnection
    {
        private string customerGatewayConfigurationField;
        private string customerGatewayIdField;
        private string typeField;
        private string vpnConnectionIdField;
        private string vpnConnectionStateField;
        private string vpnGatewayIdField;

        public bool IsSetCustomerGatewayConfiguration()
        {
            return (this.customerGatewayConfigurationField != null);
        }

        public bool IsSetCustomerGatewayId()
        {
            return (this.customerGatewayIdField != null);
        }

        public bool IsSetType()
        {
            return (this.typeField != null);
        }

        public bool IsSetVpnConnectionId()
        {
            return (this.vpnConnectionIdField != null);
        }

        public bool IsSetVpnConnectionState()
        {
            return (this.vpnConnectionStateField != null);
        }

        public bool IsSetVpnGatewayId()
        {
            return (this.vpnGatewayIdField != null);
        }

        public VpnConnection WithCustomerGatewayConfiguration(string customerGatewayConfiguration)
        {
            this.customerGatewayConfigurationField = customerGatewayConfiguration;
            return this;
        }

        public VpnConnection WithCustomerGatewayId(string customerGatewayId)
        {
            this.customerGatewayIdField = customerGatewayId;
            return this;
        }

        public VpnConnection WithType(string type)
        {
            this.typeField = type;
            return this;
        }

        public VpnConnection WithVpnConnectionId(string vpnConnectionId)
        {
            this.vpnConnectionIdField = vpnConnectionId;
            return this;
        }

        public VpnConnection WithVpnConnectionState(string vpnConnectionState)
        {
            this.vpnConnectionStateField = vpnConnectionState;
            return this;
        }

        public VpnConnection WithVpnGatewayId(string vpnGatewayId)
        {
            this.vpnGatewayIdField = vpnGatewayId;
            return this;
        }

        [XmlElement(ElementName="CustomerGatewayConfiguration")]
        public string CustomerGatewayConfiguration
        {
            get
            {
                return this.customerGatewayConfigurationField;
            }
            set
            {
                this.customerGatewayConfigurationField = value;
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

        [XmlElement(ElementName="VpnConnectionId")]
        public string VpnConnectionId
        {
            get
            {
                return this.vpnConnectionIdField;
            }
            set
            {
                this.vpnConnectionIdField = value;
            }
        }

        [XmlElement(ElementName="VpnConnectionState")]
        public string VpnConnectionState
        {
            get
            {
                return this.vpnConnectionStateField;
            }
            set
            {
                this.vpnConnectionStateField = value;
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

