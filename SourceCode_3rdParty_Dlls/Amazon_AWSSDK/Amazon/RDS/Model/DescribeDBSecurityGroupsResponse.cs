namespace Amazon.RDS.Model
{
    using System;
    using System.IO;
    using System.Text;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://rds.amazonaws.com/admin/2009-10-16/", IsNullable=false)]
    public class DescribeDBSecurityGroupsResponse
    {
        private Amazon.RDS.Model.DescribeDBSecurityGroupsResult describeDBSecurityGroupsResultField;
        private Amazon.RDS.Model.ResponseMetadata responseMetadataField;

        public bool IsSetDescribeDBSecurityGroupsResult()
        {
            return (this.describeDBSecurityGroupsResultField != null);
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

        public DescribeDBSecurityGroupsResponse WithDescribeDBSecurityGroupsResult(Amazon.RDS.Model.DescribeDBSecurityGroupsResult describeDBSecurityGroupsResult)
        {
            this.describeDBSecurityGroupsResultField = describeDBSecurityGroupsResult;
            return this;
        }

        public DescribeDBSecurityGroupsResponse WithResponseMetadata(Amazon.RDS.Model.ResponseMetadata responseMetadata)
        {
            this.responseMetadataField = responseMetadata;
            return this;
        }

        [XmlElement(ElementName="DescribeDBSecurityGroupsResult")]
        public Amazon.RDS.Model.DescribeDBSecurityGroupsResult DescribeDBSecurityGroupsResult
        {
            get
            {
                return this.describeDBSecurityGroupsResultField;
            }
            set
            {
                this.describeDBSecurityGroupsResultField = value;
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

