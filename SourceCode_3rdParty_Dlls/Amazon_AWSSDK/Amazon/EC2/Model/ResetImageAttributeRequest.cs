namespace Amazon.EC2.Model
{
    using System;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://ec2.amazonaws.com/doc/2009-11-30/", IsNullable=false)]
    public class ResetImageAttributeRequest
    {
        private string attributeField;
        private string imageIdField;

        public bool IsSetAttribute()
        {
            return (this.attributeField != null);
        }

        public bool IsSetImageId()
        {
            return (this.imageIdField != null);
        }

        public ResetImageAttributeRequest WithAttribute(string attribute)
        {
            this.attributeField = attribute;
            return this;
        }

        public ResetImageAttributeRequest WithImageId(string imageId)
        {
            this.imageIdField = imageId;
            return this;
        }

        [XmlElement(ElementName="Attribute")]
        public string Attribute
        {
            get
            {
                return this.attributeField;
            }
            set
            {
                this.attributeField = value;
            }
        }

        [XmlElement(ElementName="ImageId")]
        public string ImageId
        {
            get
            {
                return this.imageIdField;
            }
            set
            {
                this.imageIdField = value;
            }
        }
    }
}

