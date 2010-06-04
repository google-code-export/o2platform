namespace Amazon.EC2.Model
{
    using System;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://ec2.amazonaws.com/doc/2009-11-30/", IsNullable=false)]
    public class ReservedInstances
    {
        private string availabilityZoneField;
        private decimal? durationField;
        private string fixedPriceField;
        private decimal? instanceCountField;
        private string instanceTypeField;
        private string productDescriptionField;
        private string purchaseStateField;
        private string reservedInstancesIdField;
        private string startTimeField;
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

        public bool IsSetInstanceCount()
        {
            return this.instanceCountField.HasValue;
        }

        public bool IsSetInstanceType()
        {
            return (this.instanceTypeField != null);
        }

        public bool IsSetProductDescription()
        {
            return (this.productDescriptionField != null);
        }

        public bool IsSetPurchaseState()
        {
            return (this.purchaseStateField != null);
        }

        public bool IsSetReservedInstancesId()
        {
            return (this.reservedInstancesIdField != null);
        }

        public bool IsSetStartTime()
        {
            return (this.startTimeField != null);
        }

        public bool IsSetUsagePrice()
        {
            return (this.usagePriceField != null);
        }

        public ReservedInstances WithAvailabilityZone(string availabilityZone)
        {
            this.availabilityZoneField = availabilityZone;
            return this;
        }

        public ReservedInstances WithDuration(decimal duration)
        {
            this.durationField = new decimal?(duration);
            return this;
        }

        public ReservedInstances WithFixedPrice(string fixedPrice)
        {
            this.fixedPriceField = fixedPrice;
            return this;
        }

        public ReservedInstances WithInstanceCount(decimal instanceCount)
        {
            this.instanceCountField = new decimal?(instanceCount);
            return this;
        }

        public ReservedInstances WithInstanceType(string instanceType)
        {
            this.instanceTypeField = instanceType;
            return this;
        }

        public ReservedInstances WithProductDescription(string productDescription)
        {
            this.productDescriptionField = productDescription;
            return this;
        }

        public ReservedInstances WithPurchaseState(string purchaseState)
        {
            this.purchaseStateField = purchaseState;
            return this;
        }

        public ReservedInstances WithReservedInstancesId(string reservedInstancesId)
        {
            this.reservedInstancesIdField = reservedInstancesId;
            return this;
        }

        public ReservedInstances WithStartTime(string startTime)
        {
            this.startTimeField = startTime;
            return this;
        }

        public ReservedInstances WithUsagePrice(string usagePrice)
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

        [XmlElement(ElementName="PurchaseState")]
        public string PurchaseState
        {
            get
            {
                return this.purchaseStateField;
            }
            set
            {
                this.purchaseStateField = value;
            }
        }

        [XmlElement(ElementName="ReservedInstancesId")]
        public string ReservedInstancesId
        {
            get
            {
                return this.reservedInstancesIdField;
            }
            set
            {
                this.reservedInstancesIdField = value;
            }
        }

        [XmlElement(ElementName="StartTime")]
        public string StartTime
        {
            get
            {
                return this.startTimeField;
            }
            set
            {
                this.startTimeField = value;
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

