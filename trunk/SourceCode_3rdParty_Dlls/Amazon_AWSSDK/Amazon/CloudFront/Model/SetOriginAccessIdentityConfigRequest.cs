namespace Amazon.CloudFront.Model
{
    using System;
    using System.Xml.Serialization;

    public class SetOriginAccessIdentityConfigRequest : CloudFrontRequest
    {
        private CloudFrontOriginAccessIdentityConfig oaiConfig;

        internal bool IsSetOriginAccessIdentityConfig()
        {
            return (this.OriginAccessIdentityConfig != null);
        }

        public SetOriginAccessIdentityConfigRequest WithETag(string etag)
        {
            base.etagHeader = etag;
            return this;
        }

        public SetOriginAccessIdentityConfigRequest WithId(string id)
        {
            base.distId = id;
            return this;
        }

        public SetOriginAccessIdentityConfigRequest WithOriginAccessIdentityConfig(CloudFrontOriginAccessIdentityConfig config)
        {
            this.oaiConfig = config;
            return this;
        }

        public override string ETag
        {
            get
            {
                return base.etagHeader;
            }
            set
            {
                base.etagHeader = value;
            }
        }

        [XmlElement(ElementName="Id")]
        public override string Id
        {
            get
            {
                return base.distId;
            }
            set
            {
                base.distId = value;
            }
        }

        [XmlElement(ElementName="OriginAccessIdentityConfig")]
        public CloudFrontOriginAccessIdentityConfig OriginAccessIdentityConfig
        {
            get
            {
                return this.oaiConfig;
            }
            set
            {
                this.oaiConfig = value;
            }
        }
    }
}

