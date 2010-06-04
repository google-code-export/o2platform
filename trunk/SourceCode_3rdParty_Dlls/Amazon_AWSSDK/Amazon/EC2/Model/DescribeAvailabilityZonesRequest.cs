namespace Amazon.EC2.Model
{
    using System;
    using System.Collections.Generic;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://ec2.amazonaws.com/doc/2009-11-30/", IsNullable=false)]
    public class DescribeAvailabilityZonesRequest
    {
        private List<string> zoneNameField;

        public bool IsSetZoneName()
        {
            return (this.ZoneName.Count > 0);
        }

        public DescribeAvailabilityZonesRequest WithZoneName(params string[] list)
        {
            foreach (string str in list)
            {
                this.ZoneName.Add(str);
            }
            return this;
        }

        [XmlElement(ElementName="ZoneName")]
        public List<string> ZoneName
        {
            get
            {
                if (this.zoneNameField == null)
                {
                    this.zoneNameField = new List<string>();
                }
                return this.zoneNameField;
            }
            set
            {
                this.zoneNameField = value;
            }
        }
    }
}

