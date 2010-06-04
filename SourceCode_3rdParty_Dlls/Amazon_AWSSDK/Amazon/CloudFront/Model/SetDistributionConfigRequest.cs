namespace Amazon.CloudFront.Model
{
    using System;
    using System.Xml.Serialization;

    public class SetDistributionConfigRequest : CloudFrontRequest
    {
        public SetDistributionConfigRequest WithDistributionConfig(CloudFrontDistributionConfig distributionConfig)
        {
            base.dConfig = distributionConfig;
            return this;
        }

        public SetDistributionConfigRequest WithETag(string etag)
        {
            base.etagHeader = etag;
            return this;
        }

        public SetDistributionConfigRequest WithId(string id)
        {
            base.distId = id;
            return this;
        }

        [XmlElement(ElementName="DistributionConfig")]
        public override CloudFrontDistributionConfig DistributionConfig
        {
            get
            {
                return base.dConfig;
            }
            set
            {
                base.dConfig = value;
            }
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
    }
}

