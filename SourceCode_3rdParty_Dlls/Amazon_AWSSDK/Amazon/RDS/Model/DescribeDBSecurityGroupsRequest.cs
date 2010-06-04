namespace Amazon.RDS.Model
{
    using System;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://rds.amazonaws.com/admin/2009-10-16/", IsNullable=false)]
    public class DescribeDBSecurityGroupsRequest
    {
        private string DBSecurityGroupNameField;
        private string markerField;
        private decimal? maxRecordsField;

        public bool IsSetDBSecurityGroupName()
        {
            return (this.DBSecurityGroupNameField != null);
        }

        public bool IsSetMarker()
        {
            return (this.markerField != null);
        }

        public bool IsSetMaxRecords()
        {
            return this.maxRecordsField.HasValue;
        }

        public DescribeDBSecurityGroupsRequest WithDBSecurityGroupName(string DBSecurityGroupName)
        {
            this.DBSecurityGroupNameField = DBSecurityGroupName;
            return this;
        }

        public DescribeDBSecurityGroupsRequest WithMarker(string marker)
        {
            this.markerField = marker;
            return this;
        }

        public DescribeDBSecurityGroupsRequest WithMaxRecords(decimal maxRecords)
        {
            this.maxRecordsField = new decimal?(maxRecords);
            return this;
        }

        [XmlElement(ElementName="DBSecurityGroupName")]
        public string DBSecurityGroupName
        {
            get
            {
                return this.DBSecurityGroupNameField;
            }
            set
            {
                this.DBSecurityGroupNameField = value;
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

