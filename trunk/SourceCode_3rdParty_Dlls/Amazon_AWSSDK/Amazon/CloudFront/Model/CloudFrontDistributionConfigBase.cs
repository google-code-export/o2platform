namespace Amazon.CloudFront.Model
{
    using Amazon.CloudFront;
    using Amazon.S3.Util;
    using System;
    using System.Collections.Generic;
    using System.Xml.Serialization;

    [Serializable]
    public class CloudFrontDistributionConfigBase
    {
        private string callerReference = DateTime.UtcNow.ToString();
        private List<string> cnames = new List<string>();
        private string comment;
        private bool enabled;
        private string eTag;
        private CloudFrontOriginAccessIdentity identity;
        private string origin;
        private UrlTrustedSigners trustedSigners;

        internal virtual bool IsSetCallerReference()
        {
            return !string.IsNullOrEmpty(this.callerReference);
        }

        internal virtual bool IsSetCNames()
        {
            return ((this.CNAME != null) && (this.CNAME.Count > 0));
        }

        internal virtual bool IsSetComment()
        {
            return !string.IsNullOrEmpty(this.comment);
        }

        internal bool IsSetETag()
        {
            return !string.IsNullOrEmpty(this.eTag);
        }

        internal virtual bool IsSetOrigin()
        {
            return !string.IsNullOrEmpty(this.origin);
        }

        internal bool IsSetOriginAccessIdentity()
        {
            return (this.identity != null);
        }

        internal bool IsSetTrustedSigners()
        {
            return (this.trustedSigners != null);
        }

        [XmlElement(ElementName="CallerReference")]
        public virtual string CallerReference
        {
            get
            {
                return this.callerReference;
            }
            set
            {
                this.callerReference = value;
            }
        }

        [XmlElement(ElementName="CNAME")]
        public virtual List<string> CNAME
        {
            get
            {
                if (this.cnames == null)
                {
                    this.cnames = new List<string>();
                }
                return this.cnames;
            }
        }

        [XmlElement(ElementName="Comment")]
        public virtual string Comment
        {
            get
            {
                return this.comment;
            }
            set
            {
                this.comment = value;
            }
        }

        [XmlElement(ElementName="Enabled")]
        public virtual bool Enabled
        {
            get
            {
                return this.enabled;
            }
            set
            {
                this.enabled = value;
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

        [XmlElement(ElementName="Origin")]
        public virtual string Origin
        {
            get
            {
                return this.origin;
            }
            set
            {
                if (!AmazonS3Util.ValidateV2Bucket(value))
                {
                    throw new AmazonCloudFrontException("Only Amazon S3 V2 style buckets are acceptable as Origin values");
                }
                this.origin = value;
            }
        }

        [XmlElement(ElementName="OriginAccessIdentity")]
        public CloudFrontOriginAccessIdentity OriginAccessIdentity
        {
            get
            {
                return this.identity;
            }
            set
            {
                this.identity = value;
            }
        }

        [XmlElement(ElementName="TrustedSigners")]
        public UrlTrustedSigners TrustedSigners
        {
            get
            {
                return this.trustedSigners;
            }
            set
            {
                this.trustedSigners = value;
            }
        }
    }
}

