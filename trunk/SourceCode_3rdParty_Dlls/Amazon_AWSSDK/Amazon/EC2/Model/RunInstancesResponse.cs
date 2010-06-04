namespace Amazon.EC2.Model
{
    using System;
    using System.IO;
    using System.Text;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://ec2.amazonaws.com/doc/2009-11-30/", IsNullable=false)]
    public class RunInstancesResponse
    {
        private Amazon.EC2.Model.ResponseMetadata responseMetadataField;
        private Amazon.EC2.Model.RunInstancesResult runInstancesResultField;

        public bool IsSetResponseMetadata()
        {
            return (this.responseMetadataField != null);
        }

        public bool IsSetRunInstancesResult()
        {
            return (this.runInstancesResultField != null);
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

        public RunInstancesResponse WithResponseMetadata(Amazon.EC2.Model.ResponseMetadata responseMetadata)
        {
            this.responseMetadataField = responseMetadata;
            return this;
        }

        public RunInstancesResponse WithRunInstancesResult(Amazon.EC2.Model.RunInstancesResult runInstancesResult)
        {
            this.runInstancesResultField = runInstancesResult;
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

        [XmlElement(ElementName="RunInstancesResult")]
        public Amazon.EC2.Model.RunInstancesResult RunInstancesResult
        {
            get
            {
                return this.runInstancesResultField;
            }
            set
            {
                this.runInstancesResultField = value;
            }
        }
    }
}

