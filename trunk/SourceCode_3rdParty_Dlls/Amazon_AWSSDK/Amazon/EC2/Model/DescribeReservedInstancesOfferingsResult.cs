namespace Amazon.EC2.Model
{
    using System;
    using System.Collections.Generic;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://ec2.amazonaws.com/doc/2009-11-30/", IsNullable=false)]
    public class DescribeReservedInstancesOfferingsResult
    {
        private List<Amazon.EC2.Model.ReservedInstancesOffering> reservedInstancesOfferingField;

        public bool IsSetReservedInstancesOffering()
        {
            return (this.ReservedInstancesOffering.Count > 0);
        }

        public DescribeReservedInstancesOfferingsResult WithReservedInstancesOffering(params Amazon.EC2.Model.ReservedInstancesOffering[] list)
        {
            foreach (Amazon.EC2.Model.ReservedInstancesOffering offering in list)
            {
                this.ReservedInstancesOffering.Add(offering);
            }
            return this;
        }

        [XmlElement(ElementName="ReservedInstancesOffering")]
        public List<Amazon.EC2.Model.ReservedInstancesOffering> ReservedInstancesOffering
        {
            get
            {
                if (this.reservedInstancesOfferingField == null)
                {
                    this.reservedInstancesOfferingField = new List<Amazon.EC2.Model.ReservedInstancesOffering>();
                }
                return this.reservedInstancesOfferingField;
            }
            set
            {
                this.reservedInstancesOfferingField = value;
            }
        }
    }
}

