namespace Amazon.EC2.Model
{
    using System;
    using System.Collections.Generic;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://ec2.amazonaws.com/doc/2009-11-30/", IsNullable=false)]
    public class DescribeVpnGatewaysResult
    {
        private List<Amazon.EC2.Model.VpnGateway> vpnGatewayField;

        public bool IsSetVpnGateway()
        {
            return (this.VpnGateway.Count > 0);
        }

        public DescribeVpnGatewaysResult WithVpnGateway(params Amazon.EC2.Model.VpnGateway[] list)
        {
            foreach (Amazon.EC2.Model.VpnGateway gateway in list)
            {
                this.VpnGateway.Add(gateway);
            }
            return this;
        }

        [XmlElement(ElementName="VpnGateway")]
        public List<Amazon.EC2.Model.VpnGateway> VpnGateway
        {
            get
            {
                if (this.vpnGatewayField == null)
                {
                    this.vpnGatewayField = new List<Amazon.EC2.Model.VpnGateway>();
                }
                return this.vpnGatewayField;
            }
            set
            {
                this.vpnGatewayField = value;
            }
        }
    }
}

