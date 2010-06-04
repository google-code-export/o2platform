namespace Amazon.RDS.Model
{
    using System;
    using System.IO;
    using System.Text;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://rds.amazonaws.com/admin/2009-10-16/", IsNullable=false)]
    public class DescribeDBParametersResponse
    {
        private Amazon.RDS.Model.DescribeDBParametersResult describeDBParametersResultField;
        private Amazon.RDS.Model.ResponseMetadata responseMetadataField;

        public bool IsSetDescribeDBParametersResult()
        {
            return (this.describeDBParametersResultField != null);
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

        public DescribeDBParametersResponse WithDescribeDBParametersResult(Amazon.RDS.Model.DescribeDBParametersResult describeDBParametersResult)
        {
            this.describeDBParametersResultField = describeDBParametersResult;
            return this;
        }

        public DescribeDBParametersResponse WithResponseMetadata(Amazon.RDS.Model.ResponseMetadata responseMetadata)
        {
            this.responseMetadataField = responseMetadata;
            return this;
        }

        [XmlElement(ElementName="DescribeDBParametersResult")]
        public Amazon.RDS.Model.DescribeDBParametersResult DescribeDBParametersResult
        {
            get
            {
                return this.describeDBParametersResultField;
            }
            set
            {
                this.describeDBParametersResultField = value;
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

