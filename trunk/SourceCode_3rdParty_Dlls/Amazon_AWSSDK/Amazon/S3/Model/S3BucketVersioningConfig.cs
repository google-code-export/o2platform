namespace Amazon.S3.Model
{
    using System;
    using System.Text;
    using System.Xml.Serialization;

    [Serializable]
    public class S3BucketVersioningConfig
    {
        private bool? enableMfaDelete;
        private string status = "Off";

        public bool IsSetEnableMfaDelete()
        {
            return this.enableMfaDelete.HasValue;
        }

        internal bool IsSetStatus()
        {
            return !string.IsNullOrEmpty(this.status);
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder(0x400);
            builder.Append("<VersioningConfiguration xmlns=\"http://s3.amazonaws.com/doc/2006-03-01/\">");
            builder.Append("<Status>" + this.Status + "</Status>");
            if (this.IsSetEnableMfaDelete())
            {
                builder.Append("<MfaDelete>" + (this.EnableMfaDelete.Value ? "Enabled" : "Disabled") + "</MfaDelete>");
            }
            builder.Append("</VersioningConfiguration>");
            return builder.ToString();
        }

        public S3BucketVersioningConfig WithEnableMfaDelete(bool fEnabled)
        {
            this.enableMfaDelete = new bool?(fEnabled);
            return this;
        }

        public S3BucketVersioningConfig WithStatus(string status)
        {
            this.Status = status;
            return this;
        }

        [XmlElement(ElementName="EnableMfaDelete")]
        public bool? EnableMfaDelete
        {
            get
            {
                return new bool?(this.enableMfaDelete.GetValueOrDefault());
            }
            set
            {
                this.enableMfaDelete = value;
            }
        }

        [XmlElement(ElementName="Status")]
        public string Status
        {
            get
            {
                return this.status;
            }
            set
            {
                if ((!value.Equals("Enabled") && !value.Equals("Off")) && !value.Equals("Suspended"))
                {
                    throw new ArgumentException("Invalid Versioning Status!", "value");
                }
                this.status = value;
            }
        }
    }
}

