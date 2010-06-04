namespace Amazon.RDS.Model
{
    using System;
    using System.IO;
    using System.Text;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://rds.amazonaws.com/admin/2009-10-16/", IsNullable=false)]
    public class DescribeEngineDefaultParametersResponse
    {
        private Amazon.RDS.Model.DescribeEngineDefaultParametersResult describeEngineDefaultParametersResultField;
        private Amazon.RDS.Model.ResponseMetadata responseMetadataField;

        public bool IsSetDescribeEngineDefaultParametersResult()
        {
            return (this.describeEngineDefaultParametersResultField != null);
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

        public DescribeEngineDefaultParametersResponse WithDescribeEngineDefaultParametersResult(Amazon.RDS.Model.DescribeEngineDefaultParametersResult describeEngineDefaultParametersResult)
        {
            this.describeEngineDefaultParametersResultField = describeEngineDefaultParametersResult;
            return this;
        }

        public DescribeEngineDefaultParametersResponse WithResponseMetadata(Amazon.RDS.Model.ResponseMetadata responseMetadata)
        {
            this.responseMetadataField = responseMetadata;
            return this;
        }

        [XmlElement(ElementName="DescribeEngineDefaultParametersResult")]
        public Amazon.RDS.Model.DescribeEngineDefaultParametersResult DescribeEngineDefaultParametersResult
        {
            get
            {
                return this.describeEngineDefaultParametersResultField;
            }
            set
            {
                this.describeEngineDefaultParametersResultField = value;
            }
        }

        [XmlElement(ElementName="ResponseMetadata")]
        public Amazon.RDS.Model.ResponseMetadata ResponseMetadata
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

