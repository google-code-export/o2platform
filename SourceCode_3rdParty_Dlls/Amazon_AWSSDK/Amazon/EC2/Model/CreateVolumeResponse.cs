namespace Amazon.EC2.Model
{
    using System;
    using System.IO;
    using System.Text;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://ec2.amazonaws.com/doc/2009-11-30/", IsNullable=false)]
    public class CreateVolumeResponse
    {
        private Amazon.EC2.Model.CreateVolumeResult createVolumeResultField;
        private Amazon.EC2.Model.ResponseMetadata responseMetadataField;

        public bool IsSetCreateVolumeResult()
        {
            return (this.createVolumeResultField != null);
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

        public CreateVolumeResponse WithCreateVolumeResult(Amazon.EC2.Model.CreateVolumeResult createVolumeResult)
        {
            this.createVolumeResultField = createVolumeResult;
            return this;
        }

        public CreateVolumeResponse WithResponseMetadata(Amazon.EC2.Model.ResponseMetadata responseMetadata)
        {
            this.responseMetadataField = responseMetadata;
            return this;
        }

        [XmlElement(ElementName="CreateVolumeResult")]
        public Amazon.EC2.Model.CreateVolumeResult CreateVolumeResult
        {
            get
            {
                return this.createVolumeResultField;
            }
            set
            {
                this.createVolumeResultField = value;
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

