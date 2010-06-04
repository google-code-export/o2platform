namespace Amazon.EC2.Model
{
    using System;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://ec2.amazonaws.com/doc/2009-11-30/", IsNullable=false)]
    public class Attachment
    {
        private string attachTimeField;
        private bool? deleteOnTerminationField;
        private string deviceField;
        private string instanceIdField;
        private string statusField;
        private string volumeIdField;

        public bool IsSetAttachTime()
        {
            return (this.attachTimeField != null);
        }

        public bool IsSetDeleteOnTermination()
        {
            return this.deleteOnTerminationField.HasValue;
        }

        public bool IsSetDevice()
        {
            return (this.deviceField != null);
        }

        public bool IsSetInstanceId()
        {
            return (this.instanceIdField != null);
        }

        public bool IsSetStatus()
        {
            return (this.statusField != null);
        }

        public bool IsSetVolumeId()
        {
            return (this.volumeIdField != null);
        }

        public Attachment WithAttachTime(string attachTime)
        {
            this.attachTimeField = attachTime;
            return this;
        }

        public Attachment WithDeleteOnTermination(bool deleteOnTermination)
        {
            this.deleteOnTerminationField = new bool?(deleteOnTermination);
            return this;
        }

        public Attachment WithDevice(string device)
        {
            this.deviceField = device;
            return this;
        }

        public Attachment WithInstanceId(string instanceId)
        {
            this.instanceIdField = instanceId;
            return this;
        }

        public Attachment WithStatus(string status)
        {
            this.statusField = status;
            return this;
        }

        public Attachment WithVolumeId(string volumeId)
        {
            this.volumeIdField = volumeId;
            return this;
        }

        [XmlElement(ElementName="AttachTime")]
        public string AttachTime
        {
            get
            {
                return this.attachTimeField;
            }
            set
            {
                this.attachTimeField = value;
            }
        }

        [XmlElement(ElementName="DeleteOnTermination")]
        public bool DeleteOnTermination
        {
            get
            {
                return this.deleteOnTerminationField.GetValueOrDefault();
            }
            set
            {
                this.deleteOnTerminationField = new bool?(value);
            }
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

        [XmlElement(ElementName="Status")]
        public string Status
        {
            get
            {
                return this.statusField;
            }
            set
            {
                this.statusField = value;
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

