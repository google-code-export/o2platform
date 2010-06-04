namespace Amazon.EC2.Model
{
    using System;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://ec2.amazonaws.com/doc/2009-11-30/", IsNullable=false)]
    public class PurchaseReservedInstancesOfferingResult
    {
        private string reservedInstancesIdField;

        public bool IsSetReservedInstancesId()
        {
            return (this.reservedInstancesIdField != null);
        }

        public PurchaseReservedInstancesOfferingResult WithReservedInstancesId(string reservedInstancesId)
        {
            this.reservedInstancesIdField = reservedInstancesId;
            return this;
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
    }
}

