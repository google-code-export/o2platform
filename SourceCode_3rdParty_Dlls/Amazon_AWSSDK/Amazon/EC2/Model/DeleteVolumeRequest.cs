namespace Amazon.EC2.Model
{
    using System;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://ec2.amazonaws.com/doc/2009-11-30/", IsNullable=false)]
    public class DeleteVolumeRequest
    {
        private string volumeIdField;

        public bool IsSetVolumeId()
        {
            return (this.volumeIdField != null);
        }

        public DeleteVolumeRequest WithVolumeId(string volumeId)
        {
            this.volumeIdField = volumeId;
            return this;
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

