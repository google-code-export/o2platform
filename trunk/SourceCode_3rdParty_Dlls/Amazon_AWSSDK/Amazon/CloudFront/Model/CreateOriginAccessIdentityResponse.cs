namespace Amazon.CloudFront.Model
{
    using Amazon.CloudFront;
    using System;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://cloudfront.amazonaws.com/doc/2010-03-01/", IsNullable=false)]
    public class CreateOriginAccessIdentityResponse : CloudFrontResponse
    {
        private string location;
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

        [XmlElement(ElementName="Location")]
        public string Location
        {
            get
            {
                if ((this.location == null) && (this.Headers != null))
                {
                    string str = this.Headers.Get("Location");
                    if (str == null)
                    {
                        throw new AmazonCloudFrontException("No Location retrieved for newly created CloudFront Origin Access Identity");
                    }
                    this.location = str;
                }
                return this.location;
            }
            set
            {
                this.location = value;
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

