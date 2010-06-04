namespace Amazon.EC2.Model
{
    using System;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://ec2.amazonaws.com/doc/2009-11-30/", IsNullable=false)]
    public class EbsBlockDevice
    {
        private bool? deleteOnTerminationField;
        private string snapshotIdField;
        private decimal? volumeSizeField;

        public bool IsSetDeleteOnTermination()
        {
            return this.deleteOnTerminationField.HasValue;
        }

        public bool IsSetSnapshotId()
        {
            return (this.snapshotIdField != null);
        }

        public bool IsSetVolumeSize()
        {
            return this.volumeSizeField.HasValue;
        }

        public EbsBlockDevice WithDeleteOnTermination(bool deleteOnTermination)
        {
            this.deleteOnTerminationField = new bool?(deleteOnTermination);
            return this;
        }

        public EbsBlockDevice WithSnapshotId(string snapshotId)
        {
            this.snapshotIdField = snapshotId;
            return this;
        }

        public EbsBlockDevice WithVolumeSize(decimal volumeSize)
        {
            this.volumeSizeField = new decimal?(volumeSize);
            return this;
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

        [XmlElement(ElementName="VolumeSize")]
        public decimal VolumeSize
        {
            get
            {
                return this.volumeSizeField.GetValueOrDefault();
            }
            set
            {
                this.volumeSizeField = new decimal?(value);
            }
        }
    }
}

