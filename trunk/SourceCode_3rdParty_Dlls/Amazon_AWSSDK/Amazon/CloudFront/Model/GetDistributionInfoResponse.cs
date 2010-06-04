namespace Amazon.CloudFront.Model
{
    using System;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://cloudfront.amazonaws.com/doc/2010-03-01/", IsNullable=false)]
    public class GetDistributionInfoResponse : CloudFrontResponse
    {
        private CloudFrontDistribution distribution;

        [XmlElement(ElementName="Distribution")]
        public CloudFrontDistribution Distribution
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

        [XmlElement(ElementName="ETag")]
        public override string ETag
        {
            set
            {
                base.etagHeader = value;
                if (this.Distribution != null)
                {
                    this.Distribution.ETag = value;
                }
            }
        }
    }
}

