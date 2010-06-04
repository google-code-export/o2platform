namespace Amazon.EC2.Model
{
    using System;
    using System.IO;
    using System.Text;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://ec2.amazonaws.com/doc/2009-11-30/", IsNullable=false)]
    public class DetachVolumeResponse
    {
        private Amazon.EC2.Model.DetachVolumeResult detachVolumeResultField;
        private Amazon.EC2.Model.ResponseMetadata responseMetadataField;

        public bool IsSetDetachVolumeResult()
        {
            return (this.detachVolumeResultField != null);
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

        public DetachVolumeResponse WithDetachVolumeResult(Amazon.EC2.Model.DetachVolumeResult detachVolumeResult)
        {
            this.detachVolumeResultField = detachVolumeResult;
            return this;
        }

        public DetachVolumeResponse WithResponseMetadata(Amazon.EC2.Model.ResponseMetadata responseMetadata)
        {
            this.responseMetadataField = responseMetadata;
            return this;
        }

        [XmlElement(ElementName="DetachVolumeResult")]
        public Amazon.EC2.Model.DetachVolumeResult DetachVolumeResult
        {
            get
            {
                return this.detachVolumeResultField;
            }
            set
            {
                this.detachVolumeResultField = value;
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

