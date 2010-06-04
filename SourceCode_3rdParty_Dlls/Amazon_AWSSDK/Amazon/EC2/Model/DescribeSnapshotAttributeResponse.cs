namespace Amazon.EC2.Model
{
    using System;
    using System.IO;
    using System.Text;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://ec2.amazonaws.com/doc/2009-11-30/", IsNullable=false)]
    public class DescribeSnapshotAttributeResponse
    {
        private Amazon.EC2.Model.DescribeSnapshotAttributeResult describeSnapshotAttributeResultField;
        private Amazon.EC2.Model.ResponseMetadata responseMetadataField;

        public bool IsSetDescribeSnapshotAttributeResult()
        {
            return (this.describeSnapshotAttributeResultField != null);
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

        public DescribeSnapshotAttributeResponse WithDescribeSnapshotAttributeResult(Amazon.EC2.Model.DescribeSnapshotAttributeResult describeSnapshotAttributeResult)
        {
            this.describeSnapshotAttributeResultField = describeSnapshotAttributeResult;
            return this;
        }

        public DescribeSnapshotAttributeResponse WithResponseMetadata(Amazon.EC2.Model.ResponseMetadata responseMetadata)
        {
            this.responseMetadataField = responseMetadata;
            return this;
        }

        [XmlElement(ElementName="DescribeSnapshotAttributeResult")]
        public Amazon.EC2.Model.DescribeSnapshotAttributeResult DescribeSnapshotAttributeResult
        {
            get
            {
                return this.describeSnapshotAttributeResultField;
            }
            set
            {
                this.describeSnapshotAttributeResultField = value;
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

