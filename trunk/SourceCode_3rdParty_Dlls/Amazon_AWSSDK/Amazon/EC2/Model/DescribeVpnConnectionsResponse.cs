namespace Amazon.EC2.Model
{
    using System;
    using System.IO;
    using System.Text;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://ec2.amazonaws.com/doc/2009-11-30/", IsNullable=false)]
    public class DescribeVpnConnectionsResponse
    {
        private Amazon.EC2.Model.DescribeVpnConnectionsResult describeVpnConnectionsResultField;
        private Amazon.EC2.Model.ResponseMetadata responseMetadataField;

        public bool IsSetDescribeVpnConnectionsResult()
        {
            return (this.describeVpnConnectionsResultField != null);
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

        public DescribeVpnConnectionsResponse WithDescribeVpnConnectionsResult(Amazon.EC2.Model.DescribeVpnConnectionsResult describeVpnConnectionsResult)
        {
            this.describeVpnConnectionsResultField = describeVpnConnectionsResult;
            return this;
        }

        public DescribeVpnConnectionsResponse WithResponseMetadata(Amazon.EC2.Model.ResponseMetadata responseMetadata)
        {
            this.responseMetadataField = responseMetadata;
            return this;
        }

        [XmlElement(ElementName="DescribeVpnConnectionsResult")]
        public Amazon.EC2.Model.DescribeVpnConnectionsResult DescribeVpnConnectionsResult
        {
            get
            {
                return this.describeVpnConnectionsResultField;
            }
            set
            {
                this.describeVpnConnectionsResultField = value;
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

