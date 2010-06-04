namespace Amazon.EC2.Model
{
    using System;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://ec2.amazonaws.com/doc/2009-11-30/", IsNullable=false)]
    public class DescribeImageAttributeResult
    {
        private Amazon.EC2.Model.ImageAttribute imageAttributeField;

        public bool IsSetImageAttribute()
        {
            return (this.imageAttributeField != null);
        }

        public DescribeImageAttributeResult WithImageAttribute(Amazon.EC2.Model.ImageAttribute imageAttribute)
        {
            this.imageAttributeField = imageAttribute;
            return this;
        }

        [XmlElement(ElementName="ImageAttribute")]
        public Amazon.EC2.Model.ImageAttribute ImageAttribute
        {
            get
            {
                return this.imageAttributeField;
            }
            set
            {
                this.imageAttributeField = value;
            }
        }
    }
}

