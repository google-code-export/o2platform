namespace Amazon.SimpleNotificationService.Model
{
    using System;
    using System.IO;
    using System.Text;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://sns.amazonaws.com/doc/2010-03-31/", IsNullable=false)]
    public class SubscribeResult
    {
        private string subscriptionArnField;

        public bool IsSetSubscriptionArn()
        {
            return (this.subscriptionArnField != null);
        }

        public override string ToString()
        {
            return this.ToXML();
        }

        public string ToXML()
        {
            StringBuilder sb = new StringBuilder(0x400);
            XmlSerializer serializer = new XmlSerializer(base.GetType());
            using (StringWriter writer = new StringWriter(sb))
            {
                serializer.Serialize((TextWriter) writer, this);
            }
            return sb.ToString();
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

