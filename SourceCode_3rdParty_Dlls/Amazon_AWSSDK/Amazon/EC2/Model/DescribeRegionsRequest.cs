namespace Amazon.EC2.Model
{
    using System;
    using System.Collections.Generic;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://ec2.amazonaws.com/doc/2009-11-30/", IsNullable=false)]
    public class DescribeRegionsRequest
    {
        private List<string> regionNameField;

        public bool IsSetRegionName()
        {
            return (this.RegionName.Count > 0);
        }

        public DescribeRegionsRequest WithRegionName(params string[] list)
        {
            foreach (string str in list)
            {
                this.RegionName.Add(str);
            }
            return this;
        }

        [XmlElement(ElementName="RegionName")]
        public List<string> RegionName
        {
            get
            {
                if (this.regionNameField == null)
                {
                    this.regionNameField = new List<string>();
                }
                return this.regionNameField;
            }
            set
            {
                this.regionNameField = value;
            }
        }
    }
}

