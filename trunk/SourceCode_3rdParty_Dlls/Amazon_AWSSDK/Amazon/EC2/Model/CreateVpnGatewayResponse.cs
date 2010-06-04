namespace Amazon.EC2.Model
{
    using System;
    using System.IO;
    using System.Text;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://ec2.amazonaws.com/doc/2009-11-30/", IsNullable=false)]
    public class CreateVpnGatewayResponse
    {
        private Amazon.EC2.Model.CreateVpnGatewayResult createVpnGatewayResultField;
        private Amazon.EC2.Model.ResponseMetadata responseMetadataField;

        public bool IsSetCreateVpnGatewayResult()
        {
            return (this.createVpnGatewayResultField != null);
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

        public CreateVpnGatewayResponse WithCreateVpnGatewayResult(Amazon.EC2.Model.CreateVpnGatewayResult createVpnGatewayResult)
        {
            this.createVpnGatewayResultField = createVpnGatewayResult;
            return this;
        }

        public CreateVpnGatewayResponse WithResponseMetadata(Amazon.EC2.Model.ResponseMetadata responseMetadata)
        {
            this.responseMetadataField = responseMetadata;
            return this;
        }

        [XmlElement(ElementName="CreateVpnGatewayResult")]
        public Amazon.EC2.Model.CreateVpnGatewayResult CreateVpnGatewayResult
        {
            get
            {
                return this.createVpnGatewayResultField;
            }
            set
            {
                this.createVpnGatewayResultField = value;
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

