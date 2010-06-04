namespace Amazon.SimpleNotificationService.Model
{
    using System;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://sns.amazonaws.com/doc/2010-03-31/", IsNullable=false)]
    public class UnsubscribeRequest
    {
        private string subscriptionArnField;

        public bool IsSetSubscriptionArn()
        {
            return (this.subscriptionArnField != null);
        }

        public UnsubscribeRequest WithSubscriptionArn(string subscriptionArn)
        {
            this.subscriptionArnField = subscriptionArn;
            return this;
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
    }
}

