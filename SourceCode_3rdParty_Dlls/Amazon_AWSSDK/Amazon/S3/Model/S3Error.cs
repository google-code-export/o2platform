namespace Amazon.S3.Model
{
    using System;
    using System.Xml.Serialization;

    public class S3Error
    {
        private string code;
        private string hostId;
        private string message;
        private string requestId;

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

        [XmlElement(ElementName="HostId")]
        public string HostId
        {
            get
            {
                return this.hostId;
            }
            set
            {
                this.hostId = value;
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
    }
}

