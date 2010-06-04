namespace Amazon.RDS.Model
{
    using System;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://rds.amazonaws.com/admin/2009-10-16/", IsNullable=false)]
    public class DescribeEngineDefaultParametersRequest
    {
        private string engineField;
        private string markerField;
        private decimal? maxRecordsField;

        public bool IsSetEngine()
        {
            return (this.engineField != null);
        }

        public bool IsSetMarker()
        {
            return (this.markerField != null);
        }

        public bool IsSetMaxRecords()
        {
            return this.maxRecordsField.HasValue;
        }

        public DescribeEngineDefaultParametersRequest WithEngine(string engine)
        {
            this.engineField = engine;
            return this;
        }

        public DescribeEngineDefaultParametersRequest WithMarker(string marker)
        {
            this.markerField = marker;
            return this;
        }

        public DescribeEngineDefaultParametersRequest WithMaxRecords(decimal maxRecords)
        {
            this.maxRecordsField = new decimal?(maxRecords);
            return this;
        }

        [XmlElement(ElementName="Engine")]
        public string Engine
        {
            get
            {
                return this.engineField;
            }
            set
            {
                this.engineField = value;
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
    }
}

