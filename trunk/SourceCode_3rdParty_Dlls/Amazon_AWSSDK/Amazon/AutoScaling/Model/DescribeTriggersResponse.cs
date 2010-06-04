namespace Amazon.AutoScaling.Model
{
    using System;
    using System.IO;
    using System.Text;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://autoscaling.amazonaws.com/doc/2009-05-15/", IsNullable=false)]
    public class DescribeTriggersResponse
    {
        private Amazon.AutoScaling.Model.DescribeTriggersResult describeTriggersResultField;
        private Amazon.AutoScaling.Model.ResponseMetadata responseMetadataField;

        public bool IsSetDescribeTriggersResult()
        {
            return (this.describeTriggersResultField != null);
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

        public DescribeTriggersResponse WithDescribeTriggersResult(Amazon.AutoScaling.Model.DescribeTriggersResult describeTriggersResult)
        {
            this.describeTriggersResultField = describeTriggersResult;
            return this;
        }

        public DescribeTriggersResponse WithResponseMetadata(Amazon.AutoScaling.Model.ResponseMetadata responseMetadata)
        {
            this.responseMetadataField = responseMetadata;
            return this;
        }

        [XmlElement(ElementName="DescribeTriggersResult")]
        public Amazon.AutoScaling.Model.DescribeTriggersResult DescribeTriggersResult
        {
            get
            {
                return this.describeTriggersResultField;
            }
            set
            {
                this.describeTriggersResultField = value;
            }
        }

        [XmlElement(ElementName="ResponseMetadata")]
        public Amazon.AutoScaling.Model.ResponseMetadata ResponseMetadata
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

