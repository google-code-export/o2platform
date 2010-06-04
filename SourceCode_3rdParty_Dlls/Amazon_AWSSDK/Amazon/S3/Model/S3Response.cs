namespace Amazon.S3.Model
{
    using System;
    using System.Collections.Specialized;
    using System.IO;
    using System.Net;
    using System.Xml.Serialization;

    public class S3Response : IDisposable
    {
        private string amazonId2;
        private bool disposed;
        internal HttpWebResponse httpResponse;
        private NameValueCollection metadata;
        private string requestId;
        private Stream responseStream;
        private string responseXml;
        private WebHeaderCollection webHeaders;

        public void Dispose()
        {
            this.Dispose(true);
            if (!this.disposed)
            {
                GC.SuppressFinalize(this);
            }
        }

        private void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing && (this.webHeaders != null))
                {
                    this.webHeaders.Clear();
                }
                if (this.responseStream != null)
                {
                    this.responseStream.Close();
                    this.responseStream = null;
                }
                if (this.httpResponse != null)
                {
                    this.httpResponse.Close();
                    this.httpResponse = null;
                }
                this.disposed = true;
            }
        }

        ~S3Response()
        {
            this.Dispose(false);
        }

        [XmlElement(ElementName="AmazonId2")]
        public string AmazonId2
        {
            get
            {
                return this.amazonId2;
            }
            set
            {
                this.amazonId2 = value;
            }
        }

        [XmlIgnore]
        public virtual WebHeaderCollection Headers
        {
            get
            {
                if (this.webHeaders == null)
                {
                    this.webHeaders = new WebHeaderCollection();
                }
                return this.webHeaders;
            }
            set
            {
                string str;
                this.webHeaders = value;
                if (!string.IsNullOrEmpty(str = value.Get("x-amz-request-id")))
                {
                    this.RequestId = str;
                }
                if (!string.IsNullOrEmpty(str = value.Get("x-amz-id-2")))
                {
                    this.AmazonId2 = str;
                }
                foreach (string str2 in value.Keys)
                {
                    if (str2.StartsWith("x-amz-meta-"))
                    {
                        this.Metadata.Add(str2, value.Get(str2));
                    }
                }
            }
        }

        [XmlIgnore]
        public NameValueCollection Metadata
        {
            get
            {
                if (this.metadata == null)
                {
                    this.metadata = new NameValueCollection();
                }
                return this.metadata;
            }
        }

        [XmlElement(ElementName="RequestId")]
        public string RequestId
        {
            get
            {
                return this.requestId;
            }
            set
            {
                this.requestId = value;
            }
        }

        [XmlElement(ElementName="ResponseStream")]
        public Stream ResponseStream
        {
            get
            {
                return this.responseStream;
            }
            set
            {
                this.responseStream = value;
            }
        }

        [XmlIgnore]
        public string ResponseXml
        {
            get
            {
                return this.responseXml;
            }
            set
            {
                this.responseXml = value;
            }
        }
    }
}

