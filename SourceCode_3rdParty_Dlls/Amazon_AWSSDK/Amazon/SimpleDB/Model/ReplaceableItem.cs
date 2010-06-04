namespace Amazon.SimpleDB.Model
{
    using System;
    using System.Collections.Generic;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://sdb.amazonaws.com/doc/2009-04-15/", IsNullable=false)]
    public class ReplaceableItem
    {
        private List<ReplaceableAttribute> attributeField;
        private string itemNameField;

        public bool IsSetAttribute()
        {
            return (this.Attribute.Count > 0);
        }

        public bool IsSetItemName()
        {
            return (this.itemNameField != null);
        }

        public ReplaceableItem WithAttribute(params ReplaceableAttribute[] list)
        {
            foreach (ReplaceableAttribute attribute in list)
            {
                this.Attribute.Add(attribute);
            }
            return this;
        }

        public ReplaceableItem WithItemName(string itemName)
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

