namespace Amazon.CloudFront.Model
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Xml.Serialization;

    [Serializable, XmlRoot(Namespace="http://cloudfront.amazonaws.com/doc/2010-03-01/", IsNullable=false)]
    public class CloudFrontDistributionBase
    {
        private List<Signer> activeTrustedSigners;
        private string domainName;
        private string eTag;
        private string id;
        private DateTime? lastModifiedTime;
        private string status;

        internal bool IsSetActiveTrustedSigners()
        {
            return ((this.ActiveTrustedSigners != null) && (this.ActiveTrustedSigners.Count > 0));
        }

        internal virtual bool IsSetDomainName()
        {
            return !string.IsNullOrEmpty(this.domainName);
        }

        internal bool IsSetETag()
        {
            return !string.IsNullOrEmpty(this.eTag);
        }

        internal virtual bool IsSetId()
        {
            return !string.IsNullOrEmpty(this.id);
        }

        internal virtual bool IsSetLastModifiedTime()
        {
            return this.lastModifiedTime.HasValue;
        }

        internal virtual bool IsSetStatus()
        {
            return !string.IsNullOrEmpty(this.status);
        }

        [XmlElement(ElementName="Signer")]
        public List<Signer> ActiveTrustedSigners
        {
            get
            {
                return this.activeTrustedSigners;
            }
            set
            {
                this.activeTrustedSigners = value;
            }
        }

        [XmlElement(ElementName="DomainName")]
        public virtual string DomainName
        {
            get
            {
                return this.domainName;
            }
            set
            {
                this.domainName = value;
            }
        }

        [XmlElement(ElementName="ETag")]
        public virtual string ETag
        {
            get
            {
                return this.eTag;
            }
            set
            {
                this.eTag = value;
            }
        }

        [XmlElement(ElementName="Id")]
        public virtual string Id
        {
            get
            {
                return this.id;
            }
            set
            {
                this.id = value;
            }
        }

        [XmlElement(ElementName="LastModifiedTime")]
        public virtual string LastModifiedTime
        {
            get
            {
                return this.lastModifiedTime.GetValueOrDefault().ToString(@"ddd, dd MMM yyyy HH:mm:ss \G\M\T");
            }
            set
            {
                this.lastModifiedTime = new DateTime?(DateTime.ParseExact(value, @"yyyy-MM-dd\THH:mm:ss.fff\Z", CultureInfo.InvariantCulture));
            }
        }

        [XmlElement(ElementName="Status")]
        public virtual string Status
        {
            get
            {
                return this.status;
            }
            set
            {
                this.status = value;
            }
        }
    }
}

