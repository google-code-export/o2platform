namespace Amazon.EC2.Model
{
    using System;
    using System.IO;
    using System.Text;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://ec2.amazonaws.com/doc/2009-11-30/", IsNullable=false)]
    public class StopInstancesResponse
    {
        private Amazon.EC2.Model.ResponseMetadata responseMetadataField;
        private Amazon.EC2.Model.StopInstancesResult stopInstancesResultField;

        public bool IsSetResponseMetadata()
        {
            return (this.responseMetadataField != null);
        }

        public bool IsSetStopInstancesResult()
        {
            return (this.stopInstancesResultField != null);
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

        public StopInstancesResponse WithResponseMetadata(Amazon.EC2.Model.ResponseMetadata responseMetadata)
        {
            this.responseMetadataField = responseMetadata;
            return this;
        }

        public StopInstancesResponse WithStopInstancesResult(Amazon.EC2.Model.StopInstancesResult stopInstancesResult)
        {
            this.stopInstancesResultField = stopInstancesResult;
            return this;
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

        [XmlElement(ElementName="StopInstancesResult")]
        public Amazon.EC2.Model.StopInstancesResult StopInstancesResult
        {
            get
            {
                return this.stopInstancesResultField;
            }
            set
            {
                this.stopInstancesResultField = value;
            }
        }
    }
}

