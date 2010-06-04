namespace Amazon.SQS.Model
{
    using System;
    using System.Collections.Generic;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://queue.amazonaws.com/doc/2009-02-01/", IsNullable=false)]
    public class ListQueuesResult
    {
        private List<string> queueUrlField;

        public bool IsSetQueueUrl()
        {
            return (this.QueueUrl.Count > 0);
        }

        public ListQueuesResult WithQueueUrl(params string[] list)
        {
            foreach (string str in list)
            {
                this.QueueUrl.Add(str);
            }
            return this;
        }

        [XmlElement(ElementName="QueueUrl")]
        public List<string> QueueUrl
        {
            get
            {
                if (this.queueUrlField == null)
                {
                    this.queueUrlField = new List<string>();
                }
                return this.queueUrlField;
            }
            set
            {
                this.queueUrlField = value;
            }
        }
    }
}

