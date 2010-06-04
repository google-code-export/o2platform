namespace Amazon.SQS.Model
{
    using System;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://queue.amazonaws.com/doc/2009-02-01/", IsNullable=false)]
    public class SendMessageResult
    {
        private string MD5OfMessageBodyField;
        private string messageIdField;

        public bool IsSetMD5OfMessageBody()
        {
            return (this.MD5OfMessageBodyField != null);
        }

        public bool IsSetMessageId()
        {
            return (this.messageIdField != null);
        }

        public SendMessageResult WithMD5OfMessageBody(string MD5OfMessageBody)
        {
            this.MD5OfMessageBodyField = MD5OfMessageBody;
            return this;
        }

        public SendMessageResult WithMessageId(string messageId)
        {
            this.messageIdField = messageId;
            return this;
        }

        [XmlElement(ElementName="MD5OfMessageBody")]
        public string MD5OfMessageBody
        {
            get
            {
                return this.MD5OfMessageBodyField;
            }
            set
            {
                this.MD5OfMessageBodyField = value;
            }
        }

        [XmlElement(ElementName="MessageId")]
        public string MessageId
        {
            get
            {
                return this.messageIdField;
            }
            set
            {
                this.messageIdField = value;
            }
        }
    }
}

