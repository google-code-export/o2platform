namespace Amazon.CloudFront.Model
{
    using System;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://cloudfront.amazonaws.com/doc/2010-03-01/", IsNullable=false)]
    public class GetDistributionConfigResponse : CloudFrontResponse
    {
        private CloudFrontDistributionConfig distributionConfig;

        [XmlElement(ElementName="DistributionConfig")]
        public CloudFrontDistributionConfig DistributionConfig
        {
            get
            {
                return this.distributionConfig;
            }
            set
            {
                this.distributionConfig = value;
            }
        }

        [XmlElement(ElementName="ETag")]
        public override string ETag
        {
            set
            {
                base.etagHeader = value;
                if (this.distributionConfig != null)
                {
                    this.distributionConfig.ETag = value;
                }
            }
        }
    }
}

