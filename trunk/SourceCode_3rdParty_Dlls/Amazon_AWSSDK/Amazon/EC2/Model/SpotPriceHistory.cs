namespace Amazon.EC2.Model
{
    using System;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://ec2.amazonaws.com/doc/2009-11-30/", IsNullable=false)]
    public class SpotPriceHistory
    {
        private string instanceTypeField;
        private string productDescriptionField;
        private string spotPriceField;
        private string timestampField;

        public bool IsSetInstanceType()
        {
            return (this.instanceTypeField != null);
        }

        public bool IsSetProductDescription()
        {
            return (this.productDescriptionField != null);
        }

        public bool IsSetSpotPrice()
        {
            return (this.spotPriceField != null);
        }

        public bool IsSetTimestamp()
        {
            return (this.timestampField != null);
        }

        public SpotPriceHistory WithInstanceType(string instanceType)
        {
            this.instanceTypeField = instanceType;
            return this;
        }

        public SpotPriceHistory WithProductDescription(string productDescription)
        {
            this.productDescriptionField = productDescription;
            return this;
        }

        public SpotPriceHistory WithSpotPrice(string spotPrice)
        {
            this.spotPriceField = spotPrice;
            return this;
        }

        public SpotPriceHistory WithTimestamp(string timestamp)
        {
            this.timestampField = timestamp;
            return this;
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

        [XmlElement(ElementName="Timestamp")]
        public string Timestamp
        {
            get
            {
                return this.timestampField;
            }
            set
            {
                this.timestampField = value;
            }
        }
    }
}

