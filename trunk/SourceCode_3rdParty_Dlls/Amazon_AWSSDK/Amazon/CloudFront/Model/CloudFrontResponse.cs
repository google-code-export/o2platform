namespace Amazon.CloudFront.Model
{
    using System;
    using System.Net;
    using System.Xml.Serialization;

    public class CloudFrontResponse
    {
        protected string etagHeader;
        private string requestId;
        protected WebHeaderCollection webHeaders;
        private string xml;

        [XmlElement(ElementName="ETag")]
        public virtual string ETag
        {
            get
            {
                return this.etagHeader;
            }
            set
            {
                this.etagHeader = value;
            }
        }

        [XmlIgnore]
        public virtual WebHeaderCollection Headers
        {
            get
            {
                if (this.webHeaders == null)
                {
                    this.webHeaders = new WebHeaderCollection();
                }
                return this.webHeaders;
            }
            set
            {
                this.webHeaders = value;
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

        [XmlIgnore]
        public string XML
        {
            get
            {
                return this.xml;
            }
            set
            {
                this.xml = value;
            }
        }
    }
}

