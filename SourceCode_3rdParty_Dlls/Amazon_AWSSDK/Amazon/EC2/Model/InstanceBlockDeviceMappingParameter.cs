namespace Amazon.EC2.Model
{
    using System;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://ec2.amazonaws.com/doc/2009-11-30/", IsNullable=false)]
    public class InstanceBlockDeviceMappingParameter
    {
        private string deviceNameField;
        private InstanceEbsBlockDeviceParameter ebsField;
        private string noDeviceField;
        private string virtualNameField;

        public bool IsSetDeviceName()
        {
            return (this.deviceNameField != null);
        }

        public bool IsSetEbs()
        {
            return (this.ebsField != null);
        }

        public bool IsSetNoDevice()
        {
            return (this.noDeviceField != null);
        }

        public bool IsSetVirtualName()
        {
            return (this.virtualNameField != null);
        }

        public InstanceBlockDeviceMappingParameter WithDeviceName(string deviceName)
        {
            this.deviceNameField = deviceName;
            return this;
        }

        public InstanceBlockDeviceMappingParameter WithEbs(InstanceEbsBlockDeviceParameter ebs)
        {
            this.ebsField = ebs;
            return this;
        }

        public InstanceBlockDeviceMappingParameter WithNoDevice(string noDevice)
        {
            this.noDeviceField = noDevice;
            return this;
        }

        public InstanceBlockDeviceMappingParameter WithVirtualName(string virtualName)
        {
            this.virtualNameField = virtualName;
            return this;
        }

        [XmlElement(ElementName="DeviceName")]
        public string DeviceName
        {
            get
            {
                return this.deviceNameField;
            }
            set
            {
                this.deviceNameField = value;
            }
        }

        [XmlElement(ElementName="Ebs")]
        public InstanceEbsBlockDeviceParameter Ebs
        {
            get
            {
                return this.ebsField;
            }
            set
            {
                this.ebsField = value;
            }
        }

        [XmlElement(ElementName="NoDevice")]
        public string NoDevice
        {
            get
            {
                return this.noDeviceField;
            }
            set
            {
                this.noDeviceField = value;
            }
        }

        [XmlElement(ElementName="VirtualName")]
        public string VirtualName
        {
            get
            {
                return this.virtualNameField;
            }
            set
            {
                this.virtualNameField = value;
            }
        }
    }
}

