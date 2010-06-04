namespace Amazon.CloudWatch.Model
{
    using System;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://monitoring.amazonaws.com/doc/2009-05-15/", IsNullable=false)]
    public class Datapoint
    {
        private double? averageField;
        private string customUnitField;
        private double? maximumField;
        private double? minimumField;
        private double? samplesField;
        private double? sumField;
        private string timestampField;
        private string unitField;

        public bool IsSetAverage()
        {
            return this.averageField.HasValue;
        }

        public bool IsSetCustomUnit()
        {
            return (this.customUnitField != null);
        }

        public bool IsSetMaximum()
        {
            return this.maximumField.HasValue;
        }

        public bool IsSetMinimum()
        {
            return this.minimumField.HasValue;
        }

        public bool IsSetSamples()
        {
            return this.samplesField.HasValue;
        }

        public bool IsSetSum()
        {
            return this.sumField.HasValue;
        }

        public bool IsSetTimestamp()
        {
            return (this.timestampField != null);
        }

        public bool IsSetUnit()
        {
            return (this.unitField != null);
        }

        public Datapoint WithAverage(double average)
        {
            this.averageField = new double?(average);
            return this;
        }

        public Datapoint WithCustomUnit(string customUnit)
        {
            this.customUnitField = customUnit;
            return this;
        }

        public Datapoint WithMaximum(double maximum)
        {
            this.maximumField = new double?(maximum);
            return this;
        }

        public Datapoint WithMinimum(double minimum)
        {
            this.minimumField = new double?(minimum);
            return this;
        }

        public Datapoint WithSamples(double samples)
        {
            this.samplesField = new double?(samples);
            return this;
        }

        public Datapoint WithSum(double sum)
        {
            this.sumField = new double?(sum);
            return this;
        }

        public Datapoint WithTimestamp(string timestamp)
        {
            this.timestampField = timestamp;
            return this;
        }

        public Datapoint WithUnit(string unit)
        {
            this.unitField = unit;
            return this;
        }

        [XmlElement(ElementName="Average")]
        public double Average
        {
            get
            {
                return this.averageField.GetValueOrDefault();
            }
            set
            {
                this.averageField = new double?(value);
            }
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

        [XmlElement(ElementName="Maximum")]
        public double Maximum
        {
            get
            {
                return this.maximumField.GetValueOrDefault();
            }
            set
            {
                this.maximumField = new double?(value);
            }
        }

        [XmlElement(ElementName="Minimum")]
        public double Minimum
        {
            get
            {
                return this.minimumField.GetValueOrDefault();
            }
            set
            {
                this.minimumField = new double?(value);
            }
        }

        [XmlElement(ElementName="Samples")]
        public double Samples
        {
            get
            {
                return this.samplesField.GetValueOrDefault();
            }
            set
            {
                this.samplesField = new double?(value);
            }
        }

        [XmlElement(ElementName="Sum")]
        public double Sum
        {
            get
            {
                return this.sumField.GetValueOrDefault();
            }
            set
            {
                this.sumField = new double?(value);
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

