namespace Amazon.S3.Model
{
    using System;
    using System.Net;
    using System.Xml.Serialization;

    public class SetACLResponse : S3Response
    {
        private string versionId;

        public override WebHeaderCollection Headers
        {
            set
            {
                base.Headers = value;
                string str = null;
                if (!string.IsNullOrEmpty(str = value.Get("x-amz-version-id")))
                {
                    this.VersionId = str;
                }
            }
        }

        [XmlIgnore]
        public string VersionId
        {
            get
            {
                return this.versionId;
            }
            set
            {
                this.versionId = value;
            }
        }
    }
}

