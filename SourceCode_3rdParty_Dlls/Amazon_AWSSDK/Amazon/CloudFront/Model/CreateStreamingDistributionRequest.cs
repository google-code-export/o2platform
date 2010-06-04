namespace Amazon.CloudFront.Model
{
    using System;
    using System.Xml.Serialization;

    public class CreateStreamingDistributionRequest : CloudFrontRequest
    {
        public CreateStreamingDistributionRequest WithStreamingDistributionConfig(CloudFrontStreamingDistributionConfig distributionConfig)
        {
            base.sdConfig = distributionConfig;
            return this;
        }

        [XmlElement(ElementName="StreamingDistributionConfig")]
        public override CloudFrontStreamingDistributionConfig StreamingDistributionConfig
        {
            get
            {
                if (base.sdConfig == null)
                {
                    base.sdConfig = new CloudFrontStreamingDistributionConfig();
                }
                return base.sdConfig;
            }
            set
            {
                base.sdConfig = value;
            }
        }
    }
}

