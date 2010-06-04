namespace Amazon.SimpleDB.Model
{
    using System;
    using System.Collections.Generic;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://sdb.amazonaws.com/doc/2009-04-15/", IsNullable=false)]
    public class GetAttributesResult
    {
        private List<Amazon.SimpleDB.Model.Attribute> attributeField;

        public bool IsSetAttribute()
        {
            return (this.Attribute.Count > 0);
        }

        public GetAttributesResult WithAttribute(params Amazon.SimpleDB.Model.Attribute[] list)
        {
            foreach (Amazon.SimpleDB.Model.Attribute attribute in list)
            {
                this.Attribute.Add(attribute);
            }
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
    }
}

