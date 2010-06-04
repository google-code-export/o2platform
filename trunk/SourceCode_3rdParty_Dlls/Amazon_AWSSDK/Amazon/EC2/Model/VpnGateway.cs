namespace Amazon.EC2.Model
{
    using System;
    using System.Collections.Generic;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://ec2.amazonaws.com/doc/2009-11-30/", IsNullable=false)]
    public class VpnGateway
    {
        private string availabilityZoneField;
        private string typeField;
        private List<Amazon.EC2.Model.VpcAttachment> vpcAttachmentField;
        private string vpnGatewayIdField;
        private string vpnGatewayStateField;

        public bool IsSetAvailabilityZone()
        {
            return (this.availabilityZoneField != null);
        }

        public bool IsSetType()
        {
            return (this.typeField != null);
        }

        public bool IsSetVpcAttachment()
        {
            return (this.VpcAttachment.Count > 0);
        }

        public bool IsSetVpnGatewayId()
        {
            return (this.vpnGatewayIdField != null);
        }

        public bool IsSetVpnGatewayState()
        {
            return (this.vpnGatewayStateField != null);
        }

        public VpnGateway WithAvailabilityZone(string availabilityZone)
        {
            this.availabilityZoneField = availabilityZone;
            return this;
        }

        public VpnGateway WithType(string type)
        {
            this.typeField = type;
            return this;
        }

        public VpnGateway WithVpcAttachment(params Amazon.EC2.Model.VpcAttachment[] list)
        {
            foreach (Amazon.EC2.Model.VpcAttachment attachment in list)
            {
                this.VpcAttachment.Add(attachment);
            }
            return this;
        }

        public VpnGateway WithVpnGatewayId(string vpnGatewayId)
        {
            this.vpnGatewayIdField = vpnGatewayId;
            return this;
        }

        public VpnGateway WithVpnGatewayState(string vpnGatewayState)
        {
            this.vpnGatewayStateField = vpnGatewayState;
            return this;
        }

        [XmlElement(ElementName="AvailabilityZone")]
        public string AvailabilityZone
        {
            get
            {
                return this.availabilityZoneField;
            }
            set
            {
                this.availabilityZoneField = value;
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

        [XmlElement(ElementName="VpcAttachment")]
        public List<Amazon.EC2.Model.VpcAttachment> VpcAttachment
        {
            get
            {
                if (this.vpcAttachmentField == null)
                {
                    this.vpcAttachmentField = new List<Amazon.EC2.Model.VpcAttachment>();
                }
                return this.vpcAttachmentField;
            }
            set
            {
                this.vpcAttachmentField = value;
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

        [XmlElement(ElementName="VpnGatewayState")]
        public string VpnGatewayState
        {
            get
            {
                return this.vpnGatewayStateField;
            }
            set
            {
                this.vpnGatewayStateField = value;
            }
        }
    }
}

