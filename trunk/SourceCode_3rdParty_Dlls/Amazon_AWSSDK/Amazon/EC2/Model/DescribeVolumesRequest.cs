namespace Amazon.EC2.Model
{
    using System;
    using System.Collections.Generic;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://ec2.amazonaws.com/doc/2009-11-30/", IsNullable=false)]
    public class DescribeVolumesRequest
    {
        private List<string> volumeIdField;

        public bool IsSetVolumeId()
        {
            return (this.VolumeId.Count > 0);
        }

        public DescribeVolumesRequest WithVolumeId(params string[] list)
        {
            foreach (string str in list)
            {
                this.VolumeId.Add(str);
            }
            return this;
        }

        [XmlElement(ElementName="VolumeId")]
        public List<string> VolumeId
        {
            get
            {
                if (this.volumeIdField == null)
                {
                    this.volumeIdField = new List<string>();
                }
                return this.volumeIdField;
            }
            set
            {
                this.volumeIdField = value;
            }
        }
    }
}

