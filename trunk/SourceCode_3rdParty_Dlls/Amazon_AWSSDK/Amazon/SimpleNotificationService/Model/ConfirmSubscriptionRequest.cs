namespace Amazon.SimpleNotificationService.Model
{
    using System;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://sns.amazonaws.com/doc/2010-03-31/", IsNullable=false)]
    public class ConfirmSubscriptionRequest
    {
        private string authenticateOnUnsubscribeField;
        private string tokenField;
        private string topicArnField;

        public bool IsSetAuthenticateOnUnsubscribe()
        {
            return (this.authenticateOnUnsubscribeField != null);
        }

        public bool IsSetToken()
        {
            return (this.tokenField != null);
        }

        public bool IsSetTopicArn()
        {
            return (this.topicArnField != null);
        }

        public ConfirmSubscriptionRequest WithAuthenticateOnUnsubscribe(string authenticateOnUnsubscribe)
        {
            this.authenticateOnUnsubscribeField = authenticateOnUnsubscribe;
            return this;
        }

        public ConfirmSubscriptionRequest WithToken(string token)
        {
            this.tokenField = token;
            return this;
        }

        public ConfirmSubscriptionRequest WithTopicArn(string topicArn)
        {
            this.topicArnField = topicArn;
            return this;
        }

        [XmlElement(ElementName="AuthenticateOnUnsubscribe")]
        public string AuthenticateOnUnsubscribe
        {
            get
            {
                return this.authenticateOnUnsubscribeField;
            }
            set
            {
                this.authenticateOnUnsubscribeField = value;
            }
        }

        [XmlElement(ElementName="Token")]
        public string Token
        {
            get
            {
                return this.tokenField;
            }
            set
            {
                this.tokenField = value;
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

