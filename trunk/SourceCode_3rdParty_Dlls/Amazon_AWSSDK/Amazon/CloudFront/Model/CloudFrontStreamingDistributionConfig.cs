namespace Amazon.CloudFront.Model
{
    using System;
    using System.Text;

    [Serializable]
    public class CloudFrontStreamingDistributionConfig : CloudFrontDistributionConfigBase
    {
        public override string ToString()
        {
            StringBuilder builder = new StringBuilder(0x400);
            builder.Append("<?xml version=\"1.0\" encoding=\"UTF-8\"?><StreamingDistributionConfig ");
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
            builder.Append("<Enabled>" + (this.Enabled ? "true" : "false") + "</Enabled>");
            if (this.IsSetComment())
            {
                builder.Append("<Comment>" + this.Comment + "</Comment>");
            }
            if (base.IsSetOriginAccessIdentity())
            {
                builder.Append("<OriginAccessIdentity>" + base.OriginAccessIdentity + "</OriginAccessIdentity>");
            }
            if (base.IsSetTrustedSigners())
            {
                builder.Append("<TrustedSigners>" + base.TrustedSigners + "</TrustedSigners>");
            }
            builder.Append("</StreamingDistributionConfig>");
            return builder.ToString();
        }

        public CloudFrontStreamingDistributionConfig WithCallerReference(string callerReference)
        {
            this.CallerReference = callerReference;
            return this;
        }

        public CloudFrontStreamingDistributionConfig WithCNames(params string[] cnames)
        {
            foreach (string str in cnames)
            {
                this.CNAME.Add(str);
            }
            return this;
        }

        public CloudFrontStreamingDistributionConfig WithComment(string comment)
        {
            this.Comment = comment;
            return this;
        }

        public CloudFrontStreamingDistributionConfig WithEnabled(bool enabled)
        {
            this.Enabled = enabled;
            return this;
        }

        public CloudFrontStreamingDistributionConfig WithOrigin(string origin)
        {
            this.Origin = origin;
            return this;
        }

        public CloudFrontStreamingDistributionConfig WithOriginAccessIdentity(CloudFrontOriginAccessIdentity identity)
        {
            base.OriginAccessIdentity = identity;
            return this;
        }

        public CloudFrontStreamingDistributionConfig WithTrustedSigners(UrlTrustedSigners signers)
        {
            base.TrustedSigners = signers;
            return this;
        }
    }
}

