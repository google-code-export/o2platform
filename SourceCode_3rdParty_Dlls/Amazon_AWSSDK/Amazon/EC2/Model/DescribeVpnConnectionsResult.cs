namespace Amazon.EC2.Model
{
    using System;
    using System.Collections.Generic;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://ec2.amazonaws.com/doc/2009-11-30/", IsNullable=false)]
    public class DescribeVpnConnectionsResult
    {
        private List<Amazon.EC2.Model.VpnConnection> vpnConnectionField;

        public bool IsSetVpnConnection()
        {
            return (this.VpnConnection.Count > 0);
        }

        public DescribeVpnConnectionsResult WithVpnConnection(params Amazon.EC2.Model.VpnConnection[] list)
        {
            foreach (Amazon.EC2.Model.VpnConnection connection in list)
            {
                this.VpnConnection.Add(connection);
            }
            return this;
        }

        [XmlElement(ElementName="VpnConnection")]
        public List<Amazon.EC2.Model.VpnConnection> VpnConnection
        {
            get
            {
                if (this.vpnConnectionField == null)
                {
                    this.vpnConnectionField = new List<Amazon.EC2.Model.VpnConnection>();
                }
                return this.vpnConnectionField;
            }
            set
            {
                this.vpnConnectionField = value;
            }
        }
    }
}

