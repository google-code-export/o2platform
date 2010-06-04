namespace Amazon.SimpleDB.Model
{
    using System;
    using System.Collections.Generic;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://sdb.amazonaws.com/doc/2009-04-15/", IsNullable=false)]
    public class Item
    {
        private List<Amazon.SimpleDB.Model.Attribute> attributeField;
        private string nameEncodingField;
        private string nameField;

        public bool IsSetAttribute()
        {
            return (this.Attribute.Count > 0);
        }

        public bool IsSetName()
        {
            return (this.nameField != null);
        }

        public bool IsSetNameEncoding()
        {
            return (this.nameEncodingField != null);
        }

        public Item WithAttribute(params Amazon.SimpleDB.Model.Attribute[] list)
        {
            foreach (Amazon.SimpleDB.Model.Attribute attribute in list)
            {
                this.Attribute.Add(attribute);
            }
            return this;
        }

        public Item WithName(string name)
        {
            this.nameField = name;
            return this;
        }

        public Item WithNameEncoding(string nameEncoding)
        {
            this.nameEncodingField = nameEncoding;
            return this;
        }

        [XmlElement(ElementName="Attribute")]
        public List<Amazon.SimpleDB.Model.Attribute> Attribute
        {
            get
            {
                if (this.attributeField == null)
                {
                    this.attributeField = new List<Amazon.SimpleDB.Model.Attribute>();
                }
                return this.attributeField;
            }
            set
            {
                this.attributeField = value;
            }
        }

        [XmlElement(ElementName="Name")]
        public string Name
        {
            get
            {
                return this.nameField;
            }
            set
            {
                this.nameField = value;
            }
        }

        [XmlElement(ElementName="NameEncoding")]
        public string NameEncoding
        {
            get
            {
                return this.nameEncodingField;
            }
            set
            {
                this.nameEncodingField = value;
            }
        }
    }
}

