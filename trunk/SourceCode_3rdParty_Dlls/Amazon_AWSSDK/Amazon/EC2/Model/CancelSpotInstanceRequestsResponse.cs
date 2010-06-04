namespace Amazon.EC2.Model
{
    using System;
    using System.IO;
    using System.Text;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://ec2.amazonaws.com/doc/2009-11-30/", IsNullable=false)]
    public class CancelSpotInstanceRequestsResponse
    {
        private Amazon.EC2.Model.CancelSpotInstanceRequestsResult cancelSpotInstanceRequestsResultField;
        private Amazon.EC2.Model.ResponseMetadata responseMetadataField;

        public bool IsSetCancelSpotInstanceRequestsResult()
        {
            return (this.cancelSpotInstanceRequestsResultField != null);
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

        public CancelSpotInstanceRequestsResponse WithCancelSpotInstanceRequestsResult(Amazon.EC2.Model.CancelSpotInstanceRequestsResult cancelSpotInstanceRequestsResult)
        {
            this.cancelSpotInstanceRequestsResultField = cancelSpotInstanceRequestsResult;
            return this;
        }

        public CancelSpotInstanceRequestsResponse WithResponseMetadata(Amazon.EC2.Model.ResponseMetadata responseMetadata)
        {
            this.responseMetadataField = responseMetadata;
            return this;
        }

        [XmlElement(ElementName="CancelSpotInstanceRequestsResult")]
        public Amazon.EC2.Model.CancelSpotInstanceRequestsResult CancelSpotInstanceRequestsResult
        {
            get
            {
                return this.cancelSpotInstanceRequestsResultField;
            }
            set
            {
                this.cancelSpotInstanceRequestsResultField = value;
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

