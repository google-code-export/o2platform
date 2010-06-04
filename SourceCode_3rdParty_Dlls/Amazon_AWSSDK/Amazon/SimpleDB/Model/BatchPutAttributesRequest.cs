namespace Amazon.SimpleDB.Model
{
    using System;
    using System.Collections.Generic;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://sdb.amazonaws.com/doc/2009-04-15/", IsNullable=false)]
    public class BatchPutAttributesRequest
    {
        private string domainNameField;
        private List<ReplaceableItem> itemField;

        public bool IsSetDomainName()
        {
            return (this.domainNameField != null);
        }

        public bool IsSetItem()
        {
            return (this.Item.Count > 0);
        }

        public BatchPutAttributesRequest WithDomainName(string domainName)
        {
            this.domainNameField = domainName;
            return this;
        }

        public BatchPutAttributesRequest WithItem(params ReplaceableItem[] list)
        {
            foreach (ReplaceableItem item in list)
            {
                this.Item.Add(item);
            }
            return this;
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

        [XmlElement(ElementName="Item")]
        public List<ReplaceableItem> Item
        {
            get
            {
                if (this.itemField == null)
                {
                    this.itemField = new List<ReplaceableItem>();
                }
                return this.itemField;
            }
            set
            {
                this.itemField = value;
            }
        }
    }
}

