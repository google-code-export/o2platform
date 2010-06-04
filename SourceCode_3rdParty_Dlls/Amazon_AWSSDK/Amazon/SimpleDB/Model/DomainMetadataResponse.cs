namespace Amazon.SimpleDB.Model
{
    using System;
    using System.IO;
    using System.Text;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://sdb.amazonaws.com/doc/2009-04-15/", IsNullable=false)]
    public class DomainMetadataResponse
    {
        private Amazon.SimpleDB.Model.DomainMetadataResult domainMetadataResultField;
        private Amazon.SimpleDB.Model.ResponseMetadata responseMetadataField;

        public bool IsSetDomainMetadataResult()
        {
            return (this.domainMetadataResultField != null);
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

        public DomainMetadataResponse WithDomainMetadataResult(Amazon.SimpleDB.Model.DomainMetadataResult domainMetadataResult)
        {
            this.domainMetadataResultField = domainMetadataResult;
            return this;
        }

        public DomainMetadataResponse WithResponseMetadata(Amazon.SimpleDB.Model.ResponseMetadata responseMetadata)
        {
            this.responseMetadataField = responseMetadata;
            return this;
        }

        [XmlElement(ElementName="DomainMetadataResult")]
        public Amazon.SimpleDB.Model.DomainMetadataResult DomainMetadataResult
        {
            get
            {
                return this.domainMetadataResultField;
            }
            set
            {
                this.domainMetadataResultField = value;
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

