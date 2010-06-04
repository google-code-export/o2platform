namespace Amazon.EC2.Model
{
    using System;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://ec2.amazonaws.com/doc/2009-11-30/", IsNullable=false)]
    public class CreateVpnConnectionResult
    {
        private Amazon.EC2.Model.VpnConnection vpnConnectionField;

        public bool IsSetVpnConnection()
        {
            return (this.vpnConnectionField != null);
        }

        public CreateVpnConnectionResult WithVpnConnection(Amazon.EC2.Model.VpnConnection vpnConnection)
        {
            this.vpnConnectionField = vpnConnection;
            return this;
        }

        [XmlElement(ElementName="VpnConnection")]
        public Amazon.EC2.Model.VpnConnection VpnConnection
        {
            get
            {
                return this.vpnConnectionField;
            }
            set
            {
                this.vpnConnectionField = value;
            }
        }
    }
}

