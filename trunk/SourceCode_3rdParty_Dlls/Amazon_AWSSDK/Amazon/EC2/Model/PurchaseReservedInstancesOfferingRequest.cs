namespace Amazon.EC2.Model
{
    using System;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://ec2.amazonaws.com/doc/2009-11-30/", IsNullable=false)]
    public class PurchaseReservedInstancesOfferingRequest
    {
        private string instanceCountField;
        private string reservedInstancesOfferingIdField;

        public bool IsSetInstanceCount()
        {
            return (this.instanceCountField != null);
        }

        public bool IsSetReservedInstancesOfferingId()
        {
            return (this.reservedInstancesOfferingIdField != null);
        }

        public PurchaseReservedInstancesOfferingRequest WithInstanceCount(string instanceCount)
        {
            this.instanceCountField = instanceCount;
            return this;
        }

        public PurchaseReservedInstancesOfferingRequest WithReservedInstancesOfferingId(string reservedInstancesOfferingId)
        {
            this.reservedInstancesOfferingIdField = reservedInstancesOfferingId;
            return this;
        }

        [XmlElement(ElementName="InstanceCount")]
        public string InstanceCount
        {
            get
            {
                return this.instanceCountField;
            }
            set
            {
                this.instanceCountField = value;
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
    }
}

