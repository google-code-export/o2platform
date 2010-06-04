namespace Amazon.CloudFront.Model
{
    using System;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://cloudfront.amazonaws.com/doc/2010-03-01/", IsNullable=false)]
    public class SetStreamingDistributionConfigResponse : GetStreamingDistributionInfoResponse
    {
        [XmlElement(ElementName="ETag")]
        public override string ETag
        {
            set
            {
                base.etagHeader = value;
                CloudFrontStreamingDistributionConfig streamingDistributionConfig = base.StreamingDistribution.StreamingDistributionConfig;
                if (streamingDistributionConfig != null)
                {
                    streamingDistributionConfig.ETag = value;
                }
            }
        }
    }
}

