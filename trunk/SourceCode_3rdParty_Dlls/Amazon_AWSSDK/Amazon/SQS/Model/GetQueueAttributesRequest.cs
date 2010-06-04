namespace Amazon.SQS.Model
{
    using System;
    using System.Collections.Generic;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://queue.amazonaws.com/doc/2009-02-01/", IsNullable=false)]
    public class GetQueueAttributesRequest
    {
        private List<string> attributeNameField;
        private string queueUrlField;

        public bool IsSetAttributeName()
        {
            return (this.AttributeName.Count > 0);
        }

        public bool IsSetQueueUrl()
        {
            return (this.queueUrlField != null);
        }

        public GetQueueAttributesRequest WithAttributeName(params string[] list)
        {
            foreach (string str in list)
            {
                this.AttributeName.Add(str);
            }
            return this;
        }

        public GetQueueAttributesRequest WithQueueUrl(string queueUrl)
        {
            this.queueUrlField = queueUrl;
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

