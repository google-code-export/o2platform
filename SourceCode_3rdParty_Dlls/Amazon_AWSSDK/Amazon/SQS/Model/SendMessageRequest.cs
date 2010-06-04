namespace Amazon.SQS.Model
{
    using System;
    using System.Collections.Generic;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://queue.amazonaws.com/doc/2009-02-01/", IsNullable=false)]
    public class SendMessageRequest
    {
        private List<Amazon.SQS.Model.Attribute> attributeField;
        private string messageBodyField;
        private string queueUrlField;

        public bool IsSetAttribute()
        {
            return (this.Attribute.Count > 0);
        }

        public bool IsSetMessageBody()
        {
            return (this.messageBodyField != null);
        }

        public bool IsSetQueueUrl()
        {
            return (this.queueUrlField != null);
        }

        public SendMessageRequest WithAttribute(params Amazon.SQS.Model.Attribute[] list)
        {
            foreach (Amazon.SQS.Model.Attribute attribute in list)
            {
                this.Attribute.Add(attribute);
            }
            return this;
        }

        public SendMessageRequest WithMessageBody(string messageBody)
        {
            this.messageBodyField = messageBody;
            return this;
        }

        public SendMessageRequest WithQueueUrl(string queueUrl)
        {
            this.queueUrlField = queueUrl;
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

        [XmlElement(ElementName="MessageBody")]
        public string MessageBody
        {
            get
            {
                return this.messageBodyField;
            }
            set
            {
                this.messageBodyField = value;
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
    }
}

