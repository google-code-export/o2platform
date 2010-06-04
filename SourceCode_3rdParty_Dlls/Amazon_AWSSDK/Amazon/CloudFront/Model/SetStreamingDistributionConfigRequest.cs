namespace Amazon.CloudFront.Model
{
    using System;
    using System.Xml.Serialization;

    public class SetStreamingDistributionConfigRequest : CloudFrontRequest
    {
        public SetStreamingDistributionConfigRequest WithETag(string etag)
        {
            base.etagHeader = etag;
            return this;
        }

        public SetStreamingDistributionConfigRequest WithId(string id)
        {
            base.distId = id;
            return this;
        }

        public SetStreamingDistributionConfigRequest WithStreamingDistributionConfig(CloudFrontStreamingDistributionConfig config)
        {
            base.sdConfig = config;
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

        [XmlElement(ElementName="StreamingDistributionConfig")]
        public override CloudFrontStreamingDistributionConfig StreamingDistributionConfig
        {
            get
            {
                return base.sdConfig;
            }
            set
            {
                base.sdConfig = value;
            }
        }
    }
}

