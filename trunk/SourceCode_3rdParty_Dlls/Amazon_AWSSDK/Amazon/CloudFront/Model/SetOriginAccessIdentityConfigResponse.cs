namespace Amazon.CloudFront.Model
{
    using System;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://cloudfront.amazonaws.com/doc/2010-03-01/", IsNullable=false)]
    public class SetOriginAccessIdentityConfigResponse : GetOriginAccessIdentityInfoResponse
    {
        [XmlElement(ElementName="ETag")]
        public override string ETag
        {
            set
            {
                base.etagHeader = value;
                CloudFrontOriginAccessIdentityConfig originAccessIdentityConfig = base.OriginAccessIdentity.OriginAccessIdentityConfig;
                if (originAccessIdentityConfig != null)
                {
                    originAccessIdentityConfig.ETag = value;
                }
            }
        }
    }
}

