namespace Amazon.EC2.Model
{
    using System;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://ec2.amazonaws.com/doc/2009-11-30/", IsNullable=false)]
    public class DeleteVpnGatewayRequest
    {
        private string vpnGatewayIdField;

        public bool IsSetVpnGatewayId()
        {
            return (this.vpnGatewayIdField != null);
        }

        public DeleteVpnGatewayRequest WithVpnGatewayId(string vpnGatewayId)
        {
            this.vpnGatewayIdField = vpnGatewayId;
            return this;
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

