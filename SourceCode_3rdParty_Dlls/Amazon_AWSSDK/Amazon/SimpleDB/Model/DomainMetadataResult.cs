namespace Amazon.SimpleDB.Model
{
    using System;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://sdb.amazonaws.com/doc/2009-04-15/", IsNullable=false)]
    public class DomainMetadataResult
    {
        private string attributeNameCountField;
        private string attributeNamesSizeBytesField;
        private string attributeValueCountField;
        private string attributeValuesSizeBytesField;
        private string itemCountField;
        private string itemNamesSizeBytesField;
        private string timestampField;

        public bool IsSetAttributeNameCount()
        {
            return (this.attributeNameCountField != null);
        }

        public bool IsSetAttributeNamesSizeBytes()
        {
            return (this.attributeNamesSizeBytesField != null);
        }

        public bool IsSetAttributeValueCount()
        {
            return (this.attributeValueCountField != null);
        }

        public bool IsSetAttributeValuesSizeBytes()
        {
            return (this.attributeValuesSizeBytesField != null);
        }

        public bool IsSetItemCount()
        {
            return (this.itemCountField != null);
        }

        public bool IsSetItemNamesSizeBytes()
        {
            return (this.itemNamesSizeBytesField != null);
        }

        public bool IsSetTimestamp()
        {
            return (this.timestampField != null);
        }

        public DomainMetadataResult WithAttributeNameCount(string attributeNameCount)
        {
            this.attributeNameCountField = attributeNameCount;
            return this;
        }

        public DomainMetadataResult WithAttributeNamesSizeBytes(string attributeNamesSizeBytes)
        {
            this.attributeNamesSizeBytesField = attributeNamesSizeBytes;
            return this;
        }

        public DomainMetadataResult WithAttributeValueCount(string attributeValueCount)
        {
            this.attributeValueCountField = attributeValueCount;
            return this;
        }

        public DomainMetadataResult WithAttributeValuesSizeBytes(string attributeValuesSizeBytes)
        {
            this.attributeValuesSizeBytesField = attributeValuesSizeBytes;
            return this;
        }

        public DomainMetadataResult WithItemCount(string itemCount)
        {
            this.itemCountField = itemCount;
            return this;
        }

        public DomainMetadataResult WithItemNamesSizeBytes(string itemNamesSizeBytes)
        {
            this.itemNamesSizeBytesField = itemNamesSizeBytes;
            return this;
        }

        public DomainMetadataResult WithTimestamp(string timestamp)
        {
            this.timestampField = timestamp;
            return this;
        }

        [XmlElement(ElementName="AttributeNameCount")]
        public string AttributeNameCount
        {
            get
            {
                return this.attributeNameCountField;
            }
            set
            {
                this.attributeNameCountField = value;
            }
        }

        [XmlElement(ElementName="AttributeNamesSizeBytes")]
        public string AttributeNamesSizeBytes
        {
            get
            {
                return this.attributeNamesSizeBytesField;
            }
            set
            {
                this.attributeNamesSizeBytesField = value;
            }
        }

        [XmlElement(ElementName="AttributeValueCount")]
        public string AttributeValueCount
        {
            get
            {
                return this.attributeValueCountField;
            }
            set
            {
                this.attributeValueCountField = value;
            }
        }

        [XmlElement(ElementName="AttributeValuesSizeBytes")]
        public string AttributeValuesSizeBytes
        {
            get
            {
                return this.attributeValuesSizeBytesField;
            }
            set
            {
                this.attributeValuesSizeBytesField = value;
            }
        }

        [XmlElement(ElementName="ItemCount")]
        public string ItemCount
        {
            get
            {
                return this.itemCountField;
            }
            set
            {
                this.itemCountField = value;
            }
        }

        [XmlElement(ElementName="ItemNamesSizeBytes")]
        public string ItemNamesSizeBytes
        {
            get
            {
                return this.itemNamesSizeBytesField;
            }
            set
            {
                this.itemNamesSizeBytesField = value;
            }
        }

        [XmlElement(ElementName="Timestamp")]
        public string Timestamp
        {
            get
            {
                return this.timestampField;
            }
            set
            {
                this.timestampField = value;
            }
        }
    }
}

