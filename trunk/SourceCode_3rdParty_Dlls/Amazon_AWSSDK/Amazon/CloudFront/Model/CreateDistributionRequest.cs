namespace Amazon.CloudFront.Model
{
    using System;
    using System.Xml.Serialization;

    public class CreateDistributionRequest : CloudFrontRequest
    {
        public CreateDistributionRequest WithDistributionConfig(CloudFrontDistributionConfig distributionConfig)
        {
            base.dConfig = distributionConfig;
            return this;
        }

        [XmlElement(ElementName="DistributionConfig")]
        public override CloudFrontDistributionConfig DistributionConfig
        {
            get
            {
                if (base.dConfig == null)
                {
                    base.dConfig = new CloudFrontDistributionConfig();
                }
                return base.dConfig;
            }
            set
            {
                base.dConfig = value;
            }
        }
    }
}

