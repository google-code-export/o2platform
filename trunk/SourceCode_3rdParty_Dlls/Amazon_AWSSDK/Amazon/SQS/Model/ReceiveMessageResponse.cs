namespace Amazon.SQS.Model
{
    using System;
    using System.IO;
    using System.Text;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://queue.amazonaws.com/doc/2009-02-01/", IsNullable=false)]
    public class ReceiveMessageResponse
    {
        private Amazon.SQS.Model.ReceiveMessageResult receiveMessageResultField;
        private Amazon.SQS.Model.ResponseMetadata responseMetadataField;

        public bool IsSetReceiveMessageResult()
        {
            return (this.receiveMessageResultField != null);
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

        public ReceiveMessageResponse WithReceiveMessageResult(Amazon.SQS.Model.ReceiveMessageResult receiveMessageResult)
        {
            this.receiveMessageResultField = receiveMessageResult;
            return this;
        }

        public ReceiveMessageResponse WithResponseMetadata(Amazon.SQS.Model.ResponseMetadata responseMetadata)
        {
            this.responseMetadataField = responseMetadata;
            return this;
        }

        [XmlElement(ElementName="ReceiveMessageResult")]
        public Amazon.SQS.Model.ReceiveMessageResult ReceiveMessageResult
        {
            get
            {
                return this.receiveMessageResultField;
            }
            set
            {
                this.receiveMessageResultField = value;
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

