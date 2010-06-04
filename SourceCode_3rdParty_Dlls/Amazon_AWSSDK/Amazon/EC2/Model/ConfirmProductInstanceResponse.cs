namespace Amazon.EC2.Model
{
    using System;
    using System.IO;
    using System.Text;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://ec2.amazonaws.com/doc/2009-11-30/", IsNullable=false)]
    public class ConfirmProductInstanceResponse
    {
        private Amazon.EC2.Model.ConfirmProductInstanceResult confirmProductInstanceResultField;
        private Amazon.EC2.Model.ResponseMetadata responseMetadataField;

        public bool IsSetConfirmProductInstanceResult()
        {
            return (this.confirmProductInstanceResultField != null);
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

        public ConfirmProductInstanceResponse WithConfirmProductInstanceResult(Amazon.EC2.Model.ConfirmProductInstanceResult confirmProductInstanceResult)
        {
            this.confirmProductInstanceResultField = confirmProductInstanceResult;
            return this;
        }

        public ConfirmProductInstanceResponse WithResponseMetadata(Amazon.EC2.Model.ResponseMetadata responseMetadata)
        {
            this.responseMetadataField = responseMetadata;
            return this;
        }

        [XmlElement(ElementName="ConfirmProductInstanceResult")]
        public Amazon.EC2.Model.ConfirmProductInstanceResult ConfirmProductInstanceResult
        {
            get
            {
                return this.confirmProductInstanceResultField;
            }
            set
            {
                this.confirmProductInstanceResultField = value;
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

