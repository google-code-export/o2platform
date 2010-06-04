namespace Amazon.RDS.Model
{
    using System;
    using System.Collections.Generic;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://rds.amazonaws.com/admin/2009-10-16/", IsNullable=false)]
    public class DescribeDBInstancesResult
    {
        private List<Amazon.RDS.Model.DBInstance> DBInstanceField;
        private string markerField;

        public bool IsSetDBInstance()
        {
            return (this.DBInstance.Count > 0);
        }

        public bool IsSetMarker()
        {
            return (this.markerField != null);
        }

        public DescribeDBInstancesResult WithDBInstance(params Amazon.RDS.Model.DBInstance[] list)
        {
            foreach (Amazon.RDS.Model.DBInstance instance in list)
            {
                this.DBInstance.Add(instance);
            }
            return this;
        }

        public DescribeDBInstancesResult WithMarker(string marker)
        {
            this.markerField = marker;
            return this;
        }

        [XmlElement(ElementName="DBInstance")]
        public List<Amazon.RDS.Model.DBInstance> DBInstance
        {
            get
            {
                if (this.DBInstanceField == null)
                {
                    this.DBInstanceField = new List<Amazon.RDS.Model.DBInstance>();
                }
                return this.DBInstanceField;
            }
            set
            {
                this.DBInstanceField = value;
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

