namespace Amazon.RDS.Model
{
    using System;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://rds.amazonaws.com/admin/2009-10-16/", IsNullable=false)]
    public class DescribeDBInstancesRequest
    {
        private string DBInstanceIdentifierField;
        private string markerField;
        private decimal? maxRecordsField;

        public bool IsSetDBInstanceIdentifier()
        {
            return (this.DBInstanceIdentifierField != null);
        }

        public bool IsSetMarker()
        {
            return (this.markerField != null);
        }

        public bool IsSetMaxRecords()
        {
            return this.maxRecordsField.HasValue;
        }

        public DescribeDBInstancesRequest WithDBInstanceIdentifier(string DBInstanceIdentifier)
        {
            this.DBInstanceIdentifierField = DBInstanceIdentifier;
            return this;
        }

        public DescribeDBInstancesRequest WithMarker(string marker)
        {
            this.markerField = marker;
            return this;
        }

        public DescribeDBInstancesRequest WithMaxRecords(decimal maxRecords)
        {
            this.maxRecordsField = new decimal?(maxRecords);
            return this;
        }

        [XmlElement(ElementName="DBInstanceIdentifier")]
        public string DBInstanceIdentifier
        {
            get
            {
                return this.DBInstanceIdentifierField;
            }
            set
            {
                this.DBInstanceIdentifierField = value;
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

