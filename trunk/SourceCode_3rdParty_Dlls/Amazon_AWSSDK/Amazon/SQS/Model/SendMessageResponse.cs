namespace Amazon.SQS.Model
{
    using System;
    using System.IO;
    using System.Text;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://queue.amazonaws.com/doc/2009-02-01/", IsNullable=false)]
    public class SendMessageResponse
    {
        private Amazon.SQS.Model.ResponseMetadata responseMetadataField;
        private Amazon.SQS.Model.SendMessageResult sendMessageResultField;

        public bool IsSetResponseMetadata()
        {
            return (this.responseMetadataField != null);
        }

        public bool IsSetSendMessageResult()
        {
            return (this.sendMessageResultField != null);
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

        public SendMessageResponse WithResponseMetadata(Amazon.SQS.Model.ResponseMetadata responseMetadata)
        {
            this.responseMetadataField = responseMetadata;
            return this;
        }

        public SendMessageResponse WithSendMessageResult(Amazon.SQS.Model.SendMessageResult sendMessageResult)
        {
            this.sendMessageResultField = sendMessageResult;
            return this;
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

        [XmlElement(ElementName="SendMessageResult")]
        public Amazon.SQS.Model.SendMessageResult SendMessageResult
        {
            get
            {
                return this.sendMessageResultField;
            }
            set
            {
                this.sendMessageResultField = value;
            }
        }
    }
}

