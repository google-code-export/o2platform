namespace Amazon.SQS.Model
{
    using System;
    using System.IO;
    using System.Text;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://queue.amazonaws.com/doc/2009-02-01/", IsNullable=false)]
    public class CreateQueueResponse
    {
        private Amazon.SQS.Model.CreateQueueResult createQueueResultField;
        private Amazon.SQS.Model.ResponseMetadata responseMetadataField;

        public bool IsSetCreateQueueResult()
        {
            return (this.createQueueResultField != null);
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

        public CreateQueueResponse WithCreateQueueResult(Amazon.SQS.Model.CreateQueueResult createQueueResult)
        {
            this.createQueueResultField = createQueueResult;
            return this;
        }

        public CreateQueueResponse WithResponseMetadata(Amazon.SQS.Model.ResponseMetadata responseMetadata)
        {
            this.responseMetadataField = responseMetadata;
            return this;
        }

        [XmlElement(ElementName="CreateQueueResult")]
        public Amazon.SQS.Model.CreateQueueResult CreateQueueResult
        {
            get
            {
                return this.createQueueResultField;
            }
            set
            {
                this.createQueueResultField = value;
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

