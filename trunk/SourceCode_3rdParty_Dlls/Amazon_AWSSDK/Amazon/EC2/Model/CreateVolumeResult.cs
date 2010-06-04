namespace Amazon.EC2.Model
{
    using System;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://ec2.amazonaws.com/doc/2009-11-30/", IsNullable=false)]
    public class CreateVolumeResult
    {
        private Amazon.EC2.Model.Volume volumeField;

        public bool IsSetVolume()
        {
            return (this.volumeField != null);
        }

        public CreateVolumeResult WithVolume(Amazon.EC2.Model.Volume volume)
        {
            this.volumeField = volume;
            return this;
        }

        [XmlElement(ElementName="Volume")]
        public Amazon.EC2.Model.Volume Volume
        {
            get
            {
                return this.volumeField;
            }
            set
            {
                this.volumeField = value;
            }
        }
    }
}

