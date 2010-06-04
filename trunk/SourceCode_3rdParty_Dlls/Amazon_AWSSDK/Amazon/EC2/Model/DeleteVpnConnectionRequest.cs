namespace Amazon.EC2.Model
{
    using System;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://ec2.amazonaws.com/doc/2009-11-30/", IsNullable=false)]
    public class DeleteVpnConnectionRequest
    {
        private string vpnConnectionIdField;

        public bool IsSetVpnConnectionId()
        {
            return (this.vpnConnectionIdField != null);
        }

        public DeleteVpnConnectionRequest WithVpnConnectionId(string vpnConnectionId)
        {
            this.vpnConnectionIdField = vpnConnectionId;
            return this;
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
    }
}

