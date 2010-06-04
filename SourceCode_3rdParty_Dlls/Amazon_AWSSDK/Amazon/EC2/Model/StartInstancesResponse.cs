namespace Amazon.EC2.Model
{
    using System;
    using System.IO;
    using System.Text;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://ec2.amazonaws.com/doc/2009-11-30/", IsNullable=false)]
    public class StartInstancesResponse
    {
        private Amazon.EC2.Model.ResponseMetadata responseMetadataField;
        private Amazon.EC2.Model.StartInstancesResult startInstancesResultField;

        public bool IsSetResponseMetadata()
        {
            return (this.responseMetadataField != null);
        }

        public bool IsSetStartInstancesResult()
        {
            return (this.startInstancesResultField != null);
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

        public StartInstancesResponse WithResponseMetadata(Amazon.EC2.Model.ResponseMetadata responseMetadata)
        {
            this.responseMetadataField = responseMetadata;
            return this;
        }

        public StartInstancesResponse WithStartInstancesResult(Amazon.EC2.Model.StartInstancesResult startInstancesResult)
        {
            this.startInstancesResultField = startInstancesResult;
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

        [XmlElement(ElementName="StartInstancesResult")]
        public Amazon.EC2.Model.StartInstancesResult StartInstancesResult
        {
            get
            {
                return this.startInstancesResultField;
            }
            set
            {
                this.startInstancesResultField = value;
            }
        }
    }
}

