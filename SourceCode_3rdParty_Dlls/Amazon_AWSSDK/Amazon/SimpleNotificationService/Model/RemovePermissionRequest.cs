namespace Amazon.SimpleNotificationService.Model
{
    using System;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://sns.amazonaws.com/doc/2010-03-31/", IsNullable=false)]
    public class RemovePermissionRequest
    {
        private string labelField;
        private string topicArnField;

        public bool IsSetLabel()
        {
            return (this.labelField != null);
        }

        public bool IsSetTopicArn()
        {
            return (this.topicArnField != null);
        }

        public RemovePermissionRequest WithLabel(string label)
        {
            this.labelField = label;
            return this;
        }

        public RemovePermissionRequest WithTopicArn(string topicArn)
        {
            this.topicArnField = topicArn;
            return this;
        }

        [XmlElement(ElementName="Label")]
        public string Label
        {
            get
            {
                return this.labelField;
            }
            set
            {
                this.labelField = value;
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

