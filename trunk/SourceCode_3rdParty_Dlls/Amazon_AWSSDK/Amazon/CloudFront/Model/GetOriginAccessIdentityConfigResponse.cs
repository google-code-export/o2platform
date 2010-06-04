namespace Amazon.CloudFront.Model
{
    using System;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://cloudfront.amazonaws.com/doc/2010-03-01/", IsNullable=false)]
    public class GetOriginAccessIdentityConfigResponse : CloudFrontResponse
    {
        private CloudFrontOriginAccessIdentityConfig originAccessIdentityConfig;

        [XmlElement(ElementName="ETag")]
        public override string ETag
        {
            set
            {
                base.etagHeader = value;
                if (this.OriginAccessIdentityConfig != null)
                {
                    this.OriginAccessIdentityConfig.ETag = value;
                }
            }
        }

        [XmlElement(ElementName="OriginAccessIdentityConfig")]
        public CloudFrontOriginAccessIdentityConfig OriginAccessIdentityConfig
        {
            get
            {
                return this.originAccessIdentityConfig;
            }
            set
            {
                this.originAccessIdentityConfig = value;
            }
        }
    }
}

