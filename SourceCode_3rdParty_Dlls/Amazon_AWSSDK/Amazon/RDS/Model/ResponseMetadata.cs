namespace Amazon.RDS.Model
{
    using System;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://rds.amazonaws.com/admin/2009-10-16/", IsNullable=false)]
    public class ResponseMetadata
    {
        private string requestIdField;

        public bool IsSetRequestId()
        {
            return (this.requestIdField != null);
        }

        public ResponseMetadata WithRequestId(string requestId)
        {
            this.requestIdField = requestId;
            return this;
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

