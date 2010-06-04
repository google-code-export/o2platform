namespace Amazon.SimpleNotificationService.Model
{
    using System;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://sns.amazonaws.com/doc/2010-03-31/", IsNullable=false)]
    public class ListSubscriptionsByTopicRequest
    {
        private string nextTokenField;
        private string topicArnField;

        public bool IsSetNextToken()
        {
            return (this.nextTokenField != null);
        }

        public bool IsSetTopicArn()
        {
            return (this.topicArnField != null);
        }

        public ListSubscriptionsByTopicRequest WithNextToken(string nextToken)
        {
            this.nextTokenField = nextToken;
            return this;
        }

        public ListSubscriptionsByTopicRequest WithTopicArn(string topicArn)
        {
            this.topicArnField = topicArn;
            return this;
        }

        [XmlElement(ElementName="NextToken")]
        public string NextToken
        {
            get
            {
                return this.nextTokenField;
            }
            set
            {
                this.nextTokenField = value;
            }
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

