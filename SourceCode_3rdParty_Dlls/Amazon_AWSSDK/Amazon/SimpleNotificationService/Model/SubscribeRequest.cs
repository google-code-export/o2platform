namespace Amazon.SimpleNotificationService.Model
{
    using System;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://sns.amazonaws.com/doc/2010-03-31/", IsNullable=false)]
    public class SubscribeRequest
    {
        private string endpointField;
        private string protocolField;
        private string topicArnField;

        public bool IsSetEndpoint()
        {
            return (this.endpointField != null);
        }

        public bool IsSetProtocol()
        {
            return (this.protocolField != null);
        }

        public bool IsSetTopicArn()
        {
            return (this.topicArnField != null);
        }

        public SubscribeRequest WithEndpoint(string endpoint)
        {
            this.endpointField = endpoint;
            return this;
        }

        public SubscribeRequest WithProtocol(string protocol)
        {
            this.protocolField = protocol;
            return this;
        }

        public SubscribeRequest WithTopicArn(string topicArn)
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

