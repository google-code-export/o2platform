namespace Amazon.EC2.Model
{
    using System;
    using System.IO;
    using System.Text;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://ec2.amazonaws.com/doc/2009-11-30/", IsNullable=false)]
    public class GetConsoleOutputResponse
    {
        private Amazon.EC2.Model.GetConsoleOutputResult getConsoleOutputResultField;
        private Amazon.EC2.Model.ResponseMetadata responseMetadataField;

        public bool IsSetGetConsoleOutputResult()
        {
            return (this.getConsoleOutputResultField != null);
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

        public GetConsoleOutputResponse WithGetConsoleOutputResult(Amazon.EC2.Model.GetConsoleOutputResult getConsoleOutputResult)
        {
            this.getConsoleOutputResultField = getConsoleOutputResult;
            return this;
        }

        public GetConsoleOutputResponse WithResponseMetadata(Amazon.EC2.Model.ResponseMetadata responseMetadata)
        {
            this.responseMetadataField = responseMetadata;
            return this;
        }

        [XmlElement(ElementName="GetConsoleOutputResult")]
        public Amazon.EC2.Model.GetConsoleOutputResult GetConsoleOutputResult
        {
            get
            {
                return this.getConsoleOutputResultField;
            }
            set
            {
                this.getConsoleOutputResultField = value;
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

