namespace Amazon.RDS.Model
{
    using System;
    using System.Collections.Generic;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://rds.amazonaws.com/admin/2009-10-16/", IsNullable=false)]
    public class DescribeDBSecurityGroupsResult
    {
        private List<Amazon.RDS.Model.DBSecurityGroup> DBSecurityGroupField;
        private string markerField;

        public bool IsSetDBSecurityGroup()
        {
            return (this.DBSecurityGroup.Count > 0);
        }

        public bool IsSetMarker()
        {
            return (this.markerField != null);
        }

        public DescribeDBSecurityGroupsResult WithDBSecurityGroup(params Amazon.RDS.Model.DBSecurityGroup[] list)
        {
            foreach (Amazon.RDS.Model.DBSecurityGroup group in list)
            {
                this.DBSecurityGroup.Add(group);
            }
            return this;
        }

        public DescribeDBSecurityGroupsResult WithMarker(string marker)
        {
            this.markerField = marker;
            return this;
        }

        [XmlElement(ElementName="DBSecurityGroup")]
        public List<Amazon.RDS.Model.DBSecurityGroup> DBSecurityGroup
        {
            get
            {
                if (this.DBSecurityGroupField == null)
                {
                    this.DBSecurityGroupField = new List<Amazon.RDS.Model.DBSecurityGroup>();
                }
                return this.DBSecurityGroupField;
            }
            set
            {
                this.DBSecurityGroupField = value;
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

