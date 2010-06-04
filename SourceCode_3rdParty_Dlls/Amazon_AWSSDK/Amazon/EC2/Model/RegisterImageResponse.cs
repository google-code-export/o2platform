namespace Amazon.EC2.Model
{
    using System;
    using System.IO;
    using System.Text;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://ec2.amazonaws.com/doc/2009-11-30/", IsNullable=false)]
    public class RegisterImageResponse
    {
        private Amazon.EC2.Model.RegisterImageResult registerImageResultField;
        private Amazon.EC2.Model.ResponseMetadata responseMetadataField;

        public bool IsSetRegisterImageResult()
        {
            return (this.registerImageResultField != null);
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

        public RegisterImageResponse WithRegisterImageResult(Amazon.EC2.Model.RegisterImageResult registerImageResult)
        {
            this.registerImageResultField = registerImageResult;
            return this;
        }

        public RegisterImageResponse WithResponseMetadata(Amazon.EC2.Model.ResponseMetadata responseMetadata)
        {
            this.responseMetadataField = responseMetadata;
            return this;
        }

        [XmlElement(ElementName="RegisterImageResult")]
        public Amazon.EC2.Model.RegisterImageResult RegisterImageResult
        {
            get
            {
                return this.registerImageResultField;
            }
            set
            {
                this.registerImageResultField = value;
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

