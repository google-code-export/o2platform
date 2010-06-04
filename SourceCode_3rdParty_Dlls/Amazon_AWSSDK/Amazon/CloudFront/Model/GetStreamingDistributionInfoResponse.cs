namespace Amazon.CloudFront.Model
{
    using System;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://cloudfront.amazonaws.com/doc/2010-03-01/", IsNullable=false)]
    public class GetStreamingDistributionInfoResponse : CloudFrontResponse
    {
        private CloudFrontStreamingDistribution distribution;

        [XmlElement(ElementName="ETag")]
        public override string ETag
        {
            set
            {
                base.etagHeader = value;
                if (this.StreamingDistribution != null)
                {
                    this.StreamingDistribution.ETag = value;
                }
            }
        }

        [XmlElement(ElementName="StreamingDistribution")]
        public CloudFrontStreamingDistribution StreamingDistribution
        {
            get
            {
                return this.distribution;
            }
            set
            {
                this.distribution = value;
            }
        }
    }
}

