namespace Amazon.EC2.Model
{
    using System;
    using System.Collections.Generic;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://ec2.amazonaws.com/doc/2009-11-30/", IsNullable=false)]
    public class Volume
    {
        private List<Amazon.EC2.Model.Attachment> attachmentField;
        private string availabilityZoneField;
        private string createTimeField;
        private string sizeField;
        private string snapshotIdField;
        private string statusField;
        private string volumeIdField;

        public bool IsSetAttachment()
        {
            return (this.Attachment.Count > 0);
        }

        public bool IsSetAvailabilityZone()
        {
            return (this.availabilityZoneField != null);
        }

        public bool IsSetCreateTime()
        {
            return (this.createTimeField != null);
        }

        public bool IsSetSize()
        {
            return (this.sizeField != null);
        }

        public bool IsSetSnapshotId()
        {
            return (this.snapshotIdField != null);
        }

        public bool IsSetStatus()
        {
            return (this.statusField != null);
        }

        public bool IsSetVolumeId()
        {
            return (this.volumeIdField != null);
        }

        public Volume WithAttachment(params Amazon.EC2.Model.Attachment[] list)
        {
            foreach (Amazon.EC2.Model.Attachment attachment in list)
            {
                this.Attachment.Add(attachment);
            }
            return this;
        }

        public Volume WithAvailabilityZone(string availabilityZone)
        {
            this.availabilityZoneField = availabilityZone;
            return this;
        }

        public Volume WithCreateTime(string createTime)
        {
            this.createTimeField = createTime;
            return this;
        }

        public Volume WithSize(string size)
        {
            this.sizeField = size;
            return this;
        }

        public Volume WithSnapshotId(string snapshotId)
        {
            this.snapshotIdField = snapshotId;
            return this;
        }

        public Volume WithStatus(string status)
        {
            this.statusField = status;
            return this;
        }

        public Volume WithVolumeId(string volumeId)
        {
            this.volumeIdField = volumeId;
            return this;
        }

        [XmlElement(ElementName="Attachment")]
        public List<Amazon.EC2.Model.Attachment> Attachment
        {
            get
            {
                if (this.attachmentField == null)
                {
                    this.attachmentField = new List<Amazon.EC2.Model.Attachment>();
                }
                return this.attachmentField;
            }
            set
            {
                this.attachmentField = value;
            }
        }

        [XmlElement(ElementName="AvailabilityZone")]
        public string AvailabilityZone
        {
            get
            {
                return this.availabilityZoneField;
            }
            set
            {
                this.availabilityZoneField = value;
            }
        }

        [XmlElement(ElementName="CreateTime")]
        public string CreateTime
        {
            get
            {
                return this.createTimeField;
            }
            set
            {
                this.createTimeField = value;
            }
        }

        [XmlElement(ElementName="Size")]
        public string Size
        {
            get
            {
                return this.sizeField;
            }
            set
            {
                this.sizeField = value;
            }
        }

        [XmlElement(ElementName="SnapshotId")]
        public string SnapshotId
        {
            get
            {
                return this.snapshotIdField;
            }
            set
            {
                this.snapshotIdField = value;
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

