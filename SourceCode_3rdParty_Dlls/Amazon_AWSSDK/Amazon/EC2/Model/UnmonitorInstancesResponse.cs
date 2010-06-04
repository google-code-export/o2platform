namespace Amazon.EC2.Model
{
    using System;
    using System.IO;
    using System.Text;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://ec2.amazonaws.com/doc/2009-11-30/", IsNullable=false)]
    public class UnmonitorInstancesResponse
    {
        private Amazon.EC2.Model.ResponseMetadata responseMetadataField;
        private Amazon.EC2.Model.UnmonitorInstancesResult unmonitorInstancesResultField;

        public bool IsSetResponseMetadata()
        {
            return (this.responseMetadataField != null);
        }

        public bool IsSetUnmonitorInstancesResult()
        {
            return (this.unmonitorInstancesResultField != null);
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

        public UnmonitorInstancesResponse WithResponseMetadata(Amazon.EC2.Model.ResponseMetadata responseMetadata)
        {
            this.responseMetadataField = responseMetadata;
            return this;
        }

        public UnmonitorInstancesResponse WithUnmonitorInstancesResult(Amazon.EC2.Model.UnmonitorInstancesResult unmonitorInstancesResult)
        {
            this.unmonitorInstancesResultField = unmonitorInstancesResult;
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

        [XmlElement(ElementName="UnmonitorInstancesResult")]
        public Amazon.EC2.Model.UnmonitorInstancesResult UnmonitorInstancesResult
        {
            get
            {
                return this.unmonitorInstancesResultField;
            }
            set
            {
                this.unmonitorInstancesResultField = value;
            }
        }
    }
}

