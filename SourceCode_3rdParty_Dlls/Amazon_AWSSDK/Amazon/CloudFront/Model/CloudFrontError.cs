namespace Amazon.CloudFront.Model
{
    using System;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://cloudfront.amazonaws.com/doc/2010-03-01/", IsNullable=false)]
    public class CloudFrontError
    {
        private string code;
        private string message;
        private string requestId;
        private string type;

        internal bool IsSetCode()
        {
            return (this.code != null);
        }

        public CloudFrontError WithCode(string code)
        {
            this.code = code;
            return this;
        }

        [XmlElement(ElementName="Code")]
        public string Code
        {
            get
            {
                return this.code;
            }
            set
            {
                this.code = value;
            }
        }

        [XmlElement(ElementName="Message")]
        public string Message
        {
            get
            {
                return this.message;
            }
            set
            {
                this.message = value;
            }
        }

        [XmlElement(ElementName="RequestId")]
        public string RequestId
        {
            get
            {
                return this.requestId;
            }
            set
            {
                this.requestId = value;
            }
        }

        [XmlElement(ElementName="Type")]
        public string Type
        {
            get
            {
                return this.type;
            }
            set
            {
                this.type = value;
            }
        }
    }
}

