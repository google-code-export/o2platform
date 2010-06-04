namespace Amazon.S3.Model
{
    using System;
    using System.Net;

    public class PutObjectResponse : S3Response
    {
        private string etag;
        private string versionId;

        public string ETag
        {
            get
            {
                return this.etag;
            }
            set
            {
                this.etag = value;
            }
        }

        public override WebHeaderCollection Headers
        {
            set
            {
                base.Headers = value;
                string str = null;
                if (!string.IsNullOrEmpty(str = value.Get("ETag")))
                {
                    this.ETag = str;
                }
                if (!string.IsNullOrEmpty(str = value.Get("x-amz-version-id")))
                {
                    this.VersionId = str;
                }
            }
        }

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

