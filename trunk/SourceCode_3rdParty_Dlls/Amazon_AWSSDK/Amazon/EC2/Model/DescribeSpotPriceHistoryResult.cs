namespace Amazon.EC2.Model
{
    using System;
    using System.Collections.Generic;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://ec2.amazonaws.com/doc/2009-11-30/", IsNullable=false)]
    public class DescribeSpotPriceHistoryResult
    {
        private List<Amazon.EC2.Model.SpotPriceHistory> spotPriceHistoryField;

        public bool IsSetSpotPriceHistory()
        {
            return (this.SpotPriceHistory.Count > 0);
        }

        public DescribeSpotPriceHistoryResult WithSpotPriceHistory(params Amazon.EC2.Model.SpotPriceHistory[] list)
        {
            foreach (Amazon.EC2.Model.SpotPriceHistory history in list)
            {
                this.SpotPriceHistory.Add(history);
            }
            return this;
        }

        [XmlElement(ElementName="SpotPriceHistory")]
        public List<Amazon.EC2.Model.SpotPriceHistory> SpotPriceHistory
        {
            get
            {
                if (this.spotPriceHistoryField == null)
                {
                    this.spotPriceHistoryField = new List<Amazon.EC2.Model.SpotPriceHistory>();
                }
                return this.spotPriceHistoryField;
            }
            set
            {
                this.spotPriceHistoryField = value;
            }
        }
    }
}

