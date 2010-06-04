namespace Amazon.SQS.Model
{
    using System;
    using System.IO;
    using System.Text;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://queue.amazonaws.com/doc/2009-02-01/", IsNullable=false)]
    public class GetQueueAttributesResponse
    {
        private Amazon.SQS.Model.GetQueueAttributesResult getQueueAttributesResultField;
        private Amazon.SQS.Model.ResponseMetadata responseMetadataField;

        public bool IsSetGetQueueAttributesResult()
        {
            return (this.getQueueAttributesResultField != null);
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

        public GetQueueAttributesResponse WithGetQueueAttributesResult(Amazon.SQS.Model.GetQueueAttributesResult getQueueAttributesResult)
        {
            this.getQueueAttributesResultField = getQueueAttributesResult;
            return this;
        }

        public GetQueueAttributesResponse WithResponseMetadata(Amazon.SQS.Model.ResponseMetadata responseMetadata)
        {
            this.responseMetadataField = responseMetadata;
            return this;
        }

        [XmlElement(ElementName="GetQueueAttributesResult")]
        public Amazon.SQS.Model.GetQueueAttributesResult GetQueueAttributesResult
        {
            get
            {
                return this.getQueueAttributesResultField;
            }
            set
            {
                this.getQueueAttributesResultField = value;
            }
        }

        [XmlElement(ElementName="ResponseMetadata")]
        public Amazon.SQS.Model.ResponseMetadata ResponseMetadata
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

