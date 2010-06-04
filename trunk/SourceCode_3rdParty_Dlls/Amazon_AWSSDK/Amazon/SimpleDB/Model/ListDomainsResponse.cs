namespace Amazon.SimpleDB.Model
{
    using System;
    using System.IO;
    using System.Text;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://sdb.amazonaws.com/doc/2009-04-15/", IsNullable=false)]
    public class ListDomainsResponse
    {
        private Amazon.SimpleDB.Model.ListDomainsResult listDomainsResultField;
        private Amazon.SimpleDB.Model.ResponseMetadata responseMetadataField;

        public bool IsSetListDomainsResult()
        {
            return (this.listDomainsResultField != null);
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

        public ListDomainsResponse WithListDomainsResult(Amazon.SimpleDB.Model.ListDomainsResult listDomainsResult)
        {
            this.listDomainsResultField = listDomainsResult;
            return this;
        }

        public ListDomainsResponse WithResponseMetadata(Amazon.SimpleDB.Model.ResponseMetadata responseMetadata)
        {
            this.responseMetadataField = responseMetadata;
            return this;
        }

        [XmlElement(ElementName="ListDomainsResult")]
        public Amazon.SimpleDB.Model.ListDomainsResult ListDomainsResult
        {
            get
            {
                return this.listDomainsResultField;
            }
            set
            {
                this.listDomainsResultField = value;
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

