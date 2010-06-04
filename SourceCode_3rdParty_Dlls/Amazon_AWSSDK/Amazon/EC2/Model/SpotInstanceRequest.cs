namespace Amazon.EC2.Model
{
    using System;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://ec2.amazonaws.com/doc/2009-11-30/", IsNullable=false)]
    public class SpotInstanceRequest
    {
        private string availabilityZoneGroupField;
        private string createTimeField;
        private SpotInstanceStateFault faultField;
        private string instanceIdField;
        private string launchGroupField;
        private Amazon.EC2.Model.LaunchSpecification launchSpecificationField;
        private string productDescriptionField;
        private string spotInstanceRequestIdField;
        private string spotPriceField;
        private string stateField;
        private string typeField;
        private string validFromField;
        private string validUntilField;

        public bool IsSetAvailabilityZoneGroup()
        {
            return (this.availabilityZoneGroupField != null);
        }

        public bool IsSetCreateTime()
        {
            return (this.createTimeField != null);
        }

        public bool IsSetFault()
        {
            return (this.faultField != null);
        }

        public bool IsSetInstanceId()
        {
            return (this.instanceIdField != null);
        }

        public bool IsSetLaunchGroup()
        {
            return (this.launchGroupField != null);
        }

        public bool IsSetLaunchSpecification()
        {
            return (this.launchSpecificationField != null);
        }

        public bool IsSetProductDescription()
        {
            return (this.productDescriptionField != null);
        }

        public bool IsSetSpotInstanceRequestId()
        {
            return (this.spotInstanceRequestIdField != null);
        }

        public bool IsSetSpotPrice()
        {
            return (this.spotPriceField != null);
        }

        public bool IsSetState()
        {
            return (this.stateField != null);
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

        public SpotInstanceRequest WithAvailabilityZoneGroup(string availabilityZoneGroup)
        {
            this.availabilityZoneGroupField = availabilityZoneGroup;
            return this;
        }

        public SpotInstanceRequest WithCreateTime(string createTime)
        {
            this.createTimeField = createTime;
            return this;
        }

        public SpotInstanceRequest WithFault(SpotInstanceStateFault fault)
        {
            this.faultField = fault;
            return this;
        }

        public SpotInstanceRequest WithInstanceId(string instanceId)
        {
            this.instanceIdField = instanceId;
            return this;
        }

        public SpotInstanceRequest WithLaunchGroup(string launchGroup)
        {
            this.launchGroupField = launchGroup;
            return this;
        }

        public SpotInstanceRequest WithLaunchSpecification(Amazon.EC2.Model.LaunchSpecification launchSpecification)
        {
            this.launchSpecificationField = launchSpecification;
            return this;
        }

        public SpotInstanceRequest WithProductDescription(string productDescription)
        {
            this.productDescriptionField = productDescription;
            return this;
        }

        public SpotInstanceRequest WithSpotInstanceRequestId(string spotInstanceRequestId)
        {
            this.spotInstanceRequestIdField = spotInstanceRequestId;
            return this;
        }

        public SpotInstanceRequest WithSpotPrice(string spotPrice)
        {
            this.spotPriceField = spotPrice;
            return this;
        }

        public SpotInstanceRequest WithState(string state)
        {
            this.stateField = state;
            return this;
        }

        public SpotInstanceRequest WithType(string type)
        {
            this.typeField = type;
            return this;
        }

        public SpotInstanceRequest WithValidFrom(string validFrom)
        {
            this.validFromField = validFrom;
            return this;
        }

        public SpotInstanceRequest WithValidUntil(string validUntil)
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

        [XmlElement(ElementName="CreateTime")]
        public string CreateTime
        {
            get
            {
                return this.createTimeField;
            }
            set
            {
                this.createTimeField = value;
            }
        }

        [XmlElement(ElementName="Fault")]
        public SpotInstanceStateFault Fault
        {
            get
            {
                return this.faultField;
            }
            set
            {
                this.faultField = value;
            }
        }

        [XmlElement(ElementName="InstanceId")]
        public string InstanceId
        {
            get
            {
                return this.instanceIdField;
            }
            set
            {
                this.instanceIdField = value;
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

        [XmlElement(ElementName="SpotInstanceRequestId")]
        public string SpotInstanceRequestId
        {
            get
            {
                return this.spotInstanceRequestIdField;
            }
            set
            {
                this.spotInstanceRequestIdField = value;
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

        [XmlElement(ElementName="State")]
        public string State
        {
            get
            {
                return this.stateField;
            }
            set
            {
                this.stateField = value;
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

