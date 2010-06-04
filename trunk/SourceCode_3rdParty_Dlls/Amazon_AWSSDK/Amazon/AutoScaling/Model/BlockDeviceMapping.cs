namespace Amazon.AutoScaling.Model
{
    using System;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://autoscaling.amazonaws.com/doc/2009-05-15/", IsNullable=false)]
    public class BlockDeviceMapping
    {
        private string deviceNameField;
        private string virtualNameField;

        public bool IsSetDeviceName()
        {
            return (this.deviceNameField != null);
        }

        public bool IsSetVirtualName()
        {
            return (this.virtualNameField != null);
        }

        public BlockDeviceMapping WithDeviceName(string deviceName)
        {
            this.deviceNameField = deviceName;
            return this;
        }

        public BlockDeviceMapping WithVirtualName(string virtualName)
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

