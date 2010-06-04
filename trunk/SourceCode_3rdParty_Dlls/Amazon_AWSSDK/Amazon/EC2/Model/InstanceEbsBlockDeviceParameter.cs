namespace Amazon.EC2.Model
{
    using System;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://ec2.amazonaws.com/doc/2009-11-30/", IsNullable=false)]
    public class InstanceEbsBlockDeviceParameter
    {
        private bool? deleteOnTerminationField;
        private string volumeIdField;

        public bool IsSetDeleteOnTermination()
        {
            return this.deleteOnTerminationField.HasValue;
        }

        public bool IsSetVolumeId()
        {
            return (this.volumeIdField != null);
        }

        public InstanceEbsBlockDeviceParameter WithDeleteOnTermination(bool deleteOnTermination)
        {
            this.deleteOnTerminationField = new bool?(deleteOnTermination);
            return this;
        }

        public InstanceEbsBlockDeviceParameter WithVolumeId(string volumeId)
        {
            this.volumeIdField = volumeId;
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

