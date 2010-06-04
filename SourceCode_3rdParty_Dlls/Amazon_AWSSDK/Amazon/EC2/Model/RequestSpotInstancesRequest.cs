namespace Amazon.EC2.Model
{
    using System;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://ec2.amazonaws.com/doc/2009-11-30/", IsNullable=false)]
    public class RequestSpotInstancesRequest
    {
        private string availabilityZoneGroupField;
        private decimal? instanceCountField;
        private string launchGroupField;
        private Amazon.EC2.Model.LaunchSpecification launchSpecificationField;
        private string spotPriceField;
        private string typeField;
        private string validFromField;
        private string validUntilField;

        public bool IsSetAvailabilityZoneGroup()
        {
            return (this.availabilityZoneGroupField != null);
        }

        public bool IsSetInstanceCount()
        {
            return this.instanceCountField.HasValue;
        }

        public bool IsSetLaunchGroup()
        {
            return (this.launchGroupField != null);
        }

        public bool IsSetLaunchSpecification()
        {
            return (this.launchSpecificationField != null);
        }

        public bool IsSetSpotPrice()
        {
            return (this.spotPriceField != null);
        }

        public bool IsSetType()
        {
            return (this.typeField != null);
        }

        public bool IsSetValidFrom()
        {
            return (this.validFromField != null);
        }

        public bool IsSetValidUntil()
        {
            return (this.validUntilField != null);
        }

        public RequestSpotInstancesRequest WithAvailabilityZoneGroup(string availabilityZoneGroup)
        {
            this.availabilityZoneGroupField = availabilityZoneGroup;
            return this;
        }

        public RequestSpotInstancesRequest WithInstanceCount(decimal instanceCount)
        {
            this.instanceCountField = new decimal?(instanceCount);
            return this;
        }

        public RequestSpotInstancesRequest WithLaunchGroup(string launchGroup)
        {
            this.launchGroupField = launchGroup;
            return this;
        }

        public RequestSpotInstancesRequest WithLaunchSpecification(Amazon.EC2.Model.LaunchSpecification launchSpecification)
        {
            this.launchSpecificationField = launchSpecification;
            return this;
        }

        public RequestSpotInstancesRequest WithSpotPrice(string spotPrice)
        {
            this.spotPriceField = spotPrice;
            return this;
        }

        public RequestSpotInstancesRequest WithType(string type)
        {
            this.typeField = type;
            return this;
        }

        public RequestSpotInstancesRequest WithValidFrom(string validFrom)
        {
            this.validFromField = validFrom;
            return this;
        }

        public RequestSpotInstancesRequest WithValidUntil(string validUntil)
        {
            this.validUntilField = validUntil;
            return this;
        }

        [XmlElement(ElementName="AvailabilityZoneGroup")]
        public string AvailabilityZoneGroup
        {
            get
            {
                return this.availabilityZoneGroupField;
            }
            set
            {
                this.availabilityZoneGroupField = value;
            }
        }

        [XmlElement(ElementName="InstanceCount")]
        public decimal InstanceCount
        {
            get
            {
                return this.instanceCountField.GetValueOrDefault();
            }
            set
            {
                this.instanceCountField = new decimal?(value);
            }
        }

        [XmlElement(ElementName="LaunchGroup")]
        public string LaunchGroup
        {
            get
            {
                return this.launchGroupField;
            }
            set
            {
                this.launchGroupField = value;
            }
        }

        [XmlElement(ElementName="LaunchSpecification")]
        public Amazon.EC2.Model.LaunchSpecification LaunchSpecification
        {
            get
            {
                return this.launchSpecificationField;
            }
            set
            {
                this.launchSpecificationField = value;
            }
        }

        [XmlElement(ElementName="SpotPrice")]
        public string SpotPrice
        {
            get
            {
                return this.spotPriceField;
            }
            set
            {
                this.spotPriceField = value;
            }
        }

        [XmlElement(ElementName="Type")]
        public string Type
        {
            get
            {
                return this.typeField;
            }
            set
            {
                this.typeField = value;
            }
        }

        [XmlElement(ElementName="ValidFrom")]
        public string ValidFrom
        {
            get
            {
                return this.validFromField;
            }
            set
            {
                this.validFromField = value;
            }
        }

        [XmlElement(ElementName="ValidUntil")]
        public string ValidUntil
        {
            get
            {
                return this.validUntilField;
            }
            set
            {
                this.validUntilField = value;
            }
        }
    }
}

