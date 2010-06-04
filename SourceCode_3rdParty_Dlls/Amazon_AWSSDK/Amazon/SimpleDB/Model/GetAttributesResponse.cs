namespace Amazon.SimpleDB.Model
{
    using System;
    using System.IO;
    using System.Text;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://sdb.amazonaws.com/doc/2009-04-15/", IsNullable=false)]
    public class GetAttributesResponse
    {
        private Amazon.SimpleDB.Model.GetAttributesResult getAttributesResultField;
        private Amazon.SimpleDB.Model.ResponseMetadata responseMetadataField;

        public bool IsSetGetAttributesResult()
        {
            return (this.getAttributesResultField != null);
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

        public GetAttributesResponse WithGetAttributesResult(Amazon.SimpleDB.Model.GetAttributesResult getAttributesResult)
        {
            this.getAttributesResultField = getAttributesResult;
            return this;
        }

        public GetAttributesResponse WithResponseMetadata(Amazon.SimpleDB.Model.ResponseMetadata responseMetadata)
        {
            this.responseMetadataField = responseMetadata;
            return this;
        }

        [XmlElement(ElementName="GetAttributesResult")]
        public Amazon.SimpleDB.Model.GetAttributesResult GetAttributesResult
        {
            get
            {
                return this.getAttributesResultField;
            }
            set
            {
                this.getAttributesResultField = value;
            }
        }

        [XmlElement(ElementName="ResponseMetadata")]
        public Amazon.SimpleDB.Model.ResponseMetadata ResponseMetadata
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

