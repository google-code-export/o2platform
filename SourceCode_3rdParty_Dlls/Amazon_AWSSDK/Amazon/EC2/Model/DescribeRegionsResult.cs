namespace Amazon.EC2.Model
{
    using System;
    using System.Collections.Generic;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://ec2.amazonaws.com/doc/2009-11-30/", IsNullable=false)]
    public class DescribeRegionsResult
    {
        private List<Amazon.EC2.Model.Region> regionField;

        public bool IsSetRegion()
        {
            return (this.Region.Count > 0);
        }

        public DescribeRegionsResult WithRegion(params Amazon.EC2.Model.Region[] list)
        {
            foreach (Amazon.EC2.Model.Region region in list)
            {
                this.Region.Add(region);
            }
            return this;
        }

        [XmlElement(ElementName="Region")]
        public List<Amazon.EC2.Model.Region> Region
        {
            get
            {
                if (this.regionField == null)
                {
                    this.regionField = new List<Amazon.EC2.Model.Region>();
                }
                return this.regionField;
            }
            set
            {
                this.regionField = value;
            }
        }
    }
}

