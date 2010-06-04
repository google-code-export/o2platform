namespace Amazon.EC2.Model
{
    using System;
    using System.IO;
    using System.Text;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://ec2.amazonaws.com/doc/2009-11-30/", IsNullable=false)]
    public class DescribeVolumesResponse
    {
        private Amazon.EC2.Model.DescribeVolumesResult describeVolumesResultField;
        private Amazon.EC2.Model.ResponseMetadata responseMetadataField;

        public bool IsSetDescribeVolumesResult()
        {
            return (this.describeVolumesResultField != null);
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

        public DescribeVolumesResponse WithDescribeVolumesResult(Amazon.EC2.Model.DescribeVolumesResult describeVolumesResult)
        {
            this.describeVolumesResultField = describeVolumesResult;
            return this;
        }

        public DescribeVolumesResponse WithResponseMetadata(Amazon.EC2.Model.ResponseMetadata responseMetadata)
        {
            this.responseMetadataField = responseMetadata;
            return this;
        }

        [XmlElement(ElementName="DescribeVolumesResult")]
        public Amazon.EC2.Model.DescribeVolumesResult DescribeVolumesResult
        {
            get
            {
                return this.describeVolumesResultField;
            }
            set
            {
                this.describeVolumesResultField = value;
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

