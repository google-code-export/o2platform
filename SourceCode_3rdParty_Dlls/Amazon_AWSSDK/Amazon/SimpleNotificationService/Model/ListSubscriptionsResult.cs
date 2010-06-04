namespace Amazon.SimpleNotificationService.Model
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://sns.amazonaws.com/doc/2010-03-31/", IsNullable=false)]
    public class ListSubscriptionsResult
    {
        private string nextTokenField;
        private List<Subscription> subscriptionsField;

        public bool IsSetNextToken()
        {
            return (this.nextTokenField != null);
        }

        public bool IsSetSubscriptions()
        {
            return (this.Subscriptions.Count > 0);
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

        [XmlElement(ElementName="Subscriptions")]
        public List<Subscription> Subscriptions
        {
            get
            {
                if (this.subscriptionsField == null)
                {
                    this.subscriptionsField = new List<Subscription>();
                }
                return this.subscriptionsField;
            }
            set
            {
                this.subscriptionsField = value;
            }
        }
    }
}

