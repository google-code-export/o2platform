namespace Amazon.CloudFront.Model
{
    using System;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://cloudfront.amazonaws.com/doc/2010-03-01/", IsNullable=false)]
    public class GetOriginAccessIdentityInfoResponse : CloudFrontResponse
    {
        private CloudFrontOriginAccessIdentity originAccessIdentity;

        [XmlElement(ElementName="ETag")]
        public override string ETag
        {
            set
            {
                base.etagHeader = value;
                if (this.originAccessIdentity != null)
                {
                    this.originAccessIdentity.ETag = value;
                }
            }
        }

        [XmlElement(ElementName="OriginAccessIdentity")]
        public CloudFrontOriginAccessIdentity OriginAccessIdentity
        {
            get
            {
                return this.originAccessIdentity;
            }
            set
            {
                this.originAccessIdentity = value;
            }
        }
    }
}

