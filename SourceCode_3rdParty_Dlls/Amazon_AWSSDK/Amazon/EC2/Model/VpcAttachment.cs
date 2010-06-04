namespace Amazon.EC2.Model
{
    using System;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://ec2.amazonaws.com/doc/2009-11-30/", IsNullable=false)]
    public class VpcAttachment
    {
        private string vpcAttachmentStateField;
        private string vpcIdField;

        public bool IsSetVpcAttachmentState()
        {
            return (this.vpcAttachmentStateField != null);
        }

        public bool IsSetVpcId()
        {
            return (this.vpcIdField != null);
        }

        public VpcAttachment WithVpcAttachmentState(string vpcAttachmentState)
        {
            this.vpcAttachmentStateField = vpcAttachmentState;
            return this;
        }

        public VpcAttachment WithVpcId(string vpcId)
        {
            this.vpcIdField = vpcId;
            return this;
        }

        [XmlElement(ElementName="VpcAttachmentState")]
        public string VpcAttachmentState
        {
            get
            {
                return this.vpcAttachmentStateField;
            }
            set
            {
                this.vpcAttachmentStateField = value;
            }
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
    }
}

