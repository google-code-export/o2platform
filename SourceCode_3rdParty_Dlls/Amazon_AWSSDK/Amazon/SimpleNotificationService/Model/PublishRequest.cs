namespace Amazon.SimpleNotificationService.Model
{
    using System;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://sns.amazonaws.com/doc/2010-03-31/", IsNullable=false)]
    public class PublishRequest
    {
        private string messageField;
        private string subjectField;
        private string topicArnField;

        public bool IsSetMessage()
        {
            return (this.messageField != null);
        }

        public bool IsSetSubject()
        {
            return (this.subjectField != null);
        }

        public bool IsSetTopicArn()
        {
            return (this.topicArnField != null);
        }

        public PublishRequest WithMessage(string message)
        {
            this.messageField = message;
            return this;
        }

        public PublishRequest WithSubject(string subject)
        {
            this.subjectField = subject;
            return this;
        }

        public PublishRequest WithTopicArn(string topicArn)
        {
            this.topicArnField = topicArn;
            return this;
        }

        [XmlElement(ElementName="Message")]
        public string Message
        {
            get
            {
                return this.messageField;
            }
            set
            {
                this.messageField = value;
            }
        }

        [XmlElement(ElementName="Subject")]
        public string Subject
        {
            get
            {
                return this.subjectField;
            }
            set
            {
                this.subjectField = value;
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

