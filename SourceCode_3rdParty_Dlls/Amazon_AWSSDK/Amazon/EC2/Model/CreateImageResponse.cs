namespace Amazon.EC2.Model
{
    using System;
    using System.IO;
    using System.Text;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://ec2.amazonaws.com/doc/2009-11-30/", IsNullable=false)]
    public class CreateImageResponse
    {
        private Amazon.EC2.Model.CreateImageResult createImageResultField;
        private Amazon.EC2.Model.ResponseMetadata responseMetadataField;

        public bool IsSetCreateImageResult()
        {
            return (this.createImageResultField != null);
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

        public CreateImageResponse WithCreateImageResult(Amazon.EC2.Model.CreateImageResult createImageResult)
        {
            this.createImageResultField = createImageResult;
            return this;
        }

        public CreateImageResponse WithResponseMetadata(Amazon.EC2.Model.ResponseMetadata responseMetadata)
        {
            this.responseMetadataField = responseMetadata;
            return this;
        }

        [XmlElement(ElementName="CreateImageResult")]
        public Amazon.EC2.Model.CreateImageResult CreateImageResult
        {
            get
            {
                return this.createImageResultField;
            }
            set
            {
                this.createImageResultField = value;
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

