namespace Amazon.EC2.Model
{
    using System;
    using System.IO;
    using System.Text;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://ec2.amazonaws.com/doc/2009-11-30/", IsNullable=false)]
    public class DescribeSecurityGroupsResponse
    {
        private Amazon.EC2.Model.DescribeSecurityGroupsResult describeSecurityGroupsResultField;
        private Amazon.EC2.Model.ResponseMetadata responseMetadataField;

        public bool IsSetDescribeSecurityGroupsResult()
        {
            return (this.describeSecurityGroupsResultField != null);
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

        public DescribeSecurityGroupsResponse WithDescribeSecurityGroupsResult(Amazon.EC2.Model.DescribeSecurityGroupsResult describeSecurityGroupsResult)
        {
            this.describeSecurityGroupsResultField = describeSecurityGroupsResult;
            return this;
        }

        public DescribeSecurityGroupsResponse WithResponseMetadata(Amazon.EC2.Model.ResponseMetadata responseMetadata)
        {
            this.responseMetadataField = responseMetadata;
            return this;
        }

        [XmlElement(ElementName="DescribeSecurityGroupsResult")]
        public Amazon.EC2.Model.DescribeSecurityGroupsResult DescribeSecurityGroupsResult
        {
            get
            {
                return this.describeSecurityGroupsResultField;
            }
            set
            {
                this.describeSecurityGroupsResultField = value;
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

