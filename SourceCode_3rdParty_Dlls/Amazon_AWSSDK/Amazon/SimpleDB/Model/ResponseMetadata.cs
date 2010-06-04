namespace Amazon.SimpleDB.Model
{
    using System;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://sdb.amazonaws.com/doc/2009-04-15/", IsNullable=false)]
    public class ResponseMetadata
    {
        private string boxUsageField;
        private string requestIdField;

        public bool IsSetBoxUsage()
        {
            return (this.boxUsageField != null);
        }

        public bool IsSetRequestId()
        {
            return (this.requestIdField != null);
        }

        public ResponseMetadata WithBoxUsage(string boxUsage)
        {
            this.boxUsageField = boxUsage;
            return this;
        }

        public ResponseMetadata WithRequestId(string requestId)
        {
            this.requestIdField = requestId;
            return this;
        }

        [XmlElement(ElementName="BoxUsage")]
        public string BoxUsage
        {
            get
            {
                return this.boxUsageField;
            }
            set
            {
                this.boxUsageField = value;
            }
        }

        [XmlElement(ElementName="RequestId")]
        public string RequestId
        {
            get
            {
                return this.requestIdField;
            }
            set
            {
                this.requestIdField = value;
            }
        }
    }
}

