namespace Amazon.EC2.Model
{
    using System;
    using System.IO;
    using System.Text;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://ec2.amazonaws.com/doc/2009-11-30/", IsNullable=false)]
    public class AttachVolumeResponse
    {
        private Amazon.EC2.Model.AttachVolumeResult attachVolumeResultField;
        private Amazon.EC2.Model.ResponseMetadata responseMetadataField;

        public bool IsSetAttachVolumeResult()
        {
            return (this.attachVolumeResultField != null);
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

        public AttachVolumeResponse WithAttachVolumeResult(Amazon.EC2.Model.AttachVolumeResult attachVolumeResult)
        {
            this.attachVolumeResultField = attachVolumeResult;
            return this;
        }

        public AttachVolumeResponse WithResponseMetadata(Amazon.EC2.Model.ResponseMetadata responseMetadata)
        {
            this.responseMetadataField = responseMetadata;
            return this;
        }

        [XmlElement(ElementName="AttachVolumeResult")]
        public Amazon.EC2.Model.AttachVolumeResult AttachVolumeResult
        {
            get
            {
                return this.attachVolumeResultField;
            }
            set
            {
                this.attachVolumeResultField = value;
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

