namespace Amazon.SimpleDB.Model
{
    using System;
    using System.Collections.Generic;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://sdb.amazonaws.com/doc/2009-04-15/", IsNullable=false)]
    public class SelectResult
    {
        private List<Amazon.SimpleDB.Model.Item> itemField;
        private string nextTokenField;

        public bool IsSetItem()
        {
            return (this.Item.Count > 0);
        }

        public bool IsSetNextToken()
        {
            return (this.nextTokenField != null);
        }

        public SelectResult WithItem(params Amazon.SimpleDB.Model.Item[] list)
        {
            foreach (Amazon.SimpleDB.Model.Item item in list)
            {
                this.Item.Add(item);
            }
            return this;
        }

        public SelectResult WithNextToken(string nextToken)
        {
            this.nextTokenField = nextToken;
            return this;
        }

        [XmlElement(ElementName="Item")]
        public List<Amazon.SimpleDB.Model.Item> Item
        {
            get
            {
                if (this.itemField == null)
                {
                    this.itemField = new List<Amazon.SimpleDB.Model.Item>();
                }
                return this.itemField;
            }
            set
            {
                this.itemField = value;
            }
        }

        [XmlElement(ElementName="NextToken")]
        public string NextToken
        {
            get
            {
                return this.nextTokenField;
            }
            set
            {
                this.nextTokenField = value;
            }
        }
    }
}

