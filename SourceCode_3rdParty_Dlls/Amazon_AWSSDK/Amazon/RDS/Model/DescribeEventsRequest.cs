namespace Amazon.RDS.Model
{
    using System;
    using System.Globalization;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://rds.amazonaws.com/admin/2009-10-16/", IsNullable=false)]
    public class DescribeEventsRequest
    {
        private decimal? durationField;
        private DateTime? endTimeField;
        private string markerField;
        private decimal? maxRecordsField;
        private string sourceIdentifierField;
        private string sourceTypeField;
        private DateTime? startTimeField;

        public bool IsSetDuration()
        {
            return this.durationField.HasValue;
        }

        public bool IsSetEndTime()
        {
            return this.endTimeField.HasValue;
        }

        public bool IsSetMarker()
        {
            return (this.markerField != null);
        }

        public bool IsSetMaxRecords()
        {
            return this.maxRecordsField.HasValue;
        }

        public bool IsSetSourceIdentifier()
        {
            return (this.sourceIdentifierField != null);
        }

        public bool IsSetSourceType()
        {
            return (this.sourceTypeField != null);
        }

        public bool IsSetStartTime()
        {
            return this.startTimeField.HasValue;
        }

        public DescribeEventsRequest WithDuration(decimal duration)
        {
            this.durationField = new decimal?(duration);
            return this;
        }

        public DescribeEventsRequest WithEndTime(DateTime endTime)
        {
            this.endTimeField = new DateTime?(endTime);
            return this;
        }

        public DescribeEventsRequest WithMarker(string marker)
        {
            this.markerField = marker;
            return this;
        }

        public DescribeEventsRequest WithMaxRecords(decimal maxRecords)
        {
            this.maxRecordsField = new decimal?(maxRecords);
            return this;
        }

        public DescribeEventsRequest WithSourceIdentifier(string sourceIdentifier)
        {
            this.sourceIdentifierField = sourceIdentifier;
            return this;
        }

        public DescribeEventsRequest WithSourceType(string sourceType)
        {
            this.sourceTypeField = sourceType;
            return this;
        }

        public DescribeEventsRequest WithStartTime(DateTime startTime)
        {
            this.startTimeField = new DateTime?(startTime);
            return this;
        }

        [XmlElement(ElementName="Duration")]
        public decimal Duration
        {
            get
            {
                return this.durationField.GetValueOrDefault();
            }
            set
            {
                this.durationField = new decimal?(value);
            }
        }

        [XmlElement(ElementName="EndTime")]
        public DateTime EndTime
        {
            get
            {
                return this.endTimeField.GetValueOrDefault();
            }
            set
            {
                this.endTimeField = new DateTime?(DateTime.ParseExact(value.ToString(), @"yyyy-MM-dd\THH:mm:ss.fff\Z", CultureInfo.InvariantCulture));
            }
        }

        [XmlElement(ElementName="Marker")]
        public string Marker
        {
            get
            {
                return this.markerField;
            }
            set
            {
                this.markerField = value;
            }
        }

        [XmlElement(ElementName="MaxRecords")]
        public decimal MaxRecords
        {
            get
            {
                return this.maxRecordsField.GetValueOrDefault();
            }
            set
            {
                this.maxRecordsField = new decimal?(value);
            }
        }

        [XmlElement(ElementName="SourceIdentifier")]
        public string SourceIdentifier
        {
            get
            {
                return this.sourceIdentifierField;
            }
            set
            {
                this.sourceIdentifierField = value;
            }
        }

        [XmlElement(ElementName="SourceType")]
        public string SourceType
        {
            get
            {
                return this.sourceTypeField;
            }
            set
            {
                this.sourceTypeField = value;
            }
        }

        [XmlElement(ElementName="StartTime")]
        public DateTime StartTime
        {
            get
            {
                return this.startTimeField.GetValueOrDefault();
            }
            set
            {
                this.startTimeField = new DateTime?(DateTime.ParseExact(value.ToString(), @"yyyy-MM-dd\THH:mm:ss.fff\Z", CultureInfo.InvariantCulture));
            }
        }
    }
}

