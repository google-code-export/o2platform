namespace Amazon.EC2.Model
{
    using System;
    using System.IO;
    using System.Text;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://ec2.amazonaws.com/doc/2009-11-30/", IsNullable=false)]
    public class PurchaseReservedInstancesOfferingResponse
    {
        private Amazon.EC2.Model.PurchaseReservedInstancesOfferingResult purchaseReservedInstancesOfferingResultField;
        private Amazon.EC2.Model.ResponseMetadata responseMetadataField;

        public bool IsSetPurchaseReservedInstancesOfferingResult()
        {
            return (this.purchaseReservedInstancesOfferingResultField != null);
        }

        public bool IsSetResponseMetadata()
        {
            return (this.responseMetadataField != null);
        }

        public string ToXML()
        {
            StringBuilder sb = new StringBuilder(0x400);
            XmlSerializer serializer = new XmlSerializer(base.GetType());
            using (StringWriter writer = new StringWriter(sb))
            {
                serializer.Serialize((TextWriter) writer, this);
            }
            return sb.ToString();
        }

        public PurchaseReservedInstancesOfferingResponse WithPurchaseReservedInstancesOfferingResult(Amazon.EC2.Model.PurchaseReservedInstancesOfferingResult purchaseReservedInstancesOfferingResult)
        {
            this.purchaseReservedInstancesOfferingResultField = purchaseReservedInstancesOfferingResult;
            return this;
        }

        public PurchaseReservedInstancesOfferingResponse WithResponseMetadata(Amazon.EC2.Model.ResponseMetadata responseMetadata)
        {
            this.responseMetadataField = responseMetadata;
            return this;
        }

        [XmlElement(ElementName="PurchaseReservedInstancesOfferingResult")]
        public Amazon.EC2.Model.PurchaseReservedInstancesOfferingResult PurchaseReservedInstancesOfferingResult
        {
            get
            {
                return this.purchaseReservedInstancesOfferingResultField;
            }
            set
            {
                this.purchaseReservedInstancesOfferingResultField = value;
            }
        }

        [XmlElement(ElementName="ResponseMetadata")]
        public Amazon.EC2.Model.ResponseMetadata ResponseMetadata
        {
            get
            {
                return this.responseMetadataField;
            }
            set
            {
                this.responseMetadataField = value;
            }
        }
    }
}

