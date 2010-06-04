namespace Amazon.CloudFront.Model
{
    using System;
    using System.Xml.Serialization;

    public class CreateOriginAccessIdentityRequest : CloudFrontRequest
    {
        private CloudFrontOriginAccessIdentityConfig oaiConfig;

        public CreateOriginAccessIdentityRequest WithOriginAccessIdentityConfig(CloudFrontOriginAccessIdentityConfig oaiConfig)
        {
            this.oaiConfig = oaiConfig;
            return this;
        }

        [XmlElement(ElementName="OriginAccessIdentityConfig")]
        public CloudFrontOriginAccessIdentityConfig OriginAccessIdentityConfig
        {
            get
            {
                if (this.oaiConfig == null)
                {
                    this.oaiConfig = new CloudFrontOriginAccessIdentityConfig();
                }
                return this.oaiConfig;
            }
            set
            {
                this.oaiConfig = value;
            }
        }
    }
}

