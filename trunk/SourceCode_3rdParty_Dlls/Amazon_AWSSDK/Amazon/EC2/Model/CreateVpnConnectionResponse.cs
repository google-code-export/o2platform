namespace Amazon.EC2.Model
{
    using System;
    using System.IO;
    using System.Text;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://ec2.amazonaws.com/doc/2009-11-30/", IsNullable=false)]
    public class CreateVpnConnectionResponse
    {
        private Amazon.EC2.Model.CreateVpnConnectionResult createVpnConnectionResultField;
        private Amazon.EC2.Model.ResponseMetadata responseMetadataField;

        public bool IsSetCreateVpnConnectionResult()
        {
            return (this.createVpnConnectionResultField != null);
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

        public CreateVpnConnectionResponse WithCreateVpnConnectionResult(Amazon.EC2.Model.CreateVpnConnectionResult createVpnConnectionResult)
        {
            this.createVpnConnectionResultField = createVpnConnectionResult;
            return this;
        }

        public CreateVpnConnectionResponse WithResponseMetadata(Amazon.EC2.Model.ResponseMetadata responseMetadata)
        {
            this.responseMetadataField = responseMetadata;
            return this;
        }

        [XmlElement(ElementName="CreateVpnConnectionResult")]
        public Amazon.EC2.Model.CreateVpnConnectionResult CreateVpnConnectionResult
        {
            get
            {
                return this.createVpnConnectionResultField;
            }
            set
            {
                this.createVpnConnectionResultField = value;
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

