namespace Amazon.AutoScaling.Model
{
    using System;
    using System.IO;
    using System.Text;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://autoscaling.amazonaws.com/doc/2009-05-15/", IsNullable=false)]
    public class DescribeAutoScalingGroupsResponse
    {
        private Amazon.AutoScaling.Model.DescribeAutoScalingGroupsResult describeAutoScalingGroupsResultField;
        private Amazon.AutoScaling.Model.ResponseMetadata responseMetadataField;

        public bool IsSetDescribeAutoScalingGroupsResult()
        {
            return (this.describeAutoScalingGroupsResultField != null);
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

        public DescribeAutoScalingGroupsResponse WithDescribeAutoScalingGroupsResult(Amazon.AutoScaling.Model.DescribeAutoScalingGroupsResult describeAutoScalingGroupsResult)
        {
            this.describeAutoScalingGroupsResultField = describeAutoScalingGroupsResult;
            return this;
        }

        public DescribeAutoScalingGroupsResponse WithResponseMetadata(Amazon.AutoScaling.Model.ResponseMetadata responseMetadata)
        {
            this.responseMetadataField = responseMetadata;
            return this;
        }

        [XmlElement(ElementName="DescribeAutoScalingGroupsResult")]
        public Amazon.AutoScaling.Model.DescribeAutoScalingGroupsResult DescribeAutoScalingGroupsResult
        {
            get
            {
                return this.describeAutoScalingGroupsResultField;
            }
            set
            {
                this.describeAutoScalingGroupsResultField = value;
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

