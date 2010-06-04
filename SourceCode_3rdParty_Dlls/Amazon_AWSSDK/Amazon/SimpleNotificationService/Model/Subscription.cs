namespace Amazon.SimpleNotificationService.Model
{
    using System;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://sns.amazonaws.com/doc/2010-03-31/", IsNullable=false)]
    public class Subscription
    {
        private string endpointField;
        private string ownerField;
        private string protocolField;
        private string subscriptionArnField;
        private string topicArnField;

        public bool IsSetEndpoint()
        {
            return (this.endpointField != null);
        }

        public bool IsSetOwner()
        {
            return (this.ownerField != null);
        }

        public bool IsSetProtocol()
        {
            return (this.protocolField != null);
        }

        public bool IsSetSubscriptionArn()
        {
            return (this.subscriptionArnField != null);
        }

        public bool IsSetTopicArn()
        {
            return (this.topicArnField != null);
        }

        public Subscription WithEndpoint(string endpoint)
        {
            this.endpointField = endpoint;
            return this;
        }

        public Subscription WithOwner(string owner)
        {
            this.ownerField = owner;
            return this;
        }

        public Subscription WithProtocol(string protocol)
        {
            this.protocolField = protocol;
            return this;
        }

        public Subscription WithSubscriptionArn(string subscriptionArn)
        {
            this.subscriptionArnField = subscriptionArn;
            return this;
        }

        public Subscription WithTopicArn(string topicArn)
        {
            this.topicArnField = topicArn;
            return this;
        }

        [XmlElement(ElementName="Endpoint")]
        public string Endpoint
        {
            get
            {
                return this.endpointField;
            }
            set
            {
                this.endpointField = value;
            }
        }

        [XmlElement(ElementName="Owner")]
        public string Owner
        {
            get
            {
                return this.ownerField;
            }
            set
            {
                this.ownerField = value;
            }
        }

        [XmlElement(ElementName="Protocol")]
        public string Protocol
        {
            get
            {
                return this.protocolField;
            }
            set
            {
                this.protocolField = value;
            }
        }

        [XmlElement(ElementName="SubscriptionArn")]
        public string SubscriptionArn
        {
            get
            {
                return this.subscriptionArnField;
            }
            set
            {
                this.subscriptionArnField = value;
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

