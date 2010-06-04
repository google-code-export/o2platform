namespace Amazon.SimpleDB.Model
{
    using System;
    using System.IO;
    using System.Text;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://sdb.amazonaws.com/doc/2009-04-15/", IsNullable=false)]
    public class SelectResponse
    {
        private Amazon.SimpleDB.Model.ResponseMetadata responseMetadataField;
        private Amazon.SimpleDB.Model.SelectResult selectResultField;

        public bool IsSetResponseMetadata()
        {
            return (this.responseMetadataField != null);
        }

        public bool IsSetSelectResult()
        {
            return (this.selectResultField != null);
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

        public SelectResponse WithResponseMetadata(Amazon.SimpleDB.Model.ResponseMetadata responseMetadata)
        {
            this.responseMetadataField = responseMetadata;
            return this;
        }

        public SelectResponse WithSelectResult(Amazon.SimpleDB.Model.SelectResult selectResult)
        {
            this.selectResultField = selectResult;
            return this;
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

        [XmlElement(ElementName="SelectResult")]
        public Amazon.SimpleDB.Model.SelectResult SelectResult
        {
            get
            {
                return this.selectResultField;
            }
            set
            {
                this.selectResultField = value;
            }
        }
    }
}

