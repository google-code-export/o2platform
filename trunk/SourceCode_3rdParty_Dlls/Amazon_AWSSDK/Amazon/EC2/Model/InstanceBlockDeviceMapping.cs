namespace Amazon.EC2.Model
{
    using System;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://ec2.amazonaws.com/doc/2009-11-30/", IsNullable=false)]
    public class InstanceBlockDeviceMapping
    {
        private string deviceNameField;
        private InstanceEbsBlockDevice ebsField;

        public bool IsSetDeviceName()
        {
            return (this.deviceNameField != null);
        }

        public bool IsSetEbs()
        {
            return (this.ebsField != null);
        }

        public InstanceBlockDeviceMapping WithDeviceName(string deviceName)
        {
            this.deviceNameField = deviceName;
            return this;
        }

        public InstanceBlockDeviceMapping WithEbs(InstanceEbsBlockDevice ebs)
        {
            this.ebsField = ebs;
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
        public InstanceEbsBlockDevice Ebs
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
    }
}

