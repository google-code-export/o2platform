namespace Amazon.SQS.Model
{
    using System;
    using System.Collections.Generic;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://queue.amazonaws.com/doc/2009-02-01/", IsNullable=false)]
    public class ChangeMessageVisibilityRequest
    {
        private List<Amazon.SQS.Model.Attribute> attributeField;
        private string queueUrlField;
        private string receiptHandleField;
        private decimal? visibilityTimeoutField;

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

        public bool IsSetVisibilityTimeout()
        {
            return this.visibilityTimeoutField.HasValue;
        }

        public ChangeMessageVisibilityRequest WithAttribute(params Amazon.SQS.Model.Attribute[] list)
        {
            foreach (Amazon.SQS.Model.Attribute attribute in list)
            {
                this.Attribute.Add(attribute);
            }
            return this;
        }

        public ChangeMessageVisibilityRequest WithQueueUrl(string queueUrl)
        {
            this.queueUrlField = queueUrl;
            return this;
        }

        public ChangeMessageVisibilityRequest WithReceiptHandle(string receiptHandle)
        {
            this.receiptHandleField = receiptHandle;
            return this;
        }

        public ChangeMessageVisibilityRequest WithVisibilityTimeout(decimal visibilityTimeout)
        {
            this.visibilityTimeoutField = new decimal?(visibilityTimeout);
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

        [XmlElement(ElementName="VisibilityTimeout")]
        public decimal VisibilityTimeout
        {
            get
            {
                return this.visibilityTimeoutField.GetValueOrDefault();
            }
            set
            {
                this.visibilityTimeoutField = new decimal?(value);
            }
        }
    }
}

