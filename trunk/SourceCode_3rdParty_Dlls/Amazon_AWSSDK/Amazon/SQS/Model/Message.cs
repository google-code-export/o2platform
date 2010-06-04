namespace Amazon.SQS.Model
{
    using System;
    using System.Collections.Generic;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://queue.amazonaws.com/doc/2009-02-01/", IsNullable=false)]
    public class Message
    {
        private List<Amazon.SQS.Model.Attribute> attributeField;
        private string bodyField;
        private string MD5OfBodyField;
        private string messageIdField;
        private string receiptHandleField;

        public bool IsSetAttribute()
        {
            return (this.Attribute.Count > 0);
        }

        public bool IsSetBody()
        {
            return (this.bodyField != null);
        }

        public bool IsSetMD5OfBody()
        {
            return (this.MD5OfBodyField != null);
        }

        public bool IsSetMessageId()
        {
            return (this.messageIdField != null);
        }

        public bool IsSetReceiptHandle()
        {
            return (this.receiptHandleField != null);
        }

        public Message WithAttribute(params Amazon.SQS.Model.Attribute[] list)
        {
            foreach (Amazon.SQS.Model.Attribute attribute in list)
            {
                this.Attribute.Add(attribute);
            }
            return this;
        }

        public Message WithBody(string body)
        {
            this.bodyField = body;
            return this;
        }

        public Message WithMD5OfBody(string MD5OfBody)
        {
            this.MD5OfBodyField = MD5OfBody;
            return this;
        }

        public Message WithMessageId(string messageId)
        {
            this.messageIdField = messageId;
            return this;
        }

        public Message WithReceiptHandle(string receiptHandle)
        {
            this.receiptHandleField = receiptHandle;
            return this;
        }

        [XmlElement(ElementName="Attribute")]
        public List<Amazon.SQS.Model.Attribute> Attribute
        {
            get
            {
                if (this.attributeField == null)
                {
                    this.attributeField = new List<Amazon.SQS.Model.Attribute>();
                }
                return this.attributeField;
            }
            set
            {
                this.attributeField = value;
            }
        }

        [XmlElement(ElementName="Body")]
        public string Body
        {
            get
            {
                return this.bodyField;
            }
            set
            {
                this.bodyField = value;
            }
        }

        [XmlElement(ElementName="MD5OfBody")]
        public string MD5OfBody
        {
            get
            {
                return this.MD5OfBodyField;
            }
            set
            {
                this.MD5OfBodyField = value;
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

        [XmlElement(ElementName="ReceiptHandle")]
        public string ReceiptHandle
        {
            get
            {
                return this.receiptHandleField;
            }
            set
            {
                this.receiptHandleField = value;
            }
        }
    }
}

