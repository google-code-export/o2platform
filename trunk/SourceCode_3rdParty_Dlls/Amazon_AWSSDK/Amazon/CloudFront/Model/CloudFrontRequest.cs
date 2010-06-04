namespace Amazon.CloudFront.Model
{
    using System;
    using System.Collections.Specialized;
    using System.Net;
    using System.Xml.Serialization;

    public abstract class CloudFrontRequest
    {
        protected CloudFrontDistributionConfig dConfig;
        protected string distId;
        protected string etagHeader;
        protected WebHeaderCollection headers;
        protected string reqMarker;
        protected string reqMaxItems;
        protected CloudFrontStreamingDistributionConfig sdConfig;

        protected CloudFrontRequest()
        {
        }

        public void AddHeaders(NameValueCollection collection)
        {
            this.Headers.Add(collection);
        }

        internal bool IsSetDistributionConfig()
        {
            return (this.DistributionConfig != null);
        }

        internal bool IsSetETag()
        {
            return !string.IsNullOrEmpty(this.etagHeader);
        }

        internal bool IsSetHeaders()
        {
            return ((this.headers != null) && (this.headers.Count > 0));
        }

        internal bool IsSetId()
        {
            return !string.IsNullOrEmpty(this.distId);
        }

        internal bool IsSetMarker()
        {
            return !string.IsNullOrEmpty(this.reqMarker);
        }

        internal bool IsSetMaxItems()
        {
            return !string.IsNullOrEmpty(this.reqMaxItems);
        }

        internal bool IsSetStreamingDistributionConfig()
        {
            return (this.StreamingDistributionConfig != null);
        }

        [XmlElement(ElementName="DistributionConfig")]
        public virtual CloudFrontDistributionConfig DistributionConfig
        {
            get
            {
                return null;
            }
            set
            {
            }
        }

        public virtual string ETag
        {
            get
            {
                return null;
            }
            set
            {
            }
        }

        internal WebHeaderCollection Headers
        {
            get
            {
                if (this.headers == null)
                {
                    this.headers = new WebHeaderCollection();
                }
                return this.headers;
            }
        }

        [XmlElement(ElementName="Id")]
        public virtual string Id
        {
            get
            {
                return null;
            }
            set
            {
            }
        }

        [XmlElement(ElementName="Marker")]
        public virtual string Marker
        {
            get
            {
                return null;
            }
            set
            {
            }
        }

        [XmlElement(ElementName="MaxItems")]
        public virtual string MaxItems
        {
            get
            {
                return null;
            }
            set
            {
            }
        }

        [XmlElement(ElementName="StreamingDistributionConfig")]
        public virtual CloudFrontStreamingDistributionConfig StreamingDistributionConfig
        {
            get
            {
                return null;
            }
            set
            {
            }
        }
    }
}

