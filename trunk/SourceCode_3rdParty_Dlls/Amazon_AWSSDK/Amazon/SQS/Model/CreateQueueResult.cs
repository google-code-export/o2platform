namespace Amazon.SQS.Model
{
    using System;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://queue.amazonaws.com/doc/2009-02-01/", IsNullable=false)]
    public class CreateQueueResult
    {
        private string queueUrlField;

        public bool IsSetQueueUrl()
        {
            return (this.queueUrlField != null);
        }

        public CreateQueueResult WithQueueUrl(string queueUrl)
        {
            this.queueUrlField = queueUrl;
            return this;
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

