namespace Amazon.SQS.Model
{
    using System;
    using System.Collections.Generic;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://queue.amazonaws.com/doc/2009-02-01/", IsNullable=false)]
    public class ReceiveMessageRequest
    {
        private List<string> attributeNameField;
        private decimal? maxNumberOfMessagesField;
        private string queueUrlField;
        private decimal? visibilityTimeoutField;

        public bool IsSetAttributeName()
        {
            return (this.AttributeName.Count > 0);
        }

        public bool IsSetMaxNumberOfMessages()
        {
            return this.maxNumberOfMessagesField.HasValue;
        }

        public bool IsSetQueueUrl()
        {
            return (this.queueUrlField != null);
        }

        public bool IsSetVisibilityTimeout()
        {
            return this.visibilityTimeoutField.HasValue;
        }

        public ReceiveMessageRequest WithAttributeName(params string[] list)
        {
            foreach (string str in list)
            {
                this.AttributeName.Add(str);
            }
            return this;
        }

        public ReceiveMessageRequest WithMaxNumberOfMessages(decimal maxNumberOfMessages)
        {
            this.maxNumberOfMessagesField = new decimal?(maxNumberOfMessages);
            return this;
        }

        public ReceiveMessageRequest WithQueueUrl(string queueUrl)
        {
            this.queueUrlField = queueUrl;
            return this;
        }

        public ReceiveMessageRequest WithVisibilityTimeout(decimal visibilityTimeout)
        {
            this.visibilityTimeoutField = new decimal?(visibilityTimeout);
            return this;
        }

        [XmlElement(ElementName="AttributeName")]
        public List<string> AttributeName
        {
            get
            {
                if (this.attributeNameField == null)
                {
                    this.attributeNameField = new List<string>();
                }
                return this.attributeNameField;
            }
            set
            {
                this.attributeNameField = value;
            }
        }

        [XmlElement(ElementName="MaxNumberOfMessages")]
        public decimal MaxNumberOfMessages
        {
            get
            {
                return this.maxNumberOfMessagesField.GetValueOrDefault();
            }
            set
            {
                this.maxNumberOfMessagesField = new decimal?(value);
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

