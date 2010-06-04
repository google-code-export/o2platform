namespace Amazon.EC2.Model
{
    using System;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://ec2.amazonaws.com/doc/2009-11-30/", IsNullable=false)]
    public class CreateVpnGatewayResult
    {
        private Amazon.EC2.Model.VpnGateway vpnGatewayField;

        public bool IsSetVpnGateway()
        {
            return (this.vpnGatewayField != null);
        }

        public CreateVpnGatewayResult WithVpnGateway(Amazon.EC2.Model.VpnGateway vpnGateway)
        {
            this.vpnGatewayField = vpnGateway;
            return this;
        }

        [XmlElement(ElementName="VpnGateway")]
        public Amazon.EC2.Model.VpnGateway VpnGateway
        {
            get
            {
                return this.vpnGatewayField;
            }
            set
            {
                this.vpnGatewayField = value;
            }
        }
    }
}

