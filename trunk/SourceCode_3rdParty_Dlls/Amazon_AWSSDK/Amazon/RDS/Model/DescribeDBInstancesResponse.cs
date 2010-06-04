namespace Amazon.RDS.Model
{
    using System;
    using System.IO;
    using System.Text;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://rds.amazonaws.com/admin/2009-10-16/", IsNullable=false)]
    public class DescribeDBInstancesResponse
    {
        private Amazon.RDS.Model.DescribeDBInstancesResult describeDBInstancesResultField;
        private Amazon.RDS.Model.ResponseMetadata responseMetadataField;

        public bool IsSetDescribeDBInstancesResult()
        {
            return (this.describeDBInstancesResultField != null);
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

        public DescribeDBInstancesResponse WithDescribeDBInstancesResult(Amazon.RDS.Model.DescribeDBInstancesResult describeDBInstancesResult)
        {
            this.describeDBInstancesResultField = describeDBInstancesResult;
            return this;
        }

        public DescribeDBInstancesResponse WithResponseMetadata(Amazon.RDS.Model.ResponseMetadata responseMetadata)
        {
            this.responseMetadataField = responseMetadata;
            return this;
        }

        [XmlElement(ElementName="DescribeDBInstancesResult")]
        public Amazon.RDS.Model.DescribeDBInstancesResult DescribeDBInstancesResult
        {
            get
            {
                return this.describeDBInstancesResultField;
            }
            set
            {
                this.describeDBInstancesResultField = value;
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

