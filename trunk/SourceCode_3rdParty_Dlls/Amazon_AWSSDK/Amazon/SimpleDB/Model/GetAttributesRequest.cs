namespace Amazon.SimpleDB.Model
{
    using System;
    using System.Collections.Generic;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://sdb.amazonaws.com/doc/2009-04-15/", IsNullable=false)]
    public class GetAttributesRequest
    {
        private List<string> attributeNameField;
        private bool? consistentReadField;
        private string domainNameField;
        private string itemNameField;

        public bool IsSetAttributeName()
        {
            return (this.AttributeName.Count > 0);
        }

        public bool IsSetConsistentRead()
        {
            return this.consistentReadField.HasValue;
        }

        public bool IsSetDomainName()
        {
            return (this.domainNameField != null);
        }

        public bool IsSetItemName()
        {
            return (this.itemNameField != null);
        }

        public GetAttributesRequest WithAttributeName(params string[] list)
        {
            foreach (string str in list)
            {
                this.AttributeName.Add(str);
            }
            return this;
        }

        public GetAttributesRequest WithConsistentRead(bool consistentRead)
        {
            this.consistentReadField = new bool?(consistentRead);
            return this;
        }

        public GetAttributesRequest WithDomainName(string domainName)
        {
            this.domainNameField = domainName;
            return this;
        }

        public GetAttributesRequest WithItemName(string itemName)
        {
            this.itemNameField = itemName;
            return this;
        }

        [XmlElement(ElementName="AttributeName")]
        public List<string> AttributeName
        {
            get
            {
                if (this.attributeNameField == null)
                {
                    this.attributeNameField = new List<string>();
                }
                return this.attributeNameField;
            }
            set
            {
                this.attributeNameField = value;
            }
        }

        [XmlElement(ElementName="ConsistentRead")]
        public bool ConsistentRead
        {
            get
            {
                return this.consistentReadField.GetValueOrDefault();
            }
            set
            {
                this.consistentReadField = new bool?(value);
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

