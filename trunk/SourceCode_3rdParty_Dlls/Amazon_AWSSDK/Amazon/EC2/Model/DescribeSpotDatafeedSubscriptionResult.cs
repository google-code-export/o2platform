namespace Amazon.EC2.Model
{
    using System;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://ec2.amazonaws.com/doc/2009-11-30/", IsNullable=false)]
    public class DescribeSpotDatafeedSubscriptionResult
    {
        private Amazon.EC2.Model.SpotDatafeedSubscription spotDatafeedSubscriptionField;

        public bool IsSetSpotDatafeedSubscription()
        {
            return (this.spotDatafeedSubscriptionField != null);
        }

        public DescribeSpotDatafeedSubscriptionResult WithSpotDatafeedSubscription(Amazon.EC2.Model.SpotDatafeedSubscription spotDatafeedSubscription)
        {
            this.spotDatafeedSubscriptionField = spotDatafeedSubscription;
            return this;
        }

        [XmlElement(ElementName="SpotDatafeedSubscription")]
        public Amazon.EC2.Model.SpotDatafeedSubscription SpotDatafeedSubscription
        {
            get
            {
                return this.spotDatafeedSubscriptionField;
            }
            set
            {
                this.spotDatafeedSubscriptionField = value;
            }
        }
    }
}

