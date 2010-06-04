namespace Amazon.SQS.Model
{
    using System;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://queue.amazonaws.com/doc/2009-02-01/", IsNullable=false)]
    public class RemovePermissionRequest
    {
        private string labelField;
        private string queueUrlField;

        public bool IsSetLabel()
        {
            return (this.labelField != null);
        }

        public bool IsSetQueueUrl()
        {
            return (this.queueUrlField != null);
        }

        public RemovePermissionRequest WithLabel(string label)
        {
            this.labelField = label;
            return this;
        }

        public RemovePermissionRequest WithQueueUrl(string queueUrl)
        {
            this.queueUrlField = queueUrl;
            return this;
        }

        [XmlElement(ElementName="Label")]
        public string Label
        {
            get
            {
                return this.labelField;
            }
            set
            {
                this.labelField = value;
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

