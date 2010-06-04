namespace Amazon.AutoScaling.Model
{
    using System;
    using System.IO;
    using System.Text;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://autoscaling.amazonaws.com/doc/2009-05-15/", IsNullable=false)]
    public class TerminateInstanceInAutoScalingGroupResponse
    {
        private Amazon.AutoScaling.Model.ResponseMetadata responseMetadataField;
        private Amazon.AutoScaling.Model.TerminateInstanceInAutoScalingGroupResult terminateInstanceInAutoScalingGroupResultField;

        public bool IsSetResponseMetadata()
        {
            return (this.responseMetadataField != null);
        }

        public bool IsSetTerminateInstanceInAutoScalingGroupResult()
        {
            return (this.terminateInstanceInAutoScalingGroupResultField != null);
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

        public TerminateInstanceInAutoScalingGroupResponse WithResponseMetadata(Amazon.AutoScaling.Model.ResponseMetadata responseMetadata)
        {
            this.responseMetadataField = responseMetadata;
            return this;
        }

        public TerminateInstanceInAutoScalingGroupResponse WithTerminateInstanceInAutoScalingGroupResult(Amazon.AutoScaling.Model.TerminateInstanceInAutoScalingGroupResult terminateInstanceInAutoScalingGroupResult)
        {
            this.terminateInstanceInAutoScalingGroupResultField = terminateInstanceInAutoScalingGroupResult;
            return this;
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

        [XmlElement(ElementName="TerminateInstanceInAutoScalingGroupResult")]
        public Amazon.AutoScaling.Model.TerminateInstanceInAutoScalingGroupResult TerminateInstanceInAutoScalingGroupResult
        {
            get
            {
                return this.terminateInstanceInAutoScalingGroupResultField;
            }
            set
            {
                this.terminateInstanceInAutoScalingGroupResultField = value;
            }
        }
    }
}

