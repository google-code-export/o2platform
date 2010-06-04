namespace Amazon.SimpleDB.Model
{
    using System;
    using System.Collections.Generic;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://sdb.amazonaws.com/doc/2009-04-15/", IsNullable=false)]
    public class PutAttributesRequest
    {
        private List<ReplaceableAttribute> attributeField;
        private string domainNameField;
        private UpdateCondition expectedField;
        private string itemNameField;

        public bool IsSetAttribute()
        {
            return (this.Attribute.Count > 0);
        }

        public bool IsSetDomainName()
        {
            return (this.domainNameField != null);
        }

        public bool IsSetExpected()
        {
            return (this.expectedField != null);
        }

        public bool IsSetItemName()
        {
            return (this.itemNameField != null);
        }

        public PutAttributesRequest WithAttribute(params ReplaceableAttribute[] list)
        {
            foreach (ReplaceableAttribute attribute in list)
            {
                this.Attribute.Add(attribute);
            }
            return this;
        }

        public PutAttributesRequest WithDomainName(string domainName)
        {
            this.domainNameField = domainName;
            return this;
        }

        public PutAttributesRequest WithExpected(UpdateCondition expected)
        {
            this.expectedField = expected;
            return this;
        }

        public PutAttributesRequest WithItemName(string itemName)
        {
            this.itemNameField = itemName;
            return this;
        }

        [XmlElement(ElementName="Attribute")]
        public List<ReplaceableAttribute> Attribute
        {
            get
            {
                if (this.attributeField == null)
                {
                    this.attributeField = new List<ReplaceableAttribute>();
                }
                return this.attributeField;
            }
            set
            {
                this.attributeField = value;
            }
        }

        [XmlElement(ElementName="DomainName")]
        public string DomainName
        {
            get
            {
                return this.domainNameField;
            }
            set
            {
                this.domainNameField = value;
            }
        }

        [XmlElement(ElementName="Expected")]
        public UpdateCondition Expected
        {
            get
            {
                return this.expectedField;
            }
            set
            {
                this.expectedField = value;
            }
        }

        [XmlElement(ElementName="ItemName")]
        public string ItemName
        {
            get
            {
                return this.itemNameField;
            }
            set
            {
                this.itemNameField = value;
            }
        }
    }
}

