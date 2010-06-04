namespace Amazon.CloudFront
{
    using System;
    using System.Net;
    using System.Runtime.Serialization;
    using System.Security.Permissions;

    [Serializable]
    public class AmazonCloudFrontException : Exception, ISerializable
    {
        private string errorCode;
        private string message;
        private string requestAddr;
        private string requestId;
        private WebHeaderCollection responseHeaders;
        private HttpStatusCode statusCode;
        private string type;
        private string xml;

        public AmazonCloudFrontException()
        {
        }

        public AmazonCloudFrontException(Exception innerException) : this(innerException.Message, innerException)
        {
        }

        public AmazonCloudFrontException(string message)
        {
            this.message = message;
        }

        protected AmazonCloudFrontException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            this.statusCode = (HttpStatusCode) info.GetValue("statusCode", typeof(HttpStatusCode));
            this.errorCode = info.GetString("errorCode");
            this.message = info.GetString("message");
            this.type = info.GetString("type");
            this.requestId = info.GetString("requestId");
            this.xml = info.GetString("xml");
            this.requestAddr = info.GetString("requestAddr");
            this.responseHeaders = (WebHeaderCollection) info.GetValue("responseHeaders", typeof(WebHeaderCollection));
        }

        public AmazonCloudFrontException(string message, Exception innerException) : base(message, innerException)
        {
            this.message = message;
            AmazonCloudFrontException exception = innerException as AmazonCloudFrontException;
            if (exception != null)
            {
                this.statusCode = exception.StatusCode;
                this.errorCode = exception.ErrorCode;
                this.requestId = exception.RequestId;
                this.message = exception.Message;
                this.type = exception.type;
                this.xml = exception.XML;
            }
        }

        public AmazonCloudFrontException(string message, HttpStatusCode statusCode) : this(message)
        {
            this.statusCode = statusCode;
        }

        public AmazonCloudFrontException(string message, HttpStatusCode statusCode, string errorCode, string requestId, string type, string xml, string requestAddr, WebHeaderCollection responseHeaders) : this(message, statusCode)
        {
            this.errorCode = errorCode;
            this.type = type;
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
            info.AddValue("type", this.type, typeof(string));
            info.AddValue("requestId", this.requestId, typeof(string));
            info.AddValue("xml", this.xml, typeof(string));
            info.AddValue("requestAddr", this.requestAddr, typeof(string));
            info.AddValue("responseHeaders", this.responseHeaders, typeof(WebHeaderCollection));
            base.GetObjectData(info, context);
        }

        public string CloudFrontRequestAddress
        {
            get
            {
                return this.requestAddr;
            }
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
                return this.type;
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

