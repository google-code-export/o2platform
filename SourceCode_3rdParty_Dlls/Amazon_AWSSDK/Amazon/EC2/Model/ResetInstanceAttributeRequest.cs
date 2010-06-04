namespace Amazon.EC2.Model
{
    using System;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://ec2.amazonaws.com/doc/2009-11-30/", IsNullable=false)]
    public class ResetInstanceAttributeRequest
    {
        private string attributeField;
        private string instanceIdField;

        public bool IsSetAttribute()
        {
            return (this.attributeField != null);
        }

        public bool IsSetInstanceId()
        {
            return (this.instanceIdField != null);
        }

        public ResetInstanceAttributeRequest WithAttribute(string attribute)
        {
            this.attributeField = attribute;
            return this;
        }

        public ResetInstanceAttributeRequest WithInstanceId(string instanceId)
        {
            this.instanceIdField = instanceId;
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

        [XmlElement(ElementName="InstanceId")]
        public string InstanceId
        {
            get
            {
                return this.instanceIdField;
            }
            set
            {
                this.instanceIdField = value;
            }
        }
    }
}

