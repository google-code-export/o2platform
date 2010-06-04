namespace Amazon.EC2.Model
{
    using System;
    using System.IO;
    using System.Text;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://ec2.amazonaws.com/doc/2009-11-30/", IsNullable=false)]
    public class AllocateAddressResponse
    {
        private Amazon.EC2.Model.AllocateAddressResult allocateAddressResultField;
        private Amazon.EC2.Model.ResponseMetadata responseMetadataField;

        public bool IsSetAllocateAddressResult()
        {
            return (this.allocateAddressResultField != null);
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

        public AllocateAddressResponse WithAllocateAddressResult(Amazon.EC2.Model.AllocateAddressResult allocateAddressResult)
        {
            this.allocateAddressResultField = allocateAddressResult;
            return this;
        }

        public AllocateAddressResponse WithResponseMetadata(Amazon.EC2.Model.ResponseMetadata responseMetadata)
        {
            this.responseMetadataField = responseMetadata;
            return this;
        }

        [XmlElement(ElementName="AllocateAddressResult")]
        public Amazon.EC2.Model.AllocateAddressResult AllocateAddressResult
        {
            get
            {
                return this.allocateAddressResultField;
            }
            set
            {
                this.allocateAddressResultField = value;
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

