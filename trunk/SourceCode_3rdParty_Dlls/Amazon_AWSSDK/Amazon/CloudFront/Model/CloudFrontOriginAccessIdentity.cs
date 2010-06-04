namespace Amazon.CloudFront.Model
{
    using Amazon.CloudFront;
    using System;
    using System.Xml.Serialization;

    [Serializable, XmlRoot(Namespace="http://cloudfront.amazonaws.com/doc/2010-03-01/", IsNullable=false)]
    public class CloudFrontOriginAccessIdentity
    {
        private string canonicalUserId;
        private CloudFrontOriginAccessIdentityConfig config;
        private string eTag;
        private string id;

        internal bool IsSetConfig()
        {
            return (this.config != null);
        }

        internal bool IsSetETag()
        {
            return !string.IsNullOrEmpty(this.eTag);
        }

        internal bool IsSetId()
        {
            return !string.IsNullOrEmpty(this.id);
        }

        internal bool IsSetS3CanonicalUserId()
        {
            return !string.IsNullOrEmpty(this.canonicalUserId);
        }

        public override string ToString()
        {
            if (!this.IsSetId())
            {
                throw new AmazonCloudFrontException("A CloudFront Origin Access Identity object has no ID!");
            }
            return ("origin-access-identity/cloudfront/" + this.Id);
        }

        [XmlElement(ElementName="ETag")]
        public string ETag
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
        public string Id
        {
            get
            {
                return this.id;
            }
            set
            {
                this.id = value;
                if (this.id.Contains("/"))
                {
                    string[] strArray = this.id.Split(new char[] { '/' });
                    this.id = strArray[strArray.Length - 1];
                }
            }
        }

        [XmlElement(ElementName="OriginAccessIdentityConfig")]
        public CloudFrontOriginAccessIdentityConfig OriginAccessIdentityConfig
        {
            get
            {
                if (this.config == null)
                {
                    this.config = new CloudFrontOriginAccessIdentityConfig();
                }
                return this.config;
            }
            set
            {
                this.config = value;
            }
        }

        [XmlElement(ElementName="S3CanonicalUserId")]
        public string S3CanonicalUserId
        {
            get
            {
                return this.canonicalUserId;
            }
            set
            {
                this.canonicalUserId = value;
            }
        }
    }
}

