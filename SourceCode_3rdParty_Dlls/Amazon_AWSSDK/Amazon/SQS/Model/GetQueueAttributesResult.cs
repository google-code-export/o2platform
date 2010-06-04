namespace Amazon.SQS.Model
{
    using System;
    using System.Collections.Generic;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://queue.amazonaws.com/doc/2009-02-01/", IsNullable=false)]
    public class GetQueueAttributesResult
    {
        private List<Amazon.SQS.Model.Attribute> attributeField;

        public bool IsSetAttribute()
        {
            return (this.Attribute.Count > 0);
        }

        public GetQueueAttributesResult WithAttribute(params Amazon.SQS.Model.Attribute[] list)
        {
            foreach (Amazon.SQS.Model.Attribute attribute in list)
            {
                this.Attribute.Add(attribute);
            }
            return this;
        }

        [XmlElement(ElementName="Attribute")]
        public List<Amazon.SQS.Model.Attribute> Attribute
        {
            get
            {
                if (this.attributeField == null)
                {
                    this.attributeField = new List<Amazon.SQS.Model.Attribute>();
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

