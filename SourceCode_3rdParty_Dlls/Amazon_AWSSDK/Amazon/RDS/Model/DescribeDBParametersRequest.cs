namespace Amazon.RDS.Model
{
    using System;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://rds.amazonaws.com/admin/2009-10-16/", IsNullable=false)]
    public class DescribeDBParametersRequest
    {
        private string DBParameterGroupNameField;
        private string markerField;
        private decimal? maxRecordsField;
        private string sourceField;

        public bool IsSetDBParameterGroupName()
        {
            return (this.DBParameterGroupNameField != null);
        }

        public bool IsSetMarker()
        {
            return (this.markerField != null);
        }

        public bool IsSetMaxRecords()
        {
            return this.maxRecordsField.HasValue;
        }

        public bool IsSetSource()
        {
            return (this.sourceField != null);
        }

        public DescribeDBParametersRequest WithDBParameterGroupName(string DBParameterGroupName)
        {
            this.DBParameterGroupNameField = DBParameterGroupName;
            return this;
        }

        public DescribeDBParametersRequest WithMarker(string marker)
        {
            this.markerField = marker;
            return this;
        }

        public DescribeDBParametersRequest WithMaxRecords(decimal maxRecords)
        {
            this.maxRecordsField = new decimal?(maxRecords);
            return this;
        }

        public DescribeDBParametersRequest WithSource(string source)
        {
            this.sourceField = source;
            return this;
        }

        [XmlElement(ElementName="DBParameterGroupName")]
        public string DBParameterGroupName
        {
            get
            {
                return this.DBParameterGroupNameField;
            }
            set
            {
                this.DBParameterGroupNameField = value;
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

        [XmlElement(ElementName="Source")]
        public string Source
        {
            get
            {
                return this.sourceField;
            }
            set
            {
                this.sourceField = value;
            }
        }
    }
}

