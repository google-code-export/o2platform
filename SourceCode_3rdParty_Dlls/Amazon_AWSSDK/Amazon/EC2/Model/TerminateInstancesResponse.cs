namespace Amazon.EC2.Model
{
    using System;
    using System.IO;
    using System.Text;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://ec2.amazonaws.com/doc/2009-11-30/", IsNullable=false)]
    public class TerminateInstancesResponse
    {
        private Amazon.EC2.Model.ResponseMetadata responseMetadataField;
        private Amazon.EC2.Model.TerminateInstancesResult terminateInstancesResultField;

        public bool IsSetResponseMetadata()
        {
            return (this.responseMetadataField != null);
        }

        public bool IsSetTerminateInstancesResult()
        {
            return (this.terminateInstancesResultField != null);
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

        public TerminateInstancesResponse WithResponseMetadata(Amazon.EC2.Model.ResponseMetadata responseMetadata)
        {
            this.responseMetadataField = responseMetadata;
            return this;
        }

        public TerminateInstancesResponse WithTerminateInstancesResult(Amazon.EC2.Model.TerminateInstancesResult terminateInstancesResult)
        {
            this.terminateInstancesResultField = terminateInstancesResult;
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

        [XmlElement(ElementName="TerminateInstancesResult")]
        public Amazon.EC2.Model.TerminateInstancesResult TerminateInstancesResult
        {
            get
            {
                return this.terminateInstancesResultField;
            }
            set
            {
                this.terminateInstancesResultField = value;
            }
        }
    }
}

