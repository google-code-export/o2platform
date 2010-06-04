namespace Amazon.RDS.Model
{
    using System;
    using System.IO;
    using System.Text;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://rds.amazonaws.com/admin/2009-10-16/", IsNullable=false)]
    public class DescribeDBSnapshotsResponse
    {
        private Amazon.RDS.Model.DescribeDBSnapshotsResult describeDBSnapshotsResultField;
        private Amazon.RDS.Model.ResponseMetadata responseMetadataField;

        public bool IsSetDescribeDBSnapshotsResult()
        {
            return (this.describeDBSnapshotsResultField != null);
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

        public DescribeDBSnapshotsResponse WithDescribeDBSnapshotsResult(Amazon.RDS.Model.DescribeDBSnapshotsResult describeDBSnapshotsResult)
        {
            this.describeDBSnapshotsResultField = describeDBSnapshotsResult;
            return this;
        }

        public DescribeDBSnapshotsResponse WithResponseMetadata(Amazon.RDS.Model.ResponseMetadata responseMetadata)
        {
            this.responseMetadataField = responseMetadata;
            return this;
        }

        [XmlElement(ElementName="DescribeDBSnapshotsResult")]
        public Amazon.RDS.Model.DescribeDBSnapshotsResult DescribeDBSnapshotsResult
        {
            get
            {
                return this.describeDBSnapshotsResultField;
            }
            set
            {
                this.describeDBSnapshotsResultField = value;
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

