namespace Amazon.EC2.Model
{
    using System;
    using System.Collections.Generic;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://ec2.amazonaws.com/doc/2009-11-30/", IsNullable=false)]
    public class ModifyInstanceAttributeRequest
    {
        private string attributeField;
        private List<InstanceBlockDeviceMappingParameter> blockDeviceMappingField;
        private string instanceIdField;
        private string valueField;

        public bool IsSetAttribute()
        {
            return (this.attributeField != null);
        }

        public bool IsSetBlockDeviceMapping()
        {
            return (this.BlockDeviceMapping.Count > 0);
        }

        public bool IsSetInstanceId()
        {
            return (this.instanceIdField != null);
        }

        public bool IsSetValue()
        {
            return (this.valueField != null);
        }

        public ModifyInstanceAttributeRequest WithAttribute(string attribute)
        {
            this.attributeField = attribute;
            return this;
        }

        public ModifyInstanceAttributeRequest WithBlockDeviceMapping(params InstanceBlockDeviceMappingParameter[] list)
        {
            foreach (InstanceBlockDeviceMappingParameter parameter in list)
            {
                this.BlockDeviceMapping.Add(parameter);
            }
            return this;
        }

        public ModifyInstanceAttributeRequest WithInstanceId(string instanceId)
        {
            this.instanceIdField = instanceId;
            return this;
        }

        public ModifyInstanceAttributeRequest WithValue(string value)
        {
            this.valueField = value;
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

        [XmlElement(ElementName="BlockDeviceMapping")]
        public List<InstanceBlockDeviceMappingParameter> BlockDeviceMapping
        {
            get
            {
                if (this.blockDeviceMappingField == null)
                {
                    this.blockDeviceMappingField = new List<InstanceBlockDeviceMappingParameter>();
                }
                return this.blockDeviceMappingField;
            }
            set
            {
                this.blockDeviceMappingField = value;
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

        [XmlElement(ElementName="Value")]
        public string Value
        {
            get
            {
                return this.valueField;
            }
            set
            {
                this.valueField = value;
            }
        }
    }
}

