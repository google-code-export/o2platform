namespace Amazon.EC2.Model
{
    using System;
    using System.IO;
    using System.Text;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://ec2.amazonaws.com/doc/2009-11-30/", IsNullable=false)]
    public class DescribeSnapshotsResponse
    {
        private Amazon.EC2.Model.DescribeSnapshotsResult describeSnapshotsResultField;
        private Amazon.EC2.Model.ResponseMetadata responseMetadataField;

        public bool IsSetDescribeSnapshotsResult()
        {
            return (this.describeSnapshotsResultField != null);
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

        public DescribeSnapshotsResponse WithDescribeSnapshotsResult(Amazon.EC2.Model.DescribeSnapshotsResult describeSnapshotsResult)
        {
            this.describeSnapshotsResultField = describeSnapshotsResult;
            return this;
        }

        public DescribeSnapshotsResponse WithResponseMetadata(Amazon.EC2.Model.ResponseMetadata responseMetadata)
        {
            this.responseMetadataField = responseMetadata;
            return this;
        }

        [XmlElement(ElementName="DescribeSnapshotsResult")]
        public Amazon.EC2.Model.DescribeSnapshotsResult DescribeSnapshotsResult
        {
            get
            {
                return this.describeSnapshotsResultField;
            }
            set
            {
                this.describeSnapshotsResultField = value;
            }
        }

        [XmlElement(ElementName="ResponseMetadata")]
        public Amazon.EC2.Model.ResponseMetadata ResponseMetadata
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

