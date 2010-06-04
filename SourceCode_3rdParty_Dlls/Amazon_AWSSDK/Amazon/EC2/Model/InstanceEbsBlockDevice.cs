namespace Amazon.EC2.Model
{
    using System;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://ec2.amazonaws.com/doc/2009-11-30/", IsNullable=false)]
    public class InstanceEbsBlockDevice
    {
        private string attachTimeField;
        private bool? deleteOnTerminationField;
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

        public bool IsSetStatus()
        {
            return (this.statusField != null);
        }

        public bool IsSetVolumeId()
        {
            return (this.volumeIdField != null);
        }

        public InstanceEbsBlockDevice WithAttachTime(string attachTime)
        {
            this.attachTimeField = attachTime;
            return this;
        }

        public InstanceEbsBlockDevice WithDeleteOnTermination(bool deleteOnTermination)
        {
            this.deleteOnTerminationField = new bool?(deleteOnTermination);
            return this;
        }

        public InstanceEbsBlockDevice WithStatus(string status)
        {
            this.statusField = status;
            return this;
        }

        public InstanceEbsBlockDevice WithVolumeId(string volumeId)
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

