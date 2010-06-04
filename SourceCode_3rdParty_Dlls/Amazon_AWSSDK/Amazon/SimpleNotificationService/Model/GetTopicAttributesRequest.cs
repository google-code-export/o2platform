namespace Amazon.SimpleNotificationService.Model
{
    using System;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://sns.amazonaws.com/doc/2010-03-31/", IsNullable=false)]
    public class GetTopicAttributesRequest
    {
        private string topicArnField;

        public bool IsSetTopicArn()
        {
            return (this.topicArnField != null);
        }

        public GetTopicAttributesRequest WithTopicArn(string topicArn)
        {
            this.topicArnField = topicArn;
            return this;
        }

        [XmlElement(ElementName="TopicArn")]
        public string TopicArn
        {
            get
            {
                return this.topicArnField;
            }
            set
            {
                this.topicArnField = value;
            }
        }
    }
}

