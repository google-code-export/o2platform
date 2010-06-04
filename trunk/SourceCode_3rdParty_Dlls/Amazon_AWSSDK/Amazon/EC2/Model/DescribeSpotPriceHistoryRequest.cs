namespace Amazon.EC2.Model
{
    using System;
    using System.Collections.Generic;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://ec2.amazonaws.com/doc/2009-11-30/", IsNullable=false)]
    public class DescribeSpotPriceHistoryRequest
    {
        private string endTimeField;
        private List<string> instanceTypeField;
        private List<string> productDescriptionField;
        private string startTimeField;

        public bool IsSetEndTime()
        {
            return (this.endTimeField != null);
        }

        public bool IsSetInstanceType()
        {
            return (this.InstanceType.Count > 0);
        }

        public bool IsSetProductDescription()
        {
            return (this.ProductDescription.Count > 0);
        }

        public bool IsSetStartTime()
        {
            return (this.startTimeField != null);
        }

        public DescribeSpotPriceHistoryRequest WithEndTime(string endTime)
        {
            this.endTimeField = endTime;
            return this;
        }

        public DescribeSpotPriceHistoryRequest WithInstanceType(params string[] list)
        {
            foreach (string str in list)
            {
                this.InstanceType.Add(str);
            }
            return this;
        }

        public DescribeSpotPriceHistoryRequest WithProductDescription(params string[] list)
        {
            foreach (string str in list)
            {
                this.ProductDescription.Add(str);
            }
            return this;
        }

        public DescribeSpotPriceHistoryRequest WithStartTime(string startTime)
        {
            this.startTimeField = startTime;
            return this;
        }

        [XmlElement(ElementName="EndTime")]
        public string EndTime
        {
            get
            {
                return this.endTimeField;
            }
            set
            {
                this.endTimeField = value;
            }
        }

        [XmlElement(ElementName="InstanceType")]
        public List<string> InstanceType
        {
            get
            {
                if (this.instanceTypeField == null)
                {
                    this.instanceTypeField = new List<string>();
                }
                return this.instanceTypeField;
            }
            set
            {
                this.instanceTypeField = value;
            }
        }

        [XmlElement(ElementName="ProductDescription")]
        public List<string> ProductDescription
        {
            get
            {
                if (this.productDescriptionField == null)
                {
                    this.productDescriptionField = new List<string>();
                }
                return this.productDescriptionField;
            }
            set
            {
                this.productDescriptionField = value;
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
    }
}

