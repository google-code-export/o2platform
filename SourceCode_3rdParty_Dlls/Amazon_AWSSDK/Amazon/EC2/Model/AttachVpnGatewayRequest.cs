namespace Amazon.EC2.Model
{
    using System;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://ec2.amazonaws.com/doc/2009-11-30/", IsNullable=false)]
    public class AttachVpnGatewayRequest
    {
        private string vpcIdField;
        private string vpnGatewayIdField;

        public bool IsSetVpcId()
        {
            return (this.vpcIdField != null);
        }

        public bool IsSetVpnGatewayId()
        {
            return (this.vpnGatewayIdField != null);
        }

        public AttachVpnGatewayRequest WithVpcId(string vpcId)
        {
            this.vpcIdField = vpcId;
            return this;
        }

        public AttachVpnGatewayRequest WithVpnGatewayId(string vpnGatewayId)
        {
            this.vpnGatewayIdField = vpnGatewayId;
            return this;
        }

        [XmlElement(ElementName="VpcId")]
        public string VpcId
        {
            get
            {
                return this.vpcIdField;
            }
            set
            {
                this.vpcIdField = value;
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

