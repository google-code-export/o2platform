namespace Amazon.SQS.Model
{
    using System;
    using System.Collections.Generic;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://queue.amazonaws.com/doc/2009-02-01/", IsNullable=false)]
    public class DeleteMessageRequest
    {
        private List<Amazon.SQS.Model.Attribute> attributeField;
        private string queueUrlField;
        private string receiptHandleField;

        public bool IsSetAttribute()
        {
            return (this.Attribute.Count > 0);
        }

        public bool IsSetQueueUrl()
        {
            return (this.queueUrlField != null);
        }

        public bool IsSetReceiptHandle()
        {
            return (this.receiptHandleField != null);
        }

        public DeleteMessageRequest WithAttribute(params Amazon.SQS.Model.Attribute[] list)
        {
            foreach (Amazon.SQS.Model.Attribute attribute in list)
            {
                this.Attribute.Add(attribute);
            }
            return this;
        }

        public DeleteMessageRequest WithQueueUrl(string queueUrl)
        {
            this.queueUrlField = queueUrl;
            return this;
        }

        public DeleteMessageRequest WithReceiptHandle(string receiptHandle)
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

        [XmlElement(ElementName="QueueUrl")]
        public string QueueUrl
        {
            get
            {
                return this.queueUrlField;
            }
            set
            {
                this.queueUrlField = value;
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

