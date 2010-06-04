namespace Amazon.RDS.Model
{
    using System;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://rds.amazonaws.com/admin/2009-10-16/", IsNullable=false)]
    public class DescribeDBParameterGroupsRequest
    {
        private string DBParameterGroupNameField;
        private string markerField;
        private decimal? maxRecordsField;

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

        public DescribeDBParameterGroupsRequest WithDBParameterGroupName(string DBParameterGroupName)
        {
            this.DBParameterGroupNameField = DBParameterGroupName;
            return this;
        }

        public DescribeDBParameterGroupsRequest WithMarker(string marker)
        {
            this.markerField = marker;
            return this;
        }

        public DescribeDBParameterGroupsRequest WithMaxRecords(decimal maxRecords)
        {
            this.maxRecordsField = new decimal?(maxRecords);
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
    }
}

