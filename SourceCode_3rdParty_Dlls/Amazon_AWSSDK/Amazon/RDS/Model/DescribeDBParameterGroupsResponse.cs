namespace Amazon.RDS.Model
{
    using System;
    using System.IO;
    using System.Text;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://rds.amazonaws.com/admin/2009-10-16/", IsNullable=false)]
    public class DescribeDBParameterGroupsResponse
    {
        private Amazon.RDS.Model.DescribeDBParameterGroupsResult describeDBParameterGroupsResultField;
        private Amazon.RDS.Model.ResponseMetadata responseMetadataField;

        public bool IsSetDescribeDBParameterGroupsResult()
        {
            return (this.describeDBParameterGroupsResultField != null);
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

        public DescribeDBParameterGroupsResponse WithDescribeDBParameterGroupsResult(Amazon.RDS.Model.DescribeDBParameterGroupsResult describeDBParameterGroupsResult)
        {
            this.describeDBParameterGroupsResultField = describeDBParameterGroupsResult;
            return this;
        }

        public DescribeDBParameterGroupsResponse WithResponseMetadata(Amazon.RDS.Model.ResponseMetadata responseMetadata)
        {
            this.responseMetadataField = responseMetadata;
            return this;
        }

        [XmlElement(ElementName="DescribeDBParameterGroupsResult")]
        public Amazon.RDS.Model.DescribeDBParameterGroupsResult DescribeDBParameterGroupsResult
        {
            get
            {
                return this.describeDBParameterGroupsResultField;
            }
            set
            {
                this.describeDBParameterGroupsResultField = value;
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

