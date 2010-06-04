namespace Amazon.EC2.Model
{
    using System;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://ec2.amazonaws.com/doc/2009-11-30/", IsNullable=false)]
    public class ReservedInstancesOffering
    {
        private string availabilityZoneField;
        private decimal? durationField;
        private string fixedPriceField;
        private string instanceTypeField;
        private string productDescriptionField;
        private string reservedInstancesOfferingIdField;
        private string usagePriceField;

        public bool IsSetAvailabilityZone()
        {
            return (this.availabilityZoneField != null);
        }

        public bool IsSetDuration()
        {
            return this.durationField.HasValue;
        }

        public bool IsSetFixedPrice()
        {
            return (this.fixedPriceField != null);
        }

        public bool IsSetInstanceType()
        {
            return (this.instanceTypeField != null);
        }

        public bool IsSetProductDescription()
        {
            return (this.productDescriptionField != null);
        }

        public bool IsSetReservedInstancesOfferingId()
        {
            return (this.reservedInstancesOfferingIdField != null);
        }

        public bool IsSetUsagePrice()
        {
            return (this.usagePriceField != null);
        }

        public ReservedInstancesOffering WithAvailabilityZone(string availabilityZone)
        {
            this.availabilityZoneField = availabilityZone;
            return this;
        }

        public ReservedInstancesOffering WithDuration(decimal duration)
        {
            this.durationField = new decimal?(duration);
            return this;
        }

        public ReservedInstancesOffering WithFixedPrice(string fixedPrice)
        {
            this.fixedPriceField = fixedPrice;
            return this;
        }

        public ReservedInstancesOffering WithInstanceType(string instanceType)
        {
            this.instanceTypeField = instanceType;
            return this;
        }

        public ReservedInstancesOffering WithProductDescription(string productDescription)
        {
            this.productDescriptionField = productDescription;
            return this;
        }

        public ReservedInstancesOffering WithReservedInstancesOfferingId(string reservedInstancesOfferingId)
        {
            this.reservedInstancesOfferingIdField = reservedInstancesOfferingId;
            return this;
        }

        public ReservedInstancesOffering WithUsagePrice(string usagePrice)
        {
            this.usagePriceField = usagePrice;
            return this;
        }

        [XmlElement(ElementName="AvailabilityZone")]
        public string AvailabilityZone
        {
            get
            {
                return this.availabilityZoneField;
            }
            set
            {
                this.availabilityZoneField = value;
            }
        }

        [XmlElement(ElementName="Duration")]
        public decimal Duration
        {
            get
            {
                return this.durationField.GetValueOrDefault();
            }
            set
            {
                this.durationField = new decimal?(value);
            }
        }

        [XmlElement(ElementName="FixedPrice")]
        public string FixedPrice
        {
            get
            {
                return this.fixedPriceField;
            }
            set
            {
                this.fixedPriceField = value;
            }
        }

        [XmlElement(ElementName="InstanceType")]
        public string InstanceType
        {
            get
            {
                return this.instanceTypeField;
            }
            set
            {
                this.instanceTypeField = value;
            }
        }

        [XmlElement(ElementName="ProductDescription")]
        public string ProductDescription
        {
            get
            {
                return this.productDescriptionField;
            }
            set
            {
                this.productDescriptionField = value;
            }
        }

        [XmlElement(ElementName="ReservedInstancesOfferingId")]
        public string ReservedInstancesOfferingId
        {
            get
            {
                return this.reservedInstancesOfferingIdField;
            }
            set
            {
                this.reservedInstancesOfferingIdField = value;
            }
        }

        [XmlElement(ElementName="UsagePrice")]
        public string UsagePrice
        {
            get
            {
                return this.usagePriceField;
            }
            set
            {
                this.usagePriceField = value;
            }
        }
    }
}

