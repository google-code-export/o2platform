namespace Amazon.SQS.Model
{
    using System;
    using System.IO;
    using System.Text;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://queue.amazonaws.com/doc/2009-02-01/", IsNullable=false)]
    public class ListQueuesResponse
    {
        private Amazon.SQS.Model.ListQueuesResult listQueuesResultField;
        private Amazon.SQS.Model.ResponseMetadata responseMetadataField;

        public bool IsSetListQueuesResult()
        {
            return (this.listQueuesResultField != null);
        }

        public bool IsSetResponseMetadata()
        {
            return (this.responseMetadataField != null);
        }

        public string ToXML()
        {
            StringBuilder sb = new StringBuilder(0x400);
            XmlSerializer serializer = new XmlSerializer(base.GetType());
            using (StringWriter writer = new StringWriter(sb))
            {
                serializer.Serialize((TextWriter) writer, this);
            }
            return sb.ToString();
        }

        public ListQueuesResponse WithListQueuesResult(Amazon.SQS.Model.ListQueuesResult listQueuesResult)
        {
            this.listQueuesResultField = listQueuesResult;
            return this;
        }

        public ListQueuesResponse WithResponseMetadata(Amazon.SQS.Model.ResponseMetadata responseMetadata)
        {
            this.responseMetadataField = responseMetadata;
            return this;
        }

        [XmlElement(ElementName="ListQueuesResult")]
        public Amazon.SQS.Model.ListQueuesResult ListQueuesResult
        {
            get
            {
                return this.listQueuesResultField;
            }
            set
            {
                this.listQueuesResultField = value;
            }
        }

        [XmlElement(ElementName="ResponseMetadata")]
        public Amazon.SQS.Model.ResponseMetadata ResponseMetadata
        {
            get
            {
                return this.responseMetadataField;
            }
            set
            {
                this.responseMetadataField = value;
            }
        }
    }
}

