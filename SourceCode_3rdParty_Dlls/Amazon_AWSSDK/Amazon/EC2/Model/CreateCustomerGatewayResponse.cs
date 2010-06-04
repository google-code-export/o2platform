namespace Amazon.EC2.Model
{
    using System;
    using System.IO;
    using System.Text;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://ec2.amazonaws.com/doc/2009-11-30/", IsNullable=false)]
    public class CreateCustomerGatewayResponse
    {
        private Amazon.EC2.Model.CreateCustomerGatewayResult createCustomerGatewayResultField;
        private Amazon.EC2.Model.ResponseMetadata responseMetadataField;

        public bool IsSetCreateCustomerGatewayResult()
        {
            return (this.createCustomerGatewayResultField != null);
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

        public CreateCustomerGatewayResponse WithCreateCustomerGatewayResult(Amazon.EC2.Model.CreateCustomerGatewayResult createCustomerGatewayResult)
        {
            this.createCustomerGatewayResultField = createCustomerGatewayResult;
            return this;
        }

        public CreateCustomerGatewayResponse WithResponseMetadata(Amazon.EC2.Model.ResponseMetadata responseMetadata)
        {
            this.responseMetadataField = responseMetadata;
            return this;
        }

        [XmlElement(ElementName="CreateCustomerGatewayResult")]
        public Amazon.EC2.Model.CreateCustomerGatewayResult CreateCustomerGatewayResult
        {
            get
            {
                return this.createCustomerGatewayResultField;
            }
            set
            {
                this.createCustomerGatewayResultField = value;
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

