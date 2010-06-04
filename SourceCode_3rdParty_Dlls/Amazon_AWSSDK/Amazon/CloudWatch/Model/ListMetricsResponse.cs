namespace Amazon.CloudWatch.Model
{
    using System;
    using System.IO;
    using System.Text;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://monitoring.amazonaws.com/doc/2009-05-15/", IsNullable=false)]
    public class ListMetricsResponse
    {
        private Amazon.CloudWatch.Model.ListMetricsResult listMetricsResultField;
        private Amazon.CloudWatch.Model.ResponseMetadata responseMetadataField;

        public bool IsSetListMetricsResult()
        {
            return (this.listMetricsResultField != null);
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

        public ListMetricsResponse WithListMetricsResult(Amazon.CloudWatch.Model.ListMetricsResult listMetricsResult)
        {
            this.listMetricsResultField = listMetricsResult;
            return this;
        }

        public ListMetricsResponse WithResponseMetadata(Amazon.CloudWatch.Model.ResponseMetadata responseMetadata)
        {
            this.responseMetadataField = responseMetadata;
            return this;
        }

        [XmlElement(ElementName="ListMetricsResult")]
        public Amazon.CloudWatch.Model.ListMetricsResult ListMetricsResult
        {
            get
            {
                return this.listMetricsResultField;
            }
            set
            {
                this.listMetricsResultField = value;
            }
        }

        [XmlElement(ElementName="ResponseMetadata")]
        public Amazon.CloudWatch.Model.ResponseMetadata ResponseMetadata
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

