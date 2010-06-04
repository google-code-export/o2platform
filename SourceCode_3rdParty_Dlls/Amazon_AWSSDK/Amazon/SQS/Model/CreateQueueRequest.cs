namespace Amazon.SQS.Model
{
    using System;
    using System.Collections.Generic;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://queue.amazonaws.com/doc/2009-02-01/", IsNullable=false)]
    public class CreateQueueRequest
    {
        private List<Amazon.SQS.Model.Attribute> attributeField;
        private decimal? defaultVisibilityTimeoutField;
        private string queueNameField;

        public bool IsSetAttribute()
        {
            return (this.Attribute.Count > 0);
        }

        public bool IsSetDefaultVisibilityTimeout()
        {
            return this.defaultVisibilityTimeoutField.HasValue;
        }

        public bool IsSetQueueName()
        {
            return (this.queueNameField != null);
        }

        public CreateQueueRequest WithAttribute(params Amazon.SQS.Model.Attribute[] list)
        {
            foreach (Amazon.SQS.Model.Attribute attribute in list)
            {
                this.Attribute.Add(attribute);
            }
            return this;
        }

        public CreateQueueRequest WithDefaultVisibilityTimeout(decimal defaultVisibilityTimeout)
        {
            this.defaultVisibilityTimeoutField = new decimal?(defaultVisibilityTimeout);
            return this;
        }

        public CreateQueueRequest WithQueueName(string queueName)
        {
            this.queueNameField = queueName;
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

        [XmlElement(ElementName="DefaultVisibilityTimeout")]
        public decimal DefaultVisibilityTimeout
        {
            get
            {
                return this.defaultVisibilityTimeoutField.GetValueOrDefault();
            }
            set
            {
                this.defaultVisibilityTimeoutField = new decimal?(value);
            }
        }

        [XmlElement(ElementName="QueueName")]
        public string QueueName
        {
            get
            {
                return this.queueNameField;
            }
            set
            {
                this.queueNameField = value;
            }
        }
    }
}

