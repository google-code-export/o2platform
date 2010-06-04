namespace Amazon.EC2.Model
{
    using System;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://ec2.amazonaws.com/doc/2009-11-30/", IsNullable=false)]
    public class AttachVpnGatewayResult
    {
        private Amazon.EC2.Model.VpcAttachment vpcAttachmentField;

        public bool IsSetVpcAttachment()
        {
            return (this.vpcAttachmentField != null);
        }

        public AttachVpnGatewayResult WithVpcAttachment(Amazon.EC2.Model.VpcAttachment vpcAttachment)
        {
            this.vpcAttachmentField = vpcAttachment;
            return this;
        }

        [XmlElement(ElementName="VpcAttachment")]
        public Amazon.EC2.Model.VpcAttachment VpcAttachment
        {
            get
            {
                return this.vpcAttachmentField;
            }
            set
            {
                this.vpcAttachmentField = value;
            }
        }
    }
}

