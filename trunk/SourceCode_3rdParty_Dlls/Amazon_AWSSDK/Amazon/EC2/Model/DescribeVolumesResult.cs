namespace Amazon.EC2.Model
{
    using System;
    using System.Collections.Generic;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://ec2.amazonaws.com/doc/2009-11-30/", IsNullable=false)]
    public class DescribeVolumesResult
    {
        private List<Amazon.EC2.Model.Volume> volumeField;

        public bool IsSetVolume()
        {
            return (this.Volume.Count > 0);
        }

        public DescribeVolumesResult WithVolume(params Amazon.EC2.Model.Volume[] list)
        {
            foreach (Amazon.EC2.Model.Volume volume in list)
            {
                this.Volume.Add(volume);
            }
            return this;
        }

        [XmlElement(ElementName="Volume")]
        public List<Amazon.EC2.Model.Volume> Volume
        {
            get
            {
                if (this.volumeField == null)
                {
                    this.volumeField = new List<Amazon.EC2.Model.Volume>();
                }
                return this.volumeField;
            }
            set
            {
                this.volumeField = value;
            }
        }
    }
}

