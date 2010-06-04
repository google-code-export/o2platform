namespace Amazon.CloudFront.Model
{
    using System;
    using System.IO;
    using System.Text;
    using System.Xml.Serialization;

    [Serializable, XmlRoot(Namespace="http://cloudfront.amazonaws.com/doc/2010-03-01/", IsNullable=false)]
    public class CloudFrontStreamingDistribution : CloudFrontDistributionBase
    {
        private CloudFrontStreamingDistributionConfig config;

        internal bool IsSetConfig()
        {
            return (this.config != null);
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder(0x400);
            XmlSerializer serializer = new XmlSerializer(base.GetType());
            using (StringWriter writer = new StringWriter(sb))
            {
                serializer.Serialize((TextWriter) writer, this);
            }
            return sb.ToString();
        }

        [XmlElement(ElementName="StreamingDistributionConfig")]
        public CloudFrontStreamingDistributionConfig StreamingDistributionConfig
        {
            get
            {
                return this.config;
            }
            set
            {
                this.config = value;
            }
        }
    }
}

