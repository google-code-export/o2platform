namespace Amazon.CloudFront.Model
{
    using System;
    using System.Text;
    using System.Xml.Serialization;

    [Serializable]
    public class CloudFrontOriginAccessIdentityConfig
    {
        private string callerReference = DateTime.UtcNow.ToString();
        private string comment;
        private string eTag;

        internal bool IsSetCallerReference()
        {
            return !string.IsNullOrEmpty(this.callerReference);
        }

        internal bool IsSetComment()
        {
            return !string.IsNullOrEmpty(this.comment);
        }

        internal bool IsSetETag()
        {
            return !string.IsNullOrEmpty(this.eTag);
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder(0x400);
            builder.Append("<?xml version=\"1.0\" encoding=\"UTF-8\"?><CloudFrontOriginAccessIdentityConfig ");
            builder.Append("xmlns=\"http://cloudfront.amazonaws.com/doc/2010-03-01/\">");
            if (this.IsSetCallerReference())
            {
                builder.Append("<CallerReference>" + this.CallerReference + "</CallerReference>");
            }
            if (this.IsSetComment())
            {
                builder.Append("<Comment>" + this.Comment + "</Comment>");
            }
            builder.Append("</CloudFrontOriginAccessIdentityConfig>");
            return builder.ToString();
        }

        public CloudFrontOriginAccessIdentityConfig WithCallerReference(string callerReference)
        {
            this.callerReference = callerReference;
            return this;
        }

        public CloudFrontOriginAccessIdentityConfig WithComment(string comment)
        {
            this.comment = comment;
            return this;
        }

        [XmlElement(ElementName="CallerReference")]
        public string CallerReference
        {
            get
            {
                return this.callerReference;
            }
            set
            {
                this.callerReference = value;
            }
        }

        [XmlElement(ElementName="Comment")]
        public string Comment
        {
            get
            {
                return this.comment;
            }
            set
            {
                this.comment = value;
            }
        }

        [XmlElement(ElementName="ETag")]
        public string ETag
        {
            get
            {
                return this.eTag;
            }
            set
            {
                this.eTag = value;
            }
        }
    }
}

