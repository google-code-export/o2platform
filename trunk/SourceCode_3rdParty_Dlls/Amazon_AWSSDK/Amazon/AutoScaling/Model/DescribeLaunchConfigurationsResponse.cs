namespace Amazon.AutoScaling.Model
{
    using System;
    using System.IO;
    using System.Text;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://autoscaling.amazonaws.com/doc/2009-05-15/", IsNullable=false)]
    public class DescribeLaunchConfigurationsResponse
    {
        private Amazon.AutoScaling.Model.DescribeLaunchConfigurationsResult describeLaunchConfigurationsResultField;
        private Amazon.AutoScaling.Model.ResponseMetadata responseMetadataField;

        public bool IsSetDescribeLaunchConfigurationsResult()
        {
            return (this.describeLaunchConfigurationsResultField != null);
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

        public DescribeLaunchConfigurationsResponse WithDescribeLaunchConfigurationsResult(Amazon.AutoScaling.Model.DescribeLaunchConfigurationsResult describeLaunchConfigurationsResult)
        {
            this.describeLaunchConfigurationsResultField = describeLaunchConfigurationsResult;
            return this;
        }

        public DescribeLaunchConfigurationsResponse WithResponseMetadata(Amazon.AutoScaling.Model.ResponseMetadata responseMetadata)
        {
            this.responseMetadataField = responseMetadata;
            return this;
        }

        [XmlElement(ElementName="DescribeLaunchConfigurationsResult")]
        public Amazon.AutoScaling.Model.DescribeLaunchConfigurationsResult DescribeLaunchConfigurationsResult
        {
            get
            {
                return this.describeLaunchConfigurationsResultField;
            }
            set
            {
                this.describeLaunchConfigurationsResultField = value;
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

