namespace Amazon.EC2.Model
{
    using System;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://ec2.amazonaws.com/doc/2009-11-30/", IsNullable=false)]
    public class Region
    {
        private string endpointField;
        private string regionNameField;

        public bool IsSetEndpoint()
        {
            return (this.endpointField != null);
        }

        public bool IsSetRegionName()
        {
            return (this.regionNameField != null);
        }

        public Region WithEndpoint(string endpoint)
        {
            this.endpointField = endpoint;
            return this;
        }

        public Region WithRegionName(string regionName)
        {
            this.regionNameField = regionName;
            return this;
        }

        [XmlElement(ElementName="Endpoint")]
        public string Endpoint
        {
            get
            {
                return this.endpointField;
            }
            set
            {
                this.endpointField = value;
            }
        }

        [XmlElement(ElementName="RegionName")]
        public string RegionName
        {
            get
            {
                return this.regionNameField;
            }
            set
            {
                this.regionNameField = value;
            }
        }
    }
}

