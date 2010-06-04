namespace Amazon.EC2.Model
{
    using System;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://ec2.amazonaws.com/doc/2009-11-30/", IsNullable=false)]
    public class DescribeInstanceAttributeResult
    {
        private Amazon.EC2.Model.InstanceAttribute instanceAttributeField;

        public bool IsSetInstanceAttribute()
        {
            return (this.instanceAttributeField != null);
        }

        public DescribeInstanceAttributeResult WithInstanceAttribute(Amazon.EC2.Model.InstanceAttribute instanceAttribute)
        {
            this.instanceAttributeField = instanceAttribute;
            return this;
        }

        [XmlElement(ElementName="InstanceAttribute")]
        public Amazon.EC2.Model.InstanceAttribute InstanceAttribute
        {
            get
            {
                return this.instanceAttributeField;
            }
            set
            {
                this.instanceAttributeField = value;
            }
        }
    }
}

