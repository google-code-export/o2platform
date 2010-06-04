namespace Amazon.S3.Model
{
    using System;
    using System.Net;

    public class GetObjectMetadataResponse : S3Response
    {
        private long contentLength;
        private string contentType;
        private string etag;
        private string versionId;

        public long ContentLength
        {
            get
            {
                return this.contentLength;
            }
            set
            {
                this.contentLength = value;
            }
        }

        public string ContentType
        {
            get
            {
                return this.contentType;
            }
            set
            {
                this.contentType = value;
            }
        }

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
                if (!string.IsNullOrEmpty(str = value.Get("Content-Type")))
                {
                    this.ContentType = str;
                }
                if (!string.IsNullOrEmpty(str = value.Get("Content-Length")))
                {
                    this.ContentLength = Convert.ToInt64(str);
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

