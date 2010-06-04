namespace Amazon.EC2.Model
{
    using System;
    using System.IO;
    using System.Text;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://ec2.amazonaws.com/doc/2009-11-30/", IsNullable=false)]
    public class CreateKeyPairResponse
    {
        private Amazon.EC2.Model.CreateKeyPairResult createKeyPairResultField;
        private Amazon.EC2.Model.ResponseMetadata responseMetadataField;

        public bool IsSetCreateKeyPairResult()
        {
            return (this.createKeyPairResultField != null);
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

        public CreateKeyPairResponse WithCreateKeyPairResult(Amazon.EC2.Model.CreateKeyPairResult createKeyPairResult)
        {
            this.createKeyPairResultField = createKeyPairResult;
            return this;
        }

        public CreateKeyPairResponse WithResponseMetadata(Amazon.EC2.Model.ResponseMetadata responseMetadata)
        {
            this.responseMetadataField = responseMetadata;
            return this;
        }

        [XmlElement(ElementName="CreateKeyPairResult")]
        public Amazon.EC2.Model.CreateKeyPairResult CreateKeyPairResult
        {
            get
            {
                return this.createKeyPairResultField;
            }
            set
            {
                this.createKeyPairResultField = value;
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

