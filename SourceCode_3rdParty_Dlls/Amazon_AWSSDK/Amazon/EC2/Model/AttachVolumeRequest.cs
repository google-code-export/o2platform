namespace Amazon.EC2.Model
{
    using System;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://ec2.amazonaws.com/doc/2009-11-30/", IsNullable=false)]
    public class AttachVolumeRequest
    {
        private string deviceField;
        private string instanceIdField;
        private string volumeIdField;

        public bool IsSetDevice()
        {
            return (this.deviceField != null);
        }

        public bool IsSetInstanceId()
        {
            return (this.instanceIdField != null);
        }

        public bool IsSetVolumeId()
        {
            return (this.volumeIdField != null);
        }

        public AttachVolumeRequest WithDevice(string device)
        {
            this.deviceField = device;
            return this;
        }

        public AttachVolumeRequest WithInstanceId(string instanceId)
        {
            this.instanceIdField = instanceId;
            return this;
        }

        public AttachVolumeRequest WithVolumeId(string volumeId)
        {
            this.volumeIdField = volumeId;
            return this;
        }

        [XmlElement(ElementName="Device")]
        public string Device
        {
            get
            {
                return this.deviceField;
            }
            set
            {
                this.deviceField = value;
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

        [XmlElement(ElementName="VolumeId")]
        public string VolumeId
        {
            get
            {
                return this.volumeIdField;
            }
            set
            {
                this.volumeIdField = value;
            }
        }
    }
}

