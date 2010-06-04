namespace Amazon.SQS.Model
{
    using System;
    using System.Collections.Generic;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://queue.amazonaws.com/doc/2009-02-01/", IsNullable=false)]
    public class ListQueuesRequest
    {
        private List<Amazon.SQS.Model.Attribute> attributeField;
        private string queueNamePrefixField;

        public bool IsSetAttribute()
        {
            return (this.Attribute.Count > 0);
        }

        public bool IsSetQueueNamePrefix()
        {
            return (this.queueNamePrefixField != null);
        }

        public ListQueuesRequest WithAttribute(params Amazon.SQS.Model.Attribute[] list)
        {
            foreach (Amazon.SQS.Model.Attribute attribute in list)
            {
                this.Attribute.Add(attribute);
            }
            return this;
        }

        public ListQueuesRequest WithQueueNamePrefix(string queueNamePrefix)
        {
            this.queueNamePrefixField = queueNamePrefix;
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

        [XmlElement(ElementName="QueueNamePrefix")]
        public string QueueNamePrefix
        {
            get
            {
                return this.queueNamePrefixField;
            }
            set
            {
                this.queueNamePrefixField = value;
            }
        }
    }
}

