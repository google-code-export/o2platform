namespace Amazon.CloudWatch.Model
{
    using System;
    using System.IO;
    using System.Text;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://monitoring.amazonaws.com/doc/2009-05-15/", IsNullable=false)]
    public class GetMetricStatisticsResponse
    {
        private Amazon.CloudWatch.Model.GetMetricStatisticsResult getMetricStatisticsResultField;
        private Amazon.CloudWatch.Model.ResponseMetadata responseMetadataField;

        public bool IsSetGetMetricStatisticsResult()
        {
            return (this.getMetricStatisticsResultField != null);
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

        public GetMetricStatisticsResponse WithGetMetricStatisticsResult(Amazon.CloudWatch.Model.GetMetricStatisticsResult getMetricStatisticsResult)
        {
            this.getMetricStatisticsResultField = getMetricStatisticsResult;
            return this;
        }

        public GetMetricStatisticsResponse WithResponseMetadata(Amazon.CloudWatch.Model.ResponseMetadata responseMetadata)
        {
            this.responseMetadataField = responseMetadata;
            return this;
        }

        [XmlElement(ElementName="GetMetricStatisticsResult")]
        public Amazon.CloudWatch.Model.GetMetricStatisticsResult GetMetricStatisticsResult
        {
            get
            {
                return this.getMetricStatisticsResultField;
            }
            set
            {
                this.getMetricStatisticsResultField = value;
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

