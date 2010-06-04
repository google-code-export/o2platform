namespace Amazon.EC2.Model
{
    using System;
    using System.IO;
    using System.Text;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://ec2.amazonaws.com/doc/2009-11-30/", IsNullable=false)]
    public class CreateDhcpOptionsResponse
    {
        private Amazon.EC2.Model.CreateDhcpOptionsResult createDhcpOptionsResultField;
        private Amazon.EC2.Model.ResponseMetadata responseMetadataField;

        public bool IsSetCreateDhcpOptionsResult()
        {
            return (this.createDhcpOptionsResultField != null);
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

        public CreateDhcpOptionsResponse WithCreateDhcpOptionsResult(Amazon.EC2.Model.CreateDhcpOptionsResult createDhcpOptionsResult)
        {
            this.createDhcpOptionsResultField = createDhcpOptionsResult;
            return this;
        }

        public CreateDhcpOptionsResponse WithResponseMetadata(Amazon.EC2.Model.ResponseMetadata responseMetadata)
        {
            this.responseMetadataField = responseMetadata;
            return this;
        }

        [XmlElement(ElementName="CreateDhcpOptionsResult")]
        public Amazon.EC2.Model.CreateDhcpOptionsResult CreateDhcpOptionsResult
        {
            get
            {
                return this.createDhcpOptionsResultField;
            }
            set
            {
                this.createDhcpOptionsResultField = value;
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

