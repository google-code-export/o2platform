namespace Amazon.SimpleNotificationService.Model
{
    using System;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://sns.amazonaws.com/doc/2010-03-31/", IsNullable=false)]
    public class SetTopicAttributesRequest
    {
        private string attributeNameField;
        private string attributeValueField;
        private string topicArnField;

        public bool IsSetAttributeName()
        {
            return (this.attributeNameField != null);
        }

        public bool IsSetAttributeValue()
        {
            return (this.attributeValueField != null);
        }

        public bool IsSetTopicArn()
        {
            return (this.topicArnField != null);
        }

        public SetTopicAttributesRequest WithAttributeName(string attributeName)
        {
            this.attributeNameField = attributeName;
            return this;
        }

        public SetTopicAttributesRequest WithAttributeValue(string attributeValue)
        {
            this.attributeValueField = attributeValue;
            return this;
        }

        public SetTopicAttributesRequest WithTopicArn(string topicArn)
        {
            this.topicArnField = topicArn;
            return this;
        }

        [XmlElement(ElementName="AttributeName")]
        public string AttributeName
        {
            get
            {
                return this.attributeNameField;
            }
            set
            {
                this.attributeNameField = value;
            }
        }

        [XmlElement(ElementName="AttributeValue")]
        public string AttributeValue
        {
            get
            {
                return this.attributeValueField;
            }
            set
            {
                this.attributeValueField = value;
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

