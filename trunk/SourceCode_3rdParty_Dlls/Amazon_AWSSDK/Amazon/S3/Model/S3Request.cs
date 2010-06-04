namespace Amazon.S3.Model
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.IO;
    using System.Net;
    using System.Xml.Serialization;

    public class S3Request
    {
        private WebHeaderCollection headers;
        private Stream inputStream;
        internal IDictionary<S3QueryParameter, string> parameters = new Dictionary<S3QueryParameter, string>(10);
        internal NameValueCollection removedHeaders = new NameValueCollection();

        public void AddHeaders(NameValueCollection collection)
        {
            this.Headers.Add(collection);
        }

        internal bool IsSetHeaders()
        {
            return ((this.headers != null) && (this.headers.Count > 0));
        }

        internal bool IsSetInputStream()
        {
            return (this.inputStream != null);
        }

        public S3Request WithInputStream(Stream inputStream)
        {
            this.inputStream = inputStream;
            return this;
        }

        internal WebHeaderCollection Headers
        {
            get
            {
                if (this.headers == null)
                {
                    this.headers = new WebHeaderCollection();
                }
                return this.headers;
            }
        }

        [XmlElement(ElementName="InputStream")]
        public Stream InputStream
        {
            get
            {
                return this.inputStream;
            }
            set
            {
                this.inputStream = value;
            }
        }
    }
}

