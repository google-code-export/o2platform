namespace Amazon.CloudWatch.Model
{
    using System;
    using System.Collections.Generic;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://monitoring.amazonaws.com/doc/2009-05-15/", IsNullable=false)]
    public class GetMetricStatisticsRequest
    {
        private string customUnitField;
        private List<Dimension> dimensionsField;
        private string endTimeField;
        private string measureNameField;
        private string namespaceValueField;
        private decimal? periodField;
        private string startTimeField;
        private List<string> statisticsField;
        private string unitField;

        public bool IsSetCustomUnit()
        {
            return (this.customUnitField != null);
        }

        public bool IsSetDimensions()
        {
            return (this.Dimensions.Count > 0);
        }

        public bool IsSetEndTime()
        {
            return (this.endTimeField != null);
        }

        public bool IsSetMeasureName()
        {
            return (this.measureNameField != null);
        }

        public bool IsSetNamespace()
        {
            return (this.namespaceValueField != null);
        }

        public bool IsSetPeriod()
        {
            return this.periodField.HasValue;
        }

        public bool IsSetStartTime()
        {
            return (this.startTimeField != null);
        }

        public bool IsSetStatistics()
        {
            return (this.Statistics.Count > 0);
        }

        public bool IsSetUnit()
        {
            return (this.unitField != null);
        }

        public GetMetricStatisticsRequest WithCustomUnit(string customUnit)
        {
            this.customUnitField = customUnit;
            return this;
        }

        public GetMetricStatisticsRequest WithDimensions(params Dimension[] list)
        {
            foreach (Dimension dimension in list)
            {
                this.Dimensions.Add(dimension);
            }
            return this;
        }

        public GetMetricStatisticsRequest WithEndTime(string endTime)
        {
            this.endTimeField = endTime;
            return this;
        }

        public GetMetricStatisticsRequest WithMeasureName(string measureName)
        {
            this.measureNameField = measureName;
            return this;
        }

        public GetMetricStatisticsRequest WithNamespace(string namespaceValue)
        {
            this.namespaceValueField = namespaceValue;
            return this;
        }

        public GetMetricStatisticsRequest WithPeriod(decimal period)
        {
            this.periodField = new decimal?(period);
            return this;
        }

        public GetMetricStatisticsRequest WithStartTime(string startTime)
        {
            this.startTimeField = startTime;
            return this;
        }

        public GetMetricStatisticsRequest WithStatistics(params string[] list)
        {
            foreach (string str in list)
            {
                this.Statistics.Add(str);
            }
            return this;
        }

        public GetMetricStatisticsRequest WithUnit(string unit)
        {
            this.unitField = unit;
            return this;
        }

        [XmlElement(ElementName="CustomUnit")]
        public string CustomUnit
        {
            get
            {
                return this.customUnitField;
            }
            set
            {
                this.customUnitField = value;
            }
        }

        [XmlElement(ElementName="Dimensions")]
        public List<Dimension> Dimensions
        {
            get
            {
                if (this.dimensionsField == null)
                {
                    this.dimensionsField = new List<Dimension>();
                }
                return this.dimensionsField;
            }
            set
            {
                this.dimensionsField = value;
            }
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

        [XmlElement(ElementName="MeasureName")]
        public string MeasureName
        {
            get
            {
                return this.measureNameField;
            }
            set
            {
                this.measureNameField = value;
            }
        }

        [XmlElement(ElementName="Namespace")]
        public string Namespace
        {
            get
            {
                return this.namespaceValueField;
            }
            set
            {
                this.namespaceValueField = value;
            }
        }

        [XmlElement(ElementName="Period")]
        public decimal Period
        {
            get
            {
                return this.periodField.GetValueOrDefault();
            }
            set
            {
                this.periodField = new decimal?(value);
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

        [XmlElement(ElementName="Statistics")]
        public List<string> Statistics
        {
            get
            {
                if (this.statisticsField == null)
                {
                    this.statisticsField = new List<string>();
                }
                return this.statisticsField;
            }
            set
            {
                this.statisticsField = value;
            }
        }

        [XmlElement(ElementName="Unit")]
        public string Unit
        {
            get
            {
                return this.unitField;
            }
            set
            {
                this.unitField = value;
            }
        }
    }
}

