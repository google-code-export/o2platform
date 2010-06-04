namespace Amazon.RDS.Model
{
    using System;
    using System.Collections.Generic;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://rds.amazonaws.com/admin/2009-10-16/", IsNullable=false)]
    public class DescribeDBParameterGroupsResult
    {
        private List<Amazon.RDS.Model.DBParameterGroup> DBParameterGroupField;
        private string markerField;

        public bool IsSetDBParameterGroup()
        {
            return (this.DBParameterGroup.Count > 0);
        }

        public bool IsSetMarker()
        {
            return (this.markerField != null);
        }

        public DescribeDBParameterGroupsResult WithDBParameterGroup(params Amazon.RDS.Model.DBParameterGroup[] list)
        {
            foreach (Amazon.RDS.Model.DBParameterGroup group in list)
            {
                this.DBParameterGroup.Add(group);
            }
            return this;
        }

        public DescribeDBParameterGroupsResult WithMarker(string marker)
        {
            this.markerField = marker;
            return this;
        }

        [XmlElement(ElementName="DBParameterGroup")]
        public List<Amazon.RDS.Model.DBParameterGroup> DBParameterGroup
        {
            get
            {
                if (this.DBParameterGroupField == null)
                {
                    this.DBParameterGroupField = new List<Amazon.RDS.Model.DBParameterGroup>();
                }
                return this.DBParameterGroupField;
            }
            set
            {
                this.DBParameterGroupField = value;
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
    }
}

