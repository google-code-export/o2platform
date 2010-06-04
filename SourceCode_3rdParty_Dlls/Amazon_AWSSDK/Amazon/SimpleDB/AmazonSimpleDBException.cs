namespace Amazon.SimpleDB
{
    using System;
    using System.Net;
    using System.Runtime.Serialization;
    using System.Security.Permissions;

    [Serializable]
    public class AmazonSimpleDBException : Exception, ISerializable
    {
        private string boxUsage;
        private string errorCode;
        private string errorType;
        private string message;
        private string requestId;
        private HttpStatusCode statusCode;
        private string xml;

        public AmazonSimpleDBException()
        {
        }

        public AmazonSimpleDBException(Exception innerException) : this(innerException.Message, innerException)
        {
        }

        public AmazonSimpleDBException(string message)
        {
            this.message = message;
        }

        protected AmazonSimpleDBException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            this.message = info.GetString("message");
            this.statusCode = (HttpStatusCode) info.GetValue("statusCode", typeof(HttpStatusCode));
            this.errorCode = info.GetString("errorCode");
            this.errorType = info.GetString("errorType");
            this.boxUsage = info.GetString("boxUsage");
            this.requestId = info.GetString("requestId");
            this.xml = info.GetString("xml");
        }

        public AmazonSimpleDBException(string message, Exception innerException) : base(message, innerException)
        {
            this.message = message;
            AmazonSimpleDBException exception = innerException as AmazonSimpleDBException;
            if (exception != null)
            {
                this.statusCode = exception.StatusCode;
                this.errorCode = exception.ErrorCode;
                this.errorType = exception.ErrorType;
                this.boxUsage = exception.BoxUsage;
                this.requestId = exception.RequestId;
                this.xml = exception.XML;
            }
        }

        public AmazonSimpleDBException(string message, HttpStatusCode statusCode) : this(message)
        {
            this.statusCode = statusCode;
        }

        public AmazonSimpleDBException(string message, HttpStatusCode statusCode, string errorCode, string errorType, string boxUsage, string requestId, string xml) : this(message, statusCode)
        {
            this.errorCode = errorCode;
            this.errorType = errorType;
            this.boxUsage = boxUsage;
            this.requestId = requestId;
            this.xml = xml;
        }

        [SecurityPermission(SecurityAction.LinkDemand, Flags=SecurityPermissionFlag.SerializationFormatter)]
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("message", this.message, typeof(string));
            info.AddValue("statusCode", this.statusCode, typeof(HttpStatusCode));
            info.AddValue("errorCode", this.errorCode, typeof(string));
            info.AddValue("errorType", this.errorType, typeof(string));
            info.AddValue("boxUsage", this.boxUsage, typeof(string));
            info.AddValue("requestId", this.requestId, typeof(string));
            info.AddValue("xml", this.xml, typeof(string));
            base.GetObjectData(info, context);
        }

        public string BoxUsage
        {
            get
            {
                return this.boxUsage;
            }
        }

        public string ErrorCode
        {
            get
            {
                return this.errorCode;
            }
        }

        public string ErrorType
        {
            get
            {
                return this.errorType;
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

