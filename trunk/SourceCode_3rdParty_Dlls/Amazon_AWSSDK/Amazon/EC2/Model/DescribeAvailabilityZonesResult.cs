namespace Amazon.EC2.Model
{
    using System;
    using System.Collections.Generic;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://ec2.amazonaws.com/doc/2009-11-30/", IsNullable=false)]
    public class DescribeAvailabilityZonesResult
    {
        private List<Amazon.EC2.Model.AvailabilityZone> availabilityZoneField;

        public bool IsSetAvailabilityZone()
        {
            return (this.AvailabilityZone.Count > 0);
        }

        public DescribeAvailabilityZonesResult WithAvailabilityZone(params Amazon.EC2.Model.AvailabilityZone[] list)
        {
            foreach (Amazon.EC2.Model.AvailabilityZone zone in list)
            {
                this.AvailabilityZone.Add(zone);
            }
            return this;
        }

        [XmlElement(ElementName="AvailabilityZone")]
        public List<Amazon.EC2.Model.AvailabilityZone> AvailabilityZone
        {
            get
            {
                if (this.availabilityZoneField == null)
                {
                    this.availabilityZoneField = new List<Amazon.EC2.Model.AvailabilityZone>();
                }
                return this.availabilityZoneField;
            }
            set
            {
                this.availabilityZoneField = value;
            }
        }
    }
}

