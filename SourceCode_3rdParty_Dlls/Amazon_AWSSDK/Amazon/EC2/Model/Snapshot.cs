namespace Amazon.EC2.Model
{
    using System;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://ec2.amazonaws.com/doc/2009-11-30/", IsNullable=false)]
    public class Snapshot
    {
        private string descriptionField;
        private string ownerAliasField;
        private string ownerIdField;
        private string progressField;
        private string snapshotIdField;
        private string startTimeField;
        private string statusField;
        private string volumeIdField;
        private string volumeSizeField;

        public bool IsSetDescription()
        {
            return (this.descriptionField != null);
        }

        public bool IsSetOwnerAlias()
        {
            return (this.ownerAliasField != null);
        }

        public bool IsSetOwnerId()
        {
            return (this.ownerIdField != null);
        }

        public bool IsSetProgress()
        {
            return (this.progressField != null);
        }

        public bool IsSetSnapshotId()
        {
            return (this.snapshotIdField != null);
        }

        public bool IsSetStartTime()
        {
            return (this.startTimeField != null);
        }

        public bool IsSetStatus()
        {
            return (this.statusField != null);
        }

        public bool IsSetVolumeId()
        {
            return (this.volumeIdField != null);
        }

        public bool IsSetVolumeSize()
        {
            return (this.volumeSizeField != null);
        }

        public Snapshot WithDescription(string description)
        {
            this.descriptionField = description;
            return this;
        }

        public Snapshot WithOwnerAlias(string ownerAlias)
        {
            this.ownerAliasField = ownerAlias;
            return this;
        }

        public Snapshot WithOwnerId(string ownerId)
        {
            this.ownerIdField = ownerId;
            return this;
        }

        public Snapshot WithProgress(string progress)
        {
            this.progressField = progress;
            return this;
        }

        public Snapshot WithSnapshotId(string snapshotId)
        {
            this.snapshotIdField = snapshotId;
            return this;
        }

        public Snapshot WithStartTime(string startTime)
        {
            this.startTimeField = startTime;
            return this;
        }

        public Snapshot WithStatus(string status)
        {
            this.statusField = status;
            return this;
        }

        public Snapshot WithVolumeId(string volumeId)
        {
            this.volumeIdField = volumeId;
            return this;
        }

        public Snapshot WithVolumeSize(string volumeSize)
        {
            this.volumeSizeField = volumeSize;
            return this;
        }

        [XmlElement(ElementName="Description")]
        public string Description
        {
            get
            {
                return this.descriptionField;
            }
            set
            {
                this.descriptionField = value;
            }
        }

        [XmlElement(ElementName="OwnerAlias")]
        public string OwnerAlias
        {
            get
            {
                return this.ownerAliasField;
            }
            set
            {
                this.ownerAliasField = value;
            }
        }

        [XmlElement(ElementName="OwnerId")]
        public string OwnerId
        {
            get
            {
                return this.ownerIdField;
            }
            set
            {
                this.ownerIdField = value;
            }
        }

        [XmlElement(ElementName="Progress")]
        public string Progress
        {
            get
            {
                return this.progressField;
            }
            set
            {
                this.progressField = value;
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

        [XmlElement(ElementName="StartTime")]
        public string StartTime
        {
            get
            {
                return this.startTimeField;
            }
            set
            {
                this.startTimeField = value;
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

        [XmlElement(ElementName="VolumeSize")]
        public string VolumeSize
        {
            get
            {
                return this.volumeSizeField;
            }
            set
            {
                this.volumeSizeField = value;
            }
        }
    }
}

