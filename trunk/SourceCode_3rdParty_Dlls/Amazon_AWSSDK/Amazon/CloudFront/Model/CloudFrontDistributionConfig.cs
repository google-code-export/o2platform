namespace Amazon.CloudFront.Model
{
    using System;
    using System.Text;
    using System.Xml.Serialization;

    [Serializable]
    public class CloudFrontDistributionConfig : CloudFrontDistributionConfigBase
    {
        [NonSerialized]
        private Tuple<string, string> logging;

        internal bool IsSetLogging()
        {
            return (((this.logging != null) && !string.IsNullOrEmpty(this.Logging.First)) && !string.IsNullOrEmpty(this.Logging.Second));
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder(0x400);
            builder.Append("<?xml version=\"1.0\" encoding=\"UTF-8\"?><DistributionConfig ");
            builder.Append("xmlns=\"http://cloudfront.amazonaws.com/doc/2010-03-01/\">");
            if (this.IsSetOrigin())
            {
                builder.Append("<Origin>");
                builder.Append(this.Origin.EndsWith(".s3.amazonaws.com") ? this.Origin : (this.Origin + ".s3.amazonaws.com"));
                builder.Append("</Origin>");
            }
            if (this.IsSetCallerReference())
            {
                builder.Append("<CallerReference>" + this.CallerReference + "</CallerReference>");
            }
            if (this.IsSetCNames())
            {
                foreach (string str in this.CNAME)
                {
                    if (!string.IsNullOrEmpty(str))
                    {
                        builder.Append("<CNAME>" + str + "</CNAME>");
                    }
                }
            }
            if (this.IsSetComment())
            {
                builder.Append("<Comment>" + this.Comment + "</Comment>");
            }
            builder.Append("<Enabled>" + (this.Enabled ? "true" : "false") + "</Enabled>");
            if (this.IsSetLogging())
            {
                builder.Append("<Logging>");
                if (!string.IsNullOrEmpty(this.Logging.First))
                {
                    builder.Append("<Bucket>" + this.Logging.First + "</Bucket>");
                }
                if (!string.IsNullOrEmpty(this.Logging.Second))
                {
                    builder.Append("<Prefix>" + this.Logging.Second + "</Prefix>");
                }
                else
                {
                    builder.Append("<Prefix/>");
                }
                builder.Append("</Logging>");
            }
            if (base.IsSetOriginAccessIdentity())
            {
                builder.Append("<OriginAccessIdentity>" + base.OriginAccessIdentity + "</OriginAccessIdentity>");
            }
            if (base.IsSetTrustedSigners())
            {
                builder.Append("<TrustedSigners>" + base.TrustedSigners + "</TrustedSigners>");
            }
            builder.Append("</DistributionConfig>");
            return builder.ToString();
        }

        public CloudFrontDistributionConfig WithCallerReference(string callerReference)
        {
            this.CallerReference = callerReference;
            return this;
        }

        public CloudFrontDistributionConfig WithCNames(params string[] cnames)
        {
            foreach (string str in cnames)
            {
                this.CNAME.Add(str);
            }
            return this;
        }

        public CloudFrontDistributionConfig WithComment(string comment)
        {
            this.Comment = comment;
            return this;
        }

        public CloudFrontDistributionConfig WithEnabled(bool enabled)
        {
            this.Enabled = enabled;
            return this;
        }

        public CloudFrontDistributionConfig WithLogging(string bucket, string prefix)
        {
            if (string.IsNullOrEmpty(bucket))
            {
                throw new ArgumentNullException("bucket", "The bucket specified as part of the Logging Config is null or the empty string");
            }
            this.logging = new Tuple<string, string>(bucket, prefix);
            return this;
        }

        public CloudFrontDistributionConfig WithOrigin(string origin)
        {
            this.Origin = origin;
            return this;
        }

        public CloudFrontDistributionConfig WithOriginAccessIdentity(CloudFrontOriginAccessIdentity identity)
        {
            base.OriginAccessIdentity = identity;
            return this;
        }

        public CloudFrontDistributionConfig WithTrustedSigners(UrlTrustedSigners signers)
        {
            base.TrustedSigners = signers;
            return this;
        }

        [XmlIgnore]
        public Tuple<string, string> Logging
        {
            get
            {
                if (this.logging == null)
                {
                    this.logging = new Tuple<string, string>();
                }
                return this.logging;
            }
            set
            {
                this.logging = value;
            }
        }
    }
}

