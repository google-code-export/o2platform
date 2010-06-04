namespace Amazon.CloudFront.Model
{
    using System;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://cloudfront.amazonaws.com/doc/2010-03-01/", IsNullable=false)]
    public class SetDistributionConfigResponse : GetDistributionInfoResponse
    {
        [XmlElement(ElementName="ETag")]
        public override string ETag
        {
            set
            {
                base.etagHeader = value;
                CloudFrontDistributionConfig distributionConfig = base.Distribution.DistributionConfig;
                if (distributionConfig != null)
                {
                    distributionConfig.ETag = value;
                }
            }
        }
    }
}

