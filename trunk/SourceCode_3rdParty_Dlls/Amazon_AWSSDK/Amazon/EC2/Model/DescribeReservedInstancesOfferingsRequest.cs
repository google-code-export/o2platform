namespace Amazon.EC2.Model
{
    using System;
    using System.Collections.Generic;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://ec2.amazonaws.com/doc/2009-11-30/", IsNullable=false)]
    public class DescribeReservedInstancesOfferingsRequest
    {
        private string availabilityZoneField;
        private string instanceTypeField;
        private string productDescriptionField;
        private List<string> reservedInstancesIdField;

        public bool IsSetAvailabilityZone()
        {
            return (this.availabilityZoneField != null);
        }

        public bool IsSetInstanceType()
        {
            return (this.instanceTypeField != null);
        }

        public bool IsSetProductDescription()
        {
            return (this.productDescriptionField != null);
        }

        public bool IsSetReservedInstancesId()
        {
            return (this.ReservedInstancesId.Count > 0);
        }

        public DescribeReservedInstancesOfferingsRequest WithAvailabilityZone(string availabilityZone)
        {
            this.availabilityZoneField = availabilityZone;
            return this;
        }

        public DescribeReservedInstancesOfferingsRequest WithInstanceType(string instanceType)
        {
            this.instanceTypeField = instanceType;
            return this;
        }

        public DescribeReservedInstancesOfferingsRequest WithProductDescription(string productDescription)
        {
            this.productDescriptionField = productDescription;
            return this;
        }

        public DescribeReservedInstancesOfferingsRequest WithReservedInstancesId(params string[] list)
        {
            foreach (string str in list)
            {
                this.ReservedInstancesId.Add(str);
            }
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

        [XmlElement(ElementName="ReservedInstancesId")]
        public List<string> ReservedInstancesId
        {
            get
            {
                if (this.reservedInstancesIdField == null)
                {
                    this.reservedInstancesIdField = new List<string>();
                }
                return this.reservedInstancesIdField;
            }
            set
            {
                this.reservedInstancesIdField = value;
            }
        }
    }
}

