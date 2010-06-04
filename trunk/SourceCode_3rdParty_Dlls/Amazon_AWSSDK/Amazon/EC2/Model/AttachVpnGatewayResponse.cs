namespace Amazon.EC2.Model
{
    using System;
    using System.IO;
    using System.Text;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://ec2.amazonaws.com/doc/2009-11-30/", IsNullable=false)]
    public class AttachVpnGatewayResponse
    {
        private Amazon.EC2.Model.AttachVpnGatewayResult attachVpnGatewayResultField;
        private Amazon.EC2.Model.ResponseMetadata responseMetadataField;

        public bool IsSetAttachVpnGatewayResult()
        {
            return (this.attachVpnGatewayResultField != null);
        }

        public bool IsSetResponseMetadata()
        {
            return (this.responseMetadataField != null);
        }

        public string ToXML()
        {
            StringBuilder sb = new StringBuilder(0x400);
            XmlSerializer serializer = new XmlSerializer(base.GetType());
            using (StringWriter writer = new StringWriter(sb))
            {
                serializer.Serialize((TextWriter) writer, this);
            }
            return sb.ToString();
        }

        public AttachVpnGatewayResponse WithAttachVpnGatewayResult(Amazon.EC2.Model.AttachVpnGatewayResult attachVpnGatewayResult)
        {
            this.attachVpnGatewayResultField = attachVpnGatewayResult;
            return this;
        }

        public AttachVpnGatewayResponse WithResponseMetadata(Amazon.EC2.Model.ResponseMetadata responseMetadata)
        {
            this.responseMetadataField = responseMetadata;
            return this;
        }

        [XmlElement(ElementName="AttachVpnGatewayResult")]
        public Amazon.EC2.Model.AttachVpnGatewayResult AttachVpnGatewayResult
        {
            get
            {
                return this.attachVpnGatewayResultField;
            }
            set
            {
                this.attachVpnGatewayResultField = value;
            }
        }

        [XmlElement(ElementName="ResponseMetadata")]
        public Amazon.EC2.Model.ResponseMetadata ResponseMetadata
        {
            get
            {
                return this.responseMetadataField;
            }
            set
            {
                this.responseMetadataField = value;
            }
        }
    }
}

