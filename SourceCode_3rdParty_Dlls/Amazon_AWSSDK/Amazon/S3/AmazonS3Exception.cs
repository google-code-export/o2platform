namespace Amazon.S3
{
    using System;
    using System.Net;
    using System.Runtime.Serialization;
    using System.Security.Permissions;

    [Serializable]
    public class AmazonS3Exception : Exception, ISerializable
    {
        private string errorCode;
        private string hostId;
        private string message;
        private string requestAddr;
        private string requestId;
        private WebHeaderCollection responseHeaders;
        private HttpStatusCode statusCode;
        private string xml;

        public AmazonS3Exception()
        {
        }

        public AmazonS3Exception(Exception innerException) : this(innerException.Message, innerException)
        {
        }

        public AmazonS3Exception(string message)
        {
            this.message = message;
        }

        protected AmazonS3Exception(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            this.statusCode = (HttpStatusCode) info.GetValue("statusCode", typeof(HttpStatusCode));
            this.errorCode = info.GetString("errorCode");
            this.message = info.GetString("message");
            this.hostId = info.GetString("hostId");
            this.requestId = info.GetString("requestId");
            this.xml = info.GetString("xml");
            this.requestAddr = info.GetString("requestAddr");
            this.responseHeaders = (WebHeaderCollection) info.GetValue("responseHeaders", typeof(WebHeaderCollection));
        }

        public AmazonS3Exception(string message, Exception innerException) : base(message, innerException)
        {
            this.message = message;
            AmazonS3Exception exception = innerException as AmazonS3Exception;
            if (exception != null)
            {
                this.statusCode = exception.StatusCode;
                this.errorCode = exception.ErrorCode;
                this.requestId = exception.RequestId;
                this.message = exception.Message;
                this.hostId = exception.hostId;
                this.xml = exception.XML;
            }
        }

        public AmazonS3Exception(string message, HttpStatusCode statusCode) : this(message)
        {
            this.statusCode = statusCode;
        }

        public AmazonS3Exception(string message, HttpStatusCode statusCode, string errorCode, string requestId, string hostId, string xml, string requestAddr, WebHeaderCollection responseHeaders) : this(message, statusCode)
        {
            this.errorCode = errorCode;
            this.hostId = hostId;
            this.requestId = requestId;
            this.xml = xml;
            this.requestAddr = requestAddr;
            this.responseHeaders = responseHeaders;
        }

        [SecurityPermission(SecurityAction.LinkDemand, Flags=SecurityPermissionFlag.SerializationFormatter)]
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("statusCode", this.statusCode, typeof(HttpStatusCode));
            info.AddValue("errorCode", this.errorCode, typeof(string));
            info.AddValue("message", this.message, typeof(string));
            info.AddValue("hostId", this.hostId, typeof(string));
            info.AddValue("requestId", this.requestId, typeof(string));
            info.AddValue("xml", this.xml, typeof(string));
            info.AddValue("requestAddr", this.requestAddr, typeof(string));
            info.AddValue("responseHeaders", this.responseHeaders, typeof(WebHeaderCollection));
            base.GetObjectData(info, context);
        }

        public string ErrorCode
        {
            get
            {
                return this.errorCode;
            }
        }

        public string HostId
        {
            get
            {
                return this.hostId;
            }
        }

        public override string Message
        {
            get
            {
                return this.message;
            }
        }

        public string RequestId
        {
            get
            {
                return this.requestId;
            }
        }

        public WebHeaderCollection ResponseHeaders
        {
            get
            {
                return this.responseHeaders;
            }
        }

        public string S3RequestAddress
        {
            get
            {
                return this.requestAddr;
            }
        }

        public HttpStatusCode StatusCode
        {
            get
            {
                return this.statusCode;
            }
        }

        public string XML
        {
            get
            {
                return this.xml;
            }
        }
    }
}

